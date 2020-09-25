using System;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Admin;
using Vcpmc.Mis.AppMatching.form.Admin;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.form.system
{
    public partial class frmLogin : Form
    {        
        private UserController _userControll;
        public frmLogin(UserController userControll)
        {
            InitializeComponent();        
            _userControll = userControll;
            lbNote.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Core.IsLogin = false;
            Core.User = "";
            Core.Password = "";
            this.Close();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lbNote.Text = "";
                LoginRequest request = new LoginRequest();
                request.UserName = txtUser.Text.Trim();
                request.Password = txtPassword.Text.Trim();
                request.RememberMe = true;               
                var check = await _userControll.SignInAsync(request);
                if (check ==string.Empty)
                {
                    #region Load
                    var user = await _userControll.GetUserByUsername(request.UserName);
                    if(user!=null)
                    {
                        Core.Role = user.Role;
                        Core.IsAdmin = user.IsAdmin;
                    }
                    else
                    {
                        Core.Role = "";
                        Core.IsAdmin = false;
                    }    
                    #endregion                   
                    this.Close();
                }
                else
                {
                    Core.Role = "";
                    Core.IsAdmin = false;
                    if (check == null)
                    {
                        check = "tài khoản chưa đúng";
                    }
                    lbNote.Text = $"Please login again!-{check}";
                } 
            }
            catch (Exception)
            {
                lbNote.Text = $"Connect server is error!";
            }            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

//#if DEBUG
//            //txtUser.Text = "qazxsw";
//            //txtPassword.Text = "P@ssw0rd
//            txtUser.Text = "admin";
//            txtPassword.Text = "Admin@123";
//#endif
        }
        /// <summary>
        /// Thay doi mat khau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmUpdatePassword frm = new frmUpdatePassword(_userControll, false);
                frm.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }            
        }
    }
}
