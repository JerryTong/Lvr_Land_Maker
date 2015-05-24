using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL.Insert
{
    public class InsertDataV2 : InsertDataCore
    {
        public override int LvrLandInsertData(List<LandFileDetailInfo> LandDetailList, out string errorMsg)
        {
            errorMsg = string.Empty;
            foreach (var detail in LandDetailList)
            {
                switch (detail.SaleType)
                {
                    case Models.Enum.SaleType.Sale:
                    case Models.Enum.SaleType.PreOrder:
                        ////dt = FrameworkLibrary.DataAccess.Helper.DataTableHelper.ConvertToDataTable<BusinessModel>(detail.BusinessModel);
                        this.InsertParentFile(detail, out errorMsg);
                        break;
                    case Models.Enum.SaleType.Leasing:
                        break;
                }
            }

            return 0;
        }

        public int InsertParentFile(LandFileDetailInfo detail, out string errorMsg)
        {
            errorMsg = string.Empty;
            int state = -1;
            if (detail.BusinessModel != null)
            {
                state = this.InsertBusinessModel(detail.BusinessModel);

            }

            return -1;
        }

        private int InsertBusinessModel(List<BusinessModel> model)
        {
            DataTable source = FrameworkLibrary.DataAccess.Helper.DataTableHelper.ConvertToDataTable<BusinessModel>(model);

            if (source == null)
            {
                return -1;
            }

            using (SqlConnection conn = new SqlConnection())
            {
                //conn.ConnectionString = @"server=TON-PC\SQLEXPRESS;database=LvrLand;uid=mtap;pwd=ton@1234";
                conn.ConnectionString = @"server=TON-PC\SQLEXPRESS;database=LvrLand;uid=tong;pwd=123456";
                conn.Open();
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
                {
                    //設定一個批次量寫入多少筆資料 
                    sqlBC.BatchSize = 1000;
                    //設定逾時的秒數 
                    sqlBC.BulkCopyTimeout = 60;

                    //設定 NotifyAfter 屬性，以便在每複製 10000 個資料列至資料表後，呼叫事件處理常式。 
                    sqlBC.NotifyAfter = 10000;
                    sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);

                    //設定要寫入的資料庫 
                    sqlBC.DestinationTableName = "dbo.LvrLandOriginalBaseTest";

                    //對應資料行 
                    sqlBC.ColumnMappings.Add("ObjectNumber", "ObjectNumber");
                    sqlBC.ColumnMappings.Add("SaleType", "SaleType");
                    sqlBC.ColumnMappings.Add("City", "City");
                    sqlBC.ColumnMappings.Add("CityCode", "CityCode");
                    sqlBC.ColumnMappings.Add("CityName", "CityName");
                    sqlBC.ColumnMappings.Add("ZipCode", "ZipCode");
                    sqlBC.ColumnMappings.Add("Townships", "Townships");
                    sqlBC.ColumnMappings.Add("Address", "Address");
                    sqlBC.ColumnMappings.Add("Subject", "Subject");
                    sqlBC.ColumnMappings.Add("SubjectCode", "SubjectCode");
                    sqlBC.ColumnMappings.Add("Partition", "Partition");
                    sqlBC.ColumnMappings.Add("PartitionCode", "PartitionCode");
                    sqlBC.ColumnMappings.Add("Non_Partition", "Non_Partition");
                    sqlBC.ColumnMappings.Add("Non_Scheduled", "Non_Scheduled");
                    sqlBC.ColumnMappings.Add("LandSquareMeter", "LandSquareMeter");
                    sqlBC.ColumnMappings.Add("LandLevelGround", "LandLevelGround");
                    sqlBC.ColumnMappings.Add("TradeDate", "TradeDate");
                    sqlBC.ColumnMappings.Add("TradeYear", "TradeYear");
                    sqlBC.ColumnMappings.Add("AllFloors", "AllFloors");
                    sqlBC.ColumnMappings.Add("TradeFloors", "TradeFloors");
                    sqlBC.ColumnMappings.Add("BuildsType", "BuildsType");
                    sqlBC.ColumnMappings.Add("BuildsTypeCode", "BuildsTypeCode");
                    sqlBC.ColumnMappings.Add("Using", "Using");
                    sqlBC.ColumnMappings.Add("UsingCode", "UsingCode");
                    sqlBC.ColumnMappings.Add("Materials", "Materials");
                    sqlBC.ColumnMappings.Add("MaterialsCode", "MaterialsCode");
                    sqlBC.ColumnMappings.Add("CompletedDate", "CompletedDate");
                    sqlBC.ColumnMappings.Add("BuildsSquareMeter", "BuildsSquareMeter");
                    sqlBC.ColumnMappings.Add("BuildsLevelGround", "BuildsLevelGround");
                    sqlBC.ColumnMappings.Add("Room", "Room");
                    sqlBC.ColumnMappings.Add("Livingroom", "Livingroom");
                    sqlBC.ColumnMappings.Add("Bathroom", "Bathroom");
                    sqlBC.ColumnMappings.Add("Compartment", "Compartment");
                    sqlBC.ColumnMappings.Add("Management", "Management");
                    sqlBC.ColumnMappings.Add("Cost", "Cost");
                    sqlBC.ColumnMappings.Add("SquareMeterCost", "SquareMeterCost");
                    sqlBC.ColumnMappings.Add("LevelGroundCost", "LevelGroundCost");
                    sqlBC.ColumnMappings.Add("CarParkType", "CarType");
                    sqlBC.ColumnMappings.Add("CarTypeCode", "CarTypeCode");
                    sqlBC.ColumnMappings.Add("CarParkSquareMeter", "CarSquareMeter");
                    sqlBC.ColumnMappings.Add("CarParkLevelGround", "CarLevelGround");
                    sqlBC.ColumnMappings.Add("CarParkCost", "CarCost");
                    sqlBC.ColumnMappings.Add("Remark", "Remark");
                    sqlBC.ColumnMappings.Add("EditTime", "EditTime");

                    //開始寫入 
                    sqlBC.WriteToServer(source);
                }
            }

            return source.Rows.Count;
        }

        private static void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            ////
        }


    }
}
