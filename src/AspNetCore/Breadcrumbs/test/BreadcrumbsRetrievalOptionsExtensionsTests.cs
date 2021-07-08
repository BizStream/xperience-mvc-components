using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests
{

    [TestFixture( Category = "Unit" )]
    [TestOf( typeof( BreadcrumbRetrievalOptionsExtensions ) )]
    public class BreadcrumbsRetrievalOptionsExtensionsTests
    {

        [Test]
        public void ExcludeCurrentPage_ShouldSetIsCurrentPageIncluded( [Values] bool excluded )
        {
            var options = new BreadcrumbRetrievalOptions()
                .ExcludeCurrentPage( excluded );

            Assert.AreEqual( !excluded, options.IsCurrentPageIncluded );
        }

        [Test]
        public void ExcludeHomePage_ShouldSetIsHomePageIncluded( [Values] bool excluded )
        {
            var options = new BreadcrumbRetrievalOptions()
                .ExcludeHomePage( excluded );

            Assert.AreEqual( !excluded, options.IsHomePageIncluded );
        }

        [Test]
        public void IncludeCurrentPage_ShouldSetIsCurrentPageIncluded( [Values] bool included )
        {
            var options = new BreadcrumbRetrievalOptions()
                .IncludeCurrentPage( included );

            Assert.AreEqual( included, options.IsCurrentPageIncluded );
        }

        [Test]
        public void IncludeHomePage_ShouldSetIsHomePageIncluded( [Values] bool included )
        {
            var options = new BreadcrumbRetrievalOptions()
                .IncludeHomePage( included );

            Assert.AreEqual( included, options.IsHomePageIncluded );
        }

    }

}
