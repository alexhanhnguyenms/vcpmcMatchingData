using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.masterlist
{
    public partial class frmMasterImportChoise : Form
    {
        public string IdDsChoise { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public frmMasterImportChoise()
        {
            InitializeComponent();
        }

        private void frmMasterImportChoise_Load(object sender, EventArgs e)
        {
            try
            {
                VcpmcContext ctx = new VcpmcContext();
                //var x = (from s in ctx.YoutubeFiles
                //         orderby s.TimeCreate descending
                //         select s).ToList();
                //dgvListPO.DataSource = x;
            }
            catch (Exception )
            {
                MessageBox.Show("Load import Masterlist PO be error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListPO.Rows.Count > 0)
                {
                    IdDsChoise = dgvListPO.CurrentRow.Cells["Id"].Value.ToString();
                    Info = dgvListPO.CurrentRow.Cells["Namex"].Value.ToString();
                    this.Close();
                }
                else
                {
                    IdDsChoise = "";
                    this.Close();
                }

            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}
