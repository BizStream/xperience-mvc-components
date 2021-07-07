# Xperience Breadcrumbs [![NuGet Version](https://img.shields.io/nuget/v/BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs)](https://nuget.org/packages/bizstream.kentico.xperience.aspnetcore.breadcrumbs)

This package provides a Mvc `ViewComponent` implementation, and accompanying services, that allow breadcrumbs to be rendered based on [Content Tree Routing](https://docs.xperience.io/developing-websites/implementing-routing/content-tree-based-routing).

## Usage

- Install the package into your Xperience Mvc project:

```bash
dotnet add package BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
```

OR

```csproj
<PackageReference Include="BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs" Version="x.x.x" />
```

- Configure services in `Startup.cs`:

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceBreadcrumbsComponent();
}
```

- Invoke the component within a Razor View:

```razor
@* ... *@

@await Component.InvokeAsync( nameof( Breadcrumbs ) )

@* ... *@
```

## FAQ

- _Will this `ViewComponent` work if I'm not using Page Builder?_

no.
