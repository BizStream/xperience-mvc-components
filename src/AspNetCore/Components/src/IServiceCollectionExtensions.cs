using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddXperienceComponents( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddXperienceMetadataComponent();

            return services;
        }

    }

}
