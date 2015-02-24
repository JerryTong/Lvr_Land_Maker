using Lvr_Land_Maker.BLL;
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

namespace Lvr_Land_Maker
{
    public class Process
    {
        private DataTable dataBaseTableFormat = null;
        private const string SUCCESSFUL = "成功";
        private const string WRITEING = "寫入";
        private const string IGNORE = "忽略";
        private const string DATA_FILE_ERROR = "非XML格式文件";
        private const string NONE_LANDDATA = "本期{0} [{1}] 無資料 - ";
        private const string SALE_DATA = "{0}-{1} [不動產] - ";
        private const string PREORDER_DATA = "{0}-{1} [預售屋] - ";
        private const string LEASING = "{0}-{1} [ 預購 ] - ";

        public Process()
        {
            if (dataBaseTableFormat == null)
            {
                dataBaseTableFormat = LandMakerHelper.CreateLandColumnDataTable();
            }
        }

        /// <summary>
        /// 確認選擇檔案皆為XML文件。
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="errorFileExtension"></param>
        /// <returns></returns>
        public bool CheckFileExtension(List<string> filePaths, out string errorFileExtension)
        {
            foreach (var path in filePaths)
            {
                if (!Path.GetExtension(path).ToUpper().Equals(".XML"))
                {
                    errorFileExtension = path;
                    return false;
                }
            }

            errorFileExtension = string.Empty;
            return true;
        }

        /// <summary>
        /// 取得實價登錄資料。
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public List<LandFileDetailInfo> GetLvrLandDetailInfo(List<string> filePaths)
        {
            var result = new List<LandFileDetailInfo>();
            foreach (var path in filePaths)
            {
                LandFileDetailInfo landDetail = new LandFileDetailInfo();
                landDetail = LandMakerHelper.GetLandXmlFileDetailInfo(path);
                landDetail.OriginalLvrLandTable = LandMakerDA.GetLvrLandInfo(path);

                result.Add(landDetail);
            }

            return result;
        }

        /// <summary>
        /// 確認實價登錄欄位屬性皆為已知。d
        /// </summary>
        /// <returns></returns>
        public DataTable TransFormatLvrLandData(LandFileDetailInfo landDetailInfo, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (landDetailInfo == null)
            {
                return null;
            }

            try
            {
                if (landDetailInfo.SaleType == SaleType.Sale || landDetailInfo.SaleType == SaleType.PreOrder)
                {
                    DataTable resultTable = LandMakerHelper.LvrTableToDataBaseTable(landDetailInfo, this.dataBaseTableFormat);
                    return resultTable;
                }

                return null;
            }
            catch (FormatException formatEx)
            {
                errorMsg = formatEx.Message;
                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message + "\n" + ex.StackTrace;
                return null;
            }
        }

        /// <summary>
        /// 將資料寫入資料庫。
        /// </summary>
        /// <param name="landDetail"></param>
        /// <returns></returns>
        public Logger LandInsertToDataBase(LandFileDetailInfo landDetail)
        {
            Logger loggerResult = null;

            if (landDetail.SaleType == SaleType.Sale || landDetail.SaleType == SaleType.PreOrder)
            {
                if (landDetail.TransLvrLandTable != null)
                {
                    LandMakerDA.WriteingToDatabase(landDetail.TransLvrLandTable);

                    loggerResult = new Logger
                    {
                        Type = LoggerType.AppMessage,
                        Path = landDetail.FileName,
                        Message = landDetail.SaleType == SaleType.Sale ? string.Format(SALE_DATA, WRITEING, SUCCESSFUL)
                                                                                : string.Format(PREORDER_DATA, WRITEING, SUCCESSFUL),
                        InternalDescription = string.Empty,
                        StackTrace = string.Empty,
                    };
                }
                else
                {
                    ////無資料情況
                    loggerResult = new Logger
                    {
                        Type = LoggerType.DataException,
                        Path = landDetail.FileName,
                        Message = landDetail.SaleType == SaleType.Sale ? string.Format(NONE_LANDDATA, landDetail.CityName, "買賣")
                                                                                : string.Format(NONE_LANDDATA, landDetail.CityName, "預售"),
                        InternalDescription = string.Empty,
                        StackTrace = string.Empty,
                    };
                }
            }
            else if (landDetail.SaleType == SaleType.Leasing)
            {
                loggerResult = new Logger
                {
                    Type = LoggerType.DataException,
                    Path = landDetail.FileName,
                    Message = string.Format(LEASING, IGNORE, WRITEING),
                    InternalDescription = string.Empty,
                    StackTrace = string.Empty,
                };
            }

            return loggerResult;
        }
    }
}
