using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MauiReactor.HotReloadConsole
{
    internal interface IHotReloadClient
    {
        Task Startup(CancellationToken cancellationToken);

        Task Run(CancellationToken cancellationToken);
    }

    internal abstract partial class HotReloadClient : IHotReloadClient
    {
        protected readonly Options _options;
        protected readonly string _workingDirectory;
        protected readonly string _projFileName;
        protected MSBuildWorkspace? _workspace;
        protected Project? _project;


        protected enum FileChangeKind
        { 
            Changed,

            Created,

            Deleted,

            Renamed
        }

        protected record FileChangeNotification(string FilePath, FileChangeKind FileChangeKind, string? OldFilePath = null);

        private readonly BufferBlock<FileChangeNotification> _fileChangedQueue = new();

        protected HotReloadClient(Options options)
        {
            _options = options;
            _workingDirectory = _options.WorkingDirectory ?? Directory.GetCurrentDirectory();

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
                    _workingDirectory = Path.GetDirectoryName(_options.ProjectFileName) ?? throw new InvalidOperationException("Unable to determine working directory from project file path.");
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

        protected static Regex _frameworkRegex = GenerateFrameworkRegex();


        [GeneratedRegex("(?<project>[^\\(]+)\\((?<framework>[\\w|\\.|\\-]+)\\)", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex GenerateFrameworkRegex();

        //public string TargetFramework => _frameworkRegex.Match(_project?.Name ?? throw new InvalidOperationException()).Groups["framework"].Value;

        protected bool IsAndroidTargetFramework() => _options.Framework?.Contains("android") ?? throw new InvalidOperationException();

        public virtual Task Startup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Run(CancellationToken cancellationToken)
        {
            if (IsAndroidTargetFramework())
            {
                if (!ExecutePortForwardCommand())
                {
                    return Task.CompletedTask;
                }
            }

            Task.WaitAll([ConnectionLoop(cancellationToken), FolderMonitorLoop(cancellationToken)], cancellationToken: cancellationToken);

            return Task.CompletedTask;
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

                    if (!await CompileProject(assemblyMemoryStream, assemblyPdbMemoryStream, cancellationToken))
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
                var relativePath = Path.GetRelativePath(_workingDirectory, e.FullPath);
                if (relativePath.StartsWith("obj"))
                {
                    return;
                }

                Console.WriteLine($"Detected changes to '{relativePath}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Changed));
                }
            }

            void createdHandler(object s, FileSystemEventArgs e)
            {
                var relativePath = Path.GetRelativePath(_workingDirectory, e.FullPath);
                if (relativePath.StartsWith("obj"))
                {
                    return;
                }
                
                Console.WriteLine($"Detected creation of '{relativePath}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Created));
                }
            }

            void renamedHandler(object s, RenamedEventArgs e)
            {
                var relativePath = Path.GetRelativePath(_workingDirectory, e.FullPath);
                if (relativePath.StartsWith("obj"))
                {
                    return;
                }

                Console.WriteLine($"Detected file rename from '{Path.GetRelativePath(_workingDirectory, e.OldFullPath)}' to '{relativePath}'");

                if (string.Compare(Path.GetExtension(e.FullPath), ".cs", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _fileChangedQueue.Post(new FileChangeNotification(e.FullPath, FileChangeKind.Renamed, e.OldFullPath));
                }
            }

            void deletedHandler(object s, FileSystemEventArgs e)
            {
                var relativePath = Path.GetRelativePath(_workingDirectory, e.FullPath);
                if (relativePath.StartsWith("obj"))
                {
                    return;
                }

                Console.WriteLine($"Detected delete of '{relativePath}'");

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

        protected abstract Task<bool> HandleFileChangeNotifications(IEnumerable<FileChangeNotification> notifications, CancellationToken cancellationToken);

        protected static async Task<string> ReadAllTextFileAsync(string filePath, CancellationToken cancellationToken)
        {
            int retry = 3;
            while (retry >= 0)
            {
                try
                {
                    using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var sr = new StreamReader(fs, Encoding.Default);
                    return await sr.ReadToEndAsync();
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

        protected abstract Task<bool> CompileProject(Stream stream, Stream pdbStream, CancellationToken cancellationToken);

        private async Task SendAssemblyFileToServer(MemoryStream stream, MemoryStream pdbStream, CancellationToken cancellationToken)
        {
            using var tcpClient = new TcpClient();

            var serverPort = IsAndroidTargetFramework() ? 45820 : 45821;


            var hostAddress = IPAddress.Loopback;
            if (!string.IsNullOrWhiteSpace(_options.Host))
            {
                if (!IPAddress.TryParse(_options.Host, out hostAddress))
                {
                    throw new InvalidOperationException($"Invalid host ip: {_options.Host}");
                }    
            }

            Console.Write($"Connecting to Hot-Reload server ({hostAddress}:{serverPort})...");

            await tcpClient.ConnectAsync(hostAddress, serverPort, cancellationToken);

            Console.WriteLine($"connected.");

            using NetworkStream networkStream = tcpClient.GetStream();

            var lengthBytes = BitConverter.GetBytes((int)stream.Length);

            await networkStream.WriteAsync(lengthBytes, cancellationToken);

            await networkStream.WriteAsync(stream.GetBuffer().AsMemory(0, (int)stream.Length), cancellationToken);

            await networkStream.FlushAsync(cancellationToken);

            if (pdbStream != null)
            {
                lengthBytes = BitConverter.GetBytes((int)pdbStream.Length);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.WriteAsync(pdbStream.GetBuffer().AsMemory(0, (int)pdbStream.Length), cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }
            else
            {
                lengthBytes = BitConverter.GetBytes(0);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }

            var booleanBuffer = new byte[1];
            if (await networkStream.ReadAsync(booleanBuffer.AsMemory(0, 1), cancellationToken) == 0)
                throw new SocketException();
        }

        private static bool ExecutePortForwardCommand()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return ExecuteMacPortForwardCommand();
            }
            else
            {
                return ExecuteWindowsPortForwardCommand();
            }
        }

        private static bool ExecuteWindowsPortForwardCommand()
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
                var adb_error = process.StandardError.ReadToEnd();

                if (adb_error != null && adb_error.Length > 0)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator (executing '{adbPath} forward tcp:45820 tcp:45820' adb tool returned '{adb_error}')");

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator (executing '{adbPath} forward tcp:45820 tcp:45820' adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(process.StandardOutput.ReadToEnd());
                //Console.WriteLine(process.StandardError.ReadToEnd());
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        
        private static bool ExecuteMacPortForwardCommand()
        {
            var adbCommandLine = "adb forward tcp:45820 tcp:45820";

            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = string.Format("-c \"{0}\"", adbCommandLine);
            process.StartInfo.FileName = "/bin/bash";

            try
            {
                process.Start();

                var adb_output = process.StandardOutput.ReadToEnd();

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator, is emulator running? (adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{process.StandardOutput.ReadToEnd()}{Environment.NewLine}{process.StandardError.ReadToEnd()}{Environment.NewLine}{ex}");
                return false;
            }

            return true;
        }
    }
}
