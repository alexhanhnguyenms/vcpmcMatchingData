namespace Vcpmc.Mis.AppMatching.form.Admin
{
    partial class frmAssignRight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssignRight));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tgMain = new System.Windows.Forms.TabPage();
            this.cheCheckAll = new System.Windows.Forms.CheckBox();
            this.pcloader = new System.Windows.Forms.PictureBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Choise = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Claim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btCommit = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbChoise = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabMain.SuspendLayout();
            this.tgMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.toolMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tgMain);
            this.tabMain.Location = new System.Drawing.Point(2, 26);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(651, 468);
            this.tabMain.TabIndex = 0;
            // 
            // tgMain
            // 
            this.tgMain.Controls.Add(this.cheCheckAll);
            this.tgMain.Controls.Add(this.pcloader);
            this.tgMain.Controls.Add(this.dgvMain);
            this.tgMain.Location = new System.Drawing.Point(4, 21);
            this.tgMain.Name = "tgMain";
            this.tgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tgMain.Size = new System.Drawing.Size(643, 443);
            this.tgMain.TabIndex = 0;
            this.tgMain.Text = "Right list";
            this.tgMain.UseVisualStyleBackColor = true;
            // 
            // cheCheckAll
            // 
            this.cheCheckAll.AutoSize = true;
            this.cheCheckAll.Location = new System.Drawing.Point(16, 10);
            this.cheCheckAll.Name = "cheCheckAll";
            this.cheCheckAll.Size = new System.Drawing.Size(15, 14);
            this.cheCheckAll.TabIndex = 47;
            this.cheCheckAll.UseVisualStyleBackColor = true;
            this.cheCheckAll.CheckedChanged += new System.EventHandler(this.cheCheckAll_CheckedChanged);
            // 
            // pcloader
            // 
            this.pcloader.Image = ((System.Drawing.Image)(resources.GetObject("pcloader.Image")));
            this.pcloader.Location = new System.Drawing.Point(289, 191);
            this.pcloader.Name = "pcloader";
            this.pcloader.Size = new System.Drawing.Size(64, 61);
            this.pcloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcloader.TabIndex = 46;
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
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Choise,
            this.Group,
            this.Claim,
            this.Description});
            this.dgvMain.Location = new System.Drawing.Point(3, 6);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(634, 434);
            this.dgvMain.TabIndex = 45;
            this.dgvMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellClick);
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
            // Group
            // 
            this.Group.DataPropertyName = "Group";
            this.Group.FillWeight = 150F;
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            this.Group.Width = 150;
            // 
            // Claim
            // 
            this.Claim.DataPropertyName = "Claim";
            this.Claim.FillWeight = 200F;
            this.Claim.HeaderText = "Right";
            this.Claim.Name = "Claim";
            this.Claim.ReadOnly = true;
            this.Claim.Width = 200;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 200F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 200;
            // 
            // toolMain
            // 
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.toolStripSeparator2,
            this.btCommit,
            this.toolStripSeparator3});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(656, 25);
            this.toolMain.TabIndex = 14;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btCommit
            // 
            this.btCommit.Image = ((System.Drawing.Image)(resources.GetObject("btCommit.Image")));
            this.btCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCommit.Name = "btCommit";
            this.btCommit.Size = new System.Drawing.Size(80, 22);
            this.btCommit.Text = "Commit";
            this.btCommit.Click += new System.EventHandler(this.btCommit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Location = new System.Drawing.Point(6, 498);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(643, 21);
            this.lbInfo.TabIndex = 15;
            // 
            // lbChoise
            // 
            this.lbChoise.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbChoise.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbChoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChoise.Location = new System.Drawing.Point(61, 26);
            this.lbChoise.Name = "lbChoise";
            this.lbChoise.Size = new System.Drawing.Size(588, 21);
            this.lbChoise.TabIndex = 16;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoMonopoly);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundMonopolyer_RunMonopolyerCompleted);
            // 
            // frmAssignRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 523);
            this.Controls.Add(this.lbChoise);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAssignRight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Right";
            this.Load += new System.EventHandler(this.frmAssignRight_Load);
            this.tabMain.ResumeLayout(false);
            this.tgMain.ResumeLayout(false);
            this.tgMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcloader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tgMain;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton btCommit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label lbChoise;
        private System.Windows.Forms.PictureBox pcloader;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choise;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Claim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.CheckBox cheCheckAll;
    }
}