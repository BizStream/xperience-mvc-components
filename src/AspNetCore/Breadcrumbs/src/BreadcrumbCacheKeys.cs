using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    /// <summary> Static Helpers for generating cache keys. </summary>
    public static class BreadcrumbCacheKeys
    {

        /// <summary> Creates a cache key for the nodes used to create breadcrumb items. </summary>
        /// <param name="node"> The node for which breadcrumb nodes are being cached. </param>
        /// <param name="options"> The options used to retrieve breadcrumb nodes. </param>
        /// <remarks> Kentico's <see cref="IPageCacheBuilder{TPageType}"/> and <see cref="IPageCacheDependencyBuilder{TPageType}"/> handle culture and site specific cache key variance. </remarks>
        public static string Nodes( TreeNode node, BreadcrumbRetrievalOptions options )
            => $"breadcrumbnodes|{node.NodeAliasPath}|{options.IsCurrentPageIncluded}|{options.IsHomePageIncluded}";

    }

}
