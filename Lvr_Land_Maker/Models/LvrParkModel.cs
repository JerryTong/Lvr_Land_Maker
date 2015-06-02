using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models
{
    public class LvrParkModel
    {
        /// <summary>
        /// 編號
        /// </summary>
        [XmlElement("編號")]
        public string Number { get; set; }

        /// <summary>
        /// 交易金額
        /// </summary>
        [XmlElement("車位價格")]
        public decimal Cost { get; set; }

        /// <summary>
        /// 平方公尺
        /// </summary>
        [XmlElement("車位面積平方公尺")]
        public decimal SquareMeter { get; set; }

        /// <summary>
        /// 坪
        /// </summary>
        public decimal LevelGround { get; set; }

        /// <summary>
        /// 車位類別
        /// </summary>
        [XmlElement("車位類別")]
        public string ParkType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EditDateTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }
    }
}
