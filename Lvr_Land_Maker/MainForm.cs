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
        List<Logger> resultLogger = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public void Start(List<string> filesPath)
        {
            int dataCount = 0; 
            resultLogger = new List<Logger>();
            Process process = new Process();

            statusLabel.AsyncText("正在檢查檔案格式...");
            string errorFileExtension = string.Empty;
            
            if (!process.CheckFileExtension(filesPath, out errorFileExtension))
            {
                resultLogger.Add(new Logger
                {
                    Type = LoggerType.DataException,
                    Message = "含有非XML格式文件。",
                    Path = errorFileExtension,
                });

                MessageBox.Show(string.Format("非XML文件檔案 - {0}", errorFileExtension), "含有非XML格式文件", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendLoggerMsg(resultLogger);
                progressBar1.SetValueAsyc(0);

                return; ////轉換失敗，程序結束。
            }

            progressBar1.SetValueAsyc(filesPath.Count);
            
            var lvrLandList = process.GetLvrLandDetailInfo(filesPath);
            if (lvrLandList == null)
            {
                ////
            }

            foreach (var detail in lvrLandList)
            {
                statusLabel.AsyncText(string.Format("正在檢查檔案-{0} 內容...", detail.FileName));

                string errorMsg = string.Empty;
                detail.TransLvrLandTable = process.TransFormatLvrLandData(detail, out errorMsg);
                progressBar1.SetValueAsyc(progressBar1.Value + 1);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    MessageBox.Show(string.Format("檔案:{0}\n錯誤分析:{1}", detail.FileName, errorMsg), "實價登錄資料轉換錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    resultLogger.Add(new Logger
                    {
                        Type = LoggerType.DataException,
                        Message = string.Format("檔案:{0}\n錯誤分析:{1}", detail.FileName, errorMsg),
                        Path = detail.FileName,
                    });

                    AppendLoggerMsg(resultLogger);
                    progressBar1.SetValueAsyc(0);
                    return; ////轉換失敗，程序結束。
                }
            }

            lvrLandList.ForEach(land =>
            {
                statusLabel.AsyncText(string.Format("正在寫入檔案-{0} 內容...", land.FileName));
                dataCount += land.TransLvrLandTable != null ? land.TransLvrLandTable.Rows.Count : 0;
                resultLogger.Add(process.LandInsertToDataBase(land));

                progressBar1.SetValueAsyc(progressBar1.Value + 1);
            });

            this.ShowLoggerMessageRichTextBoxContent(resultLogger, 0);
            MessageBox.Show(string.Format("資料已寫入({0}筆。", dataCount), "實價登錄資料轉換完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 訊息顯示於RichTextBox。
        /// </summary>
        /// <param name="logger"></param>
        private void AppendLoggerMsg(List<Logger> loggerList)
        {
            foreach (var logger in loggerList)
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
        }

        /// <summary>
        /// 選定特定LOG訊息(Error,DataError, Message.)，並顯示於RichTextBox。
        /// </summary>
        /// <param name="loggers"></param>
        /// <param name="showType">0:All , 1:Error Msg , 2:Data Msg</param>
        private void ShowLoggerMessageRichTextBoxContent(List<Logger> loggers, int showType)
        {
            if (loggers == null)
            {
                return;
            }

            richTextBox1.ClearSync();
            IEnumerable<Logger> tmpLogger = new List<Logger>();
            switch (showType)
            {
                case 0:
                    //// Error. DataError. Message.
                    tmpLogger = loggers.OrderBy(l => l.Type);
                    break;
                case 1:
                    tmpLogger = loggers.Where(l => l.Type == LoggerType.Exception);
                    break;
                case 2:
                    tmpLogger = loggers.Where(l => l.Type == LoggerType.DataException);
                    break;
            }

            AppendLoggerMsg(tmpLogger.ToList());
        }

        private async void label1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> filePath = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
            this.toolStripButtonHide_Click(null, null);
            richTextBox1.Clear();

            ////Progress Bar
            progressBar1.Maximum = filePath.Count * 3;
            progressBar1.Value = 0;

            ////statusLabel
            statusLabel.Visible = true;

            await Task.Run(() => { this.Start(filePath); });

            statusLabel.Text = "作業完成";
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
            this.ShowLoggerMessageRichTextBoxContent(resultLogger, 0);
        }

        private void toolStripButtonErrorList_Click(object sender, EventArgs e)
        {
            ////
            this.ShowLoggerMessageRichTextBoxContent(resultLogger, 1);
        }

        private void toolStripButtonDataList_Click(object sender, EventArgs e)
        {
            ////
            this.ShowLoggerMessageRichTextBoxContent(resultLogger, 2);
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
                statusLabel.Visible = false;
                toolStripButtonHide.Text = "顯示Log ↑";
            }
            else
            {
                richTextBox1.Visible = true;
                statusLabel.Visible = true;
                toolStripButtonHide.Text = "隱藏Log ↓";
            }
            
        }
    }
}
