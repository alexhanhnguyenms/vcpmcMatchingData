using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.MasterLists;

namespace Vcpmc.Mis.CompareText
{
    public partial class frmMain : Form
    {
        #region vari        
        MasterSourceViewModel CurrenObject = null;    
        OperationType Operation = OperationType.LoadExcel;
        string filepath = string.Empty;
        /// <summary>
        /// du lieu load tu excel
        /// </summary>
        List<MasterSourceViewModel> dataLoad = new List<MasterSourceViewModel>();
        /// <summary>
        /// Dữ liệu đang hiển thị
        /// </summary>
        List<MasterSourceViewModel> CurrentDataView = new List<MasterSourceViewModel>();
        int currentPage = 1;
        int totalPage = 0;        
        bool isConverCompositeToUnicode = true;      
        #endregion

        #region init
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
        }

        #endregion

        #region btn
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                //get link
                filepath = string.Empty;
                //
                isConverCompositeToUnicode = cheConvertCompositeToUnicode.Checked;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    //openFileDialog.Filter = "CSV Files|*.csv;";
                    //openFileDialog.Filter = "Excel Files|*.xlsx;";
                    openFileDialog.Filter = "Unicode Files|*.txt;";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filepath = openFileDialog.FileName;
                    }
                }
                if (filepath == string.Empty)
                {
                    MessageBox.Show("Please input file path");
                    return;
                }
                #region set backgroundWorker
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCompareText_Click(object sender, EventArgs e)
        {
            #region set backgroundWorker
            Operation = OperationType.LevenshteinDistance;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker1.RunWorkerAsync();
            #endregion
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if(dataLoad==null)
                {
                    return;
                }
                //get link
                filepath = string.Empty;
                using (SaveFileDialog openFileDialog = new SaveFileDialog())
                {
                    //openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    openFileDialog.Filter = "Excel Files|*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filepath = openFileDialog.FileName;
                    }
                }
                if (filepath == string.Empty)
                {
                    MessageBox.Show("Please input file path");
                    return;
                }
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnExport.Enabled = false;
                }));
                #region set backgroundWorker
                Operation = OperationType.ExportToExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region function
        private void LoadExcel()
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data...";
                }));               
                CurrentDataView = new List<MasterSourceViewModel>();
                DateTime starttime = DateTime.Now;
                //ExcelHelper excelHelper = new ExcelHelper();
                //dataLoad = excelHelper.ReadExcelImportPreClaimMatching(filepath);
                //dataLoad = CsvReadHelper.ReadCSVPreClaimMatching(filepath);
                dataLoad = null;
                GC.Collect();
                dataLoad = CsvReadHelper.ReadUnicodeMasterListSource(filepath);
                //excelHelper = null;
                DateTime endtime = DateTime.Now;
                DateTime starttimeConvert = DateTime.Now;
                DateTime endtimeConvert = DateTime.Now;
                if (dataLoad != null)
                {
                    #region Convert to unicode
                    if (isConverCompositeToUnicode)
                    {

                        ConvertFromComposite(dataLoad);
                        endtimeConvert = DateTime.Now;
                    }
                    #endregion
                    ConvertToUnSign(dataLoad);
                    currentPage = 1;
                    if (dataLoad.Count % Core.LimitDisplayDGV == 0)
                    {
                        totalPage = dataLoad.Count / Core.LimitDisplayDGV;
                    }
                    else
                    {
                        totalPage = dataLoad.Count / Core.LimitDisplayDGV + 1;
                    }
                    lbTotalPage.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage.Text = totalPage.ToString();
                    }));
                    txtPageCurrent.Invoke(new MethodInvoker(delegate
                    {
                        txtPageCurrent.Value = currentPage;
                    }));

                    dataLoad = dataLoad.OrderBy(p => p.SerialNo).ToList();
                    CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                    EnablePagging(currentPage, totalPage);
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = CurrentDataView;
                    }));
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Total time load: {(endtime - starttime).TotalSeconds}(s), total record(s): {dataLoad.Count}";
                        if (isConverCompositeToUnicode)
                        {
                            lbLoad.Text += $", total time convert to Unicode: {(endtimeConvert - starttimeConvert).TotalSeconds}(s)";
                        }
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Load data is finish";
                    }));
                    btnCompareText.Invoke(new MethodInvoker(delegate
                    {
                        btnCompareText.Enabled = true;
                    }));                    
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExport.Enabled = true;
                    }));
                }
                else
                {
                    dataLoad = new List<MasterSourceViewModel>();
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Load data is error!";
                    }));
                    btnCompareText.Invoke(new MethodInvoker(delegate
                    {
                        btnCompareText.Enabled = false;
                    }));                   
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExport.Enabled = false;
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Load data is finish";
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load data is error: {ex.ToString()}");
            }
        }
        private void EnablePagging(int currentPage, int totalPage)
        {
            txtPageCurrent.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent.ReadOnly = false;
            }));
            //<<
            if (currentPage > 1)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPAge.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPAge.Enabled = false;
                }));
            }
            //<
            if (currentPage > 1)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = false;
                }));
            }
            else
            {
                btnFirstPAge.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = true;
                }));
            }
        }       
        private void ConvertFromComposite(List<MasterSourceViewModel> dataLoad)
        {
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    //if(item.SerialNo ==9997)
                    //{
                    //    int a = 1;
                    //}
                    if (item.TITLE != string.Empty) item.TITLE = ConvertAllToUnicode.ConvertFromComposite(item.TITLE);
                    if (item.ARTIST != string.Empty) item.ARTIST = ConvertAllToUnicode.ConvertFromComposite(item.ARTIST);
                    if (item.ALBUM != string.Empty) item.ALBUM = ConvertAllToUnicode.ConvertFromComposite(item.ALBUM);
                    if (item.LABEL != string.Empty) item.LABEL = ConvertAllToUnicode.ConvertFromComposite(item.LABEL);
                    if (item.COMP_TITLE != string.Empty) item.COMP_TITLE = ConvertAllToUnicode.ConvertFromComposite(item.COMP_TITLE);
                    if (item.COMP_WRITERS != string.Empty) item.COMP_WRITERS = ConvertAllToUnicode.ConvertFromComposite(item.COMP_WRITERS);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ConvertToUnSign(List<MasterSourceViewModel> dataLoad)
        {
            string[] array = new string[] {"I.", "II.", "III.", "IV.", "V.","/",".",",",";","(",")","#","0","1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    if (item.TITLE != string.Empty) 
                    { 
                        item.TITLE2 = VnHelper.ConvertToUnSign(item.TITLE).ToUpper();
                        for (int i = 0; i < array.Length; i++)
                        {
                            item.TITLE2 = item.TITLE2.Replace(array[i], "");
                        }
                    }
                    if (item.COMP_TITLE != string.Empty) 
                    {
                        item.COMP_TITLE2 = VnHelper.ConvertToUnSign(item.COMP_TITLE).ToUpper(); 
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LevenshteinDistance()
        {
            try
            {
                if(dataLoad==null|| dataLoad.Count==0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    item.ScoreCompare = Vcpmc.Mis.Common.search.LevenshteinDistance.Compute(item.TITLE2, item.COMP_TITLE2);
                }
                dataLoad = dataLoad.OrderBy(p => p.ScoreCompare).ToList();
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                EnablePagging(currentPage, totalPage);
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                }));
            }
            catch (Exception)
            {
                //throw;
            }
        }
        #endregion

        #region phan trang        
        private void btnFirstPAge_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage = 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    //for (int i = 0; i < dgvMain.Rows.Count; i++)
                    //{
                    //    string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                    //    var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                    //    if (item != null)
                    //    {
                    //        if (item.IsSuccess)
                    //        {
                    //            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                    //        }
                    //    }
                    //}
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnFirstPAge.Enabled = false;
            }
        }
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage -= 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    //for (int i = 0; i < dgvMain.Rows.Count; i++)
                    //{
                    //    string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                    //    var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                    //    if (item != null)
                    //    {
                    //        if (item.IsSuccess)
                    //        {
                    //            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                    //        }
                    //    }
                    //}
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnPrevPage.Enabled = false;
            }
        }
        private void btnNxtPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage += 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    //for (int i = 0; i < dgvMain.Rows.Count; i++)
                    //{
                    //    string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                    //    var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                    //    if (item != null)
                    //    {
                    //        if (item.IsSuccess)
                    //        {
                    //            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                    //        }
                    //    }
                    //}
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnNxtPage.Enabled = false;
            }
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage = totalPage;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                    //for (int i = 0; i < dgvMain.Rows.Count; i++)
                    //{
                    //    string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                    //    var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                    //    if (item != null)
                    //    {
                    //        if (item.IsSuccess)
                    //        {
                    //            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                    //        }
                    //    }
                    //}
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            else
            {
                btnLastPage.Enabled = false;
            }

        }
        private void txtPageCurrent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dataLoad != null && dataLoad.Count > 0)
                {
                    if (((int)txtPageCurrent.Value) < 1)
                    {
                        txtPageCurrent.Value = 1;
                    }
                    else if (((int)txtPageCurrent.Value) > totalPage)
                    {
                        txtPageCurrent.Value = totalPage;
                    }
                    currentPage = (int)txtPageCurrent.Value;
                    CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                    dgvMain.Invoke(new MethodInvoker(delegate
                    {
                        dgvMain.DataSource = CurrentDataView;
                        //for (int i = 0; i < dgvMain.Rows.Count; i++)
                        //{
                        //    string id = (string)dgvMain.Rows[i].Cells["ID"].Value;
                        //    var item = CurrentDataView.Where(s => s.ID == id).FirstOrDefault();
                        //    if (item != null)
                        //    {
                        //        if (item.IsSuccess)
                        //        {
                        //            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                        //        }
                        //    }
                        //}
                    }));
                    EnablePagging(currentPage, totalPage);
                }
                else
                {
                    txtPageCurrent.ReadOnly = true;
                }
            }
        }
        #endregion            

        #region Filter
        int cboTypeChoise = -1;
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                #region cbo
                cboTypeChoise = cboType.SelectedIndex;
                #endregion

                #region set backgroundWorker
                Operation = OperationType.Filter;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filter: " + ex.ToString());
            }
        }
        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = false;
            }
        }
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            try
            {
                txtFind.Text = string.Empty;
                dgvMain.DataSource = CurrentDataView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clear filter: " + ex.ToString());
            }

        }

        private void FilterData()
        {
            try
            {
                List<MasterSourceViewModel> fill = new List<MasterSourceViewModel>();
                if (cboTypeChoise == 0)
                {
                    var query = dataLoad.Where(delegate (MasterSourceViewModel c)
                    {
                        if (c.TITLE2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    var query = dataLoad.Where(delegate (MasterSourceViewModel c)
                    {
                        if (c.COMP_TITLE2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    var query = dataLoad.Where(delegate (MasterSourceViewModel c)
                    {
                        if (c.C_Workcode.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    var query = dataLoad.Where(delegate (MasterSourceViewModel c)
                    {
                        if (c.CODE.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = fill;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Filter data, total record(s): {fill.Count}";
                }));
            }
            catch (Exception)
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
            }
        }
        #endregion   

        #region Eport
        private void ExportToExcel(string filePath)
        {
            try
            {
                string nameTest = "";
                if (dataLoad == null || dataLoad.Count == 0 || filepath == string.Empty)
                {
                    return;
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Export to excel...: {filePath}";
                }));
                string[] file = filePath.Split('.');
                string path = string.Empty;
                string name = string.Empty;
                string extension = string.Empty;
                if (file != null && file.Length > 1)
                {
                    extension = file[1];
                    int a = file[0].LastIndexOf("\\");
                    if (a > 2)
                    {
                        path = file[0].Substring(0, a);
                        name = file[0].Substring(a + 1, file[0].Length - a - 1);
                    }
                }
                if (path.Length > 0 && name.Length > 0 && extension.Length > 0)
                {
                    int totalFile = 0;
                    if (dataLoad.Count % Core.LimitDisplayExportExcel == 0)
                    {
                        totalFile = dataLoad.Count / Core.LimitDisplayExportExcel;
                    }
                    else
                    {
                        totalFile = dataLoad.Count / Core.LimitDisplayExportExcel + 1;
                    }
                    int serial = 0;
                    int index = 0;
                    while (index < dataLoad.Count)
                    {
                        nameTest += $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}"+",";
                        serial++;
                        var datax = dataLoad.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                        index += Core.LimitDisplayExportExcel;
                        bool check = WriteReportHelper.ExportMasterListSource(datax, $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}");
                        datax = null;
                        GC.Collect();
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            if (serial > totalFile) serial = totalFile;
                            float values = (float)serial / (float)totalFile * 100;
                            progressBarImport.Value = (int)values;
                            lbPercent.Text = $"{((int)values).ToString()}%";
                        }));
                    }
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Export to excel be finish: {nameTest}";
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnExport.Enabled = true;
                }));

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 100;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }          

        }
        #endregion

        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadExcel)
                {
                    LoadExcel();
                }               
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(filepath);
                }
                else if (Operation == OperationType.LevenshteinDistance)
                {
                    LevenshteinDistance();
                }
            }
            catch (Exception ex)
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));           
        }
        #endregion

        
    }
}
