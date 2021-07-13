using System;
using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph
{

    /// <summary> Extensions to <see cref="IServiceCollection"/>. </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary> Registers services required by Xperience OpenGraph Component. </summary>
        public static IServiceCollection AddXperienceOpenGraphComponent( this IServiceCollection services )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            services.AddOptions<OpenGraphDataRetrievalOptions>();
            services.AddTransient<IOpenGraphDataRetriever, OpenGraphDataRetriever>();

            return services;
        }

    }

}
