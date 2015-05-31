using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models
{
    public class LvrBuildModel
    {
        /// <summary>
        /// 編號
        /// </summary>
        [XmlElement("編號")]
        public string Number { get; set; }

        /// <summary>
        /// 購買屋齡
        /// </summary>
        [XmlElement("屋齡")]
        public int Year { get; set; }

        /// <summary>
        /// 平方公尺
        /// </summary>
        [XmlElement("建物移轉面積平方公尺")]
        public decimal SquareMeter { get; set; }

        /// <summary>
        /// 坪
        /// </summary>
        public decimal LevelGround { get; set; }

        /// <summary>
        /// 建物類型
        /// </summary>
        [XmlElement("主要用途")]
        public string BuildsType { get; set; }

        /// <summary>
        /// 建材
        /// </summary>
        [XmlElement("主要建材")]
        public string Materials { get; set; }

        /// <summary>
        /// 完工年
        /// </summary>
        [XmlElement("建築完成日期")]
        public DateTime CompletedDate { get; set; }

        /// <summary>
        /// 總樓層
        /// </summary>
        [XmlElement("總層數")]
        public int AllFloors { get; set; }

        /// <summary>
        /// 交易樓層
        /// </summary>
        [XmlElement("建物分層")]
        public int BuildPartitioned { get; set; }
    }
}
