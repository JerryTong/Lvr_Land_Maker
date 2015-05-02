using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class VersionTrnasfrom : TransformBase
    {
        public TransformResult Run(TransformPerporty perporty)
        {
            string errMessage = string.Empty;
            this.Transform(perporty);

            if (!string.IsNullOrEmpty(perporty.ErrorMessage))
            {
                return new TransformResult { Message = perporty.ErrorMessage };
            }

            List<LandFileDetailInfo> lvrLandDetailList = this.GetLvrLandDetailInfo(perporty.FilesName);
            Process process = new Process();
            TransformResult result = new TransformResult();
            result.ErrorData = new DataTable();
            result.ErrorData.Columns.Add("IsAdd");
            foreach (DataRow row in LandMakerHelper.CreateLandColumnDataTable().Rows)
            {
                result.ErrorData.Columns.Add(row["ColumnName"].ToString());
            }


            foreach (var detail in lvrLandDetailList)
            {
                detail.TransLvrLandTable = process.TransFormatLvrLandData(detail, out errMessage);
                if (!string.IsNullOrEmpty(errMessage))
                {
                    return new TransformResult { Message = perporty.ErrorMessage };
                }

                if ((detail.SaleType == Models.Enum.SaleType.PreOrder || detail.SaleType == Models.Enum.SaleType.Sale) && detail.TransLvrLandTable != null)
                {
                    foreach (var row in detail.TransLvrLandTable.AsEnumerable().ToList())
                    {
                        bool isAccuracy = process.CheckTransDataRowsAccuracy(row, detail.SaleType, out errMessage);
                        if (isAccuracy == false)
                        {
                            result.ErrorData.Rows.Add(row.ItemArray);
                            row.SetField<string>("IsAdd", "0");
                        }
                    }
                }
            }

            lvrLandDetailList.ForEach(land =>
            {
                process.LandInsertToDataBase(land);
            });

            result.Message = "Success";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        private List<LandFileDetailInfo> GetLvrLandDetailInfo(List<string> filePaths)
        {
            var result = new List<LandFileDetailInfo>();
            foreach (var path in filePaths)
            {
                LandFileDetailInfo landDetail = new LandFileDetailInfo();
                landDetail = LandMakerHelper.GetLandXmlFileDetailInfo(path);
                landDetail.OriginalLvrLandTable = Lvr_Land_Maker.DAL.LandMakerDA.GetLvrLandInfo(path);

                result.Add(landDetail);
            }

            return result;
        }
    }
}
