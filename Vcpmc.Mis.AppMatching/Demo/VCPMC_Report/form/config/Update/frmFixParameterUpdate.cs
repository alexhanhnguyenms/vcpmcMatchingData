using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.System;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Shared.Parameter;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.AppMatching.form.config.Update
{
    public partial class frmFixParameterUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private FixParameterViewModel currenObject;
        private FixParameterUpdateRequest currenObjectUpdate = new FixParameterUpdateRequest();
        private FixParameterCreateRequest currenObjectCreate = new FixParameterCreateRequest();
        private FixParameterController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        TypeFixParameter _typeFixParameter;
        int _group = 0;
        #endregion
        public frmFixParameterUpdate(FixParameterController Controller, UpdataType view, FixParameterViewModel currenObject, TypeFixParameter typeFixParameter)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObject;
            this._typeFixParameter = typeFixParameter;
            //this._group = Group;
        }

        private void frmFixParameterUpdate_Load(object sender, EventArgs e)
        {
            //TypeFixParameter typefix = new TypeFixParameter();
            List<string> MyNames = ((TypeFixParameter[])Enum.GetValues(typeof(TypeFixParameter))).Select(c => c.ToString()).ToList();
            cboType.DataSource = MyNames;            
            LoadFrom();
        }
        #region Data        
        private void LoadFrom()
        { 
            if (_updataType == UpdataType.Edit || _updataType == UpdataType.View)
            {
                if (_updataType == UpdataType.Edit)
                {
                    this.Text = "Update Fix Parameter";
                }
                else
                {
                    this.Text = "View Fix Parameter";
                }
                if (_group == 0)
                {
                    this.Text += "-WORK";
                }
                else
                {
                    this.Text += "-MEMBER";
                }
                if (currenObject == null)
                {
                    return;
                }
                cboType.Text = currenObject.Type;
                txtKey.Text = currenObject.Key;
                txtValue1.Text = currenObject.Value1;
                txtValue2.Text = currenObject.Value2;
                txtValue3.Text = currenObject.Value3;
                txtValue4.Text = currenObject.Value4;
                txtValue5.Text = currenObject.Value5;
                txtValue6.Text = currenObject.Value6;
                txtValue7.Text = currenObject.Value7;
                txtValue8.Text = currenObject.Value8;
                txtValue9.Text = currenObject.Value9;
                txtValue10.Text = currenObject.Value10;
               

                if (_updataType == UpdataType.Edit)
                {
                    cboType.Enabled = false;
                    txtKey.ReadOnly = true;
                }
                else
                {
                    //txtNo.ReadOnly = true;                    
                    cboType.Enabled = false;
                    txtKey.ReadOnly = true;
                    txtValue1.ReadOnly = true;
                    txtValue2.ReadOnly = true;
                    txtValue3.ReadOnly = true;

                    txtValue4.ReadOnly = true;
                    txtValue5.ReadOnly = true;
                    txtValue6.ReadOnly = true;
                    txtValue7.ReadOnly = true;
                    txtValue8.ReadOnly = true;
                    txtValue9.ReadOnly = true;
                    txtValue10.ReadOnly = true;                    
                    btnOk.Enabled = false;
                }
            }
            else if (_updataType == UpdataType.Add)
            {
                this.Text = "Add Fix Parameter";
                cboType.Text = _typeFixParameter.ToString();
                currenObject = new FixParameterViewModel();
            }
        }
        private void GetDataFromUI()
        {
            if (currenObject == null)
            {
                MessageBox.Show("Data is null, please check again");
                return;
            }
            if (_updataType == UpdataType.Add)
            {
                currenObjectCreate.Type = cboType.Text.Trim();
                currenObjectCreate.Key = txtKey.Text.Trim().ToUpper();
                currenObjectCreate.Value1 = txtValue1.Text.Trim().ToUpper();
                currenObjectCreate.Value2 = txtValue2.Text.Trim().ToUpper();
                currenObjectCreate.Value3 = txtValue3.Text.Trim().ToUpper();
                currenObjectCreate.Value4 = txtValue4.Text.Trim().ToUpper();
                currenObjectCreate.Value5 = txtValue5.Text.Trim().ToUpper();
                currenObjectCreate.Value6 = txtValue6.Text.Trim().ToUpper();
                currenObjectCreate.Value7 = txtValue7.Text.Trim().ToUpper();
                currenObjectCreate.Value8 = txtValue8.Text.Trim().ToUpper();
                currenObjectCreate.Value9 = txtValue9.Text.Trim().ToUpper();
                currenObjectCreate.Value10 = txtValue10.Text.Trim().ToUpper();               
               
            }
            else
            {
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.Type = cboType.Text.Trim();
                currenObjectUpdate.Key = txtKey.Text.Trim().ToUpper();
                currenObjectUpdate.Value1 = txtValue1.Text.Trim().ToUpper();
                currenObjectUpdate.Value2 = txtValue2.Text.Trim().ToUpper();
                currenObjectUpdate.Value3 = txtValue3.Text.Trim().ToUpper();
                currenObjectUpdate.Value4 = txtValue4.Text.Trim().ToUpper();
                currenObjectUpdate.Value5 = txtValue5.Text.Trim().ToUpper();
                currenObjectUpdate.Value6 = txtValue6.Text.Trim().ToUpper();
                currenObjectUpdate.Value7 = txtValue7.Text.Trim().ToUpper();
                currenObjectUpdate.Value8 = txtValue8.Text.Trim().ToUpper();
                currenObjectUpdate.Value9 = txtValue9.Text.Trim().ToUpper();
                currenObjectUpdate.Value10 = txtValue10.Text.Trim().ToUpper();

            }

        }
        #endregion

        #region Data        
        private bool CheckData()
        {
           
            if (cboType.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "Please input type!";
                return false;
            }
            if (txtKey.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "Please input key!";
                return false;
            }
            lbInfo.Text = "";
            return true;
        }
        #endregion

        #region btn
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ObjectReturn == null)
            {
                ObjectReturn = new UpdateStatusViewModel
                {
                    Status = Utilities.Common.UpdateStatus.Failure,
                    Message = "No reponse"
                };
            }
            this.Close();
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                {
                    //lbInfo.Text = "Please check again data!";
                    return;
                }
                ObjectReturn = null;
                GetDataFromUI();
                if (_updataType == UpdataType.Add)
                {
                    #region Add
                    var data = await _Controller.Create(currenObjectCreate);
                    ObjectReturn = data;
                    if (data != null)
                    {
                        if (data.Status == Utilities.Common.UpdateStatus.Successfull)
                        {
                            this.Close();
                        }
                        else
                        {
                            lbInfo.Text = data.Message; ;
                        }
                        return;
                    }
                    else
                    {
                        lbInfo.Text = "No reponse";
                    }
                    #endregion
                }
                else if (_updataType == UpdataType.Edit)
                {
                    #region edit
                    var data = await _Controller.Update(currenObjectUpdate);
                    ObjectReturn = data;
                    if (data != null)
                    {
                        if (data.Status == Utilities.Common.UpdateStatus.Successfull)
                        {
                            this.Close();
                        }
                        else
                        {
                            lbInfo.Text = data.Message; ;
                        }
                        return;
                    }
                    else
                    {
                        lbInfo.Text = "No reponse";
                    }
                    #endregion
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }

        }

        #endregion
    }
}
