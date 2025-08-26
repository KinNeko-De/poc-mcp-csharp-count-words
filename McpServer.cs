using CharacterCounterMcp.Models;
using CharacterCounterMcp.Tools;
using System.Text.Json;

namespace CharacterCounterMcp;

/// <summary>
/// MCP Server implementation for character counting
/// </summary>
public class McpServer
{
    private readonly JsonSerializerOptions _jsonOptions;

    public McpServer()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    /// <summary>
    /// Starts the MCP server and handles incoming requests
    /// </summary>
    public async Task StartAsync()
    {
        var stdin = Console.OpenStandardInput();
        var stdout = Console.OpenStandardOutput();

        using var reader = new StreamReader(stdin);
        using var writer = new StreamWriter(stdout) { AutoFlush = true };

        while (true)
        {
            var line = await reader.ReadLineAsync();
            if (line == null) break;

            try
            {
                var request = JsonSerializer.Deserialize<McpRequest>(line, _jsonOptions);
                if (request != null)
                {
                    var response = await HandleRequestAsync(request);
                    var responseJson = JsonSerializer.Serialize(response, _jsonOptions);
                    await writer.WriteLineAsync(responseJson);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new McpResponse
                {
                    Id = null,
                    Error = new McpError
                    {
                        Code = -32603, // Internal error
                        Message = $"Internal server error: {ex.Message}"
                    }
                };
                var errorJson = JsonSerializer.Serialize(errorResponse, _jsonOptions);
                await writer.WriteLineAsync(errorJson);
            }
        }
    }

    private Task<McpResponse> HandleRequestAsync(McpRequest request)
    {
        var response = request.Method switch
        {
            "initialize" => HandleInitialize(request),
            "tools/list" => HandleToolsList(request),
            "tools/call" => HandleToolCall(request),
            _ => new McpResponse
            {
                Id = request.Id,
                Error = new McpError
                {
                    Code = -32601, // Method not found
                    Message = $"Method not found: {request.Method}"
                }
            }
        };
        return Task.FromResult(response);
    }

    private McpResponse HandleInitialize(McpRequest request)
    {
        var result = new InitializeResult
        {
            ProtocolVersion = "2024-11-05",
            Capabilities = new ServerCapabilities
            {
                Tools = new { }
            },
            ServerInfo = new Implementation
            {
                Name = "character-counter-mcp",
                Version = "1.0.0"
            }
        };

        return new McpResponse
        {
            Id = request.Id,
            Result = result
        };
    }

    private McpResponse HandleToolsList(McpRequest request)
    {
        var tools = CharacterCounter.GetTools();
        var result = new { tools = tools };

        return new McpResponse
        {
            Id = request.Id,
            Result = result
        };
    }

    private McpResponse HandleToolCall(McpRequest request)
    {
        try
        {
            if (request.Params is not JsonElement paramsElement)
            {
                throw new ArgumentException("Invalid parameters");
            }

            var toolCall = JsonSerializer.Deserialize<ToolCallParams>(paramsElement.GetRawText(), _jsonOptions);
            if (toolCall == null)
            {
                throw new ArgumentException("Failed to deserialize tool call parameters");
            }

            var result = CharacterCounter.ExecuteTool(toolCall.Name, toolCall.Arguments);

            return new McpResponse
            {
                Id = request.Id,
                Result = result
            };
        }
        catch (Exception ex)
        {
            return new McpResponse
            {
                Id = request.Id,
                Error = new McpError
                {
                    Code = -32602, // Invalid params
                    Message = ex.Message
                }
            };
        }
    }
}
