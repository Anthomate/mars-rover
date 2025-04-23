using System.Diagnostics;
using LibGit2Sharp;

namespace CITools.Helpers;

public static class GitHelper
{
    public static string GetLastCommitMessage(string repositoryPath = ".")
    {
        try
        {
            using var repo = new Repository(repositoryPath);
            var commit = repo.Head.Tip;
            return commit.Message;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return GetLastCommitMessageUsingCli(repositoryPath);
        }
    }
    private static string GetLastCommitMessageUsingCli(string repositoryPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "log -1 --pretty=%B",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = repositoryPath
        };

        using var process = Process.Start(startInfo);
        if (process == null)
        {
            throw new InvalidOperationException("Unable to start Git process");
        }

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode == 0) return output.Trim();
        
        string error = process.StandardError.ReadToEnd();
        throw new InvalidOperationException($"Git Error (code {process.ExitCode}): {error}");

    }
}