using BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Abstractions;

namespace BizStream.Kentico.Xperience.AspNetCore.Components.OpenGraph.Infrastructure
{

    /// <summary> Builder type for assisting with the configuration of <see cref="OpenGraphDataRetrievalOptions"/>s. </summary>
    /// <remarks> This type is intended for use by default infrastructure. </remarks>
    public class OpenGraphDataRetrievalOptionsBuilder
    {
        #region Fields
        private readonly OpenGraphDataRetrievalOptions options;
        #endregion

        #region Properties

        /// <summary> Access the current state of the builder. </summary>
        public OpenGraphDataRetrievalOptions Options => options;
        #endregion

        public OpenGraphDataRetrievalOptionsBuilder( )
            => options = new();

        public OpenGraphDataRetrievalOptionsBuilder( OpenGraphDataRetrievalOptions options )
            => this.options = Clone( options );

        /// <summary> Builds the current state of the builder to a new <see cref="OpenGraphDataRetrievalOptions"/> instance. </summary>
        public OpenGraphDataRetrievalOptions Build( )
            => Clone( options );

        /// <summary> Clones all options to a new <see cref="OpenGraphDataRetrievalOptions"/> instance.  </summary>
        public OpenGraphDataRetrievalOptions Clone( OpenGraphDataRetrievalOptions options )
            => new()
            {
                FieldNames = Clone( options.FieldNames )
            };

        /// <summary> Clones all options to a new <see cref="OpenGraphPageFields"/> instance.  </summary>
        public OpenGraphPageFields Clone( OpenGraphPageFields fields )
            => new()
            {
                Description = fields.Description,
                Image = fields.Image,
                Title = fields.Title,
                Video = fields.Video
            };

    }

}
