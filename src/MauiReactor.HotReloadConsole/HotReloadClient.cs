using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClient
    {
        private readonly Options _options;
        private readonly string _workingDirectory;
        private readonly string _projFileName;

        private record ParsedFileInfo(string FilePath, SyntaxTree SyntaxTree, DateTime LastModified);

        private enum FileChangeKind
        { 
            Changed,

            Created,

            Deleted,

            Renamed
        }

        private record FileChangeNotification(string FilePath, FileChangeKind FileChangeKind, string? OldFilePath = null);

        private readonly BufferBlock<FileChangeNotification> _fileChangedQueue = new();

        private MSBuildWorkspace? _workspace;
        private Project? _project;
        private Compilation? _projectCompilation;
        private readonly Dictionary<string, ParsedFileInfo> _parsedFiles = new();

        static HotReloadClient()
        {
            MSBuildLocator.RegisterDefaults();
        }

        public HotReloadClient(Options options)
        {
            _options = options;
            _workingDirectory = _options.WorkingDirectory ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException();

            if (_options.ProjectFileName == null)
            {
                var projFiles = Directory.GetFiles(_workingDirectory, "*.csproj");
                if (projFiles.Length == 1)
                {
                    _projFileName = projFiles[0];
                }
                else
                {
                    throw new InvalidOperationException($"Directory {_workingDirectory} contains {projFiles.Length} projects");
                }
            }
            else
            {
                if (Path.IsPathFullyQualified(_options.ProjectFileName))
                {
                    _projFileName = _options.ProjectFileName;
                }
                else
                {
                    _projFileName = Path.Combine(_workingDirectory, _options.ProjectFileName);
                }
            }

            if (Path.GetExtension(_projFileName) == ".csproj")
            {
                _projFileName = Path.GetFileNameWithoutExtension(_projFileName);
            }
            else
            {
                _projFileName = Path.GetFileName(_projFileName);
            }
        }

        private bool IsAndroidTargetFramework() => _project?.Name.EndsWith("(net6.0-android)") ?? throw new InvalidOperationException();

        public async Task Run(CancellationToken cancellationToken)
        {
            await SetupProjectCompilation(cancellationToken);

            if (IsAndroidTargetFramework())
            {
                if (!ExecutePortForwardCommmand())
                {
                    return;
                }
            }

            Task.WaitAll(
                ConnectionLoop(cancellationToken),
                FolderMonitorLoop(cancellationToken));
        }

        private async Task ConnectionLoop(CancellationToken cancellationToken)
        {
            using var assemblyMemoryStream = new MemoryStream();
            using var assemblyPdbMemoryStream = new MemoryStream();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    while (await _fileChangedQueue.OutputAvailableAsync(cancellationToken).ConfigureAwait(false))
                    {
                        now = DateTime.Now;

                        if (_fileChangedQueue.TryReceiveAll(out var notifications))
                        {
                            bool requireHotReload = await HandleFileChangeNotifications(notifications, cancellationToken);

                            if (requireHotReload)
                            {
                                break;
                            }
                        };
                    }


                    assemblyMemoryStream.Position = 0;
                    assemblyPdbMemoryStream.Position = 0;

                    if (!CompileProjectInMemory(assemblyMemoryStream, assemblyPdbMemoryStream, cancellationToken))
                    {
                        continue;
                    }

                    await SendAssemblyFileToServer(assemblyMemoryStream, assemblyPdbMemoryStream, cancellationToken);

                    Console.WriteLine($"Hot-Reload completed in {(DateTime.Now - now).TotalMilliseconds} ms");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to hot reload the application:{Environment.NewLine}{ex}");
                    continue;
                }
            }
        }

        private async Task FolderMonitorLoop(CancellationToken cancellationToken)
        {
            using FileSystemWatcher watcher = new(_workingDirectory + Path.DirectorySeparatorChar, "*.cs")
            { 
                IncludeSubdirectories = true,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.LastAccess | NotifyFilters.FileName,
            };

            var tcs = new TaskCompletionSource<bool>();

            void changedHandler(object s, FileSystemEventArgs e)
            {
                Console.WriteLine($"Detected changes to '{Path.GetRelativePath(_workingDirectory, e.FullPath)}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Changed));
                }
            }

            void createdHandler(object s, FileSystemEventArgs e)
            {
                Console.WriteLine($"Detected creation of '{Path.GetRelativePath(_workingDirectory, e.FullPath)}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Created));
                }
            }

            void renamedHandler(object s, RenamedEventArgs e)
            {
                Console.WriteLine($"Detected file rename from '{Path.GetRelativePath(_workingDirectory, e.OldFullPath)}' to '{Path.GetRelativePath(_workingDirectory, e.FullPath)}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Renamed, e.OldFullPath));
                }
            }

            void deletedHandler(object s, FileSystemEventArgs e)
            {
                Console.WriteLine($"Detected delete of '{Path.GetRelativePath(_workingDirectory, e.FullPath)}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Deleted));
                }
            }

            watcher.Changed += changedHandler;
            watcher.Created += createdHandler;
            watcher.Renamed += renamedHandler;
            watcher.Deleted += deletedHandler;

            cancellationToken.Register(() => tcs.SetCanceled());

            Console.WriteLine($"Monitoring folder '{_workingDirectory}'...");

            await tcs.Task;

            watcher.Changed -= changedHandler;
            watcher.Created -= createdHandler;
            watcher.Renamed -= renamedHandler;
            watcher.Deleted -= deletedHandler;
        }

        private async Task SetupProjectCompilation(CancellationToken cancellationToken)
        {
            Console.Write($"Setting up build pipeline for {_projFileName} project...");

            _workspace ??= MSBuildWorkspace.Create();

            _workspace.CloseSolution();

            _project = await _workspace.OpenProjectAsync(Path.Combine(_workingDirectory, $"{_projFileName}.csproj"), cancellationToken: cancellationToken);
            _projectCompilation = await _project.GetCompilationAsync(cancellationToken);

            if (_projectCompilation == null)
            {
                throw new InvalidOperationException();
            }
            
            _parsedFiles.Clear();

            foreach (var syntaxTree in _projectCompilation.SyntaxTrees)
            {
                _parsedFiles[syntaxTree.FilePath] = new ParsedFileInfo(syntaxTree.FilePath, syntaxTree, File.GetLastWriteTime(syntaxTree.FilePath));
            }

            Console.WriteLine("done.");
        }

        private async Task<bool> HandleFileChangeNotifications(IEnumerable<FileChangeNotification> notifications, CancellationToken cancellationToken)
        {
            if (_projectCompilation == null)
            {
                throw new InvalidOperationException();
            }

            if (notifications.Any(_ => 
                _.FileChangeKind == FileChangeKind.Created || 
                _.FileChangeKind == FileChangeKind.Deleted ||
                _.FileChangeKind == FileChangeKind.Renamed && !_parsedFiles.ContainsKey(_.FilePath)))
            {
                await SetupProjectCompilation(cancellationToken);
                return true;
            }

            bool requiredHotReload = false;

            foreach (var notification in notifications)
            {
                if (!_parsedFiles.TryGetValue(notification.FilePath, out var parsedFileInfo))
                {
                    //this is a notification for a file not included in the project according to
                    //the current solution configuration
                    return false;
                };

                var currentFileLastWriteTime = File.GetLastWriteTime(notification.FilePath);

                if (parsedFileInfo.LastModified != currentFileLastWriteTime)
                {
                    var newSyntaxTree = SyntaxFactory.ParseSyntaxTree(
                        await ReadAllTextFileAsync(notification.FilePath, cancellationToken), cancellationToken: cancellationToken);

                    Console.WriteLine($"Replacing syntax tree for: {Path.GetRelativePath(_workingDirectory, notification.FilePath)}");

                    _projectCompilation = _projectCompilation.ReplaceSyntaxTree(parsedFileInfo.SyntaxTree, newSyntaxTree);

                    _parsedFiles[notification.FilePath] = new ParsedFileInfo(notification.FilePath, newSyntaxTree, currentFileLastWriteTime);

                    requiredHotReload = true;
                }
            }


            return requiredHotReload;
        }

        private static async Task<string> ReadAllTextFileAsync(string filePath, CancellationToken cancellationToken)
        {
            int retry = 3;
            while (retry >= 0)
            {
                try
                {
                    return await File.ReadAllTextAsync(filePath, cancellationToken);
                }
                catch (IOException)
                {
                    retry--;
                }
                catch (Exception)
                {
                    throw;
                }                
            }

            throw new InvalidOperationException($"Unable to access file {filePath}");
        }

        private bool CompileProjectInMemory(Stream stream, Stream pdbStream, CancellationToken cancellationToken)
        {
            if (_projectCompilation == null)
            {
                throw new InvalidOperationException();
            }

            //Dictionary<SyntaxTree, SyntaxTree>? replaceTreeMap = new();

            //foreach (var syntaxTree in _projectCompilation.SyntaxTrees)
            //{
            //    if (_parsedFiles.TryGetValue(syntaxTree, out var parsedFileInfo))
            //    { 
            //        var currentWriteTimeStamp = File.GetLastWriteTime(parsedFileInfo.FilePath);

            //        if (currentWriteTimeStamp != parsedFileInfo.LastModified)
            //        {
            //            var newSyntaxTree = SyntaxFactory.ParseSyntaxTree(
            //                await File.ReadAllTextAsync(parsedFileInfo.FilePath, cancellationToken), cancellationToken: cancellationToken);
                        
            //            _parsedFiles.Remove(syntaxTree);
            //            _parsedFiles.Add(newSyntaxTree, new ParsedFileInfo(parsedFileInfo.FilePath, newSyntaxTree, currentWriteTimeStamp));                    

            //            Console.WriteLine($"Replacing syntax tree for: {Path.GetRelativePath(_workingDirectory, parsedFileInfo.FilePath)}");

            //            replaceTreeMap.Add(syntaxTree, newSyntaxTree);
            //        }
            //    }
            //}

            //foreach (var (oldTree, newTree) in replaceTreeMap)
            //{
            //    _projectCompilation = _projectCompilation.ReplaceSyntaxTree(oldTree, newTree);
            //}

            var result = _projectCompilation.Emit(stream, pdbStream, cancellationToken: cancellationToken);

            if (!result.Success)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Diagnostics.Select(_ => _.GetMessage())));
            }

            return result.Success;
        }

        private async Task SendAssemblyFileToServer(MemoryStream stream, MemoryStream pdbStream, CancellationToken cancellationToken)
        {
            using var tcpClient = new TcpClient();

            var serverPort = IsAndroidTargetFramework() ? 45820 : 45821;

            Console.Write($"Connecting to Hot-Reload server (Port: {serverPort})...");
            await tcpClient.ConnectAsync(IPAddress.Loopback, serverPort, cancellationToken);

            Console.WriteLine($"connected.");

            using NetworkStream networkStream = tcpClient.GetStream();

            var lengthBytes = BitConverter.GetBytes((int)stream.Length);

            await networkStream.WriteAsync(lengthBytes, cancellationToken);

            await networkStream.WriteAsync(stream.GetBuffer(), 0, (int)stream.Length, cancellationToken);

            await networkStream.FlushAsync(cancellationToken);

            if (pdbStream != null)
            {
                lengthBytes = BitConverter.GetBytes((int)pdbStream.Length);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.WriteAsync(pdbStream.GetBuffer(), 0, (int)pdbStream.Length, cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }
            else
            {
                lengthBytes = BitConverter.GetBytes(0);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }

            var booleanBuffer = new byte[1];
            if (await networkStream.ReadAsync(booleanBuffer, 0, 1, cancellationToken) == 0)
                throw new SocketException();
        }

        private static bool ExecutePortForwardCommmand()
        {
            var adbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Android", "android-sdk", "platform-tools", "adb.exe");

            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "forward tcp:45820 tcp:45820";
            process.StartInfo.FileName = adbPath;

            try
            {
                process.Start();

                var adb_output = process.StandardOutput.ReadToEnd();

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator (executing '{adbPath} forward tcp:45820 tcp:45820' adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

    }
}
