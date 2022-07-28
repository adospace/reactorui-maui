using CommandLine;
using MauiReactor.Scaffold;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System.Reflection;

MSBuildLocator.RegisterDefaults();

return await Parser.Default.ParseArguments<GenerateOptions, ListOptions>(args)
  .MapResult(
    (GenerateOptions opts) => GenerateWrapperCode(opts),
    (ListOptions opts) => ListControls(opts),

    errs => Task.FromResult(1));

static async Task<int> GenerateWrapperCode(GenerateOptions opts)
{
    await Task.Delay(1);
    return 0;
}

static async Task<int> ListControls(ListOptions opts)
{
    var assemblyPath = await LoadProjectOutputAssembly(opts.ProjectFileName);

    var types = LoadAssemblyTypes(assemblyPath);

    foreach(var type in types)
    {
        Console.WriteLine($"{type.Key}");
    }

    return 0;
}


static async Task<string> LoadProjectOutputAssembly(string? projectFilePath)
{
    if (projectFilePath == null)
    {
        var projFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj");
        if (projFiles.Length == 1)
        {
            projectFilePath = projFiles[0];
        }
        else
        {
            throw new InvalidOperationException($"Directory {Directory.GetCurrentDirectory()} contains {projFiles.Length} projects");
        }
    }

    Console.Write($"Loading project {projectFilePath} ...");

    var properties = new Dictionary<string, string>
    {
        { "Configuration", "Debug" },
    };

    var workspace = MSBuildWorkspace.Create(properties);

    var project = await workspace.OpenProjectAsync(projectFilePath);
    
    Console.WriteLine("done.");

    if (project.OutputFilePath == null)
    {
        throw new InvalidOperationException();
    }

    return project.OutputFilePath;
}


static IReadOnlyDictionary<string, Type> LoadAssemblyTypes(string assemblyPath)
{
    var _ = new Microsoft.Maui.Controls.Shapes.Rectangle();

    //var assemblyRaw = await File.ReadAllBytesAsync(assemblyPath);
    return (from assemblyType in Assembly.LoadFrom(assemblyPath).GetTypes()
            where typeof(BindableObject).IsAssignableFrom(assemblyType)
            select assemblyType)
    .ToDictionary(_ => _.FullName ?? throw new InvalidOperationException(), _ => _);
}

static void Scaffold(Type typeToScaffold, string outputPath)
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

[Verb("list", HelpText = "List the Maui controls found in the project.")]
class ListOptions
{
    [Option('p', "proj", Required = false)]
    public string? ProjectFileName { get; set; }
}

[Verb("generate", HelpText = "Generate the source code for the wrappers in the output directory.")]
class GenerateOptions
{
    [Option('p', "proj", Required = false)]
    public string? ProjectFileName { get; set; }

    [Option('o', "output-dir", Required = false, HelpText = "Set the output directory for the generated code.")]
    public string? OutputDirectory { get; set; }
    
}
