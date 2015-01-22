using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum SubjectType
    {
        Non = -1,
        /// <summary>
        /// 建物
        /// </summary>
        Buildings = 1,
        /// <summary>
        /// 土地
        /// </summary>
        Land = 2,
        /// <summary>
        /// 車位
        /// </summary>
        Parking = 3,
        
        /// <summary>
        /// 房地(土地+建物)
        /// </summary>
        BuildingsLand = 4,

        /// <summary>
        /// 房地(土地+建物)+車位
        /// </summary>
        BuildingsLandParking = 5
    }
}
