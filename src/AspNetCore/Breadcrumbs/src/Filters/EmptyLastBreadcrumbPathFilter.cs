using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using Microsoft.AspNetCore.Http;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Filters
{

    /// <summary> Ensures the last breadcrumb's <see cref="BreadcrumbItem.Path"/> is empty. </summary>
    public class EmptyLastBreadcrumbPathFilter : BreadcrumbsFilter
    {
        #region Properties

        /// <inheritdoc/>
        public override int Order => -1;
        #endregion

        /// <inheritdoc/>
        public override Task<IEnumerable<BreadcrumbItem>> OnFilterBreadcrumbsAsync( HttpContext context, IEnumerable<BreadcrumbItem> breadcrumbs )
        {
            var crumbs = breadcrumbs?.ToList() ?? new();
            if( crumbs.Any() )
            {
                crumbs[ ^1 ].Path = PathString.Empty;
            }

            return Task.FromResult( crumbs.AsEnumerable() );
        }

    }

}
