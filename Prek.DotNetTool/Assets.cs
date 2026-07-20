namespace Prek.DotNetTool;

internal static class Assets
{
    public static readonly Asset MacOsArm64 = new(
        "aarch64-apple-darwin",
        "prek-aarch64-apple-darwin.tar.gz",
        "b2be74cd80bc86f679d92ae368d154ce391e4ef9891dc54447976b92df7951f1",
        "prek",
        "prek");

    public static readonly Asset MacOsX64 = new(
        "x86_64-apple-darwin",
        "prek-x86_64-apple-darwin.tar.gz",
        "ae0e42f5aec02898531d6b3b7dca97967a5d128cba8df34bc2a586742e2d847d",
        "prek",
        "prek");

    public static readonly Asset WindowsArm64 = new(
        "aarch64-pc-windows-msvc",
        "prek-aarch64-pc-windows-msvc.zip",
        "ef9b5d6c643ae2ef2d73b60a0706e3303c5d1eab6a7c3e909ab95e693827be2e",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX64 = new(
        "x86_64-pc-windows-msvc",
        "prek-x86_64-pc-windows-msvc.zip",
        "872d1c5ac32efce01e93821e790dedc8c055b499f524f1250270679562bf3792",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX86 = new(
        "i686-pc-windows-msvc",
        "prek-i686-pc-windows-msvc.zip",
        "873366975c19aef30a1b230f3a66acb66e1e7fdd796248c3d602dace05725b67",
        "prek.exe",
        "prek.exe");

    public static readonly Asset LinuxGnuArm64 = new(
        "aarch64-unknown-linux-gnu",
        "prek-aarch64-unknown-linux-gnu.tar.gz",
        "f562f7d1cfebc4ffa5e119e2ff196c23785d3adfd400f18f744aa14c144aeefe",
        "prek",
        "prek");

    public static readonly Asset LinuxGnuX64 = new(
        "x86_64-unknown-linux-gnu",
        "prek-x86_64-unknown-linux-gnu.tar.gz",
        "446a4471b9d3e651762ade734865ae092392f42a4e931fcebcb4a508928a6d8d",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslArm64 = new(
        "aarch64-unknown-linux-musl",
        "prek-aarch64-unknown-linux-musl.tar.gz",
        "9e743f493f681e84b94e65bec1199d992643f0bf4ce0929880298b3b4747989e",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslX64 = new(
        "x86_64-unknown-linux-musl",
        "prek-x86_64-unknown-linux-musl.tar.gz",
        "d31168fcbfe9921ad63572dc7ddbee07be5d5d6befe11e6e7397e3861a4edd49",
        "prek",
        "prek");
}
