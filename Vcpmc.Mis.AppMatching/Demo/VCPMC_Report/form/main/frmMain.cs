using System;
using System.Net.Http;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Admin;
using Vcpmc.Mis.AppMatching.form.Admin;
using Vcpmc.Mis.AppMatching.form.config;
using Vcpmc.Mis.AppMatching.form.contract;
using Vcpmc.Mis.AppMatching.form.convert;
using Vcpmc.Mis.AppMatching.form.masterlist;
using Vcpmc.Mis.AppMatching.form.Matching.Preclaim;
using Vcpmc.Mis.AppMatching.form.Matching.Work;
using Vcpmc.Mis.AppMatching.form.mic.distribution;
using Vcpmc.Mis.AppMatching.form.mic.Distribution.BH.report;
using Vcpmc.Mis.AppMatching.form.mic.membership;
using Vcpmc.Mis.AppMatching.form.system;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Member;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly;
using Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work;
using Vcpmc.Mis.AppMatching.form.Warehouse.Youtube;
using Vcpmc.Mis.AppMatching.form.youtube;
using Vcpmc.Mis.AppMatching.Services.Admin;
using Vcpmc.Mis.Common.master;
using Vcpmc.Mis.Infrastructure;
using System.Linq;
using Vcpmc.Mis.ViewModels.System.Roles;
using System.Collections.Generic;
using Vcpmc.Mis.AppMatching.form.Search;
using Vcpmc.Mis.AppMatching.form.Distribution.Quarter;
using Vcpmc.Mis.AppMatching.form.mic.Distribution.BH;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.AppMatching.form.Tool.control;

namespace Vcpmc.Mis.AppMatching.form.main
{
    public partial class frmMain : Form
    {
        #region Variable
        bool isShowLogin = false;
        //private UserApiClient _userApiClient;
        //dang nhap    
        private UserController _userController;
        private frmLogin _frmLogin;
        #endregion

        #region init+Load 
        public frmMain()
        {
            InitializeComponent();
            Core.Innit();
            Core.Client = new HttpClient();
            //var a = Core.Client.Timeout;
            Core.Client.Timeout = Core.TimeoutHttpClient;
            Core.Client.BaseAddress = new Uri(Core.BaseAddress);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //login
                UserApiClient _userApiClient = new UserApiClient(Core.Client);            
                //user               
                RoleApiClient apiClientRole = new RoleApiClient(Core.Client);
                _userController = new UserController(_userApiClient, apiClientRole);
                //
                timer1.Enabled = true;
                timerCheckLogin.Enabled = true;
                MasterList.Innit(Core.User,Core.Path);
                if (Core.IsLogin)
                {
                    lbUser.Text = Core.User;
                }
                else
                {
                    lbUser.Text = "no login";                    
                    if (!_userController.IsLogin())
                    {
                        Login();
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        //bool isCheckLogin = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Core.Time = DateTime.Now;
                lbTimeSys.Text = Core.Time.ToString("dd/MM/yyyy HH:mm:ss");  
            }
            catch (Exception)
            {

            }
        }
        private void timerCheckLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                //20 tieng dang nhap lai
                //if( (DateTime.Now - Core.TimeLogin).TotalMinutes > 1200)
                if(isShowLogin)
                {
                    return;
                }
                //if ((DateTime.Now - Core.TimeLogin).TotalSeconds > 20)
                if( (DateTime.Now - Core.TimeLogin).TotalMinutes > 1200)
                {
                    Login();
                }
                //if (!_userController.IsLogin())
                //{
                //    Logout();
                //}
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Login
        /// <summary>
        /// Dang nhap
        /// </summary>
        private async void Login()
        {
            isShowLogin = true;
            _frmLogin = new frmLogin(_userController);
            _frmLogin.ShowDialog();
            if(_userController.IsLogin())
            {
                tmsLogin.Text = "Logout";
                lbUser.Text = Core.User;
                lbtypeTimeLogin.Text = "Time login: ";
                lbTimeLogin.Text = Core.TimeLogin.ToString("dd/MM/yyyy HH:mm:ss");               
                Core.IsLogin = true;
                tmsLogin.Text = "Logout";

                #region Lay danh sach phan quyen    
                
                if(Core.IsAdmin)
                {
                    EnableControl(true,true, Core.RoleViewModel);
                    changePasswordToolStripMenuItem.Enabled = true;
                }
                else
                {
                    var data = await _userController.GetAllPagingRole();
                    if(data!=null&& data.ResultObj!=null&& data.ResultObj.Items.Count>0)
                    {
                        RoleViewModel item = data.ResultObj.Items.Where(p => p.Name == Core.Role).FirstOrDefault();
                        if(item!=null)
                        {
                            Core.RoleViewModel = item;                           
                        }
                        else
                        {
                            Core.RoleViewModel = new RoleViewModel();
                        }
                    }
                    EnableControl(false, true, Core.RoleViewModel);
                    changePasswordToolStripMenuItem.Enabled = true;
                }
                #endregion
            }
            else
            {
                tmsLogin.Text = "Login";
                lbUser.Text = "(no login)";
                lbTimeLogin.Text = "";
                Core.IsLogin = false;
                Core.IsAdmin = false;
                Core.Role = "";
                Core.RoleViewModel = new RoleViewModel();
                EnableControl(false,false, Core.RoleViewModel);
                tmsLogin.Text = "Login";
            }
            isShowLogin = false;
        }
        /// <summary>
        /// Dang xuat
        /// </summary>
        private void Logout()
        {
            if (_userController.IsLogin())
            {
                tmsLogin.Text = "Logout";
                lbUser.Text = "Logout";
                lbtypeTimeLogin.Text = "Time logout: ";
                lbTimeLogin.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                Core.User = string.Empty;
                Core.IsLogin = false;   
                EnableControl(false,false, Core.RoleViewModel);
                tmsLogin.Text = "Login";
                changePasswordToolStripMenuItem.Enabled = false;
            }                 
        }
        private void EnableControl(bool isAdmin, bool isEnnable, RoleViewModel role)
        {
            List<MenuList> menu = new List<MenuList>();
            MenuList sub;
            //1.cap 1
            foreach (ToolStripMenuItem menuItem1 in mainMenu.Items)
            {
                if(menuItem1.DropDownItems.Count == 0)
                {
                    if(menuItem1.Tag !=null&& !menuItem1.Tag.ToString().Trim().ToUpper().Contains("PUBLIC"))
                    {
                        sub = new MenuList
                        {
                            Item = menuItem1,
                            Level = 1,                          
                        };
                        menu.Add(sub);
                    }
                }
                else
                {
                    if (((ToolStripMenuItem)menuItem1).Tag == null)
                    {
                        sub = new MenuList
                        {
                            Item = ((ToolStripMenuItem)menuItem1),
                            Level = 1,                          
                        };
                        menu.Add(sub);
                    }
                    else if (((ToolStripMenuItem)menuItem1).Tag != null && !((ToolStripMenuItem)menuItem1).Tag.ToString().Trim().ToUpper().Contains("PUBLIC"))
                    {
                        sub = new MenuList
                        {
                            Item = ((ToolStripMenuItem)menuItem1),
                            Level = 1,                          
                        };
                        menu.Add(sub);
                    }
                    //2.cap 2
                    foreach (var menuItem2 in menuItem1.DropDownItems)
                    {
                        if (menuItem2 is ToolStripMenuItem)
                        {
                            #region Cap 2
                            if (((ToolStripMenuItem)menuItem2).DropDownItems.Count == 0)
                            {
                                if (((ToolStripMenuItem)menuItem2).Tag != null && !((ToolStripMenuItem)menuItem2).Tag.ToString().Trim().ToUpper().Contains("PUBLIC"))
                                {
                                    sub = new MenuList
                                    {
                                        Item = ((ToolStripMenuItem)menuItem2),
                                        Level = 2
                                    };
                                    menu.Add(sub);                                    
                                }
                            }
                            else
                            {
                                if(((ToolStripMenuItem)menuItem2).Tag == null)
                                {
                                    sub = new MenuList
                                    {
                                        Item = ((ToolStripMenuItem)menuItem2),
                                        Level = 2
                                    };
                                    menu.Add(sub);
                                }
                                else if(((ToolStripMenuItem)menuItem2).Tag != null&&!((ToolStripMenuItem)menuItem2).Tag.ToString().Trim().ToUpper().Contains("PUBLIC"))
                                {
                                    sub = new MenuList
                                    {
                                        Item = ((ToolStripMenuItem)menuItem2),
                                        Level = 2
                                    };
                                    menu.Add(sub);
                                }                                
                                //3.cap 3
                                foreach (var menuItem3 in ((ToolStripMenuItem)menuItem2).DropDownItems)
                                {
                                    if (menuItem3 is ToolStripMenuItem)
                                    {
                                        if (((ToolStripMenuItem)menuItem3).DropDownItems.Count == 0)
                                        {
                                            if (((ToolStripMenuItem)menuItem3).Tag != null && !((ToolStripMenuItem)menuItem3).Tag.ToString().Trim().ToUpper().Contains("PUBLIC"))
                                            {
                                                sub = new MenuList
                                                {
                                                    Item = ((ToolStripMenuItem)menuItem3),
                                                    Level = 3
                                                };
                                                menu.Add(sub);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("get item in menu loss");
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            //TODO
            if (isAdmin)
            {
                if(!isEnnable)
                {
                    //dong het
                    foreach (var item in menu)
                    {
                        item.Item.Enabled = false;
                    }
                }
                else
                {
                    foreach (var item in menu)
                    {
                        item.Item.Enabled = true;
                    }
                }
            }
            else
            {
                if (!isEnnable)
                {
                    //dong het
                    foreach (var item in menu)
                    {
                        item.Item.Enabled = false;
                    }
                }
                else
                {
                    foreach(var item in menu)
                    {
                        item.Item.Enabled = false;
                    }
                    List<string> tagLevelParent = new List<string>();
                    //sub
                    if(role!=null&&role.Rights!=null&&role.Rights.Count>0)
                    {
                        foreach (var right in role.Rights)
                        {
                            //string text = $"{right.Group}.{right.Claim}".ToUpper();
                            string text = $"{right.Claim}".ToUpper();                            
                            //enable
                            foreach (var item in menu)
                            {
                                if (item.Item.Tag != null && item.Item.Tag.ToString().Trim().ToUpper() == text)
                                {
                                    item.Item.Enabled = true;
                                    #region lay item parent
                                    string[] arrayTag = item.Item.Tag.ToString().Trim().ToUpper().Split('.');
                                    if(arrayTag.Length>1)
                                    {
                                        string tem = "";
                                        switch (arrayTag.Length)
                                        {
                                            #region switch
                                            case 0://bo
                                                break;
                                            case 1://bo
                                                break;
                                            case 2:
                                                tem = arrayTag[0];
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                break;
                                            case 3:
                                                tem = arrayTag[0];
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                tem = $"{arrayTag[0]}.{arrayTag[1]}";
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                break;
                                            case 4:
                                                tem = arrayTag[0];
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                tem = $"{arrayTag[0]}.{arrayTag[1]}";
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                tem = $"{arrayTag[0]}.{arrayTag[1]}.{arrayTag[2]}";
                                                if (tem.Length > 0)
                                                {
                                                    if (!tagLevelParent.Contains(tem))
                                                    {
                                                        tagLevelParent.Add(tem);
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                                #endregion
                                        }                                                                              
                                    }
                                    #endregion
                                }
                            }
                        }
                    }

                    #region Open parent
                    foreach (var temp in tagLevelParent)
                    {
                        string text = temp;
                        //enable
                        foreach (var item in menu)
                        {
                            if (item.Item.Tag != null && item.Item.Tag.ToString().Trim().ToUpper() == text)
                            {
                                item.Item.Enabled = true;
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 1.user
        private void tsbLogInOut_Click(object sender, EventArgs e)
        {
            if (tmsLogin.Text == "Logout")
            {
                Logout();
            }
            else
            {
                Login();
            }
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUpdatePassword frm = new frmUpdatePassword(_userController, false, Core.User);
                frm.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        
        #region 2.search
        private void infoWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmInfoWork frm = new frmInfoWork();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 3.matching
        private void preclaimToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmPreclaimMatching frm = new frmPreclaimMatching();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {
               
            }
        }

        private void workToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmWorkMatching frm = new frmWorkMatching();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 4.1.info
        private void preclaimToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                frmPreclaim frm = new frmPreclaim(UpdataType.View);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void workToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                frmWork frm = new frmWork(UpdataType.View);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void misToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmMemberList frm = new frmMemberList(UpdataType.View);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void monopolyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMonopoly frm = new frmMonopoly(UpdataType.View);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 4.WareHouse
        private void preclaimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPreclaim frm = new frmPreclaim(UpdataType.Add);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void trackingWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmTrackingWork frm = new frmTrackingWork();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void workToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmWork frm = new frmWork(UpdataType.Add);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
       
        private void monopolyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmMonopoly frm = new frmMonopoly(UpdataType.Add);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 5.Distribution
        private void quaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDistributionQuarter frm = new frmDistributionQuarter();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 6.Youtube
        private void tsmGetVideoList_Click(object sender, EventArgs e)
        {
            try
            {
                frmGetDataFromYoutube frm = new frmGetDataFromYoutube();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void tsmMakeMasterList_Click(object sender, EventArgs e)
        {
            try
            {
                frmMasterList frm = new frmMasterList();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 7.Contract
        private void contractListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmContractList frm = new frmContractList();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void tssBtnMemberBh_Click(object sender, EventArgs e)
        {
            try
            {
                frmMember frm = new frmMember();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void tssBtnImportWorkMember_Click(object sender, EventArgs e)
        {
            try
            {
                frmImportWorkMember frm = new frmImportWorkMember();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void tssBtnExceptionWork_Click(object sender, EventArgs e)
        {
            try
            {
                frmExceptionWork frm = new frmExceptionWork();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void tsmMakeDistribution_Click(object sender, EventArgs e)
        {
            try
            {
                frmDistribution2 frm = new frmDistribution2();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void tsmDistributionReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmBhDistributionReport frm = new frmBhDistributionReport();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 8.tool
        private void membershipRetrievalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMemberShipRetrieval frm = new frmMemberShipRetrieval();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void ediFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmEdiFiles frm = new frmEdiFiles();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void convertTOUnsignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmConvertToUnsign frm = new frmConvertToUnsign();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 9.Admin
        private void roleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRole frm = new frmRole();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUser frm = new frmUser();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        private void tsmConfig_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion        

        #region 10.About
        private void tmsAbout_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }


        #endregion

        #region 11.parameter
        private void fixParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFixParameter frm = new frmFixParameter(UpdataType.All);
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        
    }
}
