namespace Prek.DotNetTool;

internal static class Assets
{
    public static readonly Asset MacOsArm64 = new(
        "aarch64-apple-darwin",
        "prek-aarch64-apple-darwin.tar.gz",
        "ed4920762f0e3db07163c437af622a1d4905e1ad7053692a495551b9ce92549f",
        "prek",
        "prek");

    public static readonly Asset MacOsX64 = new(
        "x86_64-apple-darwin",
        "prek-x86_64-apple-darwin.tar.gz",
        "b0afc8bbea69d61bbfe49100394df99eed15a9c4675b06c9936417e65aa88b16",
        "prek",
        "prek");

    public static readonly Asset WindowsArm64 = new(
        "aarch64-pc-windows-msvc",
        "prek-aarch64-pc-windows-msvc.zip",
        "4f047fa4c0feb7d0379841c24c4c66a54d38dd1fa2fb0e83a0ef8d6678cb12e1",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX64 = new(
        "x86_64-pc-windows-msvc",
        "prek-x86_64-pc-windows-msvc.zip",
        "1ee0d2b210acc805e0ccd1b00fa84eb1d0d939f1a79da37ae9f432e1c16c5986",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX86 = new(
        "i686-pc-windows-msvc",
        "prek-i686-pc-windows-msvc.zip",
        "6c68300c052b902abbf95bc92b3fbbc1a438fba76d3fa5f5d8dfcff7179f042c",
        "prek.exe",
        "prek.exe");

    public static readonly Asset LinuxGnuArm64 = new(
        "aarch64-unknown-linux-gnu",
        "prek-aarch64-unknown-linux-gnu.tar.gz",
        "6a41437fd68641de79eaac3d6dd6667fa38512c93a73f661a266de2b12557b1b",
        "prek",
        "prek");

    public static readonly Asset LinuxGnuX64 = new(
        "x86_64-unknown-linux-gnu",
        "prek-x86_64-unknown-linux-gnu.tar.gz",
        "dc86e18e532516dd629ee621ab76bc4c4761052219b27e3af1e47cbf9a71a270",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslArm64 = new(
        "aarch64-unknown-linux-musl",
        "prek-aarch64-unknown-linux-musl.tar.gz",
        "9c3f0686b6f12995b2f9cbc2953a457987622a29bbcdeaa64f62527af851fc1e",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslX64 = new(
        "x86_64-unknown-linux-musl",
        "prek-x86_64-unknown-linux-musl.tar.gz",
        "94698d1cd74e7b462a017257efeab6f46142f4fb3da9a1314f06c1d55c26d614",
        "prek",
        "prek");
}
