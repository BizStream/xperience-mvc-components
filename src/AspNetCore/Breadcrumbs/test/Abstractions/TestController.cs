using Microsoft.AspNetCore.Mvc;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions
{

    public class TestController : Controller
    {

        [HttpGet( "" )]
        public IActionResult Index( )
            => Content( "Hello, World!" );

        public IActionResult Test( )
            => ViewComponent( nameof( BreadcrumbsComponent ) );

    }

}
