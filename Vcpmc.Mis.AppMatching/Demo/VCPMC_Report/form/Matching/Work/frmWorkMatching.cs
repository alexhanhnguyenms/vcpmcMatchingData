using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.AppMatching.Controllers.System;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.Services.System;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.Member;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared;
using Vcpmc.Mis.Shared.Mis.Members;
using Vcpmc.Mis.Shared.Parameter;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.Utilities.Common;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.AppMatching.form.Matching.Work
{
    public partial class frmWorkMatching : Form
    {
        #region vari                
        WorkController controller;
        WorkApiClient apiClient;
        //request matching
        WorkMatchingListRequest request = new WorkMatchingListRequest();        
        /// <summary>
        /// Dữ liệu trả về từ matching
        /// </summary>
        ApiResult<PagedResult<WorkViewModel>> dataReponse = new ApiResult<PagedResult<WorkViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        string filepath = string.Empty;
        /// <summary>
        /// du lieu load tu excel
        /// </summary>
        List<WorkMatchingViewModel> dataLoad = new List<WorkMatchingViewModel>();
        /// <summary>
        /// Dữ liệu đang hiển thị
        /// </summary>
        List<WorkMatchingViewModel> CurrentDataView = new List<WorkMatchingViewModel>();
        int currentPage = 1;
        int totalPage = 0;

        MatchCalc _matching = new MatchCalc();
        bool isStop = false;
        MonopolyType monopolyType = MonopolyType.Not;
        //MonopolyGroupType monopolyGroupType = MonopolyGroupType.Work;
        //bool isMatcingDone = false;
        //mono
        MonopolyController MonoController;
        MonopolyApiClient monoApiClient;
        GetMonopolyPagingRequest monoRequest = new GetMonopolyPagingRequest();
        //fix pẩ mêtr
        FixParameterController fixcontroller;
        FixParameterApiClient fixapiClient;
        GetFixParameterPagingRequest fixrequest = new GetFixParameterPagingRequest();
        ApiResult<PagedResult<FixParameterViewModel>> fixdata = new ApiResult<PagedResult<FixParameterViewModel>>();
        /// <summary>
        /// thay the title dau vao 
        /// </summary>
        bool isReplateTitle = false;
        /// <summary>
        /// thay the tac gia dau vao
        /// </summary>
        bool isReplateWriter = false;
        /// <summary>
        /// doi sang code moi va matching
        /// </summary>
        bool isChangeWorkcode = false;
        /// <summary>
        /// Title->Wr->Art->COM->Mem->Non-mem
        /// Title->Wr->Art->Mem->COM->Non-mem
        /// Title->Wr->Art->Mem->Non-mem->COM
        /// </summary>
        int priorityValue = 1;
        /// <summary>
        /// uu tien VCPMC
        /// </summary>
        bool isPrioriryForVcpmc = false;
        #endregion

        #region Innit
        public frmWorkMatching()
        {
            InitializeComponent();
            apiClient = new WorkApiClient(Core.Client);
            controller = new WorkController(apiClient);
            //mono
            monoApiClient = new MonopolyApiClient(Core.Client);
            MonoController = new MonopolyController(monoApiClient);
            //
            fixapiClient = new FixParameterApiClient(Core.Client);
            fixcontroller = new FixParameterController(fixapiClient);
        }

        private void frmWorkMatching_Load(object sender, EventArgs e)
        {
            cboPriority.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            cboMonopolyType.SelectedIndex = 0;
            cboNumsItem.SelectedIndex = 6;
            cboTypeMatching.SelectedIndex = 0;

            cboRateWriter.SelectedIndex = 0;
            cboRateArtist.SelectedIndex = 2;
            cboUnique.SelectedIndex = 0;
            cboReplateTextType.SelectedIndex = 3;

            isPrioriryForVcpmc = cboPrioriryForVcpmc.Checked;
        }
        private bool closePending;
        private void frmWorkMatching_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                closePending = true;
                backgroundWorker1.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;
                return;
            }
            //base.OnFormClosing(e);
            dataLoad = null;
            GC.Collect();
        }
        #endregion

        #region LoadData
        private async void LoadExcel()
        {
            try
            {
                //isMatcingDone = false;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Load data...";
                }));
                isStop = false;
                dataLoad = new List<WorkMatchingViewModel>();
                CurrentDataView = new List<WorkMatchingViewModel>();
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = CurrentDataView;
                }));
                DateTime starttime = DateTime.Now;
                ExcelHelper excelHelper = new ExcelHelper();
                //dataLoad = excelHelper.ReadExcelImportPreClaimMatching(filepath);
                //dataLoad = CsvReadHelper.ReadCSVPreClaimMatching(filepath);
                dataLoad = null;
                GC.Collect();
                dataLoad = CsvReadHelper.ReadUnicodeWorkMatching(filepath);
                excelHelper = null;
                DateTime endtime = DateTime.Now;               
                if (dataLoad != null)
                {
                    //chuyen doi
                    List<ReplateText> listTitle = new List<ReplateText>();
                    List<ReplateText> listWriter = new List<ReplateText>();                    
                 
                    #region Chuyen tu none member sang to member
                    if (isReplateTitle)
                    {
                        fixrequest.Type = TypeFixParameter.MatchingReplateTitle.ToString();
                        fixdata = await fixcontroller.GetAllPaging(fixrequest);
                        if (fixdata != null && fixdata.ResultObj != null && fixdata.ResultObj.Items != null && fixdata.ResultObj.Items.Count > 0)
                        {
                            var data1 = fixdata.ResultObj.Items.Where(p => p.Type == TypeFixParameter.MatchingReplateTitle.ToString()).ToList();
                            for (int i = 0; i < data1.Count; i++)
                            {
                                listTitle.Add(new ReplateText
                                {
                                    OldValue = data1[i].Key.Trim().ToUpper(),
                                    NewValue = data1[i].Value1.Trim().ToUpper(),
                                });
                            }
                        }
                        if (listTitle.Count > 0)
                        {
                            foreach (var item in dataLoad)
                            {
                                foreach (var itemT in listTitle)
                                {
                                    item.Title2 = item.Title2.Replace(itemT.OldValue, itemT.NewValue);
                                }

                            }
                            //int a = 1;
                        }
                    }

                    #endregion

                    #region chuyen sang E
                    if (isReplateWriter)
                    {
                        fixrequest.Type = TypeFixParameter.MatchingReplateWriter.ToString();
                        fixdata = await fixcontroller.GetAllPaging(fixrequest);
                        if (fixdata != null && fixdata.ResultObj != null && fixdata.ResultObj.Items != null && fixdata.ResultObj.Items.Count > 0)
                        {
                            var data2 = fixdata.ResultObj.Items.Where(p => p.Type == TypeFixParameter.MatchingReplateWriter.ToString()).ToList();
                            string value = string.Empty;
                            for (int i = 0; i < data2.Count; i++)
                            {
                                listWriter.Add(new ReplateText
                                {
                                    OldValue = data2[i].Key.Trim().ToUpper(),
                                    NewValue = data2[i].Value1.Trim().ToUpper(),
                                });
                            }
                        }
                        if (listWriter.Count > 0)
                        {
                            foreach (var item in dataLoad)
                            {
                                foreach (var itemT in listWriter)
                                {
                                    item.Writer2 = item.Writer2.Replace(itemT.OldValue, itemT.NewValue);
                                }
                                //int a = 1;
                            }
                        }
                    }
                    #endregion
               
                    if(isChangeWorkcode)
                    {
                        string pathx = Path.GetDirectoryName(Application.ExecutablePath) + @"\Data\work\vpcmc_all_work.txt";
                        var vcpmcAllWorks = CsvReadHelper.ReadVCPMCAllWorkcode(pathx);
                        foreach (var item in dataLoad)
                        {
                            var workItem = vcpmcAllWorks.Where(p => p.OldCode == item.WorkCode).ToList();
                            if(workItem.Count>0)
                            {
                                item.WorkcodeChangeToNew = workItem[0].NewCode;
                            }
                            //TODO
                            //else
                            //{
                            //    item.WorkcodeChangeToNew = item.WorkCode;
                            //}
                        }
                    }                    
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
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = true;
                    }));
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = true;
                    }));
                    btnCalcMono.Invoke(new MethodInvoker(delegate
                    {
                        btnCalcMono.Enabled = true;
                    }));
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExportS.Enabled = true;
                    }));
                }
                else
                {                    
                    lbLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbLoad.Text = $"Load data is error!";
                    }));
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = false;
                    }));
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = false;
                    }));
                    btnCalcMono.Invoke(new MethodInvoker(delegate
                    {
                        btnCalcMono.Enabled = false;
                    }));
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnExportS.Enabled = false;
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
        private async void Mathching()
        {
            try
            {
                #region cal rate by writer
                int MatchingRateWriter = 0;
                switch (_matching.MatchingRateWriter)
                {
                    case MatchingRateWriterType._100:
                        MatchingRateWriter = 100;
                        break;
                    case MatchingRateWriterType._75:
                        MatchingRateWriter = 75;
                        break;
                    case MatchingRateWriterType._50:
                        MatchingRateWriter = 50;
                        break;
                    case MatchingRateWriterType._25:
                        MatchingRateWriter = 25;
                        break;
                    case MatchingRateWriterType._0:
                        MatchingRateWriter = 0;
                        break;
                    default:
                        MatchingRateWriter = 50;
                        break;
                }
                #endregion

                #region init
                int totalRequestFailure = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "Staring matching...";
                }));
                if (dataLoad.Where(p => p.IsMatching == false).ToList().Count == 0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "The list is matched, so not excute matching, please reset status for matching again";
                    }));

                    return;
                }
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                    lbPercent.Text = "0%";
                }));
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = true;
                    isStop = false;
                }));
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = false;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Matching...";
                }));
                DateTime TheFiestTime = DateTime.Now;
                //int totalMacthingSuccess = 0;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                }));
                if (dataLoad == null)
                {
                    dataLoad = new List<WorkMatchingViewModel>();
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
                        lbInfo.Text = $"Proccessing matching...";
                    }));
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = false;
                    }));
                }
                #endregion

                #region calc total request
                int totalRequest = 0;
                if (dataLoad.Where(p => p.IsMatching == false).ToList().Count % Core.LimitMatchingWorkRequest == 0)
                {
                    totalRequest = dataLoad.Where(p => p.IsMatching == false).ToList().Count / Core.LimitMatchingWorkRequest;
                }
                else
                {
                    totalRequest = dataLoad.Where(p => p.IsMatching == false).ToList().Count / Core.LimitMatchingWorkRequest + 1;
                }
                int index = 0;
                int currentTimeRequest = 0;
                #endregion                

                string message = string.Empty;
                while (index < dataLoad.Count)
                {
                    if (isStop)
                    {
                        goto END;
                    }
                    DateTime startTime = DateTime.Now;
                    currentTimeRequest++;
                    #region Creat request
                    //TODO: sua index thanh 0=> vi mathing da bo qua
                    var blockRequest = dataLoad.Where(p => p.IsMatching == false).Skip(0).Take(Core.LimitMatchingWorkRequest).ToList();
                    WorkMatchingRequest itemRequest;
                    request.Items.Clear();                  
                    for (int i = 0; i < blockRequest.Count; i++)
                    {
                        if(blockRequest[i].Title2!=string.Empty)
                        {
                            itemRequest = new WorkMatchingRequest();
                            itemRequest.Title = blockRequest[i].Title2;
                            itemRequest.Title2 = blockRequest[i].Title2;
                            if(itemRequest.Title2!=string.Empty)
                            {
                                request.Items.Add(itemRequest);
                            }                            
                            //check
                            //blockRequest[i].IsMatching = true;
                        }                       
                    }                    
                    request.Total = request.Items.Count;
                    
                    #endregion

                    #region Matching                   
                    dataReponse = await controller.MatchingWork(request);
                    if (dataReponse != null && dataReponse.ResultObj != null && dataReponse.ResultObj.Items.Count > 0)
                    {
                        //thanh cong moi chuyen tiếp theo
                        //that bại matching lại
                        index += Core.LimitMatchingWorkRequest;
                        if (index > dataLoad.Count)
                        {
                            index = dataLoad.Count;
                        }
                        #region Tao mang nghe sy
                        string[] ARTISTlarr = null;
                        foreach (var item in dataReponse.ResultObj.Items)
                        {
                            ARTISTlarr = item.ARTIST.Split(',');
                            for (int ik = 0; ik < ARTISTlarr.Length; ik++)
                            {
                                if (ARTISTlarr[ik].Trim() != string.Empty)
                                {
                                    item.ListArtist.Add(ARTISTlarr[ik].Trim());
                                }
                            }
                        }
                        
                        #endregion

                        #region set value matching

                        for (int i = 0; i < blockRequest.Count; i++)
                        {
                            message = "";
                            //check
                            blockRequest[i].IsMatching = true;

                            if (blockRequest[i].Title2==string.Empty)
                            {
                                blockRequest[i].Messsage = "Không có tiêu đề";
                                continue;
                            }
                            IEnumerable<WorkViewModel> query=null;                           

                            #region title
                            if (_matching.IsMatchingByTitle)
                            {
                                query = dataReponse.ResultObj.Items.Where(p => p.TTL_ENG == blockRequest[i].Title2
                                || p.OtherTitles.Where(x => x.Title == blockRequest[i].Title2).Any());

                                if(query.Count()==0)
                                {
                                    if (blockRequest[i].Messsage!=string.Empty)
                                    {
                                        blockRequest[i].Messsage += ";";
                                    }
                                    blockRequest[i].Messsage += "Không tìm thấy tiêu đề tác phẩm";
                                }                                
                            }                           
                            //var workByTitleList = query.ToList();//loc tieu de tac pham

                            #endregion

                            #region Code
                            if(blockRequest[i].WorkCode == "4903614")
                            {
                                //int a = 1;
                            }
                            if (_matching.IsMatchingByWorkCode)
                            {
                                if(isChangeWorkcode)
                                {
                                    query = query.Where(p => p.WK_INT_NO == blockRequest[i].WorkcodeChangeToNew);
                                }
                                else
                                {
                                    query = query.Where(p => p.WK_INT_NO == blockRequest[i].WorkCode);
                                }
                                
                                if (query.Count() == 0)
                                {
                                    if (blockRequest[i].Messsage != string.Empty)
                                    {
                                        blockRequest[i].Messsage += ";";
                                    }
                                    blockRequest[i].Messsage += "Không tìm thấy mã tác phẩm";
                                }
                            }
                            //else
                            //{
                            //    query = workByTitleList;
                            //}
                            var workByCodeList = query.ToList();
                            #endregion

                            #region writer
                            List<WorkViewModel> workByWriterList = new List<WorkViewModel>();
                            if(workByCodeList.Count>0)
                            {
                                if (_matching.IsMatchingByWriter)
                                {

                                    #region loc theo ten tac gia
                                    workByWriterList = workByCodeList
                                        .Where(P => P.InterestedParties
                                            .Where(x => blockRequest[i].ListWriter2.Contains(x.IP_NAME)).Any())
                                        .ToList();                                    
                                    //tinh ty le tac gia chinh xac
                                    int totalWriter = blockRequest[i].ListWriter2.Count;
                                    int totalWriterMatching = 0;
                                    foreach (var workRes in workByWriterList)
                                    {
                                        totalWriterMatching = 0;
                                        for (int ik = 0; ik < blockRequest[i].ListWriter2.Count; ik++)
                                        {
                                            var fx = workRes.InterestedParties.Where(p => p.IP_NAME == blockRequest[i].ListWriter2[ik]).FirstOrDefault();
                                            if (fx != null)
                                            {
                                                totalWriterMatching++;
                                                
                                                if (fx.Society == "NS")
                                                {
                                                    workRes.TotalNonMember++;
                                                }
                                                else
                                                {
                                                    workRes.TotalMember++;
                                                    if(fx.Society == "VCPMC")
                                                    {
                                                        workRes.TotalMemberVcpmc++;
                                                    }
                                                    
                                                }
                                            }
                                        }
                                        workRes.TotalWriterRequest = totalWriter;
                                        workRes.TotalWriterMatching = totalWriterMatching;
                                        if (workRes.TotalWriterRequest != 0)
                                        {
                                            workRes.RateWriterMatch = (decimal)workRes.TotalWriterMatching / (decimal)workRes.TotalWriterRequest * 100;
                                        }
                                        else
                                        {
                                            workRes.RateWriterMatch = 100;//khong co tac gia luon dung
                                        }

                                    }

                                    workByWriterList = workByWriterList.Where(p => p.RateWriterMatch >= MatchingRateWriter).ToList();
                                    if (workByWriterList.Count == 0)
                                    {
                                        if (blockRequest[i].Messsage != string.Empty)
                                        {
                                            blockRequest[i].Messsage += ";";
                                        }
                                        blockRequest[i].Messsage += $"Tỷ lệ tác giả nhỏ hơn {MatchingRateWriter}";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    workByWriterList = workByCodeList;
                                }
                            }
                            else
                            {
                                workByWriterList = workByCodeList;
                            }
                            //xác định tác phẩm trên có compate không
                            foreach (var item in workByWriterList)
                            {
                                if (item.InterestedParties.Where(p => p.WK_STATUS == "INCOMPLETE").Any())
                                {
                                    item.WK_STATUS = "INCOMPLETE";
                                }
                                else
                                {
                                    item.WK_STATUS = "COMPLETE";
                                }
                            }
                            #endregion

                            #region Artist
                            //List<WorkViewModel> workByArtist = new List<WorkViewModel>();
                            foreach (var item in workByWriterList)
                            {
                                if (_matching.MatchingRateArist == MatchingRateAristType.All)
                                {
                                    //ban dau cho thoa, neu 1 nghe sy kogn thoa, bo qua
                                    item.IsCheckMatchingArtist = true;
                                    foreach (var subartist in blockRequest[i].ListArtist2)
                                    {
                                        var x = workByWriterList.Where(p => p.ListArtist.Contains(subartist)).ToList();
                                        if (x.Count == 0)
                                        {
                                            item.IsCheckMatchingArtist = false;
                                            break;
                                        }
                                    }
                                }
                                else if (_matching.MatchingRateArist == MatchingRateAristType.Exist)
                                {
                                    var workByArtist = workByWriterList.Where(p => blockRequest[i].ListArtist2.Where(x => item.ListArtist.Contains(x)).Any()).ToList();
                                    foreach (var subWork in workByArtist)
                                    {
                                        subWork.IsCheckMatchingArtist = true;
                                    }
                                }
                                else
                                {
                                    item.IsCheckMatchingArtist = true;
                                }
                            }
                            List<WorkViewModel> workByArist = null;
                            workByArist = workByWriterList;
                            if (_matching.isMatchingByArtist)
                            {
                                if(workByArist.Count > 0)
                                {
                                    workByArist = workByArist.Where(p => p.IsCheckMatchingArtist == true).ToList();
                                    if (workByArist.Count == 0)
                                    {
                                        if (blockRequest[i].Messsage != string.Empty)
                                        {
                                            blockRequest[i].Messsage += ";";
                                        }
                                        blockRequest[i].Messsage += "Nghệ sỹ biểu diễn không thoả điều kiện";
                                    }
                                }
                                
                            }                            
                            if(workByArist.Count!= workByWriterList.Count)
                            {
                                //int a = 1;
                            }
                            #endregion

                            if (_matching.MatchingGetCount == MatchingGetCountType.First)
                            {
                                #region sort lay theo do uu tien
                                if (workByArist != null && workByArist.Count > 0)
                                {
                                    switch (priorityValue)
                                    {
                                        case 0:
                                            #region  1.Tit > Wr > Art > Mem > COM > Rating > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                       
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                //Add more
                                                .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                                .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                       
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                .ToList();
                                            }
                                            
                                            break;
                                        #endregion
                                        case 1:
                                            #region  2.Tit > Wr > Art > Mem > Rating > COM > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                                                               
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                //Add more
                                                .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                                .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                                                               
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                .ToList();
                                            }
                                            
                                            break;
                                        #endregion
                                        case 2:
                                            #region  3.Tit > Wr > Art > Rating > Mem > COM > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM 
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               //Add more
                                               .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                               .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM 
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               .ToList();
                                            }
                                           
                                            break;
                                        #endregion
                                        case 3:
                                            #region  4.Tit > Wr > Art > COM > Mem > Rating > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                 
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất                                                              
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                //Add more
                                                .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                                .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                 
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất                                                              
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                .ToList();
                                            }
                                            
                                            break;
                                        #endregion
                                        case 4:
                                            #region  5.Tit > Wr > Art > COM > Rating > Mem > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM       
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất   
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               //Add more
                                               .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                               .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM       
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất   
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               .ToList();
                                            }
                                           
                                            break;
                                        #endregion
                                        case 5:
                                            #region  6.Tit > Wr > Art > Rating > COM > Mem > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                 
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất  
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               //Add more
                                               .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                               .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                               .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                               .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                               .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                               .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                               .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                 
                                               .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất  
                                               .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                               .ToList();
                                            }
                                           
                                            break;
                                        #endregion
                                        default:
                                            #region  1.Tit > Wr > Art > Mem > COM > Rating > Non - mem
                                            if (isPrioriryForVcpmc)
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                       
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng
                                                //Add more
                                                .OrderByDescending(p => p.TotalMemberVcpmc)//uu tien VCPMC
                                                .ToList();
                                            }
                                            else
                                            {
                                                workByArist = workByArist
                                                .OrderByDescending(p => p.TotalWriterMatching)//lấy số tác giả matching nhiều nhất
                                                .OrderBy(p => (p.InterestedParties.Count - p.TotalWriterRequest))//số tác giả lệch so với kho it nhất
                                                .OrderBy(p => p.IsCheckMatchingArtist)//nếu có matching theo nghệ sỹ biểu diễn
                                                .OrderByDescending(p => p.TotalMember)//ưu tiên có số tác giả matching đúng là member nhiều nhất
                                                .OrderBy(p => p.WK_STATUS == "COMPLETE")//ưu tiên COM                                       
                                                .OrderByDescending(p => p.StarRating)//ưu tiên rating
                                                .OrderByDescending(p => p.TotalNonMember)//ưu tiên số lượng tác giả Non-member cuối cùng                                                
                                                .ToList();
                                            }
                                            
                                            break;
                                            #endregion
                                    }                                    
                                }
                                #endregion
                            }
                            if (workByArist != null && workByArist.Count > 0)
                            {
                                #region set matching
                                blockRequest[i].WorkCodeMatching = workByArist[0].WK_INT_NO;
                                blockRequest[i].TitleMatching = workByArist[0].TTL_ENG;
                                blockRequest[i].WriterMatching = workByArist[0].WRITER;
                                blockRequest[i].InterestedPartiesMatching = workByArist[0].InterestedParties;

                                if (_matching.MatchingGetCount == MatchingGetCountType.First)
                                {
                                    #region first
                                    blockRequest[i].IsSuccess = true;
                                    blockRequest[i].Messsage = $"Tỷ lệ tác giả {workByArist[0].TotalWriterMatching}/{workByArist[0].TotalWriterRequest}" +
                                        $"({workByArist[0].RateWriterMatch.ToString("##,##")}%) ;Số lượng tìm thấy {workByArist.Count}";
                                    #endregion
                                }
                                else
                                {
                                    #region Unique
                                    if (workByArist.Count == 1)
                                    {
                                        blockRequest[i].IsSuccess = true;
                                        blockRequest[i].Messsage = $"Tỷ lệ tác giả {workByArist[0].TotalWriterMatching}/{workByArist[0].TotalWriterRequest}" +
                                            $"({workByArist[0].RateWriterMatch.ToString("##,##")}%) ;Số lượng tìm thấy {workByArist.Count}";
                                    }
                                    else
                                    {
                                        if (blockRequest[i].Messsage != string.Empty)
                                        {
                                            blockRequest[i].Messsage = ";";
                                        }
                                        blockRequest[i].Messsage += $"Số lượng tìm thấy {workByArist.Count} > 1";
                                        blockRequest[i].IsSuccess = false;
                                    }
                                    #endregion
                                }
                                #endregion

                                #region thông tin tac gia
                                foreach (var item in workByArist[0].InterestedParties)
                                {
                                    blockRequest[i].WriterCodeMatching += $"{item.IP_INT_NO},";
                                    blockRequest[i].WriterIPNumberMatching += $"{item.IP_NUMBER},";
                                    blockRequest[i].SocietyMatching += $"{item.Society},";
                                    if (item.Society != string.Empty)
                                    {
                                        blockRequest[i].WriterMatchingWithSoceity += $"{item.IP_NAME}({item.Society}),";
                                    }
                                }
                                blockRequest[i].ArtistMatching = workByArist[0].ARTIST;

                                if (blockRequest[i].WriterCodeMatching.Length > 0)
                                    blockRequest[i].WriterCodeMatching = blockRequest[i].WriterCodeMatching.Substring(0, blockRequest[i].WriterCodeMatching.Length - 1);

                                if (blockRequest[i].WriterIPNumberMatching.Length > 0)
                                    blockRequest[i].WriterIPNumberMatching = blockRequest[i].WriterIPNumberMatching.Substring(0, blockRequest[i].WriterIPNumberMatching.Length - 1);

                                if (blockRequest[i].SocietyMatching.Length > 0)
                                    blockRequest[i].SocietyMatching = blockRequest[i].SocietyMatching.Substring(0, blockRequest[i].SocietyMatching.Length - 1);

                                if (blockRequest[i].WriterMatchingWithSoceity.Length > 0)
                                    blockRequest[i].WriterMatchingWithSoceity = blockRequest[i].WriterMatchingWithSoceity.Substring(0, blockRequest[i].WriterMatchingWithSoceity.Length - 1);
                                #endregion
                            }
                        }
                        #endregion

                        #region Update UI
                        DateTime endtime = DateTime.Now;
                        richinfo.Invoke(new MethodInvoker(delegate
                        {
                            richinfo.Text = "";
                            richinfo.Text += $"Total record(s): {dataReponse.ResultObj.TotalRecords}{Environment.NewLine}";
                            richinfo.Text += $"Page index: {dataReponse.ResultObj.PageIndex}{Environment.NewLine}";
                            richinfo.Text += $"Page count: {dataReponse.ResultObj.PageCount}{Environment.NewLine}";
                            richinfo.Text += $"Page size: {dataReponse.ResultObj.PageSize}{Environment.NewLine}";
                            richinfo.Text += $"Start time(search): {startTime.ToString("HH:mm:ss")}{Environment.NewLine}";
                            richinfo.Text += $"End time(search): {endtime.ToString("HH:mm:ss")}{Environment.NewLine}";
                            richinfo.Text += $"Time response(sec(s)): {(endtime - startTime).TotalSeconds.ToString("##0.00")}{Environment.NewLine}";
                        }));
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = $"request/total: {index}/{dataLoad.Count}";
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
                    else
                    {
                        #region Update UI
                        totalRequestFailure++;
                        DateTime endtime = DateTime.Now;
                        richinfo.Invoke(new MethodInvoker(delegate
                        {
                            richinfo.Text = "";
                            richinfo.Text += $"Reponse timeout, total times fail: {totalRequestFailure}, request again!{Environment.NewLine}";                            
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
                    for (int i = 0; i < dgvMain.Rows.Count; i++)
                    {
                        if((bool)dgvMain.Rows[i].Cells["IsSuccess"].Value)
                        {
                            dgvMain.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                        }                        
                    }
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = $"Matching is finish, total time {(DateTime.Now - TheFiestTime).TotalSeconds}(s)";
                    lbInfo.Text += $", total matching success/total: {dataLoad.Where(p => p.IsSuccess).Count()}/{dataLoad.Count}";
                }));
                btnMatching.Invoke(new MethodInvoker(delegate
                {
                    btnMatching.Enabled = true;
                }));
                btnStop.Invoke(new MethodInvoker(delegate
                {
                    btnStop.Enabled = false;
                }));
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = true;
                }));
                isStop = false;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Matching is finish";
                }));
                #endregion

                //isMatcingDone = true;
            }
            catch (Exception)
            {
                #region ex
                isStop = false;
                if (btnStop != null && !btnStop.IsDisposed)
                {
                    btnStop.Invoke(new MethodInvoker(delegate
                    {
                        btnStop.Enabled = true;
                    }));
                }
                if (btnMatching != null && !btnMatching.IsDisposed)
                {
                    btnMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnMatching.Enabled = true;
                    }));
                }
                if (btnResetMatching != null && !btnResetMatching.IsDisposed)
                {
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = true;
                    }));
                }
                if (btnCalcMono != null && !btnCalcMono.IsDisposed)
                {
                    btnCalcMono.Invoke(new MethodInvoker(delegate
                    {
                        btnCalcMono.Enabled = true;
                    }));
                }
                if (lbInfo != null && !lbInfo.IsDisposed)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Matching is failure";
                    }));
                }
                #endregion
            }
        }

        private void SetMonopoly(MonopolyType monopolyType, WorkMatchingViewModel ItemRequest,MonopolyViewModel mono, bool isWorkMono, string nameWriter = "")
        {
            #region Doc quyen
            string MemberMonopolyNote = string.Empty;
            string _seperate = "----";
            switch (monopolyType)
            {
                case MonopolyType.Not:
                    break;
                case MonopolyType.All:
                    #region All
                    if (mono.Tone ||//1
                            mono.Web ||//2
                            mono.Performances ||//3
                            mono.PerformancesHCM ||//4
                            mono.Cddvd ||//5
                            mono.Kok ||//6
                            mono.Broadcasting ||//7
                            mono.Entertaiment ||//8
                            mono.Film ||//9
                            mono.Advertisement ||//10
                            mono.PubMusicBook ||//11
                            mono.Youtube ||//12
                            mono.Other//13
                           )
                    {
                        if(isWorkMono)
                        {
                            
                            string fields = string.Empty;
                            if (mono.Web) fields += ",Tone";
                            if (mono.Web) fields += ",Web";
                            if (mono.Performances) fields += ",Performances";
                            if (mono.PerformancesHCM) fields += ",PerformancesHCM";
                            if (mono.Cddvd) fields += ",Cddvd";
                            if (mono.Kok) fields += ",Kok";
                            if (mono.Broadcasting) fields += ",Broadcasting";
                            if (mono.Entertaiment) fields += ",Entertaiment";
                            if (mono.Film) fields += ",Film";
                            if (mono.Advertisement) fields += ",Advertisement";
                            if (mono.PubMusicBook) fields += ",PubMusicBook";
                            if (mono.Youtube) fields += ",Youtube";
                            if (mono.Other) fields += ",Other";
                            if (fields.Length > 1) fields = fields.Substring(1, fields.Length - 1);
                            ItemRequest.WorkFields = $"{monopolyType.ToString()}:{fields}";
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            string fields = string.Empty;
                            if (mono.Web) fields += ",Tone";
                            if (mono.Web) fields += ",Web";
                            if (mono.Performances) fields += ",Performances";
                            if (mono.PerformancesHCM) fields += ",PerformancesHCM";
                            if (mono.Cddvd) fields += ",Cddvd";
                            if (mono.Kok) fields += ",Kok";
                            if (mono.Broadcasting) fields += ",Broadcasting";
                            if (mono.Entertaiment) fields += ",Entertaiment";
                            if (mono.Film) fields += ",Film";
                            if (mono.Advertisement) fields += ",Advertisement";
                            if (mono.PubMusicBook) fields += ",PubMusicBook";
                            if (mono.Youtube) fields += ",Youtube";
                            if (mono.Other) fields += ",Other";
                            if (fields.Length > 1) fields = fields.Substring(1, fields.Length - 1);
                            ItemRequest.MemberFields = $"{monopolyType.ToString()}:{fields}";
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        } 
                    }                    
                    break;
                #endregion
                case MonopolyType.Tone:
                    #region Tone
                    if (mono.Tone)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }                    
                    break;
                #endregion
                case MonopolyType.Web:
                    #region Web
                    if (mono.Web)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }                    
                    break;
                #endregion

                case MonopolyType.Performances:
                    #region Performances
                    if (mono.Performances)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }                   
                    break;
                #endregion

                case MonopolyType.PerformancesHCM:
                    #region PerformancesHCM
                    if (mono.PerformancesHCM)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                       
                    }                    
                    break;
                #endregion

                case MonopolyType.Cddvd:
                    #region Cddvd
                    if (mono.Cddvd)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }                    
                    break;
                #endregion

                case MonopolyType.Kok:
                    #region Kok
                    if (mono.Kok)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }                    
                    break;
                #endregion

                case MonopolyType.Broadcasting:
                    #region Broadcasting
                    if (mono.Broadcasting)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.Entertaiment:
                    #region Entertaiment
                    if (mono.Entertaiment)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.Film:
                    #region Film
                    if (mono.Film)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.Advertisement:
                    #region Advertisement
                    if (mono.Advertisement)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.PubMusicBook:
                    #region PubMusicBook
                    if (mono.PubMusicBook)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.Youtube:
                    #region Youtube
                    if (mono.Youtube)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                case MonopolyType.Other:
                    #region Other
                    if (mono.Other)
                    {
                        if (isWorkMono)
                        {
                            ItemRequest.WorkFields = monopolyType.ToString();
                            ItemRequest.WorkMonopolyNote = mono.NoteMono;
                            ItemRequest.IsWorkMonopoly = true;
                        }
                        else
                        {
                            ItemRequest.MemberFields = monopolyType.ToString();
                            MemberMonopolyNote = nameWriter == string.Empty ? mono.NoteMono : $"({nameWriter}) {mono.NoteMono}";
                            if (ItemRequest.MemberMonopolyNote.Length > 0)
                            {
                                ItemRequest.MemberMonopolyNote += _seperate;
                            }
                            ItemRequest.MemberMonopolyNote += MemberMonopolyNote;
                            ItemRequest.IsMemberMonopoly = true;
                        }                        
                    }
                    break;
                #endregion

                default:
                    break;
            }
            #endregion
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
        //private void ConvertTo(List<WorkMatchingViewModel> dataLoad)
        //{
        //    try
        //    {
        //        if (dataLoad == null || dataLoad.Count == 0)
        //        {
        //            return;
        //        }
        //        foreach (var item in dataLoad)
        //        {
        //            if (item.Title != string.Empty) item.Title2 = VnHelper.ConvertToUnSign(item.Title).Replace("(", "").Replace(")", "").ToUpper();
        //            if (item.Writer != string.Empty) item.Writer2 = VnHelper.ConvertToUnSign(item.Writer).Replace("(", "").Replace(")", "").ToUpper();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        private void ResetStatus()
        {
            try
            {
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = false;
                }));
                if (dataLoad == null)
                {
                    dataLoad = new List<WorkMatchingViewModel>();
                }
                if (dataLoad.Count == 0)
                {
                    return;
                }
                foreach (var item in dataLoad)
                {
                    item.IsMatching = false;
                    item.IsSuccess = false;
                    //thong tin tac pham matching
                    item.TitleMatching = string.Empty;                    
                    item.WorkCodeMatching = string.Empty;
                    //thong tin nghe sy mathhing
                    item.ArtistMatching = string.Empty;                   
                    //thong tin tac gia matching
                    item.InterestedPartiesMatching.Clear();
                    item.SocietyMatching = string.Empty;
                    item.WriterCodeMatching = string.Empty;
                    item.WriterMatching = string.Empty;
                    item.WriterMatchingWithSoceity = string.Empty;
                    item.WriterIPNumberMatching = string.Empty;                  
                    //doc quyen tac gia
                    item.MemberFields = string.Empty;                    
                    item.MemberMonopolyNote = string.Empty;                    
                    item.IsMemberMonopoly = false;
                    //do quyen tac pham
                    item.WorkFields = string.Empty;
                    item.WorkMonopolyNote = string.Empty;
                    item.IsWorkMonopoly = false;
                    // ghi chu matching
                    item.Messsage = string.Empty;
                }
                dgvMain.Invoke(new MethodInvoker(delegate
                {
                    dgvMain.DataSource = dataLoad;
                }));
                btnResetMatching.Invoke(new MethodInvoker(delegate
                {
                    btnResetMatching.Enabled = true;
                }));
            }
            catch (Exception)
            {
                if (btnResetMatching != null && !btnResetMatching.IsDisposed)
                {
                    btnResetMatching.Invoke(new MethodInvoker(delegate
                    {
                        btnResetMatching.Enabled = true;
                    }));
                }
            }
        }
        private void reRefreshUI()
        {
            try
            {
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    cboType.SelectedIndex = 0;
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    txtFind.Text = string.Empty;
                }));

                if (dataLoad == null)
                {
                    dataLoad = new List<WorkMatchingViewModel>();
                }
                else
                {
                    if (dataLoad.Count > 0)
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
                            lbLoad.Text = $"Refresh data";
                        }));
                        statusMain.Invoke(new MethodInvoker(delegate
                        {
                            lbOperation.Text = "Refresh data";
                        }));
                    }
                }
            }
            catch (Exception)
            {
                if (toolMain != null && !toolMain.IsDisposed)
                {
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnRefresh.Enabled = true;
                    }));
                }
            }
        }
        #endregion

        #region Export
        private void btnExportS_Click(object sender, EventArgs e)
        {

        }
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
            int pos = filePath.LastIndexOf('.');
            string x1 = filePath.Substring(0, pos);
            string x2 = filePath.Substring(pos + 1, filePath.Length - pos - 1);
            string[] file = new string[] { x1, x2 };
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
                //All
                if (typeMatchingExport==0)
                {
                    if (dataLoad.Count % numsItemPerFileExport == 0)
                    {
                        totalFile = dataLoad.Count / numsItemPerFileExport;
                    }
                    else
                    {
                        totalFile = dataLoad.Count / numsItemPerFileExport + 1;
                    }
                    int serial = 0;
                    int index = 0;
                    while (index < dataLoad.Count)
                    {
                        serial++;
                        var datax = dataLoad.Skip(index).Take(numsItemPerFileExport).ToList();
                        index += numsItemPerFileExport;
                        bool check = WriteReportHelper.ExportWorkMatching(datax, $"{path}\\{name}{serial.ToString().PadLeft(3, '0')}.{extension}", typeExport);
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
                //successfull matching
                else if (typeMatchingExport == 1)
                {
                    var dataSuc = dataLoad.Where(p => p.IsSuccess == true).ToList();
                    if(dataSuc.Count>0)
                    {
                        if (dataSuc.Count % numsItemPerFileExport == 0)
                        {
                            totalFile = dataSuc.Count / numsItemPerFileExport;
                        }
                        else
                        {
                            totalFile = dataSuc.Count / numsItemPerFileExport + 1;
                        }
                        int serial = 0;
                        int index = 0;
                        while (index < dataSuc.Count)
                        {
                            serial++;
                            var datax = dataSuc.Skip(index).Take(numsItemPerFileExport).ToList();
                            index += numsItemPerFileExport;
                            bool check = WriteReportHelper.ExportWorkMatching(datax, $"{path}\\{name}{serial.ToString().PadLeft(3, '0')}.{extension}",typeExport);
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
                    
                }
                //failure matching
                else if (typeMatchingExport == 2)
                {
                    var dataFailue = dataLoad.Where(p => p.IsSuccess == false).ToList();
                    if (dataFailue.Count > 0)
                    {
                        if (dataFailue.Count % numsItemPerFileExport == 0)
                        {
                            totalFile = dataFailue.Count / numsItemPerFileExport;
                        }
                        else
                        {
                            totalFile = dataFailue.Count / numsItemPerFileExport + 1;
                        }
                        int serial = 0;
                        int index = 0;
                        while (index < dataFailue.Count)
                        {
                            serial++;
                            var datax = dataFailue.Skip(index).Take(numsItemPerFileExport).ToList();
                            index += numsItemPerFileExport;
                            bool check = WriteReportHelper.ExportWorkMatching(datax, $"{path}\\{name}{serial.ToString().PadLeft(3, '0')}.{extension}", typeExport);
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
                }
            }
            statusMain.Invoke(new MethodInvoker(delegate
            {
                lbOperation.Text = "Export to excel be finish";
            }));
            toolMain.Invoke(new MethodInvoker(delegate
            {
                btnExportS.Enabled = true;
            }));

            statusMain.Invoke(new MethodInvoker(delegate
            {
                progressBarImport.Value = 100;
            }));

        }
        #endregion

        #region CBO
        private void cboUnique_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnique.SelectedIndex == 0)
            {
                cboPriority.Visible = false;
                lbPriority.Visible = false;
                cboPrioriryForVcpmc.Visible = false;
            }   
            else
            {
                cboPriority.Visible = true;
                lbPriority.Visible = true;
                cboPrioriryForVcpmc.Visible = true;
            }
        }
        private void cboPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            priorityValue = cboPriority.SelectedIndex;
        }
        private void cboPrioriryForVcpmc_CheckedChanged(object sender, EventArgs e)
        {
            isPrioriryForVcpmc = cboPrioriryForVcpmc.Checked;
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
                //isMatcingDone = false;
                btnFirstPAge.Enabled = false;
                btnPrevPage.Enabled = false;
                btnNxtPage.Enabled = false;
                btnLastPage.Enabled = false;
                txtPageCurrent.ReadOnly = true;
                //get link
                filepath = string.Empty;
                //
                isChangeWorkcode = cheChangeWorkcode.Checked;
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
                //replate
                if (cboReplateTextType.SelectedIndex == 0)
                {
                    isReplateTitle = false;
                    isReplateWriter = false;
                }
                else if (cboReplateTextType.SelectedIndex == 1)
                {
                    isReplateTitle = true;
                    isReplateWriter = false;
                }
                else if (cboReplateTextType.SelectedIndex == 2)
                {
                    isReplateTitle = false;
                    isReplateWriter = true;
                }
                else
                {
                    isReplateTitle = true;
                    isReplateWriter = true;
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
        private void btnMatching_Click(object sender, EventArgs e)
        {
            try
            {
                //isMatcingDone = false;

                _matching.IsMatchingByTitle = cheTitle.Checked;
                _matching.IsMatchingByWriter = cheWriter.Checked;
                _matching.isMatchingByArtist = cheArtist.Checked;
                _matching.IsMatchingByWorkCode = cheWorkCode.Checked;

                _matching.MatchingTitle = MatchingTitleType.Match;
                /*
                 100%
                    75%
                    50%
                    25%
                    0%
                 */
                switch (cboRateWriter.SelectedIndex)
                {
                    case 0:
                        _matching.MatchingRateWriter = MatchingRateWriterType._100;
                        break;
                    case 1:
                        _matching.MatchingRateWriter = MatchingRateWriterType._75;
                        break;
                    case 2:
                        _matching.MatchingRateWriter = MatchingRateWriterType._50;
                        break;
                    case 3:
                        _matching.MatchingRateWriter = MatchingRateWriterType._25;
                        break;
                    default:
                        _matching.MatchingRateWriter = MatchingRateWriterType._50;
                        break;
                }
                /*
                    All
                    Exist
                    Not
                 */
                switch (cboRateArtist.SelectedIndex)
                {
                    case 0:
                        _matching.MatchingRateArist = MatchingRateAristType.All;
                        break;
                    case 1:
                        _matching.MatchingRateArist = MatchingRateAristType.Exist;
                        break;
                    case 2:
                        _matching.MatchingRateArist = MatchingRateAristType.Not;
                        break;
                    default:
                        _matching.MatchingRateArist = MatchingRateAristType.Not;
                        break;
                }
                /*
                 Unique
                    First
                 */
                switch (cboUnique.SelectedIndex)
                {
                    case 0:
                        _matching.MatchingGetCount = MatchingGetCountType.Unique;
                        break;
                    case 1:
                        _matching.MatchingGetCount = MatchingGetCountType.First;
                        break;                   
                    default:
                        _matching.MatchingGetCount = MatchingGetCountType.Unique;
                        break;
                }
                
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
                    btnExportS.Enabled = false;
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
        int typeExport = 0;
        int typeMatchingExport = 0;
        int numsItemPerFileExport = 10000;
        private void btnExportFileUpMis_Click(object sender, EventArgs e)
        {
            try
            {
                typeExport = 0;
                typeMatchingExport = cboTypeMatching.SelectedIndex;
                numsItemPerFileExport = int.Parse(cboNumsItem.Text);
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
                    btnExportS.Enabled = false;
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

        private void btnExportDetail_Click(object sender, EventArgs e)
        {
            try
            {
                typeExport = 1;
                typeMatchingExport = cboTypeMatching.SelectedIndex;
                numsItemPerFileExport = int.Parse(cboNumsItem.Text);
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
                    btnExportS.Enabled = false;
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
                List<WorkMatchingViewModel> fill = new List<WorkMatchingViewModel>();
                if (cboTypeChoise == 0)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Title).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.Title.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                    var query = dataLoad.Where(c => c.TitleMatching.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                    var query = dataLoad.Where(c => c.Writer.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                    var query = dataLoad.Where(c => c.WriterMatching.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WorkCode).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.WorkCode.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 5)
                {
                    //var query = dataLoad.Where(delegate (WorkMatchingViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WorkCodeMatching).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = dataLoad.Where(c => c.WorkCodeMatching.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
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
                else if (Operation == OperationType.ResetStatus)
                {
                    ResetStatus();
                }
                else if (Operation == OperationType.RefreshUI)
                {
                    reRefreshUI();
                }
                else if (Operation == OperationType.GetDataFromServer)
                {
                    Mathching();
                }
                else if (Operation == OperationType.FindMonopoly)
                {
                    FindMonopoly();
                }
                else if (Operation == OperationType.Filter)
                {
                    FilterData();
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(filepath);
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

        #region Do doc quyen
        private void btnCalcMono_Click(object sender, EventArgs e)
        {
            try
            {
                #region loai doc quyen
                /*
                    Not
                    All
                    Tone
                    Web
                    Performances
                    PerformancesHCM
                    Cddvd
                    Kok
                    Broadcasting
                    Entertaiment
                    Film
                    Advertisement
                    PubMusicBook
                    Youtube
                    Other
                 */
                monopolyType = (MonopolyType)cboMonopolyType.SelectedIndex;
                #endregion

                if (dataLoad==null|| dataLoad.Count==0)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "data is empty, so don't find monopoly";
                    }));
                    return;
                }

                #region set backgroundWorker
                    Operation = OperationType.FindMonopoly;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
                #endregion  
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async void FindMonopoly()
        {
            try
            {
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "Staring find monopoly...";
                }));
                btnCalcMono.Invoke(new MethodInvoker(delegate
                {
                    btnCalcMono.Enabled = false;
                }));
                //0.load fix parameter
                #region Chuyen tu none member sang to member
                fixrequest.Type = TypeFixParameter.NonMemberToMember.ToString();
                fixdata = await fixcontroller.GetAllPaging(fixrequest);
                List<CheckMember> listNon = new List<CheckMember>();
                if (fixdata != null && fixdata.ResultObj != null && fixdata.ResultObj.Items != null && fixdata.ResultObj.Items.Count > 0)
                {
                    #region 3.Data fix
                    var data1 = fixdata.ResultObj.Items.Where(p => p.Type == TypeFixParameter.NonMemberToMember.ToString()).ToList();
                    string value = string.Empty;
                    for (int i = 0; i < data1.Count; i++)
                    {
                        value = data1[i].Key;
                        if (value.Length > 2)
                        {
                            listNon.Add(new CheckMember
                            {
                                IpCode = value.Substring(0, value.Length - 2),
                                IpNameType = value.Substring(value.Length - 2, 2),
                                CheckValue = data1[i].Value1,
                                ToValue = data1[i].Value2,
                            });
                        }
                    }                    
                    #endregion
                }
                #endregion

                #region chuyen sang E
                fixrequest.Type = TypeFixParameter.IpWorkRoleToE.ToString();
                fixdata = await fixcontroller.GetAllPaging(fixrequest);
                List<CheckMember> listE = new List<CheckMember>();
                if (fixdata != null && fixdata.ResultObj != null && fixdata.ResultObj.Items != null && fixdata.ResultObj.Items.Count > 0)
                {
                    var data2 = fixdata.ResultObj.Items.Where(p => p.Type == TypeFixParameter.IpWorkRoleToE.ToString()).ToList();
                    string value = string.Empty;
                    for (int i = 0; i < data2.Count; i++)
                    {
                        value = data2[i].Key;
                        if (value.Length > 2)
                        {
                            listE.Add(new CheckMember
                            {
                                IpCode = value.Substring(0, value.Length - 2),
                                IpNameType = value.Substring(value.Length - 2, 2),
                                CheckValue = data2[i].Value1,
                                ToValue = data2[i].Value2,
                            });
                        }
                    }
                }
                #endregion
                
                #region 1.tai doc quyen
                List<MonopolyViewModel> monoWorks = new List<MonopolyViewModel>();
                List<MonopolyViewModel> monoMembers = new List<MonopolyViewModel>();
                monoRequest.Group = 0;
                monoRequest.PageIndex = 1;
                var monoDataWorkSer = await MonoController.GetAllPaging(monoRequest);
                if (monoDataWorkSer != null && monoDataWorkSer.ResultObj != null && monoDataWorkSer.ResultObj.Items != null && monoDataWorkSer.ResultObj.Items.Count > 0)
                {
                    monoWorks = monoDataWorkSer.ResultObj.Items;
                }
                monoRequest.Group = 1;
                monoRequest.PageIndex = 1;
                var monoDatamemberSer = await MonoController.GetAllPaging(monoRequest);
                if (monoDatamemberSer != null && monoDatamemberSer.ResultObj != null && monoDatamemberSer.ResultObj.Items != null && monoDatamemberSer.ResultObj.Items.Count > 0)
                {
                    monoMembers = monoDatamemberSer.ResultObj.Items;
                }
                #endregion

                #region 2. tinh toan doc quyen        
                string valueFix = string.Empty;
                foreach (var item in dataLoad)
                {
                    #region reset before reset
                    item.WorkFields = string.Empty;
                    item.WorkMonopolyNote = string.Empty;
                    item.IsWorkMonopoly = false;

                    item.MemberFields = string.Empty;
                    item.MemberMonopolyNote = string.Empty;
                    item.IsMemberMonopoly = false;
                    item.NonMember = string.Empty;
                    #endregion

                    #region 5.chuyen doi
                    if (listE.Count > 0)
                    {
                        foreach (var subI in item.InterestedPartiesMatching)
                        {
                            //khong co ma hoa xac dinh CA,C,E,A thi bo qua
                            if (subI.IP_INT_NO == string.Empty || subI.IP_WK_ROLE == string.Empty)
                            {
                                continue;
                            }
                            if (ConvertNonMembertoMemberHelper.ConvertValueFixParameter(listE, subI.IP_INT_NO, subI.IP_NAMETYPE, subI.IP_WK_ROLE, ref valueFix))
                            {
                                subI.IP_WK_ROLE = valueFix;
                            }
                        }
                    }
                    if (listNon.Count > 0)
                    {
                        foreach (var subI in item.InterestedPartiesMatching)
                        {
                            //khong co ma hoa xac dinh CA,C,E,A thi bo qua
                            if (subI.IP_INT_NO == string.Empty || subI.Society == string.Empty)
                            {
                                continue;
                            }
                            if (ConvertNonMembertoMemberHelper.ConvertValueFixParameter(listE, subI.IP_INT_NO, subI.IP_NAMETYPE, subI.Society, ref valueFix))
                            {
                                subI.Society = valueFix;
                            }
                        }
                    }
                    #endregion

                    #region 6.nonmember
                    foreach (var inP in item.InterestedPartiesMatching)
                    {
                        if (inP.Society == "NS")
                        {
                            if (item.NonMember.Length > 0)
                            {
                                item.NonMember += ", ";
                            }
                            item.NonMember += inP.IP_NAME_LOCAL == string.Empty? inP.IP_NAME : inP.IP_NAME_LOCAL;
                        }
                        else
                        {
                            //int a = 1;
                        }
                    }
                    item.NonMember = item.NonMember.Trim();
                    #endregion

                    #region độc quyền tác phầm
                    if (monoWorks.Count>0)
                    { 
                        var monoW = monoWorks.Where(p => p.CodeNew == item.WorkCodeMatching).ToList();
                        if (monoW.Count > 0)
                        {
                            if (monoW[0].IsExpired || monoW[0].EndTime <= DateTime.Now)
                            {
                                item.WorkFields = string.Empty;
                                item.WorkMonopolyNote = "HẾT HẠN ĐỘC QUYỀN";
                                item.IsWorkMonopoly = false;
                            }
                            else
                            {
                                SetMonopoly(monopolyType, item, monoW[0], true);
                            }
                            //chi can mot tac gia doc quyen la bo qua
                            if (item.IsWorkMonopoly)
                            {
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Độc quyền tác giả
                    if(monoMembers.Count>0)
                    {
                        foreach (var subI in item.InterestedPartiesMatching)
                        {                           
                            var monoM = monoMembers.Where(p => p.CodeNew == subI.IP_INT_NO && p.NameType == subI.IP_NAMETYPE).ToList();
                            if (monoM.Count > 0)
                            {

                                for (int k = 0; k < monoM.Count; k++)
                                {
                                    if (monoM[k].IsExpired || monoM[k].EndTime <= DateTime.Now)
                                    {
                                        //item.MemberFields = string.Empty;
                                        //item.MemberMonopolyNote = "HẾT HẠN ĐỘC QUYỀN";
                                        //item.IsMemberMonopoly = false;
                                    }
                                    else
                                    {
                                        SetMonopoly(monopolyType, item, monoM[k], false, subI.IP_NAME_LOCAL==string.Empty? subI.IP_NAME : subI.IP_NAME_LOCAL);
                                    }                                   
                                }                                
                            }
                        }
                        item.MemberMonopolyNote = item.MemberMonopolyNote.Trim();
                    }                    
                    #endregion
                }
                #endregion

                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "End find monopoly...";
                }));
                btnCalcMono.Invoke(new MethodInvoker(delegate
                {
                    btnCalcMono.Enabled = true;
                }));
            }
            catch (Exception)
            {

                if (btnCalcMono != null && !btnCalcMono.IsDisposed)
                {
                    btnCalcMono.Invoke(new MethodInvoker(delegate
                    {
                        btnCalcMono.Enabled = true;
                    }));
                }
            }
        }

        #endregion        
    }
}
