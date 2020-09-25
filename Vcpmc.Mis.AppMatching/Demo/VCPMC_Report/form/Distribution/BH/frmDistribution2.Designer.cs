namespace Vcpmc.Mis.AppMatching.form.mic.distribution
{
    partial class frmDistribution2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistribution2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTripMain = new System.Windows.Forms.StatusStrip();
            this.tssLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.sbbtnSaveDataToDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnGetDataFromDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtGetListExcel = new System.Windows.Forms.ToolStripButton();
            this.tstxtPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tssLookup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvEditFileImport = new System.Windows.Forms.DataGridView();
            this.DistributionDatax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistributionDataId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusLoad = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SubMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkInNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PoolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Share = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strContractTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beneficiary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Royalty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCreateReport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PoolName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BhAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAlwaysGet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsExcept = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsCondittionTime = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsCalcCondittionTime = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsMapByGroup = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsGiveBeneficiary = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsMapAuthor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBhMember = new System.Windows.Forms.ComboBox();
            this.cboExceptWorkMember = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshData = new System.Windows.Forms.Button();
            this.cboWorkMember = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusTripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel1.Text = "Info: ";
            // 
            // statusTripMain
            // 
            this.statusTripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssLblInfo});
            this.statusTripMain.Location = new System.Drawing.Point(0, 263);
            this.statusTripMain.Name = "statusTripMain";
            this.statusTripMain.Size = new System.Drawing.Size(807, 22);
            this.statusTripMain.TabIndex = 31;
            this.statusTripMain.Text = "statusStrip1";
            // 
            // tssLblInfo
            // 
            this.tssLblInfo.Name = "tssLblInfo";
            this.tssLblInfo.Size = new System.Drawing.Size(16, 17);
            this.tssLblInfo.Text = "...";
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(359, 166);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 32;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
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
            this.tssLookup,
            this.toolStripSeparator3,
            this.sbbtnSaveDataToDB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(807, 25);
            this.toolStrip1.TabIndex = 30;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tssLookup
            // 
            this.tssLookup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssLookup.Image = ((System.Drawing.Image)(resources.GetObject("tssLookup.Image")));
            this.tssLookup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssLookup.Name = "tssLookup";
            this.tssLookup.Size = new System.Drawing.Size(53, 22);
            this.tssLookup.Text = "Find CA";
            this.tssLookup.Click += new System.EventHandler(this.tssLookup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.DistributionDatax,
            this.DistributionDataId,
            this.StatusLoad,
            this.SubMember,
            this.Percent,
            this.Id,
            this.WorkInNo,
            this.TimeCreate,
            this.No,
            this.PoolName,
            this.SourceName,
            this.SourceName2,
            this.Title,
            this.Role,
            this.Share,
            this.Location,
            this.TotalAuthor,
            this.strContractTime,
            this.Beneficiary,
            this.Title2,
            this.Royalty,
            this.GetPart,
            this.IsCreateReport,
            this.PoolName2,
            this.BhAuthor,
            this.ReturnTime,
            this.ContractTime,
            this.returnDate,
            this.IsAlwaysGet,
            this.IsExcept,
            this.IsCondittionTime,
            this.IsCalcCondittionTime,
            this.IsMapByGroup,
            this.IsGiveBeneficiary,
            this.IsMapAuthor,
            this.Note,
            this.Note2});
            this.dgvEditFileImport.Location = new System.Drawing.Point(0, 105);
            this.dgvEditFileImport.Name = "dgvEditFileImport";
            this.dgvEditFileImport.ReadOnly = true;
            this.dgvEditFileImport.Size = new System.Drawing.Size(807, 157);
            this.dgvEditFileImport.TabIndex = 33;
            // 
            // DistributionDatax
            // 
            this.DistributionDatax.DataPropertyName = "DistributionData";
            this.DistributionDatax.FillWeight = 300F;
            this.DistributionDatax.HeaderText = "DistributionData";
            this.DistributionDatax.Name = "DistributionDatax";
            this.DistributionDatax.ReadOnly = true;
            this.DistributionDatax.Visible = false;
            this.DistributionDatax.Width = 300;
            // 
            // DistributionDataId
            // 
            this.DistributionDataId.DataPropertyName = "DistributionDataId";
            this.DistributionDataId.FillWeight = 300F;
            this.DistributionDataId.HeaderText = "DistributionDataId";
            this.DistributionDataId.Name = "DistributionDataId";
            this.DistributionDataId.ReadOnly = true;
            this.DistributionDataId.Visible = false;
            this.DistributionDataId.Width = 300;
            // 
            // StatusLoad
            // 
            this.StatusLoad.DataPropertyName = "StatusLoad";
            this.StatusLoad.HeaderText = "StatusLoad";
            this.StatusLoad.Name = "StatusLoad";
            this.StatusLoad.ReadOnly = true;
            // 
            // SubMember
            // 
            this.SubMember.DataPropertyName = "SubMember";
            this.SubMember.HeaderText = "SubMember";
            this.SubMember.Name = "SubMember";
            this.SubMember.ReadOnly = true;
            this.SubMember.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SubMember.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            this.Percent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Percent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // WorkInNo
            // 
            this.WorkInNo.DataPropertyName = "WorkInNo";
            this.WorkInNo.HeaderText = "WorkInNo";
            this.WorkInNo.Name = "WorkInNo";
            this.WorkInNo.ReadOnly = true;
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
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 55;
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
            // SourceName
            // 
            this.SourceName.DataPropertyName = "SourceName";
            this.SourceName.FillWeight = 300F;
            this.SourceName.HeaderText = "SourceName";
            this.SourceName.Name = "SourceName";
            this.SourceName.ReadOnly = true;
            this.SourceName.Width = 300;
            // 
            // SourceName2
            // 
            this.SourceName2.DataPropertyName = "SourceName2";
            this.SourceName2.HeaderText = "SourceName2";
            this.SourceName2.Name = "SourceName2";
            this.SourceName2.ReadOnly = true;
            this.SourceName2.Visible = false;
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
            // Role
            // 
            this.Role.DataPropertyName = "Role";
            this.Role.HeaderText = "Role";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Share
            // 
            this.Share.DataPropertyName = "Share";
            this.Share.HeaderText = "Share";
            this.Share.Name = "Share";
            this.Share.ReadOnly = true;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "Location";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // TotalAuthor
            // 
            this.TotalAuthor.DataPropertyName = "TotalAuthor";
            this.TotalAuthor.HeaderText = "TotalAuthor";
            this.TotalAuthor.Name = "TotalAuthor";
            this.TotalAuthor.ReadOnly = true;
            // 
            // strContractTime
            // 
            this.strContractTime.DataPropertyName = "strContractTime";
            this.strContractTime.HeaderText = "strContractTime";
            this.strContractTime.Name = "strContractTime";
            this.strContractTime.ReadOnly = true;
            // 
            // Beneficiary
            // 
            this.Beneficiary.DataPropertyName = "Beneficiary";
            this.Beneficiary.HeaderText = "Beneficiary";
            this.Beneficiary.Name = "Beneficiary";
            this.Beneficiary.ReadOnly = true;
            // 
            // Title2
            // 
            this.Title2.DataPropertyName = "Title2";
            this.Title2.FillWeight = 300F;
            this.Title2.HeaderText = "Title2";
            this.Title2.Name = "Title2";
            this.Title2.ReadOnly = true;
            this.Title2.Visible = false;
            this.Title2.Width = 300;
            // 
            // Royalty
            // 
            this.Royalty.DataPropertyName = "Royalty";
            this.Royalty.FillWeight = 150F;
            this.Royalty.HeaderText = "Royalty";
            this.Royalty.Name = "Royalty";
            this.Royalty.ReadOnly = true;
            this.Royalty.Width = 150;
            // 
            // GetPart
            // 
            this.GetPart.DataPropertyName = "GetPart";
            this.GetPart.HeaderText = "GetPart";
            this.GetPart.Name = "GetPart";
            this.GetPart.ReadOnly = true;
            this.GetPart.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GetPart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsCreateReport
            // 
            this.IsCreateReport.HeaderText = "Create Report";
            this.IsCreateReport.Name = "IsCreateReport";
            this.IsCreateReport.ReadOnly = true;
            // 
            // PoolName2
            // 
            this.PoolName2.DataPropertyName = "PoolName2";
            this.PoolName2.HeaderText = "PoolName2";
            this.PoolName2.Name = "PoolName2";
            this.PoolName2.ReadOnly = true;
            this.PoolName2.Visible = false;
            // 
            // BhAuthor
            // 
            this.BhAuthor.DataPropertyName = "BhAuthor";
            this.BhAuthor.HeaderText = "BhAuthor";
            this.BhAuthor.Name = "BhAuthor";
            this.BhAuthor.ReadOnly = true;
            // 
            // ReturnTime
            // 
            this.ReturnTime.DataPropertyName = "ReturnTime";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.ReturnTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.ReturnTime.HeaderText = "ReturnTime";
            this.ReturnTime.Name = "ReturnTime";
            this.ReturnTime.ReadOnly = true;
            // 
            // ContractTime
            // 
            this.ContractTime.DataPropertyName = "ContractTime";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.ContractTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.ContractTime.FillWeight = 150F;
            this.ContractTime.HeaderText = "ContractTime";
            this.ContractTime.Name = "ContractTime";
            this.ContractTime.ReadOnly = true;
            this.ContractTime.Width = 150;
            // 
            // returnDate
            // 
            this.returnDate.DataPropertyName = "returnDate";
            this.returnDate.HeaderText = "returnDate";
            this.returnDate.Name = "returnDate";
            this.returnDate.ReadOnly = true;
            this.returnDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.returnDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsAlwaysGet
            // 
            this.IsAlwaysGet.DataPropertyName = "IsAlwaysGet";
            this.IsAlwaysGet.HeaderText = "Always Get";
            this.IsAlwaysGet.Name = "IsAlwaysGet";
            this.IsAlwaysGet.ReadOnly = true;
            this.IsAlwaysGet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsAlwaysGet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsExcept
            // 
            this.IsExcept.HeaderText = "Except";
            this.IsExcept.Name = "IsExcept";
            this.IsExcept.ReadOnly = true;
            this.IsExcept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsExcept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsCondittionTime
            // 
            this.IsCondittionTime.DataPropertyName = "IsCondittionTime";
            this.IsCondittionTime.HeaderText = "Condittion Time";
            this.IsCondittionTime.Name = "IsCondittionTime";
            this.IsCondittionTime.ReadOnly = true;
            this.IsCondittionTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCondittionTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsCalcCondittionTime
            // 
            this.IsCalcCondittionTime.DataPropertyName = "IsCalcCondittionTime";
            this.IsCalcCondittionTime.HeaderText = "Calc Condittion Time";
            this.IsCalcCondittionTime.Name = "IsCalcCondittionTime";
            this.IsCalcCondittionTime.ReadOnly = true;
            this.IsCalcCondittionTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCalcCondittionTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsMapByGroup
            // 
            this.IsMapByGroup.HeaderText = "Map By Group";
            this.IsMapByGroup.Name = "IsMapByGroup";
            this.IsMapByGroup.ReadOnly = true;
            // 
            // IsGiveBeneficiary
            // 
            this.IsGiveBeneficiary.DataPropertyName = "IsGiveBeneficiary";
            this.IsGiveBeneficiary.HeaderText = "Give Beneficiary";
            this.IsGiveBeneficiary.Name = "IsGiveBeneficiary";
            this.IsGiveBeneficiary.ReadOnly = true;
            this.IsGiveBeneficiary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsGiveBeneficiary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsGiveBeneficiary.Visible = false;
            // 
            // IsMapAuthor
            // 
            this.IsMapAuthor.HeaderText = "Map Author";
            this.IsMapAuthor.Name = "IsMapAuthor";
            this.IsMapAuthor.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            this.Note.FillWeight = 300F;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.Width = 300;
            // 
            // Note2
            // 
            this.Note2.DataPropertyName = "Note2";
            this.Note2.HeaderText = "Note2";
            this.Note2.Name = "Note2";
            this.Note2.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Bhmedia member";
            // 
            // cboBhMember
            // 
            this.cboBhMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBhMember.FormattingEnabled = true;
            this.cboBhMember.Location = new System.Drawing.Point(121, 23);
            this.cboBhMember.Name = "cboBhMember";
            this.cboBhMember.Size = new System.Drawing.Size(262, 20);
            this.cboBhMember.TabIndex = 35;
            // 
            // cboExceptWorkMember
            // 
            this.cboExceptWorkMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExceptWorkMember.FormattingEnabled = true;
            this.cboExceptWorkMember.Location = new System.Drawing.Point(121, 73);
            this.cboExceptWorkMember.Name = "cboExceptWorkMember";
            this.cboExceptWorkMember.Size = new System.Drawing.Size(262, 20);
            this.cboExceptWorkMember.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Except work-member";
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshData.Image")));
            this.btnRefreshData.Location = new System.Drawing.Point(400, 18);
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.Size = new System.Drawing.Size(75, 21);
            this.btnRefreshData.TabIndex = 38;
            this.btnRefreshData.Text = "Refresh";
            this.btnRefreshData.UseVisualStyleBackColor = true;
            this.btnRefreshData.Click += new System.EventHandler(this.btnRefreshData_Click);
            // 
            // cboWorkMember
            // 
            this.cboWorkMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWorkMember.FormattingEnabled = true;
            this.cboWorkMember.Location = new System.Drawing.Point(121, 48);
            this.cboWorkMember.Name = "cboWorkMember";
            this.cboWorkMember.Size = new System.Drawing.Size(262, 20);
            this.cboWorkMember.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Work-member";
            // 
            // frmDistribution2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 285);
            this.Controls.Add(this.cboWorkMember);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefreshData);
            this.Controls.Add(this.cboExceptWorkMember);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboBhMember);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusTripMain);
            this.Controls.Add(this.pcloader);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvEditFileImport);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDistribution2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribution";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDistribution2_Load);
            this.statusTripMain.ResumeLayout(false);
            this.statusTripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditFileImport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusTripMain;
        private System.Windows.Forms.ToolStripStatusLabel tssLblInfo;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.ToolStripButton sbbtnSaveDataToDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnGetDataFromDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton txtGetListExcel;
        private System.Windows.Forms.ToolStripTextBox tstxtPath;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tssLookup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridView dgvEditFileImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBhMember;
        private System.Windows.Forms.ComboBox cboExceptWorkMember;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefreshData;
        private System.Windows.Forms.ComboBox cboWorkMember;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistributionDatax;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistributionDataId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StatusLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkInNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Share;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn strContractTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beneficiary;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Royalty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetPart;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCreateReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn PoolName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BhAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContractTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAlwaysGet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExcept;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCondittionTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCalcCondittionTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMapByGroup;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsGiveBeneficiary;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMapAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note2;
    }
}