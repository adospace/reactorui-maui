// See https://aka.ms/new-console-template for more information
using CommandLine;

namespace MauiReactor.HotReloadConsole
{
    public class Options
    {
        [Option('f', "framework", Required = true, HelpText = "Specify the framework: net7.0-android, net7.0-ios or net7.0-maccatalyst")]
        public string? Framework { get; set; }

        [Option('p', "proj")]
        public string? ProjectFileName { get; set; }

        [Option('d', "dir")]
        public string? WorkingDirectory { get; set; }

        [Option('m', "mode")]
        public CompilationMode CompilationMode { get; set; }

    }

    public enum CompilationMode
    {
        Slim,

        Full,

        //Auto TODO
    }
}
