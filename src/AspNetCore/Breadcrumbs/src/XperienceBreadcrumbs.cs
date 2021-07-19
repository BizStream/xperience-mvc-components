using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    /// <summary> A <see cref="ViewComponent"/> that renders breadcrumbs based on Xperience Content Tree Routing.  </summary>
    public sealed class XperienceBreadcrumbs : ViewComponent
    {
        #region Fields
        private readonly IBreadcrumbsRetriever breadcrumbsRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        private readonly IRootBreadcrumbsFilter rootBreadcrumbsFilter;
        #endregion

        public XperienceBreadcrumbs(
            IBreadcrumbsRetriever breadcrumbsRetriever,
            IPageDataContextRetriever pageContextRetriever,
            IRootBreadcrumbsFilter rootBreadcrumbsFilter
        )
        {
            this.breadcrumbsRetriever = breadcrumbsRetriever;
            this.pageContextRetriever = pageContextRetriever;
            this.rootBreadcrumbsFilter = rootBreadcrumbsFilter;
        }

        public async Task<IViewComponentResult> InvokeAsync( )
        {
            var breadcrumbs = pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? await breadcrumbsRetriever.RetrieveAsync( context.Page )
                : null;

            breadcrumbs = await rootBreadcrumbsFilter.OnFilterBreadcrumbsAsync( HttpContext, breadcrumbs );
            return View( breadcrumbs );
        }

    }

}
