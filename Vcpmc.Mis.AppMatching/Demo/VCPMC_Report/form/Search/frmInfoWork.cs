using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly.Update;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Search
{
    public partial class frmInfoWork : Form
    {
        #region vari   
        //1.WORK
        MasterPageViewModel master = new MasterPageViewModel();
        //bool searchPageFirst = true;        
        WorkController controller;
        WorkApiClient apiClient;        
        WorkViewModel CurrenObject = null;
        GetWorkPagingRequest request = new GetWorkPagingRequest();
        ApiResult<PagedResult<WorkViewModel>> data = new ApiResult<PagedResult<WorkViewModel>>();
       
        OperationType Operation = OperationType.LoadExcel;
        string filepath = string.Empty;        
        /// <summary>
        /// Dữ liệu đang hiển thị
        /// </summary>
        List<WorkViewModel> CurrentDataView = new List<WorkViewModel>();
        int currentPage = 1;
        int totalPage = 1;
        //MONO
        //MonopolyViewModel CurrenObjectMono = null;
        MonopolyController controllerMono;
        MonopolyApiClient apiClientMono;
        GetMonopolyPagingRequest requestMono = new GetMonopolyPagingRequest();
        ApiResult<PagedResult<MonopolyViewModel>> dataMono = new ApiResult<PagedResult<MonopolyViewModel>>();
        /// <summary>
        /// 0 la work, 1 la mono
        /// </summary>
        int controlTypeSearch = 0;
        bool isRequestWork = false;
        bool isRequestMono = false;
        bool isFilter = false;
        #endregion

        #region init
        public frmInfoWork()
        {
            InitializeComponent();
            apiClient = new WorkApiClient(Core.Client);
            controller = new WorkController(apiClient);

            apiClientMono = new MonopolyApiClient(Core.Client);
            controllerMono = new MonopolyController(apiClientMono);
        }

        private void frmInfoWork_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            cboMonoTypeSearch.SelectedIndex = 0;
            MonoGroupType monoGroupTypeWork = new MonoGroupType { GroupMonoId = 0 , GroupMonoName = "Work group"};
            MonoGroupType monoGroupTypeMember = new MonoGroupType { GroupMonoId = 1 , GroupMonoName = "Member group"};
            dgvCboGroup2.Items.Add(monoGroupTypeWork); 
            dgvCboGroup2.Items.Add(monoGroupTypeMember);
            dgvCboGroup2.ValueMember = "GroupMonoId";
            dgvCboGroup2.DisplayMember = "GroupMonoName";

            MonoGroupType monoGroupTypeWork1 = new MonoGroupType { GroupMonoId = 0, GroupMonoName = "Work group" };
            MonoGroupType monoGroupTypeMember1 = new MonoGroupType { GroupMonoId = 1, GroupMonoName = "Member group" };
            dgvCboGroup3.Items.Add(monoGroupTypeWork1);
            dgvCboGroup3.Items.Add(monoGroupTypeMember1);
            dgvCboGroup3.ValueMember = "GroupMonoId";
            dgvCboGroup3.DisplayMember = "GroupMonoName";


        }

        private void frmInfoWork_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (backgroundWorker.IsBusy)
            {
                //closePending = true;
                backgroundWorker.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;   // or this.Hide()
                return;
            }
            //base.OnFormClosing(e);
            data = null;
            GC.Collect();
        }
        #endregion

        #region LoadData        
        private async void SearchFromServer(int PageIndex)
        {
            try
            {
                isRequestWork = true;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Searching...";
                }));                
                DateTime startTime = DateTime.Now;
                request.PageIndex = PageIndex;

                #region Get master
                //Tam toi fix chi lay trang dau tien
                //TODO
                //if (searchPageFirst)
                //{
                //    totalPage = 1;
                //    master = await controller.TotalGetAllPaging(request);
                //    if (master.TotalRecordes == 0)
                //    {
                //        statusMain.Invoke(new MethodInvoker(delegate
                //        {
                //            lbOperation.Text = "data empty";
                //        }));
                //        isRequestWork = false;
                //        return;
                //    }
                //    else
                //    {
                //        if (master.TotalRecordes % request.PageSize == 0)
                //        {
                //            totalPage = master.TotalRecordes / request.PageSize;
                //        }
                //        else
                //        {
                //            totalPage = master.TotalRecordes / request.PageSize + 1;
                //        }
                //    }
                //    searchPageFirst = false;
                //}
                //TODO
                totalPage = 1;
                #endregion

                data = await controller.GetAllPaging(request);
                DateTime endtime = DateTime.Now;
                if (data.IsSuccessed)
                {                   
                    lbTotalPage.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage.Text = totalPage.ToString();
                    }));
                    txtPageCurrent.Invoke(new MethodInvoker(delegate
                    {
                        txtPageCurrent.Value = data.ResultObj.PageIndex;
                    }));
                    lbInfoLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbInfoLoad.Text = "";
                        lbInfoLoad.Text += $"Total record(s): {master.TotalRecordes}, ";
                        lbInfoLoad.Text += $"Page index: {data.ResultObj.PageIndex}, ";
                        lbInfoLoad.Text += $"Page count: {totalPage}, ";
                        lbInfoLoad.Text += $"Page size: {data.ResultObj.PageSize}, ";                       
                        lbInfoLoad.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}";
                    }));
                    
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        foreach (var item in data.ResultObj.Items)
                        {
                            item.WRITER = string.Empty;
                            item.WRITER_LOCAL = string.Empty;
                            foreach (var sub in item.InterestedParties)
                            {
                                item.WRITER += $"{sub.IP_NAME}, ";
                                item.WRITER_LOCAL += $"{sub.IP_NAME_LOCAL}, ";
                            }
                            item.WRITER = item.WRITER.Trim();
                            if (item.WRITER.Length > 0) item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);

                            item.WRITER_LOCAL = item.WRITER_LOCAL.Trim();
                            if (item.WRITER_LOCAL.Length > 0) item.WRITER_LOCAL = item.WRITER_LOCAL.Substring(0, item.WRITER_LOCAL.Length - 1);
                        }
                        dgvMain.DataSource = data.ResultObj.Items;                        
                        if(dgvMain.Rows.Count > 0)
                        {
                            for (int i = 0; i < dgvMain.Rows.Count; i++)
                            {
                                string id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                                var item = data.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();
                                if (item != null)
                                {
                                    if (item.MonopolyWorks.Count > 0)
                                    {
                                        dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }                          
                            CurrenObject = data.ResultObj.Items.Where(s => s.Id == (string)dgvMain.CurrentRow.Cells["Id"].Value).FirstOrDefault();
                            if (CurrenObject != null)
                            {
                                dgvMonopolyOfWork.DataSource = CurrenObject.MonopolyWorks;
                            }
                        }
                    }));                    

                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {data.ResultObj.Items.Count}";
                    }));
                    EnablePagging(PageIndex, totalPage);
                }                
              
                #region update Ui when finish
                
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Searching is finish, total time {(DateTime.Now - endtime).TotalSeconds}(s)";                    
                }));
                btnSearchWork.Invoke(new MethodInvoker(delegate
                {
                    btnSearchWork.Enabled = true;
                }));
               
               
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Searching is finish";
                }));
                #endregion
                isRequestWork = false;
            }
            catch (Exception)
            {
                isRequestWork = false;
                if (btnSearchWork != null && !btnSearchWork.IsDisposed)
                {
                    btnSearchWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSearchWork.Enabled = true;
                    }));
                }
                if (lbInfo != null && !btnSearchWork.IsDisposed)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Matching is failure";
                    }));
                }
            }
        }
        private void DirectionNarrowDisable()
        {
            btnFirstPAge.Invoke(new MethodInvoker(delegate
            {
                btnFirstPAge.Enabled = false;
            }));
            btnPrevPage.Invoke(new MethodInvoker(delegate
            {
                btnPrevPage.Enabled = false;
            }));
            btnNxtPage.Invoke(new MethodInvoker(delegate
            {
                btnNxtPage.Enabled = false;
            }));
            btnLastPage.Invoke(new MethodInvoker(delegate
            {
                btnLastPage.Enabled = false;
            }));
            txtPageCurrent.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent.ReadOnly = true;
            }));
            statusMain.Invoke(new MethodInvoker(delegate
            {
                lbInfo.Text = $"Search data from serve, total record(s): no reponse";
            }));
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
        #endregion

        #region Export
        private void ExportToExcel(string filePath)
        {
            if (data == null || data.ResultObj == null || data.ResultObj.Items == null 
                || data.ResultObj.Items.Count == 0 || filepath == string.Empty)
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
            string[] file = filePath.Split('.');
            string path = string.Empty;
            string name = string.Empty;
            string extension = string.Empty;
            if (file != null && file.Length > 1)
            {
                extension = file[1];
                int a = file[0].LastIndexOf("\\");
                if (a > 2)
                {
                    path = file[0].Substring(0, a);
                    name = file[0].Substring(a + 1, file[0].Length - a - 1);
                }
            }
            if (path.Length > 0 && name.Length > 0 && extension.Length > 0)
            {
                int totalFile = 0;
                if (data.ResultObj.Items.Count % Core.LimitDisplayExportExcel == 0)
                {
                    totalFile = data.ResultObj.Items.Count / Core.LimitDisplayExportExcel;
                }
                else
                {
                    totalFile = data.ResultObj.Items.Count / Core.LimitDisplayExportExcel + 1;
                }
                int serial = 0;
                int index = 0;
                while (index < data.ResultObj.Items.Count)
                {
                    serial++;
                    var datax = data.ResultObj.Items.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                    index += Core.LimitDisplayExportExcel;
                    bool check = WriteReportHelper.ExportSearch(datax, $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}");
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
            

            statusMain.Invoke(new MethodInvoker(delegate
            {
                progressBarImport.Value = 100;
            }));

        }
        #endregion

        #region Btn
        private void btnClearWork_Click(object sender, EventArgs e)
        {
            try
            {
                txtWK_INT_NO.Text = string.Empty;
                txtTTL_ENG.Text = string.Empty;
                txtISWC_NO.Text = string.Empty;               
                txtWRITER.Text = string.Empty;
               
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void btnSearchWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                controlTypeSearch = 0;
                var datax = new ApiResult<PagedResult<WorkViewModel>>();
                datax.ResultObj = new PagedResult<WorkViewModel>();
                datax.ResultObj.Items = new List<WorkViewModel>();
               
                dgvMonopolyOfWork.Invoke(new MethodInvoker(delegate
                {
                    dgvMonopolyOfWork.DataSource = new List<MonopolyViewModel>();
                }));
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = datax.ResultObj.Items;
                }));
                if (data != null && data.ResultObj != null && data.ResultObj.Items != null)
                {
                    data.ResultObj.Items.Clear();
                }
                else
                {
                    data = new ApiResult<PagedResult<WorkViewModel>>();
                    data.ResultObj = new PagedResult<WorkViewModel>();
                    data.ResultObj.Items = new List<WorkViewModel>();
                }
               
                //searchPageFirst = true;
                totalPage = 1;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPage = 1;

                #region set request
                string alltext = txtWK_INT_NO.Text.Trim() + txtTTL_ENG.Text.Trim() 
                        + txtISWC_NO.Text.Trim() + txtWRITER.Text.Trim();
                if(alltext == string.Empty)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "please input info for searching!";
                    }));
                    return;
                }
                request.WK_INT_NO = txtWK_INT_NO.Text.Trim().ToUpper();
                request.TTL_ENG = txtTTL_ENG.Text.Trim().ToUpper();
                request.ISWC_NO = txtISWC_NO.Text.Trim().ToUpper();
                request.WRITER = txtWRITER.Text.Trim().ToUpper();
                request.IsGetMonopolyInfo = cheIsMonopoly.Checked;
                //request.ISRC = txtISRC.Text.Trim();               
                //request.ARTIST = txtARTIST.Text.Trim();
                //request.SOC_NAME = txtSOC_NAME.Text.Trim();
                if(radContrainsWork.Checked)
                {
                    request.SearchType = 0;
                }
                else
                {
                    request.SearchType = 1;
                }
                request.PageSize = Core.LimitRequestWork;
                #endregion

                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
        private void EntertKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchWork_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
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
                backgroundWorker.RunWorkerAsync();
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
            if (isRequestWork)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching work...";
                }));
                return;
            }
            if (isRequestMono)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating filter...";
                }));
                return;
            }
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage = 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnFirstPAge.Enabled = false;
            }
        }
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (isRequestWork)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching work...";
                }));
                return;
            }
            if (isRequestMono)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating filter...";
                }));
                return;
            }
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage -= 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnPrevPage.Enabled = false;
            }
        }

        private void btnNxtPage_Click(object sender, EventArgs e)
        {
            if (isRequestWork)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching work...";
                }));
                return;
            }
            if (isRequestMono)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating filter...";
                }));
                return;
            }
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage += 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnNxtPage.Enabled = false;
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (isRequestWork)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching work...";
                }));
                return;
            }
            if (isRequestMono)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating filter...";
                }));
                return;
            }
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage = totalPage;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnLastPage.Enabled = false;
            }

        }
        private void txtPageCurrent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isRequestWork)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching work...";
                }));
                return;
            }
            if (isRequestMono)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating filter...";
                }));
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (data != null && data.IsSuccessed && data.ResultObj != null)
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
                    #region set backgroundWorker
                    Operation = OperationType.GetDataFromServer;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                    #endregion
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
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                if (data == null || data.ResultObj == null || data.ResultObj.Items == null || data.ResultObj.Items.Count == 0)
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
                backgroundWorker.RunWorkerAsync();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filter: " + ex.ToString());
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
                isFilter = true;
                if (data == null || data.ResultObj == null || data.ResultObj.Items == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                List<WorkViewModel> fill = new List<WorkViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.TTL_ENG).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.TTL_ENG.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WRITER).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.WRITER.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WK_INT_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.WK_INT_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ISWC_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.ISWC_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                isFilter = false;
            }
            catch (Exception)
            {
                isFilter = false;
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
        }
        #endregion   

        #region dgvMain       
        private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count > 0)
                {
                    string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;
                    CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();
                    if (CurrenObject == null)
                    {
                        MessageBox.Show("Eror: recode is null");
                        return;
                    }
                    frmWorkUpdate frm = new frmWorkUpdate(controller, UpdataType.View, CurrenObject);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvMonopoly_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMonopolyOfWork.Rows.Count > 0)
                {
                    string id = (string)dgvMonopolyOfWork.CurrentRow.Cells["idDetail"].Value;
                    var item = CurrenObject.MonopolyWorks.Where(s => s.Id == id).FirstOrDefault();
                    if (CurrenObject == null)
                    {
                        MessageBox.Show("Eror: recode is null");
                        return;
                    }
                    frmMonopolyUpdate frm = new frmMonopolyUpdate(null, UpdataType.View, item, 0);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count > 0)
                {
                    string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;

                    CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();                   
                    if (CurrenObject != null)
                    {
                        List<MonopolyViewModel> CurrenObjectMonoList = new List<MonopolyViewModel>();
                        foreach (var item in CurrenObject.MonopolyWorks)
                        {
                            CurrenObjectMonoList.Add(item);
                        }
                        foreach (var item in CurrenObject.MonopolyMembers)
                        {
                            CurrenObjectMonoList.Add(item);
                        }
                        dgvMonopolyOfWork.DataSource = CurrenObjectMonoList;
                        if(dgvMonopolyOfWork.Rows.Count > 0)
                        {
                            dgvMonopolyOfWork.Invoke(new MethodInvoker(delegate
                            {                              
                                for (int i = 0; i < dgvMonopolyOfWork.Rows.Count; i++)
                                {
                                    string idx = (string)dgvMonopolyOfWork.Rows[i].Cells["idDetail"].Value;
                                    //var item = CurrenObject.MonopolyWorks.Where(s => s.Id == idx).FirstOrDefault();
                                    var item = CurrenObjectMonoList.Where(s => s.Id == idx).FirstOrDefault();
                                    if (item != null)
                                    {
                                        if (item.EndTime > DateTime.Now || !item.IsExpired)
                                        {
                                            dgvMonopolyOfWork.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                                        }
                                        if (item.Group == 0)
                                        {
                                            dgvMonopolyOfWork.Rows[i].Cells["No"].Style.ForeColor = System.Drawing.Color.Maroon;
                                            dgvMonopolyOfWork.Rows[i].Cells[3].Style.ForeColor = System.Drawing.Color.Maroon;
                                            dgvMonopolyOfWork.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.Maroon;                                            
                                        }
                                        else
                                        {
                                            dgvMonopolyOfWork.Rows[i].Cells["No"].Style.ForeColor = System.Drawing.Color.Blue;
                                            dgvMonopolyOfWork.Rows[i].Cells[3].Style.ForeColor = System.Drawing.Color.Blue;
                                            dgvMonopolyOfWork.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.Blue;                                            
                                        }
                                    }

                                }
                            }));
                        }
                    }  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvMonopolyMember_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }
                if (dataMono == null || dataMono.IsSuccessed == false || dataMono.ResultObj == null || dataMono.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMonopoly.Rows.Count > 0)
                {
                    string id = (string)dgvMonopoly.CurrentRow.Cells["idMonoMember"].Value;
                    var item = dataMono.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();
                    if (item == null)
                    {
                        MessageBox.Show("Eror: recode is null");
                        return;
                    }
                    frmMonopolyUpdate frm = new frmMonopolyUpdate(null, UpdataType.View, item, 1);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {               
                if (Operation == OperationType.GetDataFromServer)
                {
                    if (controlTypeSearch == 0)
                    {
                        SearchFromServer(currentPage);
                    }
                    else
                    {
                        //chi lay trang dau thoi
                        LoadDataMono(1);
                    }
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
                pcloader3.Invoke(new MethodInvoker(delegate
                {
                    pcloader3.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(controlTypeSearch==0)
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
            else
            {
                pcloader3.Invoke(new MethodInvoker(delegate
                {
                    pcloader3.Visible = false;
                }));
            }
           
            //if (closePending) this.Close();
            //closePending = false;
        }
        #endregion       

        #region do doc quyen tac gia+tac pham
        private void btnClearMono_Click(object sender, EventArgs e)
        {
            try
            {

                txtCode3.Text = string.Empty;
                txtName3.Text = string.Empty;              

            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void btnSearchMono_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequestWork)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching work...";
                    }));
                    return;
                }
                if (isRequestMono)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating searching monopoly...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Wating filter...";
                    }));
                    return;
                }                
                #region set request
                requestMono.CodeNew = txtCode3.Text.Trim();
                requestMono.Own2 = txtName3.Text.Trim().ToUpper(); 
               
                if (radContrainsMono.Checked)
                {
                    requestMono.SearchType = 0;
                }
                else
                {
                    requestMono.SearchType = 1;
                }
                requestMono.PageSize = Core.LimitRequestMonopoly;

                if(cboMonoTypeSearch.SelectedIndex ==0 )
                {
                    requestMono.Group = 2;//all
                }
                else if(cboMonoTypeSearch.SelectedIndex == 1)
                {
                    requestMono.Group = 3;//4 mono
                }
                else 
                {
                    requestMono.Group = 4;//5 member
                }
                controlTypeSearch = 1;
                if (requestMono.CodeNew ==string.Empty && requestMono.Own2 == string.Empty)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Please input info for search monopoly!";
                    }));
                    return;
                }
                #endregion

                #region set backgroundMonopolyer
                Operation = OperationType.GetDataFromServer;
                pcloader3.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void EntertKeyDown_mono(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchMono_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }
        }
        private async void LoadDataMono(int PageIndex)
        {
            try
            {
                dataMono = new ApiResult<PagedResult<MonopolyViewModel>>();
                dgvMonopoly.Invoke(new MethodInvoker(delegate
                {
                    dgvMonopoly.DataSource = new List<MonopolyViewModel>();
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Wating searching monopoly...";
                }));
                isRequestMono = true;
                DateTime startTime = DateTime.Now;
                #region Get master   
                ////bo Total de tang toc
                ////if (searchPageFirst)
                ////{
                //    totalPage = 1;
                //    master = await controllerMono.TotalGetAllPaging(requestMono);
                //    if (master.TotalRecordes == 0)
                //    {
                //        statusMain.Invoke(new MethodInvoker(delegate
                //        {
                //            lbOperation.Text = "data empty";
                //        }));
                //        isRequestMono = false;
                //        return;
                //    }
                //    //else
                //    //{
                //    //    if (master.TotalRecordes % request.PageSize == 0)
                //    //    {
                //    //        totalPage = master.TotalRecordes / request.PageSize;
                //    //    }
                //    //    else
                //    //    {
                //    //        totalPage = master.TotalRecordes / request.PageSize + 1;
                //    //    }
                //    //}
                //    //searchPageFirst = false;
                ////}
                totalPage = 1;
                #endregion
                requestMono.PageIndex = PageIndex;
                dataMono = await controllerMono.GetAllPaging(requestMono);
                DateTime endtime = DateTime.Now;
                if (dataMono.IsSuccessed)
                {                                     
                    dgvMonopoly.Invoke(new MethodInvoker(delegate
                    {
                        dgvMonopoly.DataSource = dataMono.ResultObj.Items;
                        for (int i = 0; i < dgvMonopoly.Rows.Count; i++)
                        {
                            string id = (string)dgvMonopoly.Rows[i].Cells["idMonoMember"].Value;
                            var item = dataMono.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();
                            if(item != null)
                            {
                                if(item.EndTime > DateTime.Now || !item.IsExpired)
                                {
                                    dgvMonopoly.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                                }
                                if (item.Group == 0)
                                {
                                    dgvMonopoly.Rows[i].Cells["dataGridViewTextBoxColumn3"].Style.ForeColor = System.Drawing.Color.Maroon;
                                    dgvMonopoly.Rows[i].Cells[3].Style.ForeColor = System.Drawing.Color.Maroon;
                                    dgvMonopoly.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.Maroon;                                 
                                }
                                else
                                {
                                    dgvMonopoly.Rows[i].Cells["dataGridViewTextBoxColumn3"].Style.ForeColor = System.Drawing.Color.Blue;
                                    dgvMonopoly.Rows[i].Cells[3].Style.ForeColor = System.Drawing.Color.Blue;
                                    dgvMonopoly.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.Blue;                                   
                                }
                            }

                        }
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {dataMono.ResultObj.Items.Count}";
                    }));
                    //EnablePagging(data, totalPage);
                }
                else
                {                    
                    DirectionNarrowDisable();
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Searching monopoly is finish";
                }));
                isRequestMono = false;
            }
            catch (Exception ex)
            {
                isRequestMono = false;
                MessageBox.Show($"Load data is error: {ex.ToString()}");
            }
        }
        #endregion       
    }
}
