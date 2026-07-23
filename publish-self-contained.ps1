$ErrorActionPreference = "Stop"
$project = Join-Path $PSScriptRoot "JeremyMediaToolkit\JeremyMediaToolkit.csproj"
$publish = Join-Path $PSScriptRoot "publish\JeremyMediaToolkit-win-x64"

Write-Host "Publishing standalone Windows application..." -ForegroundColor Cyan
dotnet publish $project -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true `
  -p:DebugType=None -p:DebugSymbols=false -o $publish
if ($LASTEXITCODE -ne 0) { throw "Publish failed." }

$helperSource = Join-Path $PSScriptRoot "PremiereHelper"
$helperTarget = Join-Path $publish "PremiereHelper"
Copy-Item -LiteralPath $helperSource -Destination $helperTarget -Recurse -Force
Write-Host "Published to: $publish" -ForegroundColor Green
Read-Host "Press Enter to close"
