using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;
using System.IO;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure.data;
using Vcpmc.Mis.ApplicationCore.Entities.makeData;
using Vcpmc.Mis.Shared.viewModel.report;
using Vcpmc.Mis.ApplicationCore.Entities.dis;
using Vcpmc.Mis.Common.export;
using Vcpmc.Mis.AppMatching.form.Distribution.BH.report;
using Vcpmc.Mis.AppMatching.printer;

namespace Vcpmc.Mis.AppMatching.form.mic.Distribution.BH.report
{
    public partial class frmBhDistributionReport : Form
    {
        VcpmcContext ctx = new VcpmcContext();
        List<DistributionData> distreibutionPO = new List<DistributionData>();
        List<MemberBH> memberBhs = new List<MemberBH>();
        List<MemberBHDetail> memberBhDetail = new List<MemberBHDetail>();
        /// <summary>
        /// du lieu tong quat lay len
        /// </summary>
        List<BhDistributionViewModel> dataSource = new List<BhDistributionViewModel>();
        //List<BhDistributionModel> currentDataSource = new List<BhDistributionModel>();
        List<ItemMember> itemMembers = new List<ItemMember>();

        List<BhAggregationViewModel> BhAggregates = new List<BhAggregationViewModel>();
        List<BhAggregation2ViewModel> BhAggregates2 = new List<BhAggregation2ViewModel>();
        /// <summary>
        /// danh sach du lieu sau khi loc
        /// </summary>
        Dictionary<int, List<BhDistributionViewModel>> dict = new Dictionary<int, List<BhDistributionViewModel>>();
        OperationType Operation = OperationType.LoadExcel;
        Guid guidDisPO;
        //bool isInnit = false;
        //string strMemeberSelected = "";
        //int selectvalue = -1;
        string[] itemMemberExtend = new string[] { 
            //"Original", //tam thoi khong xem
            "Author",
            "BHMEDIA",
            "1 phan 6 TRAN THIEN THANH",
            //"All Not find member in BHMEDIA member",//tam thoi khong xem
            //"All Except",   //tam thoi khong xem         
        };
        string[] itemMemberExtend2 = new string[] {
            "NGUYEN VAN TY",
            "HOANG PHUONG",           
        };
        //bool isInnit = false;

        string strmember = "";
        string strBh = "";
        DateTime dataTime = DateTime.Now;
        #region innit
        public frmBhDistributionReport()
        {
            InitializeComponent();
        }

        private void frmDistributionReport_Load(object sender, EventArgs e)
        {
            InnitData();
            strBh = txtBhMedia.Text.Trim();
        }
        #endregion

        #region tool
        private void tsmRefreshdataInnt_Click(object sender, EventArgs e)
        {
            InnitData();
        }
        private void tsmLoadReport_Click(object sender, EventArgs e)
        {
            try
            {
                Operation = OperationType.LoadReport;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cboMemberList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ItemMember mb = (ItemMember)cboMemberList.SelectedItem;
                strmember = mb.MemberVN;
                txtMember.Text = strmember;
                List<BhDistributionViewModel> view = null;
                foreach (var item in dict)
                {
                    if (item.Key == mb.Id)
                    {
                        view = item.Value;                                              
                        break;
                    }
                }                
                if (view != null)
                {
                    ShowUI(view, mb.MemberVN);
                }
            }
            catch (Exception )
            {


            }
        }
        private void cboDistributionBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboDistributionBill.Items.Count > 0)
                {
                    guidDisPO = new Guid(cboDistributionBill.ComboBox.SelectedValue.ToString());
                }    
            }
            catch (Exception )
            {


            }
           
        }
        string currentDirectory = "";
        private void tssExport_Click(object sender, EventArgs e)
        {
            try
            {
                dataTime = dtReport.Value;
                strmember = txtBhMedia.Text.Trim();
                currentDirectory = "";
                OpenFileDialog folderBrowser = new OpenFileDialog();
                // Set validate names and check file exists to false otherwise windows will
                // not let you select "Folder Selection."
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Folder Selection.";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    currentDirectory = folderPath;
                }
                if (currentDirectory == "")
                {
                    return;
                }
                //operation = OperationMasteList.SaveFile;
                //exportType = ExportType.Excel;
                //pcloader.Visible = true;
                //pcloader.Dock = DockStyle.Fill;
                //backgroundWorker1.RunWorkerAsync();
                /////////
                Operation = OperationType.ExportToExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception )
            {


            }
        }
        private void tssViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                dataTime = dtReport.Value;
                ItemMember mb = (ItemMember)cboMemberList.SelectedItem;
                List<BhDistributionViewModel> view = null;
                foreach (var item in dict)
                {
                    if (item.Key == mb.Id)
                    {
                        view = item.Value;
                        break;
                    }
                }
                //
                //int count = 0;
                //decimal value = 0;
                //decimal value2 = 0;
                //foreach (var item in view)
                //{
                //    count++;
                //    value += item.Royalty;
                //    value2 += item.Royalty2;
                //    if (item.Royalty != item.Royalty2)
                //    {
                //        int a = 1;
                //    }
                //}
                if (view != null)
                {
                    frmViewBhDistributionReport f = new frmViewBhDistributionReport(txtMember.Text.Trim(), txtBhMedia.Text.Trim(), dtReport.Value); ;
                    f.dataSource = view;
                    if(mb.Member == "BHMEDIA")
                    {
                        f.ReportTemplate = 3;
                    }
                    else
                    {
                        f.ReportTemplate = 0;
                    }
                    f.ShowDialog();
                }
            }
            catch (Exception )
            {


            }
        }
        private void tssBtnSavePdfAll_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                dataTime = dtReport.Value;
                strmember = txtBhMedia.Text.Trim();
                currentDirectory = "";
                OpenFileDialog folderBrowser = new OpenFileDialog();
                // Set validate names and check file exists to false otherwise windows will
                // not let you select "Folder Selection."
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Folder Selection.";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    currentDirectory = folderPath;
                }
                if (currentDirectory == "")
                {
                    return;
                }
                //operation = OperationMasteList.SaveFile;
                //exportType = ExportType.Excel;
                //pcloader.Visible = true;
                //pcloader.Dock = DockStyle.Fill;
                //backgroundWorker1.RunWorkerAsync();
                /////////
                Operation = OperationType.SaveAllPdf;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception )
            {


            }
        }
        private void tssPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                dataTime = dtReport.Value;
                if (MessageBox.Show($"Total bill, Are you sure?", "Confirm printer for distrubution bill",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save
                    Operation = OperationType.PrinterListBill;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
            }
            catch (Exception )
            {

            }
        }
        
        #endregion

        #region timer      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Operation == OperationType.LoadReport)
                {
                    LoadReport();
                }
                else if (Operation == OperationType.ExportToExcel)
                {
                    //ExportToExCel(currentDirectory);
                    SaveAll(currentDirectory, "Excel");
                }
                else if (Operation == OperationType.SaveAllPdf)
                {                   
                    SaveAll(currentDirectory, "PDF");
                }
                else if (Operation == OperationType.PrinterListBill)
                {
                    PrinterAll();
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

        #region data
        private void InnitData()
        {
            try
            {
                cboMemberList.Items.Clear();
                //isInnit = false;
                //1.phieu PO
                distreibutionPO = (from s in ctx.DistributionDatas
                            orderby s.TimeCreate descending                           
                            select s
                                ).ToList();
                cboDistributionBill.ComboBox.ValueMember = "Id";
                cboDistributionBill.ComboBox.DisplayMember = "Name";                
                if (distreibutionPO.Count > 0)
                {
                    cboDistributionBill.ComboBox.DataSource = distreibutionPO;
                    cboDistributionBill.SelectedIndex = 0;
                    guidDisPO = new Guid(cboDistributionBill.ComboBox.SelectedValue.ToString());
                    //isInnit = true;
                }
                else
                {
                    cboDistributionBill.ComboBox.DataSource = new List<DistributionData>();
                    //isInnit = false;
                    return;
                }
                //2. membert hb
                cboMemberList.Items.Clear();
                memberBhs = (from s in ctx.MemberBHs
                                           orderby s.TimeCreate descending
                                           //select new
                                           //{
                                           //    s.Id,
                                           //    s.Name
                                           //    //name = $"Name: {s.Name} , Create time: { s.TimeCreate.ToString("dd/MM/yyyy HH:mm:ss")}, User: { s.User }"
                                           //}
                                           select s
                                ).ToList();

                if(memberBhs.Count>0)
                {
                    Guid guid = memberBhs[0].Id;

                    #region Load detail member bh
                    memberBhDetail = (from s in ctx.MemberBHDetails
                                      where s.MemberBHId == guid
                                      orderby s.Type descending
                                      //select new
                                      //{
                                      //    s.Id,
                                      //    s.Name
                                      //    //name = $"Name: {s.Name} , Create time: { s.TimeCreate.ToString("dd/MM/yyyy HH:mm:ss")}, User: { s.User }"
                                      //}
                                      select s
                                ).ToList();

                    if (memberBhDetail.Count > 0)
                    {
                        ItemMember iMem = null;
                        //I.danh sach nap san
                        for (int i = 0; i < memberBhDetail.Count; i++)
                        {
                            iMem = null;
                            //1.single
                            if (memberBhDetail[i].Type == "SINGLE")
                            {
                                iMem = new ItemMember();
                                iMem.Name = $"SINGLE-{memberBhDetail[i].Member}-{memberBhDetail[i].MemberVN}";
                                iMem.Type = "SINGLE";
                                iMem.Member = memberBhDetail[i].Member;
                                iMem.MemberVN = memberBhDetail[i].MemberVN;
                                itemMembers.Add(iMem);
                            }
                            //2.group
                            else if (memberBhDetail[i].Type == "GROUP")
                            {                               
                                iMem = null;
                                //2.1.tim xem co chua
                                foreach (var item in itemMembers)
                                {
                                    if (item.Name == $"GROUP-{memberBhDetail[i].Member}-{memberBhDetail[i].MemberVN}")
                                    {
                                        iMem = item;                                        
                                        break;
                                    }                                    
                                }
                                //2.2.Chua co thi tao moi
                                if (iMem==null)
                                {
                                    iMem = new ItemMember();
                                    iMem.Name = $"GROUP-{memberBhDetail[i].Member}-{memberBhDetail[i].MemberVN}";
                                    iMem.Type = "GROUP";
                                    iMem.Member = memberBhDetail[i].Member;
                                    iMem.MemberVN = memberBhDetail[i].MemberVN;
                                    //iMem.NameOfContent = memberBhDetail[i].Member;
                                    itemMembers.Add(iMem);
                                }
                                //2.3.them submember
                                string[] x = memberBhDetail[i].SubMember.Split(',');
                                if (memberBhDetail[i].GetPart == "MAIN")
                                {

                                    for (int j = 0; j < x.Length; j++)
                                    {
                                        iMem.MAIN_subMember.Add(x[j].Trim());
                                    }
                                }
                                else if (memberBhDetail[i].GetPart == "EXCEPT")
                                {
                                    for (int j = 0; j < x.Length; j++)
                                    {
                                        iMem.EXCEPT_subMember.Add(x[j].Trim());
                                    }
                                }                                
                            }
                        }
                        //II.bao cao them extend
                        for (int i = 0; i < itemMemberExtend.Length; i++)
                        {
                            iMem = new ItemMember();
                            iMem.Name = $"EXTEND-{itemMemberExtend[i]}";
                            iMem.Type = "EXTEND";
                            iMem.Member = itemMemberExtend[i];
                            iMem.MemberVN = itemMemberExtend[i];
                            //iMem.NameOfContent = itemMemberExtend[i];
                            itemMembers.Add(iMem);
                        }
                        //DANH SACH DUWX LAI
                        for (int i = 0; i < itemMemberExtend2.Length; i++)
                        {
                            iMem = new ItemMember();
                            iMem.Name = $"HOLD-{itemMemberExtend2[i]}";
                            iMem.Type = "HOLD";
                            iMem.Member = itemMemberExtend2[i];
                            iMem.MemberVN = itemMemberExtend2[i];
                            //iMem.NameOfContent = itemMemberExtend[i];
                            itemMembers.Add(iMem);
                        }
                        //II.them vao danh sach
                        int index = 1;
                        for (int i = 0; i < itemMembers.Count; i++)
                        {
                            itemMembers[i].Id = index;
                            itemMembers[i].Name = $"{(index++).ToString().PadLeft(3,' ')}. {itemMembers[i].Name}";
                        }
                        cboMemberList.ValueMember = "Id";
                        cboMemberList.DisplayMember = "Name";
                        cboMemberList.DataSource = itemMembers;
                        cboMemberList.SelectedIndex = 0;
                        //isInnit = true;
                        //tsmLoadReport.Focus();
                    }
                    else
                    {
                        //isInnit = false;
                    }
                    
                    #endregion
                }
                else
                {
                    //isInnit = false;
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Load data error! Please check again");
            }
        }

        private void LoadReport()
        {
            try
            {
                if(guidDisPO==null)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("Must choise Distribution!");
                }

                #region load du lieu goc
                var data = (from s in ctx.DistributionDataItems
                            where s.DistributionDataId == guidDisPO
                            //Order by 
                            select new
                            {
                                //basic
                                s.No,
                                s.WorkInNo,
                                s.Title,
                                s.BhAuthor,
                                s.PoolName,
                                s.SourceName,
                                s.Role,
                                s.Share,
                                s.Royalty
                                    //calc
                                    ,
                                s.IsCondittionTime,
                                s.SubMember,
                                s.Beneficiary,
                                s.GetPart,
                                s.Percent,
                                s.IsGiveBeneficiary,
                                s.IsExcept
                            }
                           ).ToList();
                dataSource.Clear();
                BhDistributionViewModel temp = null;
                foreach (var item in data)
                {
                    temp = new BhDistributionViewModel
                    {
                        No = item.No,
                        WorkInNo = item.WorkInNo,
                        Title = item.Title,
                        BhAuthor = item.BhAuthor,
                        PoolName = item.PoolName,
                        SourceName = item.SourceName,
                        Role = item.Role,
                        Share = item.Share,
                        Royalty = item.Royalty,
                        IsCondittionTime = item.IsCondittionTime,
                        SubMember = item.SubMember,
                        Beneficiary = item.Beneficiary,
                        GetPart = item.GetPart,
                        Percent = item.Percent,
                        IsGiveBeneficiary = item.IsGiveBeneficiary,
                        IsExcept = item.IsExcept,

                    };
                    //temp.Royalty2 = temp.Royalty * temp.Percent/100;
                    temp.Royalty2 = temp.Royalty;
                    string[] subMember = temp.SubMember.Split(',');
                    foreach (var sub in subMember)
                    {
                        temp.ListSubMember.Add(sub);
                    }
                    dataSource.Add(temp);
                }
                #endregion                

                #region create data
                dict.Clear();               
                for (int i = 0; i < itemMembers.Count; i++)
                {   
                    dict.Add(i+1,FilterDataAffterChoiseData(itemMembers[i], true));                    
                }
                #endregion

                #region Show report đang chọn
                cboMemberList.Invoke(new MethodInvoker(delegate
                {
                    ItemMember mb = (ItemMember)cboMemberList.SelectedItem;
                    List<BhDistributionViewModel> view = null;
                    foreach (var item in dict)
                    {
                        if (item.Key == mb.Id)
                        {
                            view = item.Value;
                            break;
                        }
                    }
                    if (view != null)
                    {
                        ShowUI(view, mb.MemberVN);
                    }
                }));
                #endregion

                #region aggregation
                string SQL = @"SELECT 'SINGLE' AS Type, BhAuthor, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where IsCondittionTime = 0 and IsExcept = 0 and ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" group by BhAuthor
                            UNION
                            SELECT 'GROUP' AS Type, 'LANG VAN' AS BhAuthor, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where IsCondittionTime = 0 and IsExcept = 0
		                            and BhAuthor in ('TRAN THIEN THANH','TU NHI') and ";

                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" UNION
                            SELECT 'GROUP' AS Type, 'GOOGLE' AS OBJECT, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where IsCondittionTime = 1 AND PoolName = 'GOOGLE'
		                            and BhAuthor IN ('GIAO TIEN', 'VINH SU', 'HOANG PHUONG', 'TO THANH SON', 'NGOC LE', 'ANH BANG') and ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @"  UNION
                            SELECT 'GROUP' AS Type, 'MASECO' AS BhAuthor, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where IsExcept = 1 and ";		                            
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" UNION
                            SELECT 'GROUP' AS Type, 'BHMEDIA' AS BhAuthor, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty * [Percent]/100) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where IsCondittionTime = 1 and IsExcept = 0 
		                            and Beneficiary = 'BHMEDIA' and ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" UNION
                            SELECT 'GROUP' AS Type, 'ORIGINAL' AS BhAuthor, count(*) as Count_total, SUM(Royalty) AS Royalty, sum(Royalty) as Royalty2
	                            FROM [DistributionDataItems]	
	                            where BhAuthor != '' and ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";


                BhAggregates = ctx.Database.SqlQuery<BhAggregationViewModel>(SQL).ToList();
                dgvAggregates1.Invoke(new MethodInvoker(delegate
                {
                    dgvAggregates1.DataSource = BhAggregates;
                }));
                #endregion

                #region aggregation2 
                SQL = $@"select BhAuthor,
                            sum(sltotalOriginal) as sltotalOriginal, sum(sltotalAuthor) as sltotalAuthor, sum(sltotalBhmedia) as sltotalBhmedia, 
		                    sum(sltotalExcept) as sltotalExcept,sum(sltotalhold) as sltotalhold,
                            sum(totalOriginal) as totalOriginal,sum(totalAuthor) as totalAuthor,sum(totalBhmedia) as totalBhmedia
                            ,sum(totalExcept) as totalExcept,sum(totalhold) as totalhold
                            from
                            (
	                            select BhAuthor, count(*) as sltotalOriginal, sum(Royalty) as totalOriginal, 0 as sltotalAuthor, 0 as totalAuthor,  
	                            0 as sltotalBhmedia , 0 as totalBhmedia, 0 as sltotalExcept,0 as totalExcept ,0 as sltotalhold, 0 as totalhold 
                                from DistributionDataItems
	                            where ";
                SQL += " DistributionDataId = '"+ guidDisPO.ToString() + "' ";//goc
                SQL += @" group by BhAuthor
	                            union
	                            select BhAuthor, 0 as sltotalOriginal, 0 as totalOriginal, count(*) as sltotalAuthor, sum(Royalty) as totalAuthor, 0 as sltotalBhmedia,
	                            0 as totalBhmedia , 0 as sltotalExcept,0 as totalExcept ,0 as sltotalhold, 0 as totalhold 
                                from DistributionDataItems
	                            where IsCondittionTime = 0 and IsExcept = 0 AND ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";//tac gia
                SQL += @" group by BhAuthor
	                            union
	                            select BhAuthor,  0 as sltotalOriginal, 0 as totalOriginal,  0 as sltotalAuthor, 0 as totalAuthor,count(*) as sltotalAuthor, 
	                            sum(Royalty) as totalBhmedia , 0 as sltotalExcept,0 as totalExcept ,0 as sltotalhold, 0 as totalhold 
                                from DistributionDataItems
	                            where  IsCondittionTime = 1 and Beneficiary = 'BHMEDIA' and IsExcept = 0 and ";
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";//bh media
                SQL += @"group by BhAuthor 
                                union
	                            select BhAuthor,  0 as sltotalOriginal, 0 as totalOriginal,  0 as sltotalAuthor, 0 as totalAuthor,0 as sltotalAuthor, 
	                            0 as totalBhmedia , count(*) as sltotalExcept, sum(Royalty) as totalExcept ,0 as sltotalhold, 0 as totalhold 
                                from DistributionDataItems
	                            where 
	                            --IsCondittionTime = 0 and //khong can xet vi ban cho maseco 24.01.2014, truoc khi ky cho nhmedia: 01/04/2018	 
	                            IsExcept = 1 and ";//loai tru
                SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" group by BhAuthor
                                union 
		                        select BhAuthor, 0 as sltotalOriginal, 0 as totalOriginal, 0 as sltotalAuthor, 0 as totalAuthor,  
	                            0 as sltotalBhmedia , 0 as totalBhmedia, 0 as sltotalExcept,0 as totalExcept ,
		                        count(*) as sltotalhold, sum(Royalty) as totalhold
		                        from DistributionDataItems
	                            where   IsCondittionTime = 1 AND Beneficiary = BhAuthor	and ";	

		        SQL += " DistributionDataId = '" + guidDisPO.ToString() + "' ";
                SQL += @" group by BhAuthor
                            )  as t
                            group by BhAuthor";
                BhAggregates2 = ctx.Database.SqlQuery<BhAggregation2ViewModel>(SQL).ToList();
                dgvAggregates2.Invoke(new MethodInvoker(delegate
                {
                    dgvAggregates2.DataSource = BhAggregates2;
                }));
                #endregion
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
        private List<BhDistributionViewModel> FilterDataAffterChoiseData(ItemMember itemMB, bool IsrefreshUi)
        {
            List<BhDistributionViewModel> currentDataSource = new List<BhDistributionViewModel>();
            List<BhDistributionViewModel> currentDataSourceClone = new List<BhDistributionViewModel>();
            try
            {
                if (dataSource.Count == 0)
                {
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Not find records";
                    }));

                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    return currentDataSourceClone;
                }
                //{ "All member", "Total Report", "Not find member in BH", "Contractime less return time"};                
                //if (strMemeberSelected.Length == 0)
                //{
                //    pcloader.Invoke(new MethodInvoker(delegate
                //    {
                //        pcloader.Visible = false;
                //    }));
                //    MessageBox.Show("Must choising View Type!");
                //    return;
                //}

                //string[] strviewSelected = strMemeberSelected.Split('-');

                switch (itemMB.Type)
                {
                    case "EXTEND":
                        #region EXTEND
                        if (currentDataSource != null)
                        {
                            currentDataSource.Clear();
                        }
                        else
                        {
                            currentDataSource = new List<BhDistributionViewModel>();
                        }
                        switch (itemMB.Member)
                        {
                            case "Original":
                                #region Original
                                currentDataSource = (from s in dataSource
                                                     orderby s.No ascending
                                                     select s
                                            ).ToList();
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                    currentDataSourceClone.Add(add);
                                }
                                #endregion
                                break;
                            case "Author":
                                #region All member
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.IsCondittionTime == false
                                                     && s.IsExcept == false
                                                     //&& (
                                                     //    (s.GetPart == "MAIN" && s.IsExcept == false)
                                                     //    ||
                                                     //     (s.GetPart == "EXCEPT" && s.IsExcept == true)
                                                     //)
                                                     orderby s.BhAuthor ascending
                                                     select s
                                             ).ToList();
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();                                    
                                    currentDataSourceClone.Add(add);
                                }
                                #endregion
                                break;
                            case "BHMEDIA":
                                #region Contractime less return time
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.Beneficiary == "BHMEDIA"
                                                        && s.IsCondittionTime == true
                                                        && s.IsExcept == false
                                                     //s.BhAuthor == ""
                                                     //&& s.GetPart
                                                     select s
                                            ).ToList();
                                decimal m1 = 0, m2 = 0;
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                    //tinh lai tien cho bh
                                    add.Percent = add.Percent;
                                    add.Royalty2 = add.Royalty * add.Percent / 100;
                                    currentDataSourceClone.Add(add);
                                    //
                                    m1 += add.Royalty;
                                    m2 += add.Royalty2;
                                }
                                //t a = 1;
                                #endregion 
                                break;
                            case "1 phan 6 TRAN THIEN THANH":
                                #region Contractime less return time
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.Beneficiary == "BHMEDIA"
                                                        && s.BhAuthor == "TRAN THIEN THANH"
                                                        && s.IsCondittionTime == true
                                                        && s.IsExcept == false
                                                     //s.BhAuthor == ""
                                                     //&& s.GetPart
                                                     select s
                                            ).ToList();
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                    //tinh lai tien cho bh
                                    add.Percent = (100 - add.Percent);
                                    add.Royalty2 = add.Royalty * add.Percent / 100;
                                    currentDataSourceClone.Add(add);                                    
                                }
                                #endregion 
                                break;
                            case "All Not find member in BHMEDIA member":
                                #region All member
                                currentDataSource = (from s in dataSource
                                                     where
                                                        //s.IsCondittionTime == true
                                                        s.BhAuthor.Trim() == ""
                                                     //&& s.GetPart
                                                     orderby s.BhAuthor ascending
                                                     select s
                                             ).ToList();
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                    currentDataSourceClone.Add(add);
                                }
                                #endregion
                                break;
                            case "All Except":
                                #region Contractime less return time
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.IsExcept == true
                                                     //s.BhAuthor == ""
                                                     //&& s.GetPart
                                                     orderby s.BhAuthor ascending
                                                     select s
                                            ).ToList();
                                #endregion
                                foreach (var item in currentDataSource)
                                {
                                    BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                    currentDataSourceClone.Add(add);
                                }
                                break;                                                     
                            default:
                                break;
                        }

                        #endregion
                        break;
                    case "HOLD":
                        #region HOLD
                        if (currentDataSource != null)
                        {
                            currentDataSource.Clear();
                        }
                        else
                        {
                            currentDataSource = new List<BhDistributionViewModel>();
                        }
                        currentDataSource = (from s in dataSource
                                             where
                                                s.IsCondittionTime == true // thuoc ve tac gia
                                                && s.BhAuthor == itemMB.Member
                                                && s.Beneficiary == itemMB.Member
                                                && (
                                                        (s.GetPart == "MAIN" && s.IsExcept == false)
                                                        ||
                                                         (s.GetPart == "EXCEPT" && s.IsExcept == true)
                                                    )
                                             //&& s.GetPart
                                             select s
                                             ).ToList();
                        foreach (var item in currentDataSource)
                        {
                            BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                            currentDataSourceClone.Add(add);
                        }
                        #endregion
                        break;
                    case "SINGLE":
                        #region SINGLE
                        if (currentDataSource != null)
                        {
                            currentDataSource.Clear();
                        }
                        else
                        {
                            currentDataSource = new List<BhDistributionViewModel>();
                        }
                        currentDataSource = (from s in dataSource
                                             where
                                                s.IsCondittionTime == false // thuoc ve tac gia
                                                && s.BhAuthor == itemMB.Member
                                                && (
                                                        (s.GetPart == "MAIN" && s.IsExcept == false)
                                                        ||
                                                         (s.GetPart == "EXCEPT" && s.IsExcept == true)
                                                    )
                                             //&& s.GetPart
                                             select s
                                             ).ToList();
                        foreach (var item in currentDataSource)
                        {
                            BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                            currentDataSourceClone.Add(add);
                        }
                        #endregion
                        break;
                    case "GROUP":
                        #region GROUP
                        if (currentDataSource != null)
                        {
                            currentDataSource.Clear();
                        }
                        else
                        {
                            currentDataSource = new List<BhDistributionViewModel>();
                        }
                        //selectvalue
                        //var itemSub = itemMembers.Find(x => x.Id == itemMB.Id);
                        if (itemMB != null)
                        {
                            if(itemMB.Member == "LANG VAN")
                            {
                                #region LANG VAN
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.IsCondittionTime == false
                                                        && (
                                                                 (
                                                                    itemMB.MAIN_subMember.Count > 0
                                                                    && s.IsExcept == false
                                                                    && itemMB.MAIN_subMember.Contains(s.BhAuthor)
                                                                )
                                                              ||
                                                                (
                                                                    itemMB.EXCEPT_subMember.Count > 0
                                                                    && s.IsExcept == false
                                                                    && itemMB.EXCEPT_subMember.Contains(s.BhAuthor)
                                                                )
                                                           )
                                                     //&& s.GetPart
                                                     orderby s.BhAuthor ascending
                                                     select s
                                              ).ToList();
                                #endregion
                            }
                            else if (itemMB.Member == "BHMEDIA")
                            {
                                #region BHMEDIA
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.IsCondittionTime == true
                                                        && (
                                                                 (
                                                                    itemMB.MAIN_subMember.Count > 0
                                                                    && s.IsExcept == false
                                                                    && itemMB.MAIN_subMember.Contains(s.BhAuthor)
                                                                )
                                                              ||
                                                                (
                                                                    itemMB.EXCEPT_subMember.Count > 0
                                                                    && s.IsExcept == true
                                                                    && itemMB.EXCEPT_subMember.Contains(s.BhAuthor)
                                                                )
                                                           )
                                                     //&& s.GetPart
                                                     orderby s.BhAuthor ascending
                                                     select s
                                             ).ToList();                                
                                #endregion
                            }
                            else if (itemMB.Member == "MASECO")
                            {
                                #region MASECO
                                currentDataSource = (from s in dataSource
                                                     where
                                                        //s.IsCondittionTime == false
                                                        //&& 
                                                        (
                                                                 (
                                                                    itemMB.MAIN_subMember.Count > 0
                                                                    && s.IsExcept == false
                                                                    && itemMB.MAIN_subMember.Contains(s.BhAuthor)
                                                                )
                                                              ||
                                                                (
                                                                    itemMB.EXCEPT_subMember.Count > 0
                                                                    && s.IsExcept == true
                                                                    && itemMB.EXCEPT_subMember.Contains(s.BhAuthor)
                                                                )
                                                           )
                                                     //&& s.GetPart
                                                     orderby s.BhAuthor ascending
                                                     select s
                                             ).ToList();
                                #endregion
                            }
                            else if (itemMB.Member == "GOOGLE")
                            {
                                #region GOOGLE
                                currentDataSource = (from s in dataSource
                                                     where
                                                        s.PoolName == "GOOGLE"
                                                        && s.IsCondittionTime == true
                                                        && (
                                                                    (
                                                                    itemMB.MAIN_subMember.Count > 0
                                                                    && s.IsExcept == false
                                                                    && itemMB.MAIN_subMember.Contains(s.BhAuthor)
                                                                )
                                                                ||
                                                                (
                                                                    itemMB.EXCEPT_subMember.Count > 0
                                                                    && s.IsExcept == true
                                                                    && itemMB.EXCEPT_subMember.Contains(s.BhAuthor)
                                                                )
                                                            )
                                                     orderby s.BhAuthor ascending
                                                     select s
                                             ).ToList();
                                #endregion
                            }
                            foreach (var item in currentDataSource)
                            {
                                BhDistributionViewModel add = (BhDistributionViewModel)item.Clone();
                                currentDataSourceClone.Add(add);
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }                             
            }
            catch (Exception ex)
            {
                currentDataSourceClone = new List<BhDistributionViewModel>();
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
            return currentDataSourceClone;
        }

        private void ShowUI(List<BhDistributionViewModel> dataSource, string strMemeberSelected)
        {
            dgvMain.Invoke(new MethodInvoker(delegate
            {
                dgvMain.DataSource = dataSource;
            }));
            int count = 0;
            decimal value = 0;
            decimal value2 = 0;
            foreach (var item in dataSource)
            {
                count++;
                value += item.Royalty;
                value2 += item.Royalty2;
                //if (item.Royalty != item.Royalty2)
                //{
                //    int a = 1;
                //}
            }

            txtCount.Invoke(new MethodInvoker(delegate
            {
                txtCount.Text = count.ToString("###,###,###");
            }));
            txtValue.Invoke(new MethodInvoker(delegate
            {
                txtValue.Text = value2.ToString("###,###,###");
            }));
            statusTripMain.Invoke(new MethodInvoker(delegate
            {
                tssLblInfo.Text = $"View data: {strMemeberSelected}-{value}";
            }));
        }
        #endregion

        #region export=printer
        private void ExportToExCel(string folderPath)
        {
            try
            {
                int stt = 1;
                foreach (var item in dict)
                {
                    ItemMember mb = null;
                    for (int i = 0; i < itemMembers.Count; i++)
                    {
                        if(item.Key == itemMembers[i].Id)
                        {
                            mb = itemMembers[i];
                            break;
                        }
                    }
                    if(mb!=null)
                    {                        
                        bool check = WriteReportHelper.WriteExcelDistribution(item.Value, folderPath, $"{stt.ToString()}-{mb.Member}", strBh, mb.MemberVN, dataTime);
                        stt++;
                    }                    
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
        private void ExportToExCelAggrerate(string folderPath)
        {
            try
            {

                //bool check = WriteReportHelper.WriteExcelBhAggregates(BhAggregates, folderPath, $"100-{"BhAggregation"}", strBh, "BhAggregation", dataTime);
                bool check = WriteReportHelper.WriteExcelBhAggregates2(BhAggregates2, folderPath, $"101-{"BhAggregation2"}", strBh, "BhAggregation", dataTime);

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
        private void SaveAll(string folderPath, string typeExport)
        {
            try
            {
                string path = "";
                int stt = 1;
                foreach (var item in dict)
                {
                    ItemMember mb = null;
                    for (int i = 0; i < itemMembers.Count; i++)
                    {
                        if (item.Key == itemMembers[i].Id)
                        {
                            mb = itemMembers[i];
                            break;
                        }
                    }
                    int reportTemplate = 0;
                    if (mb.Member == "BHMEDIA")
                    {
                        reportTemplate = 3;
                    }
                    else
                    {
                        reportTemplate = 0;
                    }
                    if (mb != null)
                    {
                        if (typeExport == "PDF")
                        {
                            path = $"{folderPath}\\{stt.ToString()}-{mb.Type}-{mb.Member}.pdf";
                            SaveAll(item.Value, path, strBh, mb.MemberVN, dataTime, typeExport, reportTemplate);
                        }
                        else
                        {
                            path = $"{folderPath}\\{stt.ToString()}-{mb.Type}-{mb.Member}.xls";
                            SaveAll(item.Value, path, strBh, mb.MemberVN, dataTime, typeExport, reportTemplate);
                        }

                        stt++;
                    }
                }
                if (typeExport == "Excel")
                {
                    ExportToExCelAggrerate(folderPath);
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
        private void SaveAll(List<BhDistributionViewModel> dataSource,string pathfull, string bhmedia, string member, DateTime dtReport,string typeExport, int reportTemplate)
        {
            try
            {
                //khong co du lieu, bo qua
                if(dataSource.Count == 0)
                {
                    return;
                }
                var reportDatSource = new ReportDataSource("DataSet1", dataSource);

                ReportParameter[] reportParameters = new ReportParameter[] {
                        //new ReportParameter("strdate", dtReport.ToString("dd")),
                        //new ReportParameter("strmonth",dtReport.ToString("MM")),
                        //new ReportParameter("stryear",dtReport.ToString("yyy")),
                        new ReportParameter("strMember", bhmedia),
                        new ReportParameter("strBh",member),

                        //new ReportParameter("strPeopleSign", member),
                    };

                LocalReport localReport = new LocalReport();
                string type = "";
                if(reportTemplate!=0)
                {
                    type = reportTemplate.ToString();
                }
                string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\" +$"DistributionReport{type}.rdlc";
                //string fullPath = Core.PathReport + $"DistributionReport{type}.rdlc";
                localReport.ReportPath = path;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(reportDatSource);
                localReport.SetParameters(reportParameters);
                //localReport.      
                if(typeExport == "PDF")
                {
                    var data = localReport.Render(typeExport); // Excel PDF
                    FileStream newFile = new FileStream(pathfull, FileMode.Create);
                    newFile.Write(data, 0, data.Length);
                    newFile.Close();
                }
                else
                {
                    var data = localReport.Render(typeExport); // Excel PDF
                    FileStream newFile = new FileStream(pathfull, FileMode.Create);
                    newFile.Write(data, 0, data.Length);
                    newFile.Close();
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
        private void PrinterAll()
        {
            try
            {
                //int stt = 1;
                foreach (var item in dict)
                {
                    ItemMember mb = null;
                    for (int i = 0; i < itemMembers.Count; i++)
                    {
                        if (item.Key == itemMembers[i].Id)
                        {
                            mb = itemMembers[i];
                            break;
                        }
                    }
                    if (mb != null)
                    {
                        if(mb.Member == "NGOC LE")
                        {
                            Printer(item.Value, strBh, mb.MemberVN, dataTime);
                        }                        
                    }
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
        private void Printer(List<BhDistributionViewModel> dataSource, string bhmedia, string member, DateTime dtReport)
        {
            try
            {
                var reportDatSource = new ReportDataSource("DataSet1", dataSource);
                ReportParameter[] reportParameters = new ReportParameter[] {
                        new ReportParameter("strdate", dtReport.ToString("dd")),
                        new ReportParameter("strmonth",dtReport.ToString("MM")),
                        new ReportParameter("stryear",dtReport.ToString("yyy")),

                        new ReportParameter("strMember", bhmedia),
                        new ReportParameter("strBh",member),

                        new ReportParameter("strPeopleSign", member),
                    };


                LocalReport localReport = new LocalReport();               
                string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\report\template\"+ @"DistributionReport.rdlc";
                //string path = Core.PathReport + @"DistributionReport.rdlc";               
                localReport.ReportPath = path;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(reportDatSource);
                localReport.SetParameters(reportParameters);

                PrinterHelper printerHelper = new PrinterHelper();
                //PrinterHelper.pa
                PrinterHelper.PrintToPrinter(localReport, true);
                //break;

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
        
    }
}
