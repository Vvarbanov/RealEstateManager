using RealEstateManager.Properties;

namespace RealEstateManager.Utils
{
    public static class Localization
    {
        public static string GetString(string resourceStringName)
        {
            return Resources.ResourceManager.GetString(resourceStringName)
                   ?? resourceStringName;
        }
    }
}
