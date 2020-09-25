using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Vcpmc.Mis.ApplicationCore.Entities.makeData;
using Vcpmc.Mis.AppMatching.form.Distribution.BH.report;
using Vcpmc.Mis.AppMatching.printer;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Infrastructure.data;


namespace Vcpmc.Mis.AppMatching.form.Distribution.BH
{
    public partial class frmDistribution : Form
    {
        OperationDistribution operationDistribution = OperationDistribution.FileListExcel;
        List<DistributionData> distributionDatas = new List<DistributionData>();
        public frmDistribution()
        {
            InitializeComponent();
        }

        private void frmDistribution_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        #region tool
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fldrDlg = new FolderBrowserDialog())
                {
                    //fldrDlg.Filter = "Png Files (*.png)|*.png";
                    //fldrDlg.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx|CSV Files (*.csv)|*.csv"

                    if (fldrDlg.ShowDialog() == DialogResult.OK)
                    {
                        //fldrDlg.SelectedPath -- your result
                        tstxtPath.Text = fldrDlg.SelectedPath;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtGetListExcel_Click(object sender, EventArgs e)
        {
            try
            {
                operationDistribution = OperationDistribution.FileListExcel;
                stStatus.Invoke(new MethodInvoker(delegate
                {
                    stsStatus.Text = "";
                    stsStatus.ForeColor = Color.White;
                    stsStatus.BackColor = Color.Green;
                    if (tstxtPath.Text.Trim() == "")
                    {
                        stsStatus.Text = "Please choise folder path!";
                        stsStatus.ForeColor = Color.White;
                        stsStatus.BackColor = Color.Red;
                        return;
                    }
                }));
                //cheAll
                cheAll.Invoke(new MethodInvoker(delegate
                {
                    cheAll.Checked = false;
                }));
                
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void sdbtnLoadDataExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvListFile.Rows.Count > 0)
                {
                    operationDistribution = OperationDistribution.LoadDataExcel;                    

                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Don't have the list  that need to read data!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void tsBtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvListFile.Rows.Count>0)
                {
                    var x = dgvListFile.CurrentRow.Cells["Id"].Value.ToString();
                    if ((bool)dgvListFile.CurrentRow.Cells["StatusSaveDataToDatabase"].Value == true)
                    {
                        frmViewBhDistributionReport f = new frmViewBhDistributionReport(x);
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("need get data from Database");
                    }
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
                frmDistributionChoisePO f = new frmDistributionChoisePO();
                f.ShowDialog();
                if(f.IdDsChoise != string.Empty)
                {
                    currentIdDsChoise = f.IdDsChoise;
                    operationDistribution = OperationDistribution.LoadDataromDB;

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
        string[] idList = null;
        private void sbbtnSaveDataToDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save data to database?", "Confirm Save data to Database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save
                    string listID = "";
                    dgvListFile.Invoke(new MethodInvoker(delegate
                    {
                        for (int i = 0; i < dgvListFile.Rows.Count; i++)
                        {
                            if (dgvListFile.Rows[i].Cells["Check"].Value != null)
                            {
                                if ((bool)dgvListFile.Rows[i].Cells["Check"].Value == true)
                                {
                                    string id = dgvListFile.Rows[i].Cells["Id"].Value.ToString();
                                    listID += $"{id},";
                                }
                            }
                        }

                    }));
                    
                    if(listID.Length > 0)
                    {
                        listID = listID.Substring(0,listID.Length - 1);
                        idList = listID.Split(',');
                        if(idList!=null&&idList.Length > 0)
                        {
                            distibution = new Distibution();
                            distibution.Id = Guid.NewGuid();
                            distibution.User = Core.User;
                            distibution.Name = $"(DIS-{distibution.User}-{ DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss")}";

                            operationDistribution = OperationDistribution.SaveToDB;
                            stStatus.Invoke(new MethodInvoker(delegate
                            {
                                stsStatus.Text = "";
                                stsStatus.ForeColor = Color.White;
                                stsStatus.BackColor = Color.Green;
                                if (tstxtPath.Text.Trim() == "")
                                {
                                    stsStatus.Text = "Saveing data to Database...";
                                    stsStatus.ForeColor = Color.White;
                                    stsStatus.BackColor = Color.Red;
                                    return;
                                }
                            }));
                            //cheAll
                            cheAll.Invoke(new MethodInvoker(delegate
                            {
                                cheAll.Checked = false;
                            }));

                            pcloader.Visible = true;
                            pcloader.Dock = DockStyle.Fill;
                            backgroundWorker1.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("Please choise item that save data to database!");
                        }
                    }
                    
                    
                    #endregion
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Save data to database be error!");
            }
        }

        private void tsBtnPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                int totalBill = 0;
                string listID = "";
                dgvListFile.Invoke(new MethodInvoker(delegate
                {
                    for (int i = 0; i < dgvListFile.Rows.Count; i++)
                    {
                        if (dgvListFile.Rows[i].Cells["Check"].Value != null)
                        {
                            if ((bool)dgvListFile.Rows[i].Cells["Check"].Value == true)
                            {
                                string id = dgvListFile.Rows[i].Cells["Id"].Value.ToString();
                                listID += $"{id},";
                                totalBill++;
                            }
                        }
                    }

                }));

                if(totalBill == 0)
                {
                    return;
                }
                if (MessageBox.Show($"Total bill: {totalBill}, Are you sure?", "Confirm printer for distrubution bill",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save


                    if (listID.Length > 0)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                        idList = listID.Split(',');
                        if (idList != null && idList.Length > 0)
                        {

                            operationDistribution = OperationDistribution.PrinterListBill;
                            //stStatus.Invoke(new MethodInvoker(delegate
                            //{
                            //    stsStatus.Text = "";
                            //    stsStatus.ForeColor = Color.White;
                            //    stsStatus.BackColor = Color.Green;
                            //    if (tstxtPath.Text.Trim() == "")
                            //    {
                            //        stsStatus.Text = "Saveing data to Database...";
                            //        stsStatus.ForeColor = Color.White;
                            //        stsStatus.BackColor = Color.Red;
                            //        return;
                            //    }
                            //}));                           

                            pcloader.Visible = true;
                            pcloader.Dock = DockStyle.Fill;
                            backgroundWorker1.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("Please choise item that save data to database!");
                        }
                    }


                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Grid
        private void cheAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool check = cheAll.Checked;
                for (int i = 0; i < dgvListFile.Rows.Count; i++)
                {
                    dgvListFile.Rows[i].Cells[0].Value = check;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvListFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == 0)
                {
                    if (dgvListFile.CurrentRow.Cells["check"].Value != null)
                    {
                        dgvListFile.CurrentRow.Cells["check"].Value = !(bool)dgvListFile.CurrentRow.Cells["check"].Value;
                    }
                }                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Timemer
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                
                if (operationDistribution == OperationDistribution.FileListExcel)
                {
                    LoadDataListFile();

                }
                else if(operationDistribution == OperationDistribution.LoadDataExcel)
                {
                    LoadDataFromExcelFile();
                    
                }
                else if (operationDistribution == OperationDistribution.LoadDataromDB)
                {
                    if(currentIdDsChoise != string.Empty)
                    {
                        LoadDatafromDB(currentIdDsChoise);
                        currentIdDsChoise = "";
                    }
                    else
                    {

                    }
                    
                }
                else if (operationDistribution == OperationDistribution.SaveToDB)
                {
                    if(idList!=null)
                    {
                        SaveDataToDB(idList, distibution);
                        idList = null;
                    }                   

                }
                else if (operationDistribution == OperationDistribution.PrinterListBill)
                {
                    if (idList != null)
                    {
                        PrintetAll(idList);
                        idList = null;
                    }
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

        #region LoadData
        private void LoadDataListFile()
        {
            try
            {
                distributionDatas = new List<DistributionData>();
                dgvListFile.Invoke(new MethodInvoker(delegate
                {
                    dgvListFile.DataSource = new List<DistributionData>();
                }));
                string[] fileArray = Directory.GetFiles(tstxtPath.Text, "*.xls");
                if(fileArray.Length > 0)
                {
                   
                    for (int i = 0; i < fileArray.Length; i++)
                    {                        
                        distributionDatas.Add(new DistributionData
                        {
                            no = (i+1),
                            Id = Guid.NewGuid(),
                            Name = fileArray[i],
                            TimeCreate = DateTime.Now,
                            StatusLoadData = false,
                            Note = "Load infomation of the file",
                            User = Core.User
                        });
                    }
                    
                    dgvListFile.Invoke(new MethodInvoker(delegate
                    {
                        dgvListFile.DataSource = distributionDatas;
                        for (int i = 0; i < dgvListFile.RowCount; i++)
                        {
                            if (distributionDatas[i].StatusLoadData)
                            {
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Green;
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                            }
                            else
                            {
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Red;
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                            }                                                      
                        }
                    }));
                }                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }       

        private void LoadDataFromExcelFile()
        {
            try
            {
                dgvListFile.Invoke(new MethodInvoker(delegate
                {
                    for (int i = 0; i < dgvListFile.Rows.Count; i++)
                    {
                        if(dgvListFile.Rows[i].Cells["Check"].Value != null)
                        {
                            if((bool)dgvListFile.Rows[i].Cells["Check"].Value == true)
                            {
                                string id = dgvListFile.Rows[i].Cells["Id"].Value.ToString();
                                foreach (var item in distributionDatas)
                                {
                                    if(id == item.Id.ToString())
                                    {
                                        string pathFile = item.Name;
                                        List<DistributionDataItem> subitems = LoadDataFromExcelFileDetail(item.Id, item.Name);
                                        if(subitems !=null)
                                        {
                                            item.DistributionDataItems = subitems;
                                            item.StatusLoadData = true;
                                            item.TotalRecord = subitems.Count;
                                        }
                                        else
                                        {
                                            item.StatusLoadData = false;
                                        }
                                    }
                                }                        
                            }
                        }
                    }
                    
                }));

                
                dgvListFile.Invoke(new MethodInvoker(delegate
                {
                    dgvListFile.DataSource = distributionDatas;
                    for (int i = 0; i < dgvListFile.RowCount; i++)
                    {
                        if ((bool)dgvListFile.Rows[i].Cells["StatusLoadData"].Value == true)
                        {
                            dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Green;
                            dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Red;
                            dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private List<DistributionDataItem> LoadDataFromExcelFileDetail(Guid id, string filename)
        {
            try
            {
                ExcelHelper excelHelper = new ExcelHelper();
                List<DistributionDataItem> distributionDataItems = excelHelper.ReadExcelDistribution(id,filename);                
                return distributionDataItems;
            }
            catch (Exception )
            {
                return null;                
            }
        }
        Distibution distibution = null;
        private void SaveDataToDB(string[] idList, Distibution distibution)
        {
            try
            {
                
                if(idList.Length > 0 && distibution != null)
                {
                    
                    VcpmcContext vcpmcContext = new VcpmcContext();

                    //vcpmcContext.SaveChanges();
                    long count = 0;
                    for (int i = 0; i < idList.Length; i++)
                    {
                        foreach (var item in distributionDatas)
                        {
                            if (item.Id.ToString() == idList[i])
                            {                               
                                //3.save detail
                                foreach (var item2 in item.DistributionDataItems)
                                {
                                    vcpmcContext.DistributionDataItems.Add(item2);
                                    count++;
                                }
                                //2.save master2
                                //TODO
                                //item.DistibutionId = distibution.Id;
                                item.StatusSaveDataToDatabase = true;
                                vcpmcContext.DistributionDatas.Add(item);
                                break;
                            }
                        }
                        //break;
                    }
                    //
                    //1.save master
                    distibution.TotalRecord = count;
                    vcpmcContext.Distibutions.Add(distibution);

                    vcpmcContext.SaveChanges();
                    distibution = null;
                    dgvListFile.Invoke(new MethodInvoker(delegate
                    {
                        for (int i = 0; i < dgvListFile.RowCount; i++)
                        {
                            if ((bool)dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Value == true)
                            {
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.BackColor = Color.Green;
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.ForeColor = Color.White;
                            }
                            else
                            {
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.BackColor = Color.Red;
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.ForeColor = Color.White;
                            }
                        }
                    }));
                }               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDatafromDB(string currentIdDsChoise)
        {
            try
            {
                Guid guid = new Guid(currentIdDsChoise);
                distributionDatas = new List<DistributionData>();
                VcpmcContext ctx = new VcpmcContext();
                var x = (from s in ctx.DistributionDatas
                         //TODO
                         //where s.DistibutionId == guid
                         select s).ToList();
                if (x != null)
                {
                    distributionDatas = x;
                    dgvListFile.Invoke(new MethodInvoker(delegate
                    {
                        dgvListFile.DataSource = distributionDatas;
                        for (int i = 0; i < dgvListFile.RowCount; i++)
                        {
                            if ((bool)dgvListFile.Rows[i].Cells["StatusLoadData"].Value == true)
                            {
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Green;
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                            }
                            else
                            {
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.BackColor = Color.Red;
                                dgvListFile.Rows[i].Cells["statusLoadData"].Style.ForeColor = Color.White;
                            }
                            if ((bool)dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Value == true)
                            {
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.BackColor = Color.Green;
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.ForeColor = Color.White;
                            }
                            else
                            {
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.BackColor = Color.Red;
                                dgvListFile.Rows[i].Cells["StatusSaveDataToDatabase"].Style.ForeColor = Color.White;
                            }
                        }
                    }));
                    
                }
                else
                {
                    MessageBox.Show("Load data from database be error!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            
        }

        private void PrintetAll(string[] idList)
        {
            try
            {

                VcpmcContext ctx = new VcpmcContext();
                for (int i = 0; i < idList.Length; i++)
                {
                    //get data
                    string id = idList[i];
                    var x = (from s in ctx.DistributionDataItems
                             where s.DistributionDataId.ToString() == id
                             select s).ToList();
                    //report
                    var reportDatSource = new ReportDataSource("DataSet1", x);
                    ReportParameter[] reportParameters = new ReportParameter[] {
                        new ReportParameter("strdate",DateTime.Now.ToString("dd")),
                        new ReportParameter("strmonth",DateTime.Now.ToString("MM")),
                        new ReportParameter("stryear",DateTime.Now.ToString("yyy")),
                        new ReportParameter("strPeopleSign","Nguyen Van Teo"),
                     };
                    //
                    LocalReport localReport = new LocalReport();
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    //string fullPath = path+ @"\VCPMC_Report\form\report\template\DistributionReport.rdlc";
                    string fullPath = @"E:\Solution\Source Code\VCPMC_masterListDataReport\TH_solution\Demo\VCPMC_Report\form\report\template\DistributionReport.rdlc";
                    localReport.ReportPath = fullPath;
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(reportDatSource);
                    localReport.SetParameters(reportParameters);
                    PrinterHelper.PrintToPrinter(localReport, true);
                    //break;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        #endregion

        private void pcloader_Click(object sender, EventArgs e)
        {

        }
    }
}
