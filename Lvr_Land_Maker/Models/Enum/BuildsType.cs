﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum BuildsType
    {
        Non = -1,
        /// <summary>
        /// 透天厝
        /// </summary>
        Detached = 1,
        /// <summary>
        /// 套房(1房1廳1衛)
        /// </summary>
        Suite = 2,
        /// <summary>
        /// 公寓(5樓含以下無電梯)
        /// </summary>
        Apartment = 3,
        /// <summary>
        /// 華廈(10層含以下有電梯)
        /// </summary>
        Building_10 = 4,
        /// <summary>
        /// 住宅大樓(11層含以上有電梯)
        /// </summary>
        LargeBuilding = 5,
        /// <summary>
        /// 店面(店鋪)
        /// </summary>
        Store = 6,
        /// <summary>
        /// 辦公商業大樓
        /// </summary>
        BusinessBuilding = 7,
        /// <summary>
        /// 工廠
        /// </summary>
        Factory = 8,
        /// <summary>
        /// 廠辦
        /// </summary>
        FactoryOffice = 9,
        /// <summary>
        /// 農舍
        /// </summary>
        Farmhouse = 10,
        /// <summary>
        /// 倉庫
        /// </summary>
        Warehouse = 11,
        /// <summary>
        /// other
        /// </summary>
        Other = 12
    }
}
