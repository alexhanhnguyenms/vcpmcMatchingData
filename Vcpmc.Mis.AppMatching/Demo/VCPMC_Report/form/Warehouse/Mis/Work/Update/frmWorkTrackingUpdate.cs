using System;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmWorkTrackingUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private WorkTrackingViewModel currenObject;
        private WorkTrackingUpdateRequest currenObjectUpdate = new WorkTrackingUpdateRequest();
        private WorkTrackingCreateRequest currenObjectCreate = new WorkTrackingCreateRequest();
        private WorkTrackingController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        #endregion

        #region init
        public frmWorkTrackingUpdate(WorkTrackingController Controller, UpdataType view, WorkTrackingViewModel currenObjec)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObjec;
        }

        private void frmWorkTrackingUpdate_Load(object sender, EventArgs e)
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
                    this.Text = "Update Tracking Work";
                }
                else
                {
                    this.Text = "View Tracking Work";
                }
                if (currenObject == null)
                {
                    return;
                }
                #region Common
                txtWK_INT_NO.Text = currenObject.WK_INT_NO.ToString();
                txtTTL_ENG.Text = currenObject.TTL_ENG;
                txtISWC_NO.Text = currenObject.ISWC_NO;
                txtISRC.Text = currenObject.ISRC;
                txtWRITER.Text = currenObject.WRITER;
                txtARTIST.Text = currenObject.ARTIST;               
                txtSOC_NAME.Text = currenObject.SOC_NAME;
                if (currenObject.TimeCreate != null)
                {
                    dtTimeCreate.Value = (DateTime)currenObject.TimeCreate;
                }
                else
                {
                    dtTimeCreate.Visible = false;
                    lbTimeCreate.Visible = false;
                }
                if (currenObject.TimeUpdate != null)
                {
                    dtTimeUpdate.Value = (DateTime)currenObject.TimeUpdate;
                }
                else
                {
                    dtTimeUpdate.Visible = false;
                    lbTimeUpdate.Visible = false;
                }
                if(currenObject.Type == 0)
                {
                    cboType.SelectedIndex = 0;
                }
                else
                {
                    cboType.SelectedIndex = 1;
                }                
                #endregion              

                #region enable                    
                if (_updataType == UpdataType.View)
                {
                    #region Common
                    txtWK_INT_NO.ReadOnly = true;
                    txtTTL_ENG.ReadOnly = true;
                    txtISWC_NO.ReadOnly = true;
                    txtISRC.ReadOnly = true;
                    txtWRITER.ReadOnly = true;
                    txtARTIST.ReadOnly = true;
                    txtSOC_NAME.ReadOnly = true;

                    btnOk.Enabled = false;
                    dtTimeCreate.Enabled = false;
                    dtTimeUpdate.Enabled = false;
                    cboType.Enabled = false;
                    #endregion
                }
                else
                {
                    txtWK_INT_NO.ReadOnly = true;
                }
                #endregion

            }
            else if (_updataType == UpdataType.Add)
            {
                this.Text = "Add Tracking Work";
                currenObject = new WorkTrackingViewModel();
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
                currenObjectCreate.WK_INT_NO = VnHelper.ConvertToUnSign(txtWK_INT_NO.Text.Trim().ToUpper());
                currenObjectCreate.TTL_ENG = VnHelper.ConvertToUnSign(txtTTL_ENG.Text.Trim().ToUpper());
                currenObjectCreate.ISWC_NO = VnHelper.ConvertToUnSign(txtISWC_NO.Text.Trim().ToUpper());
                currenObjectCreate.ISRC = VnHelper.ConvertToUnSign(txtISRC.Text.Trim().ToUpper());
                currenObjectCreate.WRITER = VnHelper.ConvertToUnSign(txtWRITER.Text.Trim().ToUpper());
                currenObjectCreate.ARTIST = VnHelper.ConvertToUnSign(txtARTIST.Text.Trim().ToUpper());
                currenObjectCreate.SOC_NAME = VnHelper.ConvertToUnSign(txtSOC_NAME.Text.Trim().ToUpper());
            }
            else
            {
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(txtWK_INT_NO.Text.Trim().ToUpper());
                currenObjectUpdate.TTL_ENG = VnHelper.ConvertToUnSign(txtTTL_ENG.Text.Trim().ToUpper());
                currenObjectUpdate.ISWC_NO = VnHelper.ConvertToUnSign(txtISWC_NO.Text.Trim().ToUpper());
                currenObjectUpdate.ISRC = VnHelper.ConvertToUnSign(txtISRC.Text.Trim().ToUpper());
                currenObjectUpdate.WRITER = VnHelper.ConvertToUnSign(txtWRITER.Text.Trim().ToUpper());
                currenObjectUpdate.ARTIST = VnHelper.ConvertToUnSign(txtARTIST.Text.Trim().ToUpper());
                currenObjectUpdate.SOC_NAME = VnHelper.ConvertToUnSign(txtSOC_NAME.Text.Trim().ToUpper());
            }

        }
        private bool CheckData()
        {
            if (txtWK_INT_NO.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtTTL_ENG.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtWRITER.Text.Trim() == string.Empty)
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!CheckData())
            //    {
            //        lbInfo.Text = "Please check again data!";
            //        return;
            //    }
            //    ObjectReturn = null;
            //    GetDataFromUI();
            //    if (_updataType == UpdataType.Add)
            //    {
            //        #region Add
            //        var data = await _Controller.Create(currenObjectCreate);
            //        ObjectReturn = data;
            //        if (data != null)
            //        {
            //            if (data.Status == Utilities.Common.UpdateStatus.Successfull)
            //            {
            //                this.Close();
            //            }
            //            else
            //            {
            //                lbInfo.Text = data.Message; ;
            //            }
            //            return;
            //        }
            //        else
            //        {
            //            lbInfo.Text = "No reponse";
            //        }
            //        #endregion
            //    }
            //    else if (_updataType == UpdataType.Edit)
            //    {
            //        #region edit
            //        var data = await _Controller.Update(currenObjectUpdate);
            //        ObjectReturn = data;
            //        if (data != null)
            //        {
            //            if (data.Status == Utilities.Common.UpdateStatus.Successfull)
            //            {
            //                this.Close();
            //            }
            //            else
            //            {
            //                lbInfo.Text = data.Message; ;
            //            }
            //            return;
            //        }
            //        else
            //        {
            //            lbInfo.Text = "No reponse";
            //        }
            //        #endregion
            //    }
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error: {ex.ToString()}");
            //}

        }

        #endregion
    }
}
