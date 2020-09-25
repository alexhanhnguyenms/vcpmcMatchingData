namespace Vcpmc.Mis.AppMatching.form.masterlist
{
    partial class frmMasterImportChoise
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMasterImportChoise));
            this.dgvListPO = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPO)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListPO
            // 
            this.dgvListPO.AllowUserToAddRows = false;
            this.dgvListPO.AllowUserToDeleteRows = false;
            this.dgvListPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Namex,
            this.TimeCreate,
            this.User,
            this.TotalRecord});
            this.dgvListPO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListPO.Location = new System.Drawing.Point(3, 14);
            this.dgvListPO.Name = "dgvListPO";
            this.dgvListPO.ReadOnly = true;
            this.dgvListPO.Size = new System.Drawing.Size(947, 181);
            this.dgvListPO.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 250F;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 250;
            // 
            // Namex
            // 
            this.Namex.DataPropertyName = "Name";
            this.Namex.FillWeight = 300F;
            this.Namex.HeaderText = "Name";
            this.Namex.Name = "Namex";
            this.Namex.ReadOnly = true;
            this.Namex.Width = 300;
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
            // User
            // 
            this.User.DataPropertyName = "User";
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            // 
            // TotalRecord
            // 
            this.TotalRecord.DataPropertyName = "TotalRecord";
            this.TotalRecord.HeaderText = "TotalRecord";
            this.TotalRecord.Name = "TotalRecord";
            this.TotalRecord.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvListPO);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 198);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List PO";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(1, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "Choise";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMasterImportChoise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 228);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMasterImportChoise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MasterI mport Choise";
            this.Load += new System.EventHandler(this.frmMasterImportChoise_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPO)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namex;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRecord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}