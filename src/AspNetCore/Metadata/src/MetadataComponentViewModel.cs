using System.Collections.Generic;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.Metadata
{

    public class MetadataComponentViewModel
    {

        public string Author { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Keywords { get; set; }

    }

}
