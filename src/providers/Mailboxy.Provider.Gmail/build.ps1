$projectPath = Join-Path $PSScriptRoot "Mailboxy.Provider.Gmail.csproj"
$srcPath = (Get-Item $PSScriptRoot).Parent.Parent.FullName
$cliOutputPath = Join-Path $srcPath "cli\bin\Debug\net5.0"
$providerOutputPath = Join-Path $cliOutputPath "providers\Gmail"

dotnet publish -o $providerOutputPath $projectPath