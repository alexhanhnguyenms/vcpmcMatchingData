using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vcpmc.Mis.Legacy;
using Vcpmc.Mis.UnicodeConverter;

namespace Vcpmc.Mis.AppMatching.form.convert
{
    public partial class frmConvertFont : Form
    {
       
       
        private StatusForm statusPanel;

        private string sourceEncoding;
        protected string baseDir;
        protected string[] fileNames;
        private int filterIndex;
        //private string locale;

        const string strEncoding = "SourceEncoding";
        const string strFilterIndex = "FilterIndex";
        const string strDirectory = "Directory";
        const string strDirOrFile = "DirOrFile";
        const string strLocale = "Locale";

        const string VIET = "vi-VN";
        const string ENG = "en-US";

        private const string patternOfBinaryExt = "\\.(dll|exe|jpg|jpeg|gif|tif|tiff|bmp|png|pdf)$";

        private Dictionary<string, IDocConverter> docConverters = new Dictionary<string, IDocConverter>();

        public frmConvertFont()
        {
            ////strProgName = "UnicodeConverter";

            //// Construct complete registry key.
            //string strRegKey1 = strRegKey + strProgName + ".NET";

            //// Access registry to determine which UI Language to be loaded.
            //// The desired locale must be known before initializing visual components
            //// with language text. Waiting until OnLoad would be too late.
            //RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey1);

            //if (regkey == null)
            //    regkey = Registry.CurrentUser.CreateSubKey(strRegKey1);

            //locale = (string)regkey.GetValue(strLocale, "en-US");
            //regkey.Close();

            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);

            InitializeComponent();

            statusPanel = new StatusForm(Properties.Resources.Conversion_Status);
            statusPanel.Owner = this;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
        private void frmConvertFont_Load(object sender, EventArgs e)
        {

        }

        void ControlOnDragOver(object obj, DragEventArgs dea)
        {
            if (dea.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (((dea.AllowedEffect & DragDropEffects.Copy) != 0))
                    dea.Effect = DragDropEffects.Copy;
            }
        }

        void ControlOnDragDrop(object obj, DragEventArgs dea)
        {
            if (dea.Data.GetDataPresent(DataFormats.FileDrop))
            {
                fileNames = (string[])dea.Data.GetData(DataFormats.FileDrop);
                if (fileNames.Length == 1 && Directory.Exists(fileNames[0]))
                {
                    // a directory
                    baseDir = fileNames[0];
                }
                else
                {
                    // files and/or directories
                    baseDir = new FileInfo(fileNames[0]).DirectoryName;
                }

                runTask();
            }
        }
        private void overviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (OwnedForms.Length > 1)
            //    return;

            //HtmlHelpForm helpForm = new HtmlHelpForm("readme_cs.html", this.overviewToolStripMenuItem.Text.Replace("&", string.Empty));
            //helpForm.Owner = this;
            //helpForm.Show();
        }
        

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(sourceEncoding))
            {
                MessageBox.Show(this, Properties.Resources.Select_a_Source_Encoding_option, Properties.Resources.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.chkBoxFileDir.Checked)
            {
                folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.SelectedPath = baseDir;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    baseDir = folderBrowserDialog.SelectedPath;
                    // a directory
                    fileNames = new string[1] { baseDir };

                    runTask();
                }

                return;
            }

            openFileDialog.Title = Properties.Resources.Convert_from_ + sourceEncoding;
            openFileDialog.FileName = "";
            openFileDialog.InitialDirectory = baseDir;
            openFileDialog.FilterIndex = filterIndex;

            if (openFileDialog.ShowDialog() == DialogResult.OK)	// OK of FileDialog clicked
            {
                FileInfo fileName = new FileInfo(openFileDialog.FileName);
                if (!fileName.Exists)
                {
                    MessageBox.Show(this, Properties.Resources.File_does_not_exist, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    baseDir = fileName.DirectoryName;
                    filterIndex = openFileDialog.FilterIndex;

                    // Multiselect denotes files or dir selection: T = files; F = dir
                    if (openFileDialog.Multiselect)
                    {
                        // files and/or directories
                        fileNames = openFileDialog.FileNames;
                    }
                    else
                    {
                        // a directory
                        fileNames = new string[1] { baseDir };
                    }

                    runTask();
                }
            }

            filterIndex = openFileDialog.FilterIndex;
        }

        protected void runTask()
        {
            statusPanel.Show();
            statusPanel.WindowState = FormWindowState.Normal;

            statusPanel.TextBox.AppendText("\t-- " + Properties.Resources.Beginning_of_task + " --\r\n");
            this.Cursor = Cursors.AppStarting;
            this.toolStripStatusLabel1.Text = Properties.Resources.Converting;
            this.toolStripProgressBar1.Visible = true;
            this.statusStrip1.Visible = true;
            //updateProgressBar(Properties.Resources.Converting);
            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }

        ///// <summary>
        ///// Display text on toolStripProgressBar; not work with Marquee style.
        ///// </summary>
        ///// <param name="text"></param>
        //private void updateProgressBar(string text)
        //{
        //    this.toolStripProgressBar1.ProgressBar.Refresh();
        //    using (Graphics gr = this.toolStripProgressBar1.ProgressBar.CreateGraphics())
        //    {
        //        gr.DrawString(text,
        //            SystemFonts.DefaultFont,
        //            Brushes.Black,
        //            new PointF(this.toolStripProgressBar1.Width / 2 - (gr.MeasureString(text, SystemFonts.DefaultFont).Width / 2.0F),
        //            this.toolStripProgressBar1.Height / 2 - (gr.MeasureString(text, SystemFonts.DefaultFont).Height / 2.0F)));
        //    }
        //}

        private void btnQuit_Click(object sender, EventArgs e)
        {
            OnClosed(e);
            Dispose();
            System.Environment.Exit(0);
        }

        private void chkBoxFileDir_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxFileDir.Checked)
            {
                this.btnSelect.Text = Properties.Resources.Select_Directory;
                openFileDialog.Multiselect = false;
            }
            else
            {
                this.btnSelect.Text = Properties.Resources.Select_Files;
                openFileDialog.Multiselect = true;
            }

            this.btnSelect.Focus();
        }

        private void comboBoxEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceEncoding = (string)this.comboBoxEncoding.SelectedItem;
        }

        /**
        *  Recursively traverse a directory tree converting files
        *
        */
        private void Convert(string parentDir, string[] fileNames, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Abort the operation if the user has canceled.
            // Note that a call to CancelAsync may have set 
            // CancellationPending to true just after the
            // last invocation of this method exits, so this 
            // code will not have the opportunity to set the 
            // DoWorkEventArgs.Cancel flag to true. This means
            // that RunWorkerCompletedEventArgs.Cancelled will
            // not be set to true in your RunWorkerCompleted
            // event handler. This is a race condition.

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                DirectoryInfo outputDir = new DirectoryInfo(Path.Combine(baseDir + "_Unicode", parentDir.Substring(baseDir.Length)));
                outputDir.Create();

                foreach (string name in fileNames)
                {
                    if (File.Exists(name))
                    {
                        // Process files
                        FileInfo file = new FileInfo(name);
                        string fileExtension = file.Extension.ToLower();

                        if (Regex.IsMatch(fileExtension, patternOfBinaryExt))
                        {
                            continue; // skip binary files
                        }
                        if(cheAll)
                        {
                            string[] arrsourceEncoding = new string[] { };
                            for (int i = 0; i < arrsourceEncoding.Length; i++)
                            {
                                IDocConverter docConverter = DocConverterFactory.CreateConverter(docConverters, fileExtension, sourceEncoding);
                                docConverter.Convert(outputDir, file);
                            }
                        }   
                        else
                        {
                            IDocConverter docConverter = DocConverterFactory.CreateConverter(docConverters, fileExtension, sourceEncoding);
                            docConverter.Convert(outputDir, file);
                        }   

                        worker.ReportProgress(100, file.FullName);
                    }
                    else if (Directory.Exists(name))
                    {
                        // Process subdirectories
                        Convert(name, Directory.GetFiles(name, getFilter(filterIndex)), worker, e); // files
                        Convert(name, Directory.GetDirectories(name), worker, e); // subdirectories
                    }
                }
            }
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            Convert(baseDir, fileNames, worker, e);
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool success = false;
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(e.Error.Message, resources.GetString("OutOfMemoryException"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Microsoft Excel could not be found.\nInstall it if you want to convert Excel workbooks.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("Microsoft Word could not be found.\nInstall it if you want to convert Word documents.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                //statusPanel.textBox.AppendText("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                success = true;
            }

            foreach (IDocConverter docConverter in docConverters.Values)
            {
                docConverter.Quit(); // clean up resources after each conversion session
            }
            docConverters.Clear();

            statusPanel.TextBox.AppendText("\t-- " + Properties.Resources.End_of_task + " --\r\n");
            this.Cursor = Cursors.Arrow;
            this.Focus();
            if (success)
            {
                this.toolStripStatusLabel1.Text = Properties.Resources.Conversion_completed;
                this.toolStripProgressBar1.Visible = false;
                //updateProgressBar(Properties.Resources.Conversion_completed);
                MessageBox.Show(Properties.Resources.Check_output_in_directory + "\n" + baseDir + "_Unicode", Properties.Resources.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.toolStripStatusLabel1.Text = string.Empty;
            this.statusStrip1.Visible = false;
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusPanel.TextBox.AppendText(e.UserState + "\r\n");
        }

        //TODO: Add docx, xlsx, html filters
        private string getFilter(int selectedFilter)
        {
            string filter = null;

            switch (selectedFilter)
            {
                case 1:
                    filter = "*.htm";
                    break;
                case 2:
                    filter = "*.txt";
                    break;
                case 3:
                    filter = "*.doc";
                    break;
                case 4:
                    filter = "*.xls";
                    break;
                case 5:
                    filter = "*.ppt";
                    break;
                case 6:
                    filter = "*.rtf";
                    break;
                case 7:
                    filter = "*.*";
                    break;
            }
            return filter;
        }

        private void Gui_Load(object sender, EventArgs e)
        {

        }
        bool cheAll = false;
        /// <summary>
        /// Thêm tính tăng convert mọi font sang unicode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cheConverAllToUnicode_CheckedChanged(object sender, EventArgs e)
        {
            if(cheConverAllToUnicode.Checked)
            {
                cheAll = true;
                comboBoxEncoding.Enabled = false;
            }
            else
            {
                cheAll = false;
                comboBoxEncoding.Enabled = true;
            }
        }
    }
}
