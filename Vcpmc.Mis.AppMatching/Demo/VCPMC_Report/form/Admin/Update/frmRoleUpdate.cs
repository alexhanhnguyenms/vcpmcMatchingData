using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Admin;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.AppMatching.form.Admin.Update
{
    public partial class frmRoleUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private RoleViewModel currenObject;
        private RoleUpdateRequest currenObjectUpdate = new RoleUpdateRequest();
        private RoleCreateRequest currenObjectCreate = new RoleCreateRequest();
        private RoleController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        #endregion

        #region init
        public frmRoleUpdate(RoleController Controller, UpdataType view, RoleViewModel currenObject)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObject;
        }

        private void frmRoleUpdate_Load(object sender, EventArgs e)
        {            
            LoadFrom();
        }
        #endregion
        #region Data        
        private void LoadFrom()
        {
            if (_updataType == UpdataType.Edit || _updataType == UpdataType.View)
            {
                if (_updataType == UpdataType.Edit)
                {
                    this.Text = "Update Role";
                }
                else
                {
                    this.Text = "View Role";
                }
                if (currenObject == null)
                {
                    return;
                }
                #region Common
                txtName.Text = currenObject.Name;
                txtDescription.Text = currenObject.Description;
               
                #endregion              

                #region enable                    
                if (_updataType == UpdataType.View)
                {
                    #region Common
                    txtName.ReadOnly = true;
                    txtDescription.ReadOnly = true;                   

                    btnOk.Enabled = false;
                    #endregion
                }
                else
                {
                    txtName.ReadOnly = true;
                }
                #endregion

            }
            else if (_updataType == UpdataType.Add)
            {
                this.Text = "Add Role";
                currenObject = new RoleViewModel();
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
                currenObjectCreate.Name = txtName.Text.Trim();
                currenObjectCreate.Description = txtDescription.Text.Trim();                
            }
            else
            {
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.Name = txtName.Text.Trim();
                currenObjectUpdate.Description = txtDescription.Text.Trim();               
            }

        }
        private bool CheckData()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                return false;
            }            
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
                    lbInfo.Text = "Please check again data!";
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
