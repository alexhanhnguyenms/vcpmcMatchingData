﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly.Update;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly
{
    public partial class frmMonopoly : Form
    {
        #region vari
        MasterPageViewModel master = new MasterPageViewModel();
        bool searchPageFirst = true;
        //OperationType operation = OperationType.LoadExcel;
        MonopolyViewModel CurrenObject = null;
        MonopolyController controller;
        MonopolyApiClient apiClient;
        GetMonopolyPagingRequest request = new GetMonopolyPagingRequest();
        ApiResult<PagedResult<MonopolyViewModel>> data = new ApiResult<PagedResult<MonopolyViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        int currentPage = 1;
        int totalPage = 1;
        UpdataType _type = UpdataType.All;
        string filepath = string.Empty;
        bool isRequest = false;
        bool isFilter = false;
        #endregion
        public frmMonopoly(UpdataType type)
        {
            InitializeComponent();
            apiClient = new MonopolyApiClient(Core.Client);
            controller = new MonopolyController(apiClient);
            _type = type;
        }

        private void frmMonopoly_Load(object sender, EventArgs e)
        {
            cboGroup.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            cboFields.SelectedIndex = 0;
            if(_type == UpdataType.View)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnImport.Visible = false;
            }    
        }
        #region LoadData
        private async void LoadData(int PageIndex)
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<MonopolyViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<MonopolyViewModel>();
                }));
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
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = data.ResultObj.Items;
                    //}));
                    cboFields_SelectedIndexChanged(null,null);
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {data.ResultObj.Items.Count}";
                    }));
                    EnablePagging(data, totalPage);
                    cboFields_SelectedIndexChanged(null, null);
                }
                else
                {
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<MonopolyViewModel>();
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

        private void EnablePagging(ApiResult<PagedResult<MonopolyViewModel>> data, int pageTotal)
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
        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Click(null,null);
            }
            catch (Exception)
            {

               
            }
        }
       
        private void btnSearch_Click(object sender, EventArgs e)
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
                btnClearFilter_Click(null, null);
                cboFields.SelectedIndex = 0;
                searchPageFirst = true;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPage = 1;
                totalPage = 1;
                #region set request
                request.Group = cboGroup.SelectedIndex;
                request.PageSize = Core.LimitRequestMonopoly;
                #endregion

                #region set backgroundMonopolyer
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage = 1;
                #region set backgroundMonopolyer
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage -= 1;
                #region set backgroundMonopolyer
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage += 1;
                #region set backgroundMonopolyer
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
            if (data != null && data.IsSuccessed && data.ResultObj != null)
            {
                currentPage = totalPage;
                #region set backgroundMonopolyer
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
                    #region set backgroundMonopolyer
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
                CurrenObject = new MonopolyViewModel();
                frmMonopolyUpdate frm = new frmMonopolyUpdate(controller, UpdataType.Add, CurrenObject, cboGroup.SelectedIndex);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Added Item: {frm.ObjectReturn.Message}, total Added: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundMonopolyer
                    Operation = OperationType.GetDataFromServer;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
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
                frmMonopolyUpdate frm = new frmMonopolyUpdate(controller, UpdataType.Edit, CurrenObject, cboGroup.SelectedIndex);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Edited Item: {frm.ObjectReturn.Message}, total Edited: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundMonopolyer
                    Operation = OperationType.GetDataFromServer;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
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
                    if (deleteStatus != null && deleteStatus.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbOperation.Text = $"Deleted record: {deleteStatus.Message}, total Deleted: {deleteStatus.TotalEffect}";
                        #region set backgroundMonopolyer
                        Operation = OperationType.GetDataFromServer;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker.RunWorkerAsync();
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

        #region Import+ export
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
                frmMonopolyImport frm = new frmMonopolyImport(controller,cboGroup.SelectedIndex);
                frm.ShowDialog();
            }
            catch (Exception)
            {


            }
        }
        private void btnExport_Click(object sender, EventArgs e)
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

                //TODO
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
                #region set backgroundMonopolyer
                Operation = OperationType.ExportToExcel;
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
        private void ExportToExcel()
        {
            try
            {
                if(fill!=null && fill.Count>0)
                {
                    //var datax = dataLoad.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                    //index += Core.LimitDisplayExportExcel;
                    bool check = WriteReportHelper.ExportMonopoly(fill, filepath);                   
                    GC.Collect();
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Export to excel is successfull";
                    }));
                }
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnExport.Enabled = true;
                }));
            }
            catch (Exception)
            {

                throw;
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
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                #region cbo
                cboTypeChoise = cboType.SelectedIndex;
                #endregion

                #region set backgroundMonopolyer
                Operation = OperationType.Filter;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion

            }
            catch (Exception)
            {
                //MessageBox.Show("Error filter: " + ex.ToString());
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
                if (data != null && data.ResultObj != null && data.ResultObj.Items != null)
                {
                    dgvMain.DataSource = data.ResultObj.Items;
                }
                cboFields.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clear filter: " + ex.ToString());
            }

        }

        private List<MonopolyViewModel> FilterData(List<MonopolyViewModel> source)
        {
            isFilter = true;
            List<MonopolyViewModel> fill = new List<MonopolyViewModel>();
            try
            {
                
                if (cboTypeChoise == 0)
                {
                    //var query = source.Where(delegate (MonopolyViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeOld).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    //string a = txtFind.Text.Trim();
                    var query = source.Where(c => c.CodeOld.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = source.Where(delegate (MonopolyViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.CodeNew.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = source.Where(delegate (MonopolyViewModel c)
                    //{
                    //    if (c.Name2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Name2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = source.Where(delegate (MonopolyViewModel c)
                    //{
                    //    if (c.Own2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Own2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = source.Where(delegate (MonopolyViewModel c)
                    //{
                    //    if (c.NameType.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.NameType.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                //dgvMain.Invoke(new MethodInvoker(delegate
                //{
                //    dgvMain.DataSource = fill;
                //}));
                //statusMain.Invoke(new MethodInvoker(delegate
                //{
                //    lbOperation.Text = $"Filter data, total record(s): {fill.Count}";
                //}));
            }
            catch (Exception)
            {
                fill = source.ToList();
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
            isFilter = false;
            return fill;
        }
        List<MonopolyViewModel> fill = new List<MonopolyViewModel>();
        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    fill = new List<MonopolyViewModel>();
                    return;
                }
                int field = 0;
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    field = cboFields.SelectedIndex;
                }));

                //List<MonopolyViewModel> fill = new List<MonopolyViewModel>();
                if (field == 0)
                {
                    fill = data.ResultObj.Items.ToList();
                }
                else if (field == 1)
                {
                    fill = data.ResultObj.Items.Where(p => p.Tone == true).ToList();
                }
                else if (field == 2)
                {
                    fill = data.ResultObj.Items.Where(p => p.Web == true).ToList();
                }
                else if (field == 3)
                {
                    fill = data.ResultObj.Items.Where(p => p.Performances == true).ToList();
                }
                else if (field == 4)
                {
                    fill = data.ResultObj.Items.Where(p => p.PerformancesHCM == true).ToList();
                }
                else if (field == 5)
                {
                    fill = data.ResultObj.Items.Where(p => p.Cddvd == true).ToList();
                }
                else if (field == 6)
                {
                    fill = data.ResultObj.Items.Where(p => p.Kok == true).ToList();
                }
                else if (field == 7)
                {
                    fill = data.ResultObj.Items.Where(p => p.Broadcasting == true).ToList();
                }
                else if (field == 8)
                {
                    fill = data.ResultObj.Items.Where(p => p.Entertaiment == true).ToList();
                }
                else if (field == 9)
                {
                    fill = data.ResultObj.Items.Where(p => p.Film == true).ToList();
                }
                else if (field == 10)
                {
                    fill = data.ResultObj.Items.Where(p => p.Advertisement == true).ToList();
                }
                else if (field == 11)
                {
                    fill = data.ResultObj.Items.Where(p => p.PubMusicBook == true).ToList();
                }
                else if (field == 12)
                {
                    fill = data.ResultObj.Items.Where(p => p.Youtube == true).ToList();
                }
                else if (field == 13)
                {
                    fill = data.ResultObj.Items.Where(p => p.Other == true).ToList();
                }
                fill = FilterData(fill);
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

                throw;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeChoise = cboType.SelectedIndex;
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
                    frmMonopolyUpdate frm = new frmMonopolyUpdate(controller, UpdataType.View, CurrenObject, cboGroup.SelectedIndex);
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
        private void backgroundWorker_DoMonopoly(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.GetDataFromServer)
                {
                    LoadData(currentPage);
                }
                else if (Operation == OperationType.Filter)
                {
                    if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                    {
                        return;
                    }
                    //FilterData(data.ResultObj.Items);
                    //FilterData(fill);
                    cboFields_SelectedIndexChanged(null, null);
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel();
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

        private void backgroundMonopolyer_RunMonopolyerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }

        #endregion
    }
}
