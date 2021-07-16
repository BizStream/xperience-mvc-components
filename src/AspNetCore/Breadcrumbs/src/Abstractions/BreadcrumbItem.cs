using Microsoft.AspNetCore.Http;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Represents a single item of a hierarchy of pages. </summary>
    public class BreadcrumbItem
    {

        /// <summary> The text to be displayed for the item. </summary>
        public string Label { get; set; }

        /// <summary> The Url path of the page the item references. </summary>
        public PathString Path { get; set; } = PathString.Empty;

    }

}
