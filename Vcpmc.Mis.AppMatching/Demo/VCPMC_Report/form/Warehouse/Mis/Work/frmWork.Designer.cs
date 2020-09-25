namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work
{
    partial class frmWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWork));
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPageCurrent = new System.Windows.Forms.NumericUpDown();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.btnFirstPAge = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.plInfo = new System.Windows.Forms.Panel();
            this.richinfo = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtWRITER = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtISRC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtISWC_NO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTTL_ENG = new System.Windows.Forms.TextBox();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbOperation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarImport = new System.Windows.Forms.ToolStripProgressBar();
            this.lbPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboType = new System.Windows.Forms.ToolStripComboBox();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnClearFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripLabel();
            this.pLeft = new System.Windows.Forms.Panel();
            this.txtSOCIETY = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSOC_NAME = new System.Windows.Forms.TextBox();
            this.txtARTIST = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWK_INT_NO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pRight = new System.Windows.Forms.Panel();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Choise = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WK_INT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TTL_ENG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISWC_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WRITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTIST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOC_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WK_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StarRating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonopolyWorks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonopolyMembers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherTitles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterestedParties = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalWriterRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalWriterMatching = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RateWriterMatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCheckMatchingArtist = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.plInfo.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.Location = new System.Drawing.Point(124, 388);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(22, 13);
            this.lbTotalPage.TabIndex = 42;
            this.lbTotalPage.Text = "(0)";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(109, 388);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "/";
            // 
            // txtPageCurrent
            // 
            this.txtPageCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPageCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageCurrent.Location = new System.Drawing.Point(54, 381);
            this.txtPageCurrent.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtPageCurrent.Name = "txtPageCurrent";
            this.txtPageCurrent.Size = new System.Drawing.Size(53, 20);
            this.txtPageCurrent.TabIndex = 40;
            this.txtPageCurrent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPageCurrent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageCurrent_KeyPress);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevPage.Enabled = false;
            this.btnPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevPage.Image")));
            this.btnPrevPage.Location = new System.Drawing.Point(28, 379);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(22, 21);
            this.btnPrevPage.TabIndex = 38;
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLastPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLastPage.Enabled = false;
            this.btnLastPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastPage.Image = ((System.Drawing.Image)(resources.GetObject("btnLastPage.Image")));
            this.btnLastPage.Location = new System.Drawing.Point(181, 380);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(22, 20);
            this.btnLastPage.TabIndex = 39;
            this.btnLastPage.UseVisualStyleBackColor = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNxtPage
            // 
            this.btnNxtPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNxtPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNxtPage.Enabled = false;
            this.btnNxtPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNxtPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNxtPage.Image")));
            this.btnNxtPage.Location = new System.Drawing.Point(157, 379);
            this.btnNxtPage.Name = "btnNxtPage";
            this.btnNxtPage.Size = new System.Drawing.Size(22, 21);
            this.btnNxtPage.TabIndex = 36;
            this.btnNxtPage.UseVisualStyleBackColor = false;
            this.btnNxtPage.Click += new System.EventHandler(this.btnNxtPage_Click);
            // 
            // btnFirstPAge
            // 
            this.btnFirstPAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFirstPAge.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFirstPAge.Enabled = false;
            this.btnFirstPAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirstPAge.Image = ((System.Drawing.Image)(resources.GetObject("btnFirstPAge.Image")));
            this.btnFirstPAge.Location = new System.Drawing.Point(3, 379);
            this.btnFirstPAge.Name = "btnFirstPAge";
            this.btnFirstPAge.Size = new System.Drawing.Size(23, 21);
            this.btnFirstPAge.TabIndex = 37;
            this.btnFirstPAge.UseVisualStyleBackColor = false;
            this.btnFirstPAge.Click += new System.EventHandler(this.btnFirstPAge_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Choise,
            this.id,
            this.serialNo,
            this.WK_INT_NO,
            this.TTL_ENG,
            this.ISWC_NO,
            this.ISRC,
            this.WRITER,
            this.ARTIST,
            this.SOC_NAME,
            this.WK_STATUS,
            this.StarRating,
            this.MonopolyWorks,
            this.MonopolyMembers,
            this.OtherTitles,
            this.InterestedParties,
            this.TotalWriterRequest,
            this.TotalWriterMatching,
            this.RateWriterMatch,
            this.ListArtist,
            this.IsCheckMatchingArtist});
            this.dgvMain.Location = new System.Drawing.Point(3, 4);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(660, 370);
            this.dgvMain.TabIndex = 0;
            this.dgvMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellClick);
            this.dgvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvMain_MouseDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(3, 311);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 24);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // plInfo
            // 
            this.plInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plInfo.Controls.Add(this.richinfo);
            this.plInfo.Location = new System.Drawing.Point(3, 341);
            this.plInfo.Name = "plInfo";
            this.plInfo.Size = new System.Drawing.Size(190, 60);
            this.plInfo.TabIndex = 15;
            // 
            // richinfo
            // 
            this.richinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richinfo.Location = new System.Drawing.Point(1, 4);
            this.richinfo.Name = "richinfo";
            this.richinfo.ReadOnly = true;
            this.richinfo.Size = new System.Drawing.Size(182, 52);
            this.richinfo.TabIndex = 0;
            this.richinfo.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Soc name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Artist";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(102, 311);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 24);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtWRITER
            // 
            this.txtWRITER.Location = new System.Drawing.Point(3, 169);
            this.txtWRITER.Name = "txtWRITER";
            this.txtWRITER.Size = new System.Drawing.Size(192, 18);
            this.txtWRITER.TabIndex = 4;
            this.txtWRITER.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Writers";
            // 
            // txtISRC
            // 
            this.txtISRC.Location = new System.Drawing.Point(3, 133);
            this.txtISRC.Name = "txtISRC";
            this.txtISRC.Size = new System.Drawing.Size(192, 18);
            this.txtISRC.TabIndex = 3;
            this.txtISRC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "ISRC";
            // 
            // txtISWC_NO
            // 
            this.txtISWC_NO.Location = new System.Drawing.Point(3, 97);
            this.txtISWC_NO.Name = "txtISWC_NO";
            this.txtISWC_NO.Size = new System.Drawing.Size(192, 18);
            this.txtISWC_NO.TabIndex = 2;
            this.txtISWC_NO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ISWC";
            // 
            // txtTTL_ENG
            // 
            this.txtTTL_ENG.Location = new System.Drawing.Point(3, 61);
            this.txtTTL_ENG.Name = "txtTTL_ENG";
            this.txtTTL_ENG.Size = new System.Drawing.Size(192, 18);
            this.txtTTL_ENG.TabIndex = 1;
            this.txtTTL_ENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbInfo,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.lbOperation,
            this.toolStripStatusLabel4,
            this.progressBarImport,
            this.lbPercent});
            this.statusMain.Location = new System.Drawing.Point(0, 430);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(880, 22);
            this.statusMain.TabIndex = 4;
            this.statusMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // lbInfo
            // 
            this.lbInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(16, 17);
            this.lbInfo.Text = "...";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "I";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabel3.Text = "Operation: ";
            // 
            // lbOperation
            // 
            this.lbOperation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbOperation.ForeColor = System.Drawing.Color.Blue;
            this.lbOperation.Name = "lbOperation";
            this.lbOperation.Size = new System.Drawing.Size(16, 17);
            this.lbOperation.Text = "...";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // progressBarImport
            // 
            this.progressBarImport.AutoSize = false;
            this.progressBarImport.Name = "progressBarImport";
            this.progressBarImport.Size = new System.Drawing.Size(200, 16);
            // 
            // lbPercent
            // 
            this.lbPercent.AutoSize = false;
            this.lbPercent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbPercent.Name = "lbPercent";
            this.lbPercent.Size = new System.Drawing.Size(40, 17);
            // 
            // toolMain
            // 
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnImport,
            this.toolStripSeparator1,
            this.cboType,
            this.txtFind,
            this.btnFind,
            this.btnClearFilter,
            this.toolStripSeparator3,
            this.btnExport});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(880, 25);
            this.toolMain.TabIndex = 5;
            this.toolMain.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(49, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImport
            // 
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(63, 22);
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Items.AddRange(new object[] {
            "Work code",
            "Title",
            "ISWC",
            "Writer",
            "Artist",
            "Soc name"});
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(100, 25);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(250, 25);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(50, 22);
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFilter.Image")));
            this.btnClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(81, 22);
            this.btnClearFilter.Text = "Clear filter";
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(103, 22);
            this.btnExport.Text = "Export-VCPMC";
            this.btnExport.Click += new System.EventHandler(this.btnExportVcpmc_Click);
            // 
            // pLeft
            // 
            this.pLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pLeft.Controls.Add(this.txtSOCIETY);
            this.pLeft.Controls.Add(this.label10);
            this.pLeft.Controls.Add(this.txtSOC_NAME);
            this.pLeft.Controls.Add(this.txtARTIST);
            this.pLeft.Controls.Add(this.btnClear);
            this.pLeft.Controls.Add(this.plInfo);
            this.pLeft.Controls.Add(this.label8);
            this.pLeft.Controls.Add(this.label6);
            this.pLeft.Controls.Add(this.btnSearch);
            this.pLeft.Controls.Add(this.txtWRITER);
            this.pLeft.Controls.Add(this.label5);
            this.pLeft.Controls.Add(this.txtISRC);
            this.pLeft.Controls.Add(this.label4);
            this.pLeft.Controls.Add(this.txtISWC_NO);
            this.pLeft.Controls.Add(this.label3);
            this.pLeft.Controls.Add(this.txtTTL_ENG);
            this.pLeft.Controls.Add(this.label2);
            this.pLeft.Controls.Add(this.txtWK_INT_NO);
            this.pLeft.Controls.Add(this.label1);
            this.pLeft.Location = new System.Drawing.Point(3, 22);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(200, 407);
            this.pLeft.TabIndex = 6;
            // 
            // txtSOCIETY
            // 
            this.txtSOCIETY.Location = new System.Drawing.Point(3, 278);
            this.txtSOCIETY.Name = "txtSOCIETY";
            this.txtSOCIETY.Size = new System.Drawing.Size(192, 18);
            this.txtSOCIETY.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Society";
            // 
            // txtSOC_NAME
            // 
            this.txtSOC_NAME.Location = new System.Drawing.Point(3, 243);
            this.txtSOC_NAME.Name = "txtSOC_NAME";
            this.txtSOC_NAME.Size = new System.Drawing.Size(192, 18);
            this.txtSOC_NAME.TabIndex = 17;
            this.txtSOC_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // txtARTIST
            // 
            this.txtARTIST.Location = new System.Drawing.Point(3, 205);
            this.txtARTIST.Name = "txtARTIST";
            this.txtARTIST.Size = new System.Drawing.Size(192, 18);
            this.txtARTIST.TabIndex = 16;
            this.txtARTIST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title";
            // 
            // txtWK_INT_NO
            // 
            this.txtWK_INT_NO.Location = new System.Drawing.Point(3, 25);
            this.txtWK_INT_NO.Name = "txtWK_INT_NO";
            this.txtWK_INT_NO.Size = new System.Drawing.Size(192, 18);
            this.txtWK_INT_NO.TabIndex = 0;
            this.txtWK_INT_NO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work code";
            // 
            // pRight
            // 
            this.pRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pRight.Controls.Add(this.pcloader);
            this.pRight.Controls.Add(this.label9);
            this.pRight.Controls.Add(this.lbTotalPage);
            this.pRight.Controls.Add(this.label7);
            this.pRight.Controls.Add(this.txtPageCurrent);
            this.pRight.Controls.Add(this.btnPrevPage);
            this.pRight.Controls.Add(this.btnLastPage);
            this.pRight.Controls.Add(this.btnNxtPage);
            this.pRight.Controls.Add(this.btnFirstPAge);
            this.pRight.Controls.Add(this.dgvMain);
            this.pRight.Location = new System.Drawing.Point(206, 22);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(670, 407);
            this.pRight.TabIndex = 7;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(201, 97);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 44;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(207, 389);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "(Double click to view item details)";
            // 
            // Choise
            // 
            this.Choise.DataPropertyName = "Choise";
            this.Choise.FillWeight = 40F;
            this.Choise.HeaderText = "";
            this.Choise.Name = "Choise";
            this.Choise.ReadOnly = true;
            this.Choise.Width = 40;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // serialNo
            // 
            this.serialNo.DataPropertyName = "SerialNo";
            this.serialNo.FillWeight = 80F;
            this.serialNo.HeaderText = "Serial no";
            this.serialNo.Name = "serialNo";
            this.serialNo.ReadOnly = true;
            this.serialNo.Width = 80;
            // 
            // WK_INT_NO
            // 
            this.WK_INT_NO.DataPropertyName = "WK_INT_NO";
            this.WK_INT_NO.HeaderText = "Workcode";
            this.WK_INT_NO.Name = "WK_INT_NO";
            this.WK_INT_NO.ReadOnly = true;
            // 
            // TTL_ENG
            // 
            this.TTL_ENG.DataPropertyName = "TTL_ENG";
            this.TTL_ENG.FillWeight = 300F;
            this.TTL_ENG.HeaderText = "Title";
            this.TTL_ENG.Name = "TTL_ENG";
            this.TTL_ENG.ReadOnly = true;
            this.TTL_ENG.Width = 300;
            // 
            // ISWC_NO
            // 
            this.ISWC_NO.DataPropertyName = "ISWC_NO";
            this.ISWC_NO.HeaderText = "ISWC";
            this.ISWC_NO.Name = "ISWC_NO";
            this.ISWC_NO.ReadOnly = true;
            // 
            // ISRC
            // 
            this.ISRC.DataPropertyName = "ISRC";
            this.ISRC.HeaderText = "ISRC";
            this.ISRC.Name = "ISRC";
            this.ISRC.ReadOnly = true;
            // 
            // WRITER
            // 
            this.WRITER.DataPropertyName = "WRITER";
            this.WRITER.FillWeight = 300F;
            this.WRITER.HeaderText = "Writers";
            this.WRITER.Name = "WRITER";
            this.WRITER.ReadOnly = true;
            this.WRITER.Width = 300;
            // 
            // ARTIST
            // 
            this.ARTIST.DataPropertyName = "ARTIST";
            this.ARTIST.HeaderText = "ARTIST";
            this.ARTIST.Name = "ARTIST";
            this.ARTIST.ReadOnly = true;
            // 
            // SOC_NAME
            // 
            this.SOC_NAME.DataPropertyName = "SOC_NAME";
            this.SOC_NAME.HeaderText = "Soc Name";
            this.SOC_NAME.Name = "SOC_NAME";
            this.SOC_NAME.ReadOnly = true;
            // 
            // WK_STATUS
            // 
            this.WK_STATUS.DataPropertyName = "WK_STATUS";
            this.WK_STATUS.HeaderText = "Work status";
            this.WK_STATUS.Name = "WK_STATUS";
            this.WK_STATUS.ReadOnly = true;
            // 
            // StarRating
            // 
            this.StarRating.DataPropertyName = "StarRating";
            this.StarRating.HeaderText = "Star Rating";
            this.StarRating.Name = "StarRating";
            this.StarRating.ReadOnly = true;
            // 
            // MonopolyWorks
            // 
            this.MonopolyWorks.DataPropertyName = "MonopolyWorks";
            this.MonopolyWorks.HeaderText = "MonopolyWorks";
            this.MonopolyWorks.Name = "MonopolyWorks";
            this.MonopolyWorks.ReadOnly = true;
            this.MonopolyWorks.Visible = false;
            // 
            // MonopolyMembers
            // 
            this.MonopolyMembers.DataPropertyName = "MonopolyMembers";
            this.MonopolyMembers.HeaderText = "MonopolyMembers";
            this.MonopolyMembers.Name = "MonopolyMembers";
            this.MonopolyMembers.ReadOnly = true;
            this.MonopolyMembers.Visible = false;
            // 
            // OtherTitles
            // 
            this.OtherTitles.DataPropertyName = "OtherTitles";
            this.OtherTitles.HeaderText = "OtherTitles";
            this.OtherTitles.Name = "OtherTitles";
            this.OtherTitles.ReadOnly = true;
            this.OtherTitles.Visible = false;
            // 
            // InterestedParties
            // 
            this.InterestedParties.DataPropertyName = "InterestedParties";
            this.InterestedParties.HeaderText = "InterestedParties";
            this.InterestedParties.Name = "InterestedParties";
            this.InterestedParties.ReadOnly = true;
            this.InterestedParties.Visible = false;
            // 
            // TotalWriterRequest
            // 
            this.TotalWriterRequest.DataPropertyName = "TotalWriterRequest";
            this.TotalWriterRequest.HeaderText = "TotalWriterRequest";
            this.TotalWriterRequest.Name = "TotalWriterRequest";
            this.TotalWriterRequest.ReadOnly = true;
            this.TotalWriterRequest.Visible = false;
            // 
            // TotalWriterMatching
            // 
            this.TotalWriterMatching.DataPropertyName = "TotalWriterMatching";
            this.TotalWriterMatching.HeaderText = "TotalWriterMatching";
            this.TotalWriterMatching.Name = "TotalWriterMatching";
            this.TotalWriterMatching.ReadOnly = true;
            this.TotalWriterMatching.Visible = false;
            // 
            // RateWriterMatch
            // 
            this.RateWriterMatch.DataPropertyName = "RateWriterMatch";
            this.RateWriterMatch.HeaderText = "RateWriterMatch";
            this.RateWriterMatch.Name = "RateWriterMatch";
            this.RateWriterMatch.ReadOnly = true;
            this.RateWriterMatch.Visible = false;
            // 
            // ListArtist
            // 
            this.ListArtist.DataPropertyName = "ListArtist";
            this.ListArtist.HeaderText = "ListArtist";
            this.ListArtist.Name = "ListArtist";
            this.ListArtist.ReadOnly = true;
            this.ListArtist.Visible = false;
            // 
            // IsCheckMatchingArtist
            // 
            this.IsCheckMatchingArtist.DataPropertyName = "IsCheckMatchingArtist";
            this.IsCheckMatchingArtist.HeaderText = "IsCheckMatchingArtist";
            this.IsCheckMatchingArtist.Name = "IsCheckMatchingArtist";
            this.IsCheckMatchingArtist.ReadOnly = true;
            this.IsCheckMatchingArtist.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCheckMatchingArtist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsCheckMatchingArtist.Visible = false;
            // 
            // frmWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 452);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pRight);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWork_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.plInfo.ResumeLayout(false);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pLeft.PerformLayout();
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtPageCurrent;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnFirstPAge;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnClear;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel plInfo;
        private System.Windows.Forms.RichTextBox richinfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtWRITER;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtISRC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtISWC_NO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTTL_ENG;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbOperation;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWK_INT_NO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSOC_NAME;
        private System.Windows.Forms.TextBox txtARTIST;
        private System.Windows.Forms.TextBox txtSOCIETY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel btnExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar progressBarImport;
        private System.Windows.Forms.ToolStripStatusLabel lbPercent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choise;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WK_INT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TTL_ENG;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISWC_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn WRITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOC_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn WK_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn StarRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonopolyWorks;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonopolyMembers;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherTitles;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterestedParties;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWriterRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWriterMatching;
        private System.Windows.Forms.DataGridViewTextBoxColumn RateWriterMatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListArtist;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCheckMatchingArtist;
    }
}