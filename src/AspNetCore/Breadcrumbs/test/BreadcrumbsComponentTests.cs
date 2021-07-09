using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests
{

    [TestFixture( Category = "IsolatedMvc" )]
    [TestOf( typeof( Breadcrumbs ) )]
    public class BreadcrumbsComponentTests : BreadcrumbsTests<BreadcrumbsComponentTests.Startup>
    {
        public class Startup : XperienceTestStartup
        {
            public override void ConfigureTests( IApplicationBuilder app )
            {
            }

            public override void ConfigureTestServices( IServiceCollection services )
                => services.AddXperienceBreadcrumbsComponent();

            public override void ConfigureXperience( IFeaturesBuilder features )
                => features.UsePageRouting();
        }

        [Test]
        [TestCase( "/testnode-0" )]
        [TestCase( "/testnode-0/testnode-1" )]
        [TestCase( "/testnode-0/testnode-1/testnode-2" )]
        public async Task BreadcrumbsComponent_RenderedContent_ShouldNotBeEmpty( string url )
        {
            var response = await Client.GetAsync( url );
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.IsFalse( string.IsNullOrEmpty( content ) );
        }

        [Test]
        [TestCase( "/testnode-0" )]
        [TestCase( "/testnode-0/testnode-1" )]
        [TestCase( "/testnode-0/testnode-1/testnode-2" )]
        public async Task BreadcrumbsComponent_RenderedContent_ShouldHaveContainerDivWithCssClass( string url )
        {
            var response = await Client.GetAsync( url );
            response.EnsureSuccessStatusCode();

            var element = await HtmlHelpers.GetElementAsync( response );

            Assert.IsInstanceOf<IHtmlDivElement>( element );

            Assert.AreEqual( "DIV", element.TagName );
            Assert.Contains( "breadcrumbs-container", element.ClassList.ToArray() );
        }

        [Test]
        [TestCase( "/testnode-0" )]
        [TestCase( "/testnode-0/testnode-1" )]
        [TestCase( "/testnode-0/testnode-1/testnode-2" )]
        public async Task BreadcrumbsComponent_RenderedContent_ShouldHaveNavWithCssClass( string url )
        {
            var response = await Client.GetAsync( url );
            response.EnsureSuccessStatusCode();

            var element = ( await HtmlHelpers.GetElementAsync( response ) )
                ?.FirstElementChild;

            Assert.AreEqual( "NAV", element.TagName );
            Assert.Contains( "breadcrumbs", element.ClassList.ToArray() );
        }

    }

}
