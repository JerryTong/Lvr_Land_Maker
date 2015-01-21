using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                process.LandMakerProcess(path);
            }
        }

        private async void label1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> filePath = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
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
