# Prek.DotNetTool

A .NET local/global tool wrapper for [`prek`](https://github.com/j178/prek).

The wrapper pins an upstream `prek` release, downloads the matching native binary for the current platform, verifies its SHA-256 checksum, caches it, and forwards all arguments to the native executable.

`prek self update` is intentionally disabled. Update this tool with `dotnet tool update` so the NuGet package version remains the source of truth.

## Development

```bash
dotnet build
dotnet run --project Prek.DotNetTool -- --version
dotnet pack Prek.DotNetTool
```
# prek-dotnet-tool
