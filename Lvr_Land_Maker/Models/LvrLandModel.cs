using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models
{
    public class LvrLandModel
    {
        /// <summary>
        /// 編號
        /// </summary>
        [XmlElement("編號")]
        public string Number { get; set; }

        /// <summary>
        /// 土地使用區分
        /// </summary>
        [XmlElement("土地區段位置")]
        public string LandSection { get; set; }

        /// <summary>
        /// 平方公尺
        /// </summary>
        [XmlElement("土地移轉面積平方公尺")]
        public decimal SquareMeter { get; set; }

        /// <summary>
        /// 坪
        /// </summary>
        public decimal LevelGround { get; set; }

        /// <summary>
        /// 都市使用區分
        /// </summary>
        [XmlElement("使用分區或編定")]
        public string Subject { get; set; }
    }
}
