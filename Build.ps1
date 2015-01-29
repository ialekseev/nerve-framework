Param(
    [Parameter(Mandatory=$true)]
    [string]$version,
    [string]$configuration = "Release",
    [string]$targetFrameworkVersion = "v4.5",
    [boolean]$tests = $false,
    [boolean]$publish = $false,
    [boolean]$nugetPack = $false
)

# Include functions
. "./BuildFunctions.ps1"

# The solution we are building
$solution = "NerveFramework.sln"

# Set the path to the .NET folder in order to use "msbuild.exe"
$env:PATH = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319"

# Start by changing the assembly version
Write-Host "Changing the assembly versions to '$version'"
$assemblyInfoFiles = Get-ChildItem -Filter "AssemblyInfo.cs" -Recurse | Resolve-Path -Relative
foreach ($assemblyInfo in $assemblyInfoFiles) {
    ChangeAssemblyVersion $assemblyInfo $version
}

# Build the entire solution
Write-Host "Cleaning and building $solution (Configuration: $configuration)"
msbuild.exe $solution /nologo /v:q /p:Configuration=$configuration
msbuild.exe $solution /nologo /v:q /p:Configuration=$configuration /clp:ErrorsOnly

# Increase the dependency on NerveFramework on all depending assemblies
Write-Host "Changing the NerveFramework NuGet packages.config version dependencies to '$version'"
$dependentAssemblies = Get-ChildItem -Filter "packages.config" -Recurse | Resolve-Path -Relative
foreach ($dependentAssembly in $dependentAssemblies) {
    ChangeNuGetPackageVersion $dependentAssembly "NerveFramework" $version
} 

# NuGet Pack the assemblies
if ($nugetPack) {
    Write-Host "Packaging NuGet Specs"
    $nugetSpecs = Get-ChildItem -Filter "NerveFramework*.nuspec" -Recurse | Resolve-Path -Relative
    foreach ($nugetSpec in $nugetSpecs) {
        PackNuSpec $nugetSpec $configuration
    } 
}

# TODO: Publish NuGet