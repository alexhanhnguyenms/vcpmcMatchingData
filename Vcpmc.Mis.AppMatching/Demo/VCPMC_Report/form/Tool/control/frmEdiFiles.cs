﻿using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vcpmc.Mis.ApplicationCore.Entities.control;
using Vcpmc.Mis.AppMatching.Controllers.System;
using Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis;
using Vcpmc.Mis.AppMatching.Services.System;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.Common.Member;
using Vcpmc.Mis.Common.search;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.Shared.Mis.Members;
using Vcpmc.Mis.Shared.Parameter;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.Utilities.Common;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.AppMatching.form.Tool.control
{
    public partial class frmEdiFiles : Form
    {        
        List<EdiFilesItem> ediFilesItems = new List<EdiFilesItem>();
        List<EdiFilesItem> ediFilesItemsClone = new List<EdiFilesItem>();
        int totalLoad = 0;
        int totalRest = 0;
        int totalRestAffCalc = 0;
        int totalDuplicate = 0;
        OperationType Operation = OperationType.LoadExcel;
        WorkController workController;
        WorkApiClient workApiClient;
        MonopolyController MonoController;
        MonopolyApiClient monoApiClient;
        GetMonopolyPagingRequest monoRequest = new GetMonopolyPagingRequest();
        //ApiResult<PagedResult<MonopolyViewModel>> monoData = new ApiResult<PagedResult<MonopolyViewModel>>();
        MonopolyType monopolyType = MonopolyType.Not;
        //
        FixParameterController fixcontroller;
        FixParameterApiClient fixapiClient;
        GetFixParameterPagingRequest fixrequest = new GetFixParameterPagingRequest();
        ApiResult<PagedResult<FixParameterViewModel>> fixdata = new ApiResult<PagedResult<FixParameterViewModel>>();

        bool isGroupByMember = false;
        bool isSync = false;
        bool isLoad = false;
        bool isFindMono = false;        
        bool isFilter = false;        
        bool _comareTitleAndWriter = false;
        /// <summary>
        /// loai bao cao nhan tu mis
        /// </summary>
        int GenerateType = 0;
        int CompareTW = 0;
        float rateWriterMatched = 0;
        //private BindingSource bindingSourceImport = new BindingSource();
        //private BindingSource bindingSourceEdit = new BindingSource();
        #region init
        public frmEdiFiles()
        {
            InitializeComponent();
            
        }
        private void frmEdiFiles_Load(object sender, EventArgs e)
        {
#if DEBUG
            //txtPath.Text = @"D:\Solution\Source Code\Matching data\data example\Phan phoi\2002\total\PP2002 - STT.xlsx";
            //txtPath1.Text = @"D:\Solution\Source Code\Matching data\data example\Phan phoi\2002\details";
#endif
            workApiClient = new WorkApiClient(Core.Client);
            workController = new WorkController(workApiClient);
            monoApiClient = new MonopolyApiClient(Core.Client);
            MonoController = new MonopolyController(monoApiClient);
            fixapiClient = new FixParameterApiClient(Core.Client);
            fixcontroller = new FixParameterController(fixapiClient);
            cboMonopolyType.SelectedIndex = 0;
            isGroupByMember = cheGroupMemberWithcmo.Checked;
            cboMatchedType.SelectedIndex = 0;
            _comareTitleAndWriter = cheCompareTitleAndWriter.Checked;
            if(radCompareTW.Checked)
            {
                CompareTW = 0;
            }
            else
            {
                CompareTW = 1;
            }
            cboRateWriterMatched.SelectedIndex = 2;
            cboType.SelectedIndex = 1;
            //dgvEditFileImport.DataSource = bindingSourceImport;
            //dgvEditCalcXXX.DataSource = bindingSourceEdit;

            //bindingSourceImport.BindingComplete +=
            //    new BindingCompleteEventHandler(bindingSource_BindingComplete);

            //bindingSourceEdit.BindingComplete +=
            //    new BindingCompleteEventHandler(bindingSource_BindingComplete);
        }
        //private void bindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        //{
        //    if (e.BindingCompleteContext == BindingCompleteContext.DataSourceUpdate
        //        && e.Exception == null)
        //        e.Binding.BindingManagerBase.EndCurrentEdit();
        //}
        #endregion

        #region btn
        private void cboMonopolyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                monopolyType = (MonopolyType)cboMonopolyType.SelectedIndex;
                //Proccessing();
            }
            catch (Exception)
            {
                
            }          
        }
        //int d = 1;
        BindingSource bSource = new BindingSource();
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cboRateWriterMatched.SelectedIndex)
                {
                    case 0:
                        rateWriterMatched = 100;
                        break;
                    case 1:
                        rateWriterMatched = 75;
                        break;
                    case 2:
                        rateWriterMatched = 50;
                        break;
                    case 3:
                        rateWriterMatched = 25;
                        break;
                    case 4:
                        rateWriterMatched = 0;
                        break;
                    default:
                        rateWriterMatched = 100;
                        break;
                }
                GenerateType = cboMatchedType.SelectedIndex;
                _comareTitleAndWriter = cheCompareTitleAndWriter.Checked;              
                #region display
                if (_comareTitleAndWriter)
                {
                    dgvEditCalcXXX.Columns["IscheckCompareTitleAndWriterx"].Visible = true;
                    dgvEditCalcXXX.Columns["MesssageCompareTitleAndWriterx"].Visible = true;
                    dgvEditCalcXXX.Columns["CountMatchWriter"].Visible = true;
                    dgvEditCalcXXX.Columns["TotalWriter"].Visible = true;
                }  
                else
                {
                    dgvEditCalcXXX.Columns["IscheckCompareTitleAndWriterx"].Visible = false;
                    dgvEditCalcXXX.Columns["MesssageCompareTitleAndWriterx"].Visible = false;
                    dgvEditCalcXXX.Columns["CountMatchWriter"].Visible = false;
                    dgvEditCalcXXX.Columns["TotalWriter"].Visible = false;
                }

                if(GenerateType == 0|| GenerateType == 1)
                {
                    //thong thuong
                    dgvEditCalcXXX.Columns["IpSetNox"].Visible = false;
                    dgvEditCalcXXX.Columns["IpInNox"].Visible = false;
                    dgvEditCalcXXX.Columns["LocalIpIntNox"].Visible = false;
                    dgvEditCalcXXX.Columns["NameNox"].Visible = false;
                    dgvEditCalcXXX.Columns["IpNamex"].Visible = false;
                    dgvEditCalcXXX.Columns["IpName2x"].Visible = false;
                    dgvEditCalcXXX.Columns["IpNameTypex"].Visible = false;
                    dgvEditCalcXXX.Columns["IpWorkRolex"].Visible = false;
                    dgvEditCalcXXX.Columns["IpNamex"].Visible = false;
                    dgvEditCalcXXX.Columns["IpNameLocal2x"].Visible = false;
                    dgvEditCalcXXX.Columns["Societyx"].Visible = false;
                   
                    dgvEditCalcXXX.Columns["PerOwnShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["PerColShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["MecOwnShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["TotalMecShrx"].Visible = false;
                  
                    dgvEditCalcXXX.Columns["SpShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["TotalMecShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["SynOwnShrx"].Visible = false;
                    dgvEditCalcXXX.Columns["SynColShrx"].Visible = false;
                }
                else
                {
                    //phan phoi                  
                    dgvEditCalcXXX.Columns["IpSetNox"].Visible = true;
                    dgvEditCalcXXX.Columns["IpInNox"].Visible = true;
                    dgvEditCalcXXX.Columns["LocalIpIntNox"].Visible = true;
                    dgvEditCalcXXX.Columns["NameNox"].Visible = true;
                    dgvEditCalcXXX.Columns["IpNamex"].Visible = true;
                    dgvEditCalcXXX.Columns["IpName2x"].Visible = true;
                    dgvEditCalcXXX.Columns["IpNameTypex"].Visible = true;
                    dgvEditCalcXXX.Columns["IpWorkRolex"].Visible = true;
                    dgvEditCalcXXX.Columns["IpNamex"].Visible = true;
                    dgvEditCalcXXX.Columns["IpNameLocal2x"].Visible = true;
                    dgvEditCalcXXX.Columns["Societyx"].Visible = true;

                    dgvEditCalcXXX.Columns["PerOwnShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["PerColShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["MecOwnShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["TotalMecShrx"].Visible = true;

                    dgvEditCalcXXX.Columns["SpShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["TotalMecShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["SynOwnShrx"].Visible = true;
                    dgvEditCalcXXX.Columns["SynColShrx"].Visible = true;


                }
                #endregion
               
                if (isLoad)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load data...";
                    }));
                    return;
                }
                if (isFindMono)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting find mono...";
                    }));
                }
                if (isSync)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting Sync data...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (radCompareTW.Checked)
                {
                    CompareTW = 0;
                }
                else
                {
                    CompareTW = 1;
                }
                monopolyType = (MonopolyType)cboMonopolyType.SelectedIndex;
                if (tstxtPath.Text=="")
                {
                    MessageBox.Show("Please choise file to import");
                    return;
                }
                //LoadDtaFromExcel();
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
        private void btnChoiseFile_Click(object sender, EventArgs e)
        {
            try
            {
                tstxtPath.Text = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        tstxtPath.Text = filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}");
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
                    LoadDtaFromExcel();

                }
                else if (Operation == OperationType.FindMonopoly)
                {
                    FindMonopoly();

                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(currentDirectory,typeExport);

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
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Visible = false;
        }
        #endregion

        #region data
        private async void LoadDtaFromExcel()
        {
            try
            {                
                #region init   
                int NoOfPerf = 0;
                int seqNo = 0;
                isLoad = true;
                isGroupByMember = cheGroupMemberWithcmo.Checked;
                ExcelHelper excelHelper = new ExcelHelper();
                int countread = 0;
                int countGood = 0;
                int countGood1 = 0;
                #endregion

                #region reset               
                totalLoad = 0;
                totalRest = 0;
                totalRestAffCalc = 0;
                totalDuplicate = 0;
                lbTotalLoad.Invoke(new MethodInvoker(delegate
                {
                    lbTotalLoad.Text = totalLoad.ToString();
                }));
                lbTotalrest.Invoke(new MethodInvoker(delegate
                {
                    lbTotalrest.Text = totalRest.ToString();
                }));
                lbTotalAftercalc.Invoke(new MethodInvoker(delegate
                {
                    lbTotalAftercalc.Text = totalRestAffCalc.ToString();
                }));
                lbTotalDuplicate.Invoke(new MethodInvoker(delegate
                {
                    lbTotalDuplicate.Text = totalDuplicate.ToString();
                }));
                ediFilesItems = new List<EdiFilesItem>();
                ediFilesItemsClone = new List<EdiFilesItem>();
                dgvEditFileImport.Invoke(new MethodInvoker(delegate
                {
                    dgvEditFileImport.DataSource = ediFilesItems;
                }));
                dgvEditCalcXXX.Invoke(new MethodInvoker(delegate
                {
                    dgvEditCalcXXX.DataSource = ediFilesItemsClone;                    
                }));
                #endregion

                //du lieu doc file
                ediFilesItems = excelHelper.ReadExcelEditFile(tstxtPath.Text, out countread, out countGood, out countGood1, GenerateType);
                
                #region 1.Loai bo dong trong   
                List<EdiFilesItem> editFiles = new List<EdiFilesItem>();
                if (ediFilesItems != null)
                {                   

                    ediFilesItemsClone.Clear();
                    string WorkInternalNo = string.Empty;
                    string IpInNo = string.Empty;
                    seqNo = -1;
                    int index_i = -1;
                    NoOfPerf = 0;
                    //bool isAdd = false;
                    for (int i = 0; i < ediFilesItems.Count; i++)
                    {
                        //isAdd = false;
                        #region general report: thuong dung
                        if (GenerateType == 0)
                        {
                            if (ediFilesItems[i].NoOfPerf > 0)
                            {
                                NoOfPerf = ediFilesItems[i].NoOfPerf;
                            }
                            if (ediFilesItems[i].NoOfPerf > 0)
                            {
                                WorkInternalNo = ediFilesItems[i].WorkInternalNo;
                                IpInNo = ediFilesItems[i].IpInNo;
                                seqNo = ediFilesItems[i].seqNo;
                                index_i = i;
                                ediFilesItemsClone.Add((EdiFilesItem)ediFilesItems[i].Clone());
                                //isAdd = true;
                            }
                            else
                            {
                                #region referent no
                                if (i > 0 && ediFilesItemsClone.Count > 0)
                                {
                                    //cap nhat NoOfPerf neu chua co
                                    if (NoOfPerf > 0 && ediFilesItemsClone[ediFilesItemsClone.Count - 1].NoOfPerf == 0)
                                    {
                                        ediFilesItemsClone[ediFilesItemsClone.Count - 1].NoOfPerf = NoOfPerf;
                                    }
                                    //cong cac WorkArtist neu bij troi xuong dong duoi
                                    if (ediFilesItems[i].WorkArtist != string.Empty)
                                    {
                                        ediFilesItemsClone[ediFilesItemsClone.Count - 1].WorkArtist =
                                            ediFilesItemsClone[ediFilesItemsClone.Count - 1].WorkArtist + " " + ediFilesItems[i].WorkArtist;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region general report ,new match report; phan phoi
                        else if (GenerateType == 1 || GenerateType == 2)
                        {
                            if (ediFilesItems[i].NoOfPerf > 0)
                            {
                                NoOfPerf = ediFilesItems[i].NoOfPerf;
                            }
                            //moi hang la mot seqNo tang dan
                            if (ediFilesItems[i].seqNo > 0)
                            {
                                WorkInternalNo = ediFilesItems[i].WorkInternalNo;
                                IpInNo = ediFilesItems[i].IpInNo;
                                seqNo = ediFilesItems[i].seqNo;
                                index_i = i;
                                ediFilesItemsClone.Add((EdiFilesItem)ediFilesItems[i].Clone());
                            }
                            else
                            {
                                #region referent no
                                if (i > 0 && ediFilesItemsClone.Count > 0)
                                {
                                    //cap nhat NoOfPerf neu chua co
                                    if (NoOfPerf > 0 && ediFilesItemsClone[ediFilesItemsClone.Count - 1].NoOfPerf == 0)
                                    {
                                        ediFilesItemsClone[ediFilesItemsClone.Count - 1].NoOfPerf = NoOfPerf;
                                    }
                                    //cong cac WorkArtist neu bij troi xuong dong duoi
                                    if (ediFilesItems[i].WorkArtist != string.Empty)
                                    {
                                        ediFilesItemsClone[ediFilesItemsClone.Count - 1].WorkArtist =
                                            ediFilesItemsClone[ediFilesItemsClone.Count - 1].WorkArtist + " " + ediFilesItems[i].WorkArtist;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion     
                    }        
                    totalRest = ediFilesItemsClone.Count;
                    lbTotalrest.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalrest.Text = totalRest.ToString();
                    }));
                }
                else
                {
                    ediFilesItemsClone.Clear();
                }
                #endregion               

                #region 3.lay thong tin tac gia
                string[] arraIpname = null;
                string[] arraComposer = null;
                string Ip_name1 = string.Empty;
                string Ip_name2 = string.Empty;
                string HoAndTen = string.Empty;
                string writerReal = string.Empty;
                string[] arrWriterReal = null;
                string text = string.Empty;
                string text2 = string.Empty;
                for (int i = 0; i < ediFilesItemsClone.Count; i++)
                {
                    if(ediFilesItemsClone[i].WorkInternalNo == "19176136")
                    {
                        //int a = 1;
                    }
                    WorkCreateRequest itmUpdate = new WorkCreateRequest();

                    #region Lây thông tin tác giả
                    //viet nam dao ngược
                    //tieng anh không => Ip_name1 và Ip_name2
                    /*
                     1.
                         IpName: LE VAN,  LOC [hoặc VU,  HOANG]
                         WorkComposer LOC LE VAN, HOANG VU
                     2.
                        IpName: ALI,  TAMPOSI
                        WorkComposer   JUSTIN TRANTER, ROBIN LENNART FREDRIKSSON, MATTIAS GUNNAR LARSSON, ALI TAMPOSI, KARLA CAMILA CABELLO
                     */
                    Ip_name1 = string.Empty;//ten 
                    Ip_name2 = string.Empty;
                    HoAndTen = string.Empty;                    
                    if (ediFilesItemsClone[i].IpWorkRole == "E")
                    {
                        writerReal = ediFilesItemsClone[i].IpName.Trim();
                        if(writerReal.Substring(writerReal.Length-1,1)==",")
                        {
                            writerReal = writerReal.Substring(0, writerReal.Length - 1);
                        }
                    }
                    else
                    {
                        #region Lay ten chinh xac tac gia
                        writerReal = string.Empty;
                        arraIpname = ediFilesItemsClone[i].IpName.Split(',');//PHAM BAO,  NAM
                        arraComposer = ediFilesItemsClone[i].WorkComposer.Split(',');// LOC LE VAN, HOANG VU

                        if (ediFilesItemsClone[i].IpName != string.Empty)
                        {
                            if (arraIpname.Length == 1)
                            {
                                writerReal = ediFilesItemsClone[i].IpName;
                            }
                        }

                        for (int ipN = 0; ipN < arraIpname.Length; ipN++)
                        {
                            HoAndTen += arraIpname[ipN].Trim() + " ";
                            Ip_name1 += arraIpname[ipN].Replace(" ", "").Trim();
                            Ip_name2 += arraIpname[arraIpname.Length - 1 - ipN].Replace(" ", "").Trim();
                        }
                        for (int com = 0; com < arraComposer.Length; com++)
                        {
                            if (arraComposer[com].Replace(" ", "").Trim() == Ip_name1)
                            {
                                //tieng anh                            
                                writerReal = arraComposer[com].Trim();
                                break;
                            }
                            else if (arraComposer[com].Replace(" ", "").Trim() == Ip_name2)
                            {
                                //tieng viet                            
                                writerReal = HoAndTen.Trim();
                                break;
                            }
                        }
                        #endregion
                    }
                    //khong tim duoc tac giả thì bỏ qua bài này
                    if (writerReal == string.Empty)
                    {
                        //int a = 1;
                        continue;
                    }
                    ediFilesItemsClone[i].IpName2 = writerReal;
                    #endregion

                    #region nếu là tác giả việt
                    if (ediFilesItemsClone[i].IpNameLocal.Length > 0)
                    {
                        text = ediFilesItemsClone[i].IpNameLocal.Replace(" ", "");
                        text2 = VnHelper.ConvertToUnSign(text);
                        arrWriterReal = writerReal.Split(' ');
                        for (int k = 0; k < arrWriterReal.Length; k++)
                        {
                            int pos = text2.IndexOf(arrWriterReal[k]);
                            if (pos >= 0)
                            {
                                int x = text.Length;
                                int x1 = text2.Length;
                                ediFilesItemsClone[i].IpNameLocal2 += text.Substring(pos, arrWriterReal[k].Length) + " ";
                                text = text.Substring(arrWriterReal[k].Length, text.Length - arrWriterReal[k].Length);
                                text2 = text2.Substring(arrWriterReal[k].Length, text2.Length - arrWriterReal[k].Length);
                            }
                            else
                            {

                            }
                        }
                        ediFilesItemsClone[i].IpNameLocal2 = ediFilesItemsClone[i].IpNameLocal2.Trim();
                    }
                    #endregion                   
                }
                #endregion

                #region Chuyen doi mot so tac gia 
                if(ediFilesItemsClone.Count > 0 )
                {
                    string valueFix = string.Empty;                   

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

                    #region Chuyen doi mo so quyen tac gia
                    foreach (var item in ediFilesItemsClone)
                    {
                        if (listE.Count > 0)
                        {
                            if (ConvertNonMembertoMemberHelper.ConvertValueFixParameterMember(listE, item.IpInNo, item.IpNameType, ref valueFix))
                            {
                                item.IpWorkRole = valueFix;
                            }
                        }
                        if (listNon.Count > 0)
                        {
                            if (ConvertNonMembertoMemberHelper.ConvertValueFixParameter(listNon, item.IpInNo, item.IpNameType, item.Society, ref valueFix))
                            {
                                item.Society = valueFix; ;
                            }
                        }
                    }
                    #endregion

                }
                #endregion
                
                #region nhom tac gia
                List<EdiFilesItem> editGroup = new List<EdiFilesItem>();
                NoOfPerf = -1;
                seqNo = -1;
                string cmos = string.Empty;
                for (int i = 0; i < ediFilesItemsClone.Count; i++)
                {
                    if (ediFilesItemsClone[i].seqNo == 104)
                    {
                        //int a = 1;
                    }
                    EdiFilesItem editGroupXXX = null;
                    if (GenerateType == 0|| GenerateType ==1)
                    {
                        if (ediFilesItemsClone[i].NoOfPerf != NoOfPerf)
                        {
                            NoOfPerf = ediFilesItemsClone[i].NoOfPerf;
                            var itemGroup = (EdiFilesItem)ediFilesItemsClone[i].Clone();
                            itemGroup.WorkStatus = "COMPLETE";
                            editGroup.Add(itemGroup);
                        }
                        editGroupXXX = editGroup.Where(p => p.NoOfPerf == NoOfPerf).FirstOrDefault();
                    }   
                    else
                    {
                        if (ediFilesItemsClone[i].seqNo != seqNo)
                        {
                            seqNo = ediFilesItemsClone[i].seqNo;
                            var itemGroup = (EdiFilesItem)ediFilesItemsClone[i].Clone();
                            itemGroup.WorkStatus = "COMPLETE";
                            editGroup.Add(itemGroup);
                        }
                        editGroupXXX = editGroup.Where(p => p.seqNo == seqNo).FirstOrDefault();
                    }
                    if (editGroupXXX != null)
                    {
                        //chi can mot UNIDENTIFIED, incomlate la in
                        if (ediFilesItemsClone[i].WorkStatus == "UNIDENTIFIED" && (editGroupXXX.WorkStatus == "COMPLETE" || editGroupXXX.WorkStatus == "INCOMPLETE"))
                        {
                            editGroupXXX.WorkStatus = "UNIDENTIFIED";
                        }

                        if (ediFilesItemsClone[i].WorkStatus == "INCOMPLETE" && (editGroupXXX.WorkStatus != "UNIDENTIFIED" && editGroupXXX.WorkStatus == "COMPLETE"))
                        {
                            editGroupXXX.WorkStatus = "INCOMPLETE";
                        }
                        //Author, music, lyrics ListInterestedParty
                        editGroupXXX.ListInterestedParty.Add(new Shared.Mis.Works.InterestedParty
                        {
                            IP_INT_NO = ediFilesItemsClone[i].IpInNo,
                            IP_NAME = ediFilesItemsClone[i].IpName2,
                            IP_NAME_LOCAL = ediFilesItemsClone[i].IpNameLocal2 == string.Empty? ediFilesItemsClone[i].IpName2 : ediFilesItemsClone[i].IpNameLocal2,
                            IP_NAMETYPE = ediFilesItemsClone[i].IpNameType,
                            Society = ediFilesItemsClone[i].Society

                        });
                        cmos = cheGroupMemberWithcmo.Checked == false ? "" : $" ({ediFilesItemsClone[i].Society})";
                        if(cmos == " (NS)")
                        {
                            cmos = " (Không Xác Định)";
                        }
                        if (ediFilesItemsClone[i].IpWorkRole == "A")
                        {
                            #region lyrics
                            if (!editGroupXXX.ListGroupLyricsCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupLyricsCode.Add(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType);
                                if (editGroupXXX.GroupLyrics.Length > 0)
                                {
                                    editGroupXXX.GroupLyrics += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupLyrics += ediFilesItemsClone[i].IpNameLocal2 + cmos;
                                }
                                else
                                {
                                    editGroupXXX.GroupLyrics += ediFilesItemsClone[i].IpName2 + cmos;
                                }                                
                            }
                            #endregion
                        }
                        else if (ediFilesItemsClone[i].IpWorkRole == "C")
                        {
                            #region composer
                            if (!editGroupXXX.ListGroupComposerCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupComposerCode.Add(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType);
                                if (editGroupXXX.GroupComposer.Length > 0)
                                {
                                    editGroupXXX.GroupComposer += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupComposer += ediFilesItemsClone[i].IpNameLocal2 + cmos;
                                }
                                else
                                {
                                    editGroupXXX.GroupComposer += ediFilesItemsClone[i].IpName2 + cmos;
                                }
                            }
                            #endregion
                        }
                        else if (ediFilesItemsClone[i].IpWorkRole == "CA")
                        {
                            #region composer, lyrics
                            //neu chua co nhac, them nhac
                            if (!editGroupXXX.ListGroupComposerCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupLyricsCode.Add(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType);
                                if (editGroupXXX.GroupComposer.Length > 0)
                                {
                                    editGroupXXX.GroupComposer += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupComposer += ediFilesItemsClone[i].IpNameLocal2 + cmos;
                                }
                                else
                                {
                                    editGroupXXX.GroupComposer += ediFilesItemsClone[i].IpName2 + cmos;
                                }
                            }
                            //neu chua co loi them loi
                            if (!editGroupXXX.ListGroupLyricsCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupComposerCode.Add(ediFilesItemsClone[i].IpName2 + cmos);
                                if (editGroupXXX.GroupLyrics.Length > 0)
                                {
                                    editGroupXXX.GroupLyrics += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupLyrics += ediFilesItemsClone[i].IpNameLocal2 + cmos;
                                }
                                else
                                {
                                    editGroupXXX.GroupLyrics += ediFilesItemsClone[i].IpName2 + cmos;
                                }
                            }
                            #endregion
                        }
                        else if (ediFilesItemsClone[i].IpWorkRole == "E")
                        {
                            #region publiser
                            if (!editGroupXXX.ListGroupLyricsCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupPublisherCode.Add(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType);
                                if (editGroupXXX.GroupPublisher.Length > 0)
                                {
                                    editGroupXXX.GroupPublisher += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupPublisher += ediFilesItemsClone[i].IpNameLocal2 + cmos;
                                }
                                else
                                {
                                    editGroupXXX.GroupPublisher += ediFilesItemsClone[i].IpName2 + cmos;
                                }
                            }
                            #endregion
                        }

                        if (ediFilesItemsClone[i].IpWorkRole != "E")
                        {
                            #region writer
                            if (!editGroupXXX.ListGroupWriterCode.Contains(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType))
                            {
                                editGroupXXX.ListGroupWriterCode.Add(ediFilesItemsClone[i].IpInNo + ediFilesItemsClone[i].IpNameType);
                                editGroupXXX.ListGroupWriterName.Add(ediFilesItemsClone[i].IpName2);
                                if (editGroupXXX.GroupWriter.Length > 0)
                                {
                                    editGroupXXX.GroupWriter += ", ";
                                }
                                if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                                {
                                    editGroupXXX.GroupWriter += ediFilesItemsClone[i].IpNameLocal2 + cmos;                                        
                                }
                                else
                                {
                                    editGroupXXX.GroupWriter += ediFilesItemsClone[i].IpName2 + cmos;                                        
                                }
                            }
                            #endregion
                        }
                        //danh sach tac gia voi ten xuat ra
                        if (!editGroupXXX.DicMember.ContainsKey($"{ediFilesItemsClone[i].IpInNo}{ediFilesItemsClone[i].IpNameType}"))
                        {
                            if (ediFilesItemsClone[i].IpNameLocal2 != string.Empty)
                            {
                                editGroupXXX.DicMember.Add($"{ediFilesItemsClone[i].IpInNo}{ediFilesItemsClone[i].IpNameType}", ediFilesItemsClone[i].IpNameLocal2);
                            }
                            else
                            {
                                editGroupXXX.DicMember.Add($"{ediFilesItemsClone[i].IpInNo}{ediFilesItemsClone[i].IpNameType}", ediFilesItemsClone[i].IpName2);
                            }
                        }
                    }
                    else
                    {
                        //int a = 1;
                    }
                }
                ediFilesItemsClone.Clear();
                ediFilesItemsClone = editGroup;
                #endregion

                #region so sanh
                string coposerTemp = string.Empty;
                if (_comareTitleAndWriter)
                {
                    string[] arrComposerInput = null;
                    string messsagecompareTitleAndWriter = string.Empty;
                    bool isCheckcomareTitle = false;
                    bool isCheckcomareWriter = false;
                    int countMatchWriter = 0;
                    bool worngWriter = false;
                    string[] arrWorkTtileOutUnSign = null;
                    string[] arrWorkTtileOut = null;
                    string WorkTtileOutRemove = string.Empty;
                    foreach (var item in ediFilesItemsClone)
                    {
                        //if(item.WorkInternalNo == "17582584" )
                        //{
                        //    int a = 1;
                        //}
                        //if (item.WorkInternalNo == "19176136")
                        //{
                        //    int a = 1;
                        //}
                        messsagecompareTitleAndWriter = string.Empty;
                        isCheckcomareTitle = true;
                        isCheckcomareWriter = true;
                        #region So sanh tieu de
                        //bang 1 trang cac tieu de la dung
                        arrWorkTtileOut = item.WorkTitle.Split(',');
                        arrWorkTtileOutUnSign = VnHelper.ConvertToUnSign(item.WorkTitle).Split(',');
                        //                        
                        arrWorkTtileOut = ListGetTile(arrWorkTtileOut, item.IpNameLocal).ToArray();
                        arrWorkTtileOutUnSign = new string[arrWorkTtileOut.Length];
                        for (int ml = 0; ml < arrWorkTtileOut.Length; ml++)
                        {
                            arrWorkTtileOutUnSign[ml] = VnHelper.ConvertToUnSign(arrWorkTtileOut[ml]);
                        }
                        //
                        
                        isCheckcomareTitle = false;
                        if (arrWorkTtileOutUnSign != null && arrWorkTtileOutUnSign.Length > 0)
                        {
                            int[] scoreTitleInput = new int[arrWorkTtileOutUnSign.Length];
                            for (int workOut = 0; workOut < arrWorkTtileOutUnSign.Length; workOut++)
                            {
                                if (CompareTW == 0)
                                {
                                    scoreTitleInput[workOut] = LevenshteinDistance.Compute(item.Title, arrWorkTtileOutUnSign[workOut].Trim());
                                    if (item.Title == arrWorkTtileOutUnSign[workOut].Trim())
                                    {
                                        isCheckcomareTitle = true;
                                        //break;
                                    }
                                }
                                else
                                {
                                    WorkTtileOutRemove = VnHelper.RemoveSpecialCharactor(arrWorkTtileOutUnSign[workOut].Trim());
                                    scoreTitleInput[workOut] = LevenshteinDistance.Compute(item.Title3, WorkTtileOutRemove);
                                    if (WorkTtileOutRemove.Contains(item.Title3))
                                    {
                                        isCheckcomareTitle = true;
                                        //break;
                                    }
                                }

                            }
                            //lay ten gan giong nhat
                            //int posSame = -1;
                            string titleSame = string.Empty;
                            var min = scoreTitleInput.Min();                           
                            for (int ig = 0; ig < arrWorkTtileOutUnSign.Length; ig++)
                            {
                                if (scoreTitleInput[ig] == min)
                                {
                                    titleSame = arrWorkTtileOut[ig];
                                    //posSame = ig;
                                    break;
                                }
                            }
                            //TODO
                            //Chinh sua tieu de
                            //GetTile(item, titleSame,item.IpNameLocal);
                            item.WorkTitle2 = titleSame;
                            if (!isCheckcomareTitle)
                            {
                                if(item.Title.Length <= 10)
                                {
                                    if (min <= 1)
                                    {
                                        isCheckcomareTitle = true;
                                    }
                                }
                                else if (item.Title.Length > 10 && item.Title.Length <= 15)
                                {
                                    if (min <= 2)
                                    {
                                        isCheckcomareTitle = true;
                                    }
                                }
                                else if (item.Title.Length > 15 && item.Title.Length <= 20)
                                {
                                    if (min < 3)
                                    {
                                        isCheckcomareTitle = true;
                                    }
                                }
                                else if (item.Title.Length > 20 && item.Title.Length <= 25)
                                {
                                    if (min < 4)
                                    {
                                        isCheckcomareTitle = true;
                                    }
                                }
                                else 
                                {
                                    if (min <= 5)
                                    {
                                        isCheckcomareTitle = true;
                                    }
                                }

                            }
                        }

                        if (!isCheckcomareTitle)
                        {
                            messsagecompareTitleAndWriter = "sai tiêu đề";
                        }
                        #endregion

                        #region Lay tac gia dau vao(khong dung den)
                        arrComposerInput = item.Composer.Split(',');
                        for (int cI = 0; cI < arrComposerInput.Length; cI++)
                        {
                            arrComposerInput[cI] = arrComposerInput[cI].Trim().ToUpper();
                            if (arrComposerInput[cI] != string.Empty)
                            {
                                arrComposerInput[cI] = ConvertAllToUnicode.ConvertFromComposite(arrComposerInput[cI]);
                                arrComposerInput[cI] = VnHelper.ConvertToUnSign(arrComposerInput[cI]);
                                item.ListComposer.Add(arrComposerInput[cI]);
                            }
                        }
                        #endregion

                        #region Ap dung ta gia dau vao khong co dau phay
                        coposerTemp = item.Composer.Replace(",", "").Trim() + " " + item.Publisher.Replace(",", "").Trim();
                        worngWriter = false;
                        countMatchWriter = 0;
                        foreach (var ComposerOut in item.ListGroupWriterName)
                        {
                            if (ComposerOut == string.Empty)
                            {
                                continue;
                            }
                            if (coposerTemp.Length == 0)
                            {
                                if (item.Composer != string.Empty)
                                {
                                    if (!worngWriter)
                                    {
                                        if (messsagecompareTitleAndWriter.Length > 0)
                                        {
                                            messsagecompareTitleAndWriter += ", ";
                                        }
                                        messsagecompareTitleAndWriter += "sai tác giả";
                                        worngWriter = !worngWriter;
                                    }
                                    isCheckcomareWriter = false;
                                }
                                break;
                            }
                            if (coposerTemp.Contains(ComposerOut))
                            {
                                //if (coposerTemp.Length == 0)
                                //{
                                //    int a = 1;
                                //}
                                coposerTemp = coposerTemp.Replace(ComposerOut, "").Trim();
                                countMatchWriter++;
                            }
                            else
                            {

                                isCheckcomareWriter = false;
                                if (!worngWriter)
                                {
                                    if (messsagecompareTitleAndWriter.Length > 0)
                                    {
                                        messsagecompareTitleAndWriter += ", ";
                                    }
                                    messsagecompareTitleAndWriter += "sai tác giả";
                                    worngWriter = !worngWriter;
                                }
                                //break;
                            }

                        }
                        //cuoi cung danh sanh tac gia dau vao van con text => chac chan sai ten tac gia
                        if (coposerTemp.Trim() != string.Empty)
                        {
                            isCheckcomareWriter = false;
                            if (!worngWriter)
                            {
                                if (messsagecompareTitleAndWriter.Length > 0)
                                {
                                    messsagecompareTitleAndWriter += ", ";
                                }
                                messsagecompareTitleAndWriter += "sai tác giả";
                                worngWriter = !worngWriter;
                            }
                        }

                        item.CountMatchWriter = countMatchWriter;
                        item.TotalWriter = item.ListGroupWriterCode.Count;
                        if(!isCheckcomareWriter)
                        {
                            if (item.TotalWriter > 0)
                            {
                                float rate = ((float)item.CountMatchWriter / (float)item.TotalWriter)*100;
                                if (rate >= rateWriterMatched)
                                {
                                    isCheckcomareWriter = true;
                                }
                            }
                            else
                            {
                                isCheckcomareWriter = true;
                            }
                        }
                        if(isCheckcomareTitle && isCheckcomareWriter)
                        {
                            item.IscheckCompareTitleAndWriter = true;
                        }
                        else
                        {
                            item.IscheckCompareTitleAndWriter = false;
                            item.MesssageCompareTitleAndWriter = messsagecompareTitleAndWriter;
                        }
                        #endregion                        

                    }
                }
                #endregion

                #region Chinh sua tieu de
                
                //foreach (var item in ediFilesItemsClone)
                //{
                //    GetTile(item);
                //}
                #endregion                

                #region Hien thi du lieu goc
                //bindingSourceImport.DataSource = ediFilesItems;
                //bindingSourceEdit.DataSource = ediFilesItemsClone;
                if (ediFilesItems != null)
                {                    
                    totalLoad = ediFilesItems.Count;
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalLoad.Text = totalLoad.ToString();
                    }));

                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = ediFilesItems;                        
                    }));

                    dgvEditCalcXXX.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditCalcXXX.DataSource = ediFilesItemsClone;                        
                        for (int i = 0; i < dgvEditCalcXXX.Rows.Count; i++)
                        {
                            if (                               
                                dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value != null 
                                && (bool)dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value                               
                                )
                            {
                                dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            }
                            else
                            {
                                dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                            }
                        }
                    }));

                    totalRestAffCalc = ediFilesItemsClone.Count;
                    totalDuplicate = totalRest - totalRestAffCalc;

                    lbTotalAftercalc.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalAftercalc.Text = totalRestAffCalc.ToString();
                    }));
                    lbTotalDuplicate.Invoke(new MethodInvoker(delegate
                    {
                        lbTotalDuplicate.Text = totalDuplicate.ToString();
                    }));
                }  
                else
                {
                    lbTotalLoad.Text = "0";                    
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load data from Excel file be error!";
                    }));
                }
                #endregion
                isLoad = false;
            }
            catch (Exception ex)
            {
                isLoad = false;
                MessageBox.Show(ex.ToString());
            }
        }

        private static void GetTile(EdiFilesItem item,string titleSame,string ipLocalName)
        {
            
            string part1, part2, part22;
            int posL = 0;
            bool hasVn = false;
            bool hasSign = false;
            hasSign = false;
            hasVn = false;
            part1 = string.Empty;
            part2 = string.Empty;
            part22 = string.Empty;
            
            if(titleSame==string.Empty)
            {
                string[] arrayTitle = null;
                arrayTitle = item.WorkTitle.Trim().Split(',');
                if (arrayTitle != null && arrayTitle.Length > 0)
                {
                    titleSame = arrayTitle[0].Trim();
                }  
                else
                {
                    titleSame = item.WorkTitle;
                }
            }
            
            if (titleSame.Length > 0)
            {
                posL = titleSame.Length / 2;
                part1 = titleSame.Substring(0, posL);
                part1 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(part1.Trim()));
                part2 = titleSame.Substring(posL, titleSame.Length - posL);
                part22 = part2.Trim();
                hasSign = VnHelper.Detect(part2);
                //if(ipLocalName.Length == 0)
                //{
                //    int a=1;
                //}
                if(!hasSign && ipLocalName.Length >0)
                {
                    //TODO
                    //hasSign = VnHelper.Detect(ipLocalName);
                    //if(!hasSign)
                    //{
                    //    int a =1;
                    //}
                    hasSign = true;
                }
                //if (ipLocalName.Length > 0)
                //{
                //    if (VnHelper.Detect(part2) != VnHelper.Detect(ipLocalName))
                //    {
                //        int a = 1;
                //    }
                //}
                part2 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(part2.Trim()));
                if (part1 == part2 && hasSign)
                {
                    hasVn = true;
                }
            }
            if (!hasVn)
            {
                part22 = titleSame;
            }
            item.WorkTitle2 = part22;
            //sau khi xu ly, van la empty => gan bang goc
            if (item.WorkTitle2 ==string.Empty)
            {
                item.WorkTitle2 = item.WorkTitle;
            }            
        }
        private static List<string> ListGetTile(string[] ListtitleSame, string ipLocalName)
        {
            string part1, part2, part22;
            int posL = 0;
            bool hasVn = false;
            bool hasSign = false;
            hasSign = false;
            hasVn = false;
            part1 = string.Empty;
            part2 = string.Empty;
            part22 = string.Empty;
            List<string> listname = new List<string>();
            string titleSame = string.Empty;
            for (int i = 0; i < ListtitleSame.Length; i++)
            {
                titleSame = ListtitleSame[i].Trim();
                if(titleSame == string.Empty)
                {
                    continue;
                }
                #region tim ten local
                posL = titleSame.Length / 2;
                part1 = titleSame.Substring(0, posL);
                part1 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(part1.Trim()));
                part2 = titleSame.Substring(posL, titleSame.Length - posL);
                part22 = part2.Trim();
                hasSign = VnHelper.Detect(part2);
                //if (ipLocalName.Length == 0)
                //{
                //    int a = 1;
                //}
                if (!hasSign && ipLocalName.Length > 0)
                {
                    hasSign = true;
                }
                part2 = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(part2.Trim()));
                if (part1 == part2 && hasSign)
                {
                    hasVn = true;
                }
                if (!hasVn)
                {
                    part22 = titleSame;
                }
                listname.Add(part22);                
                #endregion
            }
            return listname;
        }

        private void SetMonopoly(MonopolyType monopolyType, EdiFilesItem ItemRequest, MonopolyViewModel mono, bool isWorkMono,string nameWriter = "")
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
                        if (isWorkMono)
                        {

                            string fields = string.Empty;
                            if (mono.Tone) fields += ",Tone";
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
                            if (mono.Tone) fields += ",Tone";
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
        #endregion

        #region Export
        private void sbbtnSaveDataToDB_Click(object sender, EventArgs e)
        {

        }
        string currentDirectory = "";
        private void tssExport_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    currentDirectory = "";
                    SaveFileDialog saveDlg = new SaveFileDialog();
                    saveDlg.InitialDirectory = @"C:\";
                    saveDlg.Filter = "Excel files (*.xls)|*.xlsx";
                    saveDlg.FilterIndex = 0;
                    saveDlg.RestoreDirectory = true;
                    saveDlg.Title = "Export Excel File To";
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        currentDirectory = saveDlg.FileName;
                    } 
                    else
                    {
                        return;
                    }
                    
                    
                    if(currentDirectory=="")
                    {
                        MessageBox.Show("name file is empty");
                        return;
                    }
                    
                    //OpenFileDialog folderBrowser = new OpenFileDialog();
                    
                    //folderBrowser.ValidateNames = false;
                    //folderBrowser.CheckFileExists = false;
                    //folderBrowser.CheckPathExists = true;
                    //// Always default to Folder Selection.
                    //folderBrowser.FileName = "Folder Selection.";
                    //if (folderBrowser.ShowDialog() == DialogResult.OK)
                    //{
                    //    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    //    currentDirectory = folderPath;
                    //}
                    if (currentDirectory == "")
                    {
                        return;
                    }                    
                    Operation = OperationType.ExportToExcel;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
                catch (Exception )
                {


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        int typeExport = 0;
        private void btnExportDefault_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    typeExport = 0;
                    currentDirectory = "";                   
                    //get link                  
                    using (SaveFileDialog openFileDialog = new SaveFileDialog())
                    {
                        //openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        openFileDialog.Filter = "Excel Files|*.xlsx";
                        //openFileDialog.InitialDirectory = "D:\\";                   
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            currentDirectory = openFileDialog.FileName;
                        }
                    }
                    if (currentDirectory == string.Empty)
                    {
                        MessageBox.Show("Please input file path");
                        return;
                    }                    
                    Operation = OperationType.ExportToExcel;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
                catch (Exception)
                {


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnbtnExportDetail_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    typeExport = 1;
                    currentDirectory = "";
                    //get link                  
                    using (SaveFileDialog openFileDialog = new SaveFileDialog())
                    {
                        //openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        openFileDialog.Filter = "Excel Files|*.xlsx";
                        //openFileDialog.InitialDirectory = "D:\\";                   
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            currentDirectory = openFileDialog.FileName;
                        }
                    }
                    if (currentDirectory == string.Empty)
                    {
                        MessageBox.Show("Please input file path");
                        return;
                    }  
                    Operation = OperationType.ExportToExcel;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
                catch (Exception)
                {


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExportDistribution_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    typeExport = 2;
                    currentDirectory = "";
                    //get link                  
                    using (SaveFileDialog openFileDialog = new SaveFileDialog())
                    {
                        //openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        openFileDialog.Filter = "Excel Files|*.xlsx";
                        //openFileDialog.InitialDirectory = "D:\\";                   
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            currentDirectory = openFileDialog.FileName;
                        }
                    }
                    if (currentDirectory == string.Empty)
                    {
                        MessageBox.Show("Please input file path");
                        return;
                    }                   
                    Operation = OperationType.ExportToExcel;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
                catch (Exception)
                {


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ExportToExcel(string folderPath, int type)
        {
            try
            {                
                bool check = WriteReportHelper.WriteExcelEditFiles(ediFilesItemsClone, folderPath, type);

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

        #endregion

        #region Sync data to work       
        private void btnSysToWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoad)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load data...";
                    }));
                    return;
                }
                if (isFindMono)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting find mono...";
                    }));
                }
                if (isSync)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting Sync data...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (ediFilesItemsClone == null || ediFilesItemsClone.Count == 0)
                {
                    MessageBox.Show("Data is empty, so not sync to Work");
                    return;
                }
                
                DialogResult = MessageBox.Show("Are you sure to sync data to Work?", "SYNC Confirm", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    Operation = OperationType.SysnData;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception)
            {


            }
        }
        private async void SysnData()
        {
            try
            {
                if (ediFilesItemsClone == null || ediFilesItemsClone.Count == 0)
                {
                    return;
                }
                isSync = true;
                int totalSuccess = 0;
                int total = ediFilesItemsClone.Count;
                
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
               
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnChoiseFile.Enabled = false;
                    btnImport.Enabled = false;                  
                    btnExport.Enabled = false;                    
                }));
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = false;
                }));
                
                DateTime startTime = DateTime.Now;

                WorkChangeListRequest request = new WorkChangeListRequest();
                //string[] arraIpname = null;
                //string[] arraComposer = null;
                string Ip_name1 = string.Empty;
                string Ip_name2 = string.Empty;
                string HoAndTen = string.Empty;
                string writerReal = string.Empty;
                //string[] arrWriterReal = null;
                string text = string.Empty;
                string text2 = string.Empty;
                for (int i = 0; i < ediFilesItemsClone.Count; i++)
                {
                    WorkCreateRequest itmUpdate = new WorkCreateRequest();

                    #region Lây thông tin tác giả
                    if(ediFilesItemsClone[i].IpName2==string.Empty)
                    {
                        continue;
                    }                    
                    #endregion

                    #region Xac dinh da co chua
                    if (!request.Items.Where(p => p.WK_INT_NO == ediFilesItemsClone[i].WorkInternalNo.ToUpper()).Any())
                    {
                        itmUpdate = new WorkCreateRequest();
                    }
                    else
                    {
                        itmUpdate = request.Items.Where(p => p.WK_INT_NO == ediFilesItemsClone[i].WorkInternalNo.ToUpper()).FirstOrDefault();
                    }
                    itmUpdate.WK_INT_NO = VnHelper.ConvertToUnSign(ediFilesItemsClone[i].WorkInternalNo.ToUpper());
                    #endregion

                    #region Thiet lap ten
                    //if (itmUpdate.TTL_ENG != string.Empty)
                    //{                       
                    //xu ly ten phu
                    //TODO
                    if (ediFilesItemsClone[i].WorkTitle.Length > 0)
                    {
                        //GOI TEN NGUOI YEU ,ALINE ,GỌI TÊN NGƯỜI YÊU GOI TEN NGUOI YEU
                        //GỌI TÊN NGƯỜI YÊU 
                        //GOI TEN NGUOI YEU
                        string[] ilistTitle = VnHelper.ConvertToUnSign(ediFilesItemsClone[i].WorkTitle.Trim().ToUpper()).Split(',');                         
                        string s1 = "";
                        string s2 = "";                      
                        int len_div1 = 0;
                        int len_div2 = 0;
                        string subTitle = string.Empty;
                        string subRealTitle = string.Empty;
                        foreach (var iTitel in ilistTitle)
                        {
                            subTitle = iTitel.Trim();
                            len_div1 = subTitle.Length / 2;
                            len_div2 = subTitle.Length - len_div1;
                            s1 = subTitle.Substring(0, len_div1).Trim();
                            s2 = subTitle.Substring(len_div1, len_div2).Trim();
                            if(s1 == s2)
                            {
                                subRealTitle = s1;
                            }
                            else
                            {
                                subRealTitle = subTitle;
                            }
                            //1.cap nhat tieu dechinh
                            if (itmUpdate.TTL_ENG == string.Empty&& subRealTitle!=string.Empty)
                            {
                                itmUpdate.TTL_ENG = subRealTitle;
                                //lay lam tieu de chinh roi, thi khong dua vao tieu de phu
                                continue;
                            }
                            //2.neu co tieu de chinh, dua vao tieu de phu neu co
                            if (!itmUpdate.OtherTitles.Where(p => p.Title == subRealTitle).Any() && itmUpdate.TTL_ENG != subRealTitle && subRealTitle!=string.Empty)
                            {
                                itmUpdate.OtherTitles.Add(new Shared.Mis.Works.OtherTitle
                                {
                                    No = itmUpdate.OtherTitles.Count + 1,
                                    Title = subRealTitle
                                });
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    //dua vao tieu de chinh
                    //    itmUpdate.TTL_ENG = VnHelper.ConvertToUnSign(ediFilesItemsClone[i].Title.ToUpper());
                    //}
                    #endregion

                    #region thiet lap tac gia
                    //Chua co tac gia nay thi them vao
                    if (!itmUpdate.InterestedParties.Where(p => p.IP_INT_NO == ediFilesItemsClone[i].IpInNo).Any())
                    {
                        itmUpdate.InterestedParties.Add(new Shared.Mis.Works.InterestedParty
                        {
                            No = 1,
                            IP_INT_NO = ediFilesItemsClone[i].IpInNo,
                            IP_NAME = ediFilesItemsClone[i].IpName2,                           
                            IP_WK_ROLE = ediFilesItemsClone[i].IpWorkRole,

                            WK_STATUS = "COMPLETE",

                            PER_OWN_SHR = ediFilesItemsClone[i].PerOwnShr,
                            PER_COL_SHR = ediFilesItemsClone[i].PerColShr,

                            MEC_OWN_SHR = ediFilesItemsClone[i].MecOwnShr,
                            MEC_COL_SHR = ediFilesItemsClone[i].MecColShr,

                            SP_SHR = ediFilesItemsClone[i].SpShr,
                            TOTAL_MEC_SHR = ediFilesItemsClone[i].TotalMecShr,

                            SYN_OWN_SHR = ediFilesItemsClone[i].SynOwnShr,
                            SYN_COL_SHR = ediFilesItemsClone[i].SynColShr,
                            Society = ediFilesItemsClone[i].Society,
                            CountUpdate = 1,
                            LastUpdateAt = DateTime.Now,
                            LastChoiseAt = DateTime.Now,
                            IP_NUMBER = ediFilesItemsClone[i].NameNo,//ma ten
                            IP_NAME_LOCAL = ediFilesItemsClone[i].IpNameLocal2,
                            IP_NAMETYPE = ediFilesItemsClone[i].IpNameType,
                        });                        
                    }
                    bool isCOMPLETE = true;
                    for (int iP = 0; iP < itmUpdate.InterestedParties.Count; iP++)
                    {
                        if (itmUpdate.WRITER != string.Empty)
                        {
                            itmUpdate.WRITER += ",";
                        }
                        itmUpdate.WRITER += itmUpdate.InterestedParties[iP].IP_NAME;
                        if (isCOMPLETE && itmUpdate.InterestedParties[iP].WK_STATUS != "COMPLETE")
                        {
                            isCOMPLETE = false;
                        }
                    }
                    #endregion
                    
                    itmUpdate.ARTIST = VnHelper.ConvertToUnSign(ediFilesItemsClone[i].WorkArtist.ToUpper());
                    //if(ediFilesItemsClone[i].WorkArtist != string.Empty)
                    //{
                    //    int a = 1;
                    //}
                    if (itmUpdate.ARTIST != string.Empty)
                    {
                        //int a = 1;
                    }
                    itmUpdate.SOC_NAME = string.Empty;
                    itmUpdate.WK_STATUS = isCOMPLETE == true ? "COMPLETE" : "INCOMPLETE";
                    itmUpdate.StarRating = 1;
                    request.Items.Add(itmUpdate);
                }
                var dfsf = request.Items.Where(p => p.WK_INT_NO == "1602179").ToList();
                var data = await workController.ChangeList(request);
                statusMain.Invoke(new MethodInvoker(delegate
                {                   
                    progressBarImport.Value = 100;
                    lbPercent.Text = $"{100}%";
                }));
                totalSuccess += data.Items.Where(p => p.Status == Utilities.Common.UpdateStatus.Successfull).ToList().Count;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Sync..., total sync success/total: {totalSuccess}/{total}";
                }));

                #region update Ui when finish
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = true;
                }));
                toolMain.Invoke(new MethodInvoker(delegate
                {
                    btnChoiseFile.Enabled = true;
                    btnImport.Enabled = true;                   
                    btnExport.Enabled = true;
                }));
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = true;
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
                isSync = false;
            }
            catch (Exception)
            {
                if (toolMain != null && !toolMain.IsDisposed)
                {
                    toolMain.Invoke(new MethodInvoker(delegate
                    {
                        btnChoiseFile.Enabled = true;
                        btnImport.Enabled = true;                       
                        btnExport.Enabled = true;
                    }));
                }
                if (btnSysToWork != null && !btnSysToWork.IsDisposed)
                {
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = true;
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
                isSync = false;
            }
        }
        #endregion

        #region mono
        private void btnCalcMono_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoad)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load data...";
                    }));
                    return;
                }
                if (isFindMono)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting find mono...";
                    }));
                }
                if (isSync)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting Sync data...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
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

                if (ediFilesItemsClone == null || ediFilesItemsClone.Count == 0)
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
                backgroundWorker.RunWorkerAsync();
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
                #region innit
                isFindMono = true;
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbInfo.Text = "Staring find monopoly...";
                }));
                btnCalcMono.Invoke(new MethodInvoker(delegate
                {
                    btnCalcMono.Enabled = false;
                }));
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = false;
                }));
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
                for (int i = 0; i < ediFilesItemsClone.Count; i++)
                {
                    if(ediFilesItemsClone[i].WorkInternalNo == "9279634"
                        || ediFilesItemsClone[i].WorkInternalNo == "7681097"
                        || ediFilesItemsClone[i].WorkInternalNo == "7673274"
                        )
                    {
                        //int a = 1;
                    }
                    #region reset before set
                    ediFilesItemsClone[i].WorkFields = string.Empty;
                    ediFilesItemsClone[i].WorkMonopolyNote = string.Empty;
                    ediFilesItemsClone[i].IsWorkMonopoly = false;

                    ediFilesItemsClone[i].MemberFields = string.Empty;
                    ediFilesItemsClone[i].MemberMonopolyNote = string.Empty;
                    ediFilesItemsClone[i].IsMemberMonopoly = false;
                    ediFilesItemsClone[i].NonMember = string.Empty;
                    #endregion

                    #region 6.nonmember    

                    foreach (var inP in ediFilesItemsClone[i].ListInterestedParty)
                    {
                        if (inP.Society == "NS")
                        {
                            if (ediFilesItemsClone[i].NonMember.Length > 0)
                            {
                                ediFilesItemsClone[i].NonMember += ", ";
                            }
                            ediFilesItemsClone[i].NonMember += inP.IP_NAME_LOCAL;                           
                        }
                        else
                        {
                            //int a = 1;
                        }
                    }
                    ediFilesItemsClone[i].NonMember = ediFilesItemsClone[i].NonMember.Trim();                    
                    #endregion
                    //khong phai thanh vien khong can check doc quyen
                    //if (ediFilesItemsClone[i].Society == "NS")
                    //{
                    //    continue;
                    //}

                    #region gan doc quyen tac pham
                    if (monoWorks.Count > 0)
                    {                         
                        var itemMonoWork = monoWorks.Where(p => p.CodeNew == ediFilesItemsClone[i].WorkInternalNo).ToList();
                        if (itemMonoWork.Count > 0)
                        {
                            if (itemMonoWork[0].IsExpired || itemMonoWork[0].EndTime <= DateTime.Now)
                            {
                                ediFilesItemsClone[i].WorkFields = string.Empty;
                                ediFilesItemsClone[i].WorkMonopolyNote = "HẾT HẠN ĐỘC QUYỀN";
                                ediFilesItemsClone[i].IsWorkMonopoly = false;
                            }
                            else
                            {
                                SetMonopoly(monopolyType, ediFilesItemsClone[i], itemMonoWork[0], true);
                            }
                        }
                    }
                    #endregion

                    #region gan doc quyen tac gia
                    if (monoMembers.Count > 0)
                    {
                        foreach (var inp in ediFilesItemsClone[i].ListInterestedParty)
                        {
                            var itemMonoMember = monoMembers.Where(p => p.CodeNew == inp.IP_INT_NO && p.NameType == inp.IP_NAMETYPE).ToList();
                            if (itemMonoMember.Count > 0)
                            {
                                if (itemMonoMember[0].IsExpired || itemMonoMember[0].EndTime <= DateTime.Now)
                                {
                                    //TODO
                                    //ediFilesItemsClone[i].MemberFields = string.Empty;
                                    //ediFilesItemsClone[i].MemberMonopolyNote = "HẾT HẠN ĐỘC QUYỀN";
                                    //ediFilesItemsClone[i].IsMemberMonopoly = false;
                                }
                                else
                                {
                                    string nameMember = string.Empty;
                                    if (ediFilesItemsClone[i].DicMember.ContainsKey($"{itemMonoMember[0].CodeNew}{itemMonoMember[0].NameType}"))
                                    {
                                        nameMember = ediFilesItemsClone[i].DicMember[$"{itemMonoMember[0].CodeNew}{itemMonoMember[0].NameType}"];
                                    }
                                    SetMonopoly(monopolyType, ediFilesItemsClone[i], itemMonoMember[0], false, nameMember);
                                }
                            }
                        }
                        ediFilesItemsClone[i].MemberMonopolyNote = ediFilesItemsClone[i].MemberMonopolyNote.Trim();
                    }
                    #endregion
                }
                #endregion

                #region 3.to mau
                for (int i = 0; i < dgvEditCalcXXX.Rows.Count; i++)
                {
                    if (
                        (dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value)
                            ||
                        (dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value)
                        )
                    {
                        dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
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
                btnSysToWork.Invoke(new MethodInvoker(delegate
                {
                    btnSysToWork.Enabled = true;
                }));
                isFindMono = false;
            }
            catch (Exception)
            {
                isFindMono = false;
                if (btnCalcMono != null && !btnCalcMono.IsDisposed)
                {
                    btnCalcMono.Invoke(new MethodInvoker(delegate
                    {
                        btnCalcMono.Enabled = true;
                    }));
                }
                if (btnSysToWork != null && !btnSysToWork.IsDisposed)
                {
                    btnSysToWork.Invoke(new MethodInvoker(delegate
                    {
                        btnSysToWork.Enabled = true;
                    }));
                }
            }
        }

        #endregion

        private void cboMatchedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateType = cboMatchedType.SelectedIndex;
                if(GenerateType == 2)
                {
                    btnExportDefault.Visible = false;
                    btnbtnExportDetail.Visible = false;
                    btnExportDistribution.Visible = true;
                   
                }
                else
                {
                    btnExportDefault.Visible = true;
                    btnbtnExportDetail.Visible = true;
                    btnExportDistribution.Visible = false;
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        #region Filter
        bool isCheckTrueFalse = true;
        private void cheTrueFalse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                isCheckTrueFalse = cheTrueFalse.Checked;
                cboTypeChoise = cboType.SelectedIndex;
                btnFind_Click(null, null);
            }
            catch (Exception)
            {

                //throw;
            }
        }
        int cboTypeChoise = -1;
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoad)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting load data...";
                    }));
                    return;
                }
                if (isFindMono)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting find mono...";
                    }));
                }
                if (isSync)
                {
                    lbTotalLoad.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting Sync data...";
                    }));
                    return;
                }
                if (isFilter)
                {
                    statusMain.Invoke(new MethodInvoker(delegate
                    {
                        lbOperation.Text = "Waiting filter...";
                    }));
                    return;
                }
                if (ediFilesItemsClone == null || ediFilesItemsClone.Count == 0)
                {
                    return;
                }
                #region cbo
                cboTypeChoise = cboType.SelectedIndex;
                isCheckTrueFalse = cheTrueFalse.Checked;
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
                #region to mau                
                dgvEditCalcXXX.Invoke(new MethodInvoker(delegate
                {
                    dgvEditCalcXXX.DataSource = ediFilesItemsClone;                   
                    for (int i = 0; i < dgvEditCalcXXX.Rows.Count; i++)
                    {
                        if (
                            dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value != null
                            && (bool)dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value
                            )
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                        }


                        if (
                       (dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value)
                           ||
                       (dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value)
                       )
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }));
                #endregion
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
                /*
                 Workcode
                Title
                Writer
                Composer
                Lyrics
                Publisher
                Society
                Is Mono
                Is Work Mono
                Is MemberMono
                Is Non-member
                 
                 */
                isFilter = true;
                List<EdiFilesItem> fill = new List<EdiFilesItem>();
                if (cboTypeChoise == 0)
                {
                    //var query = ediFilesItemsClone.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WK_INT_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = ediFilesItemsClone.Where(c => c.WorkInternalNo.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    {
                        if (VnHelper.ConvertToUnSign(c.WorkTitle2).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.GroupWriter.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    {
                        if (VnHelper.ConvertToUnSign(c.GroupWriter).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.GroupWriter.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 3)
                {
                    var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    {
                        if (VnHelper.ConvertToUnSign(c.GroupComposer).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.IpEnglishName.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 4)
                {
                    var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    {
                        if (VnHelper.ConvertToUnSign(c.GroupLyrics).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.NameType.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 5)
                {
                    var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    {
                        if (VnHelper.ConvertToUnSign(c.GroupPublisher).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.Society.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 6)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    string soc = txtFind.Text.Trim().ToUpper();
                    var query = ediFilesItemsClone.Where(c => c.ListInterestedParty.Where(s=>s.Society.Contains(soc)).Any());
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 7)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    if(isCheckTrueFalse)
                    {
                        var query = ediFilesItemsClone.Where(c => (c.IsWorkMonopoly == true) || (c.IsMemberMonopoly == true));
                        fill = query.ToList();
                    }
                    else
                    {
                        var query = ediFilesItemsClone.Where(c => (c.IsWorkMonopoly == false) && (c.IsMemberMonopoly == false));
                        fill = query.ToList();
                    }
                    
                }
                else if (cboTypeChoise == 8)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = ediFilesItemsClone.Where(c => c.IsWorkMonopoly == isCheckTrueFalse );
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 9)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = ediFilesItemsClone.Where(c => c.IsMemberMonopoly == isCheckTrueFalse);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 10)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    if(isCheckTrueFalse)
                    {
                        var query = ediFilesItemsClone.Where(c => c.NonMember.Length > 0);
                        fill = query.ToList();
                    }
                    else
                    {
                        var query = ediFilesItemsClone.Where(c => c.NonMember.Length == 0);
                        fill = query.ToList();
                    }
                    
                }
                else if (cboTypeChoise == 11)
                {
                    //var query = ediFilesItemsClone.Where(delegate (EdiFilesItem c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.Society).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = ediFilesItemsClone.Where(c => c.IscheckCompareTitleAndWriter == isCheckTrueFalse);
                    fill = query.ToList();
                }
                dgvEditCalcXXX.Invoke(new MethodInvoker(delegate
                {
                    dgvEditCalcXXX.DataSource = fill;
                    //dgvEditCalcXXX.();
                    for (int i = 0; i < dgvEditCalcXXX.Rows.Count; i++)
                    {
                        if (
                            dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value != null
                            && (bool)dgvEditCalcXXX.Rows[i].Cells["IscheckCompareTitleAndWriterx"].Value
                            )
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                        }


                        if (
                       (dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsWorkMonopolyx"].Value)
                           ||
                       (dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value != null && (bool)dgvEditCalcXXX.Rows[i].Cells["IsMemberMonopolyx"].Value)
                       )
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvEditCalcXXX.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = $"Filter data, total record(s): {fill.Count}";
                }));                
                isFilter = false;
            }
            catch (Exception)
            {
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                isFilter = false;
            }

        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboTypeChoise = cboType.SelectedIndex;
                isCheckTrueFalse = cheTrueFalse.Checked;
                if (cboTypeChoise == 7
                    || cboTypeChoise == 8
                    || cboTypeChoise == 9
                    || cboTypeChoise == 10
                    || cboTypeChoise == 11)
                {
                    cheTrueFalse.Visible = true;
                    txtFind.Visible = false;
                    btnFind_Click(null, null);
                }
                else
                {
                    cheTrueFalse.Visible = false;
                    txtFind.Visible = true;                    
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