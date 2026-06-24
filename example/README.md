# Prek.DotNetTool Smoke Test

This example restores `Prek.DotNetTool` as a local .NET tool from a locally packed NuGet package and runs a minimal `prek` configuration.

From the repository root:

```bash
./example/smoke-test.sh
```

The smoke test verifies:

- the wrapper can be packed as a .NET tool
- the tool can be restored from `artifacts/package`
- `dotnet tool run prek --version` executes the native `prek` binary
- `dotnet tool run prek run --all-files` can run builtin hooks successfully
- `prek self update` is blocked by the wrapper
