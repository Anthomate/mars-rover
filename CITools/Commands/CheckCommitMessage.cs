using System.Text.RegularExpressions;
using CITools.Helpers;
using CITools.Models;

namespace CITools.Commands;

public class CheckCommitMessage
{
    private static readonly string[] RequiredEmojis = { "🚀", "👽", "🪐" };
    
    public static CommandResult Execute(string repositoryPath = ".")
    {
        try
        {
            string commitMessage = GitHelper.GetLastCommitMessage(repositoryPath);
            
            Console.WriteLine($"Message de commit: {commitMessage}");
            
            bool containsEmoji = RequiredEmojis.Any(emoji => commitMessage.Contains(emoji));
            
            if (containsEmoji)
            {
                return CommandResult.Ok("Le message de commit contient un emoji spatial! ✓");
            }
            else
            {
                return CommandResult.Error("❌ Le message de commit ne contient pas d'emoji spatial requis (🚀, 👽 ou 🪐)");
            }
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Erreur lors de la vérification du message de commit: {ex.Message}");
        }
    }
}