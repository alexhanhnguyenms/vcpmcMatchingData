namespace Vcpmc.Mis.AppMatching.form.Matching.Preclaim
{
    partial class frmPreclaimMatching
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreclaimMatching));
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPageCurrent = new System.Windows.Forms.NumericUpDown();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.btnFirstPAge = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.plInfo = new System.Windows.Forms.Panel();
            this.richinfo = new System.Windows.Forms.RichTextBox();
            this.btnMatching = new System.Windows.Forms.Button();
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
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboType = new System.Windows.Forms.ToolStripComboBox();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnClearFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.pLeft = new System.Windows.Forms.Panel();
            this.btnResetMatching = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cheTitle = new System.Windows.Forms.CheckBox();
            this.cheAssetId = new System.Windows.Forms.CheckBox();
            this.pRight = new System.Windows.Forms.Panel();
            this.lbLoad = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cheConvertCompositeToUnicode = new System.Windows.Forms.CheckBox();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTIST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTIST2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALBUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALBUM2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LABEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LABEL22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_ISWC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_WRITERS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_CUSTOM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTILE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMatching = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsSuccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).BeginInit();
            this.plInfo.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.Location = new System.Drawing.Point(124, 395);
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
            this.label7.Location = new System.Drawing.Point(109, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "/";
            // 
            // txtPageCurrent
            // 
            this.txtPageCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPageCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageCurrent.Location = new System.Drawing.Point(54, 388);
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
            this.btnPrevPage.Location = new System.Drawing.Point(28, 386);
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
            this.btnLastPage.Location = new System.Drawing.Point(181, 387);
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
            this.btnNxtPage.Location = new System.Drawing.Point(157, 386);
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
            this.btnFirstPAge.Location = new System.Drawing.Point(3, 386);
            this.btnFirstPAge.Name = "btnFirstPAge";
            this.btnFirstPAge.Size = new System.Drawing.Size(23, 21);
            this.btnFirstPAge.TabIndex = 37;
            this.btnFirstPAge.UseVisualStyleBackColor = false;
            this.btnFirstPAge.Click += new System.EventHandler(this.btnFirstPAge_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // plInfo
            // 
            this.plInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plInfo.Controls.Add(this.richinfo);
            this.plInfo.Location = new System.Drawing.Point(3, 108);
            this.plInfo.Name = "plInfo";
            this.plInfo.Size = new System.Drawing.Size(190, 300);
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
            this.richinfo.Size = new System.Drawing.Size(182, 292);
            this.richinfo.TabIndex = 0;
            this.richinfo.Text = "";
            // 
            // btnMatching
            // 
            this.btnMatching.Enabled = false;
            this.btnMatching.Image = ((System.Drawing.Image)(resources.GetObject("btnMatching.Image")));
            this.btnMatching.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatching.Location = new System.Drawing.Point(-3, 78);
            this.btnMatching.Name = "btnMatching";
            this.btnMatching.Size = new System.Drawing.Size(77, 24);
            this.btnMatching.TabIndex = 8;
            this.btnMatching.Text = "Matching";
            this.btnMatching.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMatching.UseVisualStyleBackColor = true;
            this.btnMatching.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.statusMain.Size = new System.Drawing.Size(843, 22);
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
            this.btnRefresh,
            this.toolStripSeparator1,
            this.cboType,
            this.txtFind,
            this.btnFind,
            this.btnClearFilter,
            this.toolStripSeparator2,
            this.btnExport});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(843, 25);
            this.toolMain.TabIndex = 5;
            this.toolMain.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            "Asset ID",
            "Title",
            "Workcode",
            "Artist"});
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(61, 22);
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pLeft
            // 
            this.pLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pLeft.Controls.Add(this.btnResetMatching);
            this.pLeft.Controls.Add(this.btnStop);
            this.pLeft.Controls.Add(this.cheTitle);
            this.pLeft.Controls.Add(this.cheAssetId);
            this.pLeft.Controls.Add(this.plInfo);
            this.pLeft.Controls.Add(this.btnMatching);
            this.pLeft.Location = new System.Drawing.Point(3, 22);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(200, 414);
            this.pLeft.TabIndex = 6;
            // 
            // btnResetMatching
            // 
            this.btnResetMatching.Enabled = false;
            this.btnResetMatching.Image = ((System.Drawing.Image)(resources.GetObject("btnResetMatching.Image")));
            this.btnResetMatching.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetMatching.Location = new System.Drawing.Point(3, 48);
            this.btnResetMatching.Name = "btnResetMatching";
            this.btnResetMatching.Size = new System.Drawing.Size(107, 24);
            this.btnResetMatching.TabIndex = 19;
            this.btnResetMatching.Text = "Reset matching";
            this.btnResetMatching.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnResetMatching.UseVisualStyleBackColor = true;
            this.btnResetMatching.Click += new System.EventHandler(this.btnResetMatching_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(80, 78);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(107, 24);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "Stop";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cheTitle
            // 
            this.cheTitle.AutoSize = true;
            this.cheTitle.Location = new System.Drawing.Point(5, 25);
            this.cheTitle.Name = "cheTitle";
            this.cheTitle.Size = new System.Drawing.Size(104, 17);
            this.cheTitle.TabIndex = 17;
            this.cheTitle.Text = "Matching by Title";
            this.cheTitle.UseVisualStyleBackColor = true;
            // 
            // cheAssetId
            // 
            this.cheAssetId.AutoSize = true;
            this.cheAssetId.Checked = true;
            this.cheAssetId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cheAssetId.Enabled = false;
            this.cheAssetId.Location = new System.Drawing.Point(5, 4);
            this.cheAssetId.Name = "cheAssetId";
            this.cheAssetId.Size = new System.Drawing.Size(124, 17);
            this.cheAssetId.TabIndex = 16;
            this.cheAssetId.Text = "Matching by Asset Id";
            this.cheAssetId.UseVisualStyleBackColor = true;
            // 
            // pRight
            // 
            this.pRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pRight.Controls.Add(this.lbLoad);
            this.pRight.Controls.Add(this.btnLoad);
            this.pRight.Controls.Add(this.cheConvertCompositeToUnicode);
            this.pRight.Controls.Add(this.pcloader);
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
            this.pRight.Size = new System.Drawing.Size(633, 414);
            this.pRight.TabIndex = 7;
            // 
            // lbLoad
            // 
            this.lbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoad.Location = new System.Drawing.Point(98, 6);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(523, 21);
            this.lbLoad.TabIndex = 45;
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(1, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(91, 24);
            this.btnLoad.TabIndex = 19;
            this.btnLoad.Text = "Load file";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cheConvertCompositeToUnicode
            // 
            this.cheConvertCompositeToUnicode.AutoSize = true;
            this.cheConvertCompositeToUnicode.Checked = true;
            this.cheConvertCompositeToUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cheConvertCompositeToUnicode.Location = new System.Drawing.Point(3, 30);
            this.cheConvertCompositeToUnicode.Name = "cheConvertCompositeToUnicode";
            this.cheConvertCompositeToUnicode.Size = new System.Drawing.Size(165, 17);
            this.cheConvertCompositeToUnicode.TabIndex = 19;
            this.cheConvertCompositeToUnicode.Text = "Convert composite to unicode";
            this.cheConvertCompositeToUnicode.UseVisualStyleBackColor = true;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(258, 138);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 44;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
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
            this.ID,
            this.TITLE,
            this.TITLE2,
            this.ARTIST,
            this.ARTIST2,
            this.ALBUM,
            this.ALBUM2,
            this.LABEL,
            this.LABEL22,
            this.ISRC,
            this.COMP_ID,
            this.COMP_TITLE,
            this.COMP_ISWC,
            this.COMP_WRITERS,
            this.COMP_CUSTOM_ID,
            this.QUANTILE,
            this.WorkCode,
            this.IsMatching,
            this.IsSuccess});
            this.dgvMain.Location = new System.Drawing.Point(1, 47);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(620, 336);
            this.dgvMain.TabIndex = 46;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "Serial No";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // TITLE
            // 
            this.TITLE.DataPropertyName = "TITLE";
            this.TITLE.FillWeight = 150F;
            this.TITLE.HeaderText = "TITLE";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            this.TITLE.Width = 150;
            // 
            // TITLE2
            // 
            this.TITLE2.DataPropertyName = "TITLE2";
            this.TITLE2.HeaderText = "TITLE2";
            this.TITLE2.Name = "TITLE2";
            this.TITLE2.ReadOnly = true;
            this.TITLE2.Visible = false;
            // 
            // ARTIST
            // 
            this.ARTIST.DataPropertyName = "ARTIST";
            this.ARTIST.HeaderText = "ARTIST";
            this.ARTIST.Name = "ARTIST";
            this.ARTIST.ReadOnly = true;
            // 
            // ARTIST2
            // 
            this.ARTIST2.DataPropertyName = "ARTIST2";
            this.ARTIST2.HeaderText = "ARTIST2";
            this.ARTIST2.Name = "ARTIST2";
            this.ARTIST2.ReadOnly = true;
            this.ARTIST2.Visible = false;
            // 
            // ALBUM
            // 
            this.ALBUM.DataPropertyName = "ALBUM";
            this.ALBUM.HeaderText = "ALBUM";
            this.ALBUM.Name = "ALBUM";
            this.ALBUM.ReadOnly = true;
            // 
            // ALBUM2
            // 
            this.ALBUM2.DataPropertyName = "ALBUM2";
            this.ALBUM2.HeaderText = "ALBUM2";
            this.ALBUM2.Name = "ALBUM2";
            this.ALBUM2.ReadOnly = true;
            this.ALBUM2.Visible = false;
            // 
            // LABEL
            // 
            this.LABEL.DataPropertyName = "LABEL";
            this.LABEL.FillWeight = 150F;
            this.LABEL.HeaderText = "LABEL";
            this.LABEL.Name = "LABEL";
            this.LABEL.ReadOnly = true;
            this.LABEL.Width = 150;
            // 
            // LABEL22
            // 
            this.LABEL22.DataPropertyName = "LABEL2";
            this.LABEL22.HeaderText = "LABEL2";
            this.LABEL22.Name = "LABEL22";
            this.LABEL22.ReadOnly = true;
            this.LABEL22.Visible = false;
            // 
            // ISRC
            // 
            this.ISRC.DataPropertyName = "ISRC";
            this.ISRC.HeaderText = "ISRC";
            this.ISRC.Name = "ISRC";
            this.ISRC.ReadOnly = true;
            // 
            // COMP_ID
            // 
            this.COMP_ID.DataPropertyName = "COMP_ID";
            this.COMP_ID.HeaderText = "COMP_ID";
            this.COMP_ID.Name = "COMP_ID";
            this.COMP_ID.ReadOnly = true;
            // 
            // COMP_TITLE
            // 
            this.COMP_TITLE.DataPropertyName = "COMP_TITLE";
            this.COMP_TITLE.FillWeight = 150F;
            this.COMP_TITLE.HeaderText = "COMP_TITLE";
            this.COMP_TITLE.Name = "COMP_TITLE";
            this.COMP_TITLE.ReadOnly = true;
            this.COMP_TITLE.Width = 150;
            // 
            // COMP_ISWC
            // 
            this.COMP_ISWC.DataPropertyName = "COMP_ISWC";
            this.COMP_ISWC.HeaderText = "COMP_ISWC";
            this.COMP_ISWC.Name = "COMP_ISWC";
            this.COMP_ISWC.ReadOnly = true;
            // 
            // COMP_WRITERS
            // 
            this.COMP_WRITERS.DataPropertyName = "COMP_WRITERS";
            this.COMP_WRITERS.HeaderText = "COMP_WRITERS";
            this.COMP_WRITERS.Name = "COMP_WRITERS";
            this.COMP_WRITERS.ReadOnly = true;
            // 
            // COMP_CUSTOM_ID
            // 
            this.COMP_CUSTOM_ID.DataPropertyName = "COMP_CUSTOM_ID";
            this.COMP_CUSTOM_ID.HeaderText = "COMP_CUSTOM_ID";
            this.COMP_CUSTOM_ID.Name = "COMP_CUSTOM_ID";
            this.COMP_CUSTOM_ID.ReadOnly = true;
            // 
            // QUANTILE
            // 
            this.QUANTILE.DataPropertyName = "QUANTILE";
            this.QUANTILE.HeaderText = "QUANTILE";
            this.QUANTILE.Name = "QUANTILE";
            this.QUANTILE.ReadOnly = true;
            // 
            // WorkCode
            // 
            this.WorkCode.DataPropertyName = "WorkCode";
            this.WorkCode.HeaderText = "WORK CODE";
            this.WorkCode.Name = "WorkCode";
            this.WorkCode.ReadOnly = true;
            // 
            // IsMatching
            // 
            this.IsMatching.DataPropertyName = "IsMatching";
            this.IsMatching.HeaderText = "IS MATCHING";
            this.IsMatching.Name = "IsMatching";
            this.IsMatching.ReadOnly = true;
            this.IsMatching.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsMatching.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsSuccess
            // 
            this.IsSuccess.DataPropertyName = "IsSuccess";
            this.IsSuccess.HeaderText = "IS SUCCESS";
            this.IsSuccess.Name = "IsSuccess";
            this.IsSuccess.ReadOnly = true;
            // 
            // frmPreclaimMatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 459);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pRight);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPreclaimMatching";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preclaim Matching";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreclaimMatching_FormClosing);
            this.Load += new System.EventHandler(this.frmPreclaimMatching_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel plInfo;
        private System.Windows.Forms.RichTextBox richinfo;
        private System.Windows.Forms.Button btnMatching;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbOperation;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.CheckBox cheTitle;
        private System.Windows.Forms.CheckBox cheAssetId;
        private System.Windows.Forms.CheckBox cheConvertCompositeToUnicode;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTIST2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALBUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALBUM2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LABEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn LABEL22;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_ISWC;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_WRITERS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_CUSTOM_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTILE;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMatching;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSuccess;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar progressBarImport;
        private System.Windows.Forms.ToolStripStatusLabel lbPercent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResetMatching;
    }
}