using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    public class BreadcrumbsComponent : ViewComponent
    {
        #region Fields
        private readonly IBreadcrumbsRetriever breadcrumbsRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public BreadcrumbsComponent(
            IBreadcrumbsRetriever breadcrumbsRetriever,
            IPageDataContextRetriever pageContextRetriever
        )
        {
            this.breadcrumbsRetriever = breadcrumbsRetriever;
            this.pageContextRetriever = pageContextRetriever;
        }

        public async Task<IViewComponentResult> InvokeAsync( )
        {
            if( !pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context ) )
            {
                return Content( string.Empty );
            }

            var breadcrumbs = await breadcrumbsRetriever.RetrieveAsync( context.Page );
            return View( breadcrumbs );
        }

    }

}
