namespace Prek.DotNetTool;

internal static class Assets
{
    public static readonly Asset MacOsArm64 = new(
        "aarch64-apple-darwin",
        "prek-aarch64-apple-darwin.tar.gz",
        "d6d705468c95ac01a9768da952645385a8e3cb93ec9a105f9f0f7bd177ae3867",
        "prek",
        "prek");

    public static readonly Asset MacOsX64 = new(
        "x86_64-apple-darwin",
        "prek-x86_64-apple-darwin.tar.gz",
        "86ce383f8d40ae874e432fe6db04c2253cf2bed2cd74e67c8e3bc4b16e158471",
        "prek",
        "prek");

    public static readonly Asset WindowsArm64 = new(
        "aarch64-pc-windows-msvc",
        "prek-aarch64-pc-windows-msvc.zip",
        "9eba043c3345ff669c5587811d7acead1abcad8089f467579f2ad4b136f771a5",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX64 = new(
        "x86_64-pc-windows-msvc",
        "prek-x86_64-pc-windows-msvc.zip",
        "e49a7c8fedb4f16038556e19c8bf7a68ffae46445ef693b28c1fa461f03df2c7",
        "prek.exe",
        "prek.exe");

    public static readonly Asset WindowsX86 = new(
        "i686-pc-windows-msvc",
        "prek-i686-pc-windows-msvc.zip",
        "36e9e7bec5f4bba3868119b0c58142f042a3811c4e61e42cf12761466a75c00e",
        "prek.exe",
        "prek.exe");

    public static readonly Asset LinuxGnuArm64 = new(
        "aarch64-unknown-linux-gnu",
        "prek-aarch64-unknown-linux-gnu.tar.gz",
        "50690508a6152aaba9599543a604ee7764ef9260f994636fe493490e2efd30a2",
        "prek",
        "prek");

    public static readonly Asset LinuxGnuX64 = new(
        "x86_64-unknown-linux-gnu",
        "prek-x86_64-unknown-linux-gnu.tar.gz",
        "3548b731f3fb150b31030aebb74a539f52c8feec2ad96f674904633c3a1b7d6c",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslArm64 = new(
        "aarch64-unknown-linux-musl",
        "prek-aarch64-unknown-linux-musl.tar.gz",
        "adcd6d3b2c6d2314525c214ead1a3584de3ea3dfd608262d60711c3dde4d5e82",
        "prek",
        "prek");

    public static readonly Asset LinuxMuslX64 = new(
        "x86_64-unknown-linux-musl",
        "prek-x86_64-unknown-linux-musl.tar.gz",
        "22545eee5091ce233192bc97c6edb7f045081ec615e4649633d5ffad97937658",
        "prek",
        "prek");
}
