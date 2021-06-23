using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bizstream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Models;
using CMS.DocumentEngine;
using CMS.Helpers;
using Kentico.Content.Web.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure
{

    public class BreadcrumbsRetriever : IBreadcrumbsRetriever
    {
        #region Fields
        private readonly IPageRetriever pageRetriever;
        private readonly IPageUrlRetriever pageUrlRetriever;
        #endregion

        public BreadcrumbsRetriever(
            IPageRetriever pageRetriever,
            IPageUrlRetriever pageUrlRetriever
        )
        {
            this.pageRetriever = pageRetriever;
            this.pageUrlRetriever = pageUrlRetriever;
        }

        protected virtual void ApplyBreadcrumbsParameters<TQuery, TNode>( IDocumentQuery<TQuery, TNode> query, TreeNode node )
            where TQuery : IDocumentQuery<TQuery, TNode>
            where TNode : TreeNode, new()
        {
            query.Properties.WithPageUrlPaths = true;
            query.Columns(
                nameof( TreeNode.DocumentCulture ),
                nameof( TreeNode.DocumentName ),
                nameof( TreeNode.NodeLevel ),
                nameof( TreeNode.NodeSiteID ),
                nameof( PageUrlPathInfo.PageUrlPathUrlPath ),

                // for cache keys
                nameof( TreeNode.DocumentID ),
                nameof( TreeNode.NodeAliasPath ),
                nameof( TreeNode.NodeClassName )
            );

            query.Where( TreePathUtils.GetNodesOnPathWhereCondition( node.NodeAliasPath, true, false ) )
                .WhereNotEmpty( nameof( PageUrlPathInfo.PageUrlPathUrlPath ) );

            query.OrderBy( nameof( TreeNode.NodeLevel ) );
        }

        /// <summary> Create a Breadcrumb for the given <paramref name="node"/> and <paramref name="url"/>. </summary>
        protected virtual BreadcrumbItem CreateBreadcrumbItem( TreeNode node, PageUrl url )
            => new BreadcrumbItem
            {
                Label = node.DocumentName,
                Path = url.RelativePath?.TrimStart( '~' )
            };

        /// <inheritdoc/>
        public async Task<IEnumerable<BreadcrumbItem>> RetrieveAsync( TreeNode node, CancellationToken cancellationToken = default )
        {
            var nodes = await pageRetriever.RetrieveAsync<TreeNode>(
                query => ApplyBreadcrumbsParameters( query, node ),
                cache => cache.Key( CacheKeys.Nodes( node.NodeAliasPath ) )
                    .Dependencies( ( _, __ ) => { } ),
                cancellationToken: cancellationToken
            );

            return nodes.Append( node )
                .Select( node => new { node, url = pageUrlRetriever.Retrieve( node ) } )
                .OrderBy( data => data.node.NodeLevel )
                .Select( data => CreateBreadcrumbItem( data.node, data.url ) );
        }

    }

}
