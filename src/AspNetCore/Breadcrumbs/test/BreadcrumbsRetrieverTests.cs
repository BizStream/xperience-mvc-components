using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using CMS.DocumentEngine;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests
{

    [TestFixture( Category = "IsolatedMvc" )]
    [TestOf( typeof( BreadcrumbsRetriever ) )]
    public class BreadcrumbsRetrieverTests : BreadcrumbsTests<BreadcrumbsRetrieverTests.Startup>
    {
        public class Startup : XperienceTestStartup
        {
            public override void ConfigureTests( IApplicationBuilder app )
            {
            }

            public override void ConfigureTestServices( IServiceCollection services )
                => services.AddTransient<BreadcrumbsRetriever>();
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
        [TestCase( "TestNode-0" )]
        [TestCase( "TestNode-1" )]
        [TestCase( "TestNode-2" )]
        public async Task RetrieveAsync_ShouldReturnItems( string alias )
        {
            var breadcrumbRetriever = Factory.Services.GetRequiredService<BreadcrumbsRetriever>();
            var node = DocumentHelper.GetDocuments<TestNode>()
                .TopN( 1 )
                .WhereEquals( nameof( TreeNode.NodeAlias ), alias )
                .First();

            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync( node );
            Assert.IsTrue( breadcrumbs.Any() );
        }

    }

}
