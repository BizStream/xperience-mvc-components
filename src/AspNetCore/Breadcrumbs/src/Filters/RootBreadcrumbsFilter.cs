using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Filters
{

    /// <summary> Implementation of an <see cref="IRootBreadcrumbsFilter"/> that executes all <see cref="IBreadcrumbsFilter"/>s registered to the <see cref="HttpContext.RequestServices"/>. </summary>
    public class RootBreadcrumbsFilter : IRootBreadcrumbsFilter
    {
        #region Properties
        public int Order => 0;
        #endregion

        /// <inheritdoc/>
        public async Task<IEnumerable<BreadcrumbItem>> OnFilterBreadcrumbsAsync( HttpContext context, IEnumerable<BreadcrumbItem> breadcrumbs )
        {
            var filters = context.RequestServices.GetRequiredService<IEnumerable<IBreadcrumbsFilter>>();

            breadcrumbs ??= Enumerable.Empty<BreadcrumbItem>();
            if( filters?.Any() == true )
            {
                foreach( var filter in filters.OrderByDescending( filter => filter.Order ) )
                {
                    breadcrumbs = await filter.OnFilterBreadcrumbsAsync( context, breadcrumbs );
                }
            }

            return breadcrumbs;
        }

    }

}
