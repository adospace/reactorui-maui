using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
using MauiReactor.MCP.Services;
using ModelContextProtocol;

internal class ProjectTools
{
    private readonly IShell _shell;

    public ProjectTools(IShell shell)
    {
        _shell = shell;
    }

    [McpServerTool(Name = "generate-empty-maui-reactor-project")]
    [Description("Generates an empty MauiReactor project into the specified directory.")]
    public void GenerateEmptyProject(
        [Description("Name of the new project")] string projectName,
        [Description("Target directory for the new project, defaults to the current directory")] string? targetDirectory = null)
    {
        if (string.IsNullOrWhiteSpace(projectName))
        {
            throw new McpException("projectName must be provided");
        }

        var workDir = targetDirectory is null ? Directory.GetCurrentDirectory() : targetDirectory;

        // Ensure target directory exists
        if (!Directory.Exists(workDir))
        {
            Directory.CreateDirectory(workDir);
        }

        // 1) Ensure the latest MauiReactor template pack is installed
        var (exitCode, stdout, stderr) = _shell.Run("dotnet", "new install Reactor.Maui.TemplatePack", workDir);
        if (exitCode != 0)
        {
            throw new McpException($"Failed to install Reactor.Maui.TemplatePack: {stderr}");
        }

        // 2) Create the new project inside the target directory
        var createArgs = $"new maui-reactor-startup -o {projectName}";
        var (createExitCode, createStdout, createStderr) = _shell.Run("dotnet", createArgs, workDir);
        if (createExitCode != 0)
        {
            throw new McpException($"Failed to create project '{projectName}': {createStderr}");
        }
    }

}
