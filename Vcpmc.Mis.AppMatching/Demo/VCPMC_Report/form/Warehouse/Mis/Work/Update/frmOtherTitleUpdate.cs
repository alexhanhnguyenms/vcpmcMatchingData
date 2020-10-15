using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmOtherTitleUpdate : Form
    {
        public OtherTitle  otherTitle = new OtherTitle();
        private UpdataType _updataType;
        private int maxNo = 0;
        public frmOtherTitleUpdate(UpdataType _updataType,int maxNo, OtherTitle otherTitle)
        {
            InitializeComponent();
            this.maxNo = maxNo;
            this._updataType = _updataType;
            this.otherTitle = otherTitle;
        }


        private void frmOtherTitleUpdate_Load(object sender, EventArgs e)
        {
            txtNo.Enabled = false;
            txtNo.Text = (maxNo + 1).ToString();
            if(_updataType == UpdataType.Add)
            {

            }
            else if (_updataType == UpdataType.Edit || _updataType == UpdataType.View)
            {
                if(_updataType == UpdataType.View)
                {
                    txtTitle.Enabled = false;
                }
                txtTitle.Text = otherTitle.Title;
                txtTTL_LOCAL.Text = otherTitle.TTL_LOCAL;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            otherTitle = null;
            if (txtTitle.Text.Trim()==string.Empty)
            {
                lbInfo.Text = "Title is empty, please input title";
                return;
            }
            otherTitle = new OtherTitle();
            otherTitle.No = (maxNo+1);
            otherTitle.Title = VnHelper.ConvertToUnSign(txtTitle.Text.Trim()).ToUpper();
            otherTitle.TTL_LOCAL = txtTTL_LOCAL.Text.Trim().ToUpper();
            //otherTitle.TitleType = txtTitleType.Text.Trim();
            //otherTitle.Language = txtLanguage.Text.Trim();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            otherTitle = null;
            this.Close();
        }
    }
}
