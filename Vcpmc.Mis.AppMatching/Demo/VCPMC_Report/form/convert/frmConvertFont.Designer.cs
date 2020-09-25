namespace Vcpmc.Mis.AppMatching.form.convert
{
    partial class frmConvertFont
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConvertFont));
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelEncoding = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.chkBoxFileDir = new System.Windows.Forms.CheckBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.cheConverAllToUnicode = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 14);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 134);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(251, 20);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // labelEncoding
            // 
            this.labelEncoding.AutoSize = true;
            this.labelEncoding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEncoding.Location = new System.Drawing.Point(39, 17);
            this.labelEncoding.Name = "labelEncoding";
            this.labelEncoding.Size = new System.Drawing.Size(90, 13);
            this.labelEncoding.TabIndex = 12;
            this.labelEncoding.Text = "Source Encoding:";
            // 
            // comboBoxEncoding
            // 
            this.comboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoding.FormattingEnabled = true;
            this.comboBoxEncoding.Items.AddRange(new object[] {
            "NCR",
            "TCVN3",
            "Unicode_Composite",
            "VIQR",
            "VISCII",
            "VNI",
            "VPS"});
            this.comboBoxEncoding.Location = new System.Drawing.Point(13, 31);
            this.comboBoxEncoding.Name = "comboBoxEncoding";
            this.comboBoxEncoding.Size = new System.Drawing.Size(178, 20);
            this.comboBoxEncoding.TabIndex = 8;
            this.toolTip.SetToolTip(this.comboBoxEncoding, "Source Encoding");
            this.comboBoxEncoding.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncoding_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = resources.GetString("openFileDialog.Filter");
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ReadOnlyChecked = true;
            // 
            // chkBoxFileDir
            // 
            this.chkBoxFileDir.AutoSize = true;
            this.chkBoxFileDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkBoxFileDir.Location = new System.Drawing.Point(13, 120);
            this.chkBoxFileDir.Name = "chkBoxFileDir";
            this.chkBoxFileDir.Size = new System.Drawing.Size(162, 17);
            this.chkBoxFileDir.TabIndex = 11;
            this.chkBoxFileDir.Text = "Entire &directory, including sub";
            this.chkBoxFileDir.UseVisualStyleBackColor = true;
            this.chkBoxFileDir.CheckedChanged += new System.EventHandler(this.chkBoxFileDir_CheckedChanged);
            // 
            // btnQuit
            // 
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnQuit.Location = new System.Drawing.Point(116, 93);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 21);
            this.btnQuit.TabIndex = 10;
            this.btnQuit.Text = "&Close";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSelect.Location = new System.Drawing.Point(13, 93);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(97, 21);
            this.btnSelect.TabIndex = 9;
            this.btnSelect.Text = "&Select Files";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.cheConverAllToUnicode);
            this.gbMain.Controls.Add(this.btnQuit);
            this.gbMain.Controls.Add(this.comboBoxEncoding);
            this.gbMain.Controls.Add(this.labelEncoding);
            this.gbMain.Controls.Add(this.btnSelect);
            this.gbMain.Controls.Add(this.chkBoxFileDir);
            this.gbMain.Location = new System.Drawing.Point(3, -1);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(322, 151);
            this.gbMain.TabIndex = 14;
            this.gbMain.TabStop = false;
            // 
            // cheConverAllToUnicode
            // 
            this.cheConverAllToUnicode.AutoSize = true;
            this.cheConverAllToUnicode.Checked = true;
            this.cheConverAllToUnicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cheConverAllToUnicode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cheConverAllToUnicode.Location = new System.Drawing.Point(13, 56);
            this.cheConverAllToUnicode.Name = "cheConverAllToUnicode";
            this.cheConverAllToUnicode.Size = new System.Drawing.Size(129, 17);
            this.cheConverAllToUnicode.TabIndex = 13;
            this.cheConverAllToUnicode.Text = "Convert all to Unicode";
            this.cheConverAllToUnicode.UseVisualStyleBackColor = true;
            this.cheConverAllToUnicode.CheckedChanged += new System.EventHandler(this.cheConverAllToUnicode_CheckedChanged);
            // 
            // frmConvertFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 154);
            this.Controls.Add(this.gbMain);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConvertFont";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert Font File";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmConvertFont_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ControlOnDragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ControlOnDragOver);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label labelEncoding;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox chkBoxFileDir;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ComboBox comboBoxEncoding;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.CheckBox cheConverAllToUnicode;
    }
}