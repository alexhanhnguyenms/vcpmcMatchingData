using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Youtube;
using Vcpmc.Mis.AppMatching.form.Warehouse.Youtube.Update;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Youtube;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Youtube
{
    public partial class frmPreclaim : Form
    {
        #region vari
        MasterPageViewModel master = new MasterPageViewModel();
        bool searchPageFirst = true;
        //OperationType operation = OperationType.LoadExcel;
        PreclaimViewModel CurrenObject = null;      
        PreclaimController controller;
        PreclaimApiClient preclaimApiClient;
        GetPreclaimPagingRequest request = new GetPreclaimPagingRequest();
        ApiResult<PagedResult<PreclaimViewModel>> data = new ApiResult<PagedResult<PreclaimViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        int currentPage = 1;
        int totalPage = 1;
        UpdataType _type = UpdataType.All;
        bool isRequest = false;
        bool isFilter = false;
        #endregion

        #region innit
        public frmPreclaim(UpdataType type)
        {
            InitializeComponent();
            preclaimApiClient = new PreclaimApiClient(Core.Client);
            controller = new PreclaimController(preclaimApiClient);
            _type = type;
        }
        private void frmPreclaim_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            //cboMonths.SelectedIndex = 0;
            //numYear.Value = DateTime.Now.Year;
            if (_type == UpdataType.View)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnImport.Visible = false;
            }
        }
        #endregion

        #region LoadData
        private async void LoadData(int PageIndex) 
        {
            try
            {               
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<PreclaimViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<PreclaimViewModel>();
                }));
                #region Get master                
                if (searchPageFirst)
                {
                    master = await controller.TotalGetAllPaging(request);
                    if (master.TotalRecordes == 0)
                    {
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = "data empty";
                        }));
                        isRequest = false;
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
                request.PageIndex = PageIndex;
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
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Searching is finish";
                    }));
                    EnablePagging(data, totalPage);
                }
                else
                {
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<PreclaimViewModel>();
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

        private void EnablePagging(ApiResult<PagedResult<PreclaimViewModel>> data, int totalPage)
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
            if (data.ResultObj.PageIndex < totalPage)
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
            if (data.ResultObj.PageIndex == totalPage)
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
               txtAsset_ID.Text = string.Empty;
               txtC_Title.Text = string.Empty;
               txtC_ISWC.Text = string.Empty;
               txtC_Workcode.Text = string.Empty;
               txtC_Writers.Text = string.Empty;
               //cboMonths.SelectedIndex = 0;
               //numYear.Value = DateTime.Now.Year;              
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void EntertKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if(isRequest)
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
                data = new ApiResult<PagedResult<PreclaimViewModel>>();
                data.ResultObj = new PagedResult<PreclaimViewModel>();
                data.ResultObj.Items = new List<PreclaimViewModel>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = data.ResultObj.Items;
                }));
                searchPageFirst = true;
                btnFirstPAge.Enabled = false;               
                btnPrevPage.Enabled = false;               
                btnNxtPage.Enabled = false;               
                btnLastPage.Enabled = false;               
                txtPageCurrent.ReadOnly = true;              
                currentPage = 1;
                totalPage = 1;
                #region set request
                request.Asset_ID = txtAsset_ID.Text.Trim().ToUpper();
                request.C_Title = txtC_Title.Text.Trim().ToUpper();
                request.C_ISWC = txtC_ISWC.Text.Trim().ToUpper();
                request.C_Workcode = txtC_Workcode.Text.Trim().ToUpper();
                request.C_Writers = txtC_Writers.Text.Trim().ToUpper();
                //if (cboMonths.SelectedIndex == 0)
                //{
                //    request.MONTH = -1;
                //}
                //else
                //{
                //    request.MONTH = cboMonths.SelectedIndex;
                //}
                //request.Year = (int)numYear.Value;
                request.PageSize = Core.LimitRequestPreclaim;
                #endregion

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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage = 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage -= 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage += 1;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
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
            if (data!=null && data.IsSuccessed && data.ResultObj!=null)
            {
                currentPage = totalPage;
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
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
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
                else
                {
                    txtPageCurrent.ReadOnly = true;                    
                }
            }
        }
        #endregion

        #region Update        
        private void btnAdd_Click(object sender, EventArgs e)
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
                CurrenObject = new PreclaimViewModel();
                frmPreclaimUpdate frm = new frmPreclaimUpdate(controller,UpdataType.Add, CurrenObject);
                frm.ShowDialog();
                if(frm.ObjectReturn!=null&& frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Added Item: {frm.ObjectReturn.Message}, total Added: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundWorker
                    Operation = OperationType.GetDataFromServer;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
                else
                {
                    if (frm.ObjectReturn != null)
                    {
                        lbOperation.Text = $"Added Item: {frm.ObjectReturn.Message}";
                    }
                    else
                    {
                        lbOperation.Text = $"Added Item is failure";
                    }                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }

        }        
        private void btnEdit_Click(object sender, EventArgs e)
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
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                string id = string.Empty;
                int count = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        count++;
                        id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                        if (count > 1)
                        {
                            MessageBox.Show("Please choise only one record!");
                            return;
                        }
                    }
                }
                if (id == string.Empty)
                {
                    MessageBox.Show("Please must choise one record!");
                    return;
                }
                CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).First();
                if (CurrenObject == null)
                {
                    MessageBox.Show("Eror: recode is null");
                    return;
                }
                frmPreclaimUpdate frm = new frmPreclaimUpdate(controller, UpdataType.Edit, CurrenObject);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Edited Item: {frm.ObjectReturn.Message}, total Edited: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundWorker
                    Operation = OperationType.GetDataFromServer;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
                else
                {
                    if (frm.ObjectReturn != null)
                    {
                        lbOperation.Text = $"Edited Item: {frm.ObjectReturn.Message}";
                    }
                    else
                    {
                        lbOperation.Text = $"Edited Item is failure";
                    }                   
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
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
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                string id = "";
                int count = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        count++;
                        id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                        if (count > 1)
                        {
                            MessageBox.Show("Please choise only one record!");
                            return;
                        }
                    }
                }
                if (id == string.Empty)
                {
                    MessageBox.Show("Please must choise one record!");
                    return;
                }
                CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).First();
                if (CurrenObject == null)
                {
                    MessageBox.Show("Eror: recode is null");
                    return;
                }
                var confirmResult = MessageBox.Show("Are you sure to delete this record?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var deleteStatus = await controller.Delete(CurrenObject.Id);                   
                    if (deleteStatus!= null && deleteStatus.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbOperation.Text = $"Deleted record: {deleteStatus.Message}, total Deleted: {deleteStatus.TotalEffect}";
                        #region set backgroundWorker
                        Operation = OperationType.GetDataFromServer;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker1.RunWorkerAsync();
                        #endregion
                        
                    }
                    else
                    {
                        if (deleteStatus != null)
                        {
                            lbOperation.Text = $"Deleted Item: {deleteStatus.Message}";
                        }
                        else
                        {
                            lbOperation.Text = $"Deleted Item is failure";
                        }                        
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }        
        #endregion

        #region Import
        private void btnImport_Click(object sender, EventArgs e)
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
                frmPreclaimImport frm = new frmPreclaimImport(controller);
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
                if (data ==null|| data.IsSuccessed == false || data.ResultObj==null || data.ResultObj.Items.Count == 0)
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
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "filter...";
                }));
                List<PreclaimViewModel> fill = new List<PreclaimViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = data.ResultObj.Items.Where(delegate (PreclaimViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Asset_ID).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.Asset_ID.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = data.ResultObj.Items.Where(delegate (PreclaimViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.C_Title).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.C_Title.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = data.ResultObj.Items.Where(delegate (PreclaimViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.C_ISWC).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.C_ISWC.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = data.ResultObj.Items.Where(delegate (PreclaimViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.C_Workcode).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.C_Workcode.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = data.ResultObj.Items.Where(delegate (PreclaimViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.C_Writers).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.C_Writers.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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

        #region dgvMain
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    frmPreclaimUpdate frm = new frmPreclaimUpdate(controller,UpdataType.View, CurrenObject);
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
                    LoadData(currentPage);
                }
                if (Operation == OperationType.Filter)
                {
                    FilterData();
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
        
    }
}
