using CharacterCounterMcp.Models;
using System.Text.Json;

namespace CharacterCounterMcp.Tools;

/// <summary>
/// Character counting tool implementations
/// </summary>
public class CharacterCounter
{
    /// <summary>
    /// Gets all available tools for character counting
    /// </summary>
    public static List<McpTool> GetTools()
    {
        return new List<McpTool>
        {
            new McpTool
            {
                Name = "count_characters",
                Description = "Count occurrences of a specific character in text (case-sensitive). Perfect for counting 'r' in 'strawberry'!",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        text = new
                        {
                            type = "string",
                            description = "The text to analyze"
                        },
                        character = new
                        {
                            type = "string",
                            description = "The character to count (single character)"
                        }
                    },
                    required = new[] { "text", "character" }
                }
            },
            new McpTool
            {
                Name = "count_characters_ignore_case",
                Description = "Count occurrences of a character in text, ignoring case differences",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        text = new
                        {
                            type = "string",
                            description = "The text to analyze"
                        },
                        character = new
                        {
                            type = "string",
                            description = "The character to count (single character)"
                        }
                    },
                    required = new[] { "text", "character" }
                }
            },
            new McpTool
            {
                Name = "get_character_stats",
                Description = "Get detailed statistics about all characters in the text",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        text = new
                        {
                            type = "string",
                            description = "The text to analyze"
                        }
                    },
                    required = new[] { "text" }
                }
            }
        };
    }

    /// <summary>
    /// Executes a character counting tool
    /// </summary>
    public static ToolResult ExecuteTool(string toolName, Dictionary<string, object> arguments)
    {
        try
        {
            return toolName switch
            {
                "count_characters" => CountCharacters(arguments),
                "count_characters_ignore_case" => CountCharactersIgnoreCase(arguments),
                "get_character_stats" => GetCharacterStats(arguments),
                _ => throw new ArgumentException($"Unknown tool: {toolName}")
            };
        }
        catch (Exception ex)
        {
            return new ToolResult
            {
                Content = new List<ToolContent>
                {
                    new ToolContent
                    {
                        Type = "text",
                        Text = $"Error executing tool '{toolName}': {ex.Message}"
                    }
                },
                IsError = true
            };
        }
    }

    private static ToolResult CountCharacters(Dictionary<string, object> arguments)
    {
        var text = GetStringArgument(arguments, "text");
        var character = GetStringArgument(arguments, "character");

        if (character.Length != 1)
        {
            throw new ArgumentException("Character must be a single character");
        }

        var count = text.Count(c => c == character[0]);

        var result = new
        {
            text = text,
            character = character,
            count = count,
            analysis = $"Found {count} occurrence(s) of '{character}' in \"{text}\""
        };

        return new ToolResult
        {
            Content = new List<ToolContent>
            {
                new ToolContent
                {
                    Type = "text",
                    Text = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true })
                }
            },
            IsError = false
        };
    }

    private static ToolResult CountCharactersIgnoreCase(Dictionary<string, object> arguments)
    {
        var text = GetStringArgument(arguments, "text");
        var character = GetStringArgument(arguments, "character");

        if (character.Length != 1)
        {
            throw new ArgumentException("Character must be a single character");
        }

        var lowerText = text.ToLowerInvariant();
        var lowerChar = char.ToLowerInvariant(character[0]);
        var count = lowerText.Count(c => c == lowerChar);

        var result = new
        {
            text = text,
            character = character,
            count = count,
            caseSensitive = false,
            analysis = $"Found {count} occurrence(s) of '{character}' in \"{text}\" (case-insensitive)"
        };

        return new ToolResult
        {
            Content = new List<ToolContent>
            {
                new ToolContent
                {
                    Type = "text",
                    Text = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true })
                }
            },
            IsError = false
        };
    }

    private static ToolResult GetCharacterStats(Dictionary<string, object> arguments)
    {
        var text = GetStringArgument(arguments, "text");

        var charCounts = new Dictionary<char, int>();
        foreach (var c in text)
        {
            charCounts[c] = charCounts.GetValueOrDefault(c, 0) + 1;
        }

        var stats = charCounts
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Select(kvp => new { character = kvp.Key.ToString(), count = kvp.Value })
            .ToList();

        var result = new
        {
            text = text,
            totalCharacters = text.Length,
            uniqueCharacters = charCounts.Count,
            characterBreakdown = stats,
            mostFrequent = stats.FirstOrDefault(),
            analysis = $"Text contains {text.Length} total characters with {charCounts.Count} unique characters"
        };

        return new ToolResult
        {
            Content = new List<ToolContent>
            {
                new ToolContent
                {
                    Type = "text",
                    Text = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true })
                }
            },
            IsError = false
        };
    }

    private static string GetStringArgument(Dictionary<string, object> arguments, string key)
    {
        if (!arguments.TryGetValue(key, out var value))
        {
            throw new ArgumentException($"Missing required argument: {key}");
        }

        return value switch
        {
            string s => s,
            JsonElement element when element.ValueKind == JsonValueKind.String => element.GetString()!,
            _ => value.ToString() ?? throw new ArgumentException($"Invalid argument type for {key}")
        };
    }
}
