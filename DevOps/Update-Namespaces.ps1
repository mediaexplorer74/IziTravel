# PowerShell script to update namespaces from Izi.Travel.Shell to Izi.Travel in all .cs and .xaml files

# Define the root directory to search in
$rootDir = "$PSScriptRoot"

# Define the search pattern for files to process
$filePatterns = @("*.cs", "*.xaml")

# Define the namespace mapping
$namespaceMappings = @{
    "Izi\.Travel\.Shell" = "Izi.Travel"
}

# Counter for tracking changes
$filesUpdated = 0

# Function to process a single file
function Update-FileContent {
    param (
        [string]$filePath
    )
    
    $content = [System.IO.File]::ReadAllText($filePath)
    $originalContent = $content
    
    # Apply each namespace mapping
    foreach ($mapping in $namespaceMappings.GetEnumerator()) {
        $content = $content -replace $mapping.Key, $mapping.Value
    }
    
    # Only write to the file if changes were made
    if ($content -ne $originalContent) {
        [System.IO.File]::WriteAllText($filePath, $content, [System.Text.Encoding]::UTF8)
        Write-Host "Updated: $filePath" -ForegroundColor Green
        $script:filesUpdated++
    }
}

# Process all matching files
Write-Host "Starting namespace update process..." -ForegroundColor Cyan

foreach ($pattern in $filePatterns) {
    Get-ChildItem -Path $rootDir -Filter $pattern -Recurse -File | ForEach-Object {
        Update-FileContent -filePath $_.FullName
    }
}

Write-Host "`nProcess complete! Updated $filesUpdated files." -ForegroundColor Cyan
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
