namespace Vcpmc.Mis.AppMatching.form.Distribution.BH
{
    partial class frmDistribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistribution));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.dgvListFile = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusLoadData = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusPrinter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StatusSaveDataToDatabase = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalrecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tstxtPath = new System.Windows.Forms.ToolStripTextBox();
            this.txtGetListExcel = new System.Windows.Forms.ToolStripButton();
            this.sdbtnLoadDataExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnGetDataFromDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnViewReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnPrinter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sbbtnSaveDataToDB = new System.Windows.Forms.ToolStripButton();
            this.stStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cheAll = new System.Windows.Forms.CheckBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListFile)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.stStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(437, 151);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 23;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            this.pcloader.Click += new System.EventHandler(this.pcloader_Click);
            // 
            // dgvListFile
            // 
            this.dgvListFile.AllowUserToAddRows = false;
            this.dgvListFile.AllowUserToDeleteRows = false;
            this.dgvListFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.STT,
            this.statusLoadData,
            this.statusPrinter,
            this.StatusSaveDataToDatabase,
            this.Id,
            this.name,
            this.totalrecord,
            this.note,
            this.timeCreate,
            this.user});
            this.dgvListFile.Location = new System.Drawing.Point(1, 26);
            this.dgvListFile.Name = "dgvListFile";
            this.dgvListFile.ReadOnly = true;
            this.dgvListFile.Size = new System.Drawing.Size(908, 315);
            this.dgvListFile.TabIndex = 0;
            this.dgvListFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListFile_CellClick);
            // 
            // Check
            // 
            this.Check.DataPropertyName = "check";
            this.Check.FillWeight = 30F;
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 30;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "no";
            this.STT.FillWeight = 40F;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.Width = 40;
            // 
            // statusLoadData
            // 
            this.statusLoadData.DataPropertyName = "StatusLoadData";
            this.statusLoadData.FillWeight = 50F;
            this.statusLoadData.HeaderText = "Load Data";
            this.statusLoadData.Name = "statusLoadData";
            this.statusLoadData.ReadOnly = true;
            this.statusLoadData.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.statusLoadData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.statusLoadData.Width = 50;
            // 
            // statusPrinter
            // 
            this.statusPrinter.DataPropertyName = "StatusPrinter";
            this.statusPrinter.FillWeight = 50F;
            this.statusPrinter.HeaderText = "Printer";
            this.statusPrinter.Name = "statusPrinter";
            this.statusPrinter.ReadOnly = true;
            this.statusPrinter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.statusPrinter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.statusPrinter.Width = 50;
            // 
            // StatusSaveDataToDatabase
            // 
            this.StatusSaveDataToDatabase.DataPropertyName = "StatusSaveDataToDatabase";
            this.StatusSaveDataToDatabase.HeaderText = "Save Data To DB";
            this.StatusSaveDataToDatabase.Name = "StatusSaveDataToDatabase";
            this.StatusSaveDataToDatabase.ReadOnly = true;
            this.StatusSaveDataToDatabase.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusSaveDataToDatabase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 220F;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 220;
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.FillWeight = 600F;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 600;
            // 
            // totalrecord
            // 
            this.totalrecord.DataPropertyName = "TotalRecord";
            this.totalrecord.FillWeight = 60F;
            this.totalrecord.HeaderText = "Total Record";
            this.totalrecord.Name = "totalrecord";
            this.totalrecord.ReadOnly = true;
            this.totalrecord.Width = 60;
            // 
            // note
            // 
            this.note.DataPropertyName = "Note";
            this.note.FillWeight = 200F;
            this.note.HeaderText = "Note";
            this.note.Name = "note";
            this.note.ReadOnly = true;
            this.note.Width = 200;
            // 
            // timeCreate
            // 
            this.timeCreate.DataPropertyName = "TimeCreate";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm:ss";
            this.timeCreate.DefaultCellStyle = dataGridViewCellStyle1;
            this.timeCreate.FillWeight = 150F;
            this.timeCreate.HeaderText = "Time Create";
            this.timeCreate.Name = "timeCreate";
            this.timeCreate.ReadOnly = true;
            this.timeCreate.Width = 150;
            // 
            // user
            // 
            this.user.DataPropertyName = "User";
            this.user.HeaderText = "User";
            this.user.Name = "user";
            this.user.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tstxtPath,
            this.txtGetListExcel,
            this.sdbtnLoadDataExcel,
            this.toolStripSeparator1,
            this.tsBtnGetDataFromDB,
            this.toolStripSeparator2,
            this.tsBtnViewReport,
            this.toolStripSeparator3,
            this.tsBtnPrinter,
            this.toolStripSeparator4,
            this.sbbtnSaveDataToDB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(911, 25);
            this.toolStrip1.TabIndex = 10;
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
            this.tstxtPath.Size = new System.Drawing.Size(100, 25);
            this.tstxtPath.Text = "E:\\Solution\\Source Code\\excel distribution";
            // 
            // txtGetListExcel
            // 
            this.txtGetListExcel.Image = ((System.Drawing.Image)(resources.GetObject("txtGetListExcel.Image")));
            this.txtGetListExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtGetListExcel.Name = "txtGetListExcel";
            this.txtGetListExcel.Size = new System.Drawing.Size(93, 22);
            this.txtGetListExcel.Text = "Get list Excel";
            this.txtGetListExcel.Click += new System.EventHandler(this.txtGetListExcel_Click);
            // 
            // sdbtnLoadDataExcel
            // 
            this.sdbtnLoadDataExcel.Image = ((System.Drawing.Image)(resources.GetObject("sdbtnLoadDataExcel.Image")));
            this.sdbtnLoadDataExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sdbtnLoadDataExcel.Name = "sdbtnLoadDataExcel";
            this.sdbtnLoadDataExcel.Size = new System.Drawing.Size(141, 22);
            this.sdbtnLoadDataExcel.Text = "Load Data From Excel";
            this.sdbtnLoadDataExcel.Click += new System.EventHandler(this.sdbtnLoadDataExcel_Click);
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
            // tsBtnViewReport
            // 
            this.tsBtnViewReport.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnViewReport.Image")));
            this.tsBtnViewReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnViewReport.Name = "tsBtnViewReport";
            this.tsBtnViewReport.Size = new System.Drawing.Size(90, 22);
            this.tsBtnViewReport.Text = "View Report";
            this.tsBtnViewReport.Click += new System.EventHandler(this.tsBtnViewReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnPrinter
            // 
            this.tsBtnPrinter.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnPrinter.Image")));
            this.tsBtnPrinter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrinter.Name = "tsBtnPrinter";
            this.tsBtnPrinter.Size = new System.Drawing.Size(62, 22);
            this.tsBtnPrinter.Text = "Printer";
            this.tsBtnPrinter.Click += new System.EventHandler(this.tsBtnPrinter_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // stStatus
            // 
            this.stStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.stsStatus});
            this.stStatus.Location = new System.Drawing.Point(0, 342);
            this.stStatus.Name = "stStatus";
            this.stStatus.Size = new System.Drawing.Size(911, 22);
            this.stStatus.TabIndex = 11;
            this.stStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabel1.Text = "Statatus:";
            // 
            // stsStatus
            // 
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(16, 17);
            this.stsStatus.Text = "...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // cheAll
            // 
            this.cheAll.AutoSize = true;
            this.cheAll.Location = new System.Drawing.Point(50, 36);
            this.cheAll.Name = "cheAll";
            this.cheAll.Size = new System.Drawing.Size(15, 14);
            this.cheAll.TabIndex = 24;
            this.cheAll.UseVisualStyleBackColor = true;
            this.cheAll.CheckedChanged += new System.EventHandler(this.cheAll_CheckedChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 41;
            this.reportViewer1.Location = new System.Drawing.Point(856, 26);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(43, 32);
            this.reportViewer1.TabIndex = 25;
            this.reportViewer1.Visible = false;
            // 
            // frmDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 364);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.cheAll);
            this.Controls.Add(this.stStatus);
            this.Controls.Add(this.pcloader);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvListFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDistribution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribution";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDistribution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListFile)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.stStatus.ResumeLayout(false);
            this.stStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvListFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripTextBox tstxtPath;
        private System.Windows.Forms.ToolStripButton txtGetListExcel;
        private System.Windows.Forms.StatusStrip stStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel stsStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.CheckBox cheAll;
        private System.Windows.Forms.ToolStripButton sdbtnLoadDataExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnGetDataFromDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton sbbtnSaveDataToDB;
        private System.Windows.Forms.ToolStripButton tsBtnViewReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsBtnPrinter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusLoadData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusPrinter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StatusSaveDataToDatabase;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalrecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn user;
    }
}