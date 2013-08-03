using System;
using System.Configuration;
using System.Linq;

namespace Common.Tools
{
    /// <summary>
    /// Class to get values of settings in config files
    /// </summary>
    public class SettingsHelper
    {
        #region Constructor & Methods

        private static string[] _appSettingsKeys;

        /// <summary>
        /// Static constructor which init the keys array
        /// </summary>
        static SettingsHelper()
        {
            _appSettingsKeys = ConfigurationManager.AppSettings.Keys.Cast<string>().ToArray();
        }

        /// <summary>
        /// Get the key value matching the current environment
        /// </summary>
        public static string GetSetting(string key)
        {
            string keyWithEnvironment = string.Format("{0}_{1}", key, CurrentEnvironment);

            if (_appSettingsKeys.Contains(keyWithEnvironment))
                return ConfigurationManager.AppSettings[keyWithEnvironment];
            else if (_appSettingsKeys.Contains(key))
                return ConfigurationManager.AppSettings[key];
            else
                throw new Exception(string.Format(@"Can't find the key ""{0}""", key));
        }

        #endregion Constructor & Methods

        #region Key Accessors

        public static string CurrentEnvironment
        {
            get { return ConfigurationManager.AppSettings["Current_Environment"]; }
        }

        public static string ConnectionString
        {
            get { return GetSetting("Connection_String"); }
        }

        #endregion Key Accessors
    }
}