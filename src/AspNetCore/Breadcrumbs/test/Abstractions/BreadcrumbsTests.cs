using System;
using System.IO;
using System.Linq;
using BizStream.Kentico.Xperience.AspNetCore.Mvc.Testing;
using CMS.Base;
using CMS.CMSImportExport;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing;
using CMS.Localization;
using CMS.Membership;
using CMS.SiteProvider;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions
{

    public class BreadcrumbsTests<TStartup> : AutomatedTestsWithIsolatedWebApplication<TStartup>
        where TStartup : class
    {
        #region Fields
        private const string ImportPackagePath = @"CMSSiteUtils\Import\BizStream_Breadcrumbs_Tests.zip";
        #endregion

        [SetUp]
        public void BreadcrumbsTestsSetUp( )
           => SeedData();

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

        protected virtual void SeedData( )
        {
            ImportObjectsData();

            var site = SiteInfo.Provider.Get( "NewSite" );
            SiteContext.CurrentSite = site;

            // create default culture
            CultureSiteInfo.Provider.Add(
                CultureInfo.Provider.Get( "en-US" ).CultureID,
                site.SiteID
            );

            var root = SeedRootNode( site );

            SeedHomeNode( root );
            SeedTestNodes( root );

            var folder = SeedTestFolder( root );
            SeedTestNodes( folder, seed: node => node.DocumentName = $"{node.DocumentName} in Folder" );
        }

        private void SeedHomeNode( TreeNode parent )
        {
            var node = new TestNode
            {
                DocumentName = "Home"
            };

            node.Insert( parent );
            SettingsKeyInfoProvider.SetValue( PageRoutingHelper.HOME_PAGE_PATH_KEY, node.NodeSiteName, node.NodeAliasPath );
        }

        private TreeNode SeedRootNode( SiteInfo site )
        {
            var data = new DataContainer();
            data.SetValue( nameof( TreeNode.NodeSiteID ), site.SiteID );

            var root = TreeNode.New<TreeNode>( SystemDocumentTypes.Root, data );
            root.Insert( null, false );

            return root;
        }

        private TreeNode SeedTestFolder( TreeNode parent )
        {
            var folder = TreeNode.New( "CMS.Folder" );
            folder.DocumentName = "Test Folder";
            folder.Insert( parent );

            return folder;
        }

        private void SeedTestNodes( TreeNode parent, int depth = 3, Action<TreeNode> seed = default )
        {
            var nodes = Enumerable.Range( 0, depth )
                .Select(
                    number =>
                    {
                        var node = new TestNode
                        {
                            DocumentName = $"{nameof( TestNode )} {number}"
                        };

                        seed?.Invoke( node );
                        return node;
                    }
                );

            foreach( var node in nodes )
            {
                node.Insert( parent );
                parent = node;
            }
        }

    }

}
