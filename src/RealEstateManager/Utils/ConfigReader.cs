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
                    : throw new InvalidOperationException("Invalid Agent Registration Key.");
            }
        }

        public static Guid AdminRegistrationKey
        {
            get
            {
                var guidStringValue = GetSettingValue("AdminRegistrationKey");

                return Guid.TryParse(guidStringValue, out var result)
                    ? result
                    : throw new InvalidOperationException("Invalid Admin Registration Key.");
            }
        }

        private static string GetSettingValue(string key)
        {
            return WebConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}
