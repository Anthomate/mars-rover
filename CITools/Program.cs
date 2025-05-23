﻿using System.CommandLine;
using CITools.Commands;

namespace CITools;

internal static class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("GitHub Actions CI Tools");

        var checkCommitCommand = new Command("check-commit");
        var repoPathOption = new Option<string>(
            "--repo-path",
            getDefaultValue: () => "."
        );
        checkCommitCommand.AddOption(repoPathOption);
        checkCommitCommand.SetHandler(repoPath =>
        {
            var result = CheckCommitMessage.Execute(repoPath);
            Console.WriteLine(result.Message);
            Environment.Exit(result.ExitCode);
        }, repoPathOption);
        rootCommand.AddCommand(checkCommitCommand);

        var runTestsCommand = new Command("run-tests");
        var projectPathOption = new Option<string>(
            "--project-path"
        )
        {
            IsRequired = true
        };
        var verbosityOption = new Option<string>(
            "--verbosity",
            getDefaultValue: () => "normal"
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

        var detectCircularRefsCommand = new Command("detect-circular-refs");
        var circularRefsProjectPathOption = new Option<string>(
            "--project-path"
        );
        circularRefsProjectPathOption.IsRequired = true;
        var includePatternOption = new Option<string>(
            "--include",
            getDefaultValue: () => "*.cs"
        );
        var excludePatternOption = new Option<string?>(
            "--exclude",
            getDefaultValue: () => null
        );
        detectCircularRefsCommand.AddOption(circularRefsProjectPathOption);
        detectCircularRefsCommand.AddOption(includePatternOption);
        detectCircularRefsCommand.AddOption(excludePatternOption);
        detectCircularRefsCommand.SetHandler((projectPath, includePattern, excludePattern) =>
        {
            var result = CircularReferenceDetector.Execute(projectPath, includePattern, excludePattern);
            Console.WriteLine(result.Message);
            Environment.Exit(result.ExitCode);
        }, circularRefsProjectPathOption, includePatternOption, excludePatternOption);
        rootCommand.AddCommand(detectCircularRefsCommand);
        
        return await rootCommand.InvokeAsync(args);
    }
}