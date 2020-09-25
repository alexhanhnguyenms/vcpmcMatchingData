using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.Infrastructure.data;

namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH
{
    public partial class frmMemberChoisePO : Form
    {
        public string IdDsChoise { get; set; } = string.Empty;
        public frmMemberChoisePO()
        {
            InitializeComponent();
        }

        private void frmMemberChoisePO_Load(object sender, EventArgs e)
        {

            try
            {
                VcpmcContext ctx = new VcpmcContext();
                var x = (from s in ctx.MemberBHs
                         orderby s.TimeCreate descending
                         select s).ToList();
                dgvListPO.DataSource = x;
            }
            catch (Exception )
            {
                MessageBox.Show("Load import work member is error");
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
