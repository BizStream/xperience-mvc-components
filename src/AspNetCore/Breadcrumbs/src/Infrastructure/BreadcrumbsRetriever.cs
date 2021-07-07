using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.Base;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing;
using Kentico.Content.Web.Mvc;
using Microsoft.Extensions.Options;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure
{

    /// <summary> The default implementation of a service that can retrieve breadcrumbs. </summary>
    public class BreadcrumbsRetriever : IBreadcrumbsRetriever
    {
        #region Fields
        private readonly IOptions<BreadcrumbRetrievalOptions> defaultOptions;
        private readonly IPageRetriever pageRetriever;
        private readonly IPageUrlRetriever pageUrlRetriever;
        private readonly ISiteService siteService;
        #endregion

        public BreadcrumbsRetriever(
            IOptions<BreadcrumbRetrievalOptions> defaultOptions,
            IPageRetriever pageRetriever,
            IPageUrlRetriever pageUrlRetriever,
            ISiteService siteService
        )
        {
            this.defaultOptions = defaultOptions;
            this.pageRetriever = pageRetriever;
            this.pageUrlRetriever = pageUrlRetriever;
            this.siteService = siteService;
        }

        /// <summary> Configure the provided <paramref name="query"/> to retrieve breadcrumb nodes for the given <paramref name="node"/>, using the given <paramref name="options"/>. </summary>
        /// <typeparam name="TQuery"> The type of <see cref="IDocumentQuery{TQuery, TObject}"/> to configure. </typeparam>
        /// <param name="query"> The query to configure to retrieve the breadcrumb nodes of the given <paramref name="node"/>. </param>
        /// <param name="node"> The node for which breadcrumb nodes are to be retrieved. </param>
        /// <param name="options"> Options used to alter configuration of the given <paramref name="query"/>. </param>
        protected virtual void ApplyBreadcrumbsParameters<TQuery>( IDocumentQuery<TQuery, TreeNode> query, TreeNode node, BreadcrumbRetrievalOptions options )
            where TQuery : IDocumentQuery<TQuery, TreeNode>
        {
            var orderColumn = new QueryColumn()
            {
                ColumnAlias = "_BreadcrumbOrder",
                Expression = nameof( TreeNode.NodeLevel )
            };

            query.Columns(
                nameof( TreeNode.DocumentCulture ),
                nameof( TreeNode.DocumentName ),
                nameof( TreeNode.NodeLevel ),
                nameof( PageUrlPathInfo.PageUrlPathUrlPath ),

                // for cache keys
                nameof( TreeNode.ClassName ),
                nameof( TreeNode.DocumentID ),
                nameof( TreeNode.NodeAliasPath ),
                nameof( TreeNode.NodeSiteID )
            );

            query.WithPageUrlPaths()
                .Where( TreePathUtils.GetNodesOnPathWhereCondition( node.NodeAliasPath, false, options.IsCurrentPageIncluded ) )
                .WhereNotEmpty( nameof( PageUrlPathInfo.PageUrlPathUrlPath ) );

            if( options.IsHomePageIncluded )
            {
                var homeAliasPath = PageRoutingHelper.GetHomePagePath( siteService.CurrentSite?.SiteName );
                if( !string.IsNullOrEmpty( homeAliasPath ) )
                {
                    // Ensure the home page is sorted to the beginning of the result set.
                    orderColumn.Expression = $"CASE WHEN [{nameof( TreeNode.NodeAliasPath )}] = '{homeAliasPath}' THEN -1 ELSE [{nameof( TreeNode.NodeLevel )}] END";

                    query.Or(
                        condition => condition.WhereEquals( nameof( TreeNode.NodeAliasPath ), homeAliasPath )
                    );
                }
            }

            query.AddColumn( orderColumn );
            query.OrderByColumns = orderColumn.Name;
        }

        /// <summary> Create a Breadcrumb for the given <paramref name="node"/>. </summary>
        protected virtual BreadcrumbItem CreateBreadcrumbItem( TreeNode node )
            => new()
            {
                Label = node.DocumentName,
                Path = pageUrlRetriever.Retrieve( node )
                    ?.RelativePath
                    ?.TrimStart( '~' )
            };

        /// <inheritdoc/>
        public async Task<IEnumerable<BreadcrumbItem>> RetrieveAsync( TreeNode page, Action<BreadcrumbRetrievalOptions> configure = default, CancellationToken cancellation = default )
        {
            var builder = new BreadcrumbRetrievalOptionsBuilder( defaultOptions.Value );
            configure?.Invoke( builder.Options );

            var options = builder.Build();
            var nodes = await pageRetriever.RetrieveAsync<TreeNode>(
                query => ApplyBreadcrumbsParameters( query, page, options ),
                cache => cache.Key( CacheKeys.Nodes( page, options ) )
                    .Dependencies( ( _, __ ) => { } ),
                cancellationToken: cancellation
            );

            return nodes.Select( CreateBreadcrumbItem );
        }

    }

}
