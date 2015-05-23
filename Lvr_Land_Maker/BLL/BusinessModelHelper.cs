using Lvr_Land_Maker.Models;
using FrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lvr_Land_Maker.Models.Enum;
using Lvr_Land_Maker.Models.Configuartion;

namespace Lvr_Land_Maker.BLL
{
    public static class BusinessModelHelper
    {
        private static string FORMAT_EXCEPTION_MSG = "轉換{0}({1})屬性時發現未知名稱: ' {2} ' ";

        public static BusinessModel Init(this BusinessModel model, LandFileDetailInfo detail)
        {
            model.City = detail.CityCode;
            model.CityCode = detail.CityCode;
            model.CityName = detail.CityName;
            model.ZipCode = detail.ZipCode;

            model.CompletedDate = ConvertCompleteDateTime(model.InternalCompletedDate);
            model.TradeDate = ConvertTradeDateTime(model.InternalTradeDate);
            model.TradeYear = ConvertTradeYear(model.InternalTradeDate);

            model.SubjectCode = ConvertSubjectType(model.Subject).ToInt(-1);
            model.PartitionCode = ConvertPartitionType(model.Partition).ToInt(-1);
            model.BuildsTypeCode = ConvertBuildsType(model.BuildsType).ToInt(-1);
            model.CarTypeCode = ConvertParkingType(model.CarParkType).ToInt(-1);
            model.UsingCode = ConvertUsingType(model.Using).ToInt(-1);
            model.MaterialsCode = ConvertMaterialsType(model.Materials).ToInt(-1);

            model.LandLevelGround = CalculateSMToLevelGround(model.LandSquareMeter);
            model.BuildsLevelGround = CalculateSMToLevelGround(model.BuildsLevelGround);
            model.CarParkLevelGround = CalculateSMToLevelGround(model.CarParkLevelGround);

            ////model.LevelGroundCost = -1;
            return model;
        }

        /// <summary>
        /// 轉換交易時間
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static DateTime ConvertTradeDateTime(string value)
        {
            string year = "-1";
            string month = "-1";
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);

            if (string.IsNullOrEmpty(value))
            {
                return DateTime.MinValue;
            }

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

            return DateTime.MinValue;
        }

        /// <summary>
        /// 轉換交易年度(民國)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int ConvertTradeYear(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return -1;
            }

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
        /// 轉換建築完工時間
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static DateTime ConvertCompleteDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DateTime.MinValue;
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

        #region LvrLandData to DataBase Table Helper Function.
        /// <summary>
        /// Convert to Subject Type.(交易標的)
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
                return (SubjectType)type.PropertyId.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "交易標的", "SubjectType", value));
        }

        /// <summary>
        /// Convert Partition Type.(都市土地使用分區)
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
                return (PartitionType)type.PropertyId.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "都市土地使用分區", "PartitionType", value));
        }

        /// <summary>
        /// Convert Build Type.(建物型態)
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
                return (BuildsType)type.PropertyId.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "建物型態", "BuildsType", value));
        }

        /// <summary>
        /// Convert Using Type.(主要用途)
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
                return (UsingType)type.PropertyId.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "主要用途", "UsingType", value));
        }

        /// <summary>
        /// Convert Materials Type.(主要建材)
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
                return (MaterialsType)type.PropertyId.ToInt(-1);
            }


            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "主要建材", "MaterialsType", value));
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
                return type.PropertyId == 1;
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "布林值轉換", "Had-Bool", value));
        }

        /// <summary>
        /// Convert parking Type.(車位類別)
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
                return (ParkingType)type.PropertyId.ToInt(-1);
            }

            throw new FormatException(string.Format(FORMAT_EXCEPTION_MSG, "車位類別", "ParkingType", value));
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
        /// Square Meter to Level Ground.(平方公尺 -- 坪)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static decimal CalculateSMToLevelGround(decimal value)
        {
            return (decimal)(value * 0.3025M);
        }
        #endregion
    }
}
