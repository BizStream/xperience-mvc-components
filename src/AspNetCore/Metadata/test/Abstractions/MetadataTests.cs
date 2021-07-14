using System;
using System.IO;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using CMS.Base;
using CMS.CMSImportExport;
using CMS.DocumentEngine;
using CMS.Localization;
using CMS.Membership;
using CMS.SiteProvider;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata.Tests.Abstractions
{

    public class MetadataTests<TStartup> : AutomatedTestsWithIsolatedWebApplication<TStartup>
    where TStartup : class
    {
        #region Fields
        private const string ImportPackagePath = @"CMSSiteUtils\Import\BizStream_Metadata_Tests.zip";
        #endregion

        [SetUp]
        public void BreadcrumbsTestsSetUp( )
           => SeedData();

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

        protected virtual void SeedData( )
        {
            ImportObjectsData();

            var site = SeedSite();
            var root = SeedRootNode( site );

            SeedTestNodeWithCompleteMetadata( root );
            SeedTestNodeWithoutDescriptionMetadata( root );
            SeedTestNodeWithoutKeywordsMetadata( root );
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

        private void SeedTestNodeWithCompleteMetadata( TreeNode parent )
        {
            var name = $"{nameof( TestNode )} With Complete Metadata";
            var node = new TestNode
            {
                DocumentName = name
            };

            node.SetValue( nameof( TreeNode.DocumentPageTitle ), name );
            node.SetValue( nameof( TreeNode.DocumentPageDescription ), "Test Description" );
            node.SetValue( nameof( TreeNode.DocumentPageKeyWords ), "Test,Metadata,Xperience" );

            node.Insert( parent );
        }

        private void SeedTestNodeWithoutDescriptionMetadata( TreeNode parent )
        {
            var name = $"{nameof( TestNode )} Without Description Metadata";
            var node = new TestNode
            {
                DocumentName = name
            };

            node.SetValue( nameof( TreeNode.DocumentPageTitle ), name );
            node.SetValue( nameof( TreeNode.DocumentPageKeyWords ), "Test,Metadata,Xperience" );

            node.Insert( parent );
        }

        private void SeedTestNodeWithoutKeywordsMetadata( TreeNode parent )
        {
            var name = $"{nameof( TestNode )} Without Keywords Metadata";
            var node = new TestNode
            {
                DocumentName = name
            };

            node.SetValue( nameof( TreeNode.DocumentPageTitle ), name );
            node.SetValue( nameof( TreeNode.DocumentPageDescription ), "Test Description" );

            node.Insert( parent );
        }

    }

}
