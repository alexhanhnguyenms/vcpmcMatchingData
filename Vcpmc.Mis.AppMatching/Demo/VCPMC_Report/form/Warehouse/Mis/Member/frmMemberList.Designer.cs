namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Member
{
    partial class frmMemberList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberList));
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbOperation = new System.Windows.Forms.ToolStripStatusLabel();
            this.plInfo = new System.Windows.Forms.Panel();
            this.richinfo = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.cboType = new System.Windows.Forms.ToolStripComboBox();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnClearFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.pLeft = new System.Windows.Forms.Panel();
            this.txtSociety = new System.Windows.Forms.TextBox();
            this.txtIP_NAME_Type = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIP_ENGLISH_NAME = new System.Windows.Forms.TextBox();
            this.IP_ENGLISH_NAME = new System.Windows.Forms.Label();
            this.txtInternalNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPI_NUMBER = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pRight = new System.Windows.Forms.Panel();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPageCurrent = new System.Windows.Forms.NumericUpDown();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNxtPage = new System.Windows.Forms.Button();
            this.btnFirstPAge = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Choise = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpiNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InternalNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpEnglishName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpLocalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Society = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarImport = new System.Windows.Forms.ToolStripProgressBar();
            this.lbPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMain.SuspendLayout();
            this.plInfo.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(102, 193);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 24);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Society";
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
            this.statusMain.Location = new System.Drawing.Point(0, 357);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(804, 22);
            this.statusMain.TabIndex = 8;
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
            // plInfo
            // 
            this.plInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plInfo.Controls.Add(this.richinfo);
            this.plInfo.Location = new System.Drawing.Point(3, 223);
            this.plInfo.Name = "plInfo";
            this.plInfo.Size = new System.Drawing.Size(190, 105);
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
            this.richinfo.Size = new System.Drawing.Size(182, 97);
            this.richinfo.TabIndex = 0;
            this.richinfo.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(3, 193);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 24);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolMain
            // 
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboType,
            this.txtFind,
            this.btnFind,
            this.btnClearFilter,
            this.toolStripSeparator1,
            this.btnExport});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(804, 25);
            this.toolMain.TabIndex = 9;
            this.toolMain.Text = "toolStrip1";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Items.AddRange(new object[] {
            "IP number",
            "Internal no",
            "IP English name",
            "Name type",
            "Society"});
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(150, 25);
            // 
            // txtFind
            // 
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
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
            this.pLeft.Controls.Add(this.btnClear);
            this.pLeft.Controls.Add(this.plInfo);
            this.pLeft.Controls.Add(this.btnSearch);
            this.pLeft.Controls.Add(this.txtSociety);
            this.pLeft.Controls.Add(this.label5);
            this.pLeft.Controls.Add(this.txtIP_NAME_Type);
            this.pLeft.Controls.Add(this.label4);
            this.pLeft.Controls.Add(this.txtIP_ENGLISH_NAME);
            this.pLeft.Controls.Add(this.IP_ENGLISH_NAME);
            this.pLeft.Controls.Add(this.txtInternalNo);
            this.pLeft.Controls.Add(this.label2);
            this.pLeft.Controls.Add(this.txtIPI_NUMBER);
            this.pLeft.Controls.Add(this.label1);
            this.pLeft.Location = new System.Drawing.Point(3, 22);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(200, 334);
            this.pLeft.TabIndex = 10;
            // 
            // txtSociety
            // 
            this.txtSociety.Location = new System.Drawing.Point(3, 169);
            this.txtSociety.Name = "txtSociety";
            this.txtSociety.Size = new System.Drawing.Size(192, 18);
            this.txtSociety.TabIndex = 4;
            this.txtSociety.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // txtIP_NAME_Type
            // 
            this.txtIP_NAME_Type.Location = new System.Drawing.Point(3, 133);
            this.txtIP_NAME_Type.Name = "txtIP_NAME_Type";
            this.txtIP_NAME_Type.Size = new System.Drawing.Size(192, 18);
            this.txtIP_NAME_Type.TabIndex = 3;
            this.txtIP_NAME_Type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP name type";
            // 
            // txtIP_ENGLISH_NAME
            // 
            this.txtIP_ENGLISH_NAME.Location = new System.Drawing.Point(3, 97);
            this.txtIP_ENGLISH_NAME.Name = "txtIP_ENGLISH_NAME";
            this.txtIP_ENGLISH_NAME.Size = new System.Drawing.Size(192, 18);
            this.txtIP_ENGLISH_NAME.TabIndex = 2;
            this.txtIP_ENGLISH_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // IP_ENGLISH_NAME
            // 
            this.IP_ENGLISH_NAME.AutoSize = true;
            this.IP_ENGLISH_NAME.Location = new System.Drawing.Point(3, 82);
            this.IP_ENGLISH_NAME.Name = "IP_ENGLISH_NAME";
            this.IP_ENGLISH_NAME.Size = new System.Drawing.Size(83, 13);
            this.IP_ENGLISH_NAME.TabIndex = 4;
            this.IP_ENGLISH_NAME.Text = "IP English name";
            // 
            // txtInternalNo
            // 
            this.txtInternalNo.Location = new System.Drawing.Point(3, 61);
            this.txtInternalNo.Name = "txtInternalNo";
            this.txtInternalNo.Size = new System.Drawing.Size(192, 18);
            this.txtInternalNo.TabIndex = 1;
            this.txtInternalNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Internal No";
            // 
            // txtIPI_NUMBER
            // 
            this.txtIPI_NUMBER.Location = new System.Drawing.Point(3, 25);
            this.txtIPI_NUMBER.Name = "txtIPI_NUMBER";
            this.txtIPI_NUMBER.Size = new System.Drawing.Size(192, 18);
            this.txtIPI_NUMBER.TabIndex = 0;
            this.txtIPI_NUMBER.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IPI number";
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
            this.pRight.Size = new System.Drawing.Size(594, 334);
            this.pRight.TabIndex = 11;
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
            this.label9.Location = new System.Drawing.Point(207, 316);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "(Double click to view item details)";
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.Location = new System.Drawing.Point(124, 315);
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
            this.label7.Location = new System.Drawing.Point(109, 315);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "/";
            // 
            // txtPageCurrent
            // 
            this.txtPageCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPageCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageCurrent.Location = new System.Drawing.Point(54, 308);
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
            this.btnPrevPage.Location = new System.Drawing.Point(28, 306);
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
            this.btnLastPage.Location = new System.Drawing.Point(181, 307);
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
            this.btnNxtPage.Location = new System.Drawing.Point(157, 306);
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
            this.btnFirstPAge.Location = new System.Drawing.Point(3, 306);
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
            this.IpiNumber,
            this.InternalNo,
            this.NameType,
            this.IpEnglishName,
            this.IpLocalName,
            this.Society});
            this.dgvMain.Location = new System.Drawing.Point(3, 4);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(584, 297);
            this.dgvMain.TabIndex = 0;
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
            this.id.DataPropertyName = "Id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // IpiNumber
            // 
            this.IpiNumber.DataPropertyName = "IpiNumber";
            this.IpiNumber.HeaderText = "IPI Number";
            this.IpiNumber.Name = "IpiNumber";
            this.IpiNumber.ReadOnly = true;
            // 
            // InternalNo
            // 
            this.InternalNo.DataPropertyName = "InternalNo";
            this.InternalNo.HeaderText = "Internal No";
            this.InternalNo.Name = "InternalNo";
            this.InternalNo.ReadOnly = true;
            // 
            // NameType
            // 
            this.NameType.DataPropertyName = "NameType";
            this.NameType.HeaderText = "Name type";
            this.NameType.Name = "NameType";
            this.NameType.ReadOnly = true;
            // 
            // IpEnglishName
            // 
            this.IpEnglishName.DataPropertyName = "IpEnglishName";
            this.IpEnglishName.FillWeight = 300F;
            this.IpEnglishName.HeaderText = "IP English name";
            this.IpEnglishName.Name = "IpEnglishName";
            this.IpEnglishName.ReadOnly = true;
            this.IpEnglishName.Width = 300;
            // 
            // IpLocalName
            // 
            this.IpLocalName.DataPropertyName = "IpLocalName";
            this.IpLocalName.FillWeight = 300F;
            this.IpLocalName.HeaderText = "IP local name";
            this.IpLocalName.Name = "IpLocalName";
            this.IpLocalName.ReadOnly = true;
            this.IpLocalName.Width = 300;
            // 
            // Society
            // 
            this.Society.DataPropertyName = "Society";
            this.Society.HeaderText = "Society";
            this.Society.Name = "Society";
            this.Society.ReadOnly = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
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
            // frmMemberList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 379);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pRight);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMemberList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMemberList_Load);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.plInfo.ResumeLayout(false);
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pLeft.PerformLayout();
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbOperation;
        private System.Windows.Forms.Panel plInfo;
        private System.Windows.Forms.RichTextBox richinfo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.TextBox txtIP_NAME_Type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIP_ENGLISH_NAME;
        private System.Windows.Forms.Label IP_ENGLISH_NAME;
        private System.Windows.Forms.TextBox txtInternalNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIPI_NUMBER;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.PictureBox pcloader;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtPageCurrent;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnFirstPAge;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox txtSociety;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choise;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpiNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn InternalNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpEnglishName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IpLocalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Society;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripProgressBar progressBarImport;
        private System.Windows.Forms.ToolStripStatusLabel lbPercent;
    }
}