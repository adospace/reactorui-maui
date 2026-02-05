using System.Diagnostics;
using System.IO;

namespace MauiReactor.MCP.Services
{
    public interface IShell
    {
        (int exitCode, string stdout, string stderr) Run(string fileName, string arguments, string workingDirectory);
    }

    public class ShellService : IShell
    {
        public (int exitCode, string stdout, string stderr) Run(string fileName, string arguments, string workingDirectory)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = new Process { StartInfo = psi };
            var stdoutBuilder = new StringWriter();
            var stderrBuilder = new StringWriter();

            proc.OutputDataReceived += (s, e) => { if (e.Data != null) stdoutBuilder.WriteLine(e.Data); };
            proc.ErrorDataReceived += (s, e) => { if (e.Data != null) stderrBuilder.WriteLine(e.Data); };

            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();

            return (proc.ExitCode, stdoutBuilder.ToString(), stderrBuilder.ToString());
        }
    }
}
