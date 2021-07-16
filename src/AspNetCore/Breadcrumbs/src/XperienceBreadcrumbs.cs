using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    /// <summary> A <see cref="ViewComponent"/> that renders breadcrumbs based on Xperience Content Tree Routing.  </summary>
    public class XperienceBreadcrumbs : ViewComponent
    {
        #region Fields
        private readonly IEnumerable<IBreadcrumbsFilter> filters;
        private readonly IBreadcrumbsRetriever breadcrumbsRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public XperienceBreadcrumbs(
            IEnumerable<IBreadcrumbsFilter> filters,
            IBreadcrumbsRetriever breadcrumbsRetriever,
            IPageDataContextRetriever pageContextRetriever
        )
        {
            this.filters = filters;
            this.breadcrumbsRetriever = breadcrumbsRetriever;
            this.pageContextRetriever = pageContextRetriever;
        }

        /// <summary> Execute <see cref="IBreadcrumbsFilter"/>s against the given <paramref name="breadcrumbs"/>. </summary>
        /// <param name="breadcrumbs"> The breadcrumbs to filter. </param>
        protected async Task<IEnumerable<BreadcrumbItem>> FilterBreadcrumbsAsync( IEnumerable<BreadcrumbItem> breadcrumbs )
        {
            breadcrumbs ??= Enumerable.Empty<BreadcrumbItem>();
            if( filters?.Any() == true )
            {
                foreach( var filter in filters.OrderByDescending( filter => filter.Order ) )
                {
                    breadcrumbs = await filter.OnFilterBreadcrumbsAsync( HttpContext, breadcrumbs );
                }
            }

            return breadcrumbs;
        }

        /// <summary> Get breadcrumbs for the current Xperience page. </summary>
        protected async Task<IEnumerable<BreadcrumbItem>> GetPageBreadcrumbsAsync( )
            => pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? await breadcrumbsRetriever.RetrieveAsync( context.Page )
                : null;

        public virtual async Task<IViewComponentResult> InvokeAsync( )
        {
            var breadcrumbs = await GetPageBreadcrumbsAsync();
            return View(
                await FilterBreadcrumbsAsync( breadcrumbs )
            );
        }

    }

}
