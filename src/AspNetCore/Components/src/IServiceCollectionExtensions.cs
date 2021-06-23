using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs;
using Microsoft.Extensions.DependencyInjection;

namespace Bizstream.Kentico.Xperience.AspNetCore.Components
{

    /// <summary> Extensions to <see cref="IServiceCollection"/> for registering services required by Xperience Mvc Components. </summary>
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddXperienceComponents( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddXperienceBreadcrumbsComponent();

            return services;
        }

    }

}
