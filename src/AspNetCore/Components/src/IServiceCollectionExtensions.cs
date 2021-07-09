using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs;
using BizStream.Kentico.Xperience.AspNetCore.Components.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components
{

    /// <summary> Extensions to <see cref="IServiceCollection"/> for registering services required by Xperience Mvc Components. </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary> Registers services required by Xperience Components. </summary>
        public static IServiceCollection AddXperienceComponents( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddXperienceBreadcrumbsComponent();
            services.AddXperienceMetadataComponent();

            return services;
        }

    }

}
