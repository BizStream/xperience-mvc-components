using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Describes a service that can retrieve breadcrumbs. </summary>
    public interface IBreadcrumbsRetriever
    {

        /// <summary> Retrieve the <see cref="BreadcrumbItem"/>s for the given <paramref name="page"/>. </summary>s
        /// <param name="page"> The page in which to retrieve breadcrumbs. </param>
        /// <param name="configure"> An optional lambda to configure options used to retrieve breadcrumbs. </param>
        Task<IEnumerable<BreadcrumbItem>> RetrieveAsync( TreeNode page, Action<BreadcrumbRetrievalOptions> configure = default, CancellationToken cancellation = default );

    }

}
