// See https://aka.ms/new-console-template for more information
using CommandLine;
using System.ComponentModel;
using System.Globalization;
using System.Net;

namespace MauiReactor.HotReloadConsole
{
    public class Options
    {
        [Option('f', "framework", Required = true, HelpText = "Specify the framework: net9.0-android, net9.0-ios, net9.0-maccatalyst, or net9.0-windows10.0.XXXXX.0")]
        public string? Framework { get; set; }

        [Option('p', "proj", HelpText = "Project file name (if different from that contained in the current directory)")]
        public string? ProjectFileName { get; set; }

        [Option('d', "dir", HelpText = "Working directory (if different from the current one)")]
        public string? WorkingDirectory { get; set; }

        [Option('m', "mode", HelpText = "Slim(default) or Full")]
        public CompilationMode CompilationMode { get; set; }

        [Option('h', "host", HelpText = "Identify the remote host running the emulator")]
        public string? Host { get; set; }

        [Option('r', "register-defaults", HelpText = "Register default MsBuild locations using MSBuildLocator.RegisterDefaults()")]
        public bool MsBuildLocatorRegisterDefaults { get; set; }

    }

    public enum CompilationMode
    {
        Slim,

        Full,

        //Auto TODO
    }

    class IPAddressConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                if (IPAddress.TryParse(stringValue, out var address))
                {
                    return address;
                }
                throw new FormatException($"{stringValue} is not a valid IP address");
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
