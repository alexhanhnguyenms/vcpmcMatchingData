using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using Vcpmc.Mis.Shared.viewModel.report;

namespace Vcpmc.Mis.AppMatching.form.Distribution.BH.report
{
    public partial class frmViewBhDistributionReport : Form
    {
        public frmViewBhDistributionReport(string IdData)
        {
            InitializeComponent();
            this.IdData = IdData;
        }
        string member;
        string bhmedia;
        DateTime dtReport;
        public frmViewBhDistributionReport(string member, string bhmedia, DateTime dtReport)
        {
            InitializeComponent();
            this.IdData = "";
            this.member = member;
            this.bhmedia = bhmedia;
            this.dtReport = dtReport;
        }
        public string IdData = "";
        public List<BhDistributionViewModel> dataSource = new List<BhDistributionViewModel>();
        /// <summary>
        /// Mau
        /// </summary>
        public int ReportTemplate { get; internal set; } = 1;

        private void frmDistributionReport_Load(object sender, EventArgs e)
        {
            try
            {
                var reportDatSource = new ReportDataSource("DataSet1", dataSource);
                //int count = 0;
                //decimal value = 0;
                //decimal value2 = 0;
                //foreach (var item in dataSource)
                //{
                //    count++;
                //    value += item.Royalty;
                //    value2 += item.Royalty2;
                //    if (item.Royalty != item.Royalty2)
                //    {
                //        int a = 1;
                //    }
                //}
                string type = "";
                if (ReportTemplate != 0)
                {
                    type = ReportTemplate.ToString();
                }
                ReportParameter[] reportParameters = new ReportParameter[] {
                    //new ReportParameter("strdate", dtReport.ToString("dd")),
                    //new ReportParameter("strmonth",dtReport.ToString("MM")),
                   //new ReportParameter("stryear",dtReport.ToString("yyy")),
                    new ReportParameter("strMember", bhmedia),
                    new ReportParameter("strBh",member),
                    //new ReportParameter("strPeopleSign", member),
                };
                string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\" + $"DistributionReport{type}.rdlc";
                //string path = Core.PathReport+ $"DistributionReport{type}.rdlc";
                reportViewer1.LocalReport.ReportPath = path;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDatSource);
                reportViewer1.LocalReport.SetParameters(reportParameters);
                this.reportViewer1.RefreshReport();               
                //var data = reportViewer1.LocalReport.Render("PDF");
                //FileStream newFile = new FileStream(@"D:\a.pdf", FileMode.Create);
                //newFile.Write(data, 0, data.Length);
                //newFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
    }
}
