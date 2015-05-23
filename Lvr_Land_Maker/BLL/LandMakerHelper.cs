using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using Lvr_Land_Maker.Models.Enum;
using FrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lvr_Land_Maker.Models.Configuartion;

namespace Lvr_Land_Maker.BLL
{
    public class LandMakerHelper
    {
        private static string FORMAT_EXCEPTION_MSG = "轉換{0}({1})屬性時發現未知名稱: ' {2} ' ";
        private static List<LandFileDetailInfo> taiwanLocationInfo = null;
        private static List<Logger> transLogger = new List<Logger>();

        /// <summary>
        /// 獲取檔案銷售類型(A、B、C)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SaleType GetSaleType(string token)
        {
            switch (token)
            {
                case "A":
                    return SaleType.Sale;
                case "B":
                    return SaleType.Leasing;
                case "C":
                    return SaleType.PreOrder;
                default:
                    return SaleType.none;
            }
        }

        public static List<Logger> GetTransLogger()
        {
            return transLogger;
        }

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

        /// <summary>
        /// 取得指定實價登錄檔案地區區域資料。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LandFileDetailInfo GetLandXmlFileDetailInfo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            if (taiwanLocationInfo == null)
            {
                taiwanLocationInfo = LandMakerDA.GetLocationInfo();
            }
           
            string withoutFileName = Path.GetFileNameWithoutExtension(fileName);
            string cityCode = withoutFileName.Substring(0, 1);
            var locationDetail = taiwanLocationInfo.Where(t => t.CityCode == cityCode).FirstOrDefault();
            
            LandFileDetailInfo result = new LandFileDetailInfo();
            if (locationDetail == null)
            {
                result.CityCode = "-1";
                result.CityName = "None";
            }

            result.CityName = locationDetail.CityName;
            result.CityCode = locationDetail.CityCode;
            result.ZipCode = locationDetail.ZipCode;
            result.ParnetFileName = withoutFileName;
            result.SaleType = ConvertSaleType(withoutFileName.Substring(withoutFileName.Length - 1, 1));

            return result;
        }

        /// <summary>
        /// 將原始資料(買賣、預售屋)轉換成Database table 欄位。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DataTable LvrTableToDataBaseTable(LandFileDetailInfo sourceDetail, DataTable columnTable)
        {
            transLogger.Clear();

            if (sourceDetail == null || sourceDetail.OriginalLvrLandTable == null)
            {
                return null;
            }

            DataTable result = new DataTable();
            result.Columns.Add("IsAdd");

            foreach (DataRow row in columnTable.Rows)
            {
                result.Columns.Add(row["ColumnName"].ToString());
            }
           
            int index = 0;
            foreach (DataRow dr in sourceDetail.OriginalLvrLandTable.Rows)
            {
                DataRow tempRow = result.NewRow();
                tempRow["SaleType"] = sourceDetail.SaleType;
                tempRow["City"] = sourceDetail.CityCode;
                tempRow["CityCode"] = sourceDetail.CityCode;
                tempRow["CityName"] = sourceDetail.CityName;
                tempRow["ZipCode"] = sourceDetail.ZipCode;
                tempRow["Townships"] = dr["鄉鎮市區"];
                tempRow["Address"] = dr["土地區段位置或建物區門牌"];
                tempRow["Subject"] = dr["交易標的"];
                tempRow["SubjectCode"] = (int)ConvertSubjectType(dr["交易標的"].ToString());
                tempRow["Partition"] = dr["都市土地使用分區"];
                tempRow["PartitionCode"] = (int)ConvertPartitionType(dr["都市土地使用分區"].ToString());
                tempRow["Non_Partition"] = dr["非都市土地使用分區"];
                tempRow["Non_Scheduled"] = dr["非都市土地使用編定"];
                tempRow["LandSquareMeter"] = dr["土地移轉總面積平方公尺"];
                tempRow["LandLevelGround"] = CalculateSMToLevelGround(dr["土地移轉總面積平方公尺"].ToDobule(0.0)).ToInt(-1);
                tempRow["TradeDate"] = ConvertTradeDateTime(dr["交易年月"].ToString());
                tempRow["TradeYear"] = ConvertTradeYear(dr["交易年月"].ToString());
                tempRow["AllFloors"] = dr["總樓層數"].ToInt(-1);
                tempRow["TradeFloors"] = dr["移轉層次"];
                tempRow["BuildsType"] = dr["建物型態"];
                tempRow["BuildsTypeCode"] = (int)ConvertBuildsType(dr["建物型態"].ToString());
                tempRow["Using"] = dr["主要用途"];
                tempRow["UsingCode"] = (int)ConvertUsingType(dr["主要用途"].ToString());
                tempRow["Materials"] = dr["主要建材"];
                tempRow["MaterialsCode"] = (int)ConvertMaterialsType(dr["主要建材"].ToString());
                tempRow["CompletedDate"] = ConvertCompleteDateTime(dr["建築完成年月"].ToString());
                tempRow["BuildsSquareMeter"] = dr["建物移轉總面積平方公尺"];
                tempRow["BuildsLevelGround"] = CalculateSMToLevelGround(dr["建物移轉總面積平方公尺"].ToDobule(0.0));
                tempRow["Room"] = dr["建物現況格局-房"];
                tempRow["Livingroom"] = dr["建物現況格局-廳"];
                tempRow["Bathroom"] = dr["建物現況格局-衛"];
                tempRow["Compartment"] = ConvertBool(dr["建物現況格局-隔間"].ToString()).ToInt(0);
                tempRow["Management"] = ConvertBool(dr["有無管理組織"].ToString()).ToInt(0);
                tempRow["Cost"] = dr["總價元"];
                tempRow["SquareMeterCost"] = dr["單價每平方公尺"].ToDecimal(0.0M);
                tempRow["LevelGroundCost"] = ConvertCostSM2LG(dr["單價每平方公尺"].ToString());
                tempRow["CarType"] = dr["車位類別"];
                tempRow["CarTypeCode"] = (int)ConvertParkingType(dr["車位類別"].ToString());
                tempRow["CarSquareMeter"] = dr["車位移轉總面積平方公尺"];
                tempRow["CarLevelGround"] = CalculateSMToLevelGround(dr["車位移轉總面積平方公尺"].ToDobule(0.0));
                tempRow["CarCost"] = dr["車位總價元"];
                tempRow["EditTime"] = DateTime.Now;

                ////是否允許寫入DB
                tempRow["IsAdd"] = 1;

                result.Rows.Add(tempRow);
                index++;
            }

            return result;
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

        #region LvrLandData to DataBase Table Helper Function.
        /// <summary>
        /// Convert to Subject Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static SubjectType ConvertSubjectType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return SubjectType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("SubjectType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (SubjectType)type.Value.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "交易標的", "SubjectType", value));

            #region old
            ////switch (value)
            ////{
            ////    case "建物":
            ////        return SubjectType.Buildings;
            ////    case "土地":
            ////        return SubjectType.Land;
            ////    case "車位":
            ////        return SubjectType.Parking;
            ////    case "房地(土地+建物)":
            ////        return SubjectType.BuildingsLand;
            ////    case "房地(土地+建物)+車位":
            ////        return SubjectType.BuildingsLand;
            ////    default:
            ////        return SubjectType.Non;
            ////}
            #endregion            
        }

        /// <summary>
        /// Convert Partition Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static PartitionType ConvertPartitionType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return PartitionType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("PartitionType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (PartitionType)type.Value.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "都市土地使用分區", "PartitionType", value));

            #region old
            ////switch (value)
            ////{
            ////    case "住":
            ////        return PartitionType.Residential;
            ////    case "商":
            ////        return PartitionType.Business;
            ////    case "工":
            ////        return PartitionType.Industrial;
            ////    case "農":
            ////        return PartitionType.Agricultural;
            ////    case "其他":
            ////        return PartitionType.Other;
            ////    default:
            ////        return PartitionType.Non;
            ////}
            #endregion
        }

        /// <summary>
        /// Convert Build Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static BuildsType ConvertBuildsType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BuildsType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("BuildsType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (BuildsType)type.Value.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "建物型態", "BuildsType", value));

            #region
            ////switch (value)
            ////{
            ////    case "辦公商業大樓":
            ////        return BuildsType.BusinessBuilding;
            ////    case "華夏(10樓以下電梯)":
            ////        return BuildsType.Building_10;
            ////    case "公寓(5樓含以下無電梯)":
            ////        return BuildsType.Apartment;
            ////    case "透天厝":
            ////        return BuildsType.Detached;
            ////    case "廠辦":
            ////        return BuildsType.FactoryOffice;
            ////    case "住宅大樓(11層含以上有電梯)":
            ////        return BuildsType.LargeBuilding;
            ////    case "店面(店鋪)":
            ////        return BuildsType.Store;
            ////    case "套房(1房1廳1衛)":
            ////        return BuildsType.Suite;
            ////    case "其他":
            ////        return BuildsType.Other;
            ////    default:
            ////        return BuildsType.Non;
            ////}
            #endregion
        }

        /// <summary>
        /// Convert Using Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static UsingType ConvertUsingType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return UsingType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("UsingType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (UsingType)type.Value.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "主要用途", "UsingType", value));

            #region
            ////switch (value)
            ////{
            ////    case "住家用":
            ////        return UsingType.HomeUse;
            ////    case "商業用":
            ////        return UsingType.Commercial;
            ////    case "國民住宅":
            ////        return UsingType.NationalHouse;
            ////    case "見使用執照":
            ////        return UsingType.SeeUseLicense;
            ////    case "停車空間":
            ////        return UsingType.ParkingSpace;
            ////    case "工業用":
            ////        return UsingType.Industrial;
            ////    case "住商用":
            ////        return UsingType.HomeUseCommercial;
            ////    case "見其他登記事項":
            ////        return UsingType.Other;
            ////    default:
            ////        return UsingType.Non;
            ////}
            #endregion
        }

        /// <summary>
        /// Convert Materials Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static MaterialsType ConvertMaterialsType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return MaterialsType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("MaterialsType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (MaterialsType)type.Value.ToInt(-1);
            }


            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "主要建材", "MaterialsType", value));

            #region 
            ////switch (value)
            ////{
            ////    case "鋼筋混擬土造":
            ////        return MaterialsType.RCC;
            ////    case "鋼骨混凝土造":
            ////        return MaterialsType.SCC;
            ////    case "鋼骨鋼筋混凝土造":
            ////        return MaterialsType.SRCC;
            ////    case "預力混凝土造":
            ////        return MaterialsType.PCC;
            ////    case "土磚石混合造":
            ////        return MaterialsType.MSM;
            ////    case "磚造":
            ////        return MaterialsType.MB;
            ////    case "加強磚造":
            ////        return MaterialsType.SB;
            ////    case "見使用執照":
            ////        return MaterialsType.SeeLicense;
            ////    case "見其他登記事項":
            ////        return MaterialsType.Other;
            ////    default:
            ////        return MaterialsType.Non;


            ////}
            #endregion
        }


        /// <summary>
        /// Convert bool Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool ConvertBool(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var type = AttributeConfigManager.GetAttribute("HasBool").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return type.Value == "1";
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "布林值轉換", "Had-Bool", value));

            #region
            ////switch (value)
            ////{
            ////    case "有":
            ////        return true;
            ////    case "無":
            ////        return false;
            ////    default:
            ////        return false;
            ////}
            #endregion
        }

        /// <summary>
        /// Convert parking Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static ParkingType ConvertParkingType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ParkingType.Non;
            }

            var type = AttributeConfigManager.GetAttribute("ParkingType").Items.Where(s => s.Name.Equals(value)).FirstOrDefault();
            if (type != null)
            {
                return (ParkingType)type.Value.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "車位類別", "ParkingType", value));

            #region
            ////switch (value)
            ////{
            ////    case "坡道平面":
            ////        return ParkingType.Rp;
            ////    case "一樓平面":
            ////        return ParkingType.Ff;
            ////    case "坡道機械":
            ////        return ParkingType.Rm;
            ////    case "塔式車位":
            ////        return ParkingType.Tp;
            ////    case "升降機械":
            ////        return ParkingType.Lm;
            ////    case "升降平面":
            ////        return ParkingType.LP;
            ////    case "其他":
            ////        return ParkingType.Other;
            ////    default:
            ////        return ParkingType.Non;
            ////}
            #endregion           
        }

        /// <summary>
        /// Convert parking Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static DateTime ConvertTradeDateTime(string value)
        {
            string year = "-1";
            string month = "-1";
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);

            try
            {
                if (value.Length == 4)
                {
                    ////9910
                    year = (value.Remove(2, 2).ToInt(-1) + 1911).ToString();
                    month = value.Remove(0, 2);

                    int tmpMonth = month.ToInt(-1);
                    if (tmpMonth < 1 || tmpMonth > 12)
                    {
                        year = "1900";
                        month = "01";
                    }

                    return DateTime.ParseExact(string.Format("{0}{1}01", year, month), "yyyyMMdd", culture);
                }
                else if (value.Length == 5)
                {
                    ////10310
                    year = (value.Remove(3, 2).ToInt(-1) + 1911).ToString();
                    month = value.Remove(0, 3);

                    int tmpMonth = month.ToInt(-1);
                    if (tmpMonth < 1 || tmpMonth > 12)
                    {
                        year = "1900";
                        month = "01";
                    }

                    return DateTime.ParseExact(string.Format("{0}{1}01", year, month), "yyyyMMdd", culture);
                }
            }
            catch (Exception ex)
            {
                throw new ArithmeticException(string.Format("Origianl Value:{0}\n{1}\n{2}", value, ex.Message, ex.StackTrace));
            }

            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Convert complete date time.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static DateTime ConvertCompleteDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, 0);
            }

            try
            {
                string year = (value.Substring(0, 3).ToInt(0) + 1911).ToString();
                string month = (value.Length >= 5) ? value.Substring(3, 2) : "01";
                string date = (value.Length >= 7) ? value.Substring(5, 2) : "01";

                bool isLeapYear = false;
                if (month == "02")
                {
                    int iYear = year.ToInt(-1);
                    if ((iYear % 4 == 0 && iYear % 100 != 0) || iYear % 400 == 0)
                    {
                        isLeapYear = true;
                    }

                    if (!isLeapYear && date.ToInt(-1) > 28)
                    {
                        date = "28";
                    }
                }

                IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);
                return DateTime.ParseExact(string.Format("{0}{1}{2}", year, month, date), "yyyyMMdd", culture);
            }
            catch (Exception ex)
            {
                throw new ArithmeticException(string.Format("Function on ConvertCompleteDateTime() - value: {0}\n{1}\n{2}", value, ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Convert complete date time.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static decimal ConvertCostSM2LG(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(decimal);
            }

            decimal original = value.ToDecimal(0.0M);
            return (original * 0.3025M).ToDecimal(0.0M);
        }

        /// <summary>
        /// Convert parking Type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int ConvertTradeYear(string value)
        {
            if (value.Length == 4)
            {
                ////9910
                return value.Remove(2, 2).ToInt(-1) + 1911;
            }
            else if (value.Length == 5)
            {
                ////10310
                return value.Remove(3, 2).ToInt(-1) + 1911;
            }

            return -1;
        }

        /// <summary>
        /// Square Meter to Level Ground.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static double CalculateSMToLevelGround(double value)
        {
            return (double)(value * 0.3025);
        }
        #endregion
        
    }
}
