using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Common
{
    public static class ReadConfigData
    {       
        public static string GetAppSettingsValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }
    }
}
