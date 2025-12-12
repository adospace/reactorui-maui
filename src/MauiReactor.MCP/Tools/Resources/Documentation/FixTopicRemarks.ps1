$ErrorActionPreference = 'Stop'
$docsRoot = "C:\Source\github\reactorui-maui-docs"
$topicsDir = "C:\Source\github\reactorui-maui\src\MauiReactor.MCP\Tools\Resources\Documentation\topics"

function Skip-YamlFrontMatter {
  param([string]$markdown)
  $lines = $markdown -split "`n"
  if ($lines.Length -gt 0 -and $lines[0].Trim() -eq '---') {
    # find end of front matter
    for ($i=1; $i -lt $lines.Length; $i++) {
      if ($lines[$i].Trim() -eq '---') { return ($lines[($i+1)..($lines.Length-1)] -join "`n") }
    }
  }
  return $markdown
}

function Get-FirstHeading {
  param([string]$markdown)
  foreach ($line in $markdown -split "`n") {
    if ($line -match '^\s*#\s+(.+)$') { return $Matches[1].Trim() }
  }
  return $null
}

function Get-FirstParagraph {
  param([string]$markdown)
  $m = Skip-YamlFrontMatter -markdown $markdown
  $lines = $m -split "`n"
  $buf = New-Object System.Collections.Generic.List[string]
  $inCode = $false
  foreach ($l in $lines) {
    $trim = $l.Trim()
    if ($trim.Length -eq 0) { if ($buf.Count -gt 0) { break } else { continue } }
    if ($trim.StartsWith('#')) { continue }
    if ($trim.StartsWith('```')) { $inCode = -not $inCode; if (-not $inCode -and $buf.Count -gt 0) { break }; continue }
    if ($inCode) { continue }
    # Skip hint blocks
    if ($trim.StartsWith('{%') -or $trim.StartsWith('<figure')) { continue }
    $buf.Add($trim)
  }
  return ($buf -join ' ')
}

function Get-FirstCodeBlock {
  param([string]$markdown)
  $m = Skip-YamlFrontMatter -markdown $markdown
  $lines = $m -split "`n"
  $in = $false
  $buf = New-Object System.Collections.Generic.List[string]
  foreach ($l in $lines) {
    $trim = $l.Trim()
    if ($trim.StartsWith('```')) { $in = -not $in; if (-not $in) { break }; continue }
    if ($in) { $buf.Add($l) }
  }
  if ($buf.Count -gt 0) { return ($buf -join "`n") } else { return $null }
}

$fixed = 0
Get-ChildItem -Path $topicsDir -Filter '*.json' | ForEach-Object {
  $path = $_.FullName
  $raw = Get-Content $path -Raw
  if ($raw -match '"Remarks"\s*:\s*"---') {
    # Extract Key via regex to avoid JSON parse issues
    if ($raw -match '"Key"\s*:\s*"([^"]+)"') { $key = $Matches[1] } else { return }
    $mdPath = Join-Path $docsRoot ($key + '.md')
    if (-not (Test-Path $mdPath)) { return }
    $md = Get-Content $mdPath -Raw
    $summary = (Get-FirstHeading -markdown $md)
    if (-not $summary) {
      # try to fallback to existing Summary from the file
      if ($raw -match '"Summary"\s*:\s*"([^"]+)"') { $summary = $Matches[1] } else { $summary = $key }
    }
    $remarks = (Get-FirstParagraph -markdown $md)
    $example = (Get-FirstCodeBlock -markdown $md)

    $updated = [pscustomobject]@{
      Key = $key
      Summary = $summary
      Remarks = $remarks
      Example = $example
    }
    $updated | ConvertTo-Json -Depth 5 | Set-Content -Path $path -Encoding UTF8
    $fixed++
  }
}

Write-Host "Fixed $fixed topics with front-matter remarks."