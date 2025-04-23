using System.Diagnostics;
using CITools.Models;

namespace CITools.Commands;

public static class TestRunner
{
    public static CommandResult Execute(string projectPath, string verbosity = "normal")
    {
        try
        {
            Console.WriteLine("Restoration of dependencies...");
            var restoreResult = RunDotNetCommand("restore", projectPath);
            if (!restoreResult.Success)
            {
                return restoreResult;
            }

            Console.WriteLine("Compilation of the project...");
            var buildResult = RunDotNetCommand("build --no-restore", projectPath);
            if (!buildResult.Success)
            {
                return buildResult;
            }

            Console.WriteLine("Running the tests...");
            var testResult = RunDotNetCommand($"test --no-build --verbosity {verbosity}", projectPath);
            
            if (testResult.Success)
            {
                return CommandResult.Ok("☀️ Tests successfully completed.");
            }
            else
            {
                return CommandResult.Error(
                    "⛈️ Some tests failed!\n" +
                    "👽 Human, your code is incomprehensible to my civilization. Can you add some tests?"
                );
            }
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Error running tests: {ex.Message}");
        }
    }
    private static CommandResult RunDotNetCommand(string arguments, string workingDirectory)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = workingDirectory
        };

        using var process = Process.Start(startInfo);
        if (process == null)
        {
            return CommandResult.Error("Unable to start dotnet process");
        }

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        
        process.WaitForExit();
        
        Console.WriteLine(output);

        if (process.ExitCode == 0) return CommandResult.Ok(output);
        
        if (!string.IsNullOrEmpty(error))
        {
            Console.Error.WriteLine(error);
        }
        return CommandResult.Error($"'dotnet command {arguments}' failed with code {process.ExitCode}");

    }
}