using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InventoryApplication.ExtraClasses
{
    public class CustomLogger
    {
        public static void Info(string data)
        {
            string base_dire = System.Configuration.ConfigurationManager.AppSettings.Get("LogDirPath") + "InventoryLogs.csv";
            using (StreamWriter writer = File.CreateText(base_dire))
            {
                writer.Write(data.ToString());
            }
        }
    }
}