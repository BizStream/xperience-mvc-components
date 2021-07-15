using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Filters;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests
{

    [TestFixture( Category = "Unit" )]
    [TestOf( typeof( EmptyLastBreadcrumbPathFilter ) )]
    public class EmptyLastBreadcrumbPathFilterTests
    {

        [Test]
        public void Filter_ShouldNotThrowOnNullOrEmptyBreadcrumbs( )
        {
            var filter = new EmptyLastBreadcrumbPathFilter();

            Assert.DoesNotThrowAsync(
                async ( ) => await filter.OnFilterBreadcrumbsAsync( new DefaultHttpContext(), null )
            );

            Assert.DoesNotThrowAsync(
              async ( ) => await filter.OnFilterBreadcrumbsAsync( new DefaultHttpContext(), Enumerable.Empty<BreadcrumbItem>() )
            );
        }

        [Test]
        public async Task Filter_ShouldEmptyLastBreadcrumbItemsPath( )
        {
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Label = "Test", Path = "/test" }
            };

            var filter = new EmptyLastBreadcrumbPathFilter();
            var crumbs = ( await filter.OnFilterBreadcrumbsAsync( new DefaultHttpContext(), breadcrumbs ) )
                .ToList();

            Assert.AreEqual( PathString.Empty, crumbs[ ^1 ].Path );
        }

    }
}
