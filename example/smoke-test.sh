#!/usr/bin/env bash
set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repo_root="$(cd -- "${script_dir}/.." && pwd)"
work_dir="$(mktemp -d)"

cleanup() {
  rm -rf "${work_dir}"
}

trap cleanup EXIT

dotnet pack "${repo_root}/Prek.DotNetTool" -c Release --nologo

dotnet tool restore --tool-manifest "${script_dir}/.config/dotnet-tools.json" --configfile "${script_dir}/NuGet.config"

cp "${script_dir}/prek.toml" "${work_dir}/prek.toml"
cp "${script_dir}/sample.txt" "${work_dir}/sample.txt"
git -C "${work_dir}" init --quiet
git -C "${work_dir}" add .

(
  cd "${script_dir}"
  dotnet tool run prek --version
  dotnet tool run prek --cd "${work_dir}" run --all-files
)

if (cd "${script_dir}" && dotnet tool run prek self update); then
  echo "Expected 'prek self update' to be blocked" >&2
  exit 1
fi

echo "Prek.DotNetTool smoke test passed"
