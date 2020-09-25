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
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.form.Admin.Update
{
    public partial class frmUserUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private UserViewModel currenObject;
        private UserUpdateRequest currenObjectUpdate = new UserUpdateRequest();
        private UserCreateRequest currenObjectCreate = new UserCreateRequest();
        private UserController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        public List<RoleViewModel> _RoleList = null;
        #endregion

        #region init
        public frmUserUpdate(UserController Controller, UpdataType view, UserViewModel currenObject, List<RoleViewModel> RoleList)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObject;
            _RoleList = RoleList;
            #region load role
            cboRole.DataSource = _RoleList;
            cboRole.ValueMember = "Id";
            cboRole.DisplayMember = "Name";
            if (_RoleList.Count == 0)
            {
                lbInfo.Text = "Role is empty, please create role before create user!";
                btnOk.Enabled = false;
            }
            else
            {
                cboRole.SelectedIndex = 0;
            }
            #endregion
        }

        private void frmUserUpdate_Load(object sender, EventArgs e)
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
                    this.Text = "Update User";
                }
                else
                {
                    this.Text = "View User";
                }
                if (currenObject == null)
                {
                    return;
                }
                #region Common
                txtUserName.Text = currenObject.UserName;
                txtEmail.Text = currenObject.Email;
                //txtPassword.Text = currenObject.PasswordHash;
                //txtConfirmPassword.Text = currenObject.PasswordHash;
                lbPassword.Visible = false;
                lbConfirm.Visible = false;
                txtPassword.Visible = false;
                txtConfirmPassword.Visible = false;

                txtPhoneNumber.Text = currenObject.PhoneNumber;
                txtFirstName.Text = currenObject.FirstName;
                txtLastName.Text = currenObject.LastName;
                if(currenObject.Role!=string.Empty)
                {
                    cboRole.Text = currenObject.Role;
                }
                #endregion

                #region enable                    
                if (_updataType == UpdataType.View)
                {
                    #region Common
                    txtUserName.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtConfirmPassword.ReadOnly = true;
                    txtPhoneNumber.ReadOnly = true;
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    cboRole.Enabled = false;

                    btnOk.Enabled = false;
                    #endregion
                }
                else
                {
                    txtUserName.ReadOnly = true;
                }
                #endregion

            }
            else if (_updataType == UpdataType.Add)
            {
                this.Text = "Add User";
                currenObject = new UserViewModel();
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
                currenObjectCreate.UserName = txtUserName.Text.Trim();
                currenObjectCreate.Email = txtEmail.Text.Trim();
                currenObjectCreate.Password = txtPassword.Text.Trim();
                currenObjectCreate.ConfirmPassword = txtConfirmPassword.Text.Trim();
                currenObjectCreate.PhoneNumber = txtPhoneNumber.Text.Trim();
                currenObjectCreate.FirstName = txtFirstName.Text.Trim();
                currenObjectCreate.LastName = txtLastName.Text.Trim();
                currenObjectCreate.Role = cboRole.Text.Trim();
            }
            else
            {
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.UserName = txtUserName.Text.Trim();
                currenObjectUpdate.Email = txtEmail.Text.Trim();
                currenObjectUpdate.Password = "";// txtPassword.Text;
                currenObjectUpdate.ConfirmPassword = "";// txtConfirmPassword.Text;
                currenObjectUpdate.PhoneNumber = txtPhoneNumber.Text.Trim();
                currenObjectUpdate.FirstName = txtFirstName.Text.Trim();
                currenObjectUpdate.LastName = txtLastName.Text.Trim();
                currenObjectUpdate.Role = cboRole.Text.Trim();
            }

        }
        private bool CheckData()
        {
            if (txtUserName.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "input user";
                txtUserName.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == string.Empty||!txtEmail.Text.Trim().Contains("@"))
            {
                lbInfo.Text = "input again email";
                txtEmail.Focus();
                return false;
            }
            if(_updataType == UpdataType.Add)
            {
                if (txtPassword.Text.Trim() == string.Empty)
                {
                    lbInfo.Text = "input password";
                    return false;
                }
                else
                {
                    if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                    {
                        lbInfo.Text = "confirm password not match";
                        txtConfirmPassword.Focus();
                        return false;
                    }
                }
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
