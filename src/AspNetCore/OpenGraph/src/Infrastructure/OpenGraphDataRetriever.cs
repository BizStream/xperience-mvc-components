using System;
using System.Threading;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using CMS.Base;
using CMS.DocumentEngine;
using CMS.SiteProvider;
using Microsoft.Extensions.Options;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure
{

    /// <summary> The default implementation of a service that can retrieve a page's OpenGraph data. </summary>
    public class OpenGraphDataRetriever : IOpenGraphDataRetriever
    {
        #region Fields
        private readonly IOptions<OpenGraphDataRetrievalOptions> defaultOptions;
        private readonly ISiteService siteService;
        #endregion

        public OpenGraphDataRetriever(
            IOptions<OpenGraphDataRetrievalOptions> defaultOptions,
            ISiteService siteService
        )
        {
            this.defaultOptions = defaultOptions;
            this.siteService = siteService;
        }

        /// <summary> Create an absolute <see cref="Uri"/> for the given <paramref name="mediaPath"/>. </summary>
        /// <remarks> The returned <see cref="Uri"/> must be absolute to meet OpenGraph requirements. </remarks>
        protected virtual Uri CreateMediaUri( string mediaPath )
        {
            mediaPath = mediaPath?.TrimStart( '~' );
            if( string.IsNullOrEmpty( mediaPath ) )
            {
                return null;
            }

            var presentationUrl = siteService?.CurrentSite
                ?.GetValue( nameof( SiteInfo.SitePresentationURL ) ) as string;

            return new( presentationUrl?.TrimEnd( '/' ) + mediaPath, UriKind.Absolute );
        }

        /// <summary> Create an <see cref="OpenGraphData"/> instance for the given <paramref name="page"/>, using the given <paramref name="fields"/>. </summary>
        /// <see href="https://ogp.me/#metadata"> The Open Graph protocol: Basic Metadata. </see>
        protected virtual OpenGraphData CreateOpenGraphData( TreeNode page, OpenGraphPageFields fields )
            => new()
            {
                Description = page.GetStringValue( fields.Description, string.Empty ),
                ImageUrl = CreateMediaUri( page.GetStringValue( fields.Image, string.Empty ) ),
                Title = page.GetStringValue( fields.Title, string.Empty ),
                VideoUrl = CreateMediaUri( page.GetStringValue( fields.Video, string.Empty ) )
            };

        /// <inheritdoc/>
        public virtual Task<OpenGraphData> RetrieveAsync( TreeNode page, Action<OpenGraphDataRetrievalOptions> configure = null, CancellationToken cancellation = default )
        {
            var builder = new OpenGraphDataRetrievalOptionsBuilder( defaultOptions.Value );
            configure?.Invoke( builder.Options );

            var options = builder.Build();
            var data = CreateOpenGraphData( page, options.FieldNames );

            return new ValueTask<OpenGraphData>( data )
                .AsTask();
        }

    }

}
