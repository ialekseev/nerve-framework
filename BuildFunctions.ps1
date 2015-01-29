Function ChangeAssemblyVersion() {
    Param(
        [Parameter(Mandatory=$true)]
        [string]$filePath,
        [Parameter(Mandatory=$true)]
        [string]$publishVersion
    )
    Write-Host "-- Updating '$filePath' to version '$publishVersion'"

    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $publishVersion + '")';
    $assemblyFileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyFileVersion = 'AssemblyFileVersion("' + $publishVersion + '")';

    (Get-Content $filePath -Encoding utf8) | ForEach-Object { 
            % { $_ -Replace $assemblyVersionPattern, $assemblyVersion } |
            % { $_ -Replace $assemblyFileVersionPattern, $assemblyFileVersion } 
    } | Set-Content $filePath
}

Function ChangeNuGetPackageVersion() {
    Param(
        [Parameter(Mandatory=$true)]
        [string]$filePath,
        [Parameter(Mandatory=$true)]
        [string]$packageId,
        [Parameter(Mandatory=$true)]
        [string]$publishVersion
    )
    [xml] $toFile = (Get-Content $filePath)
    $node = $toFile.SelectSingleNode("//packages/package[@id='" + $packageId + "']")
    if ($node) {
        Write-Host "-- Updating '$packageId' in '$filePath' to version '$publishVersion'"
        $node.version = $publishVersion
        $toFile.Save($filePath)
   }
}

Function PackNuSpec() {
    Param(
        [Parameter(Mandatory=$true)]
        [string]$nugetSpec,
        [Parameter(Mandatory=$true)]
        [string]$configuration
    )
    Write-Host "-- Packaging '$nugetSpec'"
    Invoke-Expression ".nuget\NuGet.exe pack $nugetSpec -prop Configuration=$configuration"
}