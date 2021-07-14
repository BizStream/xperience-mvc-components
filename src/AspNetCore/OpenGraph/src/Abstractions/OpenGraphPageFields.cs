using Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure;
using CMS.DocumentEngine;

namespace Bizstream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions
{

    /// <summary> Represents source field names to be used when mapping a <see cref="TreeNode"/> to a <see cref="OpenGraphData"/>. </summary>
    /// <seealso cref="OpenGraphDataRetriever.CreateOpenGraphData(TreeNode, OpenGraphPageFields)"/>
    public class OpenGraphPageFields
    {

        #region Fields

        /// <summary> Default field name prefix. </summary>
        public const string FieldPrefix = "OpenGraph";
        #endregion

        /// <summary> The <see cref="TreeNode"/> field used to map the <see cref="OpenGraphData.Description"/> value. </summary>
        public string Description { get; set; } = FieldPrefix + nameof( Description );

        /// <summary> The <see cref="TreeNode"/> field used to map the <see cref="OpenGraphData.ImageUrl"/> value. </summary>
        public string Image { get; set; } = FieldPrefix + nameof( Image );

        /// <summary> The <see cref="TreeNode"/> field used to map the <see cref="OpenGraphData.Title"/> value. </summary>
        public string Title { get; set; } = FieldPrefix + nameof( Title );

        /// <summary> The <see cref="TreeNode"/> field used to map the <see cref="OpenGraphData.VideoUrl"/> value. </summary>
        public string Video { get; set; } = FieldPrefix + nameof( Video );

    }

}
