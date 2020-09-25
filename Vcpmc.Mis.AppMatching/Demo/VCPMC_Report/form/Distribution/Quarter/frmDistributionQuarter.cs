using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.form.Distribution.Quarter.Report;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Distribution.Quarter
{
    public partial class frmDistributionQuarter : Form
    {
        #region vari
        OperationType Operation = OperationType.LoadExcel;
        //string path = "";
        //string path1 = "";
        int countMaster = 0;
        //int countDetail = 0;
       
        private List<ViewModels.Mis.Distribution.Quarter.Distribution> dataImport  = new List<ViewModels.Mis.Distribution.Quarter.Distribution>();
        DistributionViewModel dataMaster = new DistributionViewModel();
        int year = 2020;
        int quarter = 2;
        string regions = "";
        string intNo = "";
        string currentDirectory = "";
        string pathReportPDF = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\" + $"DistributionQuarterReportP.rdlc";
        string pathReportExcel = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\" + $"DistributionQuarterReportE.rdlc";
        int currentExport = 0;
        //int exportFrom = 0;
        //int exportTo = 0;
        bool isLoadMasterType = true;
        string pathMaster = string.Empty;
        string pathDetail = string.Empty;
        WorkController controller;
        WorkApiClient workApiClient;
        bool isLoadMaster = true;
        bool isLoadDetail = false;
        bool isSync = false;
        #endregion

        #region Load
        public frmDistributionQuarter()
        {
            InitializeComponent();
        }
        private void frmDistributionQuarter_Load(object sender, EventArgs e)
        {
            year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            switch (month)
            {
                case 1:
                case 2:
                case 3:               
                    quarter = 1;
                    cboQuater.SelectedIndex = quarter - 1;
                    break;
                case 4:
                case 5:
                case 6:               
                    quarter = 2;
                    cboQuater.SelectedIndex = quarter - 1;
                    break;
                case 7:
                case 8:
                case 9:                
                    quarter = 3;
                    cboQuater.SelectedIndex = quarter - 1;
                    break;
                case 10:
                case 11:
                case 12:               
                    quarter = 4;
                    cboQuater.SelectedIndex = quarter - 1;
                    break;
                default:
                    quarter = 1;
                    cboQuater.SelectedIndex = quarter - 1;
                    break;
            }
            cboRegons.SelectedIndex = 0;           
#if DEBUG
            //txtPath.Text = @"D:\Solution\Source Code\Matching data\data example\Phan phoi\2002\total\PP2002 - STT.xlsx";
            //txtPath1.Text = @"D:\Solution\Source Code\Matching data\data example\Phan phoi\2002\details";
#endif
            workApiClient = new WorkApiClient(Core.Client);
            controller = new WorkController(workApiClient);
        }

        #endregion

        #region timer      
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadExcel)
                {
                    if(isLoadMasterType)
                    {
                        LoadDataMaster(year,quarter);
                    }
                    else
                    {
                        LoadDataDetail(intNo, year, quarter);
                    }
                }
                else if (Operation == OperationType.SaveAllPdf)
                {
                    SaveAll(currentDirectory, "PDF");
                }
                else if (Operation == OperationType.SaveExcel)
                {
                    SaveAll(currentDirectory, "Excel");
                }
                else if (Operation == OperationType.SysnData)
                {
                    SysnData();
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

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }
        #endregion

        #region btn
        private void numYear_ValueChanged(object sender, EventArgs e)
        {
            year = (int)numYear.Value;
        }
        private void cboQuater_SelectedIndexChanged(object sender, EventArgs e)
        {
            quarter = cboQuater.SelectedIndex + 1;
        }
        private void cboRegons_SelectedIndexChanged(object sender, EventArgs e)
        {
            regions = cboRegons.Text;
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                //txtPath.Text = "";
                //var filePath = string.Empty;
                //using (var fbd = new FolderBrowserDialog())
                //{
                //    DialogResult result = fbd.ShowDialog();

                //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                //    {
                //        filePath = fbd.SelectedPath;
                //        txtPath.Text = filePath;

                //    }
                //}
                txtPath.Text = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        txtPath.Text = filePath;
                        pathMaster = txtPath.Text;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }

        }
        private void btnBrowser2_Click(object sender, EventArgs e)
        {
            try
            {                
                var filePath = string.Empty;
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        filePath = fbd.SelectedPath;
                        txtPath1.Text = filePath;
                        pathDetail = txtPath1.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }
        }
        private void btnMatch_Click(object sender, EventArgs e)
        {

        }
        private void btnLoadMasterList_Click(object sender, EventArgs e)
        {
            try
            {
                if(isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                pathMaster = "";
                if (txtPath.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("input master file path!");
                    return;
                }
                if (txtPath1.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("input detail folder path!");
                    return;
                }
                if(dataMaster!=null)
                {
                    if(dataMaster.Items!=null)
                    {
                        dataMaster.Items.Clear();
                        dataMaster.Items = null;
                    }
                    dataMaster = null;
                    GC.Collect();
                    dataMaster = new DistributionViewModel();                   
                    dgvMaster.DataSource = dataMaster.Items;
                    dgvMaster.Invalidate();                   
                    dgvDetail.DataSource = new List<DistributionDetails>();
                    dgvDetail.Invalidate();
                }
                pathMaster = txtPath.Text;
                pathDetail = txtPath1.Text;
                year = (int)numYear.Value;
                quarter = cboQuater.SelectedIndex + 1;
                regions = cboRegons.Text;
                isLoadMasterType = true;                
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
        private void btnLoadDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                if (dataImport == null || dataImport.Count == 0)
                {
                    MessageBox.Show("Master data is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.None,
                                        MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
                    return;
                }
                year = (int)numYear.Value;
                quarter = cboQuater.SelectedIndex + 1;
                regions = cboRegons.Text;
                isLoadMaster = false;
                //path = txtPath.Text.Trim();
                //path1 = txtPath1.Text.Trim();
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
                if (isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                currentDirectory = "";
                OpenFileDialog folderBrowser = new OpenFileDialog();                
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Folder Selection.";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    currentDirectory = folderPath;
                }
                if (currentDirectory == "")
                {
                    return;
                }                
                Operation = OperationType.SaveAllPdf;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception)
            {


            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                currentExport = 0;
                //string[] text = cboExportFrom.Text.Trim().Split('-');
                //exportFrom = int.Parse(text[0]);
                //exportTo = int.Parse(text[1]);

                currentDirectory = "";
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Folder Selection.";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    currentDirectory = folderPath;
                }
                if (currentDirectory == "")
                {
                    return;
                }
                Operation = OperationType.SaveExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception)
            {


            }
        }
        private void tssViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                if (dataMaster != null || dataMaster.Items.Count != 0)
                {
                    int SerialNo = int.Parse(dgvMaster.CurrentRow.Cells["SerialNo"].Value.ToString().Trim());
                    var detailData = dataMaster.Items.Where(p => p.SerialNo == SerialNo).FirstOrDefault();
                    if (dataMaster != null)
                    {
                        //dgvDetail.DataSource = detailData.Items;
                        frmDistrisbutionQuarterReport f = new frmDistrisbutionQuarterReport(pathReportPDF);
                        f.dataSource = detailData;                        
                        f.ShowDialog();
                    }
                }                
            }
            catch (Exception)
            {


            }
        }
        #endregion

        #region Function
        private void LoadDataMaster(int year, int quarter)
        {
            try
            {
                if (pathMaster == string.Empty || pathDetail == string.Empty) return;
                DateTime startTime = DateTime.Now;
                countMaster = 0;
                if (dataImport!=null)
                {
                    dataImport.Clear();
                }
                if (dataMaster != null)
                {
                    dataMaster.Items.Clear();
                }
                #region Read file
                ExcelHelper excelHelper = new ExcelHelper();
                dataMaster  = excelHelper.ReadExcelImportDistributionQuaterMaster(year,quarter, pathMaster);
                if(dataMaster != null)
                {
                    dataMaster.Items = dataMaster.Items.OrderBy(p => p.SerialNo).ToList();
                    dataImport = dataMaster.Items;
                }
                #endregion              

                //dataImport = JsonHelper.JsonToPreclaim(path, year, month);
                if (dataImport != null && dataImport.Count > 0)
                {
                    countMaster = dataImport.Count;
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Total master records loading: {1}";
                    }));
                    #region lay file detail
                    string[] files = Directory.GetFiles(pathDetail);
                    foreach (var item in files)
                    {
                        string[] list_name = item.Split('\\');
                        string name = "";
                        if (list_name.Length > 0)
                        {
                            name = list_name.Last().Split('.')[0];
                            string[] arrayStr = name.Split(' ');
                            if (arrayStr.Length > 0)
                            {
                                string intNo = arrayStr[arrayStr.Length - 1];
                                var data = dataImport.Where(p => p.IntNo == intNo).FirstOrDefault();
                                if (data != null)
                                {
                                    if(data.Path != string.Empty)
                                    {
                                        data.Path += ";";
                                    }
                                    data.Path += item;
                                    data.NameFile = name;
                                }
                            }
                        }
                    }
                    #endregion
                    dgvMaster.Invoke(new MethodInvoker(delegate
                    {
                        dgvMaster.DataSource = dataImport;
                    }));
                    for (int i = 0; i < dgvMaster.Rows.Count; i++)
                    {
                        if (dgvMaster.Rows[i].Cells["Pathx"].Value.ToString() == string.Empty)
                        {
                            dgvMaster.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }
                else
                { 
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load master data from excel file be error!";
                    }));
                    
                }
                DateTime endTime = DateTime.Now;
                lbInfoImport.Invoke(new MethodInvoker(delegate
                {
                    lbInfoImport.Text = $"Time load(second(s)): {(endTime - startTime).TotalSeconds.ToString("##0.##")}, ";
                    lbInfoImport.Text += $"total master record(s): {countMaster}";
                    
                }));

                #region load detail
                LoadDataDetail(intNo, year, quarter);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.None,
                                         MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
        }
        private void LoadDataDetail(string intNo, int year, int quarter)
        {
            try
            {
                isLoadDetail = true;
                DateTime startTime = DateTime.Now;
                //countDetail = 0;                
                foreach (var item in dataMaster.Items)
                {
                    item.Items.Clear();
                }
                #region Read file
                ExcelHelper excelHelper = new ExcelHelper();
                dataMaster = excelHelper.ReadExcelImportDistributionQuaterDetail(dataMaster);
                if (dataMaster != null)
                {
                    dataMaster.Items = dataMaster.Items.OrderBy(p => p.SerialNo).ToList();
                    dataImport = dataMaster.Items;
                }
                #endregion
                //dataImport = JsonHelper.JsonToPreclaim(path, year, month);
                if (dataImport != null && dataImport.Count > 0)
                {
                    countMaster = dataImport.Count;
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Total detail records loading: {1}";
                    }));                    
                    dgvMaster.Invoke(new MethodInvoker(delegate
                    {
                        dgvMaster.DataSource = dataImport;
                    }));
                    for (int i = 0; i < dgvMaster.Rows.Count; i++)
                    {
                        if (dgvMaster.Rows[i].Cells["Pathx"].Value.ToString() == string.Empty ||
                            (bool)dgvMaster.Rows[i].Cells["IsLoadDetail"].Value==false )
                        {
                            dgvMaster.Rows[i].DefaultCellStyle.ForeColor = Color.Red;

                        }
                    }
                }
                else
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load detail data from excel file be error!";
                    }));

                }
                DateTime endTime = DateTime.Now;
                lbInfoImport.Invoke(new MethodInvoker(delegate
                {
                    lbInfoImport.Text = $"Time load(second(s)): {(endTime - startTime).TotalSeconds.ToString("##0.##")}, ";
                    lbInfoImport.Text += $"total detail record(s): {countMaster}";

                }));
                isLoadDetail = false;
            }
            catch (Exception)
            {
                isLoadDetail = false;
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
        }
        private bool MakeLink(string folderPath, DistributionViewModel data, int year, int quarter, string regions)
        {
            bool check = false;
            try
            {
                
                check = WriteReportHelper.MakLinkDistributionQuarter(data, folderPath,
                    $"_PP{year.ToString().Substring(0,2)}{quarter.ToString().PadLeft(2,'0')}", year, quarter, regions);

            }
            catch (Exception ex)
            {
                check = false;
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
            return check;
        }
        private void SaveAll(string folderPath, string typeExport)
        {
            try
            {
                currentExport = 0;
                if (dataMaster==null)
                {
                    return;
                }
                string path = "";
                //1.xuat detail
                ///var source = dataMaster.Items.Skip(exportFrom-1).Take(1000).ToList(); 
                LocalReport localReport = new LocalReport();       
                foreach (var item in dataMaster.Items)
                {
                    localReport.DataSources.Clear();
                    currentExport++;
                    if (item.Items.Count == 0)
                    {
                        continue;
                    }
                    Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter.Distribution exportS =
                        (Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter.Distribution)item.Clone();
                    //Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter.Distribution exportS = new ViewModels.Mis.Distribution.Quarter.Distribution();
                    if (typeExport == "PDF")
                    {
                        path = $"{folderPath}\\{item.NameFile}.pdf";
                        SaveAll(localReport, exportS, path, typeExport);
                    }
                    else
                    {
                        path = $"{folderPath}\\{item.NameFile}.xls";
                        SaveAll(localReport, exportS, path, typeExport);
                    }
                    exportS.Items.Clear();
                    exportS.Items = null;
                    exportS = null;
                    //localReport.ReleaseSandboxAppDomain();                    
                }                
                //if(exportTo>= dataMaster.Items.Count)
                //{
                if (typeExport != "PDF")
                {
                    MakeLink(folderPath, dataMaster, year, quarter, regions);
                }
                //}
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    //lbOperation.Text = $"Export from {exportFrom} to {exportFrom}, total: {currentExport}";
                    lbOperation.Text = $"Export, total: {currentExport}";
                }));
                localReport.Dispose();
                localReport = null;
                GC.Collect();
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
        private void SaveAll(LocalReport localReport,ViewModels.Mis.Distribution.Quarter.Distribution dataSource, string pathfull, string typeExport)
        {
            try
            {
                //khong co du lieu, bo qua
                if (dataSource == null)
                {
                    return;
                }
                var reportDatSource = new ReportDataSource("DataSet1", dataSource.Items);
                ReportParameter[] reportParameters = new ReportParameter[] {
                    new ReportParameter("IP", dataSource.IntNo),
                    new ReportParameter("NAME",dataSource.Member),
                    new ReportParameter("IPI_NAME_NO",dataSource.IPINameNo),
                    new ReportParameter("IPI_BASE_NO", dataSource.IPIBaseNo),
                    new ReportParameter("SERIAL_NO",dataSource.SerialNo.ToString()),
                    new ReportParameter("TOTAL", dataSource.TotalRoyalty.ToString()),
                };
               
                //localReport.      
                if (typeExport == "PDF")
                {
                    //LocalReport localReport = new LocalReport();
                    localReport.ReportPath = pathReportPDF;// pathfull;
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(reportDatSource);
                    localReport.SetParameters(reportParameters);
                    var data = localReport.Render(typeExport); // Excel PDF
                    FileStream newFile = new FileStream(pathfull, FileMode.Create);
                    newFile.Write(data, 0, data.Length);
                    data = null;
                    newFile.Close();
                    newFile.Dispose();
                    newFile = null;
                    //localReport = null;
                }
                else
                {
                    //LocalReport localReport = new LocalReport();
                    localReport.ReportPath = pathReportExcel;// pathfull;
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(reportDatSource);
                    localReport.SetParameters(reportParameters);
                    var data = localReport.Render(typeExport); // Excel PDF
                    FileStream newFile = new FileStream(pathfull, FileMode.Create);
                    newFile.Write(data, 0, data.Length);
                    newFile.Close();
                    newFile.Dispose();
                    newFile = null;
                    //localReport = null;
                }
                reportDatSource = null;
                reportParameters = null;
                //localReport.DataSources.Clear();
                //localReport.Dispose();
                //localReport = null;
                //GC.Collect();
                //GC.SuppressFinalize(localReport);
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

        #region dgv
        private void dgvMaster_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if(dgvMaster.Rows.Count == 0)
                {
                    return;
                }    
                if (dataMaster!=null || dataMaster.Items.Count!=0)
                {
                    int SerialNo = int.Parse(dgvMaster.CurrentRow.Cells["SerialNo"].Value.ToString().Trim());
                    var detailData = dataMaster.Items.Where(p => p.SerialNo == SerialNo).FirstOrDefault();
                    if(dataMaster!=null)
                    {
                        dgvDetail.DataSource = detailData.Items;
                        lbDetail.Invoke(new MethodInvoker(delegate
                        {
                            lbDetail.Text = $"total detail record(s): {detailData.Items.Count}, total royalty: {detailData.TotalRoyalty.ToString("###,###,###")}";
                        }));
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.None,
                                         MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST

            }
        }
        #endregion

        #region sysToWork
        private void btnSysToWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoadMaster)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load master...";
                    }));
                }
                if (isLoadDetail)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load detail...";
                    }));
                }
                if (isSync)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                }
                if (dataMaster == null || dataMaster.Items == null || dataMaster.Items.Count == 0)
                {
                    MessageBox.Show("Data is empty, so not sync to Work");
                    return;
                }
                DialogResult = MessageBox.Show("Are you sure to sync data to Work?", "SYNC Confirm", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    Operation = OperationType.SysnData;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception)
            {


            }
        }

        private async void SysnData()
        {
            try
            {                
                if (dataMaster == null || dataMaster.Items == null || dataMaster.Items.Count == 0)
                {                  
                    return;
                }
                isSync = true;
                int totalSuccess = 0;
                int total = 0;
                for (int i = 0; i < dataMaster.Items.Count; i++)
                {
                    total += dataMaster.Items[i].Items.Count();
                }
                if (total == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Proccessing sync is finish";
                        lbOperation.Text = "Data is empty, so not sync to work";
                    }));
                    isSync = false;
                    return;
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Sync...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Proccessing sync...";
                }));
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = false;
                }));
                btnMatch.Invoke(new MethodInvoker(delegate
                {
                    btnMatch.Enabled = false;
                }));
                btnLoadMasterList.Invoke(new MethodInvoker(delegate
                {
                    btnLoadMasterList.Enabled = false;
                }));
                //btnLoadDetails.Invoke(new MethodInvoker(delegate
                //{
                //    btnLoadDetails.Enabled = false;
                //}));
                btnExportExcel.Invoke(new MethodInvoker(delegate
                {
                    btnExportExcel.Enabled = false;
                }));      
                
                DateTime startTime = DateTime.Now;
                

                for (int i = 0; i < dataMaster.Items.Count; i++)
                {
                    WorkChangeListRequest request = new WorkChangeListRequest();
                    string codeWriter = VnHelper.ConvertToUnSign(dataMaster.Items[i].IntNo.ToUpper());
                    string nameWriter = VnHelper.ConvertToUnSign(dataMaster.Items[i].Member.ToUpper());
                    foreach (var item in dataMaster.Items[i].Items)
                    {
                        if(!request.Items.Where(p=>p.WK_INT_NO == item.WorkIntNo.ToUpper()).Any())
                        {
                            WorkCreateRequest itmUpdate = new WorkCreateRequest();
                            itmUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(item.WorkIntNo.ToUpper());
                            itmUpdate.TTL_ENG = VnHelper.ConvertToUnSign(item.Title.Trim().ToUpper());
                            //vi other title giong voi title nen khong can them
                            //itmUpdate.OtherTitles.Add(new Shared.Mis.Works.OtherTitle
                            //{
                            //    No = 1,
                            //    Title = itmUpdate.TTL_ENG
                            //}) ;                            
                            itmUpdate.InterestedParties.Add(new Shared.Mis.Works.InterestedParty
                            {
                                No = 1,
                                IP_INT_NO = codeWriter,
                                IP_NAME = nameWriter,
                                IP_WK_ROLE = item.Role,
                                WK_STATUS = "COMPLETE",

                                PER_OWN_SHR = item.Share,
                                PER_COL_SHR = item.Share,

                                MEC_OWN_SHR = item.Share,
                                MEC_COL_SHR = item.Share,

                                SP_SHR = item.Share,
                                TOTAL_MEC_SHR = item.Share,

                                SYN_OWN_SHR = item.Share,
                                SYN_COL_SHR = item.Share,
                                Society = "",//rong se khong cap nhat
                                CountUpdate = 1,
                                LastUpdateAt = DateTime.Now,
                                LastChoiseAt = DateTime.Now,
                                IP_NUMBER = dataMaster.Items[i].IPINameNo,
                                IP_NAME_LOCAL = "",//rong se khong cap nhat
                                IP_NAMETYPE = "",//rong se khong cap nhat
                            });
                            itmUpdate.WRITER = nameWriter;
                            itmUpdate.ARTIST = string.Empty;
                            itmUpdate.SOC_NAME = string.Empty;
                            itmUpdate.WK_STATUS = "COMPLETE";
                            request.Items.Add(itmUpdate);
                        }                                             
                    }                    
                    //request.Items
                    var data = await controller.ChangeList(request);
                    statusMain.Invoke(new MethodInvoker(delegate
                    {                      
                        float values = (float)(i+1) / (float)dataMaster.Items.Count * 100;
                        progressBarImport.Value = (int)values;
                        lbPercent.Text = $"{((int)values).ToString()}%";
                    }));
                    totalSuccess += data.Items.Where(p=>p.Status == Utilities.Common.UpdateStatus.Successfull).ToList().Count;
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = $"Sync..., total sync success/total: {totalSuccess}/{total}";
                    }));
                }                
                #region update Ui when finish
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = true;
                }));
                btnMatch.Invoke(new MethodInvoker(delegate
                {
                    btnMatch.Enabled = true;
                }));
                btnLoadMasterList.Invoke(new MethodInvoker(delegate
                {
                    btnLoadMasterList.Enabled = true;
                }));
                //btnLoadDetails.Invoke(new MethodInvoker(delegate
                //{
                //    btnLoadDetails.Enabled = true;
                //}));

                btnExportExcel.Invoke(new MethodInvoker(delegate
                {
                    btnExportExcel.Enabled = true;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"sync tracking work to work be finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                    lbInfo.Text += $", total sync success/total: {totalSuccess}/{total}";
                }));
                
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Sync is finish";
                }));
                #endregion
                isSync = false;
            }
            catch (Exception)
            {
                isSync = false;
                if (btnMatch != null && !btnMatch.IsDisposed)
                {
                    btnMatch.Invoke(new MethodInvoker(delegate
                    {
                        btnMatch.Enabled = true;
                    }));
                }
                if (btnLoadMasterList != null && !btnLoadMasterList.IsDisposed)
                {
                    btnLoadMasterList.Invoke(new MethodInvoker(delegate
                    {
                        btnLoadMasterList.Enabled = true;
                    }));
                }
                //if (btnLoadDetails != null && !btnLoadDetails.IsDisposed)
                //{
                //    btnLoadDetails.Invoke(new MethodInvoker(delegate
                //    {
                //        btnLoadDetails.Enabled = true;
                //    }));
                //}
                if (btnExportExcel != null && !btnExportExcel.IsDisposed)
                {
                    btnExportExcel.Invoke(new MethodInvoker(delegate
                    {
                        btnExportExcel.Enabled = true;
                    }));
                }
                if (btnSysToWork != null && !btnSysToWork.IsDisposed)
                {
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = true;
                    }));
                }
                if (lbOperation != null && !lbInfo.IsDisposed)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = $"Sync tracking work to work be failure";
                    }));
                }
            }
        }
        #endregion

    }
}
