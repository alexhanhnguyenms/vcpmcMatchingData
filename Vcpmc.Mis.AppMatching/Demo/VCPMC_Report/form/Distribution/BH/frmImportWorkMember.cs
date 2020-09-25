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
    public partial class frmImportWorkMember : Form
    {
        VcpmcContext ctx = new VcpmcContext();
        OperationType Operation = OperationType.LoadExcel;
        ImportMapWorkMember importMapWorkMember = null;
        List<ImportMapWorkMemberDetail> importMapWorkMemberDetails = new List<ImportMapWorkMemberDetail>();
        public frmImportWorkMember()
        {
            InitializeComponent();
        }

        private void frmImportWorkMember_Load(object sender, EventArgs e)
        {
#if DEBUG
            //string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\template\BH.xlsx";
            string path = @"D:\Solution\Source Code\Matching data\BH\template\" + @"BH.xlsx";
            tstxtPath.Text = path;
#endif
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
                frmImportWorkMemberChoisePO f = new frmImportWorkMemberChoisePO();
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
                importMapWorkMemberDetails = new List<ImportMapWorkMemberDetail>();
                dgvEditFileImport.Invoke(new MethodInvoker(delegate
                {
                    dgvEditFileImport.DataSource = new List<ImportMapWorkMemberDetail>();
                }));

                importMapWorkMember = new ImportMapWorkMember();
                importMapWorkMember.Name = $"IMP-WM-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                importMapWorkMember.TimeCreate = DateTime.Now;
                importMapWorkMember.User = Core.User;
                importMapWorkMember.Id = Guid.NewGuid();
                ExcelHelper excelHelper = new ExcelHelper();
                int countread = 0;   
                importMapWorkMemberDetails = excelHelper.ReadExcelImportWorkMenber(importMapWorkMember.Id, tstxtPath.Text);


                //var orderByDescendingResult = (from s in importMapWorkMemberDetails
                //                               orderby s.No ascending
                //                               select s).ToList();
                if (importMapWorkMemberDetails != null)
                {
                    countread = importMapWorkMemberDetails.Count;
                    importMapWorkMember.TotalRecord = importMapWorkMemberDetails.Count;
                    importMapWorkMember.Note = $"Load data from excel is successfull, Total record: {countread}";


                    importMapWorkMember.ImportMapWorkMemberDetails = importMapWorkMemberDetails;
                    importMapWorkMember.TotalRecord = importMapWorkMemberDetails.Count;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = importMapWorkMemberDetails;
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = importMapWorkMember.Note;
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
                importMapWorkMember = null;
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
                importMapWorkMemberDetails  = new List<ImportMapWorkMemberDetail>();
                Guid guid = new Guid(currentIdDsChoise);
                
                var data = (from s in ctx.ImportMapWorkMemberDetails
                         where s.ImportMapWorkMemberId == guid
                            orderby s.No ascending
                         select s).ToList();
                if (data != null)
                {
                    importMapWorkMemberDetails = data;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = importMapWorkMemberDetails;                        
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        var item =  ctx.importMapWorkMembers.FirstOrDefault(x => x.Id.ToString() == currentIdDsChoise);
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
                if(importMapWorkMember == null || importMapWorkMemberDetails == null)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));                  
                    MessageBox.Show("importMapWorkMember is null or ImportMapWorkMemberDetails is null");
                }
                else
                {   
                    ctx.importMapWorkMembers.Add(importMapWorkMember);
                    foreach (var item in importMapWorkMemberDetails)
                    {
                        ctx.ImportMapWorkMemberDetails.Add(item);
                    }
                    ctx.SaveChanges();
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Save data to database be successfull";
                    }));
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


        #endregion

       
    }
}
