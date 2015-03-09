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
        public static void SetMiniValueSync(this ProgressBar progressBar, int value)
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.Maximum = value;
            }));
        }
    
        public static void SetValueSync(this ProgressBar progressBar, int value)
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.Value = value;
            }));
        }
    }
}
