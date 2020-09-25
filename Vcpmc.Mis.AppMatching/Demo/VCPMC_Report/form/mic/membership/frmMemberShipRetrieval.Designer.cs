namespace Vcpmc.Mis.AppMatching.form.mic.membership
{
    partial class frmMemberShipRetrieval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberShipRetrieval));
            this.btnLogin = new System.Windows.Forms.Button();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rbLog = new System.Windows.Forms.RichTextBox();
            this.gbChanelList = new System.Windows.Forms.GroupBox();
            this.dgvChannelList = new System.Windows.Forms.DataGridView();
            this.gbLog.SuspendLayout();
            this.gbChanelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 21);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.rbLog);
            this.gbLog.Location = new System.Drawing.Point(3, 70);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(910, 336);
            this.gbLog.TabIndex = 5;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rbLog
            // 
            this.rbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbLog.Location = new System.Drawing.Point(3, 14);
            this.rbLog.Name = "rbLog";
            this.rbLog.ReadOnly = true;
            this.rbLog.Size = new System.Drawing.Size(904, 319);
            this.rbLog.TabIndex = 0;
            this.rbLog.Text = "";
            // 
            // gbChanelList
            // 
            this.gbChanelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbChanelList.Controls.Add(this.dgvChannelList);
            this.gbChanelList.Location = new System.Drawing.Point(3, 29);
            this.gbChanelList.Name = "gbChanelList";
            this.gbChanelList.Size = new System.Drawing.Size(910, 36);
            this.gbChanelList.TabIndex = 4;
            this.gbChanelList.TabStop = false;
            this.gbChanelList.Text = "Youtube ";
            // 
            // dgvChannelList
            // 
            this.dgvChannelList.AllowUserToAddRows = false;
            this.dgvChannelList.AllowUserToDeleteRows = false;
            this.dgvChannelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChannelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChannelList.Location = new System.Drawing.Point(3, 14);
            this.dgvChannelList.Name = "dgvChannelList";
            this.dgvChannelList.ReadOnly = true;
            this.dgvChannelList.Size = new System.Drawing.Size(904, 19);
            this.dgvChannelList.TabIndex = 0;
            // 
            // frmMemberShipRetrieval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 407);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbChanelList);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMemberShipRetrieval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Membership Retrieval";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMemberShipRetrieval_Load);
            this.gbLog.ResumeLayout(false);
            this.gbChanelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rbLog;
        private System.Windows.Forms.GroupBox gbChanelList;
        private System.Windows.Forms.DataGridView dgvChannelList;
    }
}