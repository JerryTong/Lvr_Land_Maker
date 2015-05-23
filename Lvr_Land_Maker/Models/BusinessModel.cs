using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lvr_Land_Maker.Models
{
    public class BusinessModel
    {
        #region 原生欄位
        [XmlElement(Mapping.Townships)]
        public string Townships { get; set; }

        [XmlElement(Mapping.Subject)]
        public string Subject { get; set; }

        [XmlElement(Mapping.Address)]
        public string Address { get; set; }

        [XmlElement(Mapping.LandSquareMeter)]
        public decimal LandSquareMeter { get; set; }

        [XmlElement(Mapping.Partition)]
        public string Partition { get; set; }

        [XmlElement(Mapping.Non_Partition)]
        public string Non_Partition { get; set; }

        [XmlElement(Mapping.Non_Scheduled)]
        public string Non_Scheduled { get; set; }

        [XmlElement(Mapping.TradeDate)]
        public string InternalTradeDate { get; set; }

        [XmlElement(Mapping.TradeDetail)]
        public string TradeDetail { get; set; }

        [XmlElement(Mapping.TradeFloors)]
        public string TradeFloors { get; set; }

        [XmlElement(Mapping.AllFloors)]
        public int AllFloors { get; set; }

        [XmlElement(Mapping.BuildsType)]
        public string BuildsType { get; set; }

        [XmlElement(Mapping.Using)]
        public string Using { get; set; }

        [XmlElement(Mapping.Materials)]
        public string Materials { get; set; }

        [XmlElement(Mapping.CompletedDate)]
        public string InternalCompletedDate { get; set; }

        [XmlElement(Mapping.BuildsSquareMeter)]
        public decimal BuildsSquareMeter { get; set; }

        [XmlElement(Mapping.Room)]
        public int Room { get; set; }

        [XmlElement(Mapping.Livingroom)]
        public int Livingroom { get; set; }

        [XmlElement(Mapping.Bathroom)]
        public int Bathroom { get; set; }

        [XmlElement(Mapping.Compartment)]
        public string InternalCompartment { get; set; }

        [XmlIgnore]
        public bool Compartment { get; set; }

        [XmlElement(Mapping.Management)]
        public string InternalManagement
        {
            set
            {
                this.Management = string.IsNullOrEmpty(value) ? false : (value == "有") ? true : false;
            }
        }

        [XmlIgnore]
        public bool Management { get; set; }

        [XmlElement(Mapping.Cost)]
        public decimal Cost { get; set; }

        [XmlElement(Mapping.SquareMeterCost)]
        public decimal SquareMeterCost { get; set; }

        [XmlElement(Mapping.CarType)]
        public string CarType { get; set; }

        [XmlElement(Mapping.CarSquareMeter)]
        public decimal CarSquareMeter { get; set; }

        [XmlElement(Mapping.CarCost)]
        public decimal CarCost { get; set; }

        [XmlElement(Mapping.Remark)]
        public string Remark { get; set; }

        [XmlElement(Mapping.ObjectNumber)]
        public string ObjectNumber { get; set; }
        #endregion

        #region 衍生欄位
        public SaleType SaleType { get; set; }

        public string City { get; set; }

        public string CityCode { get; set; }

        public string CityName { get; set; }

        public int ZipCode { get; set; }

        public DateTime EditTime
        {
            get
            {
                return DateTime.Now;
            }
        }
        #endregion

        #region 轉型衍生欄位
        public int TradeYear { get; set; }

        public int SubjectCode { get; set; }

        public int PartitionCode { get; set; }

        public int LandLevelGround { get; set; }

        public int BuildsTypeCode { get; set; }

        public int BuildsLevelGround { get; set; }

        public int UsingCode { get; set;}

        public int MaterialsCode { get; set; }

        public int LevelGroundCost { get; set; }

        public int CarTypeCode { get; set; }

        public int CarLevelGround { get; set; }

        public DateTime CompletedDate { get; set; }

        public DateTime TradeDate { get; set; }
        #endregion
    }
}
