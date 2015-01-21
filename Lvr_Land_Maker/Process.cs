using Lvr_Land_Maker.BLL;
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
        private DataTable DataBaseTableFormat = null;

        public Process()
        {
            if (DataBaseTableFormat == null)
            {
                DataBaseTableFormat = LandMakerHelper.CreateLandColumnDataTable();
            }

            
        }

        public void LandMakerProcess(string filesPath)
        {

        }
    }
}
