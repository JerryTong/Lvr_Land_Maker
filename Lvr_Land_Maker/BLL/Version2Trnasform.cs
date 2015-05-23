using Lvr_Land_Maker.BLL.Insert;
using Lvr_Land_Maker.BLL.Transform;
using Lvr_Land_Maker.BLL.Validate;
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
    public class Version2Trnasform : TransformBase
    {
        public TransformResult NewRun(TransformProperty property)
        {
            string errorMsg = string.Empty;
            if (!new ValidateV2().Core(property, out errorMsg))
            {
                return new TransformResult { Message = property.ErrorMessage };
            }

            List<LandFileDetailInfo> LandDetailList = new TransformV2().Core(property, out errorMsg);
            if (LandDetailList == null)
            {
                return new TransformResult { Message = property.ErrorMessage };
            }

            int state = new InsertDataV2().Core(LandDetailList, out errorMsg);
            if (state == -1)
            {
                return new TransformResult { Message = property.ErrorMessage };
            }


            return null;
        }

        public TransformResult Run(TransformProperty perporty)
        {
            string errMessage = string.Empty;
            this.Transform(perporty);

            if (!string.IsNullOrEmpty(perporty.ErrorMessage))
            {
                return new TransformResult { Message = perporty.ErrorMessage };
            }

            IEnumerable<string> mainFile = perporty.FilesName.Where(f => f.Count(s => s == '_') == 3);
            Dictionary<int, FilesDict> filesDict = this.TransToFileDict(perporty.FilesName, mainFile, out errMessage);
            if (!string.IsNullOrEmpty(errMessage))
            {
                return new TransformResult { Message = perporty.ErrorMessage };
            }

            List<LandFileDetailInfo> lvrLandDetailList = this.GetLvrLandDetailInfo(filesDict);
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
        public override List<LandFileDetailInfo> GetLvrLandDetailInfo(Dictionary<int, FilesDict> fileDict)
        {
            var result = new List<LandFileDetailInfo>();
            foreach (KeyValuePair<int, FilesDict> dict in fileDict)
            {
                LandFileDetailInfo landDetail = new LandFileDetailInfo();
                landDetail = LandMakerHelper.GetLandXmlFileDetailInfo(dict.Value.ParentFile);

                #region Version 2.0 
                landDetail.LandFileName = dict.Value.LandFile;
                landDetail.ParkFileName = dict.Value.ParkFile;
                landDetail.OriginalLandTable = Lvr_Land_Maker.DAL.LandMakerDA.GetLvrLandInfo(landDetail.LandFileName);
                landDetail.OriginalLvrLandTable = Lvr_Land_Maker.DAL.LandMakerDA.GetLvrLandInfo(landDetail.ParkFileName);

                if (landDetail.SaleType != Models.Enum.SaleType.PreOrder)
                {
                    landDetail.BuildFileName = dict.Value.BuildFile ;
                    landDetail.OriginalBuildTable = Lvr_Land_Maker.DAL.LandMakerDA.GetLvrLandInfo(landDetail.BuildFileName);
                }
                #endregion


                landDetail.OriginalLvrLandTable = Lvr_Land_Maker.DAL.LandMakerDA.GetLvrLandInfo(dict.Value.ParentFile);
                result.Add(landDetail);
            }

            return result;
        }

        private Dictionary<int, FilesDict> TransToFileDict(List<string> filesPath, IEnumerable<string> mainFiles, out string errMessage)
        {
            Dictionary<int, FilesDict> resultDict = new Dictionary<int, FilesDict>();

            errMessage = string.Empty;
            bool isAccuracy = false;
            int index = 0;

            foreach (var main in mainFiles)
            {
                var token = Path.GetFileNameWithoutExtension(main).Split('_');
                if (token.Length != 4)
                {
                    errMessage = string.Format("檔案:{0}, 檔案名稱未如預期。", main);
                    return null;
                }

                var aboutPaths = filesPath.Where(f => f.IndexOf(Path.GetFileNameWithoutExtension(main)) > 0);
                FilesDict dict = new FilesDict();
                dict.ParentFile = main;
                dict.BuildFile = aboutPaths.Where(p => p.EndsWith("_BUILD.XML")).FirstOrDefault();
                dict.LandFile = aboutPaths.Where(p => p.EndsWith("_LAND.XML")).FirstOrDefault();
                dict.ParkFile = aboutPaths.Where(p => p.EndsWith("_PARK.XML")).FirstOrDefault(); 

                switch (token[3])
                {
                    case "A":
                        ////Build, Land, Park
                        isAccuracy = !string.IsNullOrEmpty(dict.ParentFile) || !string.IsNullOrEmpty(dict.BuildFile) || !string.IsNullOrEmpty(dict.LandFile) || !string.IsNullOrEmpty(dict.ParkFile);
                        break;
                    case "B":
                        ////Land, Park
                        isAccuracy = !string.IsNullOrEmpty(dict.ParentFile) || !string.IsNullOrEmpty(dict.LandFile) || !string.IsNullOrEmpty(dict.ParkFile);
                        break;
                    case "C":
                        ////Build, Land, Park
                        isAccuracy = !string.IsNullOrEmpty(dict.ParentFile) || !string.IsNullOrEmpty(dict.BuildFile) || !string.IsNullOrEmpty(dict.LandFile) || !string.IsNullOrEmpty(dict.ParkFile);
                        break;
                }

                if (isAccuracy == false)
                {
                    errMessage = string.Format("主資料:{0}, 找不到其他附加相關檔案{1}。", main, token[3] == "B" ? "Land, Park" : "Build, Land, Park");
                    return null;
                }


                resultDict.Add(index, dict);
                index++;
            }

            return resultDict;
        }
    }

    public class FilesDict
    {
        public SaleType SaleType { get; set; }

        public string ParentFile { get; set; }

        public string BuildFile { get; set; }

        public string LandFile { get; set; }

        public string ParkFile { get; set; }
    }
}
