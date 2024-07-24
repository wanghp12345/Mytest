using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mytest.@class
{
    internal class ConfigHelper
    {

        public static string GetAppConfig(string str_Key)
        {
            string file = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string[] allKeys = config.AppSettings.Settings.AllKeys;
            foreach (string key in allKeys)
            {
                if (key == str_Key)
                {
                    return config.AppSettings.Settings[str_Key].Value.ToString();
                }
            }
            return null;
        }

        public static void UpdateAppConfig(string str_newKey, string str_newVal)
        {
            string file = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            string[] allKeys = config.AppSettings.Settings.AllKeys;
            foreach (string key in allKeys)
            {
                if (key == str_newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(str_newKey);
            }
            config.AppSettings.Settings.Add(str_newKey, str_newVal);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
