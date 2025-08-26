@echo off
REM Simple test script for the Character Counter MCP Server
REM This script demonstrates how to test the server manually

echo Building the project...
dotnet build

if %ERRORLEVEL% neq 0 (
    echo Build failed!
    exit /b 1
)

echo Starting MCP Server test...
echo You can test the server by sending JSON-RPC messages.
echo.
echo Example test messages:
echo.

echo 1. Initialize the server:
echo {"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}
echo.

echo 2. List available tools:
echo {"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}
echo.

echo 3. Count 'r' characters in 'strawberry':
echo {"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"count_characters","arguments":{"text":"strawberry","character":"r"}}}
echo.

echo 4. Get character statistics:
echo {"jsonrpc":"2.0","id":4,"method":"tools/call","params":{"name":"get_character_stats","arguments":{"text":"hello world"}}}
echo.

echo To test interactively, run: dotnet run
echo Then paste the JSON messages above (one per line) and press Enter.
echo.
echo To test with a file, create a file with JSON messages and run:
echo dotnet run ^< test_messages.json
