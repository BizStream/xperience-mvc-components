using System;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions
{

    /// <summary> Represents a page's OpenGraph data. </summary>
    public class OpenGraphData
    {

        /// <summary> A description of the page. </summary>
        public string Description { get; set; }

        /// <summary> An absolute url to an image for the page. </summary>
        public Uri ImageUrl { get; set; }

        /// <summary> An absolute url to an image for the page. </summary>
        public string Title { get; set; }

        /// <summary> An absolute url to a video for the page. </summary>
        public Uri VideoUrl { get; set; }

    }

}
