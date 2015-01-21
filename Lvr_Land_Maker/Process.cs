using Lvr_Land_Maker.BLL;
using Lvr_Land_Maker.DAL;
using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker
{
    public class Process
    {
        private DataTable dataBaseTableFormat = null;

        public Process()
        {
            if (dataBaseTableFormat == null)
            {
                dataBaseTableFormat = LandMakerHelper.CreateLandColumnDataTable();
            }
        }

        public void LandMakerProcess(string filePath)
        {
            LandFileDetailInfo LandDetail = new LandFileDetailInfo();
            LandDetail = LandMakerHelper.GetLandXmlFileDetailInfo(filePath);
            LandDetail.LandCollectionTable = LandMakerDA.GetLvrLandInfo(filePath);
        }
    }
}
