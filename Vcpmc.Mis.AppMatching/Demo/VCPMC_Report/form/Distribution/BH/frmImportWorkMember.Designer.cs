namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH
{
    partial class frmImportWorkMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportWorkMember));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tstxtPath = new System.Windows.Forms.ToolStripTextBox();
            this.txtGetListExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnGetDataFromDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sbbtnSaveDataToDB = new System.Windows.Forms.ToolStripButton();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.dgvEditFileImport = new System.Windows.Forms.DataGridView();
            this.statusTripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).BeginInit();
            this.statusTripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tstxtPath,
            this.txtGetListExcel,
            this.toolStripSeparator1,
            this.tsBtnGetDataFromDB,
            this.toolStripSeparator2,
            this.sbbtnSaveDataToDB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(99, 22);
            this.toolStripButton1.Text = "Choise Folder";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tstxtPath
            // 
            this.tstxtPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstxtPath.Name = "tstxtPath";
            this.tstxtPath.ReadOnly = true;
            this.tstxtPath.Size = new System.Drawing.Size(200, 25);
            // 
            // txtGetListExcel
            // 
            this.txtGetListExcel.Image = ((System.Drawing.Image)(resources.GetObject("txtGetListExcel.Image")));
            this.txtGetListExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtGetListExcel.Name = "txtGetListExcel";
            this.txtGetListExcel.Size = new System.Drawing.Size(133, 22);
            this.txtGetListExcel.Text = "Get Data From Excel";
            this.txtGetListExcel.Click += new System.EventHandler(this.txtGetListExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnGetDataFromDB
            // 
            this.tsBtnGetDataFromDB.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnGetDataFromDB.Image")));
            this.tsBtnGetDataFromDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGetDataFromDB.Name = "tsBtnGetDataFromDB";
            this.tsBtnGetDataFromDB.Size = new System.Drawing.Size(129, 22);
            this.tsBtnGetDataFromDB.Text = "Load Data From DB";
            this.tsBtnGetDataFromDB.Click += new System.EventHandler(this.tsBtnGetDataFromDB_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // sbbtnSaveDataToDB
            // 
            this.sbbtnSaveDataToDB.Image = ((System.Drawing.Image)(resources.GetObject("sbbtnSaveDataToDB.Image")));
            this.sbbtnSaveDataToDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbbtnSaveDataToDB.Name = "sbbtnSaveDataToDB";
            this.sbbtnSaveDataToDB.Size = new System.Drawing.Size(83, 22);
            this.sbbtnSaveDataToDB.Text = "Save to DB";
            this.sbbtnSaveDataToDB.Click += new System.EventHandler(this.sbbtnSaveDataToDB_Click);
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(372, 62);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 28;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // dgvEditFileImport
            // 
            this.dgvEditFileImport.AllowUserToAddRows = false;
            this.dgvEditFileImport.AllowUserToDeleteRows = false;
            this.dgvEditFileImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEditFileImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditFileImport.Location = new System.Drawing.Point(0, 23);
            this.dgvEditFileImport.Name = "dgvEditFileImport";
            this.dgvEditFileImport.ReadOnly = true;
            this.dgvEditFileImport.Size = new System.Drawing.Size(800, 152);
            this.dgvEditFileImport.TabIndex = 25;
            // 
            // statusTripMain
            // 
            this.statusTripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssLblInfo});
            this.statusTripMain.Location = new System.Drawing.Point(0, 176);
            this.statusTripMain.Name = "statusTripMain";
            this.statusTripMain.Size = new System.Drawing.Size(800, 22);
            this.statusTripMain.TabIndex = 27;
            this.statusTripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel1.Text = "Info: ";
            // 
            // tssLblInfo
            // 
            this.tssLblInfo.Name = "tssLblInfo";
            this.tssLblInfo.Size = new System.Drawing.Size(16, 17);
            this.tssLblInfo.Text = "...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmImportWorkMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 198);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pcloader);
            this.Controls.Add(this.dgvEditFileImport);
            this.Controls.Add(this.statusTripMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImportWorkMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Work Member";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImportWorkMember_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).EndInit();
            this.statusTripMain.ResumeLayout(false);
            this.statusTripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripTextBox tstxtPath;
        private System.Windows.Forms.ToolStripButton txtGetListExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton sbbtnSaveDataToDB;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.DataGridView dgvEditFileImport;
        private System.Windows.Forms.StatusStrip statusTripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssLblInfo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripButton tsBtnGetDataFromDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}