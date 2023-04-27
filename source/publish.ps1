$ErrorActionPreference = 'Stop'
$gameName = "Hybrid.Game"

$platforms = @('Windows', 'Linux', 'MacOS')
$rids = @{
    'Windows' = 'win-x64';
    'Linux' = 'linux-x64';
    'MacOS' = 'osx-x64';
}

$publishDir = [IO.Path]::Combine($pwd, $gameName, "publish")

# Ah, PowerShell, Ah.
# Seems everything is relative to the solution root, not the cwd (source)
$zipsPath = [IO.Path]::Combine("..", "*.zip")
remove-item $zipsPath

foreach ($platform in $platforms)
{
    $zipFile = "$gameName-$platform.zip"

    if (Test-Path($publishDir))
    {
        Remove-Item $publishDir -Recurse
    }

    # Source: https://stackoverflow.com/questions/44074121/build-net-core-console-application-to-output-an-exe
    # Publish to an exe + dependencies. 40MB baseline.
    dotnet publish -c Release -r $rids[$platform] -o $publishDir

    $command = ("chmod a+x " + [IO.Path]::Combine($publishDir, $gameName))
    if ($platform -eq 'Windows')
    {
        $command += ".exe"
    }
    #Invoke-Expression $command

    # Copy all content over since we're not using the MonoGame content pipeline
    foreach ($folder in @('assets'))
    {
        $sourceDir = [IO.Path]::Combine($gameName, $folder)
        $destDir = [IO.Path]::Combine($publishDir, $folder)
        Copy-Item -Recurse  $sourceDir $destDir
    }

    # Zip it up.
    if (Test-Path($zipFile))
    {
        Remove-Item $zipFile -Force
    }

    chmod -R 755 $publishDir

    Add-Type -A 'System.IO.Compression.FileSystem'
    [IO.Compression.ZipFile]::CreateFromDirectory($publishDir, $zipFile);
    Write-Host DONE! Zipped to $zipFile
}