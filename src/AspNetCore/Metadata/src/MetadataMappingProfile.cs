using System;
using AutoMapper;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    public class MetadataMappingProfile : Profile
    {

        public MetadataMappingProfile( )
        {
            CreateMap<IPageDataContext<TreeNode>, MetadataComponentViewModel>()
                .ForMember( viewModel => viewModel.Description, opt => opt.MapFrom( context => context.Metadata.Description ) )
                .ForMember( viewModel => viewModel.Title, opt => opt.MapFrom( context => context.Metadata.Title ) )
                .ForMember( viewModel => viewModel.Keywords, opt => opt.MapFrom( context => context.Metadata.Keywords.Split( new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries ) ) );
        }

    }

}
