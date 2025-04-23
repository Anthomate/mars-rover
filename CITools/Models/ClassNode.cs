namespace CITools.Models;

public class ClassNode
{
    public string Name { get; set; }
    public string FilePath { get; set; }
    public HashSet<string> Dependencies { get; set; } = new();
        
    public ClassNode(string name, string filePath)
    {
        Name = name;
        FilePath = filePath;
    }
    public override string ToString()
    {
        return $"{Name} -> [{string.Join(", ", Dependencies)}]";
    }
}