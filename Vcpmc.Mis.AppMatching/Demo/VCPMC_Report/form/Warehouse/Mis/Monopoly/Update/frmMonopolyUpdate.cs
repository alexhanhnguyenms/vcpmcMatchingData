using System;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly.Update
{
    public partial class frmMonopolyUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private MonopolyViewModel currenObject;
        private MonopolyUpdateRequest currenObjectUpdate = new MonopolyUpdateRequest();
        private MonopolyCreateRequest currenObjectCreate = new MonopolyCreateRequest();
        private MonopolyController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        int _group = 0;
        #endregion

        #region Load form        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Group">nhom doc quyen, 0 tac pham, 1 tac gia</param>
        /// <param name="updataType"></param>
        /// <param name="monopolyObject">Data</param>
        public frmMonopolyUpdate(MonopolyController Controller, UpdataType view, MonopolyViewModel currenObject,int Group)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObject;
            this._group = Group;            
        }

        private void frmUpdateMonopoly_Load(object sender, EventArgs e)
        {
            LoadFrom();
        }
        #endregion

        #region Data        
        private void LoadFrom()
        {
            dtStartTime.Value = new DateTime(2020, 01, 01);
            dtEndTime.Value = dtEndTime.MaxDate;
            if (_group == 0)
            {
                lbName.Visible = true;
                txtName.Visible = true;
                lbNameType.Visible = false;
                txtNameType.Visible = false;
                //lbName.Text = "Tên tác phẩm độc quyền";
            }
            else
            {
                lbName.Visible = false;
                txtName.Visible = false;
                lbNameType.Visible = true;
                txtNameType.Visible = true;
                //lbName.Text = "IP name";
            }
            if(_updataType == UpdataType.Edit || _updataType ==UpdataType.View)
            {
                if(_updataType == UpdataType.Edit)
                {
                    this.Text = "Update Monopoly";
                }
                else
                {
                    this.Text = "View Monopoly";
                }
                if(_group == 0)
                {
                    this.Text += "-WORK";
                }
                else
                {
                    this.Text += "-MEMBER";
                }
                if(currenObject == null)
                {
                    return;
                }                          
                txtCodeOld.Text = currenObject.CodeOld;
                txtCodeNew.Text = currenObject.CodeNew;
                txtName.Text = currenObject.Name;
                txtOwn.Text = currenObject.Own;
                txtNameType.Text = currenObject.NameType;

                txtNoteMono.Text = currenObject.NoteMono;
                txtNote2.Text = currenObject.Note2;
                cheIsExpired.Checked = currenObject.IsExpired;
                txtNote3.Text = currenObject.Note3;

                dtStartTime.Value = currenObject.StartTime > dtStartTime.MaxDate ? dtStartTime.MaxDate : currenObject.StartTime;
                dtEndTime.Value = currenObject.EndTime>dtEndTime.MaxDate? dtEndTime.MaxDate:currenObject.EndTime;
                dtUpdateTime.Value = currenObject.UpdateTime > dtUpdateTime.MaxDate ? dtUpdateTime.MaxDate : currenObject.UpdateTime;
                dtReceiveTime.Value = currenObject.ReceiveTime > dtReceiveTime.MaxDate ? dtReceiveTime.MaxDate : currenObject.ReceiveTime;

                cheTone.Checked = currenObject.Tone;
                cheWeb.Checked = currenObject.Web;
                chePerformances.Checked = currenObject.Performances;
                chePerformancesHCM.Checked = currenObject.PerformancesHCM;
                cheCddvd.Checked = currenObject.Cddvd;
                cheKok.Checked = currenObject.Kok;
                cheBroadCasting.Checked = currenObject.Broadcasting;

                cheEntertaiment.Checked = currenObject.Entertaiment;

                cheFilm.Checked = currenObject.Film;
                cheAdvertisement.Checked = currenObject.Advertisement;
                chePubMusicBook.Checked = currenObject.PubMusicBook;
                cheYoutube.Checked = currenObject.Youtube;
                cheOther.Checked = currenObject.Other;

                if(_updataType == UpdataType.Edit)
                {
                    //txtNo.ReadOnly = true;
                }
                else
                {
                    //txtNo.ReadOnly = true;                    
                    txtCodeOld.ReadOnly = true;
                    txtCodeNew.ReadOnly = true;
                    txtName.ReadOnly = true;
                    txtOwn.ReadOnly = true;
                    txtNameType.ReadOnly = true;

                    txtNoteMono.ReadOnly = true;
                    txtNote2.ReadOnly = true;
                    cheIsExpired.Enabled = false;
                    txtNote3.ReadOnly = true;

                    dtStartTime.Enabled = false;
                    dtEndTime.Enabled = false;
                    dtUpdateTime.Enabled = false;
                    dtReceiveTime.Enabled = false;

                    cheTone.Enabled = false;
                    cheWeb.Enabled = false;
                    chePerformances.Enabled = false;
                    chePerformancesHCM.Enabled = false;
                    cheCddvd.Enabled = false;
                    cheKok.Enabled = false;
                    cheBroadCasting.Enabled = false;

                    cheEntertaiment.Enabled = false;

                    cheFilm.Enabled = false;
                    cheAdvertisement.Enabled = false;
                    chePubMusicBook.Enabled = false;
                    cheYoutube.Enabled = false;
                    cheOther.Enabled = false;
                    btnOk.Enabled = false;
                }
            }
            else if(_updataType == UpdataType.Add)
            {
                this.Text = "Add monopoly";
                if (_group == 0)
                {
                    this.Text += "-WORK";
                }
                else
                {
                    this.Text += "-MEMBER";
                }
                currenObject = new MonopolyViewModel();
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
                currenObjectCreate.Group = _group;
                currenObjectCreate.CodeOld = VnHelper.ConvertToUnSign(txtCodeOld.Text.Trim().ToUpper());
                currenObjectCreate.CodeNew = VnHelper.ConvertToUnSign(txtCodeNew.Text.Trim().ToUpper());
                currenObjectCreate.Name = VnHelper.ConvertToUnSign(txtName.Text.Trim().ToUpper());
                currenObjectCreate.Own = VnHelper.ConvertToUnSign(txtOwn.Text.Trim().ToUpper());
                currenObjectCreate.NameType = VnHelper.ConvertToUnSign(txtNameType.Text.Trim().ToUpper());

                currenObjectCreate.NoteMono = txtNoteMono.Text.Trim().ToUpper();
                currenObjectCreate.Note2 = txtNote2.Text.Trim().ToUpper();
                currenObjectCreate.Note3 = txtNote3.Text.Trim().ToUpper();
                currenObjectCreate.IsExpired = cheIsExpired.Checked;

                currenObjectCreate.StartTime = dtStartTime.Value;
                currenObjectCreate.EndTime = dtEndTime.Value;
                currenObjectCreate.UpdateTime = dtUpdateTime.Value;
                currenObjectCreate.ReceiveTime = dtReceiveTime.Value;

                currenObjectCreate.Tone = cheTone.Checked;
                currenObjectCreate.Web = cheWeb.Checked;
                currenObjectCreate.Performances = chePerformances.Checked;
                currenObjectCreate.PerformancesHCM = chePerformancesHCM.Checked;
                currenObjectCreate.Cddvd = cheCddvd.Checked;
                currenObjectCreate.Kok = cheKok.Checked;
                currenObjectCreate.Broadcasting = cheBroadCasting.Checked;

                currenObjectCreate.Entertaiment = cheEntertaiment.Checked;

                currenObjectCreate.Film = cheFilm.Checked;
                currenObjectCreate.Advertisement = cheAdvertisement.Checked;
                currenObjectCreate.PubMusicBook = chePubMusicBook.Checked;
                currenObjectCreate.Youtube = cheYoutube.Checked;
                currenObjectCreate.Other = cheOther.Checked;
            }
            else
            {
                currenObjectUpdate.Group = _group;
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.CodeOld = VnHelper.ConvertToUnSign(txtCodeOld.Text.Trim().ToUpper());
                currenObjectUpdate.CodeNew = VnHelper.ConvertToUnSign(txtCodeNew.Text.Trim().ToUpper());
                currenObjectUpdate.Name = VnHelper.ConvertToUnSign(txtName.Text.Trim().ToUpper());
                currenObjectUpdate.Own = VnHelper.ConvertToUnSign(txtOwn.Text.Trim().ToUpper());
                currenObjectUpdate.NameType = VnHelper.ConvertToUnSign(txtNameType.Text.Trim().ToUpper());

                currenObjectUpdate.NoteMono = txtNoteMono.Text.Trim().ToUpper();
                currenObjectUpdate.Note2 = txtNote2.Text.Trim().ToUpper();
                currenObjectUpdate.Note3 = txtNote3.Text.Trim().ToUpper();
                currenObjectUpdate.IsExpired = cheIsExpired.Checked;

                currenObjectUpdate.StartTime = dtStartTime.Value;
                currenObjectUpdate.EndTime = dtEndTime.Value;
                currenObjectUpdate.UpdateTime = dtUpdateTime.Value;
                currenObjectUpdate.ReceiveTime = dtReceiveTime.Value;

                currenObjectUpdate.Tone = cheTone.Checked;
                currenObjectUpdate.Web = cheWeb.Checked;
                currenObjectUpdate.Performances = chePerformances.Checked;
                currenObjectUpdate.PerformancesHCM = chePerformancesHCM.Checked;
                currenObjectUpdate.Cddvd = cheCddvd.Checked;
                currenObjectUpdate.Kok = cheKok.Checked;
                currenObjectUpdate.Broadcasting = cheBroadCasting.Checked;

                currenObjectUpdate.Entertaiment = cheEntertaiment.Checked;

                currenObjectUpdate.Film = cheFilm.Checked;
                currenObjectUpdate.Advertisement = cheAdvertisement.Checked;
                currenObjectUpdate.PubMusicBook = chePubMusicBook.Checked;
                currenObjectUpdate.Youtube = cheYoutube.Checked;
                currenObjectUpdate.Other = cheOther.Checked;
            }

        }
        #endregion

        #region Data        
        private bool CheckData()
        {
            if (txtCodeNew.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtOwn.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtNoteMono.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (_group==0)
            {
                if (txtName.Text.Trim() == string.Empty)
                {
                    return false;
                }
            }
            else
            {

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
