using FrameworkLibrary.DataAccess;
using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.DAL
{
    public class LandMakerDA
    {
        /// <summary>
        /// 取得台灣鄉鎮市區資料集。
        /// </summary>
        /// <returns></returns>
        public static List<LandFileDetailInfo> GetLocationInfo()
        {
            var result = XmlDataAccess.LoadCollection<LandFileDetailInfo>("LocationInfo.xml");
            return result == null ? null : result.ToList();
        }

        /// <summary>
        /// 取得指定路徑之內政部實價登入檔。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataTable GetLvrLandInfo(string path)
        {
            return XmlDataAccess.LoadDataTable(Path.GetFileName(path), Path.GetDirectoryName(path));
        }
    }
}
