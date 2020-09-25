using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Admin;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.AppMatching.form.Admin
{
    public partial class frmAssignRight : Form
    {
        #region vari
        OperationType Operation = OperationType.LoadExcel;
        ApiResult<PagedResult<AppClaimViewModel>> data = new ApiResult<PagedResult<AppClaimViewModel>>();      
        List<AppClaimViewModel> listChoise = new List<AppClaimViewModel>();             
        private RoleController _Controller;
        public  UpdateStatusViewModel ObjectReturn = null;
        RoleViewModel _currentRoleObject;
        bool isRequest = false;
        bool isCommit = false;
        #endregion      

        #region Init
        public frmAssignRight(RoleController controller, RoleViewModel model)
        {
            InitializeComponent();
            _Controller = controller;
            _currentRoleObject = model;
        }
        private void frmAssignRight_Load(object sender, EventArgs e)
        {
            this.Text = $"Assign right-[Role: {_currentRoleObject.Name}]";
            Operation = OperationType.GetDataFromServer;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();            
        }
        #endregion

        #region btn
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (isRequest)
            {
                lbInfo.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "Waiting reponse...";
                }));
                return;
            }
            if (isCommit)
            {
                lbInfo.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "Waiting commit...";
                }));
                return;
            }
            Operation = OperationType.GetDataFromServer;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();
        }

        private void btCommit_Click(object sender, EventArgs e)
        {
            Operation = OperationType.SaveDatabase;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region Function
        private async void RefreshData()
        {
            try
            {
                isRequest = true;
                DateTime startTime = DateTime.Now;
                data = new ApiResult<PagedResult<AppClaimViewModel>>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = new List<AppClaimViewModel>();
                }));
                data = await _Controller.GetAllPagingAppClaim();
                DateTime endtime = DateTime.Now;
                if (data.IsSuccessed)
                {
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = data.ResultObj.Items;
                    }));
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Load data from server, total record(s): {data.ResultObj.Items.Count}";
                        lbInfo.Text += $", total time: {(endtime - startTime).TotalSeconds} (s)";
                    }));
                    MapData();
                }
                else
                {
                    //dgvMain.Invoke(new MethodInvoker(delegate
                    //{
                    //    dgvMain.DataSource = new List<AppClaimViewModel>();
                    //}));
                }
                isRequest = false;
            }
            catch (Exception ex)
            {
                isRequest = false;
                MessageBox.Show($"Load data is error: {ex.ToString()}");
            }
        }
        private void MapData()
        {
            if(_currentRoleObject.Rights!=null&& _currentRoleObject.Rights.Count>0)
            {
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    foreach (var item in _currentRoleObject.Rights)
                    {
                        for (int i = 0; i < dgvMain.Rows.Count; i++)
                        {
                            if (dgvMain.Rows[i].Cells["Group"].Value.ToString().Trim() == item.Group
                                && dgvMain.Rows[i].Cells["Claim"].Value.ToString().Trim() == item.Claim)
                            {
                                dgvMain.Rows[i].Cells[0].Value = true;
                                dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                                break;
                            }
                        }
                    }
                }));                
                lbChoise.Invoke(new MethodInvoker(delegate
                {
                    lbChoise.Text = $"Selected: {_currentRoleObject.Rights.Count }";
                }));
            }
        }
        private void GetData()
        {
            listChoise.Clear();
            dgvMain.Invoke(new MethodInvoker(delegate
            {
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value)
                    {                        
                        var item = data.ResultObj.Items.Where(
                                p => p.Group == dgvMain.Rows[i].Cells["Group"].Value.ToString().Trim()
                                && p.Claim == dgvMain.Rows[i].Cells["Claim"].Value.ToString().Trim()
                            ).FirstOrDefault();
                        if (item != null)
                        {
                            listChoise.Add(item);
                        }
                    }
                }
            }));
            
            _currentRoleObject.Rights = listChoise;
        }
        private async void Commit()
        {
            try
            {
                isCommit = true;
                GetData();
                RoleUpdateRequest roleUpdate = new RoleUpdateRequest { 
                    Id = _currentRoleObject.Id,
                    Name = _currentRoleObject.Name,
                    Description = _currentRoleObject.Description,
                    Rights = _currentRoleObject.Rights,
                };
                var data = await _Controller.Update(roleUpdate);
                ObjectReturn = data;
                if (data != null)
                {
                    if (data.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbInfo.Invoke(new MethodInvoker(delegate
                        {
                            lbInfo.Text = $"Commit is successfull: {data.Message}";
                        }));
                    }
                    else
                    {
                        lbInfo.Invoke(new MethodInvoker(delegate
                        {
                            lbInfo.Text = $"Commit is failure: {data.Message}";
                        }));                        
                    }
                    isCommit = false;
                    return;
                }
                else
                {
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "No reponse";
                    }));                    
                }
                isCommit = false;
            }
            catch (Exception ex)
            {
                isCommit = false;
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
                    RefreshData();
                }
                else if (Operation == OperationType.SaveDatabase)
                {
                    Commit();
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

        #region dgv
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (isRequest)
                {
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Waiting reponse...";
                    }));
                    return;
                }
                if (isCommit)
                {
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Waiting commit...";
                    }));
                    return;
                }
                if (dgvMain.Rows.Count==0)
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
                    int total = 0;
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                        {
                            dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                            total++;
                        }
                        else
                        {
                            dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    lbChoise.Invoke(new MethodInvoker(delegate
                    {
                        lbChoise.Text = $"Selected: {total}";
                    }));
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {

                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void cheCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isRequest)
                {
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Waiting reponse...";
                    }));
                    return;
                }
                if (isCommit)
                {
                    lbInfo.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Waiting commit...";
                    }));
                    return;
                }
                if (dgvMain.Rows.Count == 0)
                {
                    return;
                }
                int total = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    dgvMain.Rows[i].Cells[0].Value = cheCheckAll.Checked;
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                        total++;
                    }
                    else
                    {
                        dgvMain.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                lbChoise.Invoke(new MethodInvoker(delegate
                {
                    lbChoise.Text = $"Selected: {total}";
                }));
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion       
    }
}
