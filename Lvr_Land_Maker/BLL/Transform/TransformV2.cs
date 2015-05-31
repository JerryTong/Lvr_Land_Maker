using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL.Transform
{
    /// <summary>
    /// 使用於2015-02-01後版本
    /// </summary>
    public class TransformV2 : TransformCore
    {
        /// <summary>
        /// 使用於2015-02-01後版本
        /// </summary>
        /// <param name="property"></param>
        /// <param name="result"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public override List<LandFileDetailInfo> LvrLandTransfrom(TransformProperty property, List<LandFileDetailInfo> result, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (result == null)
            {
                errorMsg = "LvrLand Detail is null or empty";
                return null;
            }

            foreach (var detail in result)
            {
                detail.BusinessModel = LandMakerDA.GetBusinessModel(detail.ParentFileName);
                detail.BuildModel = string.IsNullOrEmpty(detail.BuildFileName) ? null : LandMakerDA.GetLvrBuildModel(detail.BuildFileName).Where(b => !string.IsNullOrEmpty(b.Number)).ToList();
                detail.LandModel = string.IsNullOrEmpty(detail.LandFileName) ? null : LandMakerDA.GetLvrLandModel(detail.LandFileName).Where(b => !string.IsNullOrEmpty(b.Number)).ToList();
                detail.ParkModel = string.IsNullOrEmpty(detail.ParkFileName) ? null : LandMakerDA.GetLvrParkModel(detail.ParkFileName).Where(b => !string.IsNullOrEmpty(b.Number)).ToList();

                foreach (var businessModel in detail.BusinessModel)
                {
                    try
                    {
                        ////Init business model.
                        businessModel.Init(detail);
                    }
                    catch (Exception ex)
                    {
                        ////擷取Init例外
                        errorMsg += ex.Message + "\n";
                    }
                }
            }

            return result;
        }
    }
}
