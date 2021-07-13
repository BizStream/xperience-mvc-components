using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using CMS.DocumentEngine;

namespace Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions
{

    /// <summary> Represents options used when retrieving <see cref="OpenGraphData"/> for a page. </summary>
    public class OpenGraphDataRetrievalOptions
    {

        /// <summary> Field names to use when mapping a <see cref="TreeNode"/> to a <see cref="OpenGraphData"/>. </summary>
        /// <seealso cref="OpenGraphDataRetriever.CreateOpenGraphData(TreeNode, OpenGraphPageFields)"/>
        public OpenGraphPageFields FieldNames { get; set; } = new();

    }

}
