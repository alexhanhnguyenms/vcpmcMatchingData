//using Org.BouncyCastle.Operators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared.Mis.Works;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmWorkImport : Form
    {
        WorkController controller;
        string filepath = string.Empty;
        OperationType Operation = OperationType.LoadExcel;
        List<WorkTXT> dataLoad = new List<WorkTXT>();
        List<string> listErr = new List<string>();
        List<WorkTXT> CurrentDataView = new List<WorkTXT>();
        //bool isStop = false;
        int currentPage = 1;
        int totalPage = 0;
        int reportType = 0;
        #region load
        public frmWorkImport(WorkController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }
        private void frmWorkImport_Load(object sender, EventArgs e)
        {
            cboReportType.SelectedIndex = 0;
            cboType.SelectedIndex = 1;
            reportType = 0;
        }
        private void frmWorkImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(dataLoad!=null)
                {
                    dataLoad.Clear();
                    GC.Collect();
                }
                if (listErr != null)
                {
                    listErr.Clear();
                    GC.Collect();
                }
            }
            catch (Exception)
            {

                //int a = 1;
            }
        }
        #endregion

        #region  btn
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                reportType = cboReportType.SelectedIndex;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                //get link
                filepath = string.Empty;     
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
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnSysToWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    MessageBox.Show("Data is empty, so not sync to Work");
                    return;
                }
                DialogResult dr = MessageBox.Show("Are you sure to sync data to Work?", "SYNC Confirm", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Operation = OperationType.SysnData;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception)
            {
                //int a = 1;
            }
        }

        #endregion

        #region proccess
        private void LoadExcel()
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data...";
                }));
                //isStop = false;
                CurrentDataView = new List<WorkTXT>();
                DateTime starttime = DateTime.Now;               
                dataLoad = null;
                listErr.Clear();
                GC.Collect();
                WorkTXTRead workTXTRead = null;
                if (reportType == 0)
                {
                    //mau tong hop thong thuong
                    workTXTRead  = CsvReadHelper.ReadUnicodeWorkTXTComon(filepath);
                }
                else if (reportType == 1)
                {
                    //mau an do nuoc ngoai(mau cu)
                    //workTXTRead = CsvReadHelper.ReadUnicodeWorkTXT(filepath);
                    //mau moi
                    workTXTRead = CsvReadHelper.ReadUnicodeWorkTXT3(filepath);
                } 
                else
                {
                    //mau tac gia tieng viet, an do dua sang
                    workTXTRead = CsvReadHelper.ReadUnicodeWorkTXT2(filepath);
                }
                if(workTXTRead!=null)
                {
                    dataLoad = workTXTRead.SuccessList;
                    listErr = workTXTRead.FailList;                   
                    string dataerr = "";
                    for (int i = 0; i < listErr.Count; i++)
                    {
                        dataerr += $"{listErr[i]}{Environment.NewLine}";
                    }
                    rbFailList.Invoke(new MethodInvoker(delegate
                    {
                        rbFailList.Text = dataerr;
                    }));
                    lbFail.Invoke(new MethodInvoker(delegate
                    {
                        lbFail.Text = listErr.Count.ToString();
                    }));
                }
                DateTime endtime = DateTime.Now;
                DateTime starttimeConvert = DateTime.Now;
                DateTime endtimeConvert = DateTime.Now;
                if (dataLoad != null)
                {      
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
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Load data is finish";
                    }));
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = true;
                    }));
                    
                }
                else
                {
                    dataLoad = new List<WorkTXT>();
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Load data is error!";
                    }));
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = false;
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
        #endregion

        #region phan trang       
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
        private void btnFirstPAge_Click(object sender, EventArgs e)
        {
            if (dataLoad != null && dataLoad.Count > 0)
            {
                currentPage = 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
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
                backgroundWorker.RunWorkerAsync();
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
                dgvMain.DataSource = dataLoad;
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
                //
                List<WorkTXT> fill = new List<WorkTXT>();
                if (cboTypeChoise == 0)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Title).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.WK_INT_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.TitleMatching).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.TTL_ENG.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Writer).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.WRITER.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WriterMatching).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.ARTIST.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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

        #region dgvMain
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMain.CurrentCell.ColumnIndex == 0)
                {
                    if (dgvMain.CurrentCell.Value == null || (bool)dgvMain.CurrentCell.Value == false)
                    {
                        dgvMain.CurrentCell.Value = true;
                    }
                    else
                    {
                        dgvMain.CurrentCell.Value = false;
                    }
                }
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
                else if (Operation == OperationType.SysnData)
                {
                    SysnData();
                }
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
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

        #region sync
        private async void SysnData()
        {
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                int totalSuccess = 0;
                int total = dataLoad.Count;
                
                if (total == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Proccessing sync is finish";
                        lbOperation.Text = "Data is empty, so not sync to work";
                    }));
                    return;
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Sync...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Proccessing sync...";
                }));                
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = false;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    btnLoad.Enabled = false;
                }));               

                DateTime startTime = DateTime.Now;

                WorkChangeListRequest request = new WorkChangeListRequest();
                string status;
                string[] writer;
                string[] writer1;
                string[] writer2;
                for (int i = 0; i < dataLoad.Count; i++)
                {   
                    if(reportType ==2)
                    {
                        #region Add
                        if (!request.Items.Where(p => p.WK_INT_NO == dataLoad[i].WK_INT_NO.ToUpper()).Any())
                        {
                            WorkCreateRequest itmUpdate = new WorkCreateRequest();
                            itmUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(dataLoad[i].WK_INT_NO.ToUpper());
                            itmUpdate.TTL_ENG = VnHelper.ConvertToUnSign(dataLoad[i].TTL_ENG.Trim().ToUpper());
                            itmUpdate.WK_STATUS = dataLoad[i].STATUS;                                                                       
                            itmUpdate.InterestedParties.Add(new Shared.Mis.Works.InterestedParty
                            {
                                No = 1,
                                IP_INT_NO = dataLoad[i].InternalNo,
                                IP_NAME = dataLoad[i].WRITER,
                                IP_WK_ROLE = dataLoad[i].WK_ROLE,

                                //TODO 2020-10-02
                                //WK_STATUS = dataLoad[i].STATUS,

                                PER_OWN_SHR = dataLoad[i].PER_OWN_SHR,
                                PER_COL_SHR = dataLoad[i].PER_OWN_SHR,

                                MEC_OWN_SHR = dataLoad[i].MEC_COL_SHR,
                                MEC_COL_SHR = dataLoad[i].MEC_COL_SHR,

                                SP_SHR = dataLoad[i].PER_OWN_SHR,
                                TOTAL_MEC_SHR = dataLoad[i].MEC_COL_SHR,

                                SYN_OWN_SHR = 0,
                                SYN_COL_SHR = 0,
                                Society = dataLoad[i].Society,
                                CountUpdate = 1,
                                LastUpdateAt = DateTime.Now,
                                LastChoiseAt = DateTime.Now,
                                IP_NUMBER = dataLoad[i].IpNumber,
                                IP_NAME_LOCAL = dataLoad[i].WRITER_LOCAL,
                                IP_NAMETYPE = dataLoad[i].IP_NAME_TYPE,
                            });
                            itmUpdate.WRITER = dataLoad[i].WRITER;
                            itmUpdate.ARTIST = dataLoad[i].ARTIST;
                            itmUpdate.SOC_NAME = dataLoad[i].SOC_NAME;
                            itmUpdate.ISWC_NO = dataLoad[i].ISWC_NO;
                            itmUpdate.ISRC = dataLoad[i].ISRC;                            
                            request.Items.Add(itmUpdate);
                        }
                        else
                        {
                            var item = request.Items.Where(p => p.WK_INT_NO == dataLoad[i].WK_INT_NO).FirstOrDefault();
                            if(item!=null)
                            {
                                if(!item.InterestedParties.Where(p=>p.IP_INT_NO == dataLoad[i].WK_INT_NO).Any())
                                {
                                    item.InterestedParties.Add(new Shared.Mis.Works.InterestedParty
                                    {
                                        No = 1,
                                        IP_INT_NO = dataLoad[i].InternalNo,
                                        IP_NAME = dataLoad[i].WRITER,
                                        IP_WK_ROLE = dataLoad[i].WK_ROLE,

                                        //TODO 2020-10-02
                                        //WK_STATUS = dataLoad[i].STATUS,

                                        PER_OWN_SHR = dataLoad[i].PER_OWN_SHR,
                                        PER_COL_SHR = dataLoad[i].PER_OWN_SHR,

                                        MEC_OWN_SHR = dataLoad[i].MEC_COL_SHR,
                                        MEC_COL_SHR = dataLoad[i].MEC_COL_SHR,

                                        SP_SHR = dataLoad[i].PER_OWN_SHR,
                                        TOTAL_MEC_SHR = dataLoad[i].MEC_COL_SHR,

                                        SYN_OWN_SHR = 0,
                                        SYN_COL_SHR = 0,
                                        Society = dataLoad[i].Society,
                                        CountUpdate = 1,
                                        LastUpdateAt = DateTime.Now,
                                        LastChoiseAt = DateTime.Now,
                                        IP_NUMBER = dataLoad[i].IpNumber,
                                        IP_NAME_LOCAL = dataLoad[i].WRITER_LOCAL,
                                        IP_NAMETYPE = dataLoad[i].IP_NAME_TYPE,
                                    });
                                }
                                else
                                {
                                    //int a = 1;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Add
                        if (!request.Items.Where(p => p.WK_INT_NO == dataLoad[i].WK_INT_NO.ToUpper()).Any())
                        {
                            WorkCreateRequest itmUpdate = new WorkCreateRequest();
                            itmUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(dataLoad[i].WK_INT_NO.ToUpper());
                            itmUpdate.TTL_ENG = VnHelper.ConvertToUnSign(dataLoad[i].TTL_ENG.Trim().ToUpper());
                            if (dataLoad[i].WK_INT_NO == "17615461")
                            {
                                //int a = 1;
                            }
                            if (dataLoad[i].STATUS != string.Empty)
                            {
                                status = dataLoad[i].STATUS;
                            }
                            else
                            {
                                status = "COMPLETE";
                            }
                            //HUNSAKER JASON BRADLEY,MARCKS WILLIAM DANIEL,WILCOX JAMES STEVEN,WOOD JEREMY M
                            writer = dataLoad[i].WRITER.Split(',');
                            //nhac
                            writer1 = dataLoad[i].WRITER2.Split(',');
                            //loi
                            writer2 = dataLoad[i].WRITER3.Split(',');
                            string role;
                            for (int k = 0; k < writer.Length; k++)
                            {
                                if (dataLoad[i].WRITER2.Length > 0 || dataLoad[i].WRITER3.Length > 0)
                                {
                                    //loi trong=> CA
                                    if (dataLoad[i].WRITER3.Length == 0)
                                    {
                                        role = "CA";
                                    }
                                    //nam trong loi
                                    else if (writer2.Contains(writer[k].Trim()))
                                    {
                                        role = "A";
                                    }
                                    else
                                    {
                                        //nhac
                                        role = "C";
                                    }
                                }
                                else
                                {
                                    role = "CA";
                                }
                                if (writer[k] != string.Empty)
                                {
                                    itmUpdate.InterestedParties.Add(new Shared.Mis.Works.InterestedParty
                                    {
                                        No = 1,
                                        IP_INT_NO = string.Empty,
                                        IP_NAME = writer[k].Trim(),
                                        IP_WK_ROLE = role,

                                        //TODO 2020-10-02
                                        //WK_STATUS = status,

                                        PER_OWN_SHR = 0,
                                        PER_COL_SHR = 0,

                                        MEC_OWN_SHR = 0,
                                        MEC_COL_SHR = 0,

                                        SP_SHR = 0,
                                        TOTAL_MEC_SHR = 0,

                                        SYN_OWN_SHR = 0,
                                        SYN_COL_SHR = 0,
                                        CountUpdate = 1,
                                        LastUpdateAt = DateTime.Now,
                                        LastChoiseAt = DateTime.Now,
                                        IP_NUMBER = dataLoad[i].IpNumber,
                                        IP_NAME_LOCAL = dataLoad[i].WRITER_LOCAL,
                                        IP_NAMETYPE = dataLoad[i].IP_NAME_TYPE,
                                    });
                                }
                            }
                            itmUpdate.WRITER = dataLoad[i].WRITER;
                            itmUpdate.ARTIST = dataLoad[i].ARTIST;
                            itmUpdate.SOC_NAME = dataLoad[i].SOC_NAME;
                            itmUpdate.ISWC_NO = dataLoad[i].ISWC_NO;
                            itmUpdate.ISRC = dataLoad[i].ISRC;
                            itmUpdate.WK_STATUS = "COMPLETE";
                            request.Items.Add(itmUpdate);

                        }
                        else
                        {
                            //int a = 1;
                        }
                        #endregion
                    }

                    #region Update                   
                    if (request.Items.Count >= Core.LimitRequestUpdate || i == dataLoad.Count - 1)
                    {
                        UpdateStatusViewModelList reponse = null;
                        //request.Items
                        reponse = await controller.ChangeList(request);
                        if (reponse == null)
                        {
                            reponse = new UpdateStatusViewModelList
                            {
                                Items = new List<UpdateStatusViewModel>()
                            };
                        }
                        request.Items.Clear();
                        #region UI
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            float values = (float)(i + 1) / (float)dataLoad.Count * 100;
                            progressBarImport.Value = (int)values;
                            lbPercent.Text = $"{((int)values).ToString()}%";
                        }));
                        totalSuccess += reponse.Items.Where(p => p.Status == Utilities.Common.UpdateStatus.Successfull).ToList().Count;
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = $"Sync..., total sync success/total: {totalSuccess}/{total}";
                        }));
                        #endregion
                    }
                    #endregion

                }
                #region update Ui when finish              
                
                if(request.Items.Count>0)
                {
                    //int a = 1;
                }
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = true;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    btnLoad.Enabled = true;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"sync tracking work to work be finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                    lbInfo.Text += $", total sync success/total: {totalSuccess}/{total}";
                }));

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Sync is finish";
                }));
                #endregion
            }
            catch (Exception)
            {
                if (statusMain != null && !statusMain.IsDisposed)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        btnLoad.Enabled = true;
                    }));
                }
                if (btnSysToWork != null && !btnSysToWork.IsDisposed)
                {
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = true;
                    }));
                }               
                if (lbOperation != null && !lbInfo.IsDisposed)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = $"Sync tracking work to work be failure";
                    }));
                }
            }
        }
        #endregion       
    }
}
