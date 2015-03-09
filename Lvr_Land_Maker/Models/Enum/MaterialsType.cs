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
        /// 鋼筋混凝土加強磚造
        /// Reinforced concrete intends to build soil
        /// </summary>
        RCCSB = 2,
        /// <summary>
        /// 鋼骨鋼筋混凝土造
        /// </summary>
        SRCC = 3,
        /// <summary>
        /// 鋼骨混凝土造
        /// </summary>
        SCC = 4,
        /// <summary>
        /// 預力混凝土造
        /// </summary>
        PCC = 5,
        /// <summary>
        /// 土磚石混合造
        /// </summary>
        MSM = 6,
        /// <summary>
        /// 磚造
        /// </summary>
        MB = 7,
        /// <summary>
        /// 加強磚造
        /// Strengthening brick
        /// </summary>
        SB = 8,
        /// <summary>
        /// 見使用執照
        /// </summary>
        SeeLicense = 9,
        /// <summary>
        /// 土造
        /// </summary>
        SoilMade = 10,
        /// <summary>
        /// 木造
        /// </summary>
        Wooden = 11,
        /// <summary>
        /// 鋼造
        /// </summary>
        Steel = 12,
        /// <summary>
        /// 鐵造
        /// </summary>
        Foundry = 13,
        /// <summary>
        /// 石造
        /// </summary>
        Stone = 14,
        /// <summary>
        /// 竹造
        /// </summary>
        Bamboo = 15,
        /// <summary>
        /// 土木造
        /// </summary>
        Soil_Wooden = 16,
        /// <summary>
        /// 土石造
        /// </summary>
        Soil_Stone = 17,
        /// <summary>
        /// 壁式預鑄鋼筋混凝土造
        /// </summary>
        RRCCSB = 18,
        /// <summary>
        /// 見其他登記事項
        /// See other registration matters
        /// </summary>
        Other = 19,
    }
}
