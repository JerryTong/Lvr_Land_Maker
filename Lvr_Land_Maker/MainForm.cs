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

        /// <summary>
        ///Prepare UI Setting (Progress Bar)
        /// </summary>
        /// <param name="filePages"></param>
        public void Prepare(List<string> filePages)
        {
            richTextBox1.ClearSync();

            ////Progress Bar
            progressBar1.SetMiniValueSync(filePages.Count * 3);
            progressBar1.SetValueSync(0);
        }

        public void Start(List<string> filesPath)
        {
            this.Prepare(filesPath);

            int dataCount = 0;
            string errorMsg = string.Empty;

            List<string> correctlyPath = new List<string>();
            Process process = new Process();
            resultLogger = new List<Logger>();
            
            statusLabel.AsyncText("正在檢查檔案格式...");

            correctlyPath = this.GetCorrectlyPath(filesPath, out errorMsg);
            if (correctlyPath == null)
            {
                resultLogger.Add(new Logger
                {
                    Type = LoggerType.DataException,
                    Message = "含有非XML格式文件。",
                    Path = errorMsg,
                });

                MessageBox.Show(string.Format("{0}\n請重新檢查檔案格式正確性。", errorMsg), "含有非XML格式文件", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendLoggerMsg(resultLogger);
                progressBar1.SetValueSync(0);

                return; ////檔案格式不符，轉換中斷。
            }

            progressBar1.SetValueSync(correctlyPath.Count);

            var lvrLandList = process.GetLvrLandDetailInfo(correctlyPath);
            if (lvrLandList == null)
            {
                MessageBox.Show(string.Format("讀取實價登錄資料發生錯誤 - {0}", "不明"), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendLoggerMsg(resultLogger);
                progressBar1.SetValueSync(0);

                return;  ////取得實價登錄資料出錯，程序結束
            }

            foreach (var detail in lvrLandList)
            {
                ////解析檔案內容
                statusLabel.AsyncText(string.Format("正在檢查檔案-{0} 內容...", detail.FileName));

                string errMessage = string.Empty;
                detail.TransLvrLandTable = process.TransFormatLvrLandData(detail, out errMessage);
                if (detail.TransLvrLandTable != null && string.IsNullOrEmpty(errMessage))
                {
                    foreach (var row in detail.TransLvrLandTable.AsEnumerable().ToList())
                    {
                        bool isAccuracy = process.CheckTransDataRowsAccuracy(row, detail.SaleType, out errMessage);
                        if (!isAccuracy && MessageBox.Show(string.Format("檔案:{0}\n錯誤分析:{1}\n\n[詢問] 是否忽略寫入此筆資料?", detail.FileName, errMessage), "實價登錄資料轉換驗證錯誤", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes) 
                        {
                            row.SetField<string>("IsAdd", "0");
                        }
                    }
                }

                progressBar1.SetValueSync(progressBar1.Value + 1);
                
                if (!string.IsNullOrEmpty(errMessage))
                {
                    MessageBox.Show(string.Format("檔案:{0}\n錯誤分析:{1}", detail.FileName, errMessage), "實價登錄資料轉換驗證錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    resultLogger.Add(new Logger
                    {
                        Type = LoggerType.DataException,
                        Message = string.Format("檔案:{0}\n錯誤分析:{1}", detail.FileName, errMessage),
                        Path = detail.FileName,
                    });

                    AppendLoggerMsg(resultLogger);
                    progressBar1.SetValueSync(0);
                    return; ////轉換失敗，程序結束。
                }
            }

            lvrLandList.ForEach(land =>
            {
                statusLabel.AsyncText(string.Format("正在寫入檔案-{0} 內容...", land.FileName));
                dataCount += land.TransLvrLandTable != null ? land.TransLvrLandTable.Rows.Count : 0;
                resultLogger.Add(process.LandInsertToDataBase(land));

                progressBar1.SetValueSync(progressBar1.Value + 1);
            });

            this.ShowLoggerMessageRichTextBoxContent(resultLogger, 0);
            MessageBox.Show(string.Format("資料已寫入({0}筆。", dataCount), "實價登錄資料轉換完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 過濾檔名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private List<string> GetCorrectlyPath(List<string> filePath, out string errorMsg)
        {
            List<string> correctlyPath = new List<string>();
            Process process = new Process();
            errorMsg = string.Empty;

            foreach (var path in filePath)
            {
                if (!process.CheckFileExtension(path, out errorMsg))
                {
                    if ((MessageBox.Show(string.Format("{0}\n\n是否忽略此檔案?[Y/N](拒絕將結束停止轉換!)", errorMsg), "含有非XML格式文件", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes))
                    {
                        ////Add Maker '_'
                        correctlyPath.Add("_" + path);
                    }
                    else
                    {
                        return null; ////檔案格式不符，轉換失敗，程序結束。
                    }
                }
                else
                {
                    correctlyPath.Add(path);
                }
            }

            correctlyPath.RemoveAll(f => f.StartsWith("_"));
            return correctlyPath;
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
            List<string> filePaths = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
            this.toolStripButtonHide_Click(null, null);

            await Task.Run(() => { this.Start(filePaths); });
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private async void selectMoreFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> filePaths = openFileDialog1.FileNames.ToList();
                this.toolStripButtonHide_Click(null, null);

                await Task.Run(() => { this.Start(filePaths); });
            }
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
