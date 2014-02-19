using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DTTraceFileHelper.Helpers
{
    public static class Config
    {
        public static string GetValue(string configKey)
        {
            string result = ConfigurationManager.AppSettings.Get(configKey);

            if (result != null)
            {
                return result;
            }

            throw new IndexOutOfRangeException(String.Format("Provided configKey {0} not found in configuration file", configKey));

        }
    }
}
