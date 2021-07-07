using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    /// <summary> A <see cref="ViewComponent"/> that can be used to render breadcrumbs from the Xperience Content Tree. </summary>
    public class Breadcrumbs : ViewComponent
    {
        #region Fields
        private readonly IBreadcrumbsRetriever breadcrumbsRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public Breadcrumbs(
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
            if( breadcrumbs?.Any() != true )
            {
                return Content( string.Empty );
            }

            return View( breadcrumbs );
        }

    }

}
