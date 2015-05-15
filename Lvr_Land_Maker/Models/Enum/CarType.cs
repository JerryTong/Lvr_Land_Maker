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
        RampPlane = 1,

        /// <summary>
        /// 一樓平面 First floor flat
        /// </summary>
        FirstFloorFlat = 2,

        /// <summary>
        /// 坡道機械 Ramp machinery
        /// </summary>
        RampMachinery = 3,

        /// <summary>
        /// 塔式車位 Tower parking
        /// </summary>
        TowerParking = 4,

        /// <summary>
        /// 升降機械 Lifting machinery
        /// </summary>
        LiftingMachinery = 5,

        /// <summary>
        /// 升降平面
        /// </summary>
        LiftingPlane = 6,

        /// <summary>
        /// 其他 other
        /// </summary>
        Other = 7,
    }
}
