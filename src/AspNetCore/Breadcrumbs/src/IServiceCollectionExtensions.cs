using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Filters;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    /// <summary> Extensions to <see cref="IServiceCollection"/>. </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary> Registers services required by Xperience Breadcrumbs Component. </summary>
        public static IServiceCollection AddXperienceBreadcrumbsComponent( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddOptions<BreadcrumbRetrievalOptions>();
            services.AddTransient<IBreadcrumbsRetriever, BreadcrumbsRetriever>();

            services.AddScoped<IBreadcrumbsFilter, ExcludeLastBreadcrumbPathFilter>();

            return services;
        }

    }

}
