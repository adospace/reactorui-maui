// See https://aka.ms/new-console-template for more information
using MauiReactor.Scaffold;
using System.Reflection;

var outputPath = args != null && args.Length > 0 ? args[0] : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "gen");
Directory.CreateDirectory(outputPath);

Console.WriteLine($"Generating MauiReactor wrappers...");
Console.WriteLine($"Output Path: {outputPath}");

//force XF assembly load
var _ = new Microsoft.Maui.Controls.Shapes.Rectangle();

var types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
             from assemblyType in domainAssembly.GetTypes()
             where typeof(BindableObject).IsAssignableFrom(assemblyType)
             select assemblyType)
    .ToDictionary(_ => _.FullName ?? throw new InvalidOperationException(), _ => _);

foreach (var classNameToGenerate in File
    .ReadAllLines("WidgetList.txt")
    .Where(_ => !string.IsNullOrWhiteSpace(_) && !_.StartsWith("//")))
{
    var typeToScaffold = types[classNameToGenerate];

    Scaffold(typeToScaffold, outputPath);
}

Console.WriteLine("Done");

void Scaffold(Type typeToScaffold, string outputPath)
{
    var typeGenerator = new TypeGenerator(typeToScaffold);
    var outputFileName = (typeToScaffold.FullName ?? throw new InvalidOperationException())
                .Replace("Microsoft.Maui.Controls.", string.Empty);
    var outputFileNameTokens = outputFileName.Split('.');

    Console.WriteLine($"Generating {outputFileName}...");

    File.WriteAllText(Path.Combine(outputPath, 
        Path.Combine(outputFileNameTokens) + ".cs"),
        typeGenerator.TransformAndPrettify());
}

