using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class LoggerBL
    {
        public static void WriteLog(string message)
        {
            using (StreamWriter w = File.AppendText(string.Format(@"Log\log_{0}.txt",DateTime.Now.ToString("yyyy-MM-dd"))))
            {
                w.WriteLine("<!------------------------------->");
                w.WriteLine("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                w.WriteLine("{0}", message);
                w.WriteLine("<!------------------------------->");
                w.WriteLine("");
                w.WriteLine("");
            }           
        }
    }
}
