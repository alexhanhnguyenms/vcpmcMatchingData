using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.MasterList;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Youtube;
using Vcpmc.Mis.AppMatching.Security.MasterList;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Youtube;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.detect;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared.masterlist;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.MasterLists;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.AppMatching.form.masterlist
{
    public partial class frmMasterList : Form
    {
        #region vari       
        private bool closePending;
        //MasterListViewModel CurrenObject = null;
        MasterListController Controller;
        MasterListApiClient ApiClient;
        //request matching
        GetMasterListPagingRequest request = new GetMasterListPagingRequest();
        /// <summary>
        /// Dữ liệu trả về từ matching
        /// </summary>
        ApiResult<PagedResult<MasterListViewModel>> dataReponse = new ApiResult<PagedResult<MasterListViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        string filepath = string.Empty;
        /// <summary>
        /// du lieu load tu excel
        /// </summary>
        List<MasterListViewModel> dataLoad = new List<MasterListViewModel>();
        /// <summary>
        /// Dữ liệu đang hiển thị
        /// </summary>
        List<MasterListViewModel> CurrentDataView = new List<MasterListViewModel>();
        int currentPage = 1;
        int totalPage = 0;
        int year = -1;
        int MONTH = -1;       
        bool isConverCompositeToUnicode = true;
        bool isStop = false;

        #endregion

        #region deteact languageby API
        readonly int nth = 100;
        Thread[] threads;
        List<MasterListViewModel>[] dataNeadDetectLan;
        LanguageDetect[] languageDetect;
        bool[] threadFinish;
        #endregion

        #region init
        public frmMasterList()
        {
            InitializeComponent();
            ApiClient = new MasterListApiClient(Core.Client);
            Controller = new MasterListController(ApiClient);
            threads = new Thread[nth];
            dataNeadDetectLan = new List<MasterListViewModel>[nth];
            languageDetect = new LanguageDetect[nth];
            threadFinish = new bool[nth];
            for (int i = 0; i < dataNeadDetectLan.Length; i++)
            {
                dataNeadDetectLan[i] = new List<MasterListViewModel>();
                languageDetect[i] = new LanguageDetect("0a2e6acb6fec06a29930c79c6dccbd4d");
                threadFinish[i] =false;
            }
        }

        private void frmMasterList_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            cboMonths.SelectedIndex = DateTime.Now.Month - 1;
            numYear.Value = DateTime.Now.Year;
        }

        private void frmMasterList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                closePending = true;
                backgroundWorker1.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;   // or this.Hide()
                return;
            }
            //base.OnFormClosing(e);
            if(dataLoad!=null)
            {
                dataLoad.Clear();
            }            
            dataLoad = null;
            GC.Collect();
        }
        #endregion

        #region LoadData
        private void LoadExcel()
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data...";
                }));
                isStop = false;
                CurrentDataView = new List<MasterListViewModel>();
                DateTime starttime = DateTime.Now;
                ExcelHelper excelHelper = new ExcelHelper();
                //dataLoad = excelHelper.ReadExcelImportPreClaimMatching(filepath);
                //dataLoad = CsvReadHelper.ReadCSVPreClaimMatching(filepath);
                dataLoad = null;
                GC.Collect();
                dataLoad = CsvReadHelper.ReadCsvMasterList(filepath, year,MONTH);
                //currentDataSource = MasterList.YoutubeFileItems;
                //dataLoad = CsvReadHelper.ReadUnicodePreClaimMatching(filepath);
                excelHelper = null;
                DateTime endtime = DateTime.Now;
                DateTime starttimeConvert = DateTime.Now;
                DateTime endtimeConvert = DateTime.Now;
                if (dataLoad != null)
                {
                    #region Convert to unicode
                    if (isConverCompositeToUnicode)
                    {

                        ConvertToUnicode(dataLoad);
                        endtimeConvert = DateTime.Now;
                    }
                    #endregion
                    //ConvertToUnSign(dataLoad);
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
                    btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectByAlgorithm.Enabled = true;
                    }));
                   
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExport.Enabled = true;
                    }));
                }
                else
                {
                    dataLoad = new List<MasterListViewModel>();
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Load data is error!";
                    }));
                    btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectByAlgorithm.Enabled = false;
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
        private async void Proccessing()
        {
            try
            {
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = true;
                    isStop = false;
                }));
               
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Proccessing...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                //int totalMacthingSuccess = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));
                if (dataLoad == null)
                {
                    dataLoad = new List<MasterListViewModel>();
                }
                if (dataLoad.Count == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Data load is empty, so not matching, please input data source";
                    }));
                    return;
                }
                else
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Proccessing ...";
                    }));
                    btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectByAlgorithm.Enabled = false;
                    }));
                }
                int totalRequest = 0;
                if (dataLoad.Count % Core.LimitRequestTrackingWork == 0)
                {
                    totalRequest = dataLoad.Where(p => p.IsProccessing == false).ToList().Count / Core.LimitRequestTrackingWork;
                }
                else
                {
                    totalRequest = dataLoad.Where(p => p.IsProccessing == false).ToList().Count / Core.LimitRequestTrackingWork + 1;
                }
                int index = 0;
                int currentTimeRequest = 0;
                
                while (index < dataLoad.Count)
                {
                    MasterListChangeListRequest request = new MasterListChangeListRequest();
                    MasterListCreateRequest item;
                    if (isStop)
                    {
                        goto END;
                    }
                    DateTime startTime = DateTime.Now;
                    currentTimeRequest++;

                    #region detect
                    ////TODO: sua index thanh 0=> vi mathing da bo qua
                    var blockRequest = dataLoad.Where(p => p.IsProccessing == false).Skip(0).Take(Core.LimitRequestTrackingWork).ToList();                    
                    foreach (var x in blockRequest)
                    {
                        x.IsProccessing = true;
                    }                   
                    #endregion

                    #region Creat request                   
                    for (int i = 0; i < blockRequest.Count; i++)
                    {
                        item = new MasterListCreateRequest();
                        item.Id = blockRequest[i].Id;
                        item.Year = blockRequest[i].Year;
                        item.Month = blockRequest[i].Month;
                        item.SerialNo = blockRequest[i].SerialNo;
                        item.ID_youtube = blockRequest[i].ID_youtube;
                        item.TITLE = blockRequest[i].TITLE;
                        item.TITLE2 = blockRequest[i].TITLE2;
                        item.ARTIST = blockRequest[i].ARTIST;
                        item.ARTIST2 = blockRequest[i].ARTIST2;
                        item.ALBUM = blockRequest[i].ALBUM;
                        item.ALBUM2 = blockRequest[i].ALBUM2;
                        item.LABEL2 = blockRequest[i].LABEL2;
                        item.ISRC = blockRequest[i].ISRC;
                        item.COMP_ID = blockRequest[i].COMP_ID;
                        item.COMP_TITLE = blockRequest[i].COMP_TITLE;
                        item.COMP_ISWC = blockRequest[i].COMP_ISWC;
                        item.COMP_WRITERS = blockRequest[i].COMP_WRITERS;
                        item.COMP_CUSTOM_ID = blockRequest[i].COMP_CUSTOM_ID;
                        item.QUANTILE = blockRequest[i].QUANTILE;
                        item.IsReport1 = blockRequest[i].IsReport1;
                        item.IsReport2 = blockRequest[i].IsReport2;
                        item.IsReport3 = blockRequest[i].IsReport3;
                        item.IsReport4 = blockRequest[i].IsReport4;
                        item.IsReport5 = blockRequest[i].IsReport5;
                        item.IsReport6 = blockRequest[i].IsReport6;
                        item.IsReport7 = blockRequest[i].IsReport7;
                        item.IsReport8 = blockRequest[i].IsReport8;
                        item.IsReport9 = blockRequest[i].IsReport9;
                        item.IsReport10 = blockRequest[i].IsReport10;
                        item.ScoreDetect1Vn = blockRequest[i].ScoreDetect1Vn;
                        item.ScoreDetect2Algorithm = blockRequest[i].ScoreDetect2Algorithm;
                        item.ScoreDetect3API = blockRequest[i].ScoreDetect3API;
                        item.DetectLanguage = blockRequest[i].DetectLanguage;
                        item.Note = blockRequest[i].Note;
                        item.TotalScore = blockRequest[i].TotalScore;
                        item.IsDetect1Vn = blockRequest[i].IsDetect1Vn;
                        item.IsDetect2Algorithm = blockRequest[i].IsDetect2Algorithm;
                        item.IsDetectAPI = blockRequest[i].IsDetectAPI;
                        item.IsISRC = blockRequest[i].IsISRC;
                        item.IsLABEL = blockRequest[i].IsLABEL;
                        item.IsVn = blockRequest[i].IsVn;
                        item.IsVnAPI = blockRequest[i].IsVnAPI;
                        item.Condition = blockRequest[i].Condition;
                        item.Condition2 = blockRequest[i].Condition2;
                        item.TotalWord = blockRequest[i].TotalWord;
                        item.IsConvertNotSignVN = blockRequest[i].IsConvertNotSignVN;
                        item.IsAnalysic = blockRequest[i].IsAnalysic;
                        item.Percents = blockRequest[i].Percents;
                        request.Items.Add(item);                       
                        blockRequest[i].IsProccessing = true;
                    }
                    ConvertToUnSign(request.Items);
                    DetatectAC(request);
                    request.Total = request.Items.Count;
                    request.Year = year;
                    request.Month = MONTH;
                    index += Core.LimitRequestTrackingWork;
                    if (index > dataLoad.Count)
                    {
                        index = dataLoad.Count;
                    }
                    #endregion
                   

                    #region save
                    var dataReponseSave = await Controller.ChangeList(request);
                    request.Items.Clear();
                    request.Items = null;
                    request = null;
                    GC.Collect();
                    #endregion

                    #region Update UI
                    DateTime endtime = DateTime.Now;
                    richinfo.Invoke(new MethodInvoker(delegate
                    {
                        richinfo.Text = "";
                        richinfo.Text += $"Total record(s): {dataReponseSave.Items.Count}{Environment.NewLine}";  
                        richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                        richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                    }));

                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        if (currentTimeRequest > totalRequest) currentTimeRequest = totalRequest;
                        float values = (float)currentTimeRequest / (float)totalRequest * 100;
                        progressBarImport.Value = (int)values;
                        lbPercent.Text = $"{((int)values).ToString()}%";
                    }));
                    #endregion
                }
            END:
                #region update Ui when finish
                currentPage = 1;
                CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                EnablePagging(currentPage, totalPage);
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;                    
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Matching is finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";                   
                }));
                btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                {
                    btnDetectByAlgorithm.Enabled = true;
                }));
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = false;
                }));
               
                isStop = false;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Matching is finish";
                }));
                #endregion
            }
            catch (Exception)
            {
                isStop = false;
                if (btnDetectByAlgorithm != null && !btnDetectByAlgorithm.IsDisposed)
                {
                    btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectByAlgorithm.Enabled = true;
                    }));
                }
                if (lbInfo != null && !btnDetectByAlgorithm.IsDisposed)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Matching is failure";
                    }));
                }
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

        private void ConvertToUnicode(List<MasterListViewModel> dataLoad)
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
            catch (Exception)
            {
            }
        }
        private void ConvertToUnSign(List<MasterListCreateRequest> dataLoad)
        {
            //string[] array = new string[] { "I.", "II.", "III.", "IV.", "V.", "/", ".", ",", ";", "(", ")","[","]","!","#","@","$","%","?", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",",","-","|"};
            try
            {
                if (dataLoad == null || dataLoad.Count == 0)
                {
                    return;
                }
                int ix = 1;
                foreach (var item in dataLoad)
                {
                    ix++;
                    if (ix > 50000) break;
                    if (item.TITLE != string.Empty)
                    {
                        item.TITLE2 = VnHelper.ConvertToUnSign(item.TITLE).ToUpper();
                        //item.TITLE2 = "1234567890!@#$%^&*()_-=+~`'-\\//-.?<>nguyen van trung";
                        item.TITLE2 = FormatVietnamese.RemoveSpecialCharacters(item.TITLE2);
                        //for (int i = 0; i < array.Length; i++)
                        //{
                        //    item.TITLE2 = item.TITLE2.Replace(array[i], "");
                        //}
                    }
                    if (item.ARTIST != string.Empty)
                    {
                        item.ARTIST2 = VnHelper.ConvertToUnSign(item.ARTIST).ToUpper();
                        item.ARTIST2 = FormatVietnamese.RemoveSpecialCharacters(item.ARTIST2);
                    }
                    if (item.ALBUM != string.Empty)
                    {
                        item.ALBUM2 = VnHelper.ConvertToUnSign(item.ALBUM).ToUpper();
                        item.ALBUM2 = FormatVietnamese.RemoveSpecialCharacters(item.ALBUM2);
                    }
                    if (item.LABEL != string.Empty)
                    {
                        item.LABEL2 = VnHelper.ConvertToUnSign(item.LABEL).ToUpper();
                        item.LABEL2 = FormatVietnamese.RemoveSpecialCharacters(item.LABEL2);
                    }                    
                }
            }
            catch (Exception)
            {
            }
        }

        private void DetatectAC(MasterListChangeListRequest source)
        {
            int x = 0;
            try
            {
                string[] eng = new string[] { "AND","IF", "OR","OF","FOR","DR","BR","ER","ED", "WH", "LL", "PP", "KN","NK" ,"SP","STR", "OW", "OO","EE","TT","SS", "SP", "YOU", "SP", "OW", "", "", "", "", "", "", "" }; 
                //Chữ cái đầu ISRC
                string[] isrc_passed = new string[] { "US", "VG", "QM", "CI", "FR", "GB", "ES", "BG", "CA", "DE", "IT", "HK", "QZ", "NO", "RU", "SE", "TC", "TW", "CH" };
                //Danh mục trong LABEL 
                string[] label_passed = new string[] { "Lang Van", "Làng Văn", "Thuy Nga", "Thúy Nga", "Thuy Nga Production", "Thúy Anh", "Rang Dong", "RANGDONG",
                "RangDong", "Rang Dong INC", "Mimosa", "Kim Ngân", "AudioSparx", "Bai Hat Ru Cho Anh", "VNG", "Young Hit Young Beat", "Elvis Phương", "DONG GIAO PRO",
                "Buda musique", "TÂN HIỆP PHÁT", "Caprice", "Y Phuong", "Horus Music Distribution", "LIDIO – SafeMUSE Sounds", "Amy Music", "SAIGON VAFACO",
                "Saigon Broadcasting Television Network", "Kawaiibi", "Inédit / Maison des cultures du monde", "Người Đẹp Bình Dương", "Kiều Thơ Mellow",
                "A Fang Entertainment", "iMusician Digital", "DONG GIAO", "Dang Khoi", "Dihavina", "Doremi", "Future Arts Production", "Great Entertainment",
                "Hãng Đĩa Thời Đại (Times Records)", "Ho Entertainment & Events", "Kawaiibi", "Kim Ngân", "MT Entertainment", "Người Đẹp Bình Dương Gold",
                "SÀI GÒN VAFACO", "SAI GON VAFACO", "SÀI GÒN - VAFACO", "TN Entertainment", "VNG", "Vega Media", "Thăng Long AV", "SAIGON VAFACO.",
                "Dong Dao 2007", "Best Of HKT", "Do Bao", "Wepro Entertainment", "Tuan Trinh Production", "Walt Disney Records" };
                string isrc_2_letters = "";
                DetectLangReturn ir;
                for (int i = 0; i < source.Items.Count; i++)
                {
                    x = 1;
                    #region Phan loai report
                    if (source.Items[i].COMP_CUSTOM_ID != "" && source.Items[i].COMP_CUSTOM_ID != "0")
                    {
                        source.Items[i].IsReport1 = true;
                    }
                    else
                    {
                        if (source.Items[i].COMP_ISWC != "" && source.Items[i].COMP_ISWC != "0")
                        {
                            source.Items[i].IsReport2 = true;
                        }
                        else
                        {
                            source.Items[i].IsReport3 = true;
                        }

                    }
                    #endregion
                    x = 2;
                    #region phat hien dau
                    if (VnHelper.Detect(source.Items[i].TITLE))
                    {
                        source.Items[i].ScoreDetect1Vn = 1;
                    }
                    else if (VnHelper.Detect(source.Items[i].ARTIST))
                    {
                        source.Items[i].ScoreDetect1Vn = 1;
                    }
                    else if (VnHelper.Detect(source.Items[i].ALBUM))
                    {
                        source.Items[i].ScoreDetect1Vn = 1;
                    }
                    else
                    {
                        source.Items[i].ScoreDetect1Vn = 0;
                    }
                    #endregion
                    x = 3;
                    #region phat hien bang thuat toan
                    //tiep tu check bang thuat toan   
                    string addStr = "";
                    ir = new DetectLangReturn();
                    if (source.Items[i].TITLE2 != null)
                    {
                        addStr = source.Items[i].TITLE2.Trim();
                    }
                    //if (source[i].ARTIST2 != null)
                    //{
                    //    addStr = $"{addStr} {source[i].ARTIST2.Trim()}";
                    //}
                    //if (source[i].ALBUM2 != null)
                    //{
                    //    addStr = $"{addStr} {source[i].ALBUM2.Trim()}";
                    //}
                    //if (source[i].LABEL2 != null)
                    //{
                    //    addStr = $"{addStr} {source[i].LABEL2.Trim()}";
                    //}    
                    //if(source[i].ID.Trim() == "A459495325022634")
                    //{
                    //    int a = 1;
                    //}
                    addStr = addStr.Trim();
                    if (addStr != "")
                    {
                        ir = FormatVietnamese.DetectVietnamese(addStr);
                    }
                    source.Items[i].IsDetect2Algorithm = true;
                    source.Items[i].ScoreDetect2Algorithm = ir.Score;
                    source.Items[i].TotalWord = ir.TotalWord;
                    source.Items[i].CorrectWord = ir.CorrectWord;
                    if (source.Items[i].TotalWord != 0)
                    {
                        source.Items[i].Percents = ((decimal)source.Items[i].CorrectWord) / ((decimal)source.Items[i].TotalWord);
                    }
                    else
                    {
                        source.Items[i].Percents = 0;
                    }
                    #endregion
                    x = 4;
                    #region kiem tra
                    if (source.Items[i].ScoreDetect2Algorithm > 0)
                    {
                        isrc_2_letters = source.Items[i].ISRC.Length>2? source.Items[i].ISRC.Substring(0, 2): string.Empty;
                        if (isrc_2_letters == "VN" && source.Items[i].ScoreDetect2Algorithm > 2)
                        {
                            source.Items[i].IsVn = true;
                            source.Items[i].IsISRC = true;
                            x = 5;
                        }
                        else
                        {
                            if (label_passed.Contains(source.Items[i].LABEL))
                            {
                                source.Items[i].IsLABEL = true;
                                if (source.Items[i].Percents >= 1 && source.Items[i].ScoreDetect2Algorithm > 2)
                                {
                                    source.Items[i].IsVn = true;
                                }
                                else if (isrc_passed.Contains(isrc_2_letters))
                                {
                                    source.Items[i].IsISRC = true;
                                    //nghe si+tuyen tap co dau
                                    if (source.Items[i].ScoreDetect1Vn < 1)
                                    {
                                        if (source.Items[i].Percents > 0.1M && source.Items[i].ScoreDetect2Algorithm > 10)
                                        {
                                            source.Items[i].IsVn = true;
                                        }
                                    }
                                    else 
                                    {
                                        //tieu de bai hat co dau
                                        if (source.Items[i].Percents > 0.1M && source.Items[i].ScoreDetect2Algorithm > 5)
                                        {
                                            source.Items[i].IsVn = true;
                                        }
                                    }                                    
                                }
                                else
                                {
                                    if (source.Items[i].Percents > 0.1M && source.Items[i].ScoreDetect2Algorithm > 15)
                                    {
                                        source.Items[i].IsVn = true;
                                    }
                                }
                                x = 6;
                            }
                            else
                            {
                                if(source.Items[i].Percents >= 1 && source.Items[i].ScoreDetect2Algorithm > 2)
                                {
                                    source.Items[i].IsVn = true;
                                }
                                else if(source.Items[i].ScoreDetect1Vn < 1)
                                {
                                    if (source.Items[i].Percents > 0.3M && source.Items[i].ScoreDetect2Algorithm > 10)
                                    {
                                        source.Items[i].IsVn = true;
                                    }
                                }
                                else
                                {
                                    if (source.Items[i].Percents > 0.3M && source.Items[i].ScoreDetect2Algorithm > 5)
                                    {
                                        source.Items[i].IsVn = true;
                                    }
                                    else if (source.Items[i].ScoreDetect2Algorithm > 25)
                                    {
                                        source.Items[i].IsVn = true;
                                    }
                                }                               
                                
                                x = 7;
                            }
                        }
                        if (!source.Items[i].IsVn && source.Items[i].ScoreDetect2Algorithm > 2 && source.Items[i].Percents > 0.2M)
                        {
                            if((source.Items[i].ScoreDetect2Algorithm > 5 && source.Items[i].Percents > 0.3M)
                                || source.Items[i].ScoreDetect2Algorithm > 10
                                )
                            {
                                source.Items[i].IsNeedDetectAPI = true;
                            }
                            else
                            {
                                //cac truong hop khong phai la tieng viet, canphai phat hien
                                bool checkEnf = false;
                                for (int k = 0; k < eng.Length; k++)
                                {
                                    if (source.Items[i].TITLE2.Contains(eng[k]))
                                    {
                                        checkEnf = true;
                                        break;
                                    }
                                }
                                if (!checkEnf)
                                {
                                    source.Items[i].IsNeedDetectAPI = true;
                                }
                            }                                                  
                        }
                        else
                        {
                            //int a = 1;
                        }
                        x = 8;
                    }
                    else
                    {
                        //int a = 1;
                    }
                    #endregion
                    x = 9;
                }
            }
            catch (Exception)
            {

                int a = x;
            }
            
        }                
        #endregion

        #region Export
        private void ExportToExcel(string filePath)
        {
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
                lbOperation.Text = "Export to excel...";
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
                //int serial = 0;
                //int index = 0;
                //while (index < dataLoad.Count)
                //{
                //    serial++;
                //    var datax = dataLoad.Skip(index).Take(Core.LimitDisplayExportExcel).ToList();
                //    index += Core.LimitDisplayExportExcel;
                //    bool check = WriteReportHelper.ExportPreClaimMatching(datax, $"{path}\\{name}-{serial.ToString().PadLeft(3, '0')}.{extension}");
                //    datax = null;
                //    GC.Collect();
                //    statusMain.Invoke(new MethodInvoker(delegate
                //    {
                //        if (serial > totalFile) serial = totalFile;
                //        float values = (float)serial / (float)totalFile * 100;
                //        progressBarImport.Value = (int)values;
                //        lbPercent.Text = $"{((int)values).ToString()}%";
                //    }));
                //}
            }
            statusMain.Invoke(new MethodInvoker(delegate
            {
                lbOperation.Text = "Export to excel be finish";
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
        #endregion

        #region Btn
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                #region set backgroundWorker
                Operation = OperationType.RefreshUI;
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
        private void btnLoad_Click(object sender, EventArgs e)
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
                    openFileDialog.Filter = "CSV Files|*.csv;";
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
                year = (int)numYear.Value;
                MONTH = cboMonths.SelectedIndex+1;
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
        private void btnResetMatching_Click(object sender, EventArgs e)
        {
            try
            {
                #region set backgroundWorker
                Operation = OperationType.ResetStatus;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion               
            }
            catch (Exception)
            {

                //throw;
            }

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
               
                #region set backgroundWorker
                Operation = OperationType.GetDataFromServer;
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
        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
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
                    //    string id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                    //    var item = CurrentDataView.Where(s => s.Id == id).FirstOrDefault();
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
                //    for (int i = 0; i < dgvMain.Rows.Count; i++)
                //    {
                //        string id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                //        var item = CurrentDataView.Where(s => s.Id == id).FirstOrDefault();
                //        if (item != null)
                //        {
                //            if (item.IsSuccess)
                //            {
                //                dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                //            }
                //        }
                //    }
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
                    //    string id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                    //    var item = CurrentDataView.Where(s => s.Id == id).FirstOrDefault();
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
                    //    string id = (string)dgvMain.Rows[i].Cells["Id"].Value;
                    //    var item = CurrentDataView.Where(s => s.Id == id).FirstOrDefault();
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
                List<MasterListViewModel> fill = new List<MasterListViewModel>();
                if (cboTypeChoise == 0)
                {
                    var query = dataLoad.Where(delegate (MasterListViewModel c)
                    {
                        if (c.ID_youtube.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    var query = dataLoad.Where(delegate (MasterListViewModel c)
                    {
                        if (c.TITLE2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    var query = dataLoad.Where(delegate (MasterListViewModel c)
                    {
                        if (c.ARTIST2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    var query = dataLoad.Where(delegate (MasterListViewModel c)
                    {
                        if (c.ALBUM2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    var query = dataLoad.Where(delegate (MasterListViewModel c)
                    {
                        if (c.LABEL2.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
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

        #region dgvMain
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dgvMain.CurrentCell.ColumnIndex == 0)
            //    {
            //        if (dgvMain.CurrentCell.Value == null || (bool)dgvMain.CurrentCell.Value == false)
            //        {
            //            dgvMain.CurrentCell.Value = true;
            //        }
            //        else
            //        {
            //            dgvMain.CurrentCell.Value = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        //private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if (data == null || data.IsSuccessed == false || data.ResultObj == null || data.ResultObj.Items.Count == 0)
        //        {
        //            return;
        //        }
        //        //if (dgvMain.Rows.Count > 0)
        //        //{
        //        //    string id = (string)dgvMain.CurrentRow.Cells["Id"].Value;
        //        //    CurrenObject = data.ResultObj.Items.Where(s => s.Id == id).First();
        //        //    if (CurrenObject == null)
        //        //    {
        //        //        MessageBox.Show("Eror: recode is null");
        //        //        return;
        //        //    }
        //        //    frmPreclaimUpdate frm = new frmPreclaimUpdate(preclaimController, UpdataType.View, CurrenObject);
        //        //    frm.ShowDialog();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

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
                else if (Operation == OperationType.GetDataFromServer)
                {
                    Proccessing();
                }
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(filepath);
                }
                else if (Operation == OperationType.DetectLanguageByAPI)
                {
                    DetectLanguageByAPI();
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
            if (closePending) this.Close();
            closePending = false;
        }
        #endregion

        #region threads
        private void btnDetectLangAPI_Click(object sender, EventArgs e)
        {
            try
            {
                year = (int)numYear.Value;
                MONTH = cboMonths.SelectedIndex + 1;
                DialogResult = MessageBox.Show($"Detect masterlist that save database and need detect by API:{Environment.NewLine} Deteail: {Environment.NewLine}" +
                     $"Year: {year}{Environment.NewLine} month: {MONTH}{Environment.NewLine}", "Confirm DETECT language by API", MessageBoxButtons.YesNo);

                if (DialogResult == DialogResult.Yes)
                {
                    #region set backgroundWorker
                    Operation = OperationType.DetectLanguageByAPI;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        int totalDetectIsVN = 0;
        int totalSendDetect = 0;
        int totalReceiceDetect = 0;

        private async Task<List<MasterListViewModel>> GetDataDetect()
        {
            List<MasterListViewModel> list = new List<MasterListViewModel>();
            try
            {

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data need detect from server...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                //int totalMacthingSuccess = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));
                //load data
                request.IsNeedDetectAPI = true;
                request.Year = year;
                request.Month = MONTH;
                request.PageSize = Core.LimitRequestTrackingWork;
                request.PageIndex = 0;
                var total = await Controller.TotalGetAllPaging(request);
                if (total != null && total.TotalRecordes > 0)
                {
                    int totalRequest = 0;
                    if (total.TotalRecordes % Core.LimitRequestTrackingWork == 0)
                    {
                        totalRequest = total.TotalRecordes / Core.LimitRequestTrackingWork;
                    }
                    else
                    {
                        totalRequest = total.TotalRecordes / Core.LimitRequestTrackingWork + 1;
                    }
                    
                    int currentTimeRequest = 0;
                    while (currentTimeRequest < totalRequest)
                    {   
                        currentTimeRequest++;
                        request.PageIndex++;
                        var source = await Controller.GetAllPaging(request);
                        if (source != null || source.ResultObj != null || source.ResultObj.Items != null || source.ResultObj.Items.Count > 0)
                        {
                            foreach (var item in source.ResultObj.Items)
                            {
                                list.Add(item);
                            }
                        }
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            if (currentTimeRequest > totalRequest) currentTimeRequest = totalRequest;
                            float values = (float)currentTimeRequest / (float)totalRequest * 100;
                            progressBarImport.Value = (int)values;
                            lbPercent.Text = $"{((int)values).ToString()}%";
                        }));
                        if (currentTimeRequest == 2) break;
                    }  
                    
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {                   
                    progressBarImport.Value = 100;
                    lbPercent.Text = "100%";
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Load data need detect from server finish, total: {list.Count}";
                }));
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                list = new List<MasterListViewModel>();
            }
            return list;
        }

        private async void DetectLanguageByAPI()
        {
            
            try
            {
                btnDetectLangAPI.Invoke(new MethodInvoker(delegate
                {
                    btnDetectLangAPI.Enabled = false;
                }));
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = true;
                    isStop = false;
                }));

                List<MasterListViewModel> sourceNeedDetect = await GetDataDetect();
                if(sourceNeedDetect.Count==0)
                {
                    btnDetectLangAPI.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectLangAPI.Enabled = true;
                    }));
                    btnStop.Invoke(new MethodInvoker(delegate
                    {
                        btnStop.Enabled = true;
                        isStop = false;
                    }));
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Total need detect by API is zero, so don't excute.";
                    }));
                    return;
                }
                totalDetectIsVN = 0;
                totalSendDetect = 0;
                totalReceiceDetect = 0;               
               
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Proccessing detect by API...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                //int totalMacthingSuccess = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             

                DateTime startTime = DateTime.Now;               
                #region Data detect
                int perRecord = sourceNeedDetect.Count / (nth);
                if (perRecord < 1) perRecord = 1;
                for (int i = 0; i < nth; i++)
                {
                    dataNeadDetectLan[i].Clear();
                    var sourceSub = sourceNeedDetect.Skip(i * perRecord).Take(perRecord).ToList();
                    if (sourceSub.Count == 0)
                    {
                        break;
                    }
                    dataNeadDetectLan[i] = sourceSub;
                }
                #endregion

                #region Detect                   
                MultiThreadDetectLanguage();
                while (!isCreateThread)
                {
                    Thread.Sleep(50);
                }
            WaitCallback:
                if (CheckDetectTheardFinish())
                {                  
                    MasterListChangeListRequest requestSave = new MasterListChangeListRequest();
                    MasterListCreateRequest item;
                    for (int i = 0; i < sourceNeedDetect.Count; i++)
                    {
                        item = new MasterListCreateRequest();
                        item.Id = sourceNeedDetect[i].Id;
                        item.IsVnAPI = sourceNeedDetect[i].IsVnAPI;
                        if (item.IsVnAPI)
                        {
                            totalDetectIsVN++;
                        }
                        item.IsDetectAPI = true;
                        requestSave.Items.Add(item);
                    }
                    var dataReponseSave = await Controller.ChangeList(requestSave);
                    requestSave.Items.Clear();
                    requestSave.Items = null;
                    requestSave = null;
                    GC.Collect();
                }
                else
                {
                    Thread.Sleep(100);
                    goto WaitCallback;
                }
                #endregion

                #region Update UI
                DateTime endtime = DateTime.Now;
                richinfo.Invoke(new MethodInvoker(delegate
                {
                    richinfo.Text = "";
                    richinfo.Text += $"Total record(s): {sourceNeedDetect.Count}{Environment.NewLine}";
                    richinfo.Text += $"Time(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                }));                
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"detect..., total detect/total: {totalDetectIsVN}/{sourceNeedDetect.Count} ";
                }));
            #endregion
            //int index = 0;
            //int currentTimeRequest = 0;
            //while (index < sourceNeedDetect.Count)
            //{                      
            //    if (isStop)
            //    {
            //        goto END;
            //    }
            //}
            END:
                #region update Ui when finish
                //currentPage = 1;
                //CurrentDataView = dataLoad.Skip((currentPage - 1) * Core.LimitDisplayDGV).Take(Core.LimitDisplayDGV).ToList();
                //EnablePagging(currentPage, totalPage);
                //dgvMain.Invoke(new MethodInvoker(delegate
                //{
                //    dgvMain.DataSource = CurrentDataView;
                //}));
                //statusMain.Invoke(new MethodInvoker(delegate
                //{
                //    lbInfo.Text = $"Matching is finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                //}));
                //btnDetectByAlgorithm.Invoke(new MethodInvoker(delegate
                //{
                //    btnDetectByAlgorithm.Enabled = true;
                //}));
                //btnStop.Invoke(new MethodInvoker(delegate
                //{
                //    btnStop.Enabled = false;
                //}));

                //isStop = false;
                //statusMain.Invoke(new MethodInvoker(delegate
                //{
                //    lbOperation.Text = $"Matching is finish, total detect/total: {totalDetectIsVN}/{sourceNeedDetect.Count} ";
                //}));
                #endregion
                //finish
                btnDetectLangAPI.Invoke(new MethodInvoker(delegate
                {
                    btnDetectLangAPI.Enabled = true;
                }));
                if (btnStop != null && !btnStop.IsDisposed)
                {
                    btnStop.Invoke(new MethodInvoker(delegate
                    {
                        btnStop.Enabled = true;
                    }));
                }
            }
            catch (Exception)
            {

                isStop = false;
                if (btnDetectByAlgorithm != null && !btnDetectByAlgorithm.IsDisposed)
                {
                    btnDetectLangAPI.Invoke(new MethodInvoker(delegate
                    {
                        btnDetectLangAPI.Enabled = true;
                    }));
                }
                if (btnStop != null && !btnStop.IsDisposed)
                {
                    btnStop.Invoke(new MethodInvoker(delegate
                    {
                        btnStop.Enabled = true;
                    }));
                }
                if (lbInfo != null && !btnDetectByAlgorithm.IsDisposed)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Detect is failure";
                    }));
                }
            }
        }

        private bool CheckDetectTheardFinish()
        {        
            for (int i = 0; i < nth; i++)
            {
                if(threadFinish[i] == false)
                {                   
                    return false;
                }
            }
            return true;
        }
        Object lockj = new object();
        bool isCreateThread = false;
        private void MultiThreadDetectLanguage()
        {
            isCreateThread = false;
            for (int i = 0; i < nth-1; i++)
            {
                //lock(threads)
                //{
                threadFinish[i] = false;
                if (threads[i] != null)
                {
                    if (threads[i].IsAlive)
                    {
                        threads[i].Abort();
                        threads[i].Join();
                    }
                }
                //ThreadStart threadStart = new ThreadStart(mythread1(dataNeadDetectLan[i]));
                //if(i< nth)
                //{
                threads[i] = new Thread(() => DetectCall(i, dataNeadDetectLan[i]));
                threads[i].Start();
                //}                  
                //} 
            }
            isCreateThread = true;
        }
        public void DetectCall(int index, List<MasterListViewModel> source)
        {
            //if(index>=nth)
            //{
            //    return;
            //}
            string adds = string.Empty;
            bool check = false;
            foreach (var item in source)
            {
                adds = $"{item.TITLE} {item.ARTIST} {item.ALBUM}";
                check = TestDetectLanguage(index, adds);
                
                totalReceiceDetect++;
                if(check)
                {
                    item.IsVnAPI = true;
                    //item.IsDetectAPI = true;
                    totalDetectIsVN++;                    
                }
                else
                {

                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbTotalDetectAPI.Text = $"{totalSendDetect}/{totalReceiceDetect}/{totalDetectIsVN}";
                }));
                //statusMain.Invoke(new MethodInvoker(delegate
                //{
                //    if (currentTimeRequest > totalRequest) currentTimeRequest = totalRequest;
                //    float values = (float)currentTimeRequest / (float)totalRequest * 100;
                //    progressBarImport.Value = (int)values;
                //    lbPercent.Text = $"{((int)values).ToString()}%";
                //}));
                Thread.Sleep(10);
            }
            threadFinish[index] = true;
        }
        private bool TestDetectLanguage(int i, string language)
        {
            /*
             Đây là API KEY của trang detectlanguage mà VCPMC đã mua với gói Plus = 0a2e6acb6fec06a29930c79c6dccbd4d
             */
            try
            {
                totalSendDetect++;
                return languageDetect[i].DetectLanguage(language);

            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }
        
        #endregion

    }
}
