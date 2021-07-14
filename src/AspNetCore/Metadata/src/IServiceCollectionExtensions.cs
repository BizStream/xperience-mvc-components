using System;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    /// <summary> Extensions to <see cref="IServiceCollection"/>. </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary> Registers services required by Xperience Metadata Component. </summary>
        public static IServiceCollection AddXperienceMetadataComponent( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            return services;
        }

    }

}
