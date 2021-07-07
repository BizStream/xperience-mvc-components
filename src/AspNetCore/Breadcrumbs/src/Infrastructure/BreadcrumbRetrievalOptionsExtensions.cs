using System;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Infrastructure
{

    /// <summary> Extensions to <see cref="BreadcrumbRetrievalOptions"/>s for fluent configuration. </summary>
    public static class BreadcrumbRetrievalOptionsExtensions
    {

        /// <summary> Configure the provided <paramref name="options"/> to exclude the current page from the retrieved breadcrumbs. </summary>
        /// <param name="options"> The <see cref="BreadcrumbRetrievalOptions"/> to configure. </param>
        /// <param name="excluded"> Whether the current page should be excluded. </param>
        public static BreadcrumbRetrievalOptions ExcludeCurrentPage( this BreadcrumbRetrievalOptions options, bool excluded = true )
            => IncludeCurrentPage( options, !excluded );

        /// <summary> Configure the provided <paramref name="options"/> to exclude the home page from the retrieved breadcrumbs. </summary>
        /// <param name="options"> The <see cref="BreadcrumbRetrievalOptions"/> to configure. </param>
        /// <param name="excluded"> Whether the home page should be excluded. </param>
        public static BreadcrumbRetrievalOptions ExcludeHomePage( this BreadcrumbRetrievalOptions options, bool excluded = true )
            => IncludeHomePage( options, !excluded );

        /// <summary> Configure the provided <paramref name="options"/> to include the current page from the retrieved breadcrumbs. </summary>
        /// <param name="options"> The <see cref="BreadcrumbRetrievalOptions"/> to configure. </param>
        /// <param name="included"> Whether the current page should be included. </param>
        public static BreadcrumbRetrievalOptions IncludeCurrentPage( this BreadcrumbRetrievalOptions options, bool included = true )
        {
            ThrowIfNull( options );

            options.IsCurrentPageIncluded = included;
            return options;
        }

        /// <summary> Configure the provided <paramref name="options"/> to include the home page from the retrieved breadcrumbs. </summary>
        /// <param name="options"> The <see cref="BreadcrumbRetrievalOptions"/> to configure. </param>
        /// <param name="included"> Whether the home page should be included. </param>
        public static BreadcrumbRetrievalOptions IncludeHomePage( this BreadcrumbRetrievalOptions options, bool included = true )
        {
            ThrowIfNull( options );

            options.IsHomePageIncluded = included;
            return options;
        }

        private static void ThrowIfNull( BreadcrumbRetrievalOptions options )
        {
            if( options is null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }
        }

    }

}
