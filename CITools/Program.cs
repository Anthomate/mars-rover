using System.CommandLine;
using CITools.Commands;

namespace CITools;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Outils CI pour les workflows GitHub Actions");

        var checkCommitCommand = new Command("check-commit", "Vérifie si le message de commit contient un emoji spatial requis");
        var repoPathOption = new Option<string>(
            "--repo-path",
            getDefaultValue: () => ".",
            description: "Chemin du dépôt Git"
        );
        checkCommitCommand.AddOption(repoPathOption);
        checkCommitCommand.SetHandler(repoPath =>
        {
            var result = CheckCommitMessage.Execute(repoPath);
            Console.WriteLine(result.Message);
            Environment.Exit(result.ExitCode);
        }, repoPathOption);
        rootCommand.AddCommand(checkCommitCommand);

        var runTestsCommand = new Command("run-tests", "Exécute les tests .NET et rapporte les résultats");
        var projectPathOption = new Option<string>(
            "--project-path",
            description: "Chemin du projet à tester"
        );
        projectPathOption.IsRequired = true;
        var verbosityOption = new Option<string>(
            "--verbosity",
            getDefaultValue: () => "normal",
            description: "Niveau de détail de sortie (quiet, minimal, normal, detailed, diagnostic)"
        );
        runTestsCommand.AddOption(projectPathOption);
        runTestsCommand.AddOption(verbosityOption);
        runTestsCommand.SetHandler((projectPath, verbosity) =>
        {
            var result = TestRunner.Execute(projectPath, verbosity);
            Console.WriteLine(result.Message);
            Environment.Exit(result.ExitCode);
        }, projectPathOption, verbosityOption);
        rootCommand.AddCommand(runTestsCommand);

        return await rootCommand.InvokeAsync(args);
    }
}