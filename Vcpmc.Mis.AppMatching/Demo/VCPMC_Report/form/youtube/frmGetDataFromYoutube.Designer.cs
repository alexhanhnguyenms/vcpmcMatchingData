namespace Vcpmc.Mis.AppMatching.form.youtube
{
    partial class frmGetDataFromYoutube
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetDataFromYoutube));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tgMain = new System.Windows.Forms.TabPage();
            this.btnGetDataFromDB = new System.Windows.Forms.Button();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rbLog = new System.Windows.Forms.RichTextBox();
            this.gbChanelList = new System.Windows.Forms.GroupBox();
            this.dgvChannelList = new System.Windows.Forms.DataGridView();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnGetDataFromeExcel = new System.Windows.Forms.Button();
            this.tgSubMain = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabMain.SuspendLayout();
            this.tgMain.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.gbChanelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tgMain);
            this.tabMain.Controls.Add(this.tgSubMain);
            this.tabMain.Location = new System.Drawing.Point(1, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(495, 329);
            this.tabMain.TabIndex = 0;
            // 
            // tgMain
            // 
            this.tgMain.Controls.Add(this.btnGetDataFromDB);
            this.tgMain.Controls.Add(this.gbLog);
            this.tgMain.Controls.Add(this.gbChanelList);
            this.tgMain.Controls.Add(this.txtFile);
            this.tgMain.Controls.Add(this.btnGetDataFromeExcel);
            this.tgMain.Location = new System.Drawing.Point(4, 21);
            this.tgMain.Name = "tgMain";
            this.tgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tgMain.Size = new System.Drawing.Size(487, 304);
            this.tgMain.TabIndex = 0;
            this.tgMain.Text = "Chanel Youtube";
            this.tgMain.UseVisualStyleBackColor = true;
            // 
            // btnGetDataFromDB
            // 
            this.btnGetDataFromDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetDataFromDB.Location = new System.Drawing.Point(388, 1);
            this.btnGetDataFromDB.Name = "btnGetDataFromDB";
            this.btnGetDataFromDB.Size = new System.Drawing.Size(94, 21);
            this.btnGetDataFromDB.TabIndex = 4;
            this.btnGetDataFromDB.Text = "Get Database";
            this.btnGetDataFromDB.UseVisualStyleBackColor = true;
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.rbLog);
            this.gbLog.Location = new System.Drawing.Point(8, 264);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(473, 39);
            this.gbLog.TabIndex = 3;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rbLog
            // 
            this.rbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLog.Location = new System.Drawing.Point(3, 14);
            this.rbLog.Name = "rbLog";
            this.rbLog.ReadOnly = true;
            this.rbLog.Size = new System.Drawing.Size(467, 22);
            this.rbLog.TabIndex = 0;
            this.rbLog.Text = "";
            // 
            // gbChanelList
            // 
            this.gbChanelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbChanelList.Controls.Add(this.dgvChannelList);
            this.gbChanelList.Location = new System.Drawing.Point(8, 29);
            this.gbChanelList.Name = "gbChanelList";
            this.gbChanelList.Size = new System.Drawing.Size(476, 230);
            this.gbChanelList.TabIndex = 2;
            this.gbChanelList.TabStop = false;
            this.gbChanelList.Text = "Youtube ";
            // 
            // dgvChannelList
            // 
            this.dgvChannelList.AllowUserToAddRows = false;
            this.dgvChannelList.AllowUserToDeleteRows = false;
            this.dgvChannelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChannelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChannelList.Location = new System.Drawing.Point(3, 14);
            this.dgvChannelList.Name = "dgvChannelList";
            this.dgvChannelList.ReadOnly = true;
            this.dgvChannelList.Size = new System.Drawing.Size(470, 213);
            this.dgvChannelList.TabIndex = 0;
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(7, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(297, 18);
            this.txtFile.TabIndex = 1;
            // 
            // btnGetDataFromeExcel
            // 
            this.btnGetDataFromeExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetDataFromeExcel.Location = new System.Drawing.Point(310, 2);
            this.btnGetDataFromeExcel.Name = "btnGetDataFromeExcel";
            this.btnGetDataFromeExcel.Size = new System.Drawing.Size(75, 21);
            this.btnGetDataFromeExcel.TabIndex = 0;
            this.btnGetDataFromeExcel.Text = "Get Excel";
            this.btnGetDataFromeExcel.UseVisualStyleBackColor = true;
            this.btnGetDataFromeExcel.Click += new System.EventHandler(this.btnGetDataFromeExcel_Click);
            // 
            // tgSubMain
            // 
            this.tgSubMain.Location = new System.Drawing.Point(4, 22);
            this.tgSubMain.Name = "tgSubMain";
            this.tgSubMain.Padding = new System.Windows.Forms.Padding(3);
            this.tgSubMain.Size = new System.Drawing.Size(487, 303);
            this.tgSubMain.TabIndex = 1;
            this.tgSubMain.Text = "Detail Chanel Youtube";
            this.tgSubMain.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 333);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmGetDataFromYoutube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 355);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGetDataFromYoutube";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Data From Youtube";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGetDataFromYoutube_Load);
            this.tabMain.ResumeLayout(false);
            this.tgMain.ResumeLayout(false);
            this.tgMain.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbChanelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tgMain;
        private System.Windows.Forms.TabPage tgSubMain;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rbLog;
        private System.Windows.Forms.GroupBox gbChanelList;
        private System.Windows.Forms.DataGridView dgvChannelList;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnGetDataFromeExcel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnGetDataFromDB;
    }
}