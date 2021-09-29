using System;
using System.Threading;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions
{

    /// <summary> Describes a service that can retrieve a page's <see cref="OpenGraphData"/>. </summary>
    public interface IOpenGraphDataRetriever
    {

        /// <summary> Retrieve the <see cref="OpenGraphData"/> for the given <paramref name="page"/>. </summary>s
        /// <param name="page"> The page in which to retrieve breadcrumbs. </param>
        /// <param name="configure"> An optional lambda to configure options used to retrieve OpenGraph data. </param>
        Task<OpenGraphData> RetrieveAsync( TreeNode page, Action<OpenGraphDataRetrievalOptions> configure = default, CancellationToken cancellation = default );

    }

}
