using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.ApplicationCore.Entities.dis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH
{
    public partial class frmExceptionWork : Form
    {
        VcpmcContext ctx = new VcpmcContext();
        OperationType Operation = OperationType.LoadExcel;
        ExceptionWork import = null;
        List<ExceptionWorkDetail> importDetails = new List<ExceptionWorkDetail>();
        public frmExceptionWork()
        {
            InitializeComponent();
        }
        #region tool
        private void toolStripButton1_Click(object sender, EventArgs e)
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

        private void txtGetListExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void sbbtnSaveDataToDB_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Do you want to save data to database?", "Confirm Save data to Database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save
                    Operation = OperationType.SaveDatabase;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        string currentIdDsChoise = "";
        private void tsBtnGetDataFromDB_Click(object sender, EventArgs e)
        {
            try
            {
                frmExcepptionWorkChoisePO f = new frmExcepptionWorkChoisePO();
                f.ShowDialog();
                if (f.IdDsChoise != string.Empty)
                {
                    currentIdDsChoise = f.IdDsChoise;
                    Operation = OperationType.LoadDB;

                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Must choising Lis PO!");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Get data from Database be error!");
            }
        }
        #endregion
        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadExcel)
                {
                    LoadDtaFromExcel();

                }
                if (Operation == OperationType.LoadDB)
                {
                    LoadDatafromDB(currentIdDsChoise);

                }
                if (Operation == OperationType.SaveDatabase)
                {
                    SaveDataToDatabase();
                }
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }
        #endregion

        #region data
        private void LoadDtaFromExcel()
        {
            try
            {
                importDetails = new List<ExceptionWorkDetail>();
                dgvEditFileImport.Invoke(new MethodInvoker(delegate
                {
                    dgvEditFileImport.DataSource = new List<ExceptionWorkDetail>();
                }));
                import = new ExceptionWork();
                import.Name = $"EXCEPTION-WORK-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                import.TimeCreate = DateTime.Now;
                import.User = Core.User;
                import.Id = Guid.NewGuid();
                ExcelHelper excelHelper = new ExcelHelper();
                int countread = 0;
                importDetails = excelHelper.ReadExcelImportExceptionWork(import.Id, tstxtPath.Text);

                //var orderByDescendingResult = (from s in importMapWorkMemberDetails
                //                               orderby s.No ascending
                //                               select s).ToList();
                if (importDetails != null)
                {
                    countread = importDetails.Count;
                    import.TotalRecord = importDetails.Count;
                    import.Note = $"Load data from excel is successfull, Total record: {countread}";


                    import.ExceptionWorkDetails = importDetails;
                    import.TotalRecord = importDetails.Count;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = importDetails;
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = import.Note;
                    }));
                }
                else
                {                    
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Load data from Excel file be error!";
                    }));
                }
            }
            catch (Exception ex)
            {
                import = null;
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDatafromDB(string currentIdDsChoise)
        {
            try
            {
                importDetails = new List<ExceptionWorkDetail>();
                Guid guid = new Guid(currentIdDsChoise);
               
                var data = (from s in ctx.ExceptionWorkDetails
                            where s.ExceptionWorkId == guid
                            orderby s.No ascending
                            select s).ToList();
                if (data != null)
                {
                    importDetails = data;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = importDetails;
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        var item = ctx.importMapWorkMembers.FirstOrDefault(x => x.Id.ToString() == currentIdDsChoise);
                        if (item != null)
                        {
                            tssLblInfo.Text = item.Note;
                        }
                    }));
                }
                else
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("Load data from database be error!");
                }
                currentIdDsChoise = "";
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

        private void SaveDataToDatabase()
        {
            try
            {
                if (import == null || importDetails == null)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("importMapWorkMember is null or ImportMapWorkMemberDetails is null");
                }
                else
                {   
                    ctx.ExceptionWorks.Add(import);
                    foreach (var item in importDetails)
                    {
                        ctx.ExceptionWorkDetails.Add(item);
                    }
                    ctx.SaveChanges();
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Save data to database be successfull";
                    }));
                }

            }
            catch (Exception )
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show("Exist or error");
            }
        }


        #endregion

        private void frmExceptionWork_Load(object sender, EventArgs e)
        {
#if DEBUG
            //string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\template\Vinh su list.xlsx";
            //tstxtPath.Text = path;
            string path = @"D:\Solution\Source Code\Matching data\BH\template\" + @"Vinh_su_list.xlsx";
            tstxtPath.Text = path;
#endif
            
        }
    }
}
