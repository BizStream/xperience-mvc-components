using AutoMapper;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    public class Metadata : ViewComponent
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IPageDataContextRetriever retriever;
        #endregion

        public Metadata(
            IMapper mapper,
            IPageDataContextRetriever retriever
        )
        {
            this.mapper = mapper;
            this.retriever = retriever;
        }

        public IViewComponentResult Invoke( )
        {
            if( !retriever.TryRetrieve( out IPageDataContext<TreeNode> context ) )
            {
                return Content( string.Empty );
            }

            var viewModel = mapper.Map<MetadataComponentViewModel>( context );
            return View( viewModel );
        }

    }

}
