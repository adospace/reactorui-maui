using CommandLine;
using MauiReactor.XamlConverterTool;
using System.Reflection;

Console.WriteLine($"MauiReactor Xaml Converter Tool");
Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}");

var optionsParsed = Parser.Default.ParseArguments<Options>(args);

await optionsParsed.WithParsedAsync(RunConverter);

return 0;


Task RunConverter(Options options)
{
    var workingDirectory = options.WorkingDirectory ?? Directory.GetCurrentDirectory();

    //var converters = new[] { new XamlParser(Path.Combine(workingDirectory, options.File ?? throw new InvalidOperationException())) };

    //foreach (var converter in converters)
    //{
    //    converter.Run();
    //}

    return Task.CompletedTask;
}

public class Options
{
    [Option('f', "file", HelpText = "Xaml file name to convert")]
    public string? File { get; set; }

    [Option("files", HelpText = "Xaml file name to convert")]
    public string? Files { get; set; }

    [Option('d', "dir", HelpText = "Working directory (if different from the current one)")]
    public string? WorkingDirectory { get; set; }

}