namespace Vcpmc.Mis.CompareText
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
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
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboType = new System.Windows.Forms.ToolStripComboBox();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnClearFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tgMain = new System.Windows.Forms.TabPage();
            this.cheConvertCompositeToUnicode = new System.Windows.Forms.CheckBox();
            this.btnCompareText = new System.Windows.Forms.Button();
            this.lbLoad = new System.Windows.Forms.Label();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.txtPageCurrent = new System.Windows.Forms.NumericUpDown();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.btnFirstPAge = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_TITLE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreCompare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTIST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALBUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LABEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_ISWC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_WRITERS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP_CUSTOM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTILE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Workcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODE_RIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusMain.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tgMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).BeginInit();
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
            this.statusMain.Location = new System.Drawing.Point(0, 393);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(800, 22);
            this.statusMain.TabIndex = 6;
            this.statusMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // lbInfo
            // 
            this.lbInfo.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(14, 17);
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
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(58, 17);
            this.toolStripStatusLabel3.Text = "Operation: ";
            // 
            // lbOperation
            // 
            this.lbOperation.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lbOperation.ForeColor = System.Drawing.Color.Blue;
            this.lbOperation.Name = "lbOperation";
            this.lbOperation.Size = new System.Drawing.Size(14, 17);
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
            this.btnImport,
            this.toolStripSeparator1,
            this.cboType,
            this.txtFind,
            this.btnFind,
            this.btnClearFilter,
            this.toolStripSeparator2,
            this.btnExport});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(800, 25);
            this.toolMain.TabIndex = 7;
            this.toolMain.Text = "toolStrip1";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(55, 22);
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
            this.cboType.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cboType.Items.AddRange(new object[] {
            "TITLE2",
            "COMP_TITLE2",
            "C_Workcode",
            "Code"});
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(75, 25);
            this.cboType.Visible = false;
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(150, 25);
            this.txtFind.Visible = false;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 22);
            this.btnFind.Text = "Find";
            this.btnFind.Visible = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnClearFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFilter.Image")));
            this.btnClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(67, 22);
            this.btnClearFilter.Text = "Clear filter";
            this.btnClearFilter.Visible = false;
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
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(53, 22);
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tgMain);
            this.tabMain.Location = new System.Drawing.Point(0, 26);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(800, 366);
            this.tabMain.TabIndex = 8;
            // 
            // tgMain
            // 
            this.tgMain.Controls.Add(this.cheConvertCompositeToUnicode);
            this.tgMain.Controls.Add(this.btnCompareText);
            this.tgMain.Controls.Add(this.lbLoad);
            this.tgMain.Controls.Add(this.pcloader);
            this.tgMain.Controls.Add(this.lbTotalPage);
            this.tgMain.Controls.Add(this.txtPageCurrent);
            this.tgMain.Controls.Add(this.btnPrevPage);
            this.tgMain.Controls.Add(this.btnLastPage);
            this.tgMain.Controls.Add(this.btnNxtPage);
            this.tgMain.Controls.Add(this.btnFirstPAge);
            this.tgMain.Controls.Add(this.dgvMain);
            this.tgMain.Location = new System.Drawing.Point(4, 21);
            this.tgMain.Name = "tgMain";
            this.tgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tgMain.Size = new System.Drawing.Size(792, 341);
            this.tgMain.TabIndex = 0;
            this.tgMain.Text = "Common";
            this.tgMain.UseVisualStyleBackColor = true;
            // 
            // cheConvertCompositeToUnicode
            // 
            this.cheConvertCompositeToUnicode.AutoSize = true;
            this.cheConvertCompositeToUnicode.Checked = true;
            this.cheConvertCompositeToUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cheConvertCompositeToUnicode.Location = new System.Drawing.Point(89, 6);
            this.cheConvertCompositeToUnicode.Name = "cheConvertCompositeToUnicode";
            this.cheConvertCompositeToUnicode.Size = new System.Drawing.Size(165, 17);
            this.cheConvertCompositeToUnicode.TabIndex = 57;
            this.cheConvertCompositeToUnicode.Text = "Convert composite to unicode";
            this.cheConvertCompositeToUnicode.UseVisualStyleBackColor = true;
            // 
            // btnCompareText
            // 
            this.btnCompareText.Location = new System.Drawing.Point(8, 4);
            this.btnCompareText.Name = "btnCompareText";
            this.btnCompareText.Size = new System.Drawing.Size(75, 21);
            this.btnCompareText.TabIndex = 56;
            this.btnCompareText.Text = "Compare";
            this.btnCompareText.UseVisualStyleBackColor = true;
            this.btnCompareText.Click += new System.EventHandler(this.btnCompareText_Click);
            // 
            // lbLoad
            // 
            this.lbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoad.Location = new System.Drawing.Point(262, 4);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(524, 23);
            this.lbLoad.TabIndex = 55;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(364, 140);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 54;
            this.pcloader.TabStop = false;
            this.pcloader.Visible = false;
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.Location = new System.Drawing.Point(127, 326);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(22, 13);
            this.lbTotalPage.TabIndex = 53;
            this.lbTotalPage.Text = "(0)";
            // 
            // txtPageCurrent
            // 
            this.txtPageCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPageCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageCurrent.Location = new System.Drawing.Point(57, 319);
            this.txtPageCurrent.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtPageCurrent.Name = "txtPageCurrent";
            this.txtPageCurrent.Size = new System.Drawing.Size(53, 20);
            this.txtPageCurrent.TabIndex = 52;
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
            this.btnPrevPage.Location = new System.Drawing.Point(31, 318);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(22, 21);
            this.btnPrevPage.TabIndex = 50;
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
            this.btnLastPage.Location = new System.Drawing.Point(184, 319);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(22, 20);
            this.btnLastPage.TabIndex = 51;
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
            this.btnNxtPage.Location = new System.Drawing.Point(160, 318);
            this.btnNxtPage.Name = "btnNxtPage";
            this.btnNxtPage.Size = new System.Drawing.Size(22, 21);
            this.btnNxtPage.TabIndex = 48;
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
            this.btnFirstPAge.Location = new System.Drawing.Point(6, 318);
            this.btnFirstPAge.Name = "btnFirstPAge";
            this.btnFirstPAge.Size = new System.Drawing.Size(23, 21);
            this.btnFirstPAge.TabIndex = 49;
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
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNo,
            this.Source,
            this.ID,
            this.TITLE,
            this.COMP_TITLE,
            this.TITLE2,
            this.COMP_TITLE2,
            this.ScoreCompare,
            this.ARTIST,
            this.ALBUM,
            this.LABEL,
            this.ISRC,
            this.COMP_ID,
            this.COMP_ISWC,
            this.AT,
            this.COMP_WRITERS,
            this.COMP_CUSTOM_ID,
            this.QUANTILE,
            this.C_Workcode,
            this.CODE,
            this.Percent,
            this.CODE_RIGHT,
            this.Note});
            this.dgvMain.Location = new System.Drawing.Point(6, 33);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(778, 281);
            this.dgvMain.TabIndex = 47;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "Serial No";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            // 
            // Source
            // 
            this.Source.DataPropertyName = "Source";
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
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
            this.TITLE.FillWeight = 200F;
            this.TITLE.HeaderText = "TITLE";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            this.TITLE.Width = 200;
            // 
            // COMP_TITLE
            // 
            this.COMP_TITLE.DataPropertyName = "COMP_TITLE";
            this.COMP_TITLE.FillWeight = 200F;
            this.COMP_TITLE.HeaderText = "COMP_TITLE";
            this.COMP_TITLE.Name = "COMP_TITLE";
            this.COMP_TITLE.ReadOnly = true;
            this.COMP_TITLE.Width = 200;
            // 
            // TITLE2
            // 
            this.TITLE2.DataPropertyName = "TITLE2";
            this.TITLE2.FillWeight = 200F;
            this.TITLE2.HeaderText = "TITLE2";
            this.TITLE2.Name = "TITLE2";
            this.TITLE2.ReadOnly = true;
            this.TITLE2.Width = 200;
            // 
            // COMP_TITLE2
            // 
            this.COMP_TITLE2.DataPropertyName = "COMP_TITLE2";
            this.COMP_TITLE2.FillWeight = 200F;
            this.COMP_TITLE2.HeaderText = "COMP_TITLE2";
            this.COMP_TITLE2.Name = "COMP_TITLE2";
            this.COMP_TITLE2.ReadOnly = true;
            this.COMP_TITLE2.Width = 200;
            // 
            // ScoreCompare
            // 
            this.ScoreCompare.DataPropertyName = "ScoreCompare";
            this.ScoreCompare.HeaderText = "ScoreCompare";
            this.ScoreCompare.Name = "ScoreCompare";
            this.ScoreCompare.ReadOnly = true;
            // 
            // ARTIST
            // 
            this.ARTIST.DataPropertyName = "ARTIST";
            this.ARTIST.HeaderText = "ARTIST";
            this.ARTIST.Name = "ARTIST";
            this.ARTIST.ReadOnly = true;
            // 
            // ALBUM
            // 
            this.ALBUM.DataPropertyName = "ALBUM";
            this.ALBUM.HeaderText = "ALBUM";
            this.ALBUM.Name = "ALBUM";
            this.ALBUM.ReadOnly = true;
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
            // COMP_ISWC
            // 
            this.COMP_ISWC.DataPropertyName = "COMP_ISWC";
            this.COMP_ISWC.HeaderText = "COMP_ISWC";
            this.COMP_ISWC.Name = "COMP_ISWC";
            this.COMP_ISWC.ReadOnly = true;
            // 
            // AT
            // 
            this.AT.DataPropertyName = "AT";
            this.AT.HeaderText = "AT";
            this.AT.Name = "AT";
            this.AT.ReadOnly = true;
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
            // C_Workcode
            // 
            this.C_Workcode.DataPropertyName = "C_Workcode";
            this.C_Workcode.HeaderText = "C_Workcode";
            this.C_Workcode.Name = "C_Workcode";
            this.C_Workcode.ReadOnly = true;
            // 
            // CODE
            // 
            this.CODE.DataPropertyName = "CODE";
            this.CODE.HeaderText = "CODE";
            this.CODE.Name = "CODE";
            this.CODE.ReadOnly = true;
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            // 
            // CODE_RIGHT
            // 
            this.CODE_RIGHT.DataPropertyName = "CODE_RIGHT";
            this.CODE_RIGHT.HeaderText = "CODE_RIGHT";
            this.CODE_RIGHT.Name = "CODE_RIGHT";
            this.CODE_RIGHT.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.toolMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare TEXT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tgMain.ResumeLayout(false);
            this.tgMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
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
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tgMain;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.NumericUpDown txtPageCurrent;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnFirstPAge;
        private System.Windows.Forms.PictureBox pcloader;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.Button btnCompareText;
        private System.Windows.Forms.CheckBox cheConvertCompositeToUnicode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_TITLE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreCompare;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALBUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LABEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_ISWC;
        private System.Windows.Forms.DataGridViewTextBoxColumn AT;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_WRITERS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP_CUSTOM_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTILE;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Workcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODE_RIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    }
}

