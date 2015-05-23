using Lvr_Land_Maker.Models;
using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL.Validate
{
    /// <summary>
    /// 使用於2015-02-01後版本
    /// </summary>
    public class ValidateV2 : ValidateCore
    {
        public override bool Validator(TransformProperty property, out string errorMsg)
        {
            errorMsg = string.Empty;
            bool baseValidate = this.ValidateByFileGroup(property, property.FilesName, out errorMsg);
        
            return baseValidate;
        }

        /// <summary>
        /// 驗證檔案群組
        /// </summary>
        /// <param name="property"></param>
        /// <param name="original"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool ValidateByFileGroup(TransformProperty property, List<string> original, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (original == null || original.Count == 0)
            {
                errorMsg = "The file collection is empty.";
                return false;
            }

            ////Example: A_Lvr_Land_A   
            var parentFile = original.Where(f => f.Count(s => s == '_') == 3);
            if (parentFile == null || parentFile.Count() == 0)
            {
                errorMsg = "Not anything parent file.";
                return false;
            }


            property.FilesDirectory = new List<FilesDict>();

            foreach (var parentName in parentFile)
            {
                ////A_Lvr_Land_A 
                var token = Path.GetFileNameWithoutExtension(parentName).Split('_');
                if (token.Length != 4)
                {
                    errorMsg = string.Format("檔案:{0}, 檔案名稱未如預期。", parentName);
                    return false;
                }

                FilesDict dict = this.BuildFilesDict(parentName, token[3], original);
        
                bool isAccuracy = this.ValidateFileDict(dict, token[3]);
                if (isAccuracy == true)
                {
                    property.FilesDirectory.Add(dict);
                    ////errorMsg = string.Format("主資料:{0}, 找不到其他附加相關檔案{1}。", parentName, token[3] == "B" ? "Land, Park" : "Build, Land, Park");
                    ////return false;
                }
            }


            return true;
        }

        /// <summary>
        /// 建立FilesDiect個體
        /// </summary>
        /// <param name="parentFile">父檔案</param>
        /// <param name="parentToken">檔案類型代號(A or B or C)</param>
        /// <param name="groupFiles"></param>
        /// <returns></returns>
        private FilesDict BuildFilesDict(string parentFile, string parentToken, IEnumerable<string> groupFiles)
        {
            ////找尋group child, ex: A_LVR_LAND_A_BUILD、A_LVR_LAND_A_LAND、A_LVR_LAND_A_PARK
            var aboutPaths = groupFiles.Where(f => f.IndexOf(Path.GetFileNameWithoutExtension(parentFile)) > 0);
            FilesDict dict = new FilesDict();

            dict.SaleType = LandMakerHelper.GetSaleType(parentToken);
            dict.ParentFile = parentFile;
            dict.BuildFile = aboutPaths.Where(p => p.EndsWith("_BUILD.XML")).FirstOrDefault();
            dict.LandFile = aboutPaths.Where(p => p.EndsWith("_LAND.XML")).FirstOrDefault();
            dict.ParkFile = aboutPaths.Where(p => p.EndsWith("_PARK.XML")).FirstOrDefault();

            return dict;
        }

        /// <summary>
        /// 驗證檔案群組是否齊全
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="caseSwitch"></param>
        /// <returns></returns>
        private bool ValidateFileDict(FilesDict dict, string caseSwitch = "")
        {
            if (dict == null || string.IsNullOrEmpty(dict.ParentFile))
            {
                return false;
            }

            if (string.IsNullOrEmpty(caseSwitch))
            {
                caseSwitch = dict.ParentFile.Split('_')[3];
            }

            bool isAccuracy = false;
            switch (caseSwitch)
            {
                case "A":
                case "C":
                    ////Build, Land, Park
                    isAccuracy = !string.IsNullOrEmpty(dict.ParentFile) && !string.IsNullOrEmpty(dict.BuildFile) && !string.IsNullOrEmpty(dict.LandFile) && !string.IsNullOrEmpty(dict.ParkFile);
                    break;
                case "B":
                    ////Land, Park
                    isAccuracy = !string.IsNullOrEmpty(dict.ParentFile) && !string.IsNullOrEmpty(dict.LandFile) && !string.IsNullOrEmpty(dict.ParkFile);
                    break;
                default:
                    isAccuracy = false;
                    break;
            }

            return isAccuracy;
        }
    }
}
