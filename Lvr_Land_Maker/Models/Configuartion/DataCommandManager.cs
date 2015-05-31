using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lvr_Land_Maker.Models.Configuartion
{
    public class DataCommandManager
    {
        private static DataCommandConifg config = null;

        public static DataCommandConifg Current
        {
            get
            {
                if (config == null)
                {
                    config = new DataCommandConifg();
                }

                return config;
            }
        }
    }

    public class DataCommandConifg
    {
        private Dictionary<string, DataCommandSetting> m_command = null;
        public Dictionary<string, DataCommandSetting> Commands
        {
            get
            {
                return this.m_command;
            }
        }

        public DataCommandConifg()
        {
            m_command = new Dictionary<string, DataCommandSetting>();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"Configuration/DataCommand.config");

            foreach (XmlElement nodes in doc.GetElementsByTagName("DataCommand"))
            {
                DataCommandSetting temp= new DataCommandSetting();
                temp.CommandText = nodes["CommandText"].InnerText;
                temp.CommandName = nodes["CommandText"].Attributes["Name"].ToString();

                m_command.Add(temp.CommandName, temp);
            }
        }
    }

    public class DataCommandSetting
    {
        public string Source { get; set; }

        public string CommandName { get; set; }

        public string CommandText { get; set; }
    }
}
