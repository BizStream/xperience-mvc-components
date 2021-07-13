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
        private readonly IBreadcrumbsRetriever breadcrumbsRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public XperienceBreadcrumbs(
            IBreadcrumbsRetriever breadcrumbsRetriever,
            IPageDataContextRetriever pageContextRetriever
        )
        {
            this.breadcrumbsRetriever = breadcrumbsRetriever;
            this.pageContextRetriever = pageContextRetriever;
        }

        public async Task<IViewComponentResult> InvokeAsync( )
        {
            var breadcrumbs = pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? await breadcrumbsRetriever.RetrieveAsync( context.Page )
                : null;

            return View( breadcrumbs ?? Enumerable.Empty<BreadcrumbItem>() );
        }

    }

}
