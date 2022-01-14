// See https://aka.ms/new-console-template for more information
using CommandLine;

namespace MauiReactor.HotReloadConsole
{
    public class Options
    {
        [Option('p', "proj", Required = false)]
        public string? ProjectFileName { get; set; }

        [Option('d', "dir", Required = false)]
        public string? WorkingDirectory { get; set; }

        [Option('f', "fr", Required=false)]
        public string Framework { get; set; } = "net6.0-android";
    }
}
