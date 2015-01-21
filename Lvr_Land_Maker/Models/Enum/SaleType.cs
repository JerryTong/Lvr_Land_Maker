using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum SaleType
    {
        none = -1,

        /// <summary>
        /// 買賣
        /// </summary>
        Sale = 0,

        /// <summary>
        /// 預售
        /// </summary>
        PreOrder = 1,

        /// <summary>
        /// 租賃
        /// </summary>
        Leasing = 2
    }
}
