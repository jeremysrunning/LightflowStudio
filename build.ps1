$ErrorActionPreference = "Stop"
$project = Join-Path $PSScriptRoot "JeremyMediaToolkit\JeremyMediaToolkit.csproj"
Write-Host "Building Jeremy Media Toolkit..." -ForegroundColor Cyan
dotnet build $project -c Release
if ($LASTEXITCODE -ne 0) { throw "Build failed." }
Write-Host "Build complete." -ForegroundColor Green
Read-Host "Press Enter to close"
