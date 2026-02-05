$ErrorActionPreference = 'Stop'
$docsRoot = "C:\Source\github\reactorui-maui-docs"
$topicsDir = "C:\Source\github\reactorui-maui\src\MauiReactor.MCP\Tools\Resources\Documentation\topics"
if (-not (Test-Path $topicsDir)) { New-Item -ItemType Directory -Path $topicsDir | Out-Null }

function Get-FirstHeading {
  param([string]$markdown)
  foreach ($line in $markdown -split "`n") {
    if ($line -match '^\s*#\s+(.+)$') { return $Matches[1].Trim() }
  }
  return $null
}

function Get-FirstParagraph {
  param([string]$markdown)
  $lines = $markdown -split "`n"
  $buf = New-Object System.Collections.Generic.List[string]
  foreach ($l in $lines) {
    if ($l.Trim().Length -eq 0) {
      if ($buf.Count -gt 0) { break } else { continue }
    }
    if ($l.Trim().StartsWith('#')) { continue }
    if ($l.Trim().StartsWith('```')) { break }
    $buf.Add($l.Trim())
  }
  return ($buf -join ' ')
}

function Get-FirstCodeBlock {
  param([string]$markdown)
  $lines = $markdown -split "`n"
  $in = $false
  $buf = New-Object System.Collections.Generic.List[string]
  foreach ($l in $lines) {
    if ($l.Trim().StartsWith('```')) { $in = -not $in; if (-not $in) { break }; continue }
    if ($in) { $buf.Add($l) }
  }
  if ($buf.Count -gt 0) { return ($buf -join "`n") } else { return $null }
}

# Load topics from SUMMARY.md
$summaryPath = Join-Path $docsRoot 'SUMMARY.md'
if (-not (Test-Path $summaryPath)) { throw "SUMMARY.md not found: $summaryPath" }
$content = Get-Content $summaryPath -Raw
$matches = [regex]::Matches($content, '\[([^\]]+)\]\(([^)]+)\)')
$topics = @()
foreach ($m in $matches) {
  $text = $m.Groups[1].Value.Trim()
  $link = $m.Groups[2].Value.Trim()
  if ($link -match '^(http|https)://') { continue }
  $cleanLink = $link -replace '<','' -replace '>' ,''
  $full = Join-Path $docsRoot $cleanLink
  if (-not (Test-Path $full)) { continue }
  $rel = $cleanLink -replace '\\','/'
  $key = [System.IO.Path]::ChangeExtension($rel, $null).TrimEnd('.')
  $topics += [pscustomobject]@{ Key = $key; Title = $text; Path = $full }
}

# Generate JSON per topic
$generated = 0
foreach ($t in $topics) {
  $md = Get-Content $t.Path -Raw
  $summary = Get-FirstHeading -markdown $md
  if (-not $summary) { $summary = $t.Title }
  $remarks = Get-FirstParagraph -markdown $md
  $example = Get-FirstCodeBlock -markdown $md

  $obj = [pscustomobject]@{
    Key = $t.Key
    Summary = $summary
    Remarks = $remarks
    Example = $example
  }
  $outPath = Join-Path $topicsDir ((($t.Key -replace '/','-') ) + '.json')
  $obj | ConvertTo-Json -Depth 5 | Set-Content -Path $outPath -Encoding UTF8
  $generated++
}

Write-Host "Generated $generated topic JSON files in $topicsDir"