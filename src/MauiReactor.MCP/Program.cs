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
    .WithTools<global::ComponentTools>();

// Register the shell service for running external processes
builder.Services.AddSingleton<MauiReactor.MCP.Services.IShell, MauiReactor.MCP.Services.ShellService>();

await builder.Build().RunAsync();
