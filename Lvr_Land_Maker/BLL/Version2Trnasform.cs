using Lvr_Land_Maker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class Version2Trnasform : TransformBase
    {
        public TransformResult Run(TransformPerporty perporty)
        {
            string errMessage = string.Empty;
            this.Transform(perporty);

            if (!string.IsNullOrEmpty(perporty.ErrorMessage))
            {
                return new TransformResult { Message = perporty.ErrorMessage };
            }



            return null;
        }

        private void GroupFilePath(List<string> filesPath)
        {
            IEnumerable<string> mainFile = filesPath.Where(f => f.Count(s => s == '_') == 3);

        }

        private bool ValidatorFile(List<string> filesPath)
        {
            ////A_LVR_LAND_A
            return false;
        }

    }
}
