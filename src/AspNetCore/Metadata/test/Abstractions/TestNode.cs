using BizStream.Kentico.Xperience.AspNetCore.Components.Metadata.Tests.Abstractions;
using CMS;
using CMS.Base;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterDocumentType( TestNode.CLASS_NAME, typeof( TestNode ) )]
[assembly: RegisterPageRoute( TestNode.CLASS_NAME, typeof( TestController ), ActionName = nameof( TestController.Test ) )]

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata.Tests.Abstractions
{
    /// <summary>
    /// Represents a content item of type TestNode.
    /// </summary>
    public partial class TestNode : TreeNode
    {
        #region "Constants and variables"

        /// <summary>
        /// The name of the data class.
        /// </summary>
        public const string CLASS_NAME = "BizStream.TestNode";

        /// <summary>
        /// The instance of the class that provides extended API for working with TestNode fields.
        /// </summary>
        private readonly TestNodeFields mFields;

        #endregion

        #region "Properties"

        /// <summary>
        /// TestNodeID.
        /// </summary>
        [DatabaseIDField]
        public int TestNodeID
        {
            get
            {
                return ValidationHelper.GetInteger( GetValue( "TestNodeID" ), 0 );
            }
            set
            {
                SetValue( "TestNodeID", value );
            }
        }


        /// <summary>
        /// Heading.
        /// </summary>
        [DatabaseField]
        public string Heading
        {
            get
            {
                return ValidationHelper.GetString( GetValue( "Heading" ), @"" );
            }
            set
            {
                SetValue( "Heading", value );
            }
        }


        /// <summary>
        /// Content.
        /// </summary>
        [DatabaseField]
        public string Content
        {
            get
            {
                return ValidationHelper.GetString( GetValue( "Content" ), @"" );
            }
            set
            {
                SetValue( "Content", value );
            }
        }


        /// <summary>
        /// Gets an object that provides extended API for working with TestNode fields.
        /// </summary>
        [RegisterProperty]
        public TestNodeFields Fields
        {
            get
            {
                return mFields;
            }
        }


        /// <summary>
        /// Provides extended API for working with TestNode fields.
        /// </summary>
        [RegisterAllProperties]
        public partial class TestNodeFields : AbstractHierarchicalObject<TestNodeFields>
        {
            /// <summary>
            /// The content item of type TestNode that is a target of the extended API.
            /// </summary>
            private readonly TestNode mInstance;


            /// <summary>
            /// Initializes a new instance of the <see cref="TestNodeFields" /> class with the specified content item of type TestNode.
            /// </summary>
            /// <param name="instance">The content item of type TestNode that is a target of the extended API.</param>
            public TestNodeFields( TestNode instance )
            {
                mInstance = instance;
            }


            /// <summary>
            /// TestNodeID.
            /// </summary>
            public int ID
            {
                get
                {
                    return mInstance.TestNodeID;
                }
                set
                {
                    mInstance.TestNodeID = value;
                }
            }


            /// <summary>
            /// Heading.
            /// </summary>
            public string Heading
            {
                get
                {
                    return mInstance.Heading;
                }
                set
                {
                    mInstance.Heading = value;
                }
            }


            /// <summary>
            /// Content.
            /// </summary>
            public string Content
            {
                get
                {
                    return mInstance.Content;
                }
                set
                {
                    mInstance.Content = value;
                }
            }
        }

        #endregion


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="TestNode" /> class.
        /// </summary>
        public TestNode( ) : base( CLASS_NAME )
        {
            mFields = new TestNodeFields( this );
        }

        #endregion
    }
}
