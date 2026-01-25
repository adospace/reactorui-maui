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
using CliWrap;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClientFull : HotReloadClient
    {
        public HotReloadClientFull(Options options) : base(options)
        {
        }


        protected override async Task<bool> CompileProject(Stream stream, Stream pdbStream, CancellationToken cancellationToken)
        {
            await using var stdOut = Console.OpenStandardOutput();
            await using var stdErr = Console.OpenStandardError();

            var result = await Cli.Wrap("dotnet")
                .WithArguments([$"build", Path.Combine(_workingDirectory, $"{_projFileName}.csproj"), "-f", _options.Framework ?? throw new InvalidOperationException(), "--no-restore", "--no-dependencies"])
                .WithWorkingDirectory(_workingDirectory)
                .WithStandardOutputPipe(PipeTarget.ToStream(stdOut))
                .WithStandardErrorPipe(PipeTarget.ToStream(stdErr))
                .WithValidation(CommandResultValidation.None)
                .ExecuteAsync(cancellationToken);


            if (result.ExitCode != 0)
            {
                Console.WriteLine("Build failed.");
                return false;
            }   

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
}
