# MauiReactor MCP Server

This package exposes a Model Context Protocol (MCP) server for working with MauiReactor and .NET MAUI projects. It provides tools for component scaffolding, workspace inspection, and basic build/test actions via stdio.

## Install (NuGet, `dnx`)

Once the package is published to NuGet, configure your IDE to install and run the server via `dnx`:

VS Code: create `.vscode/mcp.json` in your workspace

```json
{
  "servers": {
    "MauiReactor.MCP": {
      "type": "stdio",
      "command": "dnx",
      "args": [
        "MauiReactor.MCP",
        "--version",
        "0.1.0-preview",
        "--yes"
      ]
    }
  }
}
```

Visual Studio: create `.mcp.json` in the solution directory with the same content as above.

Notes:
- Replace the version with the latest published package version.
- `dnx` downloads and caches the self-contained binary per RID.

## Run from source (development)

To test locally without packaging:

```json
{
  "servers": {
    "MauiReactor.MCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "<absolute path to src/MauiReactor.MCP>"
      ]
    }
  }
}
```

## Server capabilities

- Tools: scaffolding MAUI components, reading/writing workspace files, basic build/test tasks.
- Resources: embedded templates under `Tools/Resources/Components`.
- Prompts: MCP-compliant prompt definitions for guided workflows.

## Client configuration (`.mcp/server.json`)

This package includes `.mcp/server.json` for IDE browsing. You can declare required inputs (e.g., workspace paths or feature flags) following MCP guidance. See [aka.ms/nuget/mcp/guide/configuring-inputs](https://aka.ms/nuget/mcp/guide/configuring-inputs).

## Pack and publish

```bash
dotnet pack -c Release
dotnet nuget push bin/Release/*.nupkg --source https://api.nuget.org/v3/index.json
```

Ensure `PackageId`, `RepositoryUrl`, and metadata are set in `MauiReactor.MCP.csproj`.

## Troubleshooting

- `dnx` not found: update to the latest Copilot/IDE preview that supports MCP.
- Startup errors: run with `dotnet run` to view detailed logs.
- RID mismatch: confirm your platform is listed in `<RuntimeIdentifiers>`.

## References

- Model Context Protocol: https://modelcontextprotocol.io/
- Specification: https://spec.modelcontextprotocol.io/
- .NET MCP SDK (NuGet): https://www.nuget.org/packages/ModelContextProtocol
- VS Code MCP servers: https://code.visualstudio.com/docs/copilot/chat/mcp-servers
- Visual Studio MCP servers: https://learn.microsoft.com/visualstudio/ide/mcp-servers
