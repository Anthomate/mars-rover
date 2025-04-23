namespace CITools.Models;

public class CommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int ExitCode => Success ? 0 : 1;

    public CommandResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
    public static CommandResult Ok(string message) => new(true, message);
    public static CommandResult Error(string message) => new(false, message);
}