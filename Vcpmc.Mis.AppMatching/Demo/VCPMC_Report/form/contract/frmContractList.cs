
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.ApplicationCore.Entities.contract;
using Vcpmc.Mis.AppMatching.form.contract.report;
using Vcpmc.Mis.AppMatching.form.contract.update;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.contract
{
    public partial class frmContractList : Form
    {
        #region vari
        //OperationType operation = OperationType.LoadExcel;
        //string currentDirectory = "";
        VcpmcContext ctx = new VcpmcContext();
        ContractObject CurrenContractObject = null;
        List<ContractObject> contractObjects = new List<ContractObject>();       
        #endregion

        #region Load
        public frmContractList()
        {
            InitializeComponent();
        }

        private void frmContractList_Load(object sender, EventArgs e)
        {
            LoadDta();
        }
        #endregion

        #region Change + load data     
        private void LoadDta()
        {
            try
            {
                numMonth.Value = DateTime.Now.Month;
                numYear.Value = DateTime.Now.Year;
                cboType.SelectedIndex = 0;
                //kiêm tra mã cũ đã code chưa                    
                contractObjects = (from s in ctx.ContractObjects                                                                        
                                   orderby s.No ascending
                                   select s
                         ).ToList();
                dgvMain.DataSource = contractObjects;
            }
            catch (Exception )
            {
                MessageBox.Show("Load data is error");
            }
        }
        #endregion

        #region Update        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CurrenContractObject = new ContractObject();
                frmUpdateContract frm = new frmUpdateContract(ctx, UpdataType.Add, CurrenContractObject);
                frm.ShowDialog();
                LoadDta();
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
                int id = -1;
                int count = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        count++;
                        id = (int)dgvMain.Rows[i].Cells["Id"].Value;
                        if (count > 1)
                        {
                            MessageBox.Show("Please choise only one record!");
                            return;
                        }
                    }
                }
                if (id == -1)
                {
                    MessageBox.Show("Please must choise one record!");
                    return;
                }
                CurrenContractObject = ctx.ContractObjects.Where(s => s.Id == id).First();
                if (CurrenContractObject == null)
                {
                    MessageBox.Show("Eror: recode is null");
                    return;
                }
                frmUpdateContract frm = new frmUpdateContract(ctx, UpdataType.Edit, CurrenContractObject);
                frm.ShowDialog();
                LoadDta();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = -1;
                int count = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        count++;
                        id = (int)dgvMain.Rows[i].Cells["Id"].Value;
                        if (count > 1)
                        {
                            MessageBox.Show("Please choise only one record!");
                            return;
                        }
                    }
                }
                if (id == -1)
                {
                    MessageBox.Show("Please must choise one record!");
                    return;
                }
                CurrenContractObject = ctx.ContractObjects.Where(s => s.Id == id).First();
                if (CurrenContractObject == null)
                {
                    MessageBox.Show("Eror: recode is null");
                    return;
                }
                var confirmResult = MessageBox.Show("Are you sure to delete this record?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    ctx.ContractObjects.Remove(CurrenContractObject);
                    int check = ctx.SaveChanges();
                    if (check < 1)
                    {
                        MessageBox.Show("Delete is error");
                    }
                    else
                    {
                        LoadDta();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btnRehresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDta();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        #endregion

        #region Import
        private void btnBrower_Click(object sender, EventArgs e)
        {
            try
            {
                txtPath.Text = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv|Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        txtPath.Text = filePath;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Find
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                List<ContractObject> fill = new List<ContractObject>();               
                if (cboType.SelectedIndex == 0)
                {
                    //fill = contractObjects.Where(x => x.Customer.Contains(txtFind.Text.Trim())).ToList();
                    var query = contractObjects.Where(delegate (ContractObject c)
                    {
                        if (VnHelper.ConvertToUnSign(c.Customer).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboType.SelectedIndex == 1)
                {
                    //fill = contractObjects.Where(x => x.License.Contains(txtFind.Text.Trim())).ToList();
                    var query = contractObjects.Where(delegate (ContractObject c)
                    {
                        if (VnHelper.ConvertToUnSign(c.License).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboType.SelectedIndex == 2)
                {
                    //fill = contractObjects.Where(x => x.ContractNumber.Contains(txtFind.Text.Trim())).ToList();
                    var query = contractObjects.Where(delegate (ContractObject c)
                    {
                        if (VnHelper.ConvertToUnSign(c.ContractNumber).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                dgvMain.DataSource = fill;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        private void btnFind2_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime tempEndTime = new DateTime((int)numYear.Value, (int)numMonth.Value, 1).AddMonths(1).AddDays(-1);
                DateTime tempEndTime = new DateTime((int)numYear.Value, (int)numMonth.Value, 1,23,59,59).AddDays(-1);
                List<ContractObject> fill = new List<ContractObject>();
                if (cboType.SelectedIndex == 0)
                {
                    //fill = contractObjects.Where(x => x.Customer.Contains(txtFind.Text.Trim())).ToList();
                    var query = contractObjects.Where(s=>s.EndTime > tempEndTime).AsQueryable();
                    fill = query.ToList();
                }
                dgvMain.DataSource = fill;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
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
        private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvMain.Rows.Count > 0)
                {
                    int id = (int)dgvMain.CurrentRow.Cells["Id"].Value;
                    CurrenContractObject = ctx.ContractObjects.Where(s => s.Id == id).First();
                    if (CurrenContractObject == null)
                    {
                        MessageBox.Show("Eror: recode is null");
                        return;
                    }
                    frmUpdateContract frm = new frmUpdateContract(ctx, UpdataType.View, CurrenContractObject);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region printer
        private void btnPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                int id = -1;
                int count = 0;
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (dgvMain.Rows[i].Cells[0].Value != null && (bool)dgvMain.Rows[i].Cells[0].Value == true)
                    {
                        count++;
                        id = (int)dgvMain.Rows[i].Cells["Id"].Value;
                        if (count > 1)
                        {
                            MessageBox.Show("Please choise only one record!");
                            return;
                        }
                    }
                }
                if (id == -1)
                {
                    MessageBox.Show("Please must choise one record!");
                    return;
                }
                CurrenContractObject = ctx.ContractObjects.Where(s => s.Id == id).First();
                if (CurrenContractObject == null)
                {
                    MessageBox.Show("Eror: recode is null");
                    return;
                }
                frmContracPrinter frm = new frmContracPrinter();
                frm.ShowDialog();
                //LoadDta();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }
        }
        #endregion

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
