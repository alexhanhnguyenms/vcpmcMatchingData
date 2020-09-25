using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.Tool;
using Vcpmc.Mis.UnicodeConverter;

namespace Vcpmc.Mis.AppMatching.form.Tool.control
{
    public partial class frmConvertToUnsign : Form
    {
        OperationType Operation = OperationType.LoadExcel;
        List<ConvertyToUnsign> dataLoad = new List<ConvertyToUnsign>();
        string currentDirectory = "";
        public frmConvertToUnsign()
        {
            InitializeComponent();
        }

        private void btnChoiseFile_Click(object sender, EventArgs e)
        {
            try
            {
                tstxtPath.Text = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        tstxtPath.Text = filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {       
                if (tstxtPath.Text == "")
                {
                    MessageBox.Show("Please choise file to import");
                    return;
                }
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {                    
                    currentDirectory = "";
                    SaveFileDialog saveDlg = new SaveFileDialog();
                    saveDlg.InitialDirectory = @"C:\";
                    saveDlg.Filter = "Excel files (*.xls)|*.xlsx";
                    saveDlg.FilterIndex = 0;
                    saveDlg.RestoreDirectory = true;
                    saveDlg.Title = "Export Excel File To";
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        currentDirectory = saveDlg.FileName;
                    }
                    else
                    {
                        return;
                    }


                    if (currentDirectory == "")
                    {
                        MessageBox.Show("name file is empty");
                        return;
                    }

                    //OpenFileDialog folderBrowser = new OpenFileDialog();

                    //folderBrowser.ValidateNames = false;
                    //folderBrowser.CheckFileExists = false;
                    //folderBrowser.CheckPathExists = true;
                    //// Always default to Folder Selection.
                    //folderBrowser.FileName = "Folder Selection.";
                    //if (folderBrowser.ShowDialog() == DialogResult.OK)
                    //{
                    //    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    //    currentDirectory = folderPath;
                    //}
                    if (currentDirectory == "")
                    {
                        return;
                    }
                    Operation = OperationType.ExportToExcel;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
                catch (Exception)
                {


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadExcel)
                {
                    LoadDtaFromExcel();

                }               
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(currentDirectory);

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Visible = false;
        }
        #endregion
        #region data
        private void LoadDtaFromExcel()
        {
            try
            {
                dataLoad = new List<ConvertyToUnsign>();
                dgvEditFileImport.Invoke(new MethodInvoker(delegate
                {
                    dgvEditFileImport.DataSource = new List<ConvertyToUnsign>();
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Starting load data...";
                }));
                #region init               
                ExcelHelper excelHelper = new ExcelHelper();               
                #endregion
                //du lieu doc file
                dataLoad = excelHelper.ReadExcelConvertyToUnsign(tstxtPath.Text);
                //var orderByDescendingResult = (from s in ediFilesItems
                //                               //orderby s.seqNo ascending, s.NoOfPerf ascending
                //                               select s).ToList();
                #region 1.Loai bo dong trong                
                if (dataLoad != null)
                {

                    for (int i = 0; i < dataLoad.Count; i++)
                    {
                        dataLoad[i].c1 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c1.Trim().ToUpper()));
                        dataLoad[i].c2 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c2.Trim().ToUpper()));
                        dataLoad[i].c3 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c3.Trim().ToUpper()));
                        dataLoad[i].c4 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c4.Trim().ToUpper()));
                        dataLoad[i].c5 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c5.Trim().ToUpper()));
                        dataLoad[i].c6 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c6.Trim().ToUpper()));
                        dataLoad[i].c7 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c7.Trim().ToUpper()));
                        dataLoad[i].c8 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c8.Trim().ToUpper()));
                        dataLoad[i].c9 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c9.Trim().ToUpper()));
                        dataLoad[i].c10 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(dataLoad[i].c10.Trim().ToUpper()));                        
                    }
                }
                #endregion 

                #region Hien thi du lieu goc

                if (dataLoad != null)
                {                  
                   
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = dataLoad;
                    }));                    
                }
                else
                {                 
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load data from Excel file be error!";
                    }));
                }
                #endregion
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data is finish";
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       
        #endregion

        #region Export       
        
        private void ExportToExcel(string folderPath)
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Starting Export...";
                }));

                bool check = WriteReportHelper.WriteExcelConvertToUnsign(dataLoad, folderPath);
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Export data is finish";
                }));
            }
            catch (Exception ex)
            {

                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

    }
}
