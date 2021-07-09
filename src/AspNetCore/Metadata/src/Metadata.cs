using System.Threading.Tasks;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    /// <summary> A <see cref="ViewComponent"/> that renders meta-tags based on the current Xperience Page.  </summary>
    public class Metadata : ViewComponent
    {
        #region Fields
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public Metadata( IPageDataContextRetriever pageContextRetriever )
            => this.pageContextRetriever = pageContextRetriever;

        public async Task<IViewComponentResult> InvokeAsync( )
        {
            var meta = pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? context.Metadata
                : null;

            return View( meta );
        }

    }

}
