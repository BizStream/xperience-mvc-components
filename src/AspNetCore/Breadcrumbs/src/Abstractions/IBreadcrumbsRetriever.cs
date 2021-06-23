using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Models;
using CMS.DocumentEngine;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs.Abstractions
{

    /// <summary> Describes a service that can retrieve breadcrumbs. </summary>
    public interface IBreadcrumbsRetriever
    {

        /// <summary> Retrieve the <see cref="BreadcrumbItem"/>s for the given <paramref name="node"/>. </summary>s
        Task<IEnumerable<BreadcrumbItem>> RetrieveAsync( TreeNode node, CancellationToken cancellationToken = default );

    }

}
