using System;
using System.IO;
using System.Linq;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using CMS.Base;
using CMS.CMSImportExport;
using CMS.DocumentEngine;
using CMS.Localization;
using CMS.Membership;
using CMS.SiteProvider;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests.Abstractions
{

    public class OpenGraphTests<TStartup> : AutomatedTestsWithIsolatedWebApplication<TStartup>
    where TStartup : class
    {
        #region Fields
        private const string ImportPackagePath = @"CMSSiteUtils\Import\BizStream_OpenGraph_Tests.zip";
        #endregion

        private TestNode CreateTestNode( string name )
        {
            name = $"{nameof( TestNode )} {name}";
            var node = new TestNode
            {
                DocumentName = name,
                OpenGraphDescription = "Test Description",
                OpenGraphImage = "~/getmedia/00000000-0000-0000-0000-000000000000/image.png?&ext=.png",
                OpenGraphTitle = name,
                OpenGraphVideo = "~/getmedia/00000000-0000-0000-0000-000000000000/video.mp4?&ext=.mp4"
            };

            node.SetValue( nameof( TreeNode.DocumentPageTitle ), node.OpenGraphTitle + " Page Title" );
            node.SetValue( nameof( TreeNode.DocumentPageDescription ), node.OpenGraphDescription + " Page Description" );

            return node;
        }

        protected TestNode GetTestNodeByAlias( string alias )
            => DocumentHelper.GetDocuments<TestNode>()
                .TopN( 1 )
                .WhereEquals( nameof( TreeNode.NodeAlias ), alias )
                .First();

        private void ImportObjectsData( )
        {
            var settings = new SiteImportSettings( UserInfo.Provider.Get( "administrator" ) )
            {
                EnableSearchTasks = false,
                ImportType = ImportTypeEnum.AllNonConflicting,
                SourceFilePath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, ImportPackagePath ),
                WebsitePath = SystemContext.WebApplicationPhysicalPath
            };

            settings.LoadDefaultSelection();

            ImportProvider.ImportObjectsData( settings );
            ImportProvider.DeleteTemporaryFiles( settings, false );
        }

        [SetUp]
        public void OpenGraphTestsSetUp( )
           => SeedData();

        protected virtual void SeedData( )
        {
            ImportObjectsData();

            var site = SeedSite();
            var root = SeedRootNode( site );

            SeedTestNodeWithCompleteOpenGraphData( root );
            SeedTestNodeWithoutDescriptionOpenGraphData( root );
            SeedTestNodeWithoutImageOpenGraphData( root );
            SeedTestNodeWithoutTitleOpenGraphData( root );
            SeedTestNodeWithoutVideoOpenGraphData( root );
        }

        private TreeNode SeedRootNode( SiteInfo site )
        {
            var data = new DataContainer();
            data.SetValue( nameof( TreeNode.NodeSiteID ), site.SiteID );

            var root = TreeNode.New<TreeNode>( SystemDocumentTypes.Root, data );
            root.Insert( null, false );

            return root;
        }

        private SiteInfo SeedSite( )
        {
            var site = SiteInfo.Provider.Get( "NewSite" );
            CultureSiteInfo.Provider.Add(
                CultureInfo.Provider.Get( "en-US" ).CultureID,
                site.SiteID
            );

            SiteContext.CurrentSite = site;
            return site;
        }

        private void SeedTestNodeWithCompleteOpenGraphData( TreeNode parent )
            => CreateTestNode( "With Complete OpenGraph Data" )
                .Insert( parent );

        private void SeedTestNodeWithoutDescriptionOpenGraphData( TreeNode parent )
        {
            var node = CreateTestNode( "Without Description OpenGraph Data" );

            node.OpenGraphDescription = null;
            node.SetValue( nameof( TreeNode.DocumentPageDescription ), null );

            node.Insert( parent );
        }

        private void SeedTestNodeWithoutImageOpenGraphData( TreeNode parent )
        {
            var node = CreateTestNode( "Without Image OpenGraph Data" );
            node.OpenGraphImage = null;

            node.Insert( parent );
        }

        private void SeedTestNodeWithoutTitleOpenGraphData( TreeNode parent )
        {
            var node = CreateTestNode( "Without Title OpenGraph Data" );
            node.OpenGraphTitle = null;

            node.Insert( parent );
        }

        private void SeedTestNodeWithoutVideoOpenGraphData( TreeNode parent )
        {
            var node = CreateTestNode( "Without Video OpenGraph Data" );
            node.OpenGraphVideo = null;

            node.Insert( parent );
        }

    }

}
