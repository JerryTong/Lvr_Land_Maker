using Lvr_Land_Maker.BLL;
using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data;
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
        private const string NONE_LANDDATA = "區域:{0} 本期無資料 - ";
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

        public Logger LandMakerProcess(string filePath)
        {
            Logger logger = new Logger();
            
            LandFileDetailInfo LandDetail = new LandFileDetailInfo();
            LandDetail = LandMakerHelper.GetLandXmlFileDetailInfo(filePath);
            LandDetail.LandCollectionTable = LandMakerDA.GetLvrLandInfo(filePath);

            if (LandDetail == null || LandDetail.LandCollectionTable == null || LandDetail.LandCollectionTable.Rows.Count == 0)
            {
                logger.Path = filePath;
                logger.Type = LoggerType.DataException;
                logger.Message = string.Format(NONE_LANDDATA, LandDetail.CityName);
                return logger;
            }

            if (LandDetail.SaleType == SaleType.Sale || LandDetail.SaleType == SaleType.PreOrder)
            {
                ////資料轉換
                DataTable resultTable = LandMakerHelper.LvrTableToDataBaseTable(LandDetail, this.dataBaseTableFormat);
                LandMakerDA.WriteingToDatabase(resultTable);
                return new Logger(LoggerType.AppMessage,
                                    filePath,
                                    LandDetail.SaleType == SaleType.Sale ? string.Format(SALE_DATA, WRITEING, SUCCESSFUL) 
                                                                            : string.Format(PREORDER_DATA, WRITEING, SUCCESSFUL),
                                    string.Empty, 
                                    string.Empty);
            }
            else if (LandDetail.SaleType == SaleType.Leasing)
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
