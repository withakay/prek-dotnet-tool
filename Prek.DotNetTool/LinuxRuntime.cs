namespace Prek.DotNetTool;

internal static class LinuxRuntime
{
    public static bool IsMusl()
    {
        if (!OperatingSystem.IsLinux())
        {
            return false;
        }

        if (File.Exists("/etc/alpine-release"))
        {
            return true;
        }

        return HasMuslLoader("/lib") || HasMuslLoader("/usr/lib");
    }

    private static bool HasMuslLoader(string directory)
    {
        try
        {
            return Directory.Exists(directory)
                && Directory.EnumerateFiles(directory, "ld-musl-*.so.1").Any();
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }
}
