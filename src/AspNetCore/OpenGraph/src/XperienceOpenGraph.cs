using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph
{

    /// <summary> A <see cref="ViewComponent"/> that renders meta-tags based on the current Xperience Page.  </summary>
    public class XperienceOpenGraph : ViewComponent
    {
        #region Fields
        private readonly IOpenGraphDataRetriever openGraphRetriever;
        private readonly IPageDataContextRetriever pageContextRetriever;
        #endregion

        public XperienceOpenGraph(
            IOpenGraphDataRetriever openGraphRetriever,
            IPageDataContextRetriever pageContextRetriever
        )
        {
            this.openGraphRetriever = openGraphRetriever;
            this.pageContextRetriever = pageContextRetriever;
        }

        public async Task<IViewComponentResult> InvokeAsync( )
        {
            var meta = pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> context )
                ? await openGraphRetriever.RetrieveAsync( context.Page )
                : null;

            return View( meta );
        }

    }

}
