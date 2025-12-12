$ErrorActionPreference = 'Stop'
$root = "C:\Source\github\reactorui-maui-docs"
$summary = Join-Path $root 'SUMMARY.md'
if (-not (Test-Path $summary)) {
    throw "SUMMARY.md not found at $summary"
}

$content = Get-Content $summary -Raw
$matches = [regex]::Matches($content, '\[([^\]]+)\]\(([^)]+)\)')
$topics = foreach ($m in $matches) {
    $text = $m.Groups[1].Value.Trim()
    $link = $m.Groups[2].Value.Trim()
    if ($link -match '^(http|https)://') { continue }
    $full = Join-Path $root ($link -replace '<','' -replace '>' ,'')
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