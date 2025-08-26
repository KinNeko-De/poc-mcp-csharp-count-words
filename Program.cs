using CharacterCounterMcp;

/// <summary>
/// Entry point for the Character Counter MCP Server
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        // Write startup message to stderr so it doesn't interfere with MCP protocol
        await Console.Error.WriteLineAsync("Starting Character Counter MCP Server...");
        await Console.Error.WriteLineAsync("This server provides tools to count characters in text.");
        await Console.Error.WriteLineAsync("Perfect for demonstrating that AI can indeed count 'r' letters in 'strawberry'!");
        
        var server = new McpServer();
        await server.StartAsync();
    }
}
