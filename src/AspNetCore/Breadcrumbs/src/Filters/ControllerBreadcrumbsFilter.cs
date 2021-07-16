using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Filters
{

    /// <summary> A <see cref="BreadcrumbsFilter"/> that filters breadcrumbs for the given <typeparamref name="TController"/>. </summary>
    /// <typeparam name="TController"> The type of controller to filter breadcrumbs for. </typeparam>
    public abstract class ControllerBreadcrumbsFilter<TController> : BreadcrumbsFilter
        where TController : ControllerBase
    {
        #region Fields
        private readonly TypeInfo controllerType;
        #endregion

        public ControllerBreadcrumbsFilter( )
            => controllerType = typeof( TController ).GetTypeInfo();

        /// <inheritdoc/>
        public sealed override async Task<IEnumerable<BreadcrumbItem>> OnFilterBreadcrumbsAsync( HttpContext httpContext, IEnumerable<BreadcrumbItem> breadcrumbs )
        {
            var actionContext = httpContext.RequestServices.GetRequiredService<IActionContextAccessor>()
                ?.ActionContext;

            if(
                actionContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                    && controllerActionDescriptor.ControllerTypeInfo == controllerType
            )
            {
                return await OnFilterControllerBreadcrumbsAsync( httpContext, actionContext, breadcrumbs );
            }

            return breadcrumbs;
        }

        /// <summary> Filter the given <paramref name="breadcrumbs"/> for the given <paramref name="httpContext"/>. </summary>
        /// <param name="httpContext"> The current <see cref="HttpContext"/>. </param>
        /// <param name="actionContext"> The current <see cref="ActionContext"/>. </param>
        /// <param name="breadcrumbs"> The <see cref="BreadcrumbItem"/>s to modify. </param>
        /// <returns> The modified <paramref name="breadcrumbs"/>. </returns>
        public abstract Task<IEnumerable<BreadcrumbItem>> OnFilterControllerBreadcrumbsAsync( HttpContext httpContext, ActionContext actionContext, IEnumerable<BreadcrumbItem> breadcrumbs );

    }

}
