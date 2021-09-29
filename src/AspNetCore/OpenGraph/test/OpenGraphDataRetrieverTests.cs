using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests
{

    [TestFixture( Category = "IsolatedMvc" )]
    [TestOf( typeof( OpenGraphDataRetriever ) )]
    public class OpenGraphDataRetrieverTests : OpenGraphTests<OpenGraphDataRetrieverTests.Startup>
    {
        public class Startup : XperienceTestStartup
        {
            public override void ConfigureTests( IApplicationBuilder app )
            {
            }

            public override void ConfigureTestServices( IServiceCollection services )
                => services.AddTransient<OpenGraphDataRetriever>();
        }

        [SetUp]
        public async Task BreadcrumbsRetrieverTestsSetUp( )
            => await Client.GetAsync( "/" );

        protected override XperienceWebApplicationFactory<Startup> CreateWebApplicationFactory( )
        {
            var factory = base.CreateWebApplicationFactory();
            factory.Server.PreserveExecutionContext = true;

            return factory;
        }

        [Test]
        [TestCase( "TestNode-With-Complete-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Title-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Image-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Video-OpenGraph-Data" )]
        public async Task RetrieveAsync_ShouldReturnData( string alias )
        {
            var openGraphRetriever = Factory.Services.GetRequiredService<OpenGraphDataRetriever>();
            var data = await openGraphRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias )
            );

            Assert.IsNotNull( data );
        }

        [Test]
        [TestCase( "TestNode-With-Complete-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Title-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Image-OpenGraph-Data" )]
        [TestCase( "TestNode-Without-Video-OpenGraph-Data" )]
        public async Task RetrieveAsync_UseMetadataFields_ShouldUseMetadataFields( string alias )
        {
            var openGraphRetriever = Factory.Services.GetRequiredService<OpenGraphDataRetriever>();
            var data = await openGraphRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias ),
                options => options.UseMetadataFields()
            );

            Assert.IsNotNull( data );
            Assert.IsTrue( data.Title.EndsWith("Page Title"), $"Title is not using Page Metadata Title." );
            Assert.IsTrue( data.Description.EndsWith( "Page Description" ), $"Description is not using Page Metadata Description." );

        }

    }

}
