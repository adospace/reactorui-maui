# Generate MauiReactor Documentation Topics

This PowerShell script parses the GitBook `SUMMARY.md` in the docs repo and produces a normalized JSON list of topics.

## Prerequisites
- Windows PowerShell 5.1 or PowerShell 7+
- Docs repository at `C:\Source\github\reactorui-maui-docs`

## Script

```powershell
$ErrorActionPreference = 'Stop'
$root = "C:\Source\github\reactorui-maui-docs"
$summary = Join-Path $root 'SUMMARY.md'
if (-not (Test-Path $summary)) {
    throw "SUMMARY.md not found at $summary"
}

$content = Get-Content $summary -Raw
$matches = [regex]::Matches($content, '\\[([^\\]]+)\\]\\(([^)]+)\\)')
$topics = foreach ($m in $matches) {
    $text = $m.Groups[1].Value.Trim()
    $link = $m.Groups[2].Value.Trim()
    if ($link -match '^(http|https)://') { continue }
    $full = Join-Path $root $link
    if (-not (Test-Path $full)) { continue }
    $rel = $link -replace '\\','/'
    $noext = [System.IO.Path]::ChangeExtension($rel, $null).TrimEnd('.')
    [pscustomobject]@{ Key = $noext; Summary = $text }
}

$destDir = "C:\Source\github\reactorui-maui\src\MauiReactor.MCP\Tools\Resources\Documentation"
$destFile = Join-Path $destDir 'topics.json'
if (-not (Test-Path $destDir)) { New-Item -ItemType Directory -Path $destDir | Out-Null }
$topics | Sort-Object Key | ConvertTo-Json -Depth 5 | Set-Content -Path $destFile -Encoding UTF8
Write-Host "Written $destFile with $($topics.Count) topics"
```

## Usage
Run from any location:

```powershell
pwsh -File .\Tools\Resources\Documentation\GenerateTopics.ps1
```

Or paste into your terminal as needed.
```