using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class TransformBase: ITransformBase
    {
        public void Transform(TransformProperty transformPerporty)
        {
            if (transformPerporty.FilesName == null || transformPerporty.FilesName.Count == 0)
            {
                transformPerporty.ErrorMessage = "Not any files was select";
            }

            foreach (var filePath in transformPerporty.FilesName)
            {
                if (filePath.StartsWith("_") && !Path.GetExtension(filePath).ToUpper().Equals(".XML"))
                {
                    transformPerporty.ErrorMessage = string.Format("{0} 非XML格式檔案。", filePath);
                    break;
                }
            }
        }

        public virtual List<LandFileDetailInfo> GetLvrLandDetailInfo(List<string> filesName)
        {
            return null;
        }

        public virtual List<LandFileDetailInfo> GetLvrLandDetailInfo(Dictionary<int, FilesDict> filesName)
        {
            return null;
        }
    }
}
