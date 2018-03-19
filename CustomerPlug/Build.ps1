Set-Variable -Name "version" -Value "1.0.1"

nuget pack -Version $version

squirrel.exe --releasify MyApp.$version.nupkg

nuget.exe push MyApp.$version.nupkg dsfdsfsdfsdfsdfsdfsd -Source http://localhost:9000/nuget
 