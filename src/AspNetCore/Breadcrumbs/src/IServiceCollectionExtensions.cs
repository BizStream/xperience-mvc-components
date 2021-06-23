using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddXperienceBreadcrumbsComponent( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddTransient<IBreadcrumbsRetriever, BreadcrumbsRetriever>();
            return services;
        }

    }

}
