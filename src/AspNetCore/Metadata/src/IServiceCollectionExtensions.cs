using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddXperienceMetadataComponent<TProfile>( this IServiceCollection services )
            where TProfile : Profile, new()
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddAutoMapper( map => map.AddProfile<TProfile>() );
            return services;
        }

        public static IServiceCollection AddXperienceMetadataComponent( this IServiceCollection services )
            => AddXperienceMetadataComponent<MetadataMappingProfile>( services );

    }

}
