# Update documentation JSON files from matching markdown in reactorui-maui-docs
param(
    [string]$DocsRoot = 'C:\Source\github\reactorui-maui-docs'
)

Write-Host "Docs root:" $DocsRoot

function Get-MarkdownInfo {
    param(
        [string]$MarkdownPath
    )

    $content = Get-Content -Raw -ErrorAction Stop -Path $MarkdownPath

    $info = [ordered]@{
        Title = $null
        Description = $null
        FirstCodeBlock = $null
        Remarks = $null
    }

    # Extract front matter description if present
    if ($content -match "(?s)^---\s*(.*?)\s*---") {
        $frontMatter = $matches[1]
        if ($frontMatter -match "(?m)^description:\s*(.+)$") {
            $info.Description = ($matches[1]).Trim()
        }
    }

    # Remove front matter for further parsing
    $body = $content -replace "(?s)^---\s*(.*?)\s*---\s*", ''

    # Title: first H1 heading
    if ($body -match "(?m)^#\s+(.+)$") {
        $info.Title = ($matches[1]).Trim()
    }

    # First fenced code block
    if ($body -match "(?s)```[a-zA-Z0-9]*\s*(.*?)\s*```") {
        $info.FirstCodeBlock = ($matches[1]).TrimEnd()
    }

    # Remarks: collect lines after the title, stopping at first code fence or subheading
    $lines = $body -split [System.Environment]::NewLine
    $startIdx = 0
    if ($info.Title) {
        for ($i = 0; $i -lt $lines.Length; $i++) {
            if ($lines[$i] -match '^#\s+') { $startIdx = $i + 1; break }
        }
    }
    $remarksCollected = New-Object System.Collections.Generic.List[string]
    $inCode = $false
    for ($i = $startIdx; $i -lt $lines.Length; $i++) {
        $line = $lines[$i]
        if ($line -match '^```') { break }
        if ($line -match '^##+\s+') { break }
        if ($line.Trim().Length -gt 0) { $remarksCollected.Add($line.Trim()) }
    }
    $info.Remarks = ($remarksCollected -join " ").Trim()

    return $info
}

$docsDir = Join-Path $PSScriptRoot '.'
$jsonFiles = Get-ChildItem -Path $docsDir -Filter '*.json' -Recurse

foreach ($jsonFile in $jsonFiles) {
    try {
        $jsonText = Get-Content -Raw -Path $jsonFile.FullName
        $obj = $jsonText | ConvertFrom-Json

        if (-not $obj.Key) {
            Write-Warning "Skipping (no Key): $($jsonFile.Name)"
            continue
        }

        $mdRelative = ($obj.Key -replace '/', '\') + '.md'
        $mdPath = Join-Path $DocsRoot $mdRelative

        if (-not (Test-Path -Path $mdPath)) {
            Write-Warning "Markdown not found: $mdPath for key $($obj.Key)"
            continue
        }

        $mdInfo = Get-MarkdownInfo -MarkdownPath $mdPath

        # Build improved fields
        $summary = $obj.Summary
        if ($mdInfo.Title) {
            $summary = "$($mdInfo.Title)"
        } elseif ($mdInfo.Description) {
            $summary = $mdInfo.Description
        }

        $remarks = $obj.Remarks
        $remarksParts = @()
        if ($mdInfo.Description) { $remarksParts += $mdInfo.Description }
        if ($mdInfo.Remarks) { $remarksParts += $mdInfo.Remarks }
        if ($remarksParts.Count -gt 0) { $remarks = ($remarksParts -join " ") }

        $example = $obj.Example
        if ($mdInfo.FirstCodeBlock) {
            # Normalize line endings to platform newline for consistency
            $code = ($mdInfo.FirstCodeBlock -split [System.Environment]::NewLine) -join [System.Environment]::NewLine
            $example = $code
        }

        $obj.Summary = $summary
        $obj.Remarks = $remarks
        $obj.Example = $example

        # Write back preserving indentation
        $newJson = $obj | ConvertTo-Json -Depth 10
        # Normalize line endings to platform newline
        $newJson = ($newJson -split [System.Environment]::NewLine) -join [System.Environment]::NewLine
        Set-Content -Path $jsonFile.FullName -Value ($newJson + [System.Environment]::NewLine)
        Write-Host "Updated" $jsonFile.FullName
    }
    catch {
        Write-Warning "Failed processing $($jsonFile.FullName): $($_.Exception.Message)"
    }
}

Write-Host "Done."
