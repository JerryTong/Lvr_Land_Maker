using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lvr_Land_Maker.Extension
{
    public static class LabelExtension
    {
        /// <summary>
        /// Async
        /// </summary>
        /// <param name="label"></param>
        /// <param name="message"></param>
        public static void AsyncText(this Label label, string message)
        {
            label.Invoke(new Action(() =>
            {
                label.Text = message;
            }));
        }
    }
}
