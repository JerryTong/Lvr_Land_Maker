using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lvr_Land_Maker.BLL;
using Lvr_Land_Maker.Extension;

namespace Lvr_Land_Maker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void Start(List<string> filesPath)
        {
            Process process = new Process();
         
            foreach (var path in filesPath)
            {
                try
                {
                    var result = process.LandMakerProcess(path);
                    AppendLoggerMsg(result);
                }
                catch (Exception ex)
                {
                    var exceptionReesult = new Logger
                    {
                        Type = LoggerType.Exception,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Path = path,
                    };

                    AppendLoggerMsg(exceptionReesult);
                }
            }
        }

        private void AppendLoggerMsg(Logger logger)
        {
            switch (logger.Type)
            {
                case LoggerType.Exception:
                    richTextBox1.AppendTextAsync(string.Format("{0}　{1}\n", logger.LoggerString, logger.Path), Color.Red);
                    break;
                case LoggerType.DataException:
                    richTextBox1.AppendTextAsync(string.Format("{0}　{1}\n", logger.LoggerString, logger.Path), Color.Brown);
                    break;
                case LoggerType.AppMessage:
                    richTextBox1.AppendTextAsync(string.Format("{0}　{1}\n", logger.LoggerString, logger.Path), Color.DarkGreen);
                    break;
                default:
                    break;

            }

            if (!string.IsNullOrEmpty(logger.InternalDescription))
            {
                richTextBox1.AppendTextAsync(logger.InternalDescription, Color.Red);
            }

            if (!string.IsNullOrEmpty(logger.StackTrace))
            {
                richTextBox1.AppendTextAsync(logger.StackTrace, Color.Blue);
            }

            richTextBox1.AppendTextAsync("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n", Color.Black);
        } 

        private async void label1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> filePath = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
            this.toolStripButtonHide_Click(null, null);
            richTextBox1.Clear();

            await Task.Run(() => { this.Start(filePath); });
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void selectMoreFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////
        }

        private void updateLandColumnIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////
        }

        private void updateLocationInfoXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////
        }

        private void toolStripButtonAllList_Click(object sender, EventArgs e)
        {
            ////
        }

        private void toolStripButtonErrorList_Click(object sender, EventArgs e)
        {
            ////
        }

        private void toolStripButtonDataList_Click(object sender, EventArgs e)
        {
            ////
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            ////
        }

        private void toolStripButtonHide_Click(object sender, EventArgs e)
        {
            ////
            if (richTextBox1.Visible)
            {
                richTextBox1.Visible = false;
                toolStripButtonHide.Text = "顯示Log ↑";
            }
            else
            {
                richTextBox1.Visible = true;
                toolStripButtonHide.Text = "隱藏Log ↓";
            }
            
        }
    }
}
