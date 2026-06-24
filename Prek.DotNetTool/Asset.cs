using System.Runtime.InteropServices;

namespace Prek.DotNetTool;

internal sealed record Asset(
    string PlatformKey,
    string FileName,
    string Sha256,
    string ExecutablePath,
    string ExecutableName)
{
    public Uri DownloadUri => new($"https://github.com/j178/prek/releases/download/v{PrekTool.UpstreamVersion}/{FileName}");

    public static Asset Current()
    {
        if (OperatingSystem.IsMacOS())
        {
            return RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.Arm64 => Assets.MacOsArm64,
                Architecture.X64 => Assets.MacOsX64,
                _ => throw new PrekToolException($"Unsupported macOS architecture: {RuntimeInformation.ProcessArchitecture}"),
            };
        }

        if (OperatingSystem.IsWindows())
        {
            return RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.Arm64 => Assets.WindowsArm64,
                Architecture.X64 => Assets.WindowsX64,
                Architecture.X86 => Assets.WindowsX86,
                _ => throw new PrekToolException($"Unsupported Windows architecture: {RuntimeInformation.ProcessArchitecture}"),
            };
        }

        if (OperatingSystem.IsLinux())
        {
            return CurrentLinux();
        }

        throw new PrekToolException($"Unsupported operating system: {RuntimeInformation.OSDescription}");
    }

    private static Asset CurrentLinux()
    {
        var isMusl = LinuxRuntime.IsMusl();

        return RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.Arm64 => isMusl ? Assets.LinuxMuslArm64 : Assets.LinuxGnuArm64,
            Architecture.X64 => isMusl ? Assets.LinuxMuslX64 : Assets.LinuxGnuX64,
            _ => throw new PrekToolException($"Unsupported Linux architecture: {RuntimeInformation.ProcessArchitecture}"),
        };
    }
}
