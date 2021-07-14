using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;
using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using CMS.DocumentEngine;
using NUnit.Framework;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Tests
{

    [TestFixture( Category = "Unit" )]
    [TestOf( typeof( OpenGraphDataRetrievalOptionsExtensions ) )]
    public class OpenGraphDataRetrievalOptionsExtensionsTests
    {

        [Test]
        public void FieldsNames_ShouldBeInitialized( )
        {
            var options = new OpenGraphDataRetrievalOptions();

            Assert.IsNotNull( options.FieldNames );
        }

        [Test]
        public void UseMetadataFields_ShouldSetFieldNames( )
        {
            var options = new OpenGraphDataRetrievalOptions()
                .UseMetadataFields();

            Assert.AreEqual( nameof( TreeNode.DocumentPageTitle ), options.FieldNames.Title );
            Assert.AreEqual( nameof( TreeNode.DocumentPageDescription ), options.FieldNames.Description );
        }

    }

}
