using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models
{
    public class LandFileDetailInfo
    {
        public string ParentFileName { get; set; }

        public SaleType SaleType { get; set; }

        [XmlElement("TransactionNumber")]
        public int Uid { get; set; }

        [XmlElement("CityCode")]
        public string CityCode { get; set; }

        [XmlElement("City")]
        public string CityName { get; set; }

        [XmlElement("ZipCode")]
        public int ZipCode { get; set; }

        [XmlElement("TownshipCode")]
        public string TownshipCode { get; set; }

        [XmlElement("Township")]
        public string Townships { get; set; }

        /// <summary>
        /// 原始Lvr_Land數據
        /// </summary>
        public DataTable OriginalLvrLandTable { get; set; }

        /// <summary>
        /// 預售、中古數據
        /// </summary>
        public List<BusinessModel> BusinessModel { get; set; }

        /// <summary>
        /// 建物數據
        /// </summary>
        public List<LvrBuildModel> BuildModel { get; set; }

        /// <summary>
        /// 土地數據
        /// </summary>
        public List<LvrLandModel> LandModel { get; set; }

        /// <summary>
        /// 停車數據
        /// </summary>
        public List<LvrParkModel> ParkModel { get; set; }

        /// <summary>
        /// 轉換後Lvr_Land數據
        /// </summary>
        public DataTable TransLvrLandTable { get; set; }

        #region version 2.0
        public string BuildFileName { get; set; }

        public string LandFileName { get; set; }

        public string ParkFileName { get; set; }

        public DataTable OriginalBuildTable { get; set; }

        public DataTable OriginalLandTable { get; set; }

        public DataTable OriginalParkTable { get; set; }
        #endregion
    }
}
