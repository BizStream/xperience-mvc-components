namespace Bizstream.Kentico.Xperience.AspNetCore.Components.Breadcrumbs
{

    public class CacheKeys
    {

        public static string Nodes( string nodeAliasPath )
            => $"breadcrumbs|{nodeAliasPath}";

    }

}
