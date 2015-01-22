using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models.Enum
{
    public enum MaterialsType
    {
        Non = -1,
        /// <summary>
        /// 鋼筋混擬土造
        /// Reinforced concrete intends to build soil
        /// </summary>
        RCC = 1,
        /// <summary>
        /// 鋼骨鋼筋混凝土造
        /// </summary>
        SRCC = 2,
        /// <summary>
        /// 鋼骨混凝土造
        /// </summary>
        SCC = 3,
        /// <summary>
        /// 預力混凝土造
        /// </summary>
        PCC = 4,
        /// <summary>
        /// 土磚石混合造
        /// </summary>
        MSM = 5,
        /// <summary>
        /// 磚造
        /// </summary>
        MB = 6,
        /// <summary>
        /// 加強磚造
        /// Strengthening brick
        /// </summary>
        SB = 7,
        /// <summary>
        /// 見使用執照
        /// </summary>
        SeeLicense = 8,
        /// <summary>
        /// 見其他登記事項
        /// See other registration matters
        /// </summary>
        Other = 9,
    }
}
