using System;
using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
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
            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias )
            );

            Assert.IsTrue( breadcrumbs.Any() );
        }

        [Test]
        [TestCase( "TestNode-0" )]
        [TestCase( "TestNode-1" )]
        [TestCase( "TestNode-2" )]
        public async Task RetrieveAsync_WithCurrentPage_ShouldReturnCurrentCrumb( string alias )
        {
            var breadcrumbRetriever = Factory.Services.GetRequiredService<BreadcrumbsRetriever>();
            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias ),
                options => options.IncludeHomePage()
            );

            var crumb = breadcrumbs.Last();
            Assert.IsNotNull( crumb );
            Assert.AreEqual( alias.Replace( "-", " " ), crumb.Label );
            Assert.IsTrue( crumb.Path.Value.EndsWith( alias, StringComparison.OrdinalIgnoreCase ), "Crumb path did not match current page alias." );
        }

        [Test]
        [TestCase( "TestNode-0" )]
        [TestCase( "TestNode-1" )]
        [TestCase( "TestNode-2" )]
        public async Task RetrieveAsync_WithoutCurrentPage_ShouldNotReturnCurrentCrumb( string alias )
        {
            var breadcrumbRetriever = Factory.Services.GetRequiredService<BreadcrumbsRetriever>();
            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias ),
                options => options.ExcludeCurrentPage()
            );

            var crumb = breadcrumbs.Last();
            Assert.IsNotNull( crumb );
            Assert.AreNotEqual( alias.Replace( "-", " " ), crumb.Label );
            Assert.IsFalse( crumb.Path.Value.EndsWith( alias ), "Crumb path matched current page alias." );
        }

        [Test]
        [TestCase( "TestNode-0" )]
        [TestCase( "TestNode-1" )]
        [TestCase( "TestNode-2" )]
        public async Task RetrieveAsync_WithHomePage_ShouldReturnHomeCrumb( string alias )
        {
            var breadcrumbRetriever = Factory.Services.GetRequiredService<BreadcrumbsRetriever>();
            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias ),
                options => options.IncludeHomePage()
            );

            var crumb = breadcrumbs.First();
            Assert.IsNotNull( crumb );
            Assert.AreEqual( "Home", crumb.Label );
            Assert.AreEqual( "/", crumb.Path.Value );
        }

        [Test]
        [TestCase( "TestNode-0" )]
        [TestCase( "TestNode-1" )]
        [TestCase( "TestNode-2" )]
        public async Task RetrieveAsync_WithoutHomePage_ShouldNotReturnHomeCrumb( string alias )
        {
            var breadcrumbRetriever = Factory.Services.GetRequiredService<BreadcrumbsRetriever>();
            var breadcrumbs = await breadcrumbRetriever.RetrieveAsync(
                GetTestNodeByAlias( alias ),
                options => options.ExcludeHomePage()
            );

            var crumb = breadcrumbs.First();
            Assert.IsNotNull( crumb );
            Assert.AreNotEqual( "Home", crumb.Label );
            Assert.AreNotEqual( "/", crumb.Path );
        }

    }

}
