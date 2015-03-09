namespace Lvr_Land_Maker
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectMoreFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLandColumnIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLocationInfoxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAllList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonErrorList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDataList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHide = new System.Windows.Forms.ToolStripButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Buxton Sketch", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1215, 605);
            this.label1.TabIndex = 4;
            this.label1.Text = "Drag and Drop files to here...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.DragDrop += new System.Windows.Forms.DragEventHandler(this.label1_DragDrop);
            this.label1.DragEnter += new System.Windows.Forms.DragEventHandler(this.label1_DragEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1215, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.selectMoreFileToolStripMenuItem,
            this.updateLandColumnIToolStripMenuItem,
            this.updateLocationInfoxmlToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.mainToolStripMenuItem.Text = "@Main";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
            // 
            // selectMoreFileToolStripMenuItem
            // 
            this.selectMoreFileToolStripMenuItem.Name = "selectMoreFileToolStripMenuItem";
            this.selectMoreFileToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.selectMoreFileToolStripMenuItem.Text = "Select more file ...";
            this.selectMoreFileToolStripMenuItem.Click += new System.EventHandler(this.selectMoreFileToolStripMenuItem_Click);
            // 
            // updateLandColumnIToolStripMenuItem
            // 
            this.updateLandColumnIToolStripMenuItem.Name = "updateLandColumnIToolStripMenuItem";
            this.updateLandColumnIToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.updateLandColumnIToolStripMenuItem.Text = "Update LandColumnInfo.xml ...";
            this.updateLandColumnIToolStripMenuItem.Click += new System.EventHandler(this.updateLandColumnIToolStripMenuItem_Click);
            // 
            // updateLocationInfoxmlToolStripMenuItem
            // 
            this.updateLocationInfoxmlToolStripMenuItem.Name = "updateLocationInfoxmlToolStripMenuItem";
            this.updateLocationInfoxmlToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.updateLocationInfoxmlToolStripMenuItem.Text = "Update LocationInfo.xml ...";
            this.updateLocationInfoxmlToolStripMenuItem.Click += new System.EventHandler(this.updateLocationInfoXmlToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Brown;
            this.richTextBox1.Location = new System.Drawing.Point(0, 266);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1215, 339);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "none";
            this.richTextBox1.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAllList,
            this.toolStripSeparator2,
            this.toolStripButtonErrorList,
            this.toolStripSeparator3,
            this.toolStripButtonDataList,
            this.toolStripSeparator4,
            this.toolStripButtonCopy,
            this.toolStripSeparator5,
            this.toolStripButtonHide});
            this.toolStrip2.Location = new System.Drawing.Point(0, 241);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1215, 25);
            this.toolStrip2.TabIndex = 13;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonAllList
            // 
            this.toolStripButtonAllList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAllList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAllList.Name = "toolStripButtonAllList";
            this.toolStripButtonAllList.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonAllList.Text = "詳細清單";
            this.toolStripButtonAllList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonAllList.Click += new System.EventHandler(this.toolStripButtonAllList_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonErrorList
            // 
            this.toolStripButtonErrorList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonErrorList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonErrorList.Name = "toolStripButtonErrorList";
            this.toolStripButtonErrorList.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonErrorList.Text = "錯誤清單";
            this.toolStripButtonErrorList.Click += new System.EventHandler(this.toolStripButtonErrorList_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDataList
            // 
            this.toolStripButtonDataList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDataList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDataList.Name = "toolStripButtonDataList";
            this.toolStripButtonDataList.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonDataList.Text = "資料問題清單";
            this.toolStripButtonDataList.Click += new System.EventHandler(this.toolStripButtonDataList_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonCopy.Text = "複製";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonHide
            // 
            this.toolStripButtonHide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHide.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButtonHide.ForeColor = System.Drawing.Color.Red;
            this.toolStripButtonHide.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHide.Image")));
            this.toolStripButtonHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHide.Name = "toolStripButtonHide";
            this.toolStripButtonHide.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonHide.Text = "顯示Log ↑";
            this.toolStripButtonHide.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonHide.Click += new System.EventHandler(this.toolStripButtonHide_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 605);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1215, 24);
            this.progressBar1.TabIndex = 6;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(2, 586);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 16);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "待命中...";
            this.statusLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 629);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectMoreFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateLandColumnIToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem updateLocationInfoxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAllList;
        private System.Windows.Forms.ToolStripButton toolStripButtonErrorList;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonDataList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonHide;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label statusLabel;
    }
}

