using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Abstract implementation of <see cref="IBreadcrumbsFilter"/>. </summary>
    public abstract class BreadcrumbsFilter : IBreadcrumbsFilter
    {
        #region Properties

        /// <inheritdoc/>
        public virtual int Order => 0;
        #endregion

        /// <inheritdoc/>
        public abstract Task<IEnumerable<BreadcrumbItem>> OnFilterBreadcrumbsAsync( HttpContext context, IEnumerable<BreadcrumbItem> breadcrumbs );

    }
}
