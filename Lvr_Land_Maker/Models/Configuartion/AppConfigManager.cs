using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Configuartion
{
    public class AppConfigManager
    {
        private static AppConfig config = null;

        public static AppConfig Current
        {
            get
            {
                if (config == null)
                {
                    config = new AppConfig();
                }

                return config;
            }
        }
    }

    public class AppConfig
    {
        public string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["Tong"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LvrLandOriginalBase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LvrLandOriginalBase"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TestLvrLandOriginalBase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Test_LvrLandOriginalBase"].ToString();
            }
        }

        /// <summary>
        /// 土地表名
        /// </summary>
        public string LandTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LandTable"].ToString();
            }
        }

        /// <summary>
        /// 建物表名
        /// </summary>
        public string BuildTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BuildTable"].ToString();
            }
        }

        /// <summary>
        /// 停車空間表名
        /// </summary>
        public string ParkTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ParkTable"].ToString();
            }
        }

        /// <summary>
        /// (暫存)土地表名
        /// </summary>
        public string TestLandTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Test_LandTable"].ToString();
            }
        }

        /// <summary>
        /// (暫存)建物表名
        /// </summary>
        public string TestBuildTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Test_BuildTable"].ToString();
            }
        }

        /// <summary>
        /// (暫存)停車空間表名
        /// </summary>
        public string TestParkTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Test_ParkTable"].ToString();
            }
        }
    }
}
