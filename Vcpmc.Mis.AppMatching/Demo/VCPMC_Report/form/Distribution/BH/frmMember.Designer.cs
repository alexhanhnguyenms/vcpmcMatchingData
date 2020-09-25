namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH
{
    partial class frmMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMember));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.statusTripMain = new System.Windows.Forms.StatusStrip();
            this.sbbtnSaveDataToDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnGetDataFromDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtGetListExcel = new System.Windows.Forms.ToolStripButton();
            this.tstxtPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dgvEditFileImport = new System.Windows.Forms.DataGridView();
            this.MemberBHx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemberBHId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Member = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemberVN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beneficiary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAlwaysGet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsGiveBeneficiary = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsCreateReport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            this.statusTripMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // LblInfo
            // 
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(16, 17);
            this.LblInfo.Text = "...";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel1.Text = "Info: ";
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(445, 115);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 36;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // statusTripMain
            // 
            this.statusTripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.LblInfo});
            this.statusTripMain.Location = new System.Drawing.Point(0, 305);
            this.statusTripMain.Name = "statusTripMain";
            this.statusTripMain.Size = new System.Drawing.Size(905, 22);
            this.statusTripMain.TabIndex = 35;
            this.statusTripMain.Text = "statusStrip1";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // tstxtPath
            // 
            this.tstxtPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstxtPath.Name = "tstxtPath";
            this.tstxtPath.ReadOnly = true;
            this.tstxtPath.Size = new System.Drawing.Size(200, 25);
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
            this.toolStrip1.Size = new System.Drawing.Size(905, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dgvEditFileImport
            // 
            this.dgvEditFileImport.AllowUserToAddRows = false;
            this.dgvEditFileImport.AllowUserToDeleteRows = false;
            this.dgvEditFileImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEditFileImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditFileImport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MemberBHx,
            this.MemberBHId,
            this.Id,
            this.TimeCreate,
            this.Type,
            this.No,
            this.Member,
            this.MemberVN,
            this.StageName,
            this.SubMember,
            this.Beneficiary,
            this.GetPart,
            this.returnDate,
            this.Percent,
            this.IsAlwaysGet,
            this.IsGiveBeneficiary,
            this.IsCreateReport,
            this.Note});
            this.dgvEditFileImport.Location = new System.Drawing.Point(0, 26);
            this.dgvEditFileImport.Name = "dgvEditFileImport";
            this.dgvEditFileImport.ReadOnly = true;
            this.dgvEditFileImport.Size = new System.Drawing.Size(905, 278);
            this.dgvEditFileImport.TabIndex = 37;
            // 
            // MemberBHx
            // 
            this.MemberBHx.DataPropertyName = "MemberBH";
            this.MemberBHx.FillWeight = 300F;
            this.MemberBHx.HeaderText = "MemberBH";
            this.MemberBHx.Name = "MemberBHx";
            this.MemberBHx.ReadOnly = true;
            this.MemberBHx.Visible = false;
            this.MemberBHx.Width = 300;
            // 
            // MemberBHId
            // 
            this.MemberBHId.DataPropertyName = "MemberBHId";
            this.MemberBHId.FillWeight = 300F;
            this.MemberBHId.HeaderText = "MemberBHId";
            this.MemberBHId.Name = "MemberBHId";
            this.MemberBHId.ReadOnly = true;
            this.MemberBHId.Visible = false;
            this.MemberBHId.Width = 300;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 41;
            // 
            // TimeCreate
            // 
            this.TimeCreate.DataPropertyName = "TimeCreate";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm:ss";
            this.TimeCreate.DefaultCellStyle = dataGridViewCellStyle1;
            this.TimeCreate.FillWeight = 150F;
            this.TimeCreate.HeaderText = "TimeCreate";
            this.TimeCreate.Name = "TimeCreate";
            this.TimeCreate.ReadOnly = true;
            this.TimeCreate.Width = 150;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.FillWeight = 80F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 80;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 55;
            // 
            // Member
            // 
            this.Member.DataPropertyName = "Member";
            this.Member.FillWeight = 150F;
            this.Member.HeaderText = "Member";
            this.Member.Name = "Member";
            this.Member.ReadOnly = true;
            this.Member.Width = 150;
            // 
            // MemberVN
            // 
            this.MemberVN.DataPropertyName = "MemberVN";
            this.MemberVN.FillWeight = 150F;
            this.MemberVN.HeaderText = "MemberVN";
            this.MemberVN.Name = "MemberVN";
            this.MemberVN.ReadOnly = true;
            this.MemberVN.Width = 150;
            // 
            // StageName
            // 
            this.StageName.DataPropertyName = "StageName";
            this.StageName.HeaderText = "StageName";
            this.StageName.Name = "StageName";
            this.StageName.ReadOnly = true;
            // 
            // SubMember
            // 
            this.SubMember.DataPropertyName = "SubMember";
            this.SubMember.FillWeight = 300F;
            this.SubMember.HeaderText = "SubMember";
            this.SubMember.Name = "SubMember";
            this.SubMember.ReadOnly = true;
            this.SubMember.Width = 300;
            // 
            // Beneficiary
            // 
            this.Beneficiary.DataPropertyName = "Beneficiary";
            this.Beneficiary.FillWeight = 150F;
            this.Beneficiary.HeaderText = "Beneficiary";
            this.Beneficiary.Name = "Beneficiary";
            this.Beneficiary.ReadOnly = true;
            this.Beneficiary.Width = 150;
            // 
            // GetPart
            // 
            this.GetPart.DataPropertyName = "GetPart";
            this.GetPart.HeaderText = "Get Part";
            this.GetPart.Name = "GetPart";
            this.GetPart.ReadOnly = true;
            // 
            // returnDate
            // 
            this.returnDate.DataPropertyName = "returnDate";
            this.returnDate.HeaderText = "returnDate";
            this.returnDate.Name = "returnDate";
            this.returnDate.ReadOnly = true;
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            // 
            // IsAlwaysGet
            // 
            this.IsAlwaysGet.DataPropertyName = "IsAlwaysGet";
            this.IsAlwaysGet.FillWeight = 70F;
            this.IsAlwaysGet.HeaderText = "Always Get";
            this.IsAlwaysGet.Name = "IsAlwaysGet";
            this.IsAlwaysGet.ReadOnly = true;
            this.IsAlwaysGet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsAlwaysGet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsAlwaysGet.Width = 70;
            // 
            // IsGiveBeneficiary
            // 
            this.IsGiveBeneficiary.DataPropertyName = "IsGiveBeneficiary";
            this.IsGiveBeneficiary.FillWeight = 70F;
            this.IsGiveBeneficiary.HeaderText = "Give Beneficiary";
            this.IsGiveBeneficiary.Name = "IsGiveBeneficiary";
            this.IsGiveBeneficiary.ReadOnly = true;
            this.IsGiveBeneficiary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsGiveBeneficiary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsGiveBeneficiary.Visible = false;
            this.IsGiveBeneficiary.Width = 70;
            // 
            // IsCreateReport
            // 
            this.IsCreateReport.DataPropertyName = "IsCreateReport";
            this.IsCreateReport.FillWeight = 70F;
            this.IsCreateReport.HeaderText = "Create Report";
            this.IsCreateReport.Name = "IsCreateReport";
            this.IsCreateReport.ReadOnly = true;
            this.IsCreateReport.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCreateReport.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsCreateReport.Width = 70;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            this.Note.FillWeight = 500F;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.Width = 500;
            // 
            // frmMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 327);
            this.Controls.Add(this.pcloader);
            this.Controls.Add(this.statusTripMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvEditFileImport);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMember_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            this.statusTripMain.ResumeLayout(false);
            this.statusTripMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel LblInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.StatusStrip statusTripMain;
        private System.Windows.Forms.ToolStripButton sbbtnSaveDataToDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnGetDataFromDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton txtGetListExcel;
        private System.Windows.Forms.ToolStripTextBox tstxtPath;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgvEditFileImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberBHx;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberBHId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Member;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberVN;
        private System.Windows.Forms.DataGridViewTextBoxColumn StageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beneficiary;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAlwaysGet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsGiveBeneficiary;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCreateReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    }
}