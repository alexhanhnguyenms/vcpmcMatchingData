namespace Vcpmc.Mis.AppMatching.form.Warehouse.Youtube
{
    partial class frmPreclaim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreclaim));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbOperation = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.pLeft = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.plInfo = new System.Windows.Forms.Panel();
            this.richinfo = new System.Windows.Forms.RichTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtC_Writers = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtC_Workcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtC_ISWC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtC_Title = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAsset_ID = new System.Windows.Forms.TextBox();
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
            this.serialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asset_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comp_Asset_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_ISWC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Workcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Writers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Combined_Claim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mechanical = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Performance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATED_AT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATED_AT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MONTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusMain.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.plInfo.SuspendLayout();
            this.pRight.SuspendLayout();
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
            this.lbOperation});
            this.statusMain.Location = new System.Drawing.Point(0, 357);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(684, 22);
            this.statusMain.TabIndex = 0;
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
            this.btnClearFilter});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(684, 25);
            this.toolMain.TabIndex = 0;
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
            "Asset ID",
            "Title",
            "ISWC",
            "Workcode",
            "Writer"});
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(75, 25);
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
            // pLeft
            // 
            this.pLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pLeft.Controls.Add(this.btnClear);
            this.pLeft.Controls.Add(this.plInfo);
            this.pLeft.Controls.Add(this.btnSearch);
            this.pLeft.Controls.Add(this.txtC_Writers);
            this.pLeft.Controls.Add(this.label5);
            this.pLeft.Controls.Add(this.txtC_Workcode);
            this.pLeft.Controls.Add(this.label4);
            this.pLeft.Controls.Add(this.txtC_ISWC);
            this.pLeft.Controls.Add(this.label3);
            this.pLeft.Controls.Add(this.txtC_Title);
            this.pLeft.Controls.Add(this.label2);
            this.pLeft.Controls.Add(this.txtAsset_ID);
            this.pLeft.Controls.Add(this.label1);
            this.pLeft.Location = new System.Drawing.Point(3, 22);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(200, 334);
            this.pLeft.TabIndex = 2;
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
            // plInfo
            // 
            this.plInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plInfo.Controls.Add(this.richinfo);
            this.plInfo.Location = new System.Drawing.Point(3, 222);
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
            // txtC_Writers
            // 
            this.txtC_Writers.Location = new System.Drawing.Point(3, 169);
            this.txtC_Writers.Name = "txtC_Writers";
            this.txtC_Writers.Size = new System.Drawing.Size(192, 18);
            this.txtC_Writers.TabIndex = 4;
            this.txtC_Writers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
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
            // txtC_Workcode
            // 
            this.txtC_Workcode.Location = new System.Drawing.Point(3, 133);
            this.txtC_Workcode.Name = "txtC_Workcode";
            this.txtC_Workcode.Size = new System.Drawing.Size(192, 18);
            this.txtC_Workcode.TabIndex = 3;
            this.txtC_Workcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Workcode";
            // 
            // txtC_ISWC
            // 
            this.txtC_ISWC.Location = new System.Drawing.Point(3, 97);
            this.txtC_ISWC.Name = "txtC_ISWC";
            this.txtC_ISWC.Size = new System.Drawing.Size(192, 18);
            this.txtC_ISWC.TabIndex = 2;
            this.txtC_ISWC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
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
            // txtC_Title
            // 
            this.txtC_Title.Location = new System.Drawing.Point(3, 61);
            this.txtC_Title.Name = "txtC_Title";
            this.txtC_Title.Size = new System.Drawing.Size(192, 18);
            this.txtC_Title.TabIndex = 1;
            this.txtC_Title.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
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
            // txtAsset_ID
            // 
            this.txtAsset_ID.Location = new System.Drawing.Point(3, 25);
            this.txtAsset_ID.Name = "txtAsset_ID";
            this.txtAsset_ID.Size = new System.Drawing.Size(192, 18);
            this.txtAsset_ID.TabIndex = 0;
            this.txtAsset_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntertKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Asset Id";
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
            this.pRight.Size = new System.Drawing.Size(474, 334);
            this.pRight.TabIndex = 3;
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(221, 82);
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
            this.serialNo,
            this.Asset_ID,
            this.ISRC,
            this.Comp_Asset_ID,
            this.C_Title,
            this.C_ISWC,
            this.C_Workcode,
            this.C_Writers,
            this.Combined_Claim,
            this.Mechanical,
            this.Performance,
            this.CREATED_AT,
            this.UPDATED_AT,
            this.MONTH,
            this.Year});
            this.dgvMain.Location = new System.Drawing.Point(3, 4);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(464, 297);
            this.dgvMain.TabIndex = 0;
            this.dgvMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellClick);
            this.dgvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvMain_MouseDoubleClick);
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
            // Asset_ID
            // 
            this.Asset_ID.DataPropertyName = "Asset_ID";
            this.Asset_ID.HeaderText = "Asset ID";
            this.Asset_ID.Name = "Asset_ID";
            this.Asset_ID.ReadOnly = true;
            // 
            // ISRC
            // 
            this.ISRC.DataPropertyName = "ISRC";
            this.ISRC.HeaderText = "ISRC";
            this.ISRC.Name = "ISRC";
            this.ISRC.ReadOnly = true;
            // 
            // Comp_Asset_ID
            // 
            this.Comp_Asset_ID.DataPropertyName = "Comp_Asset_ID";
            this.Comp_Asset_ID.HeaderText = "Comp Asset ID";
            this.Comp_Asset_ID.Name = "Comp_Asset_ID";
            this.Comp_Asset_ID.ReadOnly = true;
            // 
            // C_Title
            // 
            this.C_Title.DataPropertyName = "C_Title";
            this.C_Title.FillWeight = 300F;
            this.C_Title.HeaderText = "Title";
            this.C_Title.Name = "C_Title";
            this.C_Title.ReadOnly = true;
            this.C_Title.Width = 300;
            // 
            // C_ISWC
            // 
            this.C_ISWC.DataPropertyName = "C_ISWC";
            this.C_ISWC.HeaderText = "ISWC";
            this.C_ISWC.Name = "C_ISWC";
            this.C_ISWC.ReadOnly = true;
            // 
            // C_Workcode
            // 
            this.C_Workcode.DataPropertyName = "C_Workcode";
            this.C_Workcode.HeaderText = "Workcode";
            this.C_Workcode.Name = "C_Workcode";
            this.C_Workcode.ReadOnly = true;
            // 
            // C_Writers
            // 
            this.C_Writers.DataPropertyName = "C_Writers";
            this.C_Writers.FillWeight = 300F;
            this.C_Writers.HeaderText = "Writers";
            this.C_Writers.Name = "C_Writers";
            this.C_Writers.ReadOnly = true;
            this.C_Writers.Width = 300;
            // 
            // Combined_Claim
            // 
            this.Combined_Claim.DataPropertyName = "Combined_Claim";
            this.Combined_Claim.HeaderText = "Combined Claim";
            this.Combined_Claim.Name = "Combined_Claim";
            this.Combined_Claim.ReadOnly = true;
            // 
            // Mechanical
            // 
            this.Mechanical.DataPropertyName = "Mechanical";
            this.Mechanical.HeaderText = "Mechanical";
            this.Mechanical.Name = "Mechanical";
            this.Mechanical.ReadOnly = true;
            // 
            // Performance
            // 
            this.Performance.DataPropertyName = "Performance";
            this.Performance.HeaderText = "Performance";
            this.Performance.Name = "Performance";
            this.Performance.ReadOnly = true;
            // 
            // CREATED_AT
            // 
            this.CREATED_AT.DataPropertyName = "DtCREATED_AT";
            dataGridViewCellStyle1.Format = "dd/MM/yyy HH:mm:ss";
            this.CREATED_AT.DefaultCellStyle = dataGridViewCellStyle1;
            this.CREATED_AT.FillWeight = 120F;
            this.CREATED_AT.HeaderText = "Created at";
            this.CREATED_AT.Name = "CREATED_AT";
            this.CREATED_AT.ReadOnly = true;
            this.CREATED_AT.Width = 120;
            // 
            // UPDATED_AT
            // 
            this.UPDATED_AT.DataPropertyName = "DtUPDATED_AT";
            dataGridViewCellStyle2.Format = "dd/MM/yyy HH:mm:ss";
            this.UPDATED_AT.DefaultCellStyle = dataGridViewCellStyle2;
            this.UPDATED_AT.FillWeight = 120F;
            this.UPDATED_AT.HeaderText = "Update at";
            this.UPDATED_AT.Name = "UPDATED_AT";
            this.UPDATED_AT.ReadOnly = true;
            this.UPDATED_AT.Width = 120;
            // 
            // MONTH
            // 
            this.MONTH.DataPropertyName = "MONTH";
            this.MONTH.FillWeight = 80F;
            this.MONTH.HeaderText = "Month";
            this.MONTH.Name = "MONTH";
            this.MONTH.ReadOnly = true;
            this.MONTH.Width = 80;
            // 
            // Year
            // 
            this.Year.DataPropertyName = "year";
            this.Year.FillWeight = 50F;
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            this.Year.Width = 50;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmPreclaim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 379);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.statusMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPreclaim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preclaim";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPreclaim_Load);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pLeft.PerformLayout();
            this.plInfo.ResumeLayout(false);
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtC_Writers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtC_Workcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtC_ISWC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtC_Title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAsset_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripComboBox cboType;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbInfo;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtPageCurrent;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNxtPage;
        private System.Windows.Forms.Button btnFirstPAge;
        private System.Windows.Forms.Panel plInfo;
        private System.Windows.Forms.RichTextBox richinfo;
        private System.Windows.Forms.ToolStripButton btnClearFilter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choise;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asset_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISRC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comp_Asset_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_ISWC;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Workcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Writers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Combined_Claim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mechanical;
        private System.Windows.Forms.DataGridViewTextBoxColumn Performance;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATED_AT;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDATED_AT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MONTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.PictureBox pcloader;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbOperation;
    }
}