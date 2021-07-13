using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    /// <summary> A <see cref="ViewComponent"/> that renders meta-tags based on the current Xperience Page.  </summary>
    public class XperienceMetadata : ViewComponent
    {
        #region Fields
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public XperienceMetadata( IPageDataContextRetriever pageContextRetriever )
            => this.pageContextRetriever = pageContextRetriever;

        public IViewComponentResult Invoke( )
        {
            var meta = pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? context.Metadata
                : null;

            return View( meta );
        }

    }

}
