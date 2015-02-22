using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lvr_Land_Maker.Extension
{
    public static class ProgressBarExtension
    {
        public static void SetValueAsyc(this ProgressBar progressBar, int value)
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.Value = value;
            }));
        }
    }
}
