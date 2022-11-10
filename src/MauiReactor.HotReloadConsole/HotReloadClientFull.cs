using System.IO;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClientFull : HotReloadClient
    {
        static HotReloadClientFull()
        {
            MSBuildLocator.RegisterDefaults();
        }

        public HotReloadClientFull(Options options) : base(options)
        {
        }


        protected override async Task<bool> CompileProject(Stream stream, Stream pdbStream, CancellationToken cancellationToken)
        {
            var diagnosticEntries = new Dictionary<string, List<Diagnostic>>();

            var exitCode = PlatformSupport.ExecuteShellCommand("dotnet", $"build -f {_options.Framework} --no-restore --no-dependencies", (s, e) =>
            {
                Console.WriteLine(e.Data);
            }, (s, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Data);
                }
            },
            false, _workingDirectory, false);


            if (_project?.OutputFilePath == null)
            {
                throw new InvalidOperationException();
            }

            using var fs = new FileStream(_project.OutputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            await fs.CopyToAsync(stream, cancellationToken);

            var pdbFileOutput = Path.Combine(Path.GetDirectoryName(_project.OutputFilePath) ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(_project.OutputFilePath)) + ".pdb";

            if (File.Exists(pdbFileOutput))
            {
                using var fsPdb = new FileStream(pdbFileOutput, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                await fsPdb.CopyToAsync(pdbStream, cancellationToken);
            }

            return true;
        }

        public override async Task Startup(CancellationToken cancellationToken)
        {
            await SetupProjectCompilation(cancellationToken);
        }

        private async Task SetupProjectCompilation(CancellationToken cancellationToken)
        {
            Console.Write($"Setting up build pipeline (full mode) for {_projFileName} project...");

            var properties = new Dictionary<string, string>
            {
                { "Configuration", "Debug" },
                { "TargetFramework", _options.Framework ?? throw new InvalidOperationException() }
            };

            _workspace ??= MSBuildWorkspace.Create(properties);

            _workspace.CloseSolution();

            _project = await _workspace.OpenProjectAsync(Path.Combine(_workingDirectory, $"{_projFileName}.csproj"), cancellationToken: cancellationToken);

            Console.WriteLine("done.");

            
        }

        protected override Task<bool> HandleFileChangeNotifications(IEnumerable<FileChangeNotification> notifications, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

    }

    public enum ShellExecutorType
    {
        Generic,
        Windows,
        Unix
    }
    public struct ShellExecuteResult
    {
        public int ExitCode { get; set; }
        public string Output { get; set; }
        public string ErrorOutput { get; set; }
    }

    public static class PlatformSupport
    {
        private static readonly ShellExecutorType _executorType = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows) ? ShellExecutorType.Windows : ShellExecutorType.Unix;


        public static ShellExecuteResult ExecuteShellCommand(string commandName, string args)
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            var exitCode = ExecuteShellCommand(commandName, args,
            (s, e) =>
            {
                outputBuilder.AppendLine(e.Data);
            },
            (s, e) =>
            {
                errorBuilder = new StringBuilder();
            },
            false, "");

            return new ShellExecuteResult()
            {
                ExitCode = exitCode,
                Output = outputBuilder.ToString().Trim(),
                ErrorOutput = errorBuilder.ToString().Trim()
            };
        }

        public static void LaunchShell(string workingDirectory, params string[] paths)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = workingDirectory,
            };

            foreach (var extraPath in paths)
            {
                if (extraPath != null)
                {
                    var envPath = startInfo.Environment["PATH"];
                    if (envPath != null)
                    {
                        startInfo.Environment["PATH"] = Path.Combine(envPath, extraPath);
                    }
                    else
                    {
                        startInfo.Environment["PATH"] = extraPath;
                    }
                }
            }

            if (_executorType == ShellExecutorType.Windows)
            {
                startInfo.FileName = ResolveFullExecutablePath("cmd.exe");
                startInfo.Arguments = $"/c start {startInfo.FileName}";
            }
            else //Unix
            {
                startInfo.FileName = "sh";
            }

            Process.Start(startInfo);
        }

        public static Process LaunchShellCommand(string commandName, string args, Action<object, DataReceivedEventArgs>
           outputReceivedCallback, Action<object, DataReceivedEventArgs>? errorReceivedCallback = null, bool resolveExecutable = true,
           string workingDirectory = "", bool executeInShell = true, bool includeSystemPaths = true, params string[] extraPaths)
        {
            var shellProc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory,
                }
            };

            if (!includeSystemPaths)
            {
                shellProc.StartInfo.Environment["PATH"] = "";
            }


            foreach (var extraPath in extraPaths)
            {
                if (extraPath != null)
                {
                    var envPath = shellProc.StartInfo.Environment["PATH"];
                    if (envPath != null)
                    {
                        shellProc.StartInfo.Environment["PATH"] = Path.Combine(envPath, extraPath);
                    }
                    else
                    {
                        shellProc.StartInfo.Environment["PATH"] = extraPath;
                    }
                }
            }

            if (executeInShell)
            {
                if (_executorType == ShellExecutorType.Windows)
                {
                    shellProc.StartInfo.FileName = ResolveFullExecutablePath("cmd.exe");
                    shellProc.StartInfo.Arguments = $"/C {(resolveExecutable ? ResolveFullExecutablePath(commandName, true, extraPaths) : commandName)} {args}";
                    shellProc.StartInfo.CreateNoWindow = true;
                }
                else //Unix
                {
                    shellProc.StartInfo.FileName = "sh";
                    shellProc.StartInfo.Arguments = $"-c \"{(resolveExecutable ? ResolveFullExecutablePath(commandName) : commandName)} {args}\"";
                    shellProc.StartInfo.CreateNoWindow = true;
                }
            }
            else
            {
                shellProc.StartInfo.FileName = (resolveExecutable ? ResolveFullExecutablePath(commandName, true, extraPaths) : commandName);
                shellProc.StartInfo.Arguments = args;
                shellProc.StartInfo.CreateNoWindow = true;
            }

            shellProc.OutputDataReceived += (s, a) => outputReceivedCallback(s, a);

            if (errorReceivedCallback != null)
            {
                shellProc.ErrorDataReceived += (s, a) => errorReceivedCallback(s, a);
            }

            shellProc.EnableRaisingEvents = true;

            try
            {
                shellProc.Start();

                shellProc.BeginOutputReadLine();
                shellProc.BeginErrorReadLine();
            }
            catch { }

            return shellProc;
        }

        public static string[] GetSystemPaths()
        {
            var result = ExecuteShellCommand("/bin/bash", "-l -c 'echo $PATH'");

            return result.Output.Split(':');
        }


        public static int ExecuteShellCommand(string commandName, string args, Action<object, DataReceivedEventArgs>
            outputReceivedCallback, Action<object, DataReceivedEventArgs>? errorReceivedCallback = null, bool resolveExecutable = true,
            string workingDirectory = "", bool executeInShell = true, bool includeSystemPaths = true, params string[] extraPaths)
        {
            using (var shellProc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                }
            })

            {
                if (!includeSystemPaths)
                {
                    shellProc.StartInfo.Environment["PATH"] = "";
                }
                foreach (var extraPath in extraPaths)
                {
                    if (extraPath != null)
                    {
                        var envPath = shellProc.StartInfo.Environment["PATH"];
                        if (envPath != null)
                        {
                            shellProc.StartInfo.Environment["PATH"] = Path.Combine(envPath, extraPath);
                        }
                        else
                        {
                            shellProc.StartInfo.Environment["PATH"] = extraPath;
                        }
                    }
                }

                if (executeInShell)
                {
                    if (_executorType == ShellExecutorType.Windows)
                    {
                        shellProc.StartInfo.FileName = ResolveFullExecutablePath("cmd.exe");
                        shellProc.StartInfo.Arguments = $"/C {(resolveExecutable ? ResolveFullExecutablePath(commandName, true, extraPaths) : commandName)} {args}";
                        shellProc.StartInfo.CreateNoWindow = true;
                    }
                    else //Unix
                    {
                        shellProc.StartInfo.FileName = "sh";
                        shellProc.StartInfo.Arguments = $"-c \"{(resolveExecutable ? ResolveFullExecutablePath(commandName) : commandName)} {args}\"";
                        shellProc.StartInfo.CreateNoWindow = true;
                    }
                }
                else
                {
                    shellProc.StartInfo.FileName = (resolveExecutable ? ResolveFullExecutablePath(commandName, true, extraPaths) : commandName);
                    shellProc.StartInfo.Arguments = args;
                    shellProc.StartInfo.CreateNoWindow = true;
                }

                shellProc.OutputDataReceived += (s, a) => outputReceivedCallback(s, a);

                if (errorReceivedCallback != null)
                {
                    shellProc.ErrorDataReceived += (s, a) => errorReceivedCallback(s, a);
                }

                shellProc.Start();

                shellProc.BeginOutputReadLine();
                shellProc.BeginErrorReadLine();

                shellProc.WaitForExit();

                return shellProc.ExitCode;
            }
        }

        /// <summary>
        /// Checks whether a script executable is available in the user's shell
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CheckExecutableAvailability(string fileName, params string[] extraPaths)
        {
            return ResolveFullExecutablePath(fileName, true, extraPaths) != null;
        }

        /// <summary>
        /// Attempts to locate the full path to a script
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string? ResolveFullExecutablePath(string fileName, bool returnNullOnFailure = true, params string[] extraPaths)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            if (_executorType == ShellExecutorType.Windows)
            {
                var values = new List<string>(extraPaths);
                var envPath = Environment.GetEnvironmentVariable("PATH");
                if (envPath != null && !string.IsNullOrWhiteSpace(envPath))
                {
                    values.AddRange(new List<string>(envPath.Split(';')));
                }

                foreach (var path in values)
                {
                    var fullPath = Path.Combine(path, fileName);
                    if (File.Exists(fullPath))
                        return fullPath;
                }
            }
            else
            {
                //Use the which command
                var outputBuilder = new StringBuilder();
                ExecuteShellCommand("which", $"\"{fileName}\"", (s, e) =>
                {
                    outputBuilder.AppendLine(e.Data);
                }, (s, e) => { }, false);
                var procOutput = outputBuilder.ToString();
                if (string.IsNullOrWhiteSpace(procOutput))
                {
                    return returnNullOnFailure ? null : fileName;
                }
                return procOutput.Trim();
            }
            return returnNullOnFailure ? null : fileName;
        }
    }

}
