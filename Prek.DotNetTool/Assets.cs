namespace Prek.DotNetTool;

internal static class Assets
{
    public static readonly Asset MacOsArm64 = new(
        "aarch64-apple-darwin",
        "prek-aarch64-apple-darwin.tar.gz",
        "f0084a9b5ce467c00306b2b329cc929744e2723ed1e7c369929a60ab10196fcc",
        "prek",
        "prek");

    public static readonly Asset MacOsX64 = new(
        "x86_64-apple-darwin",
        "prek-x86_64-apple-darwin.tar.gz",
        "dc402357bc5074791f4f1bcf5ce8622da38272e210ff84a551d3044e8da6f05d",
        "prek",
        "prek");

    public static readonly Asset WindowsArm64 = new(
        "aarch64-pc-windows-msvc",
        "prek-aarch64-pc-windows-msvc.zip",
        "774b8d3b2b64a4c16b2f5555493f8b50983afb88e43393b33230c1fe5c0b2a02",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX64 = new(
        "x86_64-pc-windows-msvc",
        "prek-x86_64-pc-windows-msvc.zip",
        "7c17dba682812ab24e04a4982dccc228dbf74b695444bd9fa8cbb8c1dc78664c",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX86 = new(
        "i686-pc-windows-msvc",
        "prek-i686-pc-windows-msvc.zip",
        "78b08af72710b4777c88c38c82eed0f259cef45df2feeca1a503b4de25ecad46",
        "prek.exe",
        "prek.exe");

    public static readonly Asset LinuxGnuArm64 = new(
        "aarch64-unknown-linux-gnu",
        "prek-aarch64-unknown-linux-gnu.tar.gz",
        "4a4d6ee04ed8b9142548d52e9d1f3c22055a333af72022b52d3610a62ae084de",
        "prek",
        "prek");

    public static readonly Asset LinuxGnuX64 = new(
        "x86_64-unknown-linux-gnu",
        "prek-x86_64-unknown-linux-gnu.tar.gz",
        "9b6a7c2e825c1b916ab3bb843505902c07f3937975f935294852643510d721db",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslArm64 = new(
        "aarch64-unknown-linux-musl",
        "prek-aarch64-unknown-linux-musl.tar.gz",
        "379108e6b2db7d49277ad2986446097f442b252ef27ff07bd0cbf716de982eb1",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslX64 = new(
        "x86_64-unknown-linux-musl",
        "prek-x86_64-unknown-linux-musl.tar.gz",
        "21df9ac0c3d3c047e424be612cc55f5b243546f9ef5cde12ed134752312bdf7b",
        "prek",
        "prek");
}
