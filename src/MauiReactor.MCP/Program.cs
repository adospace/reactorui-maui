using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = Host.CreateApplicationBuilder(args);

// Configure all logs to go to stderr (stdout is used for the MCP protocol messages).
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<global::ProjectTools>()
    .WithTools<global::ComponentTools>()
    .WithTools<global::DocumentationTools>();

// Register the shell service for running external processes
builder.Services.AddSingleton<MauiReactor.MCP.Services.IShell, MauiReactor.MCP.Services.ShellService>();

var tool = builder.Build();

var logger = tool.Services.GetRequiredService<ILogger<DocumentationTools>>();

var cmpTool = new DocumentationTools(logger);
var index = cmpTool.GetDocumentationIndex();

foreach (var topic in index)
{
    cmpTool.GetDocumentationTopic(topic.Key);
}


await tool.RunAsync();
