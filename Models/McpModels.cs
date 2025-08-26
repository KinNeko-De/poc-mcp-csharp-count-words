using System.Text.Json.Serialization;

namespace CharacterCounterMcp.Models;

/// <summary>
/// Base class for all MCP messages
/// </summary>
public abstract class McpMessage
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";
}

/// <summary>
/// MCP request message
/// </summary>
public class McpRequest : McpMessage
{
    [JsonPropertyName("id")]
    public object? Id { get; set; }

    [JsonPropertyName("method")]
    public required string Method { get; set; }

    [JsonPropertyName("params")]
    public object? Params { get; set; }
}

/// <summary>
/// MCP response message
/// </summary>
public class McpResponse : McpMessage
{
    [JsonPropertyName("id")]
    public object? Id { get; set; }

    [JsonPropertyName("result")]
    public object? Result { get; set; }

    [JsonPropertyName("error")]
    public McpError? Error { get; set; }
}

/// <summary>
/// MCP error information
/// </summary>
public class McpError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

/// <summary>
/// Tool definition for MCP
/// </summary>
public class McpTool
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("inputSchema")]
    public required object InputSchema { get; set; }
}

/// <summary>
/// Tool call parameters
/// </summary>
public class ToolCallParams
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("arguments")]
    public required Dictionary<string, object> Arguments { get; set; }
}

/// <summary>
/// Tool call result
/// </summary>
public class ToolResult
{
    [JsonPropertyName("content")]
    public required List<ToolContent> Content { get; set; }

    [JsonPropertyName("isError")]
    public bool IsError { get; set; }
}

/// <summary>
/// Tool content item
/// </summary>
public class ToolContent
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

/// <summary>
/// Server capabilities
/// </summary>
public class ServerCapabilities
{
    [JsonPropertyName("tools")]
    public object? Tools { get; set; }
}

/// <summary>
/// Implementation information
/// </summary>
public class Implementation
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }
}

/// <summary>
/// Initialize result
/// </summary>
public class InitializeResult
{
    [JsonPropertyName("protocolVersion")]
    public required string ProtocolVersion { get; set; }

    [JsonPropertyName("capabilities")]
    public required ServerCapabilities Capabilities { get; set; }

    [JsonPropertyName("serverInfo")]
    public required Implementation ServerInfo { get; set; }
}
