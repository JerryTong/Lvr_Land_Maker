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
        public string FileName { get; set; }

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

        public DataTable LandCollectionTable { get; set; }
    }
}
