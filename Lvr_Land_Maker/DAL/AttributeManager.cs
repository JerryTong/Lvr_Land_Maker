using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Lvr_Land_Maker.DAL
{
    public static class AttributeManager
    {
        ////Example 1:
        ////ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
        ////configMap.ExeConfigFilename = @"d:\test\justAConfigFile.config.whateverYouLikeExtension";
        ////Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

        private static string filePath = @"C:\Users\Ton\Documents\Visual Studio 2013\Projects\Lvr_Land_Maker\Lvr_Land_Maker\Attribute.config";
        private static ExeConfigurationFileMap configMap = null;
        private static Configuration configManager = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Configuration Current
        {
            get
            {
                if (configMap == null)
                {
                    configMap = new ExeConfigurationFileMap();
                    configMap.ExeConfigFilename = filePath;
                }

                if (configManager == null)
                {
                    configManager = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                }

                return configManager;
            }
        }
    }
}
