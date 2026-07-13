using System.Diagnostics;
using System.Formats.Tar;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Prek.DotNetTool;

internal static class PrekTool
{
    public const string UpstreamVersion = "0.4.9";

    private static readonly HttpClient HttpClient = new();

    public static async Task<int> RunAsync(string[] args)
    {
        if (IsSelfUpdate(args))
        {
            Console.Error.WriteLine(
                "prek self update is disabled for this .NET tool package. Use `dotnet tool update Prek.DotNetTool` instead.");
            return 1;
        }

        try
        {
            var asset = Asset.Current();
            var executablePath = await EnsurePrekAsync(asset, CancellationToken.None);
            return await ExecuteAsync(executablePath, args, CancellationToken.None);
        }
        catch (PrekToolException exception)
        {
            Console.Error.WriteLine($"prek-dotnet-tool: {exception.Message}");
            return 1;
        }
        catch (HttpRequestException exception)
        {
            Console.Error.WriteLine($"prek-dotnet-tool: failed to download prek: {exception.Message}");
            return 1;
        }
        catch (IOException exception)
        {
            Console.Error.WriteLine($"prek-dotnet-tool: failed to prepare prek: {exception.Message}");
            return 1;
        }
        catch (InvalidDataException exception)
        {
            Console.Error.WriteLine($"prek-dotnet-tool: failed to extract prek: {exception.Message}");
            return 1;
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.Error.WriteLine($"prek-dotnet-tool: failed to access prek cache: {exception.Message}");
            return 1;
        }
    }

    private static bool IsSelfUpdate(string[] args) =>
        args is ["self", "update", ..];

    private static async Task<string> EnsurePrekAsync(Asset asset, CancellationToken cancellationToken)
    {
        var installDirectory = Path.Combine(GetCacheRoot(), UpstreamVersion, asset.PlatformKey);
        var executablePath = Path.Combine(installDirectory, asset.ExecutableName);

        if (File.Exists(executablePath) && await FileMatchesHashAsync(executablePath, asset.Sha256, cancellationToken))
        {
            return executablePath;
        }

        var temporaryDirectory = Path.Combine(Path.GetTempPath(), $"prek-dotnet-tool-{Guid.NewGuid():N}");

        try
        {
            Directory.CreateDirectory(temporaryDirectory);

            var archivePath = Path.Combine(temporaryDirectory, asset.FileName);
            await DownloadAsync(asset.DownloadUri, archivePath, asset.Sha256, cancellationToken);

            var extractDirectory = Path.Combine(temporaryDirectory, "extract");
            Directory.CreateDirectory(extractDirectory);
            ExtractArchive(archivePath, extractDirectory);

            var extractedExecutablePath = FindExtractedExecutable(extractDirectory, asset.ExecutableName);
            if (!File.Exists(extractedExecutablePath))
            {
                throw new PrekToolException($"Downloaded archive did not contain {asset.ExecutableName}.");
            }

            Directory.CreateDirectory(installDirectory);
            File.Copy(extractedExecutablePath, executablePath, overwrite: true);
            MakeExecutable(executablePath);

            return executablePath;
        }
        finally
        {
            DeleteDirectoryIfExists(temporaryDirectory);
        }
    }

    private static string GetCacheRoot()
    {
        var configuredCache = Environment.GetEnvironmentVariable("PREK_DOTNET_TOOL_CACHE");
        if (!string.IsNullOrWhiteSpace(configuredCache))
        {
            return configuredCache;
        }

        var localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!string.IsNullOrWhiteSpace(localApplicationData))
        {
            return Path.Combine(localApplicationData, "prek-dotnet-tool");
        }

        return Path.Combine(Path.GetTempPath(), "prek-dotnet-tool");
    }

    private static async Task DownloadAsync(
        Uri uri,
        string archivePath,
        string expectedSha256,
        CancellationToken cancellationToken)
    {
        await using (var source = await HttpClient.GetStreamAsync(uri, cancellationToken))
        await using (var destination = File.Create(archivePath))
        {
            await source.CopyToAsync(destination, cancellationToken);
        }

        if (!await FileMatchesHashAsync(archivePath, expectedSha256, cancellationToken))
        {
            throw new PrekToolException($"Checksum verification failed for {Path.GetFileName(archivePath)}.");
        }
    }

    private static async Task<bool> FileMatchesHashAsync(
        string path,
        string expectedSha256,
        CancellationToken cancellationToken)
    {
        await using var stream = File.OpenRead(path);
        var hashBytes = await SHA256.HashDataAsync(stream, cancellationToken);
        var actualSha256 = Convert.ToHexStringLower(hashBytes);
        return string.Equals(actualSha256, expectedSha256, StringComparison.OrdinalIgnoreCase);
    }

    private static void ExtractArchive(string archivePath, string destinationDirectory)
    {
        if (archivePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            ZipFile.ExtractToDirectory(archivePath, destinationDirectory, overwriteFiles: true);
            return;
        }

        if (archivePath.EndsWith(".tar.gz", StringComparison.OrdinalIgnoreCase))
        {
            using var fileStream = File.OpenRead(archivePath);
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
            TarFile.ExtractToDirectory(gzipStream, destinationDirectory, overwriteFiles: true);
            return;
        }

        throw new PrekToolException($"Unsupported archive type: {Path.GetFileName(archivePath)}.");
    }

    private static string FindExtractedExecutable(string extractDirectory, string executableName)
    {
        var matches = Directory.EnumerateFiles(extractDirectory, executableName, SearchOption.AllDirectories).ToArray();

        return matches.Length switch
        {
            1 => matches[0],
            0 => string.Empty,
            _ => throw new PrekToolException($"Downloaded archive contained multiple {executableName} files."),
        };
    }

    private static void MakeExecutable(string executablePath)
    {
        if (OperatingSystem.IsWindows())
        {
            return;
        }

        File.SetUnixFileMode(
            executablePath,
            UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute
                | UnixFileMode.GroupRead | UnixFileMode.GroupExecute
                | UnixFileMode.OtherRead | UnixFileMode.OtherExecute);
    }

    private static async Task<int> ExecuteAsync(
        string executablePath,
        string[] args,
        CancellationToken cancellationToken)
    {
        using var process = new Process();
        process.StartInfo.FileName = executablePath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Environment["PREK_DISABLE_UPDATE"] = "1";
        process.StartInfo.Environment["PREK_UNMANAGED_INSTALL"] = "1";

        foreach (var arg in args)
        {
            process.StartInfo.ArgumentList.Add(arg);
        }

        try
        {
            process.Start();
        }
        catch (System.ComponentModel.Win32Exception exception)
        {
            throw new PrekToolException($"Failed to start prek: {exception.Message}");
        }

        await process.WaitForExitAsync(cancellationToken);
        return process.ExitCode;
    }

    private static void DeleteDirectoryIfExists(string directory)
    {
        try
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive: true);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
