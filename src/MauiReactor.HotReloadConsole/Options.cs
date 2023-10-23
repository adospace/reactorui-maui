// See https://aka.ms/new-console-template for more information
using CommandLine;

namespace MauiReactor.HotReloadConsole
{
    public class Options
    {
        [Option('f', "framework", Required = true, HelpText = "Specify the framework: net8.0-android, net8.0-ios, net8.0-maccatalyst, or net8.0-windows10.0.XXXXX.0")]
        public string? Framework { get; set; }

        [Option('p', "proj", HelpText = "Project file name (if different from that contained in the current directory)")]
        public string? ProjectFileName { get; set; }

        [Option('d', "dir", HelpText = "Working directory (if different from the current one)")]
        public string? WorkingDirectory { get; set; }

        [Option('m', "mode", HelpText = "Slim(default) or Full")]
        public CompilationMode CompilationMode { get; set; }

    }

    public enum CompilationMode
    {
        Slim,

        Full,

        //Auto TODO
    }
}
