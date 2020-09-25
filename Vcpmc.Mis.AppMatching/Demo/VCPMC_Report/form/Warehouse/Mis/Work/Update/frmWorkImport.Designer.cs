namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    partial class frmWorkImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkImport));
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbOperation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarImport = new System.Windows.Forms.ToolStripProgressBar();
            this.lbPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboReportType = new System.Windows.Forms.ToolStripComboBox();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cboType = new System.Windows.Forms.ToolStripComboBox();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnClearFilter = new System.Windows.Forms.ToolStripButton();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPageCurrent = new System.Windows.Forms.NumericUpDown();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.btnFirstPAge = new System.Windows.Forms.Button();
            this.pRight = new System.Windows.Forms.Panel();
            this.btnSysToWork = new System.Windows.Forms.Button();
            this.lbLoad = new System.Windows.Forms.Label();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WK_INT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TTL_ENG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISWC_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WRITER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WRITER2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WRITER3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTIST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOC_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbFailList = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFail = new System.Windows.Forms.Label();
            this.statusMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).BeginInit();
            this.pRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
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
            this.statusMain.Location = new System.Drawing.Point(0, 437);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(813, 22);
            this.statusMain.TabIndex = 10;
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
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboReportType,
            this.btnLoad,
            this.toolStripSeparator3,
            this.cboType,
            this.txtFind,
            this.btnFind,
            this.btnClearFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 86;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel1.Text = "Mẫu:";
            // 
            // cboReportType
            // 
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.Items.AddRange(new object[] {
            "Mẫu Chung",
            "Mẫu 1(Tác giả nước ngoài)",
            "Mẫu 2(Tác giả Việt)"});
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(150, 25);
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(53, 22);
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Items.AddRange(new object[] {
            "WK_INT_NO",
            "TTL_ENG",
            "WRITER",
            "ARTIST"});
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(150, 25);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(150, 25);
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
            // lbTotalPage
            // 
            this.lbTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.Location = new System.Drawing.Point(124, 180);
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
            this.label7.Location = new System.Drawing.Point(109, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "/";
            // 
            // txtPageCurrent
            // 
            this.txtPageCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPageCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageCurrent.Location = new System.Drawing.Point(54, 173);
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
            this.btnPrevPage.Location = new System.Drawing.Point(28, 171);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(22, 21);
            this.btnPrevPage.TabIndex = 38;
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNxtPage
            // 
            this.btnNxtPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNxtPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNxtPage.Enabled = false;
            this.btnNxtPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNxtPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNxtPage.Image")));
            this.btnNxtPage.Location = new System.Drawing.Point(157, 171);
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
            this.btnFirstPAge.Location = new System.Drawing.Point(3, 171);
            this.btnFirstPAge.Name = "btnFirstPAge";
            this.btnFirstPAge.Size = new System.Drawing.Size(23, 21);
            this.btnFirstPAge.TabIndex = 37;
            this.btnFirstPAge.UseVisualStyleBackColor = false;
            this.btnFirstPAge.Click += new System.EventHandler(this.btnFirstPAge_Click);
            // 
            // pRight
            // 
            this.pRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pRight.Controls.Add(this.btnSysToWork);
            this.pRight.Controls.Add(this.lbLoad);
            this.pRight.Controls.Add(this.pcloader);
            this.pRight.Controls.Add(this.lbTotalPage);
            this.pRight.Controls.Add(this.label7);
            this.pRight.Controls.Add(this.txtPageCurrent);
            this.pRight.Controls.Add(this.btnPrevPage);
            this.pRight.Controls.Add(this.btnLastPage);
            this.pRight.Controls.Add(this.btnNxtPage);
            this.pRight.Controls.Add(this.btnFirstPAge);
            this.pRight.Controls.Add(this.dgvMain);
            this.pRight.Location = new System.Drawing.Point(0, 28);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(813, 199);
            this.pRight.TabIndex = 87;
            // 
            // btnSysToWork
            // 
            this.btnSysToWork.Image = ((System.Drawing.Image)(resources.GetObject("btnSysToWork.Image")));
            this.btnSysToWork.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSysToWork.Location = new System.Drawing.Point(3, 3);
            this.btnSysToWork.Name = "btnSysToWork";
            this.btnSysToWork.Size = new System.Drawing.Size(95, 24);
            this.btnSysToWork.TabIndex = 47;
            this.btnSysToWork.Text = "Sync to work";
            this.btnSysToWork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSysToWork.UseVisualStyleBackColor = true;
            this.btnSysToWork.Click += new System.EventHandler(this.btnSysToWork_Click);
            // 
            // lbLoad
            // 
            this.lbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoad.Location = new System.Drawing.Point(104, 6);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(702, 21);
            this.lbLoad.TabIndex = 45;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(280, 64);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 44;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLastPage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLastPage.Enabled = false;
            this.btnLastPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastPage.Image = ((System.Drawing.Image)(resources.GetObject("btnLastPage.Image")));
            this.btnLastPage.Location = new System.Drawing.Point(181, 172);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(22, 20);
            this.btnLastPage.TabIndex = 39;
            this.btnLastPage.UseVisualStyleBackColor = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNo,
            this.WK_INT_NO,
            this.TTL_ENG,
            this.ISWC_NO,
            this.ISRC,
            this.WRITER,
            this.WRITER2,
            this.WRITER3,
            this.ARTIST,
            this.SOC_NAME});
            this.dgvMain.Location = new System.Drawing.Point(1, 30);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(805, 138);
            this.dgvMain.TabIndex = 46;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "Serial No";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            // 
            // WK_INT_NO
            // 
            this.WK_INT_NO.DataPropertyName = "WK_INT_NO";
            this.WK_INT_NO.HeaderText = "WK_INT_NO";
            this.WK_INT_NO.Name = "WK_INT_NO";
            this.WK_INT_NO.ReadOnly = true;
            // 
            // TTL_ENG
            // 
            this.TTL_ENG.DataPropertyName = "TTL_ENG";
            this.TTL_ENG.FillWeight = 300F;
            this.TTL_ENG.HeaderText = "TTL_ENG";
            this.TTL_ENG.Name = "TTL_ENG";
            this.TTL_ENG.ReadOnly = true;
            this.TTL_ENG.Width = 300;
            // 
            // ISWC_NO
            // 
            this.ISWC_NO.DataPropertyName = "ISWC_NO";
            this.ISWC_NO.HeaderText = "ISWC_NO";
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
            this.WRITER.FillWeight = 500F;
            this.WRITER.HeaderText = "WRITER";
            this.WRITER.Name = "WRITER";
            this.WRITER.ReadOnly = true;
            this.WRITER.Width = 500;
            // 
            // WRITER2
            // 
            this.WRITER2.DataPropertyName = "WRITER2";
            this.WRITER2.HeaderText = "WRITER2";
            this.WRITER2.Name = "WRITER2";
            this.WRITER2.ReadOnly = true;
            this.WRITER2.Visible = false;
            // 
            // WRITER3
            // 
            this.WRITER3.DataPropertyName = "WRITER3";
            this.WRITER3.HeaderText = "WRITER3";
            this.WRITER3.Name = "WRITER3";
            this.WRITER3.ReadOnly = true;
            this.WRITER3.Visible = false;
            // 
            // ARTIST
            // 
            this.ARTIST.DataPropertyName = "ARTIST";
            this.ARTIST.FillWeight = 500F;
            this.ARTIST.HeaderText = "ARTIST";
            this.ARTIST.Name = "ARTIST";
            this.ARTIST.ReadOnly = true;
            this.ARTIST.Width = 500;
            // 
            // SOC_NAME
            // 
            this.SOC_NAME.DataPropertyName = "SOC_NAME";
            this.SOC_NAME.HeaderText = "SOC_NAME";
            this.SOC_NAME.Name = "SOC_NAME";
            this.SOC_NAME.ReadOnly = true;
            // 
            // rbFailList
            // 
            this.rbFailList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbFailList.Location = new System.Drawing.Point(0, 246);
            this.rbFailList.Name = "rbFailList";
            this.rbFailList.Size = new System.Drawing.Size(810, 188);
            this.rbFailList.TabIndex = 88;
            this.rbFailList.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "List is fail (total records):";
            // 
            // lbFail
            // 
            this.lbFail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbFail.Location = new System.Drawing.Point(126, 230);
            this.lbFail.Name = "lbFail";
            this.lbFail.Size = new System.Drawing.Size(200, 13);
            this.lbFail.TabIndex = 90;
            // 
            // frmWorkImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 459);
            this.Controls.Add(this.lbFail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbFailList);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWorkImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work Import";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWorkImport_FormClosing);
            this.Load += new System.EventHandler(this.frmWorkImport_Load);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbOperation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar progressBarImport;
        private System.Windows.Forms.ToolStripStatusLabel lbPercent;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtPageCurrent;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnFirstPAge;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.Button btnSysToWork;
        private System.Windows.Forms.RichTextBox rbFailList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFail;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboReportType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WK_INT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TTL_ENG;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISWC_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn WRITER;
        private System.Windows.Forms.DataGridViewTextBoxColumn WRITER2;
        private System.Windows.Forms.DataGridViewTextBoxColumn WRITER3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOC_NAME;
    }
}