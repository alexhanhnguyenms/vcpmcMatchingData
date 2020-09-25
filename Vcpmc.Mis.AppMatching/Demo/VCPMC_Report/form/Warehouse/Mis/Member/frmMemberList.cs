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
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Member
{
    public partial class frmMemberList : Form
    {
        #region vari
        MasterPageViewModel master = new MasterPageViewModel();
        bool searchPageFirst = true;
        //OperationType operation = OperationType.LoadExcel;
        //MemberViewModel CurrenObject = null;
        MemberController controller;
        MemberApiClient ApiClient;
        GetMemberPagingRequest request = new GetMemberPagingRequest();
        ApiResult<PagedResult<MemberViewModel>> data = new ApiResult<PagedResult<MemberViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        int currentPage = 1;
        int totalPage = 1;
        UpdataType _type = UpdataType.All;
        bool isRequest = false;
        bool isFilter = false;
        #endregion

        #region innit
        public frmMemberList(UpdataType type)
        {
            InitializeComponent();
            ApiClient = new MemberApiClient(Core.Client);
            controller = new MemberController(ApiClient);
            _type = type;
        }
        private void frmMemberList_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 2;
        }
        #endregion

        

        #region LoadData
        private async void LoadData(int PageIndex)
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<MemberViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<MemberViewModel>();
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
                    //    dgvMain.DataSource = new List<MemberViewModel>();
                    //}));
                    DirectionNarrowDisable();
                }
                isRequest = false;
            }
            catch (Exception)
            {
                isRequest = false;
                //MessageBox.Show($"Load data is error: {ex.ToString()}");
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

        private void EnablePagging(ApiResult<PagedResult<MemberViewModel>> data, int pageTotal)
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtIPI_NUMBER.Text = string.Empty;
                txtInternalNo.Text = string.Empty;
                txtIP_ENGLISH_NAME.Text = string.Empty;
                txtIP_NAME_Type.Text = string.Empty;
                txtSociety.Text = string.Empty;                
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
                data = new ApiResult<PagedResult<MemberViewModel>>();
                data.ResultObj = new PagedResult<MemberViewModel>();
                data.ResultObj.Items = new List<MemberViewModel>();
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
                request.IpiNumber = txtIPI_NUMBER.Text.Trim().ToUpper();
                request.InternalNo = txtInternalNo.Text.Trim().ToUpper();
                request.IpEnglishName = txtIP_ENGLISH_NAME.Text.Trim().ToUpper();
                request.NameType = txtIP_NAME_Type.Text.Trim().ToUpper();
                request.Society = txtSociety.Text.Trim().ToUpper();                
                request.PageSize = Core.LimitRequestMemberList;
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
                List<MemberViewModel> fill = new List<MemberViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WK_INT_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.IpiNumber.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.TTL_ENG).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.InternalNo.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ISWC_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.IpEnglishName.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WRITER).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.NameType.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = data.ResultObj.Items.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.ARTIST).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = data.ResultObj.Items.Where(c => c.Society.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
        //private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (dgvMain.CurrentCell.ColumnIndex == 0)
        //        {
        //            if (dgvMain.CurrentCell.Value == null || (bool)dgvMain.CurrentCell.Value == false)
        //            {
        //                dgvMain.CurrentCell.Value = true;
        //            }
        //            else
        //            {
        //                dgvMain.CurrentCell.Value = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        //private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
        //        {
        //            return;
        //        }
        //        if (dgvMain.Rows.Count > 0)
        //        {
        //            string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;
        //            CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).FirstOrDefault();
        //            if (CurrenObject == null)
        //            {
        //                MessageBox.Show("Eror: recode is null");
        //                return;
        //            }
        //            frmWorkUpdate frm = new frmWorkUpdate(controller, UpdataType.View, CurrenObject);
        //            frm.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

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

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }

        #endregion

        #region Export
        string filepath = string.Empty;
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
        private void ExportToExcel(string filePath)
        {
            try
            {
                #region export
                if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Data is empty, so not export to excel";
                    }));
                    return;
                }
                //var dataExport = data.ResultObj.Items.Where()
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
                string x2 = filePath.Substring(pos + 1, filePath.Length - pos - 1);
                string[] file = new string[] { x1, x2 };
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
                        bool check = WriteReportHelper.ExportMember(datax, $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}");
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
                #endregion

            }
            catch (Exception )
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Export to excel is error!";
                }));
            }
        }
        #endregion
    }
}
