using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.BLL
{
    public class Logger
    {
        private string loggerFormat = "[{0}] || {1}";
        
        public Logger()
        {
            ////
        }

        public Logger(LoggerType type, string path, string msg, string interanlDescription, string stackTrace)
        {
            this.Type = type;
            this.Path = path;
            this.Message = msg;
            this.InternalDescription = interanlDescription;
            this.StackTrace = stackTrace;
        }

        /// <summary>
        /// Log類型.
        /// </summary>
        public LoggerType Type { get; set; }

        /// <summary>
        /// Log訊息.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 內部訊息.
        /// </summary>
        public string InternalDescription { get; set; }

        /// <summary>
        /// Exection stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 自定義All Logger message.
        /// </summary>
        public string LoggerString
        {
            get
            {
                return string.Format(this.loggerFormat, this.Type.ToString().PadRight(14), this.Message);
            }
        }
    }

    public enum LoggerType
    {
        /// <summary>
        /// 系統錯誤
        /// </summary>
        Exception,

        /// <summary>
        /// 資料集錯誤
        /// </summary>
        DataException,

        /// <summary>
        /// 應用程式信息
        /// </summary>
        AppMessage,
    }
}
