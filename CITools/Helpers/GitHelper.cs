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
            throw new InvalidOperationException("Impossible de démarrer le processus Git");
        }

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            string error = process.StandardError.ReadToEnd();
            throw new InvalidOperationException($"Erreur Git (code {process.ExitCode}): {error}");
        }

        return output.Trim();
    }
}