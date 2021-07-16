using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Describes a service that may be used by the <see cref="XperienceBreadcrumbs"/> ViewComponent to modify retrieved <see cref="BreadcrumbItem"/>s. </summary>
    public interface IBreadcrumbsFilter
    {
        #region Properties

        /// <summary> Order of filter execution. Filters with a greater <see cref="Order"/> are executed first. </summary>
        int Order { get; }
        #endregion

        /// <summary> Filter the given <paramref name="breadcrumbs"/> for the given <paramref name="httpContext"/>. </summary>
        /// <param name="httpContext"> The current <see cref="HttpContext"/>. </param>
        /// <param name="breadcrumbs"> The <see cref="BreadcrumbItem"/>s to modify. </param>
        /// <returns> The modified <paramref name="breadcrumbs"/>. </returns>
        Task<IEnumerable<BreadcrumbItem>> OnFilterBreadcrumbsAsync( HttpContext httpContext, IEnumerable<BreadcrumbItem> breadcrumbs );

    }

}
