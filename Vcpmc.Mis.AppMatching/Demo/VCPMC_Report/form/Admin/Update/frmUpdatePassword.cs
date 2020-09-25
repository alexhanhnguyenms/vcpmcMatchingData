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
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.form.Admin
{
    public partial class frmUpdatePassword : Form
    {
        #region vari         
        private UserController _Controller;
        private bool isResetPass;
        private string passReset;
        private string username;
        #endregion

        #region MyRegion
        public frmUpdatePassword(UserController Controller, bool isResetPass = false, string username = "", string passReset = "")
        {
            InitializeComponent();
            this.isResetPass = isResetPass;
            this.passReset = passReset;
            this.username = username;
            _Controller = Controller;
        }

        private void frmUpdatePassword_Load(object sender, EventArgs e)
        {
            LoadFrom();
        }
        #endregion
        #region Data        
        private void LoadFrom()
        {
            if(isResetPass)
            {
                this.Text = "Password affter reset";
                btnOk.Enabled = false;
                lbPasswordOld.Visible = false;
                lbPassword.Visible = false;
                lbConfirm.Visible = false;
                txtPasswordOld.Visible = false;
                txtPassword.Visible = false;
                txtConfirmPassword.Visible = false;

                txtUsername.Text = username;                
                txtPasswordReset.Focus();
                txtPasswordReset.Text = passReset;
            }
            else
            {
                txtUsername.Text = username;
                this.Text = "Change password";
                txtPasswordReset.Visible = false;
                lbPasswordReset.Visible = false;
                txtPasswordOld.Focus();
            }
            txtUsername.ReadOnly = true;
        }
        private bool CheckData()
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "input username";
                txtUsername.Focus();
                return false;
            }
            if (txtPasswordOld.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "input old password";
                txtPasswordOld.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() == string.Empty )
            {
                lbInfo.Text = "input new password";
                txtPassword.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                lbInfo.Text = "confirm password not match";
                txtConfirmPassword.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region btn
        private void btnCancel_Click(object sender, EventArgs e)
        {           
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
                UserChangePasswordRequest request = new UserChangePasswordRequest();
                request.Password = txtPassword.Text.Trim();
                request.ConfirmPassword = txtConfirmPassword.Text.Trim();
                request.Username = txtUsername.Text.Trim();               
                request.PasswordOld = txtPasswordOld.Text.Trim();
                var data = await _Controller.ChangePassword(request);               
                if (data != null)
                {
                    if (data.Status == Utilities.Common.UpdateStatus.Successfull)
                    {
                        lbInfo.Text = data.Message; ;
                    }
                    else
                    {
                        lbInfo.Text = data.Message; 
                    }
                    return;
                }
                else
                {
                    lbInfo.Text = "No reponse";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }
        }

        #endregion
    }
}
