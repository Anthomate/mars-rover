using System.Text.RegularExpressions;
using CITools.Helpers;
using CITools.Models;

namespace CITools.Commands;

public static class CheckCommitMessage
{
    private static readonly string[] RequiredEmojis = { "🚀", "👽", "🪐" };
    
    public static CommandResult Execute(string repositoryPath = ".")
    {
        try
        {
            string commitMessage = GitHelper.GetLastCommitMessage(repositoryPath);
            
            Console.WriteLine($"Commit message: {commitMessage}");
            
            bool containsEmoji = RequiredEmojis.Any(emoji => commitMessage.Contains(emoji));
            
            if (containsEmoji)
            {
                return CommandResult.Ok("✅ The commit message contains a space emoji !");
            }
            else
            {
                return CommandResult.Error("❌ The commit message does not contain a required space emoji (🚀, 👽 or 🪐)");
            }
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Error checking commit message : {ex.Message}");
        }
    }
}