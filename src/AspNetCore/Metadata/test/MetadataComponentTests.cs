using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Metadata.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata.Tests
{

    [TestFixture( Category = "IsolatedMvc" )]
    [TestOf( typeof( Metadata ) )]
    public class MetadataComponentTests : MetadataTests<MetadataComponentTests.Startup>
    {
        public class Startup : XperienceTestStartup
        {
            public override void ConfigureTests( IApplicationBuilder app )
            {
            }

            public override void ConfigureTestServices( IServiceCollection services )
                => services.AddXperienceMetadataComponent();

            public override void ConfigureXperience( IFeaturesBuilder features )
                => features.UsePageRouting();
        }

        [Test]
        [TestCase( "/testnode-with-complete-metadata" )]
        [TestCase( "/testnode-without-description-metadata" )]
        [TestCase( "/testnode-without-keywords-metadata" )]
        public async Task MetadataComponent_RenderedContent_ShouldNotBeEmpty( string url )
        {
            var response = await Client.GetAsync( url );
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.IsFalse( string.IsNullOrEmpty( content ) );
        }

        [Test]
        public async Task MetadataComponent_ShouldNotRenderDescription_WhenThereIsNoDescription( )
        {
            var response = await Client.GetAsync( "/testnode-without-description-metadata" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var description = document.Head.QuerySelector( "meta[name=description]" );
            Assert.IsNull( description );
        }

        [Test]
        public async Task MetadataComponent_ShouldRenderDescription_WhenThereIsADescription( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-metadata" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var description = document.Head.QuerySelector( "meta[name=description]" );
            Assert.IsNotNull( description );
            Assert.AreEqual( "Test Description", description.GetAttribute( "content" ) );
        }

        [Test]
        public async Task MetadataComponent_ShouldNotRenderKeywords_WhenThereAreNoKeywords( )
        {
            var response = await Client.GetAsync( "/testnode-without-keywords-metadata" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var keywords = document.Head.QuerySelector( "meta[name=keywords]" );
            Assert.IsNull( keywords );
        }

        [Test]
        public async Task MetadataComponent_ShouldRenderKeywords_WhenThereAreKeywords( )
        {
            var response = await Client.GetAsync( "/testnode-with-complete-metadata" );
            response.EnsureSuccessStatusCode();

            var document = await HtmlHelpers.GetDocumentAsync( response );

            var keywords = document.Head.QuerySelector( "meta[name=keywords]" );
            Assert.IsNotNull( keywords );
            Assert.AreEqual( "Test,Metadata,Xperience", keywords.GetAttribute( "content" ) );
        }

    }

}
