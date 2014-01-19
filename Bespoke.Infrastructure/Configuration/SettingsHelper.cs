using System;
using System.Configuration;

namespace Bespoke.Infrastructure.Configuration
{
    public class SettingsHelper
    {
        /// <summary>
        /// Returns the setting from ConfigurationManager.AppSetting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(string key, T defaultValue = default(T))
        {
            if (string.IsNullOrWhiteSpace(key)) 
                return defaultValue;

            var value = ConfigurationManager.AppSettings[key];

            if (value == null) 
                return default(T);

            var type = typeof(T);

            if (type.IsEnum)
                return (T)Enum.Parse(type, value, true);

            return (T)Convert.ChangeType(value, type);
        }
    }
}
