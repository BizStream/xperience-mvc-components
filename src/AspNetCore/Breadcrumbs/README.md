# Xperience Breadcrumbs Component [![NuGet Version](https://img.shields.io/nuget/v/BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs)](https://nuget.org/packages/bizstream.kentico.xperience.aspnetcore.components.breadcrumbs)

This package provides a Mvc `ViewComponent` implementation, and accompanying services, for rendering Breadcrumbs based on [Content Tree Routing](https://docs.xperience.io/developing-websites/implementing-routing/content-tree-based-routing).

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
using Kentico.Web.Mvc;
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
@using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs

@* ... *@

@await Component.InvokeAsync( nameof( XperienceBreadcrumbs ) )

@* OR *@

@( await Component.InvokeAsync<XperienceBreadcrumbs>( ) )

@* ... *@
```

### Styling

This package **does not** provide any styling for breadcrumbs, consumers are required to implement their own styles. The `Default.cshtml` rendered DOM is intended to provide a semantic and extensible structure for easy styling:

```
div.breadcrumbs-container
    nav.breadcrumbs
        ol
            li.breadcrumb-item
            ...
```

### Extensibility

#### Override Views

As described in the _Razor Class Library_ [documentation](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class#override-views-partial-views-and-pages-2), Views created in the host Web Project take precedence over that of Views referenced in a Razor Class Library. This package provides the following Views that can be overridden to customize the rendering of breadcrumbs:

- `Views\Shared\Components\XperienceBreadcrumbs\_BreadcrumbItem.cshtml`

Override this View to customize how individual `BreadcrumbItem`s are rendered within the `Default` view.

- `Views\Shared\Components\XperienceBreadcrumbs\Default.cshtml`

Override this View to completely override/customize how breadcrumbs are rendered.

#### Customize `BreadcrumbItem` Retrieval

The `BreadcrumbsRetriever`, the default `IBreadcrumbsRetriever` implementation, provides various `virtual` methods that allow customization of `BreadcrumbItem` retrieval and mapping. This can be done by inheriting from `BreadcrumbsRetriever`, and overriding the registration in the `IServiceCollection`:

`CustomBreadcrumbsRetriever.cs`
```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using CMS.Base;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.Extensions.Options;

public class CustomBreadcrumbsRetriever : BreadcrumbsRetriever
{

    public CustomBreadcrumbsRetriever(
        IOptions<BreadcrumbRetrievalOptions> defaultOptions,
        IPageRetriever pageRetriever,
        IPageUrlRetriever pageUrlRetriever,
        ISiteService siteService
    ) : base( defaultOptions, pageRetriever, pageUrlRetriever, siteService )
    {
    }

    protected override BreadcrumbItem CreateBreadcrumbItem( TreeNode page )
    {
        var breadcrumb = base.CreateBreadcrumbItem( page );

        // Use a custom field for breadcrumb label
        breadcrumb.Label = node.GetStringValue(
            "CustomBreadcrumbLabel",
            breadcrumb.Label
        );

        return breadcrumb;
    }

}
```

`Startup.cs`
```csharp
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs;
using Kentico.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

// ...

public void ConfigureServices( IServiceCollection services )
{
    services.AddMvc();
    services.AddKentico();

    // ...

    services.AddXperienceBreadcrumbsComponent();

    // replace ServiceDescriptor
    services.Replace(
        new ServiceDescriptor( typeof( IBreadcrumbsRetriever ), typeof( CustomBreadcrumbsRetriever ), ServiceLifetime.Transient )
    );
}
```

## FAQ

- _Will this `ViewComponent` work if I'm not using Page Routing?_

no.
