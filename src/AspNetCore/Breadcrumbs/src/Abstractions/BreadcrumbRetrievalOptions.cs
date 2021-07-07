namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Represents options used when retrieving <see cref="BreadcrumbItem"/>s for a page. </summary>
    public class BreadcrumbRetrievalOptions
    {

        /// <summary> Indicates whether the page that items are being retrieved for, should be included in the retrieved items. </summary>
        public bool IsCurrentPageIncluded { get; set; } = true;

        /// <summary> Indicates whether the Home page should be included in the retrieved items. </summary>
        public bool IsHomePageIncluded { get; set; } = true;

    }

}
