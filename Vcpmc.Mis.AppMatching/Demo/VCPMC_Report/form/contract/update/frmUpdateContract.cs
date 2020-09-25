using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.ApplicationCore.Entities.contract;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.contract.update
{
    public partial class frmUpdateContract : Form
    {
        #region variable
        VcpmcContext _ctx = null;
        UpdataType _updataType = UpdataType.Add;
        ContractObject _contractObjects = null;
        
        #endregion
        public frmUpdateContract(VcpmcContext ctx, UpdataType updataType = UpdataType.Add, ContractObject contractObjects = null)
        {
            InitializeComponent();
            this._updataType = updataType;
            this._contractObjects = contractObjects;           
            this._ctx = ctx;
        }

        private void frmUpdateContract_Load(object sender, EventArgs e)
        {
            LoadFrom();
        }


        #region Data        
        private void LoadFrom()
        {                       
            if (_updataType == UpdataType.Edit || _updataType == UpdataType.View)
            {
                if (_updataType == UpdataType.Edit)
                {
                    this.Text = "Update Contract";
                }
                else
                {
                    this.Text = "View Contract";
                }
                if (_contractObjects == null)
                {
                    return;
                }
                #region Common
                txtNo.Text = _contractObjects.No.ToString();
                txtCustomer.Text = _contractObjects.Customer;
                txtAddress.Text = _contractObjects.Address;
                txtDistrict.Text = _contractObjects.District;
                txtPhone.Text = _contractObjects.Phone;
                txtContact.Text = _contractObjects.Contact;
                txtTaxCode.Text = _contractObjects.TaxCode;
                #endregion

                #region Contract
                txtLicense.Text = _contractObjects.License;
                txtContractNumber.Text = _contractObjects.ContractNumber;
                CboField.Text = _contractObjects.Field;

                dtContractTime.Value = _contractObjects.ContractTime;
                dtStartTime.Value = _contractObjects.StartTime;
                dtEndTime.Value = _contractObjects.EndTime;

                txtNameSign.Text = _contractObjects.NameSign;
                numVat.Value = _contractObjects.Vat;
                numValue.Value = _contractObjects.Value;
                numValueVAT.Value = _contractObjects.ValueVAT;

                txtAddress2.Text = _contractObjects.Address2;
                txtNote.Text = _contractObjects.Note;
                cheReSigned.Checked = _contractObjects.IsReSigned;
                txtNoteResign.Text = _contractObjects.NoteReSigned;
                #endregion

                #region Position
                txtGround.Text = _contractObjects.Ground;
                txtBadger.Text = _contractObjects.Badger;
                txtFloor1.Text = _contractObjects.Floor1;
                txtFloor2.Text = _contractObjects.Floor2;
                txtFloor3.Text = _contractObjects.Floor3;
                txtFloor4.Text = _contractObjects.Floor4;
                txtFloor5.Text = _contractObjects.Floor5;
                txtFloor6.Text = _contractObjects.Floor6;
                txtFloor7.Text = _contractObjects.Floor7;
                txtFloor8.Text = _contractObjects.Floor8;
                txtFloor9.Text = _contractObjects.Floor9;
                txtFloor10.Text = _contractObjects.Floor10;
                txtTerrace.Text = _contractObjects.Terrace;

                numCountGround.Value = _contractObjects.CountGround;
                numCountBadger.Value = _contractObjects.CountBadger;
                numCountFloor1.Value = _contractObjects.CountFloor1;
                numCountFloor2.Value = _contractObjects.CountFloor2;
                numCountFloor3.Value = _contractObjects.CountFloor3;
                numCountFloor4.Value = _contractObjects.CountFloor4;
                numCountFloor5.Value = _contractObjects.CountFloor5;
                numCountFloor6.Value = _contractObjects.CountFloor6;
                numCountFloor7.Value = _contractObjects.CountFloor7;
                numCountFloor8.Value = _contractObjects.CountFloor8;
                numCountFloor9.Value = _contractObjects.CountFloor9;
                numCountFloor10.Value = _contractObjects.CountFloor10;
                numCountTerrace.Value = _contractObjects.CountTerrace;

                #endregion

                #region View               
                if (_updataType == UpdataType.View)
                {                    
                    #region Common
                    txtNo.ReadOnly = true;
                    txtCustomer.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtDistrict.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtContact.ReadOnly = true;
                    txtTaxCode.ReadOnly = true;
                    #endregion

                    #region Contract
                    txtLicense.ReadOnly = true;
                    txtContractNumber.ReadOnly = true;
                    CboField.Enabled = true;

                    dtContractTime.Enabled = false;
                    dtStartTime.Enabled = false;
                    dtEndTime.Enabled = false;

                    txtNameSign.ReadOnly = true;
                    numVat.ReadOnly = true;
                    numValue.ReadOnly = true;

                    txtAddress2.ReadOnly = true;
                    txtNote.Text = _contractObjects.Note;
                    cheReSigned.Enabled = false;
                    txtNoteResign.ReadOnly = true;
                    #endregion

                    #region Position
                    txtGround.ReadOnly = true;
                    txtBadger.ReadOnly = true;
                    txtFloor1.ReadOnly = true;
                    txtFloor2.ReadOnly = true;
                    txtFloor3.ReadOnly = true;
                    txtFloor4.ReadOnly = true;
                    txtFloor5.ReadOnly = true;
                    txtFloor6.ReadOnly = true;
                    txtFloor7.ReadOnly = true;
                    txtFloor8.ReadOnly = true;
                    txtFloor9.ReadOnly = true;
                    txtFloor10.ReadOnly = true;
                    txtTerrace.ReadOnly = true;

                    numCountGround.ReadOnly = true;
                    numCountBadger.ReadOnly = true;
                    numCountFloor1.ReadOnly = true;
                    numCountFloor2.ReadOnly = true;
                    numCountFloor3.ReadOnly = true;
                    numCountFloor4.ReadOnly = true;
                    numCountFloor5.ReadOnly = true;
                    numCountFloor6.ReadOnly = true;
                    numCountFloor7.ReadOnly = true;
                    numCountFloor8.ReadOnly = true;
                    numCountFloor9.ReadOnly = true;
                    numCountFloor10.ReadOnly = true;
                    numCountTerrace.ReadOnly = true;
                    btnOk.Enabled = false;
                    #endregion
                }
                #endregion

                #region Edit
                else
                {
                    txtNo.ReadOnly = true;                   
                }
                #endregion
            }
            else if (_updataType == UpdataType.Add)
            {
                //this.Text = "Add Contract";
                //int no = 1;
                //var list = (from s in _ctx.MonopolyObjects                            
                //            orderby s.No descending
                //            select s
                //            ).ToList();
                //if (list.Count > 0)
                //{
                //    no = list[0].No + 1;
                //    txtNo.Text = no.ToString();
                //}
                //else
                //{
                //    txtNo.Text = "1";
                //}
                CboField.SelectedIndex = 0;
                _contractObjects = new ContractObject();
            }
        }

        private void GetDataFromUI()
        {
            if (_contractObjects == null)
            {
                MessageBox.Show("Data is null, please check again");
                return;
            }
            if (_updataType == UpdataType.Add)
            {
                //
                int no = 1;
                var list = (from s in _ctx.ContractObjects                           
                            orderby s.No descending
                            select s
                            ).ToList();
                if (list.Count > 0)
                {
                    no = list[0].No + 1;
                }
                _contractObjects.No = no;               
            }
            #region Common            
            _contractObjects.Customer = txtCustomer.Text.Trim();
            _contractObjects.Address = txtAddress.Text.Trim();
            _contractObjects.District = txtDistrict.Text.Trim();
            _contractObjects.Phone = txtPhone.Text.Trim();
            _contractObjects.Contact = txtContact.Text.Trim();
            _contractObjects.TaxCode = txtTaxCode.Text.Trim();
            #endregion

            #region Contract
            _contractObjects.License = txtLicense.Text.Trim();
            _contractObjects.ContractNumber = txtContractNumber.Text.Trim();
            _contractObjects.Field = CboField.Text.Trim();

            _contractObjects.ContractTime = dtContractTime.Value;
            _contractObjects.StartTime = dtStartTime.Value;
            _contractObjects.EndTime = dtEndTime.Value;

            _contractObjects.NameSign = txtNameSign.Text.Trim();
            _contractObjects.Vat = numVat.Value;
            _contractObjects.Value = numValue.Value;
            _contractObjects.ValueVAT = numValueVAT.Value;

            _contractObjects.Address2 = txtAddress2.Text.Trim();
            _contractObjects.Note = txtNote.Text.Trim(); 
            _contractObjects.IsReSigned = cheReSigned.Checked;
            _contractObjects.NoteReSigned = txtNoteResign.Text.Trim();
            #endregion

            #region Position
            _contractObjects.Ground = txtGround.Text.Trim();
            _contractObjects.Badger = txtBadger.Text.Trim();
            _contractObjects.Floor1 = txtFloor1.Text.Trim();
            _contractObjects.Floor2 = txtFloor2.Text.Trim();
            _contractObjects.Floor3 = txtFloor3.Text.Trim();
            _contractObjects.Floor4 = txtFloor4.Text.Trim();
            _contractObjects.Floor5 = txtFloor5.Text.Trim();
            _contractObjects.Floor6 = txtFloor6.Text.Trim();
            _contractObjects.Floor7 = txtFloor7.Text.Trim();
            _contractObjects.Floor8 = txtFloor8.Text.Trim();
            _contractObjects.Floor9 = txtFloor9.Text.Trim();
            _contractObjects.Floor10 = txtFloor10.Text.Trim();
            _contractObjects.Terrace = txtTerrace.Text.Trim();

            _contractObjects.CountGround =  (int)numCountGround.Value;
            _contractObjects.CountBadger =  (int)numCountBadger.Value;
            _contractObjects.CountFloor1 =  (int)numCountFloor1.Value;
            _contractObjects.CountFloor2 =  (int)numCountFloor2.Value;
            _contractObjects.CountFloor3 =  (int)numCountFloor3.Value;
            _contractObjects.CountFloor4 =  (int)numCountFloor4.Value;
            _contractObjects.CountFloor5 =  (int)numCountFloor5.Value;
            _contractObjects.CountFloor6 =  (int)numCountFloor6.Value;
            _contractObjects.CountFloor7 =  (int)numCountFloor8.Value;
            _contractObjects.CountFloor8 =  (int)numCountFloor8.Value;
            _contractObjects.CountFloor9 =  (int)numCountFloor9.Value;
            _contractObjects.CountFloor10 = (int)numCountFloor10.Value;
            _contractObjects.CountTerrace = (int)numCountTerrace.Value;
            #endregion
        }
        #endregion

        #region btn
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ctx == null)
                {
                    MessageBox.Show("Connect database is error!");
                    return;
                }
                GetDataFromUI();
                if (_updataType == UpdataType.Add)
                {                    
                    //add
                    _ctx.ContractObjects.Add(_contractObjects);
                    //save change
                    int check = _ctx.SaveChanges();
                    if (check < 1)
                    {
                        MessageBox.Show("Edit data is error");
                        return;
                    }
                }
                else if (_updataType == UpdataType.Edit)
                {                    
                    var item = (from s in _ctx.ContractObjects
                                where
                                   s.Id == _contractObjects.Id
                                select s
                             ).FirstOrDefault();
                    if (item == null)
                    {
                        MessageBox.Show("Not find in the table, don't update!");
                        return;
                    }
                    else
                    {
                        #region Common            
                        item.Customer = _contractObjects.Customer;
                        item.Address = _contractObjects.Address;
                        item.District = _contractObjects.District;
                        item.Phone = _contractObjects.Phone;
                        item.Contact = _contractObjects.Contact;
                        item.TaxCode = _contractObjects.TaxCode;
                        #endregion

                        #region Contract
                        item.License = _contractObjects.License;
                        item.ContractNumber = _contractObjects.ContractNumber;
                        item.Field = _contractObjects.Field;

                        item.ContractTime = _contractObjects.ContractTime;
                        item.StartTime = _contractObjects.StartTime;
                        item.EndTime = _contractObjects.EndTime;

                        item.NameSign = _contractObjects.NameSign;
                        item.Vat = _contractObjects.Vat;
                        item.Value = _contractObjects.Value;

                        item.Address2 = _contractObjects.Address2;
                        item.Note = _contractObjects.Note;
                        item.IsReSigned = _contractObjects.IsReSigned;
                        item.NoteReSigned = _contractObjects.NoteReSigned;
                        #endregion

                        #region Position
                        item.Ground = _contractObjects.Ground;
                        item.Badger = _contractObjects.Badger;
                        item.Floor1 = _contractObjects.Floor1;
                        item.Floor2 = _contractObjects.Floor2;
                        item.Floor3 = _contractObjects.Floor3;
                        item.Floor4 = _contractObjects.Floor4;
                        item.Floor5 = _contractObjects.Floor5;
                        item.Floor6 = _contractObjects.Floor6;
                        item.Floor7 = _contractObjects.Floor7;
                        item.Floor8 = _contractObjects.Floor8;
                        item.Floor9 = _contractObjects.Floor9;
                        item.Floor10 = _contractObjects.Floor10;
                        item.Terrace = _contractObjects.Terrace;

                        item.CountGround = _contractObjects.CountGround;
                        item.CountBadger = _contractObjects.CountBadger;
                        item.CountFloor1 = _contractObjects.CountFloor1;
                        item.CountFloor2 = _contractObjects.CountFloor2;
                        item.CountFloor3 = _contractObjects.CountFloor3;
                        item.CountFloor4 = _contractObjects.CountFloor4;
                        item.CountFloor5 = _contractObjects.CountFloor5;
                        item.CountFloor6 = _contractObjects.CountFloor6;
                        item.CountFloor7 = _contractObjects.CountFloor7;
                        item.CountFloor8 = _contractObjects.CountFloor8;
                        item.CountFloor9 = _contractObjects.CountFloor9;
                        item.CountFloor10 = _contractObjects.CountFloor10;
                        item.CountTerrace = _contractObjects.CountTerrace;
                        #endregion
                        //save change
                        int check = _ctx.SaveChanges();
                        if (check < 0)
                        {
                            MessageBox.Show("Edit data is error");
                            return;
                        }
                    }                    
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }

        }
        #endregion

        #region input
        private void numValue_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numValueVAT.Value = numValue.Value + numVat.Value * numValue.Value / 100;
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void numVat_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numValueVAT.Value = numValue.Value + numVat.Value * numValue.Value / 100;
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion

    }
}
