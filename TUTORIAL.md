# Tutorial: Building a C# MCP Server

This tutorial walks you through building a Model Context Protocol (MCP) server in C# that provides character counting tools.

## What You'll Learn

- Understanding the MCP protocol
- Implementing MCP messages and responses
- Creating tool definitions
- Handling tool execution
- Building a complete MCP server in C#

## Prerequisites

- .NET 8.0 or later
- Basic understanding of C# and JSON
- Familiarity with command-line tools

## Step 1: Understanding MCP

The Model Context Protocol (MCP) enables AI assistants to securely connect to external tools and data sources. An MCP server:

1. Exposes tools that can be called by clients
2. Communicates via JSON-RPC 2.0 over stdin/stdout
3. Provides structured responses to tool calls

## Step 2: Project Structure

Our MCP server consists of:

- **Models**: Data structures for MCP protocol messages
- **Tools**: Implementation of character counting functionality
- **McpServer**: Core server logic for handling requests
- **Program**: Entry point

## Step 3: MCP Protocol Implementation

### Key Components

1. **McpMessage**: Base class for all protocol messages
2. **McpRequest/McpResponse**: Request and response structures
3. **McpTool**: Tool definition with schema
4. **ToolResult**: Tool execution results

### Protocol Flow

1. Client sends `initialize` request
2. Server responds with capabilities
3. Client requests tool list via `tools/list`
4. Client calls tools via `tools/call`

## Step 4: Tool Implementation

Our character counter provides three tools:

### count_characters
Counts specific characters (case-sensitive)
```json
{
  "text": "strawberry",
  "character": "r"
}
```

### count_characters_ignore_case
Counts characters ignoring case
```json
{
  "text": "Programming", 
  "character": "r"
}
```

### get_character_stats
Provides detailed character statistics
```json
{
  "text": "hello world"
}
```

## Step 5: Error Handling

The server handles:
- Invalid JSON requests
- Unknown methods
- Missing parameters
- Tool execution errors

## Step 6: Testing

Test your server by:
1. Building with `dotnet build`
2. Running with `dotnet run`
3. Sending JSON-RPC messages via stdin

## Step 7: Integration

Connect your server to MCP clients to make the tools available to AI assistants.

## Why This Example Works

The "strawberry r-counting" example is perfect because:
- It's simple to understand
- Demonstrates exact character counting
- Addresses a common AI capability question
- Shows both case-sensitive and case-insensitive counting
- Provides clear, verifiable results

## Next Steps

1. Add more text analysis tools
2. Implement resource providers
3. Add configuration options
4. Create unit tests
5. Package for distribution

## Common Pitfalls

1. **JSON Format**: Ensure proper JSON-RPC 2.0 format
2. **Error Handling**: Always return proper error responses
3. **Schema Validation**: Validate tool parameters
4. **Stdin/Stdout**: Don't write to stdout except for responses

## Conclusion

You now have a working MCP server that demonstrates character counting capabilities. This foundation can be extended to create more complex tools and integrations.
