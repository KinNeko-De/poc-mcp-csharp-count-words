# C# MCP Server Tutorial: Character Counter

This repository demonstrates how to build a Model Context Protocol (MCP) server in C# that provides character counting functionality. Perfect for demonstrating how AI can accurately count characters - including the famous "r" letters in "strawberry"!

## Overview

This MCP server provides tools to count characters in text, addressing the common meme about AI's ability to count specific characters. The server implements the MCP specification to expose character counting capabilities to MCP clients.

## Features

- **count_characters**: Count occurrences of a specific character in text (case-sensitive)
- **count_characters_ignore_case**: Count occurrences of a character ignoring case
- **get_character_stats**: Get detailed statistics about all characters in text

## Quick Start

1. Build the project:
   ```bash
   dotnet build
   ```

2. Run the server:
   ```bash
   dotnet run
   ```

3. The server will start and listen for MCP protocol messages via stdin/stdout.

## Project Structure

```
├── Program.cs              # Main entry point
├── McpServer.cs           # Core MCP server implementation
├── Tools/                 # Tool implementations
│   └── CharacterCounter.cs
├── Models/                # Data models for MCP protocol
├── CharacterCounterMcp.csproj
└── README.md
```

## Tutorial: Building the MCP Server

### Step 1: Understanding MCP Protocol

The Model Context Protocol (MCP) is a standard for connecting AI assistants to external tools and data sources. An MCP server exposes tools that can be called by MCP clients.

### Step 2: Project Setup

This project demonstrates a complete C# implementation of an MCP server with character counting tools.

### Step 3: Tool Implementation

The character counting tools showcase how to:
- Parse tool parameters
- Process text data
- Return structured results
- Handle edge cases

## Example Usage

Once connected to an MCP client, you can use tools like:

```json
{
  "name": "count_characters",
  "arguments": {
    "text": "strawberry",
    "character": "r"
  }
}
```

Result: `3` (there are indeed 3 'r' characters in "strawberry"!)

## License

See LICENSE file for details.
