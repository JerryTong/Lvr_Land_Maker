using Lvr_Land_Maker.BLL;
using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using Lvr_Land_Maker.Models.Configuartion;
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
        public bool CheckFileExtension(List<string> filePaths, out string errorMsg)
        {
            foreach (var path in filePaths)
            {
                if (this.CheckFileExtension(path, out errorMsg))
                {
                    return false;
                }
            }

            errorMsg = string.Empty;
            return true;
        }

        /// <summary>
        /// 確認選擇檔案皆為XML文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool CheckFileExtension(string filePath, out string errorMsg)
        {
            if (!Path.GetExtension(filePath).ToUpper().Equals(".XML"))
            {
                errorMsg = string.Format("{0} 非XML格式檔案。", filePath);
                return false;
            }

            errorMsg = string.Empty;
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
        /// 確認實價登錄欄位屬性皆為已知。
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
                ////預售、中古
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
        /// 檢查實價登錄欄位資料正確性。
        /// </summary>
        /// <param name="transDataRow"></param>
        /// <param name="saleType"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public bool CheckTransDataRowsAccuracy(DataRow transDataRow, SaleType saleType, out string errMessage)
        {
            errMessage = string.Empty;
            if (transDataRow == null)
            {
                errMessage = "轉換資料為空";
                return false;
            }

            List<Column> columns = null;
            if (saleType == SaleType.Sale || saleType == SaleType.PreOrder)
            {
                columns = CheckDataConfigManager.Current.SaleAndPreorder.columns;
            }

            if (columns == null || columns.Count == 0)
            {
                return true;
            }

            foreach (var col in columns)
            {
                ////允許為空且不檢查長度則跳過
                if (col.IsAllowNull && !col.IsCheckLen)
                {
                    continue;
                }

                if (col.IsCheckLen)
                {
                    var isAccuracy = this.CheckDataLength(transDataRow[col.Name].ToString(), col.LenMax, col.LenMin);
                    if (!isAccuracy)
                    {
                        errMessage = string.Format("長度不合規定。\n錯誤欄位:{0}; 值:{1}\n地址:{2}\n總價元{3}"
                                                        , col.Name
                                                        , transDataRow[col.Name].ToString()
                                                        , transDataRow["Address"].ToString()
                                                        , transDataRow["Cost"].ToString());
                            
                        return false;
                    }
                }

                if (!col.IsAllowNull)
                {
                    if (string.IsNullOrEmpty(transDataRow[col.Name].ToString()))
                    {
                        errMessage = string.Format("資料不可為空。\n錯誤欄位:{0}; 值:{1}\n地址:{2}\n總價元{3}"
                            , col.Name
                            , transDataRow[col.Name].ToString()
                            , transDataRow["Address"].ToString()
                            , transDataRow["Cost"].ToString());

                        return false;
                    }
                }
               

            }

            return true;
        }

        /// <summary>
        /// 檢查字串長度。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lenMax"></param>
        /// <param name="lenMin"></param>
        /// <returns></returns>
        private bool CheckDataLength(string value, int lenMax, int lenMin)
        {
            int valueLen = value.Length;
            if (!(valueLen >= lenMin && valueLen <= (lenMax / 2)))
            {
                return false;
            }

            return true;
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
                    LandMakerDA.WriteingToDatabase(landDetail.TransLvrLandTable.Select("IsAdd=1").CopyToDataTable());

                    loggerResult = new Logger
                    {
                        Type = LoggerType.AppMessage,
                        Path = landDetail.ParentFileName,
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
                        Path = landDetail.ParentFileName,
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
                    Path = landDetail.ParentFileName,
                    Message = string.Format(LEASING, IGNORE, WRITEING),
                    InternalDescription = string.Empty,
                    StackTrace = string.Empty,
                };
            }

            return loggerResult;
        }
    }
}
