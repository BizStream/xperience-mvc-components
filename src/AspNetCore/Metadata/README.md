# Xperience Metadata Component [![NuGet Version](https://img.shields.io/nuget/v/BizStream.Kentico.Xperience.AspNetCore.Components.Metadata)](https://nuget.org/packages/bizstream.kentico.xperience.aspnetcore.components.Metadata)

This package provides a Mvc `ViewComponent` implementation for rendering common `meta` elements from content within Xperience.

## Usage

- Install the package into your Xperience Mvc project:

```bash
dotnet add package BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
```

OR

```csproj
<PackageReference Include="BizStream.Kentico.Xperience.AspNetCore.Components.Metadata" Version="x.x.x" />
```

- Configure services in `Startup.cs`:

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.Metadata;
using Kentico.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceMetadataComponent();
}
```

- Invoke the component within the `<head/>` of a Razor View:

```razor
<head>
    @* ... *@

    @await Component.InvokeAsync( nameof( Metadata ) )

    @* ... *@
</head>
```

## FAQ

- _Will this `ViewComponent` work if I'm not using Page Routing?_

no.
