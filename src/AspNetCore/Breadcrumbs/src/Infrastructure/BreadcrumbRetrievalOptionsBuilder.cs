using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure
{

    /// <summary> Builder type for assisting with the configuration of <see cref="BreadcrumbRetrievalOptions"/>s. </summary>
    public class BreadcrumbRetrievalOptionsBuilder
    {
        #region Fields
        private readonly BreadcrumbRetrievalOptions options;
        #endregion

        #region Properties

        /// <summary> Access the current state of the builder. </summary>
        public BreadcrumbRetrievalOptions Options => options;
        #endregion

        public BreadcrumbRetrievalOptionsBuilder( )
            => options = new();

        public BreadcrumbRetrievalOptionsBuilder( BreadcrumbRetrievalOptions options )
            => this.options = Clone( options );

        /// <summary> Builds the current state of the builder to a new <see cref="BreadcrumbRetrievalOptions"/> instance. </summary>
        public BreadcrumbRetrievalOptions Build( )
            => Clone( options );

        /// <summary> Clones all options to a new <see cref="BreadcrumbRetrievalOptions"/> instance.  </summary>
        public BreadcrumbRetrievalOptions Clone( BreadcrumbRetrievalOptions options )
            => new()
            {
                IsCurrentPageIncluded = options.IsCurrentPageIncluded,
                IsHomePageIncluded = options.IsHomePageIncluded
            };

    }

}
