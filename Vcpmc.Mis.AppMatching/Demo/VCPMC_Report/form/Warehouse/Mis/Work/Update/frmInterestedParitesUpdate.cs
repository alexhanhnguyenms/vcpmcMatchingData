//using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmInterestedParitesUpdate : Form
    {
        public InterestedParty inPar = new InterestedParty();
        List<InterestedParty> inparlist = null;
        private UpdataType _updataType;
        private int maxNo = 0;
        string IP_INT_NO_old = string.Empty;
        public frmInterestedParitesUpdate(UpdataType _updataType, int maxNo, InterestedParty inPar, List<InterestedParty> inparlist)
        {
            InitializeComponent();
            this.maxNo = maxNo;
            this._updataType = _updataType;
            this.inPar = inPar;
            this.inparlist = inparlist;
            if(inPar != null)
            {
                IP_INT_NO_old = inPar.IP_INT_NO;
            }
        }
        private void frmInterestedParitesUpdate_Load(object sender, EventArgs e)
        {
            txtNo.Enabled = false;           

            cboIP_NAMETYPE.SelectedIndex = 0;
            //cboWK_STATUS.SelectedIndex = 0;
            cboIP_WK_ROLE.SelectedIndex = 0;

            if (_updataType == UpdataType.Add)
            {
                txtNo.Text = (maxNo + 1).ToString();
            }
            else if (_updataType == UpdataType.Edit || _updataType == UpdataType.View)
            {
                txtNo.Text = (maxNo).ToString();
                //txtIP_INT_NO.Enabled = false;
                if (_updataType == UpdataType.View)
                {
                    txtIP_NAME.ReadOnly = true;
                    txtIP_INT_NO.ReadOnly = true;
                    cboIP_NAMETYPE.Enabled = false;
                    //cboWK_STATUS.Enabled = false;
                    cboIP_WK_ROLE.Enabled = false;

                    numPER_OWN_SHR.ReadOnly = true;
                    numPER_COL_SHR.ReadOnly = true;
                    numMEC_OWN_SHR.ReadOnly = true;
                    numMEC_COL_SHR.ReadOnly = true;

                    numSP_SHR.ReadOnly = true;
                    numTOTAL_MEC_SHR.ReadOnly = true;
                    numSYN_OWN_SHR.ReadOnly = true;
                    numSYN_COL_SHR.ReadOnly = true;

                }
                txtIP_INT_NO.Text = inPar.IP_INT_NO;
                txtIP_NAME.Text = inPar.IP_NAME;
                cboIP_NAMETYPE.Text = inPar.IP_NAMETYPE;
                //cboWK_STATUS.Text = inPar.WK_STATUS;
                cboIP_WK_ROLE.Text = inPar.IP_WK_ROLE;

                numPER_OWN_SHR.Value = inPar.PER_OWN_SHR;
                numPER_COL_SHR.Value = inPar.PER_COL_SHR;

                numMEC_OWN_SHR.Value = inPar.MEC_OWN_SHR;
                numMEC_COL_SHR.Value = inPar.MEC_COL_SHR;

                numSP_SHR.Value = inPar.SP_SHR;
                numTOTAL_MEC_SHR.Value = inPar.TOTAL_MEC_SHR;

                numSYN_OWN_SHR.Value = inPar.SYN_OWN_SHR;
                numSYN_COL_SHR.Value = inPar.SYN_COL_SHR;
                txtSociety.Text = inPar.Society;
                txtIP_NAME_LOCAL.Text = inPar.IP_NAME_LOCAL;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            inPar = null;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            inPar = null;
            //int countItem = 0;
            if(inparlist!=null&& inparlist.Count>0)
            {
                foreach (var item in inparlist)
                {
                    if(txtIP_INT_NO.Text.Trim() == item.IP_INT_NO)
                    {
                        //countItem++;
                        if (_updataType == UpdataType.Add)
                        {
                            lbInfo.Text = $"IP_INT_NO ({item.IP_INT_NO}) is exist, please input again";
                            return;
                        }
                        else
                        {
                            if (IP_INT_NO_old != txtIP_INT_NO.Text.Trim())
                            {
                                lbInfo.Text = $"IP_INT_NO ({item.IP_INT_NO}) is exist, please input again";
                                return;
                            }
                        }

                    }
                }
            }
            if (txtIP_NAME.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "IP_NAME is empty, please input title";
                return;
            }
            inPar = new InterestedParty();
            inPar.No = (maxNo + 1);
            inPar.IP_INT_NO = txtIP_INT_NO.Text.Trim().ToUpper();
            inPar.IP_NAME = VnHelper.ConvertToUnSign(txtIP_NAME.Text.Trim()).ToUpper(); 

            inPar.IP_NAMETYPE = cboIP_NAMETYPE.Text.Trim();
            inPar.IP_WK_ROLE = cboIP_WK_ROLE.Text.Trim();
            //inPar.WK_STATUS = cboWK_STATUS.Text.Trim();

            inPar.PER_OWN_SHR = numPER_OWN_SHR.Value;
            inPar.PER_COL_SHR = numPER_COL_SHR.Value;

            inPar.MEC_OWN_SHR = numMEC_OWN_SHR.Value;
            inPar.MEC_COL_SHR = numMEC_COL_SHR.Value;

            inPar.SP_SHR = numSP_SHR.Value;
            inPar.TOTAL_MEC_SHR = numTOTAL_MEC_SHR.Value;

            inPar.SYN_OWN_SHR = numSYN_OWN_SHR.Value;
            inPar.SYN_COL_SHR = numSYN_COL_SHR.Value;
            inPar.Society = txtSociety.Text.Trim().ToUpper();
            inPar.IP_NAME_LOCAL = txtIP_NAME_LOCAL.Text.Trim().ToUpper();
            inPar.CountUpdate++;
            inPar.LastUpdateAt = DateTime.Now;
            this.Close();
        }       
    }
}
