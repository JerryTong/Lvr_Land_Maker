using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum UsingType
    {
        Non = -1,
        /// <summary>
        /// 住家用
        /// </summary>
        HomeUse = 1,
        /// <summary>
        /// 商業用
        /// </summary>
        Commercial = 2,
        /// <summary>
        /// 國民住宅
        /// </summary>
        NationalHouse = 3,
        /// <summary>
        /// 見使用執照
        /// </summary>
        SeeUseLicense = 4,
        /// <summary>
        /// 停車空間
        /// </summary>
        ParkingSpace = 5,
        /// <summary>
        /// 工業用
        /// </summary>
        Industrial = 6,
        /// <summary>
        /// 住商用
        /// </summary>
        HomeUseCommercial = 7,
        /// <summary>
        /// 見其他登記事項
        /// </summary>
        Other = 8,
        
    }
}
