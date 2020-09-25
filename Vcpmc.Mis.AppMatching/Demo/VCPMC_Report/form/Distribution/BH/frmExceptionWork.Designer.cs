namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH
{
    partial class frmExceptionWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExceptionWork));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tstxtPath = new System.Windows.Forms.ToolStripTextBox();
            this.txtGetListExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnGetDataFromDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sbbtnSaveDataToDB = new System.Windows.Forms.ToolStripButton();
            this.dgvEditFileImport = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExceptionWorkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExceptionWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Member = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Member2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PoolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.statusTripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(783, 25);
            this.toolStrip1.TabIndex = 30;
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
            // dgvEditFileImport
            // 
            this.dgvEditFileImport.AllowUserToAddRows = false;
            this.dgvEditFileImport.AllowUserToDeleteRows = false;
            this.dgvEditFileImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEditFileImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditFileImport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ExceptionWorkId,
            this.ExceptionWork,
            this.TimeCreate,
            this.Member,
            this.Member2,
            this.No,
            this.Title,
            this.Title2,
            this.PoolName,
            this.Type});
            this.dgvEditFileImport.Location = new System.Drawing.Point(0, 23);
            this.dgvEditFileImport.Name = "dgvEditFileImport";
            this.dgvEditFileImport.ReadOnly = true;
            this.dgvEditFileImport.Size = new System.Drawing.Size(783, 162);
            this.dgvEditFileImport.TabIndex = 29;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ExceptionWorkId
            // 
            this.ExceptionWorkId.DataPropertyName = "ExceptionWorkId";
            this.ExceptionWorkId.HeaderText = "ExceptionWorkId";
            this.ExceptionWorkId.Name = "ExceptionWorkId";
            this.ExceptionWorkId.ReadOnly = true;
            this.ExceptionWorkId.Visible = false;
            // 
            // ExceptionWork
            // 
            this.ExceptionWork.DataPropertyName = "ExceptionWork";
            this.ExceptionWork.HeaderText = "ExceptionWork";
            this.ExceptionWork.Name = "ExceptionWork";
            this.ExceptionWork.ReadOnly = true;
            this.ExceptionWork.Visible = false;
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
            // Member
            // 
            this.Member.DataPropertyName = "Member";
            this.Member.FillWeight = 300F;
            this.Member.HeaderText = "Member";
            this.Member.Name = "Member";
            this.Member.ReadOnly = true;
            this.Member.Width = 300;
            // 
            // Member2
            // 
            this.Member2.DataPropertyName = "Member2";
            this.Member2.FillWeight = 300F;
            this.Member2.HeaderText = "Member2";
            this.Member2.Name = "Member2";
            this.Member2.ReadOnly = true;
            this.Member2.Width = 300;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.FillWeight = 300F;
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 300;
            // 
            // Title2
            // 
            this.Title2.DataPropertyName = "Title2";
            this.Title2.FillWeight = 300F;
            this.Title2.HeaderText = "Title2";
            this.Title2.Name = "Title2";
            this.Title2.ReadOnly = true;
            this.Title2.Width = 300;
            // 
            // PoolName
            // 
            this.PoolName.DataPropertyName = "PoolName";
            this.PoolName.FillWeight = 300F;
            this.PoolName.HeaderText = "PoolName";
            this.PoolName.Name = "PoolName";
            this.PoolName.ReadOnly = true;
            this.PoolName.Width = 300;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.FillWeight = 50F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 50;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(374, 68);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 32;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // statusTripMain
            // 
            this.statusTripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssLblInfo});
            this.statusTripMain.Location = new System.Drawing.Point(0, 186);
            this.statusTripMain.Name = "statusTripMain";
            this.statusTripMain.Size = new System.Drawing.Size(783, 22);
            this.statusTripMain.TabIndex = 31;
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
            // frmExceptionWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 208);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pcloader);
            this.Controls.Add(this.statusTripMain);
            this.Controls.Add(this.dgvEditFileImport);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExceptionWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Work";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExceptionWork_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
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
        private System.Windows.Forms.ToolStripButton tsBtnGetDataFromDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton sbbtnSaveDataToDB;
        private System.Windows.Forms.DataGridView dgvEditFileImport;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.StatusStrip statusTripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssLblInfo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExceptionWorkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExceptionWork;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Member;
        private System.Windows.Forms.DataGridViewTextBoxColumn Member2;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}