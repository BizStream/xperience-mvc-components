dotnet clean
dotnet build

Remove-Item .\packages\* -Recurse -Force

dotnet pack .\src\AspNetCore\Breadcrumbs\src -o .\packages
dotnet pack .\src\AspNetCore\Metadata\src -o .\packages
dotnet pack .\src\AspNetCore\OpenGraph\src -o .\packages
dotnet pack .\src\AspNetCore\Components\src -o .\packages

Get-ChildItem \NugetServer\BizStream.Kentico.Xperience.AspNetCore.Components* -Recurse | Remove-Item -Recurse -Force -Confirm:$false

nuget init .\packages \NugetServer
