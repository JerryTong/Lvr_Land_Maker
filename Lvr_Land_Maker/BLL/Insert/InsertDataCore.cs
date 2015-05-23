using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL.Insert
{
    public class InsertDataCore
    {
        public int Core(List<LandFileDetailInfo> LandDetailList, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (LandDetailList == null)
            {
                errorMsg = "The Land Detail collection is empty.";
                return -1;
            }
            
            return this.LvrLandInsertData(LandDetailList, out errorMsg);
        }

        public virtual int LvrLandInsertData(List<LandFileDetailInfo> LandDetailList, out string errorMsg)
        {
            errorMsg = "This is virtual method.";
            return -1;
        }
    }
}
