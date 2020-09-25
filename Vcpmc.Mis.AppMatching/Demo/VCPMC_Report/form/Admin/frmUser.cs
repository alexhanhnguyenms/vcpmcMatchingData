using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Admin;
using Vcpmc.Mis.AppMatching.form.Admin.Update;
using Vcpmc.Mis.AppMatching.Services.Admin;
using Vcpmc.Mis.Common;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.form.Admin
{
    public partial class frmUser : Form
    {
        #region vari       
        UserViewModel CurrenObject = null;
        UserController controller;
        UserApiClient apiClient;
        RoleApiClient apiClientRole;
        ApiResult<PagedResult<UserViewModel>> data = new ApiResult<PagedResult<UserViewModel>>();

        List<RoleViewModel> RoleList = new List<RoleViewModel>();
        OperationType Operation = OperationType.LoadExcel;
        bool isRequest = false;
        bool isFilter = false;
        #endregion

        #region init
        public frmUser()
        {
            InitializeComponent();
            apiClient = new UserApiClient(Core.Client);
            apiClientRole = new RoleApiClient(Core.Client);
            controller = new UserController(apiClient, apiClientRole);
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            
            cboType.SelectedIndex = 0;
            btnRefresh_Click(null, null);
        }
        #endregion
        #region LoadData
        private async void LoadData(int PageIndex)
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<UserViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<UserViewModel>();
                }));
                //load role
                var rolep = await controller.GetAllPagingRole();
                if (rolep.IsSuccessed)
                {
                    if (rolep.ResultObj.Items != null)
                    {
                        RoleList = rolep.ResultObj.Items;
                    }
                }
                data = await controller.GetAllPaging();
                DateTime endtime = DateTime.Now;
                if (data.IsSuccessed)
                {
                    lbTotalPage.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage.Text = data.ResultObj.PageCount.ToString();
                    }));
                    txtPageCurrent.Invoke(new MethodInvoker(delegate
                    {
                        txtPageCurrent.Value = data.ResultObj.PageIndex;
                    }));
                    //richinfo.Invoke(new MethodInvoker(delegate
                    //{
                    //    richinfo.Text = "";
                    //    richinfo.Text += $"Total record(s): {data.ResultObj.TotalRecords}{Environment.NewLine}";
                    //    richinfo.Text += $"Page index: {data.ResultObj.PageIndex}{Environment.NewLine}";
                    //    richinfo.Text += $"Page count: {data.ResultObj.PageCount}{Environment.NewLine}";
                    //    richinfo.Text += $"Page size: {data.ResultObj.PageSize}{Environment.NewLine}";
                    //    richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                    //    richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                    //    richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                    //}));
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = data.ResultObj.Items;

                        for (int i = 0; i < dgvMain.Rows.Count; i++)
                        {
                            if (dgvMain.Rows[i].Cells["IsLock"].Value != null && (bool)dgvMain.Rows[i].Cells["IsLock"].Value)
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Red;                               
                            }
                            else
                            {
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {data.ResultObj.Items.Count}";
                    }));
                    EnablePagging(data);
                }
                else
                {
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<UserViewModel>();
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
                lbInfo.Text = $"Search data from server, total record(s): no reponse";
            }));
        }

        private void EnablePagging(ApiResult<PagedResult<UserViewModel>> data)
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
            if (data.ResultObj.PageIndex < data.ResultObj.PageCount)
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
            if (data.ResultObj.PageIndex == data.ResultObj.PageCount)
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
        int currentPage = 1;
        private void btnRefresh_Click(object sender, EventArgs e)
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
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPage = 1;
                #region set request
                //request.Group = cboGroup.SelectedIndex;
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
                currentPage = data.ResultObj.PageCount;
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
                    else if (((int)txtPageCurrent.Value) > data.ResultObj.PageCount)
                    {
                        txtPageCurrent.Value = data.ResultObj.PageCount;
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
                CurrenObject = new UserViewModel();
                frmUserUpdate frm = new frmUserUpdate(controller, UpdataType.Add, CurrenObject, RoleList);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Added Item: {frm.ObjectReturn.Message}, total Added: {frm.ObjectReturn.TotalEffect}";
                    #region set background
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
                frmUserUpdate frm = new frmUserUpdate(controller, UpdataType.Edit, CurrenObject, RoleList);
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
                bool isAdmin = false;
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
                        isAdmin = (bool)dgvMain.Rows[i].Cells["isAdmin"].Value;
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
                if (isAdmin)
                {
                    MessageBox.Show("The user is Admin, Don't delete!");
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

        #region btn
        private async void btnUnlock_Click(object sender, EventArgs e)
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
                var confirmResult = MessageBox.Show("Are you sure to Unlock account?",
                                     "Confirm unlock account!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var varStatus = await controller.UnLock(CurrenObject.Id);
                    if (varStatus != null && varStatus.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbOperation.Text = $"Unlock account record: {varStatus.Message}";
                        #region set backgroundMonopolyer
                        Operation = OperationType.GetDataFromServer;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker.RunWorkerAsync();
                        #endregion

                    }
                    else
                    {
                        if (varStatus != null)
                        {
                            lbOperation.Text = $"Unlock account: {varStatus.Message}";
                        }
                        else
                        {
                            lbOperation.Text = $"Unlock account is failure";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        private async void btnLock_Click(object sender, EventArgs e)
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
                var confirmResult = MessageBox.Show("Are you sure to lock account?",
                                     "Confirm lock account!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var varStatus = await controller.Lock(CurrenObject.Id);
                    if (varStatus != null && varStatus.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbOperation.Text = $"Lock account record: {varStatus.Message}";
                        #region set backgroundMonopolyer
                        Operation = OperationType.GetDataFromServer;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker.RunWorkerAsync();
                        #endregion

                    }
                    else
                    {
                        if (varStatus != null)
                        {
                            lbOperation.Text = $"Lock account: {varStatus.Message}";
                        }
                        else
                        {
                            lbOperation.Text = $"Lock account is failure";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        private async void btnResetPassword_Click(object sender, EventArgs e)
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
                var confirmResult = MessageBox.Show("Are you sure to reset password?",
                                     "Confirm reset!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    string passwordDefault = RandomPasswordGenerator.GeneratePassword(true,true,true,true,10);
                    var varStatus = await controller.ResetPassword(CurrenObject.Id, passwordDefault);
                    if (varStatus != null && varStatus.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbOperation.Text = $"Reset password: {varStatus.Message}";
                        frmUpdatePassword frm = new frmUpdatePassword(controller, true, CurrenObject.UserName, passwordDefault);
                        frm.ShowDialog();
                        #region set backgroundMonopolyer
                        Operation = OperationType.GetDataFromServer;
                        pcloader.Visible = true;
                        pcloader.Dock = DockStyle.Fill;
                        backgroundWorker.RunWorkerAsync();
                        #endregion

                    }
                    else
                    {
                        if (varStatus != null)
                        {
                            lbOperation.Text = $"Reset password: {passwordDefault}, message: {varStatus.Message}";
                        }
                        else
                        {
                            lbOperation.Text = $"Reset password is failure";
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
                List<UserViewModel> fill = new List<UserViewModel>();
                if (cboTypeChoise == 0)
                {
                    var query = data.ResultObj.Items.Where(delegate (UserViewModel c)
                    {
                        if (VnHelper.ConvertToUnSign(c.UserName).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    var query = data.ResultObj.Items.Where(delegate (UserViewModel c)
                    {
                        if (VnHelper.ConvertToUnSign(c.Email).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    var query = data.ResultObj.Items.Where(delegate (UserViewModel c)
                    {
                        if (VnHelper.ConvertToUnSign(c.Role).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
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
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count == 0)
                {
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
        private void dgvMain_MouseClick(object sender, MouseEventArgs e)
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
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count > 0)
                {
                    if(dgvMain.CurrentRow.Cells["IsLock"].Value!=null)
                    {
                        bool isLock = (bool)dgvMain.CurrentRow.Cells["IsLock"].Value;
                        if(isLock)
                        {
                            btnUnlock.Enabled = true;
                        }
                        else
                        {
                            btnUnlock.Enabled = false;
                        }
                    }  
                    else
                    {
                        btnUnlock.Enabled = false;                       
                    }
                    btnLock.Enabled = !btnUnlock.Enabled;
                    if((bool)dgvMain.CurrentRow.Cells["IsAdmin"].Value)
                    {
                        btnLock.Enabled = false;
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
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    return;
                }
                if (dgvMain.Rows.Count > 0)
                {
                    string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;
                    CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).First();
                    if (CurrenObject == null)
                    {
                        MessageBox.Show("Eror: recode is null");
                        return;
                    }
                    frmUserUpdate frm = new frmUserUpdate(controller, UpdataType.View, CurrenObject, RoleList);
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
        
        private void backgroundMonopolyer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }
        #endregion        
    }
}
