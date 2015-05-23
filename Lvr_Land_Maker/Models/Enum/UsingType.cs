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
        Business = 2,

        /// <summary>
        /// 工業用
        /// </summary>
        Industrial = 3,

        /// <summary>
        /// 辦公室
        /// </summary>
        Office = 4,

        /// <summary>
        /// 農業用
        /// </summary>
        Farm = 5, 

        /// <summary>
        /// 住商用
        /// </summary>
        HomeUseWithBusiness = 6,

        /// <summary>
        /// 工商用
        /// </summary>
        IndustryWithBusiness = 7,

        /// <summary>
        /// 住工用
        /// </summary>
        HomeUseWithIndustry = 8,

        /// <summary>
        /// 廠房
        /// </summary>
        Factory = 9,

        /// <summary>
        /// 國民住宅
        /// </summary>
        PublicHousing = 10,

        /// <summary>
        /// 停車空間
        /// </summary>
        ParkingSpace = 11,
        
        /// <summary>
        /// 見其他登記事項/見其它登記事項
        /// </summary>
        Other = 99,
        
    }
}
