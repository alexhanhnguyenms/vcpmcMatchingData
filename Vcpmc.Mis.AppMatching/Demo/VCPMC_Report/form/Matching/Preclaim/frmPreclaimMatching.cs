using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Youtube;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Youtube;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.AppMatching.form.Matching.Preclaim
{
    public partial class frmPreclaimMatching : Form
    {
        #region vari        
        //PreclaimViewModel CurrenObject = null;
        PreclaimController preclaimController;
        PreclaimApiClient preclaimApiClient;
        //request matching
        PreclaimMatchingListRequest request = new PreclaimMatchingListRequest();
        /// <summary>
        /// Dữ liệu trả về từ matching
        /// </summary>
        ApiResult<PagedResult<PreclaimViewModel>> dataReponse = new ApiResult<PagedResult<PreclaimViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        string filepath = string.Empty;
        /// <summary>
        /// du lieu load tu excel
        /// </summary>
        List<PreclaimMatchingViewModel> dataLoad = new List<PreclaimMatchingViewModel>();
        /// <summary>
        /// Dữ liệu đang hiển thị
        /// </summary>
        List<PreclaimMatchingViewModel> CurrentDataView = new List<PreclaimMatchingViewModel>();
        int currentPage = 1;
        int totalPage = 0;
        //int year = -1;
        //int MONTH = -1;
        bool isMatchingByTitle = false;
        bool isConverCompositeToUnicode = true;
        bool isStop = false;
        #endregion

        #region init
        public frmPreclaimMatching()
        {
            InitializeComponent();
            preclaimApiClient = new PreclaimApiClient(Core.Client);
            preclaimController = new PreclaimController(preclaimApiClient);
        }

        private void frmPreclaimMatching_Load(object sender, EventArgs e)
        {
            //cboType.SelectedIndex = 0;
            //cboMonths.SelectedIndex = 0;
            //numYear.Value = DateTime.Now.Year;
        }
        private bool closePending;
        private void frmPreclaimMatching_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                closePending = true;
                backgroundWorker1.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;   // or this.Hide()
                return;
            }
            //base.OnFormClosing(e);
            dataLoad = null;
            GC.Collect();
        }
        #endregion

        #region LoadData
        private void LoadExcel()
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data...";
                }));
                isStop = false;
                dataLoad = new List<PreclaimMatchingViewModel>();
                CurrentDataView = new List<PreclaimMatchingViewModel>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                }));
                DateTime starttime = DateTime.Now;
                ExcelHelper excelHelper = new ExcelHelper();
                //dataLoad = excelHelper.ReadExcelImportPreClaimMatching(filepath);
                //dataLoad = CsvReadHelper.ReadCSVPreClaimMatching(filepath);
                dataLoad = null;
                GC.Collect();
                dataLoad = CsvReadHelper.ReadUnicodePreClaimMatching(filepath);
                excelHelper = null;
                DateTime endtime = DateTime.Now;
                DateTime starttimeConvert = DateTime.Now;
                DateTime endtimeConvert = DateTime.Now;
                if (dataLoad!=null)
                {
                    #region Convert to unicode
                    if(isConverCompositeToUnicode)
                    {
                       
                        ConvertToUnicode(dataLoad);
                        endtimeConvert = DateTime.Now;
                    }
                    #endregion                   
                    currentPage = 1;
                    if (dataLoad.Count % Core.LimitDisplayDGV == 0)
                    {
                        totalPage = dataLoad.Count/Core.LimitDisplayDGV;
                    }
                    else
                    {
                        totalPage = dataLoad.Count / Core.LimitDisplayDGV + 1;
                    }
                    lbTotalPage.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage.Text = totalPage.ToString();
                    }));
                    txtPageCurrent.Invoke(new MethodInvoker(delegate
                    {
                        txtPageCurrent.Value = currentPage;
                    }));

                    dataLoad = dataLoad.OrderBy(p => p.SerialNo).ToList();
                    CurrentDataView = dataLoad.Skip((currentPage - 1)* Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                    EnablePagging(currentPage,totalPage);
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = CurrentDataView;
                    }));
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Total time load: {(endtime - starttime).TotalSeconds}(s), total record(s): {dataLoad.Count}";
                        if (isConverCompositeToUnicode)
                        {
                            lbLoad.Text += $", total time convert to Unicode: {(endtimeConvert - starttimeConvert).TotalSeconds}(s)";
                        }
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Load data is finish";
                    }));
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled =true;                        
                    }));
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = true;
                    }));
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExport.Enabled = true;
                    }));
                }
                else
                {                   
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Load data is error!";
                    }));
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = false;
                    }));
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = false;
                    }));
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExport.Enabled = false;
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Load data is finish";
                    }));
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load data is error: {ex.ToString()}");
            }
        }
        private async void Mathching()
        {
            try
            {
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = true;
                    isStop = false;
                }));
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = false;                   
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Matching...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                //int totalMacthingSuccess = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                }));
                if (dataLoad==null)
                {
                    dataLoad = new List<PreclaimMatchingViewModel>();
                }
                if(dataLoad.Count ==0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Data load is empty, so not matching, please input data source";
                    }));
                    return;
                }
                else
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Proccessing matching...";
                    }));
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = false;
                    }));
                }               
                int totalRequest = 0;
                if(dataLoad.Count%Core.LimitMatchingPreclaimRequest==0)
                {
                    totalRequest = dataLoad.Where(p => p.IsMatching == false).ToList().Count / Core.LimitMatchingPreclaimRequest;
                }
                else
                {
                    totalRequest = dataLoad.Where(p => p.IsMatching == false).ToList().Count / Core.LimitMatchingPreclaimRequest + 1;
                }
                int index = 0;
                int currentTimeRequest = 0;               
                while (index < dataLoad.Count)
                {
                    if(isStop)
                    {
                        goto END;
                    }
                    DateTime startTime = DateTime.Now;
                    currentTimeRequest++;                    
                    #region Creat request
                    //TODO: sua tu in index thanh 0=> vi ismathcing da bo qua
                    var blockRequest = dataLoad.Where(p=>p.IsMatching==false).Skip(0).Take(Core.LimitMatchingPreclaimRequest).ToList();
                    PreclaimMatchingRequest itemRequest;
                    request.Items.Clear();
                    for (int i = 0; i < blockRequest.Count; i++)
                    {
                        if (blockRequest[i].ID != string.Empty)
                        {
                            itemRequest = new PreclaimMatchingRequest();
                            itemRequest.AssetId = blockRequest[i].ID;
                            request.Items.Add(itemRequest);
                            //check
                            blockRequest[i].IsMatching = true;
                        }                       
                    }
                    //request.Year = year;
                    //request.MONTH =MONTH;
                    request.Total = request.Items.Count;
                    index += Core.LimitMatchingPreclaimRequest;
                    if (index > dataLoad.Count)
                    {
                        index = dataLoad.Count;
                    }
                    #endregion

                    #region Matching                   
                    dataReponse = await preclaimController.MatchingPreclaim(request);
                    if(dataReponse!=null&&dataReponse.ResultObj!=null&&dataReponse.ResultObj.Items.Count>0)
                    {
                        for (int i = 0; i < blockRequest.Count; i++)
                        {
                            if (blockRequest[i].ID == string.Empty)
                            {
                                continue;
                            }
                            var list = dataReponse.ResultObj.Items.Where(p => p.Asset_ID == blockRequest[i].ID).ToList();
                            if(list!=null&&list.Count>0)
                            {
                                if (isMatchingByTitle)
                                {
                                    foreach (var item in list)
                                    {
                                        if (blockRequest[i].TITLE == item.C_Title)
                                        {
                                            blockRequest[i].WorkCode = item.C_Workcode;
                                            blockRequest[i].IsSuccess = true;
                                            break;
                                        }
                                    }                                    
                                }
                                else
                                {
                                    blockRequest[i].WorkCode = list[0].C_Workcode;
                                    blockRequest[i].IsSuccess = true;
                                }
                            }                           
                        }                        
                    }
                    #endregion

                    #region Update UI
                    DateTime endtime = DateTime.Now;
                    richinfo.Invoke(new MethodInvoker(delegate
                    {
                        richinfo.Text = "";
                        richinfo.Text += $"Total record(s): {dataReponse.ResultObj.TotalRecords}{Environment.NewLine}";
                        richinfo.Text += $"Page index: {dataReponse.ResultObj.PageIndex}{Environment.NewLine}";
                        richinfo.Text += $"Page count: {dataReponse.ResultObj.PageCount}{Environment.NewLine}";
                        richinfo.Text += $"Page size: {dataReponse.ResultObj.PageSize}{Environment.NewLine}";
                        richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                    }));
                    
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        if (currentTimeRequest > totalRequest) currentTimeRequest = totalRequest;
                        float values = (float)currentTimeRequest / (float)totalRequest * 100;
                        progressBarImport.Value = (int)values;
                        lbPercent.Text = $"{((int)values).ToString()}%";
                    }));
                    #endregion
                }
            END:
                #region update Ui when finish
                currentPage = 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                EnablePagging(currentPage, totalPage);
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.IsSuccess)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Matching is finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                    lbInfo.Text += $", total matching success/total: {dataLoad.Where(p => p.IsSuccess).Count()}/{dataLoad.Count}";
                }));
                btnMatching.Invoke(new MethodInvoker(delegate
                {
                    btnMatching.Enabled = true;
                }));
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = false;
                }));
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = true;
                }));
                isStop = false;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Matching is finish";
                }));
                #endregion
            }
            catch (Exception)
            {
                isStop = false;
                if (btnMatching != null && !btnMatching.IsDisposed)
                {
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = true;
                    }));
                }
                if (lbInfo != null && !btnMatching.IsDisposed)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Matching is failure";
                    }));
                }               
            }
        }        
        private void EnablePagging(int currentPage, int totalPage)
        {
            txtPageCurrent.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent.ReadOnly = false;
            }));
            //<<
            if (currentPage > 1)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPAge.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPAge.Enabled = false;
                }));
            }
            //<
            if (currentPage > 1)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = false;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = true;
                }));
            }
        }

        private void ConvertToUnicode(List<PreclaimMatchingViewModel> dataLoad)
        {
            try
            {
                if(dataLoad==null || dataLoad.Count==0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    //if(item.SerialNo ==9997)
                    //{
                    //    int a = 1;
                    //}
                    if(item.TITLE!=string.Empty) item.TITLE = ConvertAllToUnicode.ConvertFromComposite(item.TITLE);
                    if (item.ARTIST != string.Empty) item.ARTIST = ConvertAllToUnicode.ConvertFromComposite(item.ARTIST);
                    if (item.ALBUM != string.Empty) item.ALBUM = ConvertAllToUnicode.ConvertFromComposite(item.ALBUM);
                    if (item.LABEL != string.Empty) item.LABEL = ConvertAllToUnicode.ConvertFromComposite(item.LABEL);
                    if (item.COMP_TITLE != string.Empty) item.COMP_TITLE = ConvertAllToUnicode.ConvertFromComposite(item.COMP_TITLE);
                    if (item.COMP_WRITERS != string.Empty) item.COMP_WRITERS = ConvertAllToUnicode.ConvertFromComposite(item.COMP_WRITERS); 
                }
            }
            catch (Exception)
            {               
            }
        }

        private void ResetStatus()
        {
            try
            {
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = false;
                }));
                if (dataLoad == null)
                {
                    dataLoad = new List<PreclaimMatchingViewModel>();
                }
                if (dataLoad.Count == 0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    item.IsMatching = false;
                    item.IsSuccess = false;
                }
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = dataLoad;
                }));                
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = true;
                }));
            }
            catch (Exception)
            {
                if (btnResetMatching != null && !btnResetMatching.IsDisposed)
                {
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = true;
                    }));
                }
            }
        }

        private void reRefreshUI()
        {
            try
            {
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    cboType.SelectedIndex = 0;
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    txtFind.Text = string.Empty;
                }));                
               
                if (dataLoad == null)
                {
                    dataLoad = new List<PreclaimMatchingViewModel>();
                }
                else
                {
                    if (dataLoad.Count > 0)
                    {
                        currentPage = 1;
                        if (dataLoad.Count % Core.LimitDisplayDGV == 0)
                        {
                            totalPage = dataLoad.Count / Core.LimitDisplayDGV;
                        }
                        else
                        {
                            totalPage = dataLoad.Count / Core.LimitDisplayDGV + 1;
                        }
                        lbTotalPage.Invoke(new MethodInvoker(delegate
                        {
                            lbTotalPage.Text = totalPage.ToString();
                        }));
                        txtPageCurrent.Invoke(new MethodInvoker(delegate
                        {
                            txtPageCurrent.Value = currentPage;
                        }));

                        dataLoad = dataLoad.OrderBy(p => p.SerialNo).ToList();
                        CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                        EnablePagging(currentPage, totalPage);
                        dgvMain.Invoke(new MethodInvoker(delegate
                        {
                            dgvMain.DataSource = CurrentDataView;
                        }));
                        lbLoad.Invoke(new MethodInvoker(delegate
                        {
                            lbLoad.Text = $"Refresh data";
                        }));
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = "Refresh data";
                        }));
                    }
                }
            }
            catch (Exception)
            {
                if (toolMain != null && !toolMain.IsDisposed)
                {
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnRefresh.Enabled = true;
                    }));
                }
            }
        }
        #endregion

        #region Eport
        private void ExportToExcel(string filePath)
        {
            if (dataLoad == null || dataLoad.Count == 0 || filepath == string.Empty)
            {
                return;
            }
            statusMain.Invoke(new MethodInvoker(delegate
            {
                progressBarImport.Value = 0;
                lbPercent.Text = "0%";
            }));
            statusMain.Invoke(new MethodInvoker(delegate
            {
                lbOperation.Text = "Export to excel...";
            }));
            int pos = filePath.LastIndexOf('.');
            string x1 = filePath.Substring(0, pos);
            string x2 = filePath.Substring(pos+1, filePath.Length-pos-1);
            string[] file = new string[] {x1,x2 };
            string path = string.Empty;
            string name = string.Empty;
            string extension = string.Empty;
            if(file!=null && file.Length>1)
            {
                extension = file[1];
                int a = file[0].LastIndexOf("\\");
                if(a>2)
                {
                    path = file[0].Substring(0, a);
                    name = file[0].Substring(a+1, file[0].Length-a-1);
                }
            }
            if(path.Length>0 && name.Length>0 && extension.Length>0)
            {
                int totalFile = 0;
                if (dataLoad.Count % Core.LimitDisplayExportExcel == 0)
                {
                    totalFile = dataLoad.Count / Core.LimitDisplayExportExcel;
                }
                else
                {
                    totalFile = dataLoad.Count / Core.LimitDisplayExportExcel + 1;
                }
                int serial = 0;
                int index = 0;
                while(index < dataLoad.Count)
                {
                    serial++;
                    var datax = dataLoad.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                    index += Core.LimitDisplayExportExcel;
                    string pathFull = $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}";
                    bool check = WriteReportHelper.ExportPreClaimMatching(datax, pathFull);
                    datax = null;
                    GC.Collect();
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        if (serial > totalFile) serial = totalFile;
                        float values = (float)serial / (float)totalFile * 100;
                        progressBarImport.Value = (int)values;
                        lbPercent.Text = $"{((int)values).ToString()}%";
                    }));
                }
            }
            statusMain.Invoke(new MethodInvoker(delegate
            {
                lbOperation.Text = "Export to excel be finish";
            }));
            toolMain.Invoke(new MethodInvoker(delegate
            {
                btnExport.Enabled = true;
            }));

            statusMain.Invoke(new MethodInvoker(delegate
            {
                progressBarImport.Value = 100;
            }));

        }
        #endregion

        #region Btn
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                #region set backgroundWorker
                Operation = OperationType.RefreshUI;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                //get link
                filepath = string.Empty;
                //
                isConverCompositeToUnicode = cheConvertCompositeToUnicode.Checked;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    //openFileDialog.Filter = "CSV Files|*.csv;";
                    //openFileDialog.Filter = "Excel Files|*.xlsx;";
                    openFileDialog.Filter = "Unicode Files|*.txt;";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filepath = openFileDialog.FileName;
                    }
                }
                if (filepath == string.Empty)
                {
                    MessageBox.Show("Please input file path");
                    return;
                }
                #region set backgroundWorker
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnResetMatching_Click(object sender, EventArgs e)
        {
            try
            {
                #region set backgroundWorker
                Operation = OperationType.ResetStatus;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion               
            }
            catch (Exception)
            {

                //throw;
            }

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //year = (int)numYear.Value;
                //MONTH = cboMonths.SelectedIndex - 1;
                isMatchingByTitle = cheTitle.Checked;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {              
                //get link
                filepath = string.Empty;
                using (SaveFileDialog openFileDialog = new SaveFileDialog())
                {
                    //openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    openFileDialog.Filter = "Excel Files|*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filepath = openFileDialog.FileName;
                    }
                }
                if (filepath == string.Empty)
                {
                    MessageBox.Show("Please input file path");
                    return;
                }
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnExport.Enabled = false;
                }));
                #region set backgroundWorker
                Operation = OperationType.ExportToExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region phan trang        
        private void btnFirstPAge_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage = 1;              
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.IsSuccess)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnFirstPAge.Enabled = false;
            }
        }
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage -= 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.IsSuccess)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnPrevPage.Enabled = false;
            }
        }

        private void btnNxtPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage += 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.IsSuccess)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnNxtPage.Enabled = false;
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage = totalPage;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.IsSuccess)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnLastPage.Enabled = false;
            }

        }
        private void txtPageCurrent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dataLoad != null && dataLoad.Count > 0)
                {
                    if (((int)txtPageCurrent.Value) < 1)
                    {
                        txtPageCurrent.Value = 1;
                    }
                    else if (((int)txtPageCurrent.Value) > totalPage)
                    {
                        txtPageCurrent.Value = totalPage;
                    }
                    currentPage = (int)txtPageCurrent.Value;
                    CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = CurrentDataView;
                        for (int i = 0; i < dgvMain.Rows.Count; i++)
                        {
                            string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                            var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                            if (item != null)
                            {
                                if (item.IsSuccess)
                                {
                                    dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                                }
                            }
                        }
                    }));
                    EnablePagging(currentPage, totalPage);
                }
                else
                {
                    txtPageCurrent.ReadOnly = true;
                }
            }
        }
        #endregion            

        #region Filter
        int cboTypeChoise = -1;
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                #region cbo
                cboTypeChoise = cboType.SelectedIndex;
                #endregion

                #region set backgroundWorker
                Operation = OperationType.Filter;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filter: " + ex.ToString());
            }
        }
        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }
        }
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            try
            {
                txtFind.Text = string.Empty;
                dgvMain.DataSource = CurrentDataView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clear filter: " + ex.ToString());
            }

        }

        private void FilterData()
        {
            try
            {
                List<PreclaimMatchingViewModel> fill = new List<PreclaimMatchingViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = dataLoad.Where(delegate (PreclaimMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ID).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.ID.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = dataLoad.Where(delegate (PreclaimMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.TITLE).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.TITLE.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = dataLoad.Where(delegate (PreclaimMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WorkCode).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.WorkCode.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = dataLoad.Where(delegate (PreclaimMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ARTIST).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.ARTIST.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = fill;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Filter data, total record(s): {fill.Count}";
                }));
            }
            catch (Exception)
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
        }
        #endregion   

        #region dgvMain
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMain.CurrentCell.ColumnIndex == 0)
                {
                    if (dgvMain.CurrentCell.Value == null || (bool)dgvMain.CurrentCell.Value == false)
                    {
                        dgvMain.CurrentCell.Value = true;
                    }
                    else
                    {
                        dgvMain.CurrentCell.Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
        //        {
        //            return;
        //        }
        //        //if (dgvMain.Rows.Count > 0)
        //        //{
        //        //    string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;
        //        //    CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).First();
        //        //    if (CurrenObject == null)
        //        //    {
        //        //        MessageBox.Show("Eror: recode is null");
        //        //        return;
        //        //    }
        //        //    frmPreclaimUpdate frm = new frmPreclaimUpdate(preclaimController, UpdataType.View, CurrenObject);
        //        //    frm.ShowDialog();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        #endregion        

        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadExcel)
                {
                    LoadExcel();
                }
                else if (Operation == OperationType.ResetStatus)
                {
                    ResetStatus();
                }
                else if (Operation == OperationType.RefreshUI)
                {
                    reRefreshUI();
                }
                else if (Operation == OperationType.GetDataFromServer)
                {
                    Mathching();
                }
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(filepath);
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
            if (closePending) this.Close();
            closePending = false;                    
        }
        #endregion       
    }
}
