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
        private const string NONE_LANDDATA = "{0} 本期 {1} 無資料 - ";
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
        /// 主方法。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Logger LandMakerProcess(string filePath)
        {
            Logger logger = new Logger();

            if (!Path.GetExtension(filePath).Equals("xml"))
            {
                logger.Path = filePath;
                logger.Type = LoggerType.DataException;
                logger.Message = string.Format(DATA_FILE_ERROR);
                return logger;
            }

            LandFileDetailInfo landDetail = new LandFileDetailInfo();
            landDetail = LandMakerHelper.GetLandXmlFileDetailInfo(filePath);
            landDetail.LandCollectionTable = LandMakerDA.GetLvrLandInfo(filePath);

            if (landDetail == null || landDetail.LandCollectionTable == null || landDetail.LandCollectionTable.Rows.Count == 0)
            {
                logger.Path = filePath;
                logger.Type = LoggerType.DataException;
                logger.Message = string.Format(NONE_LANDDATA, landDetail.CityName, landDetail.SaleType);
                return logger;
            }

            if (landDetail.SaleType == SaleType.Sale || landDetail.SaleType == SaleType.PreOrder)
            {
                ////資料轉換
                DataTable resultTable = LandMakerHelper.LvrTableToDataBaseTable(landDetail, this.dataBaseTableFormat);
                LandMakerDA.WriteingToDatabase(resultTable);
                return new Logger(LoggerType.AppMessage,
                                    filePath,
                                    landDetail.SaleType == SaleType.Sale ? string.Format(SALE_DATA, WRITEING, SUCCESSFUL) 
                                                                            : string.Format(PREORDER_DATA, WRITEING, SUCCESSFUL),
                                    string.Empty, 
                                    string.Empty);
            }
            else if (landDetail.SaleType == SaleType.Leasing)
            {
                return new Logger(LoggerType.DataException,
                                    filePath,
                                    string.Format(LEASING, IGNORE, WRITEING), 
                                    string.Empty, 
                                    string.Empty);
            }

            return logger;
        }
    }
}
