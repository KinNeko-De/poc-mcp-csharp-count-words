# Quick Start Guide

Get the Character Counter MCP Server running in 3 simple steps!

## 1. Build the Project
```bash
dotnet build CharacterCounterMcp.csproj
```

## 2. Run the Server
```bash
dotnet run --project CharacterCounterMcp.csproj
```

## 3. Test with Sample Messages

### Option A: Interactive Testing
Paste these JSON-RPC messages one at a time:

**Initialize the server:**
```json
{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}
```

**List available tools:**
```json
{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}
```

**Count 'r' in 'strawberry':**
```json
{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"count_characters","arguments":{"text":"strawberry","character":"r"}}}
```

### Option B: Batch Testing
```bash
dotnet run --project CharacterCounterMcp.csproj < test_messages.json
```

## Expected Results

You should see JSON responses showing:
- Server initialization confirmation
- List of 3 available tools
- Result showing 3 'r' characters in "strawberry"

## Next Steps

1. Read `TUTORIAL.md` for detailed implementation explanation
2. Check `EXAMPLES.md` for more test cases
3. See `TESTING.md` for comprehensive testing guide

## Troubleshooting

**Build fails?** Ensure you have .NET 8.0 SDK installed
**No response?** Check that you're sending properly formatted JSON
**Server exits?** Send an empty line or Ctrl+C to terminate properly

## Integration

To use with MCP clients:
1. Configure the client to run: `dotnet run --project CharacterCounterMcp.csproj`
2. The tools will appear in the client's available tools
3. Call them with the specified parameters

That's it! You now have a working MCP server that can accurately count characters. 🎉
