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

        //net6.0-android;net6.0-ios;net6.0-maccatalyst;net6.0-windows[10.0.19041]
        [Option('f', "fr", Required=false)]
        public string Framework { get; set; } = "net6.0-android";
    }
}
