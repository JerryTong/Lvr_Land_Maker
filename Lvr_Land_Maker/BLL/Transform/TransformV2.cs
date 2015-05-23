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
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public override List<LandFileDetailInfo> LvrLandTransfrom(TransformProperty property, List<LandFileDetailInfo> result, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (result == null)
            {
                return null;
            }

            foreach (var detail in result)
            {
                detail.BusinessModel = LandMakerDA.GetBusinessModel(detail.ParnetFileName);
                foreach (var businessModel in detail.BusinessModel)
                {
                    ////Init business model.
                    businessModel.Init(detail);
                }
            }

            return result;
        }
    }
}
