# Testing the Character Counter MCP Server

This document provides examples of how to test the character counting functionality.

## Manual Testing Examples

### Test 1: The Classic "Strawberry" Test
```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "tools/call",
  "params": {
    "name": "count_characters",
    "arguments": {
      "text": "strawberry",
      "character": "r"
    }
  }
}
```
**Expected Result**: 3 occurrences of 'r'

### Test 2: Case Sensitivity
```json
{
  "jsonrpc": "2.0",
  "id": 2,
  "method": "tools/call",
  "params": {
    "name": "count_characters",
    "arguments": {
      "text": "Programming",
      "character": "r"
    }
  }
}
```
**Expected Result**: 2 occurrences of 'r' (lowercase only)

### Test 3: Case-Insensitive Counting
```json
{
  "jsonrpc": "2.0",
  "id": 3,
  "method": "tools/call",
  "params": {
    "name": "count_characters_ignore_case",
    "arguments": {
      "text": "Programming",
      "character": "r"
    }
  }
}
```
**Expected Result**: 2 occurrences of 'r' (both lowercase r's)

### Test 4: Character Statistics
```json
{
  "jsonrpc": "2.0",
  "id": 4,
  "method": "tools/call",
  "params": {
    "name": "get_character_stats",
    "arguments": {
      "text": "hello world"
    }
  }
}
```
**Expected Result**: Detailed breakdown of all characters and their frequencies

## Running Tests

1. Build the project:
   ```bash
   dotnet build
   ```

2. Run the server:
   ```bash
   dotnet run
   ```

3. Send JSON-RPC messages via stdin. You can use a tool like `jq` or create a simple test script.

## Common Issues and Solutions

### Issue: "Character must be a single character"
**Solution**: Ensure the `character` parameter contains exactly one character.

### Issue: Server not responding
**Solution**: Ensure you're sending properly formatted JSON-RPC 2.0 messages with correct line endings.

### Issue: Invalid JSON
**Solution**: Validate your JSON syntax before sending to the server.

## Integration with MCP Clients

This server is designed to work with any MCP-compatible client. The tools will appear in the client's tool list and can be called with the specified parameters.
