using FrameworkLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models.Configuartion
{
    public class CheckDataConfigManager
    {
        private static CheckDataConfig checkDataConfig = null;

        public static CheckDataConfig Current
        {
            get
            {
                if (checkDataConfig == null)
                {
                    checkDataConfig = XmlDataAccess.LoadConfig<CheckDataConfig>("CheckData.config");
                }

                return checkDataConfig;
            }
        }
    }

    [XmlRoot("checkData")]
    public class CheckDataConfig
    {
        [XmlElement("SaleAndPreorder")]
        public SaleAndPerorder SaleAndPreorder { get; set; }
    }

    public class SaleAndPerorder
    {
        [XmlElement("column")]
        public List<Column> columns { get; set; }
    }

    public class Column
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("check_len")]
        public bool IsCheckLen { get; set; }
        
        [XmlAttribute("len_min")]
        public int LenMin { get; set; }

        [XmlAttribute("len_max")]
        public int LenMax { get; set; }

        [XmlAttribute("allowNull")]
        public bool IsAllowNull { get; set; }
    }
}
