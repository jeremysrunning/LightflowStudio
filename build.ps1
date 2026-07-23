$ErrorActionPreference = "Stop"
$project = Join-Path $PSScriptRoot "LightflowStudio\LightflowStudio.csproj"
Write-Host "Building Lightflow Studio..." -ForegroundColor Cyan
dotnet build $project -c Release
if ($LASTEXITCODE -ne 0) { throw "Build failed." }
Write-Host "Build complete." -ForegroundColor Green
Read-Host "Press Enter to close"
