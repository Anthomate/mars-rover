using System.Diagnostics;
using CITools.Models;

namespace CITools.Commands;

public class TestRunner
{
    public static CommandResult Execute(string projectPath, string verbosity = "normal")
    {
        try
        {
            Console.WriteLine("Restauration des dépendances...");
            var restoreResult = RunDotNetCommand("restore", projectPath);
            if (!restoreResult.Success)
            {
                return restoreResult;
            }

            Console.WriteLine("Compilation du projet...");
            var buildResult = RunDotNetCommand("build --no-restore", projectPath);
            if (!buildResult.Success)
            {
                return buildResult;
            }

            Console.WriteLine("Exécution des tests...");
            var testResult = RunDotNetCommand($"test --no-build --verbosity {verbosity}", projectPath);
            
            if (testResult.Success)
            {
                return CommandResult.Ok("☀️ Tous les tests ont réussi!");
            }
            else
            {
                return CommandResult.Error(
                    "⛈️ Certains tests ont échoué!\n" +
                    "👽 Humain ton code est incompréhensible pour ma civilisation. Peux tu ajouter des tests ?"
                );
            }
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Erreur lors de l'exécution des tests: {ex.Message}");
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
            return CommandResult.Error("Impossible de démarrer le processus dotnet");
        }

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        
        process.WaitForExit();
        
        Console.WriteLine(output);
        
        if (process.ExitCode != 0)
        {
            if (!string.IsNullOrEmpty(error))
            {
                Console.Error.WriteLine(error);
            }
            return CommandResult.Error($"La commande 'dotnet {arguments}' a échoué avec le code {process.ExitCode}");
        }

        return CommandResult.Ok(output);
    }
}