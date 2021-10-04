using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure
{

    /// <summary> Extensions to <see cref="OpenGraphDataRetrievalOptions"/>s for fluent configuration. </summary>
    public static class OpenGraphDataRetrievalOptionsExtensions
    {

        /// <summary> Configures the <see cref="OpenGraphDataRetrievalOptions.FieldNames"/> to use the fields used for <see cref="IPageMetadata"/>. </summary>
        /// <param name="options"> The <see cref="OpenGraphDataRetrievalOptions"/> to configure. </param>
        public static OpenGraphDataRetrievalOptions UseMetadataFields( this OpenGraphDataRetrievalOptions options )
        {
            if( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }

            options.FieldNames ??= new();

            options.FieldNames.Description = nameof( TreeNode.DocumentPageDescription );
            options.FieldNames.Title = nameof( TreeNode.DocumentPageTitle );

            return options;
        }

    }

}
