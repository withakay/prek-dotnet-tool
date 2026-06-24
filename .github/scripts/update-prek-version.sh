#!/usr/bin/env bash
set -euo pipefail

repo_root=$(git rev-parse --show-toplevel)
cd "${repo_root}"

write_output() {
  if [[ -n "${GITHUB_OUTPUT:-}" ]]; then
    printf '%s=%s\n' "$1" "$2" >> "${GITHUB_OUTPUT}"
  fi
}

latest_tag=$(gh api repos/j178/prek/releases/latest --jq '.tag_name')
latest_version=${latest_tag#v}
current_version=$(perl -ne 'print "$1\n" if /UpstreamVersion = "([^"]+)"/' Prek.DotNetTool/PrekTool.cs)

write_output current_version "${current_version}"
write_output upstream_version "${latest_version}"

if [[ "${current_version}" == "${latest_version}" ]]; then
  echo "Prek.DotNetTool already pins prek ${current_version}."
  write_output changed false
  exit 0
fi

export LATEST_VERSION="${latest_version}"
perl -0pi -e 's/(UpstreamVersion = ")[^"]+(")/${1}$ENV{LATEST_VERSION}$2/' Prek.DotNetTool/PrekTool.cs
perl -0pi -e 's/(<Version>)[^<]+(<\/Version>)/${1}$ENV{LATEST_VERSION}$2/' Prek.DotNetTool/Prek.DotNetTool.csproj
perl -0pi -e 's/("version": ")[^"]+(")/${1}$ENV{LATEST_VERSION}$2/' example/.config/dotnet-tools.json

assets=(
  prek-aarch64-apple-darwin.tar.gz
  prek-x86_64-apple-darwin.tar.gz
  prek-aarch64-pc-windows-msvc.zip
  prek-x86_64-pc-windows-msvc.zip
  prek-i686-pc-windows-msvc.zip
  prek-aarch64-unknown-linux-gnu.tar.gz
  prek-x86_64-unknown-linux-gnu.tar.gz
  prek-aarch64-unknown-linux-musl.tar.gz
  prek-x86_64-unknown-linux-musl.tar.gz
)

for asset in "${assets[@]}"; do
  sha_url="https://github.com/j178/prek/releases/download/${latest_tag}/${asset}.sha256"
  sha_line=$(curl -fsSL "${sha_url}")
  sha=${sha_line%%[[:space:]]*}

  if [[ ! "${sha}" =~ ^[0-9a-fA-F]{64}$ ]]; then
    echo "Invalid SHA-256 returned for ${asset}: ${sha}" >&2
    exit 1
  fi

  ASSET="${asset}" SHA="${sha}" perl -0pi -e '
    BEGIN {
      $asset = quotemeta($ENV{ASSET});
      $sha = $ENV{SHA};
    }

    $count += s/("$asset",\n\s*")[0-9a-fA-F]{64}(")/$1$sha$2/g;

    END {
      exit($count == 1 ? 0 : 1);
    }
  ' Prek.DotNetTool/Assets.cs
done

if git diff --quiet -- Prek.DotNetTool/PrekTool.cs Prek.DotNetTool/Prek.DotNetTool.csproj Prek.DotNetTool/Assets.cs example/.config/dotnet-tools.json; then
  echo "Latest upstream version ${latest_version} produced no file changes."
  write_output changed false
  exit 0
fi

echo "Updated Prek.DotNetTool from prek ${current_version} to ${latest_version}."
write_output changed true
