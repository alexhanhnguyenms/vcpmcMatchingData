using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.Mis.Works;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmWorkUpdate : Form
    {
        #region vari
        private UpdataType _updataType;
        private WorkViewModel currenObject;
        private WorkUpdateRequest currenObjectUpdate = new WorkUpdateRequest();
        private WorkCreateRequest currenObjectCreate = new WorkCreateRequest();
        private WorkController _Controller;
        public UpdateStatusViewModel ObjectReturn = null;
        #endregion

        #region init
        public frmWorkUpdate(WorkController Controller, UpdataType view, WorkViewModel currenObject)
        {
            InitializeComponent();
            this._Controller = Controller;
            this._updataType = view;
            this.currenObject = currenObject;
        }

        private void frmWorkUpdate_Load(object sender, EventArgs e)
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
                    this.Text = "Update Work";
                }
                else
                {
                    this.Text = "View Work";
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

                string[] WRITERs = currenObject.WRITER.Split(',');
                if (WRITERs.Length>0)
                {
                    int maxNo = 1;
                    if(currenObject.InterestedParties.Count > 0)
                    {
                        var item = currenObject.OtherTitles.OrderByDescending(p => p.No).FirstOrDefault();
                        if (item != null)
                        {
                            maxNo = item.No+1;
                        }
                        //else
                        //{
                        //    currenObjectUpdate
                        //}    
                    }

                    for (int i = 0; i < WRITERs.Length; i++)
                    {
                        if(currenObject.InterestedParties.Where(p=>p.IP_NAME.Trim() == WRITERs[i].Trim()).ToList().Count == 0)
                        {
                            currenObject.InterestedParties.Add(new InterestedParty
                            {        
                                No = maxNo,
                                IP_NAME = WRITERs[i].Trim()                                
                            }) ;
                            maxNo++;
                        }
                    }
                }
                currenObjectUpdate.InterestedParties = currenObject.InterestedParties;
                //txtWRITER.Text = currenObject.WRITER;
                txtARTIST.Text = currenObject.ARTIST;
                txtSOC_NAME.Text = currenObject.SOC_NAME;
                numStarRating.Value = currenObject.StarRating;

                dgvInterestedParties.DataSource = currenObject.InterestedParties;
                dgvOtherTitle.DataSource = currenObject.OtherTitles;
                #endregion

                #region Other Title
                if (currenObject.OtherTitles!=null)
                {
                    dgvOtherTitle.DataSource = currenObject.OtherTitles;
                }
                #endregion

                #region interested Paties
                if (currenObject.InterestedParties != null)
                {
                    dgvInterestedParties.DataSource = currenObject.InterestedParties;
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
                    //txtWRITER.ReadOnly = true;
                    txtARTIST.ReadOnly = true;
                    txtSOC_NAME.ReadOnly = true;                    
                    btnOk.Enabled = false;
                    //dgvInterestedParties.Enabled = false;
                    //dgvOtherTitle.Enabled = false;
                    ctMenuOtherTitle.Enabled = false;
                    ctMenuInterestedParties.Enabled = false;
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
                this.Text = "Add Work";
                currenObject = new WorkViewModel();                
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
                currenObjectCreate.ISRC = txtISRC.Text.Trim();
                bool isCOMPLETE = true;
                for (int i = 0; i < currenObjectCreate.InterestedParties.Count; i++)
                {
                    if(currenObjectCreate.WRITER!=string.Empty)
                    {
                        currenObjectCreate.WRITER += ",";
                    }
                    currenObjectCreate.WRITER += currenObjectCreate.InterestedParties[i].IP_NAME;
                    if(isCOMPLETE && currenObjectCreate.InterestedParties[i].WK_STATUS != "COMPLETE")
                    {
                        isCOMPLETE = false;
                    }
                }
                currenObjectCreate.ARTIST = VnHelper.ConvertToUnSign(txtARTIST.Text.Trim().ToUpper());
                currenObjectCreate.SOC_NAME = VnHelper.ConvertToUnSign(txtSOC_NAME.Text.Trim().ToUpper());
                currenObjectCreate.WK_STATUS = isCOMPLETE == true? "COMPLETE": "INCOMPLETE";
                currenObjectCreate.StarRating = (int)numStarRating.Value;
            }
            else
            {
                bool isCOMPLETE = true;
                currenObjectUpdate.Id = currenObject.Id;
                currenObjectUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(txtWK_INT_NO.Text.Trim().ToUpper());
                currenObjectUpdate.TTL_ENG = VnHelper.ConvertToUnSign(txtTTL_ENG.Text.Trim().ToUpper());
                currenObjectUpdate.ISWC_NO = VnHelper.ConvertToUnSign(txtISWC_NO.Text.Trim().ToUpper());
                currenObjectUpdate.ISRC = VnHelper.ConvertToUnSign(txtISRC.Text.Trim().ToUpper());

                for (int i = 0; i < currenObjectUpdate.InterestedParties.Count; i++)
                {
                    if (currenObjectUpdate.WRITER != string.Empty)
                    {
                        currenObjectUpdate.WRITER += ",";
                    }
                    currenObjectUpdate.WRITER += currenObjectUpdate.InterestedParties[i].IP_NAME;
                    if (isCOMPLETE && currenObjectUpdate.InterestedParties[i].WK_STATUS != "COMPLETE")
                    {
                        isCOMPLETE = false;
                    }
                }
                currenObjectUpdate.ARTIST = VnHelper.ConvertToUnSign(txtARTIST.Text.Trim().ToUpper());
                currenObjectUpdate.SOC_NAME = VnHelper.ConvertToUnSign(txtSOC_NAME.Text.Trim().ToUpper());
                currenObjectUpdate.WK_STATUS = isCOMPLETE == true ? "COMPLETE" : "INCOMPLETE";
                currenObjectUpdate.StarRating = (int)numStarRating.Value;
            }

        }
        private bool CheckData()
        {
            if (txtWK_INT_NO.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "Please input Wordcode";
                return false;
            }
            if (txtTTL_ENG.Text.Trim() == string.Empty)
            {
                lbInfo.Text = "Please input title";
                return false;
            }
            if(dgvInterestedParties.Rows.Count==0)
            {
                lbInfo.Text = "Please input interested parties";
                return false;
            }
            //if(_updataType == UpdataType.Add)
            //{
            //    if(currenObjectCreate.InterestedParties.Count==0)
            //    {
            //        lbInfo.Text = "Please input interested parties";
            //        return false;
            //    }
            //}
            //else
            //{
            //    if (currenObjectUpdate.InterestedParties.Count == 0)
            //    {
            //        lbInfo.Text = "Please input interested parties";
            //        return false;
            //    }
            //}           
           
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

        #region ctmenu other title
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int maxNo = 0;
                if (_updataType == UpdataType.Add)
                {
                    if(currenObjectCreate.OtherTitles.Count >0)
                    {
                        var item = currenObjectCreate.OtherTitles.OrderByDescending(p => p.No).FirstOrDefault();
                        if(item!=null)
                        {
                            maxNo = item.No;
                        }
                    }
                }
                else
                {
                    if (currenObjectUpdate.OtherTitles.Count > 0)
                    {
                        var item = currenObjectUpdate.OtherTitles.OrderByDescending(p => p.No).FirstOrDefault();
                        if (item != null)
                        {
                            maxNo = item.No;
                        }
                    }                   
                    
                }
                frmOtherTitleUpdate frm = new frmOtherTitleUpdate(UpdataType.Add, maxNo,null);
                frm.ShowDialog();
                if (frm.otherTitle != null)
                {
                    if (_updataType == UpdataType.Add)
                    {
                        currenObjectCreate.OtherTitles.Add(frm.otherTitle);
                        currenObjectCreate.OtherTitles = currenObjectCreate.OtherTitles.OrderBy(p => p.No).ToList();
                        dgvOtherTitle.DataSource = currenObjectCreate.OtherTitles;
                    }
                    else
                    {
                        currenObjectUpdate.OtherTitles.Add(frm.otherTitle);
                        currenObjectUpdate.OtherTitles = currenObjectUpdate.OtherTitles.OrderBy(p => p.No).ToList();
                        dgvOtherTitle.DataSource = currenObjectUpdate.OtherTitles;
                    }                    
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int maxNo = 0;
                OtherTitle otherTitle = null;
                if (_updataType == UpdataType.Add)
                {
                    if (currenObjectCreate.OtherTitles.Count > 0)
                    {
                        maxNo = int.Parse(dgvOtherTitle.CurrentRow.Cells["no"].Value.ToString());
                        otherTitle = currenObjectCreate.OtherTitles.Where(p => p.No == maxNo).FirstOrDefault();
                    }
                }
                else
                {
                    if (currenObjectUpdate.OtherTitles.Count > 0)
                    {
                        maxNo = int.Parse(dgvOtherTitle.CurrentRow.Cells["no"].Value.ToString());
                        otherTitle = currenObjectUpdate.OtherTitles.Where(p => p.No == maxNo).FirstOrDefault();

                    }
                }
                if(otherTitle!=null)
                {
                    frmOtherTitleUpdate frm = new frmOtherTitleUpdate(UpdataType.Edit, maxNo, otherTitle);
                    frm.ShowDialog();
                    if (frm.otherTitle != null)
                    {
                        if (_updataType == UpdataType.Add)
                        {
                            var item = currenObjectCreate.OtherTitles.Where(p => p.No == maxNo).FirstOrDefault();
                            if(item!=null)
                            {
                                item.Title = frm.otherTitle.Title;
                            }                            
                            currenObjectCreate.OtherTitles = currenObjectCreate.OtherTitles.OrderBy(p => p.No).ToList();
                            dgvOtherTitle.DataSource = currenObjectCreate.OtherTitles;
                        }
                        else
                        {
                            var item = currenObjectUpdate.OtherTitles.Where(p => p.No == maxNo).FirstOrDefault();
                            if (item != null)
                            {
                                item.Title = frm.otherTitle.Title;
                            }
                            currenObjectUpdate.OtherTitles = currenObjectUpdate.OtherTitles.OrderBy(p => p.No).ToList();
                            dgvOtherTitle.DataSource = currenObjectUpdate.OtherTitles;
                        }
                    }
                }                
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void deteleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int no=-1;
                OtherTitle otherTitle = null;
                if (_updataType == UpdataType.Add)
                {
                    if(currenObjectCreate.OtherTitles.Count>0)
                    {
                        no = int.Parse(dgvOtherTitle.CurrentRow.Cells["no"].Value.ToString());
                        otherTitle = currenObjectCreate.OtherTitles.Where(p => p.No == no).FirstOrDefault();                       
                    }                   
                }
                else
                {
                    if (currenObjectUpdate.OtherTitles.Count > 0)
                    {
                        no = int.Parse(dgvOtherTitle.CurrentRow.Cells["no"].Value.ToString());
                        otherTitle = currenObjectUpdate.OtherTitles.Where(p => p.No == no).FirstOrDefault();
                        
                    }                   
                }
                if(no > 0)
                {
                    DialogResult dr = MessageBox.Show("Are you sure delete?.", "DELETE Confirm", MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    { 
                        if (otherTitle != null)
                        {
                            if (_updataType == UpdataType.Add)
                            {
                                dgvOtherTitle.DataSource = new List<OtherTitle>();
                                currenObjectCreate.OtherTitles.Remove(otherTitle);
                                dgvOtherTitle.DataSource = currenObjectCreate.OtherTitles;                                
                            }
                            else
                            {
                                dgvOtherTitle.DataSource = new List<OtherTitle>();
                                currenObjectUpdate.OtherTitles.Remove(otherTitle);
                                //dgvOtherTitle.DataSource = currenObjectUpdate.OtherTitles;
                                //dgvOtherTitle.DataSource = new List<OtherTitle>();
                                dgvOtherTitle.DataSource = currenObjectUpdate.OtherTitles;
                                //dgvOtherTitle.Invalidate();
                            }                           
                        }
                    }                   
                }                
            }
            catch (Exception)
            {
                //throw;
            }
        }
        #endregion

        #region ctmenu interested parties
        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                List<InterestedParty> inparlist = null;
                int maxNo = 0;
                if (_updataType == UpdataType.Add)
                {
                    if (currenObjectCreate.InterestedParties.Count > 0)
                    {
                        inparlist = currenObjectCreate.InterestedParties;
                        var item = currenObjectCreate.InterestedParties.OrderByDescending(p => p.No).FirstOrDefault();
                        if (item != null)
                        {
                            maxNo = item.No;
                        }
                    }
                }
                else
                {
                    if (currenObjectUpdate.InterestedParties.Count > 0)
                    {
                        inparlist = currenObjectUpdate.InterestedParties;
                        var item = currenObjectUpdate.InterestedParties.OrderByDescending(p => p.No).FirstOrDefault();
                        if (item != null)
                        {
                            maxNo = item.No;
                        }
                    }

                }
                frmInterestedParitesUpdate frm = new frmInterestedParitesUpdate(UpdataType.Add, maxNo, null, inparlist);
                frm.ShowDialog();
                if (frm.inPar != null)
                {
                    if (_updataType == UpdataType.Add)
                    {
                        currenObjectCreate.InterestedParties.Add(frm.inPar);
                        currenObjectCreate.InterestedParties = currenObjectCreate.InterestedParties.OrderBy(p => p.IP_INT_NO).ToList();
                        dgvInterestedParties.DataSource = currenObjectCreate.InterestedParties;
                    }
                    else
                    {
                        currenObjectUpdate.InterestedParties.Add(frm.inPar);
                        currenObjectUpdate.InterestedParties = currenObjectUpdate.InterestedParties.OrderBy(p => p.IP_INT_NO).ToList();
                        dgvInterestedParties.DataSource = currenObjectUpdate.InterestedParties;
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void EditToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int maxNo = 0;
                InterestedParty inPar = null;
                List<InterestedParty> inparlist = null;
                if (_updataType == UpdataType.Add)
                {
                    if (currenObjectCreate.InterestedParties.Count > 0)
                    {
                        maxNo = int.Parse(dgvInterestedParties.CurrentRow.Cells["Nox"].Value.ToString());
                        inparlist = currenObjectCreate.InterestedParties;
                        inPar = currenObjectCreate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();
                    }
                }
                else
                {
                    if (currenObjectUpdate.InterestedParties.Count > 0)
                    {
                        maxNo = int.Parse(dgvInterestedParties.CurrentRow.Cells["Nox"].Value.ToString());
                        inparlist = currenObjectUpdate.InterestedParties;
                        inPar = currenObjectUpdate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();

                    }
                }
                if (inPar != null)
                {
                    frmInterestedParitesUpdate frm = new frmInterestedParitesUpdate(UpdataType.Edit, maxNo, inPar, inparlist);
                    frm.ShowDialog();
                    if (frm.inPar != null)
                    {
                        if (_updataType == UpdataType.Add)
                        {
                            var item = currenObjectCreate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();
                            if (item != null)
                            {
                                item.IP_INT_NO = frm.inPar.IP_INT_NO;
                                item.IP_NAMETYPE = frm.inPar.IP_NAMETYPE;
                                item.IP_WK_ROLE = frm.inPar.IP_WK_ROLE;
                                item.IP_NAME = frm.inPar.IP_NAME;
                                item.WK_STATUS = frm.inPar.WK_STATUS;
                                item.PER_OWN_SHR = frm.inPar.PER_OWN_SHR;
                                item.PER_COL_SHR = frm.inPar.PER_COL_SHR;
                                item.MEC_OWN_SHR = frm.inPar.MEC_OWN_SHR;
                                item.MEC_COL_SHR = frm.inPar.MEC_COL_SHR;
                                item.SP_SHR = frm.inPar.SP_SHR;
                                item.TOTAL_MEC_SHR = frm.inPar.TOTAL_MEC_SHR;
                                item.SYN_OWN_SHR = frm.inPar.SYN_OWN_SHR;
                                item.SYN_COL_SHR = frm.inPar.SYN_COL_SHR;
                            }
                            currenObjectCreate.InterestedParties = currenObjectCreate.InterestedParties.OrderBy(p => p.IP_INT_NO).ToList();
                            dgvInterestedParties.DataSource = currenObjectCreate.InterestedParties;
                        }
                        else
                        {
                            var item = currenObjectUpdate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();
                            if (item != null)
                            {
                                item.IP_INT_NO = frm.inPar.IP_INT_NO;
                                item.IP_NAMETYPE = frm.inPar.IP_NAMETYPE;
                                item.IP_WK_ROLE = frm.inPar.IP_WK_ROLE;
                                item.IP_NAME = frm.inPar.IP_NAME;
                                item.WK_STATUS = frm.inPar.WK_STATUS;
                                item.PER_OWN_SHR = frm.inPar.PER_OWN_SHR;
                                item.PER_COL_SHR = frm.inPar.PER_COL_SHR;
                                item.MEC_OWN_SHR = frm.inPar.MEC_OWN_SHR;
                                item.MEC_COL_SHR = frm.inPar.MEC_COL_SHR;
                                item.SP_SHR = frm.inPar.SP_SHR;
                                item.TOTAL_MEC_SHR = frm.inPar.TOTAL_MEC_SHR;
                                item.SYN_OWN_SHR = frm.inPar.SYN_OWN_SHR;
                                item.SYN_COL_SHR = frm.inPar.SYN_COL_SHR;
                            }
                            currenObjectUpdate.InterestedParties = currenObjectUpdate.InterestedParties.OrderBy(p => p.IP_INT_NO).ToList();
                            dgvInterestedParties.DataSource = currenObjectUpdate.InterestedParties;
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void DeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int maxNo = 0;
                InterestedParty inPar = null;
                if (_updataType == UpdataType.Add)
                {
                    if (currenObjectCreate.InterestedParties.Count > 0)
                    {
                        maxNo = int.Parse(dgvInterestedParties.CurrentRow.Cells["Nox"].Value.ToString());
                        inPar = currenObjectCreate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();
                    }
                }
                else
                {
                    if (currenObjectUpdate.InterestedParties.Count > 0)
                    {
                        maxNo = int.Parse(dgvInterestedParties.CurrentRow.Cells["Nox"].Value.ToString());
                        inPar = currenObjectUpdate.InterestedParties.Where(p => p.No == maxNo).FirstOrDefault();

                    }
                }
                if (maxNo > 0)
                {
                    DialogResult dr = MessageBox.Show("Are you sure delete?.", "DELETE Confirm", MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        if (inPar != null)
                        {
                            if (_updataType == UpdataType.Add)
                            {
                                //dgvInterestedParties.Rows.Clear();                                
                                currenObjectCreate.InterestedParties.Remove(inPar);
                                dgvInterestedParties.DataSource = new List<InterestedParty>();
                                dgvInterestedParties.DataSource = currenObjectCreate.InterestedParties;
                                dgvInterestedParties.Invalidate();
                               
                            }
                            else
                            {
                                //dgvInterestedParties.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
                                //dgvInterestedParties.Rows.Clear();
                                currenObjectUpdate.InterestedParties.Remove(inPar);
                                dgvInterestedParties.DataSource = new List<InterestedParty>();
                                dgvInterestedParties.DataSource = currenObjectUpdate.InterestedParties;
                                dgvInterestedParties.Invalidate();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //int a = 1;
            }
        }
        #endregion
    }
}
