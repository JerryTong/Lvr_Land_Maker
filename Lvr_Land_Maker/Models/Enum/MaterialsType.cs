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
        ReinforcedQuasiSoilMix = 1,
        /// <summary>
        /// 鋼骨混凝土造
        /// Reinforced concrete intends to build soil
        /// </summary>
        SteelReinforcedQuasiSoilMix = 2,

        /// <summary>
        /// 混擬土造
        /// </summary>
        QuasiSoilMix=3,

        /// <summary>
        /// 磚造
        /// </summary>
        Brick = 4,

        /// <summary>
        /// 其他材質
        /// </summary>
        OtherBuild = 5,

     
        /// <summary>
        /// 見其他登記事項
        /// See other registration matters
        /// </summary>
        Other = 99,
    }
}
