using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum ParkingType
    {
        Non = -1,

        /// <summary>
        /// 坡道平面 Ramp plane
        /// </summary>
        Rp = 1,

        /// <summary>
        /// 一樓平面 First floor flat
        /// </summary>
        Ff = 2,

        /// <summary>
        /// 坡道機械 Ramp machinery
        /// </summary>
        Rm = 3,

        /// <summary>
        /// 塔式車位 Tower parking
        /// </summary>
        Tp = 4,

        /// <summary>
        /// 升降機械 Lifting machinery
        /// </summary>
        Lm = 5,

        /// <summary>
        /// 升降平面
        /// </summary>
        LP = 6,

        /// <summary>
        /// 其他 other
        /// </summary>
        Other = 7,
    }
}
