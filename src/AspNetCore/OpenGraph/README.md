# Xperience Open Graph Component [![NuGet Version](https://img.shields.io/nuget/v/BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph)](https://nuget.org/packages/bizstream.kentico.xperience.aspnetcore.components.opengraph)

This package provides a Mvc `ViewComponent` implementation for rendering common OpenGraph `meta` elements from content within Xperience.

## Usage

- Install the package into your Xperience Mvc project:

```bash
dotnet add package BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph
```

OR

```csproj
<PackageReference Include="BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph" Version="x.x.x" />
```

- Configure services in `Startup.cs`:

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph;
using Kentico.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceOpenGraphComponent();
}
```

- Invoke the component within the `<head/>` of a Razor View:

```razor
<head>
    @* ... *@

    @await Component.InvokeAsync( nameof( XperienceOpenGraph ) )

    @* OR *@

    @( await Component.InvokeAsync<XperienceOpenGraph>( )

    @* ... *@
</head>
```

### Extensibility

#### Customize Source `TreeNode` Field Names

The default [`IOpenGraphDataRetriever`](src/Abstractions/IOpenGraphDataRetriever.cs) implementation, [`OpenGraphDataRetriever`](src/Infrastructure/OpenGraphDataRetriever.cs), uses the configured [`OpenGraphDataRetrievalOptions.FieldNames`](src/Abstractions/OpenGraphData.cs#L13) instance to determine the source fields of a `TreeNode` when retrieving an [`OpenGraphData`](src/Abstractions/OpenGraphData.cs) instance.

The default source field names are represented by the [`OpenGraphPageFields`](src/Abstractions/OpenGraphPageFields.cs) type, and can be configured using the [_Options Pattern_](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options):

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph;
using Kentico.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceOpenGraphComponent();
    services.PostConfigure<OpenGraphDataRetrievalOptions>(
        options =>
        {
            // Use the `DocumentPageTitle` and `DocumentPageDescription` fields provided by Xperience.
            options.UseMetadataFields();

            options.FieldsNames.Image = "CustomOpenGraphImage";
            options.FieldsNames.Video = "CustomOpenGraphVideo";
        }
    )
}
```

#### Customize `OpenGraphData` Retrieval

The default [`IOpenGraphDataRetriever`](src/Abstractions/IOpenGraphDataRetriever.cs) implementation, [`OpenGraphDataRetriever`](src/Infrastructure/OpenGraphDataRetriever.cs) provides various `virtual` methods that allow customization of `OpenGraphData` retrieval and mapping. This can be done by inheriting from [`OpenGraphDataRetriever`](src/Infrastructure/OpenGraphDataRetriever.cs), and overriding the registration in the `IServiceCollection`:

`CustomOpenGraphDataRetriever.cs`

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using CMS.Base;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.Extensions.Options;

public class CustomOpenGraphDataRetriever : OpenGraphDataRetriever
{

    public CustomOpenGraphDataRetriever(
        IOptions<OpenGraphDataRetrievalOptions> defaultOptions,
        ISiteService siteService
    ) : base( defaultOptions, siteService )
    {
    }

    protected override async Task<OpenGraphData> RetrieveAsync( TreeNode page, Action<OpenGraphDataRetrievalOptions> configure = null, CancellationToken cancellation = default )
    {
        var data = await base.RetrieveAsync( page, configure, cancellation );

        // Do something with it...

        return data;
    }

}
```

`Startup.cs`

```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph;
using Kentico.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceOpenGraphComponent();

    // replace ServiceDescriptor
    services.Replace(
        new ServiceDescriptor( typeof( IOpenGraphDataRetriever ), typeof( CustomOpenGraphDataRetriever ), ServiceLifetime.Transient )
    );
}
```

## FAQ

- _Will this `ViewComponent` work if I'm not using Page Routing?_

no.
