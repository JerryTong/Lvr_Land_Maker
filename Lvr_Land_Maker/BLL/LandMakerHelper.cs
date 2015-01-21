using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class LandMakerHelper
    {
        /// <summary>
        /// Create Land column data table.
        /// </summary>
        public static DataTable CreateLandColumnDataTable()
        {
            DataTable source = new DataTable();
            source.Columns.Add("ColumnName", typeof(string));
            source.Columns.Add("Type", typeof(string));
            source.Columns.Add("Default", typeof(string));
            source.Columns.Add("Priority", typeof(int));

            source.Rows.Add("SaleType", "string", "");
            source.Rows.Add("City", "string", "");
            source.Rows.Add("CityCode", "string", "");
            source.Rows.Add("CityName", "string", "");
            source.Rows.Add("ZipCode", "int", "");
            source.Rows.Add("Townships", "string", "");
            source.Rows.Add("Address", "string", "");
            source.Rows.Add("Subject", "string", "");
            source.Rows.Add("SubjectCode", "int", "");
            source.Rows.Add("Partition", "string", "");
            source.Rows.Add("PartitionCode", "int", "");
            source.Rows.Add("Non_Partition", "string", "");
            source.Rows.Add("Non_Scheduled", "string", "");
            source.Rows.Add("LandSquareMeter", "double", "");
            source.Rows.Add("LandLevelGround", "double", "");
            source.Rows.Add("TradeDate", "DateTime", "");
            source.Rows.Add("TradeYear", "string", "");
            source.Rows.Add("AllFloors", "int", "");
            source.Rows.Add("TradeFloors", "string", "");
            source.Rows.Add("BuildsType", "string", "");
            source.Rows.Add("BuildsTypeCode", "int", "");
            source.Rows.Add("Using", "string", "");
            source.Rows.Add("UsingCode", "int", "");
            source.Rows.Add("Materials", "string", "");
            source.Rows.Add("MaterialsCode", "int", "");
            source.Rows.Add("CompletedDate", "DateTime", "");
            source.Rows.Add("BuildsSquareMeter", "double", "");
            source.Rows.Add("BuildsLevelGround", "double", "");
            source.Rows.Add("Room", "int", "");
            source.Rows.Add("Livingroom", "int", "");
            source.Rows.Add("Bathroom", "int", "");
            source.Rows.Add("Compartment", "bool", "");
            source.Rows.Add("Management", "bool", "");
            source.Rows.Add("Cost", "decimal", "");
            source.Rows.Add("SquareMeterCost", "decimal", "");
            source.Rows.Add("LevelGroundCost", "decimal", "");
            source.Rows.Add("CarType", "string", "");
            source.Rows.Add("CarTypeCode", "int", "");
            source.Rows.Add("CarSquareMeter", "double", "");
            source.Rows.Add("CarLevelGround", "double", "");
            source.Rows.Add("CarCost", "decimal", "");
            source.Rows.Add("EditTime", "DateTime", "");

            ////setting priority
            for (int i = 0; i < source.Rows.Count; i++)
            {
                source.Rows[i][3] = i;
            }

            return source;
        }

        public static LandFileDetailInfo GetLandXmlFileDetailInfo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var taiwanlocationInfo = LandMakerDA.GetLocationInfo();

            string withoutFileName = Path.GetFileNameWithoutExtension(fileName);
            string cityCode = withoutFileName.Substring(0, 1);
            var detail = taiwanlocationInfo.Where(t => t.CityCode == cityCode).FirstOrDefault();
            if (detail == null)
            {
                detail = new LandFileDetailInfo
                {
                    CityCode = "-1",
                    CityName = "None"
                };
            }

            detail.FileName = withoutFileName;
            detail.SaleType = ConvertSaleType(withoutFileName.Substring(withoutFileName.Length - 1, 1));

            return detail;
        }


        private static SaleType ConvertSaleType(string value)
        {
            switch (value.ToUpper())
            {
                case "A":
                    return SaleType.Sale;
                case "B":
                    return SaleType.PreOrder;
                case "C":
                    return SaleType.Leasing;
                default:
                    return SaleType.none;
            }
        }
    }
}
