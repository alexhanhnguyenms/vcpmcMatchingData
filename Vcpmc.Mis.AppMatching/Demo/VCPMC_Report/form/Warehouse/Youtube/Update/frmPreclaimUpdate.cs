using System;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Youtube;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Youtube.Update
{
    public partial class frmPreclaimUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private PreclaimViewModel currenObject;
        private PreclaimUpdateRequest currenObjectUpdate = new PreclaimUpdateRequest();
        private PreclaimCreateRequest currenObjectCreate = new PreclaimCreateRequest();      
        private PreclaimController _preclaimController;
        public UpdateStatusViewModel ObjectReturn = null;
        #endregion

        #region Init  
        public frmPreclaimUpdate(PreclaimController preclaimController, UpdataType view, PreclaimViewModel currenObject)
        {
            InitializeComponent();
            this._preclaimController = preclaimController;
            this._updataType = view;
            this.currenObject = currenObject;

        }

        private void frmPreclaimUpdate_Load(object sender, EventArgs e)
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
                    this.Text = "Update Preclaim";
                }
                else
                {
                    this.Text = "View Preclaim";
                }
                if (currenObject == null)
                {
                    return;
                }
                #region Common
                txtAsset_ID.Text = currenObject.Asset_ID.ToString();
                txtISRC.Text = currenObject.ISRC;
                txtComp_Asset_ID.Text = currenObject.Comp_Asset_ID;
                txtC_Title.Text = currenObject.C_Title;
                txtC_ISWC.Text = currenObject.C_ISWC;
                txtC_Workcode.Text = currenObject.C_Workcode;
                txtC_Writers.Text = currenObject.C_Writers;
                #endregion

                #region Contract
                numCombined_Claim.Text = currenObject.Combined_Claim;
                numMechanical.Text = currenObject.Mechanical;
                numPerformance.Text = currenObject.Performance;
                //numMONTH.Text = currenObject.MONTH.ToString();
                //numYear.Text = currenObject.Year.ToString();
                if(currenObject.DtCREATED_AT!=null)
                {
                    if(currenObject.DtCREATED_AT < dtCreate_at.MinDate)
                    {
                        currenObject.DtCREATED_AT = dtCreate_at.MinDate;
                    }
                }
                else
                {
                    currenObject.DtCREATED_AT = DateTime.Now;
                }
                dtCreate_at.Value = currenObject.DtCREATED_AT;
                if (currenObject.DtUPDATED_AT != null)
                {
                    if (currenObject.DtUPDATED_AT < dtCreate_at.MinDate)
                    {
                        currenObject.DtUPDATED_AT = dtCreate_at.MinDate;
                    }
                    dtUpdate_at.Value = (DateTime)currenObject.DtUPDATED_AT;
                }
                else
                {
                    
                    if(_updataType == UpdataType.Edit)
                    {
                        dtUpdate_at.Visible = true;
                        lbUpdateAt.Text = "Update at:";
                        dtUpdate_at.Value = DateTime.Now;                        
                    }
                    else
                    {
                        dtUpdate_at.Visible = false;
                        lbUpdateAt.Text = "Update at: null";
                    }
                }
                #endregion

                #region enable     
                dtCreate_at.Enabled = false;               
                if (_updataType == UpdataType.View)
                {
                    #region Common
                    txtAsset_ID.ReadOnly = true;
                    txtISRC.ReadOnly = true;
                    txtComp_Asset_ID.ReadOnly = true;
                    txtC_Title.ReadOnly = true;
                    txtC_ISWC.ReadOnly = true;
                    txtC_Workcode.ReadOnly = true;
                    txtC_Writers.ReadOnly = true;
                    numCombined_Claim.ReadOnly = true;
                    numMechanical.ReadOnly = true;
                    numPerformance.ReadOnly = true;
                    //numMONTH.ReadOnly = true;
                    //numYear.ReadOnly = true;
                    btnOk.Enabled = false;
                    #endregion
                }
                else
                {
                    txtAsset_ID.ReadOnly = true;
                    //numMONTH.Enabled = false;
                    //numYear.Enabled = false;
                    dtUpdate_at.Enabled = false;
                }    
                #endregion
                
            }
            else if (_updataType == UpdataType.Add)
            {
                this.Text = "Add Preclaim";
                currenObject = new PreclaimViewModel();
                dtUpdate_at.Visible = false;
                lbUpdateAt.Visible = false;
                dtCreate_at.Value = DateTime.Now;
                //numMONTH.Value = DateTime.Now.Month;
                //numYear.Value = DateTime.Now.Year;
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
                currenObjectCreate.Asset_ID = VnHelper.ConvertToUnSign(txtAsset_ID.Text.Trim());
                currenObjectCreate.ISRC = VnHelper.ConvertToUnSign(txtISRC.Text.Trim().ToUpper());
                currenObjectCreate.Comp_Asset_ID = VnHelper.ConvertToUnSign(txtComp_Asset_ID.Text.Trim().ToUpper());
                currenObjectCreate.C_Title = VnHelper.ConvertToUnSign(txtC_Title.Text.Trim().ToUpper());
                currenObjectCreate.C_ISWC = VnHelper.ConvertToUnSign(txtC_ISWC.Text.Trim().ToUpper());
                currenObjectCreate.C_Workcode = VnHelper.ConvertToUnSign(txtC_Workcode.Text.Trim().ToUpper());
                currenObjectCreate.C_Writers = VnHelper.ConvertToUnSign(txtC_Writers.Text.Trim().ToUpper());

                currenObjectCreate.Combined_Claim = numCombined_Claim.Text.Trim().ToUpper();
                currenObjectCreate.Mechanical = numMechanical.Text.Trim().ToUpper();
                currenObjectCreate.Performance = numPerformance.Text.Trim().ToUpper();

                //currenObjectCreate.MONTH = (int)numMONTH.Value;    
                currenObjectCreate.DtCREATED_AT = dtCreate_at.Value;
                currenObjectCreate.DtUPDATED_AT = null;
                //currenObjectCreate.Year = (int)numYear.Value;
            }
            else
            {
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.Asset_ID = VnHelper.ConvertToUnSign(txtAsset_ID.Text.Trim());
                currenObjectUpdate.ISRC = VnHelper.ConvertToUnSign(txtISRC.Text.Trim().ToUpper());
                currenObjectUpdate.Comp_Asset_ID = VnHelper.ConvertToUnSign(txtComp_Asset_ID.Text.Trim().ToUpper());
                currenObjectUpdate.C_Title = VnHelper.ConvertToUnSign(txtC_Title.Text.Trim().ToUpper());
                currenObjectUpdate.C_ISWC = VnHelper.ConvertToUnSign(txtC_ISWC.Text.Trim().ToUpper());
                currenObjectUpdate.C_Workcode = VnHelper.ConvertToUnSign(txtC_Workcode.Text.Trim().ToUpper());
                currenObjectUpdate.C_Writers = VnHelper.ConvertToUnSign(txtC_Writers.Text.Trim().ToUpper());

                currenObjectUpdate.Combined_Claim = numCombined_Claim.Text.Trim().ToUpper();
                currenObjectUpdate.Mechanical = numMechanical.Text.Trim().ToUpper();
                currenObjectUpdate.Performance = numPerformance.Text.Trim().ToUpper();

                //currenObjectUpdate.MONTH = (int)numMONTH.Value;
                currenObjectUpdate.DtCREATED_AT = dtCreate_at.Value;
                currenObjectUpdate.DtUPDATED_AT = dtUpdate_at.Value;
                //currenObjectUpdate.Year = (int)numYear.Value;
            }

        }
        private bool CheckData()
        {
            if(txtAsset_ID.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtC_Title.Text.Trim() == string.Empty)
            {
                return false;
            }
            if (txtC_Workcode.Text.Trim() == string.Empty)
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
                ObjectReturn = new UpdateStatusViewModel {
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
                if(!CheckData())
                {
                    lbInfo.Text = "Please check again data!";
                    return;
                }
                ObjectReturn = null;
                GetDataFromUI();
                if (_updataType == UpdataType.Add)
                {
                    #region Add
                    var data = await _preclaimController.Create(currenObjectCreate);
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
                    var data = await _preclaimController.Update(currenObjectUpdate);
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
