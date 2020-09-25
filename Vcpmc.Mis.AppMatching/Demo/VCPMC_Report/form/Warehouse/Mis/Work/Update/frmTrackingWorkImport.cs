using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.Helper;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Work.Update
{
    public partial class frmTrackingWorkImport : Form
    {
        #region vari
        private WorkTrackingController _Controller;
        OperationType Operation = OperationType.LoadExcel;
        List<WorkTrackingViewModel> dataImport = new List<WorkTrackingViewModel>();
        List<WorkTrackingViewModel> dataImport1 = new List<WorkTrackingViewModel>();
        List<WorkTrackingViewModel> currentDataImView = new List<WorkTrackingViewModel>();
        List<WorkTrackingViewModel> currentDataImView1 = new List<WorkTrackingViewModel>();
        int year = 2020;
        int month = 2;
        int type = 0;
        string path = "";
        int currentPageIm = 1;
        int limitIm = 10000;//50000;
        int totalPageIm = 0;

        int currentPageIm1 = 1;
        int limitIm1 = 10000;//50000;
        int totalPageIm1 = 0;
        //int countPerSave = 1000;

        int countImportFromFile = 0;
        #endregion

        #region init
        public frmTrackingWorkImport(WorkTrackingController Controller)
        {
            InitializeComponent();
            _Controller = Controller;
        }

        private void frmTrackingWorkImport_Load(object sender, EventArgs e)
        {
            numYear.Value = DateTime.Now.Year;
            numMonth.Value = DateTime.Now.Month;
            cboType.SelectedIndex = 0;
            //
#if DEBUG
            txtPath.Text = @"D:\Solution\Source Code\Matching data\data mis\create\thang 3";
#endif
        }
        #endregion

        #region import
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                txtPath.Text = "";
                var filePath = string.Empty;
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {                        
                        filePath = fbd.SelectedPath;
                        txtPath.Text = filePath;

                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                year = (int)numYear.Value;
                month = (int)numMonth.Value;
                type = cboType.SelectedIndex;
                if (txtPath.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("input file path!");
                    return;
                }
                #region disable
                btnFirstPage.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                currentPageIm = 1;
                #endregion
                path = txtPath.Text.Trim();
                Operation = OperationType.LoadJson;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Save       
        private void btnSaveDataToDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataImport == null || dataImport.Count == 0)
                {
                    MessageBox.Show("No records to save!");
                    return;
                }
                if (MessageBox.Show(
                    $"Do you want to save data to database?{Environment.NewLine}Detail:{Environment.NewLine}" +
                    $"1.Year: {year}{Environment.NewLine}"+
                    $"2.Month: {month}{Environment.NewLine}"+
                    $"3.Type: {cboType.Text}{Environment.NewLine}"
                    
                    , "Confirm Save data to Database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save
                    Operation = OperationType.SaveDatabase;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region timer      
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadJson)
                {
                    LoadDataFreClaimJsonfile();
                }
                else if (Operation == OperationType.GoPage)
                {
                    if (dataImport == null || dataImport.Count == 0)
                    {
                        return;
                    }
                    if (currentPageIm < 1)
                    {
                        currentPageIm = 1;
                    }
                    if (currentPageIm > totalPageIm)
                    {
                        currentPageIm = totalPageIm;
                    }
                    GotoPage(currentPageIm, totalPageIm);
                }
                else if (Operation == OperationType.GoPageFailure)
                {
                    if (dataImport1 == null || dataImport1.Count == 0)
                    {
                        return;
                    }
                    if (currentPageIm1 < 1)
                    {
                        currentPageIm1 = 1;
                    }
                    if (currentPageIm1 > totalPageIm1)
                    {
                        currentPageIm1 = totalPageIm1;
                    }
                    GotoPage1(currentPageIm1, totalPageIm1);
                }
                else if (Operation == OperationType.SaveDatabase)
                {
                    SaveDataToDatabase();
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

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Invoke(new MethodInvoker(delegate
            {
                pcloader.Visible = false;
            }));
        }
        #endregion

        #region Function  
        private void LoadDataFreClaimJsonfile()
        {
            try
            {
                currentDataImView = new List<WorkTrackingViewModel>();
                dataImport = new List<WorkTrackingViewModel>();
                       
                dgvImport.Invoke(new MethodInvoker(delegate
                {
                    dgvImport.DataSource = currentDataImView;
                }));

                countImportFromFile = 0;
                DateTime startTime = DateTime.Now;
                totalPageIm = 0;

                DirectionNarrowDisable();               
                #region Read file
                string[] files = Directory.GetFiles(path);    
                int serical = 0;
                foreach (var item in files)
                {
                    string[] list_name = item.Split('\\');
                    string name = "";
                    if(list_name.Length > 0)
                    {
                        name = list_name.Last();
                    }   
                    string data = JsonHelper.LoadJson(item);
                    WorkRetrievalViewModel source = JsonHelper.JsonToWorkRetrieval(data);
                       
                    for (int i = 0; i < source._object.retrevalList.Count; i++)
                    {
                        serical++;
                        WorkTrackingViewModel preVM = new WorkTrackingViewModel();
                        preVM.SerialNo = serical;
                        preVM.WK_INT_NO = source._object.retrevalList[i].wkIntNo == null? string.Empty: VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(source._object.retrevalList[i].wkIntNo.ToUpper()));
                        preVM.TTL_ENG = source._object.retrevalList[i].engTitle == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(source._object.retrevalList[i].engTitle.ToUpper()));
                        preVM.ISWC_NO = source._object.retrevalList[i].iswcNo == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(source._object.retrevalList[i].iswcNo.ToUpper()));
                        preVM.ISRC = source._object.retrevalList[i].isrcNo == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(source._object.retrevalList[i].isrcNo.ToUpper()));
                        preVM.WRITER = source._object.retrevalList[i].composer == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(source._object.retrevalList[i].composer.ToUpper()));                       
                        for (int j = 0; j < source._object.retrevalList[i].artiste.Length; j++)
                        {
                            if (preVM.ARTIST.Length > 0)
                            {
                                preVM.ARTIST += ",";
                            }
                            preVM.ARTIST += source._object.retrevalList[i].artiste[j];
                        }
                        preVM.ARTIST = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(preVM.ARTIST));
                        // preVM.SOC_NAME = dataxxx2._object.retrevalList[i].wkIntNo;
                        preVM.Type = type;
                        preVM.Year = year;
                        preVM.MONTH = month;

                        #region bo tac gia tieng viet
                        //VIET HO HUU VIỆTHỒ HỮU
                        //NGUYEN THI SONG NGUYEN = NGUYENXETHISONG NGUYỄN
                        //NGUYEN THI SONG NGUYEN = NGUYENTHISONG NGUYEN
                        if (preVM.WRITER.Length>0)
                        {
                            //if (preVM.WRITER.Trim() == "VIET HO HUU VIỆTHỒ HỮU")
                            //{
                            //    int a = 1;
                            //}
                            //preVM.WRITER = VnHelper.ConvertToUnSign(preVM.WRITER);
                            string[] ilistWriter = preVM.WRITER.Trim().Split(',');
                            string[] arrayIwriter = null;
                            string s1 = "";
                            string s2 = "";
                            bool isCorrect = false;                          
                            int pos = 0;
                            preVM.WRITER = "";
                            string iWriterSub = "";
                            foreach (var iWriter in ilistWriter)
                            {
                                isCorrect = false;
                                arrayIwriter = iWriter.Trim().Split(' ');
                                if(arrayIwriter.Length > 1)
                                {

                                    string ho = arrayIwriter[arrayIwriter.Length - 1];                                   
                                    pos = iWriter.LastIndexOf(ho);
                                    iWriterSub = iWriter.Substring(0, pos);
                                    pos = iWriterSub.LastIndexOf(ho);
                                    if (pos > 0)
                                    {
                                        s1 = iWriter.Substring(0, pos + ho.Length);
                                        s2 = iWriter.Substring(pos + ho.Length, iWriter.Length- (pos + ho.Length));
                                        if(s1.Replace(" ","") == s2.Replace(" ", ""))
                                        {
                                            isCorrect = true;
                                        }
                                    }                                                               
                                }

                                if(isCorrect)
                                {
                                    preVM.WRITER += $",{s1}";
                                }
                                else
                                {
                                    preVM.WRITER += $",{iWriter}";
                                }
                            }
                            if (preVM.WRITER.Length > 1) preVM.WRITER = preVM.WRITER.Substring(1, preVM.WRITER.Length - 1);
                        }
                        #endregion

                        dataImport.Add(preVM);
                    }    
                }
                #endregion               
                if (dataImport != null && dataImport.Count > 0)
                {
                    countImportFromFile = dataImport.Count;
                    if (countImportFromFile % limitIm == 0)
                    {
                        totalPageIm = countImportFromFile / limitIm;
                    }
                    else
                    {
                        totalPageIm = countImportFromFile / limitIm + 1;
                    }
                    lbTotalPage.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage.Text = totalPageIm.ToString();
                    }));
                    GotoPage(currentPageIm, totalPageIm);

                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Total records load: {countImportFromFile}";
                    }));
                }
                else
                {                   
                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load data from json file be error!";
                    }));
                }
                DateTime endTime = DateTime.Now;
                lbInfoImport.Invoke(new MethodInvoker(delegate
                {
                    lbInfoImport.Text = $"Time load(second(s)): {(endTime - startTime).TotalSeconds.ToString("##0.##")}, ";
                    lbInfoImport.Text += $"total record(s): {countImportFromFile}, total page(s): {totalPageIm}, ";
                    lbInfoImport.Text += $"total records/page: {limitIm}";
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
        private async void SaveDataToDatabase()
        {
            try
            {
                currentDataImView1 = new List<WorkTrackingViewModel>();
                dataImport1 = new List<WorkTrackingViewModel>();
                dgvImportFailure.Invoke(new MethodInvoker(delegate
                {
                    dgvImportFailure.DataSource = currentDataImView1;
                }));

                WorkTrackingChangeListRequest request = new WorkTrackingChangeListRequest();
                request.Year = year;
                request.Month = month;
                request.Type = type;
                int countSend = 0;
                int countSended = 0;
                int countrun = 0;
                int totalrun = 0;
                int totalInserted = 0;
                int totalUpdateSucc = 0;
                DateTime startTime = DateTime.Now;
                dataImport1.Clear();
                //save

                if (totalPageIm % Core.countPerSave == 0)
                {
                    countSend = dataImport.Count / Core.countPerSave;
                }
                else
                {
                    countSend = dataImport.Count / Core.countPerSave + 1;
                }

                foreach (var item in dataImport)
                {                      
                    countrun++;
                    totalrun++;
                    #region create WorkTrackingCreateRequest
                    WorkTrackingCreateRequest pre = new WorkTrackingCreateRequest();
                    pre.WK_INT_NO = item.WK_INT_NO;
                    pre.TTL_ENG  = item.TTL_ENG;
                    pre.ISWC_NO  = item.ISWC_NO; 
                    pre.ISRC = item.ISRC;
                    pre.WRITER = item.WRITER;
                    pre.ARTIST = item.ARTIST;
                    pre.SOC_NAME = item.SOC_NAME;

                    pre.Type = item.Type;
                    pre.MONTH = item.MONTH;
                    pre.Year = item.Year;
                    request.Items.Add(pre);
                    #endregion
                    if (countrun == Core.countPerSave || totalrun == dataImport.Count)
                    {
                        //send
                        request.Total = countrun;
                        var changeData = await _Controller.ChangeList(request);
                        #region get info from reponse
                        if (changeData != null && changeData.Items != null)
                        {
                            //insert
                            totalInserted += changeData.Items.Where
                                (
                                p => p.Command == Utilities.Common.CommandType.Add
                                && p.Status == Utilities.Common.UpdateStatus.Successfull
                                ).Count();
                            //update
                            totalUpdateSucc += changeData.Items.Where
                                (
                                p => p.Command == Utilities.Common.CommandType.Update
                                && p.Status == Utilities.Common.UpdateStatus.Successfull
                                ).Count();
                            List<UpdateStatusViewModel> uvmL = changeData.Items.Where
                                (
                                p => p.Command == Utilities.Common.CommandType.Update
                                && p.Status == Utilities.Common.UpdateStatus.Failure
                                ).ToList();
                            foreach (var uvm in uvmL)
                            {
                                WorkTrackingViewModel x = dataImport.Where(p => p.WK_INT_NO == uvm.WorkCode).FirstOrDefault();
                                if (x != null)
                                {
                                    dataImport1.Add(x);
                                }
                            }
                            dataImport1 = dataImport1.OrderBy(p => p.SerialNo).ToList();
                        }
                        #endregion
                        //clear
                        request.Items.Clear();
                        countrun = 0;
                        //cap nhat phan tram
                        countSended++;
                        mainStatus.Invoke(new MethodInvoker(delegate
                        {
                            float values = (float)countSended / (float)countSend * 100;
                            progressBarImport.Value = (int)values;
                            progressBarImport.ToolTipText = progressBarImport.Value.ToString();
                        }));

                    }
                }
                //view
                int totalFailure = 0;
                if (dataImport1 != null && dataImport1.Count > 0)
                {
                    totalFailure = dataImport1.Count;
                    if (totalFailure % limitIm1 == 0)
                    {
                        totalPageIm1 = totalFailure / limitIm1;
                    }
                    else
                    {
                        totalPageIm1 = totalFailure / limitIm1 + 1;
                    }
                    lbTotalPage1.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalPage1.Text = totalPageIm1.ToString();
                    }));
                    GotoPage1(currentPageIm1, totalPageIm1);
                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Total records saveing be failure: {totalFailure}";
                    }));
                }
                else
                {                    
                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Saving is successfull!";
                    }));
                }
                mainStatus.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 100;
                    progressBarImport.ToolTipText = progressBarImport.Value.ToString();
                }));
                DateTime endTime = DateTime.Now;
                lbInfoImport1.Invoke(new MethodInvoker(delegate
                {
                    lbInfoImport1.Text = $"Time load(second(s)): {(endTime - startTime).TotalSeconds.ToString("##0.##")}, ";
                    lbInfoImport1.Text += $"total add new(s): {totalInserted}, total Update(s): {totalUpdateSucc}, ";
                    lbInfoImport1.Text += $"total Failure: {totalFailure}";
                }));
            }
            catch (Exception)
            {
                mainStatus.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "send request to server be error";
                }));
            }
        }
        #endregion

        #region View load       
        private void btnFirstPAge_Click(object sender, EventArgs e)
        {
            if (dataImport != null && dataImport.Count > 0)
            {
                currentPageIm = 1;
                #region set backgroundWorker
                Operation = OperationType.GoPage;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnFirstPage.Enabled = false;
            }
        }
        private void btnPrevPage_Click(object sender, EventArgs e)
        {

            if (dataImport != null && dataImport.Count > 0)
            {
                currentPageIm -= 1;
                #region set backgroundWorker
                Operation = OperationType.GoPage;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnPrevPage.Enabled = false;
            }
        }

        private void btnNxtPage_Click(object sender, EventArgs e)
        {

            if (dataImport != null && dataImport.Count > 0)
            {
                currentPageIm += 1;
                #region set backgroundWorker
                Operation = OperationType.GoPage;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnNxtPage.Enabled = false;
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {

            if (dataImport != null && dataImport.Count > 0)
            {
                currentPageIm = totalPageIm;
                #region set backgroundWorker
                Operation = OperationType.GoPage;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
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
                if (dataImport != null && dataImport.Count > 0)
                {
                    if (((int)txtPageCurrent.Value) < 1)
                    {
                        txtPageCurrent.Value = 1;
                    }
                    else if (((int)txtPageCurrent.Value) > totalPageIm)
                    {
                        txtPageCurrent.Value = totalPageIm;
                    }
                    currentPageIm = (int)txtPageCurrent.Value;
                    #region set backgroundWorker
                    Operation = OperationType.GoPage;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                    #endregion
                }
                else
                {
                    txtPageCurrent.ReadOnly = true;
                }
            }
        }
        #endregion

        #region view failure
        private void btnFirstPAge1_Click(object sender, EventArgs e)
        {
            if (dataImport1 != null && dataImport1.Count > 0)
            {
                currentPageIm1 = 1;
                #region set backgroundWorker
                Operation = OperationType.GoPageFailure;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnFirstPage1.Enabled = false;
            }
        }
        private void btnPrevPage1_Click(object sender, EventArgs e)
        {

            if (dataImport1 != null && dataImport1.Count > 0)
            {
                currentPageIm1 -= 1;
                #region set backgroundWorker
                Operation = OperationType.GoPageFailure;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnPrevPage1.Enabled = false;
            }
        }

        private void btnNxtPage1_Click(object sender, EventArgs e)
        {

            if (dataImport1 != null && dataImport1.Count > 0)
            {
                currentPageIm1 += 1;
                #region set backgroundWorker
                Operation = OperationType.GoPageFailure;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnNxtPage1.Enabled = false;
            }
        }

        private void btnLastPage1_Click(object sender, EventArgs e)
        {

            if (dataImport1 != null && dataImport1.Count > 0)
            {
                currentPageIm = totalPageIm;
                #region set backgroundWorker
                Operation = OperationType.GoPageFailure;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
                #endregion
            }
            else
            {
                btnLastPage1.Enabled = false;
            }

        }
        private void txtPageCurrent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dataImport1 != null && dataImport1.Count > 0)
                {
                    if (((int)txtPageCurrent1.Value) < 1)
                    {
                        txtPageCurrent1.Value = 1;
                    }
                    else if (((int)txtPageCurrent1.Value) > totalPageIm1)
                    {
                        txtPageCurrent1.Value = totalPageIm1;
                    }
                    currentPageIm = (int)txtPageCurrent.Value;
                    #region set backgroundWorker
                    Operation = OperationType.GoPageFailure;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                    #endregion
                }
                else
                {
                    txtPageCurrent1.ReadOnly = true;
                }
            }
        }
        #endregion

        #region view import
        private void GotoPage(int currentPage, int totalPage)
        {
            try
            {
                if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (currentPage > totalPage)
                {
                    currentPage = totalPage;
                }
                currentDataImView = dataImport
                       //.Where(p=>true)
                       .Skip((currentPage - 1) * limitIm)
                       .Take(limitIm).ToList();
                dgvImport.Invoke(new MethodInvoker(delegate
                {
                    dgvImport.DataSource = currentDataImView;
                }));
                txtPageCurrent.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent.Value = currentPage;
                }));
                EnablePagging(currentPage, totalPage);
            }
            catch (Exception)
            {


            }
        }
        private void DirectionNarrowDisable()
        {
            btnFirstPage.Invoke(new MethodInvoker(delegate
            {
                btnFirstPage.Enabled = false;
            }));
            btnPrevPage.Invoke(new MethodInvoker(delegate
            {
                btnPrevPage.Enabled = false;
            }));
            btnNxtPage.Invoke(new MethodInvoker(delegate
            {
                btnNxtPage.Enabled = false;
            }));
            btnLastPage.Invoke(new MethodInvoker(delegate
            {
                btnLastPage.Enabled = false;
            }));
            txtPageCurrent.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent.ReadOnly = true;
            }));
            mainStatus.Invoke(new MethodInvoker(delegate
            {
                lbInfo.Text = $"Search data from serve, total record(s): no reponse";
            }));
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
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPage.Enabled = false;
                }));
            }
            //<
            if (currentPage > 1)
            {
                btnPrevPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = true;
                }));
            }
            else
            {
                btnPrevPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnNxtPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = true;
                }));
            }
            else
            {
                btnNxtPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnLastPage.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = false;
                }));
            }
            else
            {
                btnLastPage.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = true;
                }));
            }
        }
        #endregion

        #region view import failure
        private void GotoPage1(int currentPage, int totalPage)
        {
            try
            {
                if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (currentPage > totalPage)
                {
                    currentPage = totalPage;
                }
                currentDataImView1 = dataImport1
                       //.Where(p=>true)
                       .Skip((currentPage - 1) * limitIm1)
                       .Take(limitIm1).ToList();
                dgvImportFailure.Invoke(new MethodInvoker(delegate
                {
                    dgvImportFailure.DataSource = currentDataImView1;
                }));
                txtPageCurrent1.Invoke(new MethodInvoker(delegate
                {
                    txtPageCurrent1.Value = currentPage;
                }));
                EnablePagging1(currentPage, totalPage);
            }
            catch (Exception)
            {


            }
        }
        private void DirectionNarrowDisable1()
        {
            btnFirstPage1.Invoke(new MethodInvoker(delegate
            {
                btnFirstPage1.Enabled = false;
            }));
            btnPrevPage1.Invoke(new MethodInvoker(delegate
            {
                btnPrevPage1.Enabled = false;
            }));
            btnNxtPage1.Invoke(new MethodInvoker(delegate
            {
                btnNxtPage1.Enabled = false;
            }));
            btnLastPage1.Invoke(new MethodInvoker(delegate
            {
                btnLastPage1.Enabled = false;
            }));
            txtPageCurrent1.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent1.ReadOnly = true;
            }));
            mainStatus.Invoke(new MethodInvoker(delegate
            {
                lbInfo.Text = $"Search data from serve, total record(s): no reponse";
            }));
        }
        private void EnablePagging1(int currentPage, int totalPage)
        {
            txtPageCurrent1.Invoke(new MethodInvoker(delegate
            {
                txtPageCurrent1.ReadOnly = false;
            }));
            //<<
            if (currentPage > 1)
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPage1.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnFirstPage1.Enabled = false;
                }));
            }
            //<
            if (currentPage > 1)
            {
                btnPrevPage1.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage1.Enabled = true;
                }));
            }
            else
            {
                btnPrevPage1.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage1.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnNxtPage1.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage1.Enabled = true;
                }));
            }
            else
            {
                btnNxtPage1.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage1.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnLastPage1.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage1.Enabled = false;
                }));
            }
            else
            {
                btnLastPage1.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage1.Enabled = true;
                }));
            }
        }
        #endregion

        #region KeyPress
        private void numYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                year = (int)numYear.Value;
                if (dataImport != null)
                {
                    foreach (var item in dataImport)
                    {
                        item.MONTH = month;
                        item.Year = year;
                        item.Type = type;
                    }
                    currentPageIm = 1;
                    GotoPage(currentPageIm, totalPageIm);
                }
            }
        }

        private void numMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                month = (int)numMonth.Value;
                if (dataImport != null)
                {
                    foreach (var item in dataImport)
                    {
                        item.MONTH = month;
                        item.Year = year;
                        item.Type = type;
                    }
                    currentPageIm = 1;
                    GotoPage(currentPageIm, totalPageIm);
                }
            }
        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = cboType.SelectedIndex;
        }
        private void btnChangeTime_Click(object sender, EventArgs e)
        {
            try
            {
                year = (int)numYear.Value;
                month = (int)numMonth.Value;
                type = cboType.SelectedIndex;
                if (dataImport != null)
                {
                    foreach (var item in dataImport)
                    {
                        item.MONTH = month;
                        item.Year = year;
                        item.Type = type;
                    }
                    currentPageIm = 1;
                    GotoPage(currentPageIm, totalPageIm);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion

       
    }
}
