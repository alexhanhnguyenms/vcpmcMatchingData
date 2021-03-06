﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.AppMatching.form.Warehouse.Mis.Monopoly.Update
{
    public partial class frmMonopolyImport : Form
    {
        #region vari
        private MonopolyController _Controller;
        OperationType Operation = OperationType.LoadExcel;
        List<MonopolyViewModel> dataImport = new List<MonopolyViewModel>();
        List<MonopolyViewModel> dataImport1 = new List<MonopolyViewModel>();
        List<MonopolyViewModel> currentDataImView = new List<MonopolyViewModel>();
        List<MonopolyViewModel> currentDataImView1 = new List<MonopolyViewModel>();
        int group = 0;
        
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
        public frmMonopolyImport(MonopolyController Controller, int group)
        {
            InitializeComponent();
            _Controller = Controller;
            cboGroup.SelectedIndex = group;
        }

        private void frmMonopolyImport_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region import
        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            group = cboGroup.SelectedIndex;
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                //txtPath.Text = "";
                //var filePath = string.Empty;
                //using (var fbd = new FolderBrowserDialog())
                //{
                //    DialogResult result = fbd.ShowDialog();

                //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                //    {
                //        filePath = fbd.SelectedPath;
                //        txtPath.Text = filePath;

                //    }
                //}
                txtPath.Text = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
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
                group = cboGroup.SelectedIndex;

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
                Operation = OperationType.LoadExcel;
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
                    $"1.Group: {cboGroup.Text}{Environment.NewLine}" 
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
                if (Operation == OperationType.LoadExcel)
                {
                    LoadDataMonopoly();
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
        private void LoadDataMonopoly()
        {
            try
            {
                currentDataImView = new List<MonopolyViewModel>();
                dataImport = new List<MonopolyViewModel>();
                dgvImport.Invoke(new MethodInvoker(delegate
                {
                    dgvImport.DataSource = currentDataImView;
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnImport.Enabled = false;
                }));
                
                countImportFromFile = 0;
                DateTime startTime = DateTime.Now;
                totalPageIm = 0;

                DirectionNarrowDisable();
                
                #region Read file
                ExcelHelper excelHelper = new ExcelHelper();               
                dataImport = excelHelper.ReadExcelImportMonopoly(group,txtPath.Text);                
                #endregion
                //dataImport = JsonHelper.JsonToPreclaim(path, year, month);
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
                        lbInfo.Text = "Load data from excel file be error!";
                    }));
                }
                DateTime endTime = DateTime.Now;
                lbInfoImport.Invoke(new MethodInvoker(delegate
                {
                    lbInfoImport.Text = $"Time load(second(s)): {(endTime - startTime).TotalSeconds.ToString("##0.##")}, ";
                    lbInfoImport.Text += $"total record(s): {countImportFromFile}, total page(s): {totalPageIm}, ";
                    lbInfoImport.Text += $"total records/page: {limitIm}";
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnImport.Enabled = true;
                }));
            }
            catch (Exception)
            {
                if(toolMain!=null && !toolMain.IsDisposed)
                {
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnImport.Enabled = true;
                    }));
                }                
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
                currentDataImView1 = new List<MonopolyViewModel>();
                dataImport1 = new List<MonopolyViewModel>();
                dgvImportFailure.Invoke(new MethodInvoker(delegate
                {
                    dgvImportFailure.DataSource = currentDataImView1;
                }));
                MonopolyChangeListRequest request = new MonopolyChangeListRequest();
                request.Group = group;                
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
                    MonopolyCreateRequest updateObj = new MonopolyCreateRequest();
                    updateObj.Group = item.Group;
                    updateObj.CodeOld = item.CodeOld;
                    updateObj.CodeNew = item.CodeNew;
                    updateObj.Name = item.Name;
                    updateObj.Name2 = item.Name2;
                    updateObj.NameType = item.NameType;
                    updateObj.Own = item.Own;
                    updateObj.Own2 = item.Own2;
                    updateObj.NoteMono = item.NoteMono;

                    updateObj.StartTime = item.StartTime;
                    updateObj.EndTime = item.EndTime;
                    updateObj.UpdateTime = item.UpdateTime;
                    updateObj.ReceiveTime = item.ReceiveTime;
                    updateObj.Note2 = item.Note2;//thời điểm nhận thông tin
                    updateObj.Note3 = item.Note3;//Ghi chú nhận tác phẩm

                    updateObj.Tone = item.Tone;
                    updateObj.Web = item.Web;
                    updateObj.Performances = item.Performances;
                    updateObj.PerformancesHCM = item.PerformancesHCM;
                    updateObj.Cddvd = item.Cddvd;
                    updateObj.Kok = item.Kok;
                    updateObj.Broadcasting = item.Broadcasting;
                    updateObj.Entertaiment = item.Entertaiment;
                    updateObj.Film = item.Film;
                    updateObj.Advertisement = item.Advertisement;
                    updateObj.PubMusicBook = item.PubMusicBook;
                    updateObj.Youtube = item.Youtube;
                    updateObj.Other = item.Other;

                    updateObj.IsExpired = item.IsExpired;                                    
                   
                    request.Items.Add(updateObj);
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
                                MonopolyViewModel x = dataImport.Where(p => p.CodeNew == uvm.WorkCode).FirstOrDefault();
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
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage.Enabled = false;
                }));
            }
            else
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
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
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage1.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnPrevPage1.Enabled = false;
                }));
            }
            //>
            if (currentPage < totalPage)
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage1.Enabled = true;
                }));
            }
            else
            {
                btnFirstPage.Invoke(new MethodInvoker(delegate
                {
                    btnNxtPage1.Enabled = false;
                }));
            }
            //>>
            if (currentPage == totalPage)
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage1.Enabled = false;
                }));
            }
            else
            {
                btnFirstPage1.Invoke(new MethodInvoker(delegate
                {
                    btnLastPage1.Enabled = true;
                }));
            }
        }
        #endregion

       
    }
}
