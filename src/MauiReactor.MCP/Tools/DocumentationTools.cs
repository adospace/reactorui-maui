/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
using MauiReactor.MCP.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

internal class DocumentationTools
{
    private readonly ILogger<DocumentationTools> _logger;

    // Cache JsonSerializerOptions instance to avoid CA1869
    private static readonly System.Text.Json.JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public DocumentationTools(ILogger<DocumentationTools> logger)
    {
        _logger = logger;
        _logger.LogInformation("DocumentationTools initialized.");
    }

    public record DocumentationTopic(string Key, string Summary);

    public record DocumentationInfo(string Key, string Summary, string Remarks, string Example);

    [McpServerTool(Name = "documentation-index")]
    [Description("Returns a machine-readable index of MauiReactor topics (architecture, components, patterns, examples). Use this to discover and select topics for deeper queries when assisting developers in building MauiReactor apps.")]
    public DocumentationTopic[] GetDocumentationIndex()
    {
        _logger.LogInformation("documentation-index invoked");

        var asm = typeof(DocumentationTools).Assembly;
        var resourceName = "MauiReactor.MCP.Tools.Resources.Documentation.topics.json";
        _logger.LogDebug("Attempting to load index resource {ResourceName}", resourceName);

        using var stream = asm.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            _logger.LogWarning("Documentation topics index resource not found");
            throw new McpException("Documentation topics index not found");
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var topics = System.Text.Json.JsonSerializer.Deserialize<DocumentationTopic[]>(json, _jsonOptions);
        if (topics is null)
        {
            _logger.LogError("Invalid documentation topics index JSON");
            throw new McpException("Invalid documentation topics index JSON");
        }

        _logger.LogInformation("documentation-index succeeded with {Count} topics", topics.Length);
        return topics;
    }


    [McpServerTool(Name = "documentation-topic")]
    [Description("Gets the details of a specific documentation topic.")]
    public DocumentationInfo GetDocumentationTopic(
        [Description("The key of the documentation topic to retrieve")] string topicKey)
    {
        if (string.IsNullOrWhiteSpace(topicKey))
        {
            throw new McpException("topicKey must be provided");
        }

        var key = topicKey.Trim();

        _logger.LogInformation("documentation-topic invoked with key={Key}", key);

        var asm = Assembly.GetExecutingAssembly();
        var resourceNames = asm.GetManifestResourceNames();
        _logger.LogDebug("Assembly manifest contains {Count} resources", resourceNames.Length);

        // Find a resource that ends with Tools.Resources.Components.{componentName}.json
        var targetSuffix = $"Tools.Resources.Documentation.{key}.json";
        string? resourceName = resourceNames.FirstOrDefault(_ => _.EndsWith(targetSuffix, StringComparison.OrdinalIgnoreCase));

        _logger.LogDebug("Attempting to load resource {ResourceName}", resourceName);

        if (resourceName is null)
        {
            _logger.LogWarning("Documentation topic not found for key={Key}", key);
            throw new InvalidOperationException($"Documentation topic not found for '{key}'.");
        }

        using var stream = asm.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            _logger.LogError("Failed to read resource stream for {key} (resource={ResourceName})", key, resourceName);
            throw new McpException($"Documentation topic not found: {key}");
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var info = System.Text.Json.JsonSerializer.Deserialize<DocumentationInfo>(json, _jsonOptions);
        if (info is null)
        {
            _logger.LogError("Invalid documentation JSON for key={Key}", key);
            throw new McpException($"Invalid documentation JSON for topic: {key}");
        }

        if (string.IsNullOrEmpty(info.Key))
        {
            info = info with { Key = key };
        }

        _logger.LogInformation("documentation-topic succeeded for key={Key}", info.Key);
        return info;
    }

}