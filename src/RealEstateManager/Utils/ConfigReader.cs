using System;
using System.Web.Configuration;

namespace RealEstateManager.Utils
{
    public static class ConfigReader
    {
        public static Guid AgentRegistrationKey
        {
            get
            {
                var guidStringValue = GetSettingValue("AgentRegistrationKey");

                return Guid.TryParse(guidStringValue, out var result) 
                    ? result 
                    : Guid.Empty;
            }
        }

        private static string GetSettingValue(string key)
        {
            return WebConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}
