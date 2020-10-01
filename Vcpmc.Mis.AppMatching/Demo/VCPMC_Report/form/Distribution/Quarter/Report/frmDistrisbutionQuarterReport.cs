using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.Distribution.Quarter.Report
{
    public partial class frmDistrisbutionQuarterReport : Form
    {
        string path = "";
        string strPP=string.Empty;
        public ViewModels.Mis.Distribution.Quarter.Distribution dataSource = new ViewModels.Mis.Distribution.Quarter.Distribution();
        public int ReportTemplate { get; internal set; } = 1;
        public frmDistrisbutionQuarterReport(string path,string strPP)
        {
            InitializeComponent();
            this.path = path;
            this.strPP = strPP;
        }

        private void frmDistrisbutionQuarter_Load(object sender, EventArgs e)
        {
            try
            {
                var reportDatSource = new ReportDataSource("DataSet1", dataSource.Items);                 
                ReportParameter[] reportParameters = new ReportParameter[] {
                    new ReportParameter("IP", dataSource.IntNo),
                    new ReportParameter("NAME",dataSource.Member),
                    new ReportParameter("IPI_NAME_NO",dataSource.IPINameNo),
                    new ReportParameter("IPI_BASE_NO", dataSource.IPIBaseNo),
                    new ReportParameter("SERIAL_NO",dataSource.SerialNo.ToString()),
                    new ReportParameter("TOTAL", dataSource.TotalRoyalty.ToString()),
                    new ReportParameter("STR_PP", strPP),
                }; 
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
