﻿using System;
using System.IO;
using System.Reflection;

namespace Songify.General
{
    /// <summary>
    /// Returns the path to all relevant directories and files.
    /// </summary>
    public class PathManager
    {
        public static string StartupDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string LogDirectory
        {
            get
            {
                string logPath = Path.Combine(StartupDirectory, "Logs");
                if (!File.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                return logPath;
            }
        }

        public static string ConfigurationDirectory
        {
            get
            {
                string configPath = Path.Combine(StartupDirectory, "Configuration");
                if (!File.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }

                return configPath;
            }
        }

        public static string PluginDirectory
        {
            get
            {
                string pluginPath = Path.Combine(StartupDirectory, "Plugins");
                if (!File.Exists(pluginPath))
                {
                    Directory.CreateDirectory(pluginPath);
                }

                return pluginPath;
            }
        }
    }
}
