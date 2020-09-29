using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.System;
using Vcpmc.Mis.AppMatching.form.config.Update;
using Vcpmc.Mis.AppMatching.Services.System;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared.Parameter;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.AppMatching.form.config
{
    public partial class frmFixParameter : Form
    {
        #region vari
        MasterPageViewModel master = new MasterPageViewModel();
        bool searchPageFirst = true;
        //OperationType operation = OperationType.LoadExcel;
        FixParameterViewModel CurrenObject = null;
        FixParameterController controller;
        FixParameterApiClient apiClient;
        GetFixParameterPagingRequest request = new GetFixParameterPagingRequest();
        ApiResult<PagedResult<FixParameterViewModel>> data = new ApiResult<PagedResult<FixParameterViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        int currentPage = 1;
        int totalPage = 1;
        UpdataType _type = UpdataType.All;
        string filepath = string.Empty;
        bool isRequest = false;
        bool isFilter = false;
        #endregion
        public frmFixParameter(Vcpmc.Mis.Common.enums.UpdataType type)
        {
            InitializeComponent();
            apiClient = new FixParameterApiClient(Core.Client);
            controller = new FixParameterController(apiClient);
            _type = type;
        }

        private void frmFixParameter_Load(object sender, EventArgs e)
        {
            if (_type == UpdataType.View)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnImport.Visible = false;
            }
            //TypeFixParameter typefix = new TypeFixParameter();
            List<string> MyNames = ((TypeFixParameter[])Enum.GetValues(typeof(TypeFixParameter))).Select(c => c.ToString()).ToList();
            cboTypeFixParameter.Items.AddRange(MyNames.ToArray());
            cboTypeFixParameter.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            btnSearch_Click(null,null);
        }       
       
        #region LoadData
        private async void LoadData(int PageIndex)
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<FixParameterViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<FixParameterViewModel>();
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
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = data.ResultObj.Items;
                    }));
                    //cboFields_SelectedIndexChanged(null, null);
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Search data from serve, total record(s): {data.ResultObj.Items.Count}";
                    }));
                    EnablePagging(data, totalPage);
                    cboTypeFixParameter_SelectedIndexChanged(null, null);
                }
                else
                {
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<FixParameterViewModel>();
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

        private void EnablePagging(ApiResult<PagedResult<FixParameterViewModel>> data, int pageTotal)
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

                searchPageFirst = true;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPage = 1;
                totalPage = 1;
                #region set request
                //request.Type = cboGroup.SelectedIndex;
                //request.Key = Core.LimitRequest;
                request.PageSize = Core.LimitRequestFixParameter;
                #endregion

                #region set backgroundFixParameterer
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
                #region set backgroundFixParameterer
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
                #region set backgroundFixParameterer
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
                #region set backgroundFixParameterer
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
                #region set backgroundFixParameterer
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
                    #region set backgroundFixParameterer
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
                TypeFixParameter typeFixParameter = new TypeFixParameter();
                if(cboTypeFixParameter.Text == "All" || cboTypeFixParameter.Text == "IpWorkRoleToE")
                {
                    typeFixParameter = TypeFixParameter.IpWorkRoleToE;
                }
                else if (cboTypeFixParameter.Text == "MatchingReplateTitle")
                {
                    typeFixParameter = TypeFixParameter.MatchingReplateTitle;
                }
                else if (cboTypeFixParameter.Text == "MatchingReplateWriter")
                {
                    typeFixParameter = TypeFixParameter.MatchingReplateWriter;
                }
                else if (cboTypeFixParameter.Text == "NonMemberToMember")
                {
                    typeFixParameter = TypeFixParameter.NonMemberToMember;
                }
                CurrenObject = new FixParameterViewModel();
                frmFixParameterUpdate frm = new frmFixParameterUpdate(controller, UpdataType.Add, CurrenObject, typeFixParameter);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Added Item: {frm.ObjectReturn.Message}, total Added: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundFixParameterer
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
                TypeFixParameter typeFixParameter = new TypeFixParameter();
                if (cboTypeFixParameter.Text == "All" || cboTypeFixParameter.Text == "IpWorkRoleToE")
                {
                    typeFixParameter = TypeFixParameter.IpWorkRoleToE;
                }
                else if (cboTypeFixParameter.Text == "MatchingReplateTitle")
                {
                    typeFixParameter = TypeFixParameter.MatchingReplateTitle;
                }
                else if (cboTypeFixParameter.Text == "MatchingReplateWriter")
                {
                    typeFixParameter = TypeFixParameter.MatchingReplateWriter;
                }
                else if (cboTypeFixParameter.Text == "NonMemberToMember")
                {
                    typeFixParameter = TypeFixParameter.NonMemberToMember;
                }
                frmFixParameterUpdate frm = new frmFixParameterUpdate(controller, UpdataType.Edit, CurrenObject, typeFixParameter);
                frm.ShowDialog();
                if (frm.ObjectReturn != null && frm.ObjectReturn.Status == Utilities.Common.UpdateStatus.Successfull)
                {
                    lbOperation.Text = $"Edited Item: {frm.ObjectReturn.Message}, total Edited: {frm.ObjectReturn.TotalEffect}";
                    #region set backgroundFixParameterer
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
                        #region set backgroundFixParameterer
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
                #region set backgroundFixParameterer
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
                if (fill != null && fill.Count > 0)
                {
                    //var datax = dataLoad.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                    //index += Core.LimitDisplayExportExcel;
                    //bool check = WriteReportHelper.ExportFixParameter(fill, filepath);
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

                #region set backgroundFixParameterer
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
                cboTypeFixParameter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clear filter: " + ex.ToString());
            }

        }

        private List<FixParameterViewModel> FilterData(List<FixParameterViewModel> source)
        {
            isFilter = true;
            List<FixParameterViewModel> fill = new List<FixParameterViewModel>();
            try
            {
                if (cboTypeChoise == 0)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeOld).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Key.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value1.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value3.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value4.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 5)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value5.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 6)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value6.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 7)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value7.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 8)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value8.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 9)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value9.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 10)
                {
                    //var query = source.Where(delegate (FixParameterViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.CodeNew).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = source.Where(c => c.Value10.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                isFilter = false;
            }
            catch (Exception)
            {
                isFilter = false;
                fill = source.ToList();
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
            isFilter = false;
            return fill;
        }
        List<FixParameterViewModel> fill = new List<FixParameterViewModel>();       

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTypeChoise = cboType.SelectedIndex;
        }        
        private void cboTypeFixParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    fill = new List<FixParameterViewModel>();
                    return;
                }
                string fix = "";
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    fix = cboTypeFixParameter.Text;
                }));
                if(fix=="All")
                {
                    fill = data.ResultObj.Items.ToList();
                }
                else
                {
                    fill = data.ResultObj.Items.Where(p => p.Type == fix).ToList();
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
                    TypeFixParameter typeFixParameter = new TypeFixParameter();
                    if (cboTypeFixParameter.Text == "All" || cboTypeFixParameter.Text == "IpWorkRoleToE")
                    {
                        typeFixParameter = TypeFixParameter.IpWorkRoleToE;
                    }
                    else if (cboTypeFixParameter.Text == "MatchingReplateTitle")
                    {
                        typeFixParameter = TypeFixParameter.MatchingReplateTitle;
                    }
                    else if (cboTypeFixParameter.Text == "MatchingReplateWriter")
                    {
                        typeFixParameter = TypeFixParameter.MatchingReplateWriter;
                    }
                    else if (cboTypeFixParameter.Text == "NonMemberToMember")
                    {
                        typeFixParameter = TypeFixParameter.NonMemberToMember;
                    }
                    frmFixParameterUpdate frm = new frmFixParameterUpdate(controller, UpdataType.View, CurrenObject, typeFixParameter);
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
        private void backgroundWorker_DoFixParameter(object sender, DoWorkEventArgs e)
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
                    //cboFields_SelectedIndexChanged(null, null);
                    cboTypeFixParameter_SelectedIndexChanged(null, null);
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

        private void backgroundFixParameterer_RunFixParametererCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }


        #endregion

        private void richinfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
