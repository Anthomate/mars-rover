using System.Text.RegularExpressions;
using CITools.Models;

namespace CITools.Commands;

public static class CircularReferenceDetector
{
    public static CommandResult Execute(string projectPath, string includePattern = "*.cs", string? excludePattern = null)
    {
        try
        {
            Console.WriteLine($"Analysis of circular references in {projectPath}...");
            
            var csharpFiles = GetCSharpFiles(projectPath, includePattern, excludePattern);
            Console.WriteLine($"{csharpFiles.Count} found to analyze.");
            
            if (csharpFiles.Count == 0)
            {
                return CommandResult.Error("No C# files found in the project.");
            }
            
            var classNodes = ExtractClassesAndDependencies(csharpFiles);
            Console.WriteLine($"{classNodes.Count} classes found to analyze.");
            
            Console.WriteLine("Detected classes and dependencies :");
            foreach (var node in classNodes.Values)
            {
                Console.WriteLine($"  {node}");
            }
            
            if (classNodes.Count == 0)
            {
                return CommandResult.Ok("No classes found in scanned files.");
            }
            
            var circularReferences = DetectCircularReferences(classNodes);
            
            if (circularReferences.Count == 0)
            {
                return CommandResult.Ok("✅ No circular reference detected.");
            }
            else
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"⚠️ {circularReferences.Count} circular references found:");
                
                foreach (var cycle in circularReferences)
                {
                    sb.AppendLine("  " + string.Join(" → ", cycle) + " → " + cycle[0]);
                }
                
                return CommandResult.Error(sb.ToString());
            }
        }
        catch (Exception ex)
        {
            return CommandResult.Error($"Error parsing circular references: {ex.Message}\n{ex.StackTrace}");
        }
    }
    
    private static List<string> GetCSharpFiles(string projectPath, string includePattern, string? excludePattern)
    {
        var files = Directory.GetFiles(projectPath, includePattern, SearchOption.AllDirectories);
        
        if (excludePattern != null)
        {
            var excludeRegex = new Regex(excludePattern, RegexOptions.IgnoreCase);
            files = files.Where(f => !excludeRegex.IsMatch(f)).ToArray();
        }
        
        return files.ToList();
    }
    
    private static Dictionary<string, ClassNode> ExtractClassesAndDependencies(List<string> csharpFiles)
    {
        var classNodes = new Dictionary<string, ClassNode>();
        
        var classRegex = new Regex(@"(?:public|internal|private|protected|static)?\s+(?:abstract|sealed|partial)?\s*class\s+(\w+)", RegexOptions.Compiled);
        
        foreach (var file in csharpFiles)
        {
            var content = File.ReadAllText(file);
            
            var classMatches = classRegex.Matches(content);
            foreach (Match match in classMatches)
            {
                var className = match.Groups[1].Value;
                if (!classNodes.ContainsKey(className))
                {
                    classNodes[className] = new ClassNode(className, file);
                }
            }
        }
        
        foreach (var file in csharpFiles)
        {
            var content = File.ReadAllText(file);
            var classNamesInFile = classRegex.Matches(content)
                                            .Cast<Match>()
                                            .Select(m => m.Groups[1].Value)
                                            .ToList();
            
            if (classNamesInFile.Count == 0)
                continue;
            
            var referencedTypes = new HashSet<string>();
            
            var propertyRegex = new Regex(@"(?:public|private|protected|internal)\s+([A-Za-z0-9_<>.]+(?:\[\])?)\s+([A-Za-z0-9_]+)\s*\{", RegexOptions.Compiled);
            var propertyMatches = propertyRegex.Matches(content);
            
            foreach (Match match in propertyMatches)
            {
                var typeName = match.Groups[1].Value;
                
                var genericTypeRegex = new Regex(@"<([A-Za-z0-9_]+)>", RegexOptions.Compiled);
                var genericMatches = genericTypeRegex.Matches(typeName);
                
                foreach (Match genericMatch in genericMatches)
                {
                    var genericType = genericMatch.Groups[1].Value;
                    if (classNodes.ContainsKey(genericType) && !classNamesInFile.Contains(genericType))
                    {
                        referencedTypes.Add(genericType);
                    }
                }
                
                var baseType = typeName.Split('<')[0].Trim();
                if (classNodes.ContainsKey(baseType) && !classNamesInFile.Contains(baseType))
                {
                    referencedTypes.Add(baseType);
                }
            }
            
            var methodParamRegex = new Regex(@"(?:public|private|protected|internal)\s+(?:[A-Za-z0-9_<>.]+(?:\[\])?)\s+[A-Za-z0-9_]+\s*\(\s*([^)]*)\s*\)", RegexOptions.Compiled);
            var methodMatches = methodParamRegex.Matches(content);
            
            foreach (Match match in methodMatches)
            {
                var parameters = match.Groups[1].Value;
                if (string.IsNullOrEmpty(parameters))
                    continue;
                
                var paramList = parameters.Split(',');
                foreach (var param in paramList)
                {
                    var paramParts = param.Trim().Split(' ');
                    if (paramParts.Length >= 2)
                    {
                        var paramType = paramParts[0].Trim();
                        
                        var genericTypeRegex = new Regex(@"<([A-Za-z0-9_]+)>", RegexOptions.Compiled);
                        var genericMatches = genericTypeRegex.Matches(paramType);
                        
                        foreach (Match genericMatch in genericMatches)
                        {
                            var genericType = genericMatch.Groups[1].Value;
                            if (classNodes.ContainsKey(genericType) && !classNamesInFile.Contains(genericType))
                            {
                                referencedTypes.Add(genericType);
                            }
                        }
                        
                        var baseType = paramType.Split('<')[0].Trim();
                        if (classNodes.ContainsKey(baseType) && !classNamesInFile.Contains(baseType))
                        {
                            referencedTypes.Add(baseType);
                        }
                    }
                }
            }
            
            var localVarRegex = new Regex(@"(?:var|[A-Za-z0-9_<>.]+(?:\[\])?)\s+([A-Za-z0-9_]+)\s*=", RegexOptions.Compiled);
            var localVarMatches = localVarRegex.Matches(content);
            
            foreach (Match match in localVarMatches)
            {
                var varDecl = match.Value;
                var typePart = varDecl.Split('=')[0].Trim();
                
                if (!typePart.StartsWith("var"))
                {
                    var typeName = typePart.Substring(0, typePart.LastIndexOf(' ')).Trim();
                    
                    var genericTypeRegex = new Regex(@"<([A-Za-z0-9_]+)>", RegexOptions.Compiled);
                    var genericMatches = genericTypeRegex.Matches(typeName);
                    
                    foreach (Match genericMatch in genericMatches)
                    {
                        var genericType = genericMatch.Groups[1].Value;
                        if (classNodes.ContainsKey(genericType) && !classNamesInFile.Contains(genericType))
                        {
                            referencedTypes.Add(genericType);
                        }
                    }
                    
                    var baseType = typeName.Split('<')[0].Trim();
                    if (classNodes.ContainsKey(baseType) && !classNamesInFile.Contains(baseType))
                    {
                        referencedTypes.Add(baseType);
                    }
                }
            }
            
            foreach (var className in classNamesInFile)
            {
                if (classNodes.TryGetValue(className, out var classNode))
                {
                    foreach (var referencedType in referencedTypes)
                    {
                        if (classNodes.ContainsKey(referencedType))
                        {
                            classNode.Dependencies.Add(referencedType);
                        }
                    }
                }
            }
        }
        
        return classNodes;
    }
    
    private static List<List<string>> DetectCircularReferences(Dictionary<string, ClassNode> classNodes)
    {
        var circularReferences = new List<List<string>>();
        
        foreach (var className in classNodes.Keys)
        {
            var visited = new HashSet<string>();
            var path = new List<string>();
            
            Dfs(className, classNodes, visited, path, circularReferences);
        }
        
        var uniqueCircularRefs = new List<List<string>>();
        foreach (var cycle in circularReferences)
        {
            var normalized = NormalizeCycle(cycle);
            if (!uniqueCircularRefs.Any(c => 
                c.Count == normalized.Count && 
                !c.Except(normalized).Any()))
            {
                uniqueCircularRefs.Add(normalized);
            }
        }
        
        return uniqueCircularRefs;
    }

    private static void Dfs(
        string current, 
        Dictionary<string, ClassNode> classNodes, 
        HashSet<string> visited,
        List<string> path,
        List<List<string>> cycles)
    {
        if (path.Contains(current))
        {
            var cycleStart = path.IndexOf(current);
            var cycle = path.Skip(cycleStart).ToList();
            cycles.Add(new List<string>(cycle));
            return;
        }
        
        if (!visited.Add(current))
            return;

        path.Add(current);
        
        if (classNodes.TryGetValue(current, out var classNode))
        {
            foreach (var dependency in classNode.Dependencies)
            {
                Dfs(dependency, classNodes, new HashSet<string>(visited), new List<string>(path), cycles);
            }
        }
        
        path.RemoveAt(path.Count - 1);
    }
    
    private static List<string> NormalizeCycle(List<string> cycle)
    {
        if (cycle.Count <= 1)
            return cycle;
        
        var minIndex = 0;
        var minValue = cycle[0];
        
        for (int i = 1; i < cycle.Count; i++)
        {
            if (string.Compare(cycle[i], minValue, StringComparison.Ordinal) < 0)
            {
                minIndex = i;
                minValue = cycle[i];
            }
        }
        
        var normalized = new List<string>();
        for (int i = 0; i < cycle.Count; i++)
        {
            normalized.Add(cycle[(minIndex + i) % cycle.Count]);
        }
        
        return normalized;
    }
}