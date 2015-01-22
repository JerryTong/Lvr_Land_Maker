using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum PartitionType
    {
        Non = -1,
        /// <summary>
        /// 住
        /// </summary>
        Residential = 1,
        /// <summary>
        /// 商
        /// </summary>
        Business = 2,
        /// <summary>
        /// 工
        /// </summary>
        Industrial = 3,
        /// <summary>
        /// 農
        /// </summary>
        Agricultural = 4,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 5
    }
}
