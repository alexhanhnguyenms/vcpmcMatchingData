using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.Distribution.BH
{
    public partial class frmDistributionChoisePO : Form
    {
        public frmDistributionChoisePO()
        {
            InitializeComponent();
        }

        public string IdDsChoise { get; internal set; }

        private void frmDistributionChoisePO_Load(object sender, EventArgs e)
        {
            try
            {
                VcpmcContext ctx = new VcpmcContext();
                var x = (from s in ctx.DistributionDatas
                         orderby s.TimeCreate descending
                         select s).ToList();
                dgvListPO.DataSource = x;
            }
            catch (Exception )
            {
                MessageBox.Show("Load list PO is error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListPO.Rows.Count > 0)
                {
                    IdDsChoise = dgvListPO.CurrentRow.Cells["Id"].Value.ToString();
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
