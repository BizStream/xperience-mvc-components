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

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Tests.Abstractions
{

    public class BreadcrumbsTests<TStartup> : AutomatedTestsWithIsolatedWebApplication<TStartup>
        where TStartup : class
    {
        #region Fields
        private const string ImportPackagePath = @"CMSSiteUtils\Import\BizStream_Breadcrumbs_Tests.zip";
        #endregion

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

            CultureSiteInfo.Provider.Add(
                CultureInfo.Provider.Get( "en-US" ).CultureID,
                site.SiteID
            );

            var data = new DataContainer();
            data.SetValue( nameof( TreeNode.NodeSiteID ), site.SiteID );

            var root = TreeNode.New<TreeNode>( SystemDocumentTypes.Root, data );
            root.Insert( null, false );

            var parent = root;
            foreach(
                var node in Enumerable.Range( 0, 3 )
                    .Select( n => new TestNode { DocumentName = $"{nameof( TestNode )} {n}" } )
            )
            {
                node.Insert( parent );
                parent = node;
            }

            var folder = TreeNode.New( "CMS.Folder" );
            folder.DocumentName = "Test Folder";
            folder.Insert( root );

            parent = folder;
            foreach(
                var node in Enumerable.Range( 0, 3 )
                    .Select( n => new TestNode { DocumentName = $"{nameof( TestNode )} {n} in Folder" } )
            )
            {
                node.Insert( parent );
                parent = node;
            }
        }

        [SetUp]
        public void BreadcrumbsTestsSetUp( )
           => SeedData();

    }

}
