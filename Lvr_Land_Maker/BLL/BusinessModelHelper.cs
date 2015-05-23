using Lvr_Land_Maker.Models;
using FrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public static class BusinessModelHelper
    {
        public static BusinessModel Init(this BusinessModel model, LandFileDetailInfo detail)
        {
            model.City = detail.CityCode;
            model.CityCode = detail.CityCode;
            model.CityName = detail.CityName;
            model.ZipCode = detail.ZipCode;

            model.CompletedDate = ConvertCompleteDateTime(model.InternalCompletedDate);
            model.TradeDate = ConvertTradeDateTime(model.InternalTradeDate);
            model.TradeYear = ConvertTradeYear(model.InternalTradeDate);
        

            model.SubjectCode = -1;

            model.PartitionCode = -1;

            model.LandLevelGround = -1;

            model.BuildsTypeCode = -1;

            model.BuildsLevelGround = -1;

            model.LevelGroundCost = -1;

            model.CarTypeCode = -1;

            model.CarLevelGround = -1;

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
    }
}
