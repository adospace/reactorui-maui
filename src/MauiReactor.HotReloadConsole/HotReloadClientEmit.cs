using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Xml;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClientEmit : HotReloadClient
    {
        private Compilation? _projectCompilation;
        private record ParsedFileInfo(string FilePath, SyntaxTree SyntaxTree, DateTime LastModified);
        private readonly Dictionary<string, ParsedFileInfo> _parsedFiles = new();
        private readonly CSharpParseOptions _parseOptions = CSharpParseOptions.Default.WithPreprocessorSymbols(new List<string>
        {
            "DEBUG"
        });

        //static HotReloadClientEmit()
        //{
        //    MSBuildLocator.RegisterDefaults();
        //}

        public HotReloadClientEmit(Options options) : base(options)
        {
        }

        protected override Task<bool> CompileProject(Stream stream, Stream pdbStream, CancellationToken cancellationToken)
        {
            if (_projectCompilation == null)
            {
                throw new InvalidOperationException();
            }

            var result = _projectCompilation.Emit(stream, pdbStream, cancellationToken: cancellationToken);

            if (!result.Success)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Diagnostics.Select(_ => _.ToString())));
            }

            return Task.FromResult(result.Success);
        }

        public override async Task Startup(CancellationToken cancellationToken)
        {
            await SetupProjectCompilation(cancellationToken);
        }

        private async Task SetupProjectCompilation(CancellationToken cancellationToken)
        {
            Console.Write($"Setting up build pipeline for {_projFileName} project...");

            var properties = new Dictionary<string, string>
            {
                { "Configuration", "Debug" },
                { "TargetFramework", _options.Framework ?? throw new InvalidOperationException() }
            };

            _workspace ??= MSBuildWorkspace.Create(properties);

            _workspace.CloseSolution();

            _project = await _workspace.OpenProjectAsync(Path.Combine(_workingDirectory, $"{_projFileName}.csproj"), cancellationToken: cancellationToken);

            foreach (var projectReference in _project.AllProjectReferences)
            {
                var referencedProject = _workspace.CurrentSolution.GetProject(projectReference.ProjectId);

                if (referencedProject == null)
                {
                    continue;
                }

                var compilation = await referencedProject.GetCompilationAsync(cancellationToken);

                // Ensure the compilation is successful
                if (compilation != null)
                {
                    // Create a MetadataReference from the referenced project's compilation and add it to the original project
                    var metadataReference = compilation.ToMetadataReference();
                    _project = _project.AddMetadataReference(metadataReference);
                }
            }

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

        protected override async Task<bool> HandleFileChangeNotifications(IEnumerable<FileChangeNotification> notifications, CancellationToken cancellationToken)
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
                    continue;
                };

                var currentFileLastWriteTime = File.GetLastWriteTime(notification.FilePath);

                if (parsedFileInfo.LastModified != currentFileLastWriteTime)
                {
                    var newSyntaxTree = SyntaxFactory.ParseSyntaxTree(
                        await ReadAllTextFileAsync(notification.FilePath, cancellationToken), options: parsedFileInfo.SyntaxTree.Options, cancellationToken: cancellationToken);

                    Console.WriteLine($"Replacing syntax tree for: {Path.GetRelativePath(_workingDirectory, notification.FilePath)}");

                    _projectCompilation = _projectCompilation.ReplaceSyntaxTree(parsedFileInfo.SyntaxTree, newSyntaxTree);

                    _parsedFiles[notification.FilePath] = new ParsedFileInfo(notification.FilePath, newSyntaxTree, currentFileLastWriteTime);

                    requiredHotReload = true;
                }
            }


            return requiredHotReload;
        }

    }
}
