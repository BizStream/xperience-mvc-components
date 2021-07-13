using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests
{

    [TestFixture( Category = "IsolatedMvc" )]
    [TestOf( typeof( OpenGraph ) )]
    public class OpenGraphComponentTests : OpenGraphTests<OpenGraphComponentTests.Startup>
    {
        public class Startup : XperienceTestStartup
        {
            public override void ConfigureTests( IApplicationBuilder app )
            {
            }

            public override void ConfigureTestServices( IServiceCollection services )
                => services.AddXperienceOpenGraphComponent();

            public override void ConfigureXperience( IFeaturesBuilder features )
                => features.UsePageRouting();
        }

        [Test]
        [TestCase( "/testnode-with-complete-opengraph-data" )]
        [TestCase( "/testnode-without-description-opengraph-data" )]
        [TestCase( "/testnode-without-image-opengraph-data" )]
        [TestCase( "/testnode-without-title-opengraph-data" )]
        [TestCase( "/testnode-without-video-opengraph-data" )]
        public async Task OpenGraphComponent_RenderedContent_ShouldNotBeEmpty( string url )
        {
            var response = await Client.GetAsync( url );
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.IsFalse( string.IsNullOrEmpty( content ) );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldNotRenderDescription_WhenThereIsNoDescription( )
        {
            var response = await Client.GetAsync( "/testnode-without-description-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var description = document.Head.QuerySelector( "meta[name='og:description']" );
            Assert.IsNull( description );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldRenderDescription_WhenThereIsADescription( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var description = document.Head.QuerySelector( "meta[name='og:description']" );
            Assert.IsNotNull( description );
            Assert.AreEqual( "Test Description", description.GetAttribute( "content" ) );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldNotRenderTitle_WhenThereIsNoTitle( )
        {
            var response = await Client.GetAsync( "/testnode-without-title-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var title = document.Head.QuerySelector( "meta[name='og:title']" );
            Assert.IsNull( title );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldRenderTitle_WhenThereIsATitle( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var title = document.Head.QuerySelector( "meta[name='og:title']" );
            Assert.IsNotNull( title );
            Assert.AreEqual( $"{nameof( TestNode )} With Complete OpenGraph Data", title.GetAttribute( "content" ) );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldNotRenderImage_WhenThereIsNoImage( )
        {
            var response = await Client.GetAsync( "/testnode-without-image-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var image = document.Head.QuerySelector( "meta[name='og:image']" );
            Assert.IsNull( image );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldRenderImage_WhenThereIsAImage( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var image = document.Head.QuerySelector( "meta[name='og:image']" );
            Assert.IsNotNull( image );
            Assert.AreEqual("http://localhost/getmedia/00000000-0000-0000-0000-000000000000/image.png?&ext=.png", image.GetAttribute( "content" ) );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldNotRenderVideo_WhenThereIsNoVideo( )
        {
            var response = await Client.GetAsync( "/testnode-without-video-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var image = document.Head.QuerySelector( "meta[name='og:video']" );
            Assert.IsNull( image );
        }

        [Test]
        public async Task OpenGraphComponent_ShouldRenderVideo_WhenThereIsAVideo( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-opengraph-data" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var image = document.Head.QuerySelector( "meta[name='og:video']" );
            Assert.IsNotNull( image );
            Assert.AreEqual( "http://localhost/getmedia/00000000-0000-0000-0000-000000000000/video.mp4?&ext=.mp4", image.GetAttribute( "content" ) );
        }

    }

}
