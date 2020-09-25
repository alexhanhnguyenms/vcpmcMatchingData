using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.contract.report
{
    public partial class frmContracPrinter : Form
    {
        //public List<BhDistributionViewModel> dataSource = new List<BhDistributionViewModel>();
        public int ReportTemplate { get; internal set; } = 1;
        public frmContracPrinter()
        {
            InitializeComponent();
        }

        private void frmContracDocumentPrinter_Load(object sender, EventArgs e)
        {
            try
            {
                //var reportDatSource = new ReportDataSource("DataSet1", dataSource);               
                string type = "";
                if (ReportTemplate != 0)
                {
                    type = ReportTemplate.ToString();
                }
                ReportParameter[] reportParameters = new ReportParameter[] {                    
                    new ReportParameter("strMember", ""),
                    new ReportParameter("strBh",""),                  
                };
                //E:\Solution\Source Code\VCPMC_masterListDataReport\TH_solution\Demo\VCPMC_Report\bin\Debug\report\template\DistributionReport.rdlc
                string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\" + $"ContractReport.rdlc";               
                reportViewer1.LocalReport.ReportPath = path;
                reportViewer1.LocalReport.DataSources.Clear();
                //reportViewer1.LocalReport.DataSources.Add(reportDatSource);
                //reportViewer1.LocalReport.SetParameters(reportParameters);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
