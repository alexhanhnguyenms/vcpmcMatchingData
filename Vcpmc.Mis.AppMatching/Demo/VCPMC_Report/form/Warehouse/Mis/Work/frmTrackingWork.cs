using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work
{    
    public partial class frmTrackingWork : Form
    {
        #region vari
        MasterPageViewModel master = new MasterPageViewModel();
        bool searchPageFirst = true;
        //OperationType operation = OperationType.LoadExcel;
        WorkTrackingViewModel CurrenObject = null;
        WorkTrackingController controller;
        WorkTrackingApiClient _apiClient;
        GetWorkTrackingPagingRequest request = new GetWorkTrackingPagingRequest();
        ApiResult<PagedResult<WorkTrackingViewModel>> data = new ApiResult<PagedResult<WorkTrackingViewModel>>();
        ApiResult<PagedResult<WorkTrackingAggregateViewModel>> dataMaster = new ApiResult<PagedResult<WorkTrackingAggregateViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        int _year = 2020;
        int _MONTH = -1;
        int _type = 0;
        int currentPage = 1;
        int totalPage = 1;
        //------
        WorkController controllerWork;
        WorkApiClient _apiClientWork;
        bool isRequest = false;
        bool isFilter = false;
        bool isSyncData = false;
        #endregion

        #region init
        public frmTrackingWork()
        {
            InitializeComponent();
            _apiClient = new WorkTrackingApiClient(Core.Client);
            controller = new WorkTrackingController(_apiClient);

            _apiClientWork = new WorkApiClient(Core.Client);
            controllerWork = new WorkController(_apiClientWork);
        }

        private void frmTrackingWork_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            cboMonths.SelectedIndex = 0;
            numYear.Value = (int)DateTime.Now.Year;
        }
        #endregion

        #region LoadData      
        private async void LoadDataAggregate()
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                dataMaster = new ApiResult<PagedResult<WorkTrackingAggregateViewModel>>();
                dgvAggregate.Invoke(new MethodInvoker(delegate
                {
                    dgvAggregate.DataSource = new List<WorkTrackingAggregateViewModel>();
                }));
                request.PageIndex = 1;;
                request.Year = _year;
                request.MONTH = _MONTH;
                dataMaster = await controller.GetArreggateMasterList(request);
                DateTime endtime = DateTime.Now;
                if (dataMaster.IsSuccessed)
                {
                    //lbTotalPage.Invoke(new MethodInvoker(delegate
                    //{
                    //    lbTotalPage.Text = data.ResultObj.PageCount.ToString();
                    //}));
                    //txtPageCurrent.Invoke(new MethodInvoker(delegate
                    //{
                    //    txtPageCurrent.Value = data.ResultObj.PageIndex;
                    //}));
                    richinfo.Invoke(new MethodInvoker(delegate
                    {
                        richinfo.Text = "";
                        richinfo.Text += $"Total record(s): {dataMaster.ResultObj.TotalRecords}{Environment.NewLine}";
                        richinfo.Text += $"Page index: {dataMaster.ResultObj.PageIndex}{Environment.NewLine}";
                        richinfo.Text += $"Page count: {dataMaster.ResultObj.PageCount}{Environment.NewLine}";
                        richinfo.Text += $"Page size: {dataMaster.ResultObj.PageSize}{Environment.NewLine}";
                        richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                    }));
                    foreach (var item in dataMaster.ResultObj.Items)
                    {
                        if(item.Year == 0)
                        {
                            item.NameType = "Create";
                        }
                        else
                        {
                            item.NameType = "Update";
                        }
                    }
                    dgvAggregate.Invoke(new MethodInvoker(delegate
                    {
                        dgvAggregate.DataSource = dataMaster.ResultObj.Items;
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {dataMaster.ResultObj.Items.Count}";
                    }));
                    //EnablePagging(data);
                }
                else
                {
                    //dgvAggregate.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvAggregate.DataSource = new List<WorkTrackingAggregateViewModel>();
                    //}));
                    DirectionNarrowDisable();
                }
                isRequest = false;
            }
            catch (Exception ex)
            {
                isRequest = false;
                MessageBox.Show($"Load data is error: {ex.ToString()}");
            }
        }
        private async void LoadData(int PageIndex)
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<WorkTrackingViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<WorkTrackingViewModel>();
                }));
                request.PageIndex = PageIndex;
                request.Year = _year;
                request.MONTH = _MONTH;
                request.Type = _type;
                request.PageSize = Core.LimitRequestTrackingWork;
                #region Get master                
                if (searchPageFirst)
                {
                    totalPage = 1;
                    master = await controller.TotalGetAllPaging(request);
                    if (master.TotalRecordes == 0)
                    {
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = "data empty";
                        }));                        
                        return;
                    }
                    else
                    {
                        if (master.TotalRecordes % request.PageSize == 0)
                        {
                            totalPage = master.TotalRecordes / request.PageSize;
                        }
                        else
                        {
                            totalPage = master.TotalRecordes / request.PageSize + 1;
                        }
                    }
                    searchPageFirst = false;
                }
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
                    richinfo.Invoke(new MethodInvoker(delegate
                    {
                        richinfo.Text = "";
                        richinfo.Text += $"Total record(s): {master.TotalRecordes}{Environment.NewLine}";
                        richinfo.Text += $"Page index: {data.ResultObj.PageIndex}{Environment.NewLine}";
                        richinfo.Text += $"Page count: {totalPage}{Environment.NewLine}";
                        richinfo.Text += $"Page size: {data.ResultObj.PageSize}{Environment.NewLine}";
                        richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                    }));
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = data.ResultObj.Items;
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {data.ResultObj.Items.Count}";
                    }));
                    EnablePagging(data, totalPage);
                }
                else
                {                   
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<WorkTrackingViewModel>();
                    //}));
                    DirectionNarrowDisable();
                }
                isRequest = false;
            }
            catch (Exception ex)
            {
                isRequest = false;
                MessageBox.Show($"Load data is error: {ex.ToString()}");
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

        private void EnablePagging(ApiResult<PagedResult<WorkTrackingViewModel>> data, int pageTotal)
        {
            txtPageCurrent.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent.ReadOnly = false;
            }));
            //<<
            if (data.ResultObj.PageIndex > 1)
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
            if (data.ResultObj.PageIndex > 1)
            {
                btnPrevPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = true;
                }));
            }
            else
            {
                btnPrevPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = false;
                }));
            }
            //>
            if (data.ResultObj.PageIndex < pageTotal)
            {
                btnNxtPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = true;
                }));
            }
            else
            {
                btnNxtPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = false;
                }));
            }
            //>>
            if (data.ResultObj.PageIndex == pageTotal)
            {
                btnLastPage.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = false;
                }));
            }
            else
            {
                btnLastPage.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = true;
                }));
            }
        }
        #endregion

        #region Search       
        
        bool isMaster = false;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
                }));
                return;
            }
            btnFirstPAge.Enabled = false;
            btnPrevPage.Enabled = false;
            btnNxtPage.Enabled = false;
            btnLastPage.Enabled = false;
            txtPageCurrent.ReadOnly = true;

            _MONTH = cboMonths.SelectedIndex - 1;
            _year = (int)numYear.Value;
            currentPage = 1;
            totalPage = 1;
            isMaster = true;
            #region set request

            #endregion

            #region set backgroundWorker
            Operation = OperationType.GetDataFromServer;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();
            #endregion
        }
        private void btnGetDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequest)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting reponse...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (isSyncData)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                    return;
                }
                #region MyRegion
                if (dataMaster == null || dataMaster.IsSuccessed == false || dataMaster.ResultObj == null || dataMaster.ResultObj.Items.Count == 0)
                {
                    return;
                }
                //string id = string.Empty;
                if(dgvAggregate.Rows.Count ==0)
                {
                    return;
                }
                var currentRow = dgvAggregate.CurrentRow;
                _year = (int)currentRow.Cells["Yearx"].Value;
                _MONTH = (int)currentRow.Cells["Monthx"].Value;
                _type = (int)currentRow.Cells["Typex"].Value;
                #endregion

                searchPageFirst = true;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPage = 1;
                totalPage = 1;
                isMaster = false;
                #region set request
                request.PageSize = Core.LimitRequestTrackingWork;
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
        private void btnFirstPAge_Click(object sender, EventArgs e)
        {
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
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
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
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
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
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
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
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
            if (isRequest)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isFilter)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting filter...";
                }));
                return;
            }
            if (isSyncData)
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Waiting sync data...";
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

        #region Import
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                frmTrackingWorkImport frm = new frmTrackingWorkImport(controller);
                frm.ShowDialog();
            }
            catch (Exception)
            {


            }
        }

        #endregion        

        #region Filter
        int cboTypeChoise = -1;
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequest)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting reponse...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (isSyncData)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
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
                dgvMain.DataSource = data.ResultObj.Items;
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
                List<WorkTrackingViewModel> fill = new List<WorkTrackingViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WK_INT_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.WK_INT_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.TTL_ENG).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.TTL_ENG.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ISWC_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.ISWC_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WRITER).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.WRITER.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ARTIST).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.ARTIST.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 5)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkTrackingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.SOC_NAME).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.SOC_NAME.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                isFilter = false;
            }
        }
        #endregion

        #region dgv
        private void dgvAggregate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAggregate.CurrentCell.ColumnIndex == 0)
                {
                    if (dgvAggregate.CurrentCell.Value == null || (bool)dgvAggregate.CurrentCell.Value == false)
                    {
                        dgvAggregate.CurrentCell.Value = true;
                    }
                    else
                    {
                        dgvAggregate.CurrentCell.Value = false;
                    }
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
        private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
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
                    frmWorkTrackingUpdate frm = new frmWorkTrackingUpdate(controller, UpdataType.View, CurrenObject);
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
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.GetDataFromServer)
                {
                    if(isMaster)
                    {
                        LoadDataAggregate();
                    }
                    else
                    {
                        LoadData(currentPage);
                    }                    
                }
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
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

        #region context menu     
        int monthUpdate = 1;
        int yearUpdate = 2020;
        int typeUpdate = 2020;
        private void sysncToWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRequest)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting reponse...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (isSyncData)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting sync data...";
                    }));
                    return;
                }
                if (dgvAggregate.Rows.Count==0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = $"The data is empty, so not sync to Work, please load tracking work data list!";
                    }));
                }
                else
                {
                    string period = $"Serial:{dgvAggregate.CurrentRow.Cells["Serial"].Value.ToString()}" +
                                    $", Year: {dgvAggregate.CurrentRow.Cells["Yearx"].Value.ToString()}" +
                                    $", Month: {dgvAggregate.CurrentRow.Cells["Monthx"].Value.ToString()}" +
                                    $", NameType: {dgvAggregate.CurrentRow.Cells["NameType"].Value.ToString()}" +
                                    $", Total: {dgvAggregate.CurrentRow.Cells["Total"].Value.ToString()}";                                   
                    var confirmResult = MessageBox.Show($"Are you sure to sync Tracking work to Work?{Environment.NewLine}DETAIL{Environment.NewLine}{period}",
                                     "Confirm SYNC DATA!",
                                     MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        monthUpdate = int.Parse(dgvAggregate.CurrentRow.Cells["Monthx"].Value.ToString());
                        yearUpdate = int.Parse(dgvAggregate.CurrentRow.Cells["Yearx"].Value.ToString());
                        typeUpdate = int.Parse(dgvAggregate.CurrentRow.Cells["Typex"].Value.ToString());
                        #region set backgroundWorker
                        Operation = OperationType.SysnData;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker.RunWorkerAsync();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private async void SysnData()
        {
            try
            {
                isSyncData = true;
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

                GetWorkTrackingPagingRequest requestUpdate = new GetWorkTrackingPagingRequest();
                requestUpdate.PageIndex = 1;
                requestUpdate.Year = yearUpdate;
                requestUpdate.MONTH = monthUpdate;
                requestUpdate.Type = typeUpdate;
                requestUpdate.PageSize = Core.LimitRequestTrackingWork;
                var masterUpdte = await controller.TotalGetAllPaging(requestUpdate);               
                if (masterUpdte == null || masterUpdte.TotalRecordes == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Data that update is empty, so not sync, please input data source";
                    }));
                    isSyncData = false;
                    return;
                }
                else
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Proccessing sync...";
                    }));
                    ctMenuStripMain.Invoke(new MethodInvoker(delegate
                    {
                        sysncToWorkToolStripMenuItem.Enabled = false;
                    }));
                    btnSearch.Invoke(new MethodInvoker(delegate
                    {
                        btnSearch.Enabled = false;
                    }));
                    btnGetDetail.Invoke(new MethodInvoker(delegate
                    {
                        btnGetDetail.Enabled = false;
                    }));
                }
                int totalRequest = 0;
                if (masterUpdte.TotalRecordes % Core.LimitRequestTrackingWork == 0)
                {
                    totalRequest = masterUpdte.TotalRecordes / Core.LimitRequestTrackingWork;
                }
                else
                {
                    totalRequest = masterUpdte.TotalRecordes / Core.LimitRequestTrackingWork + 1;
                }
                int index = 0;
                int currentTimeRequest = 0;
                int totalSuccess = 0;
                while (index < masterUpdte.TotalRecordes)
                {
                    DateTime startTime = DateTime.Now;
                    currentTimeRequest++;
                    #region Creat request
                    index += Core.LimitRequestTrackingWork;
                    if (index > masterUpdte.TotalRecordes)
                    {
                        index = masterUpdte.TotalRecordes;
                    }
                    requestUpdate.PageIndex = currentTimeRequest;
                    var data = await controllerWork.SyncFromTrackingWorkToWork(requestUpdate);

                    #endregion

                    #region Update UI
                    if(data != null|| data.Items != null)
                    {
                        totalSuccess += data.Items.Where(p => p.Status == Utilities.Common.UpdateStatus.Successfull).Count();
                        DateTime endtime = DateTime.Now;
                        richinfo.Invoke(new MethodInvoker(delegate
                        {
                            richinfo.Text = "";
                            richinfo.Text += $"Total record(s): {data.Items.Count}{Environment.NewLine}";
                            richinfo.Text += $"Page index: {currentTimeRequest}{Environment.NewLine}";
                            richinfo.Text += $"Page count: {totalRequest}{Environment.NewLine}";
                            richinfo.Text += $"Page size: {Core.LimitRequestTrackingWork}{Environment.NewLine}";
                            richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                            richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                            richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                        }));
                    }
                   

                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        if (currentTimeRequest > totalRequest) currentTimeRequest = totalRequest;
                        float values = (float)currentTimeRequest / (float)totalRequest * 100;
                        progressBarImport.Value = (int)values;
                        lbPercent.Text = $"{((int)values).ToString()}%";
                    }));
                    #endregion
                }        
                #region update Ui when finish
                
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"sync tracking work to work be finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                    lbInfo.Text += $", total sync success/total: {totalSuccess}/{masterUpdte.TotalRecordes}";
                }));
                ctMenuStripMain.Invoke(new MethodInvoker(delegate
                {
                    sysncToWorkToolStripMenuItem.Enabled = true;
                }));
                btnSearch.Invoke(new MethodInvoker(delegate
                {
                    btnSearch.Enabled = true;
                }));
                btnGetDetail.Invoke(new MethodInvoker(delegate
                {
                    btnGetDetail.Enabled = true;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Sync is finish";
                }));
                #endregion

                isSyncData = false;
            }
            catch (Exception)
            {
                if (ctMenuStripMain != null && !ctMenuStripMain.IsDisposed)
                {
                    ctMenuStripMain.Invoke(new MethodInvoker(delegate
                    {
                        sysncToWorkToolStripMenuItem.Enabled = true;
                    }));
                }
                if (btnGetDetail != null && !btnGetDetail.IsDisposed)
                {
                    btnGetDetail.Invoke(new MethodInvoker(delegate
                    {
                        btnGetDetail.Enabled = true;
                    }));
                }
                if (btnSearch != null && !btnSearch.IsDisposed)
                {
                    btnSearch.Invoke(new MethodInvoker(delegate
                    {
                        btnSearch.Enabled = true;
                    }));
                }
                if (lbOperation != null && !lbInfo.IsDisposed)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = $"Sync tracking work to work be failure";
                    }));
                }
                isSyncData = false;
            }
        }
        #endregion

    }
}
