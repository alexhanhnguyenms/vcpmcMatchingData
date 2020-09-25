using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Infrastructure.data;
using Vcpmc.Mis.ApplicationCore.Entities.dis;
using Vcpmc.Mis.ApplicationCore.Entities.makeData;
using Vcpmc.Mis.Common;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Infrastructure;
using System.IO;
using Vcpmc.Mis.AppMatching.form.Distribution.BH;

namespace Vcpmc.Mis.AppMatching.form.mic.distribution
{
    public partial class frmDistribution2 : Form
    {
        //
        VcpmcContext ctx = new VcpmcContext();
        List<MemberBH> memberBh = new List<MemberBH>();
        List<ImportMapWorkMember> workMember = new List<ImportMapWorkMember>();
        List<ExceptionWork> exceptWorkMember = new List<ExceptionWork>();
        //List<ExceptionWork> incudeWorkMember = new List<ExceptionWork>();
        Guid guidExceptWorkMember;
        Guid guidBhMember;
        Guid guidWorkMember;
        string currentIdDsChoise = "";
        OperationType Operation = OperationType.LoadExcel;
        DistributionData distributionData = null;
        List<DistributionDataItem> distributionDataItems = new List<DistributionDataItem>();
        bool isInnit = false;

        #region innit
        public frmDistribution2()
        {
            InitializeComponent();
        }
        private void frmDistribution2_Load(object sender, EventArgs e)
        {
#if DEBUG
            //string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\template\data.xlsx";
            string quarter = "01";
            int month = DateTime.Now.Month;
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    quarter = "01";
                    break;
                case 4:
                case 5:
                case 6:
                    quarter = "02";
                    break;
                case 7:
                case 8:
                case 9:
                    quarter = "03";
                    break;
                case 10:
                case 11:
                case 12:
                    quarter = "04";
                    break;
                default:
                    quarter = "01";
                    break;
            }           
            string path = @"D:\Solution\Source Code\Matching data\BH\template\PP" + $"{DateTime.Now.Year}{quarter}\\" + @"data.xlsx";
            tstxtPath.Text = path;
#endif
           
            InnitData();
        }
        #endregion

        #region tool       
        private void toolStripButton1_Click(object sender, EventArgs e)
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

        private void txtGetListExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsBtnGetDataFromDB_Click(object sender, EventArgs e)
        {
            try
            {
                frmDistributionChoisePO f = new frmDistributionChoisePO();
                f.ShowDialog();
                if (f.IdDsChoise != string.Empty)
                {
                    currentIdDsChoise = f.IdDsChoise;
                    Operation = OperationType.LoadDB;

                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Must choising Lis PO!");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Get data from Database be error!");
            }
        }

        private void sbbtnSaveDataToDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save data to database?", "Confirm Save data to Database",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    #region Save
                    Operation = OperationType.SaveDatabase;
                    pcloader.Visible = true;
                    pcloader.Dock = DockStyle.Fill;
                    backgroundWorker1.RunWorkerAsync();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void tssLookup_Click(object sender, EventArgs e)
        {
            try
            {
                if(!isInnit)
                {
                    MessageBox.Show("Condtion Data is empty, please Import: work-member, except work member, Bhmedia member!");
                    return;
                }
                guidExceptWorkMember = (Guid)cboExceptWorkMember.SelectedValue;
                guidBhMember = (Guid)cboBhMember.SelectedValue;
                guidWorkMember = (Guid)cboWorkMember.SelectedValue;

                if(distributionData!=null)
                {
                    distributionData.MemberBHId = guidBhMember;
                    distributionData.ImportMapWorkMemberId = guidWorkMember;
                    distributionData.ExceptionWorkId = guidExceptWorkMember;                   
                }
                Operation = OperationType.Lookup;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            InnitData();
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
                else if (Operation == OperationType.LoadDB)
                {
                    LoadDatafromDB(currentIdDsChoise);

                }
                else if (Operation == OperationType.SaveDatabase)
                {
                    SaveDataToDatabase();
                }
                else if (Operation == OperationType.Lookup)
                {
                    LookupCA();
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
                isInnit = false;
                //1.MemberBHs
                memberBh = (from s in ctx.MemberBHs
                                orderby s.TimeCreate descending
                            //select new
                            //{
                            //    s.Id,
                            //    s.Name
                            //    //name = $"Name: {s.Name} , Create time: { s.TimeCreate.ToString("dd/MM/yyyy HH:mm:ss")}, User: { s.User }"
                            //}
                            select s
                                ).ToList();
                cboBhMember.ValueMember = "Id";
                cboBhMember.DisplayMember = "Name";
                cboBhMember.DataSource = memberBh;
                if (memberBh.Count > 0)
                {
                    cboBhMember.SelectedItem = 0;
                   
                }
                //2.importMapWorkMembers
                workMember = (from s in ctx.importMapWorkMembers
                              orderby s.TimeCreate descending
                              //select new
                              //{
                              //    s.Id,
                              //    s.Name
                              //    //name = $"Name: {s.Name} , Create time: { s.TimeCreate.ToString("dd/MM/yyyy HH:mm:ss")}, User: { s.User }"
                              //}
                              select s
                                ).ToList();
                cboWorkMember.ValueMember = "Id";
                cboWorkMember.DisplayMember = "Name";
                cboWorkMember.DataSource = workMember;
                if (workMember.Count > 0)
                {
                    cboWorkMember.SelectedItem = 0;

                }
                //3.ExceptionWorks
                exceptWorkMember = (from s in ctx.ExceptionWorks                                   
                                    orderby s.TimeCreate descending
                                    //select new
                                    //{
                                    //    s.Id,
                                    //    s.Name
                                    //    //name = $"Name: {s.Name} , Create time: { s.TimeCreate.ToString("dd/MM/yyyy HH:mm:ss")}, User: { s.User }"
                                    //}
                                    select s
                                ).ToList();
                cboExceptWorkMember.ValueMember = "Id";
                cboExceptWorkMember.DisplayMember = "Name";
                cboExceptWorkMember.DataSource = exceptWorkMember;
                if (exceptWorkMember.Count > 0)
                {
                    cboExceptWorkMember.SelectedItem = 0;
                }
                if(memberBh.Count == 0 || workMember.Count == 0 || exceptWorkMember.Count == 0)
                {
                    isInnit = false;
                }
                else
                {
                    isInnit = true;
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Load data error! Please check again");
            }
        }
        private void LoadDtaFromExcel()
        {
            try
            {
                distributionData = new DistributionData();                
                distributionData.Name = $"CALC-DISTRIBUTION-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                distributionData.TimeCreate = DateTime.Now;
                distributionData.User = Core.User;
                distributionData.Id = Guid.NewGuid();
                ExcelHelper excelHelper = new ExcelHelper();
                int countread = 0;
               
                distributionDataItems = excelHelper.ReadExcelDistribution(distributionData.Id, tstxtPath.Text);
               
                

                //var orderByDescendingResult = (from s in importMapWorkMemberDetails
                //                               orderby s.No ascending
                //                               select s).ToList();
                if (distributionDataItems != null)
                {
                    countread = distributionDataItems.Count;
                    distributionData.TotalRecord = distributionDataItems.Count;
                    distributionData.Note = $"Load data from excel is successfull, Total record: {countread}";


                    distributionData.DistributionDataItems = distributionDataItems;
                    distributionData.TotalRecord = distributionDataItems.Count;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = distributionDataItems;
                        for (int i = 0; i < dgvEditFileImport.Rows.Count; i++)
                        {
                            if ((bool)dgvEditFileImport.Rows[i].Cells["StatusLoad"].Value == true)
                            {
                                dgvEditFileImport.Rows[i].Cells["StatusLoad"].Style.BackColor = Color.Green;
                                dgvEditFileImport.Rows[i].Cells["StatusLoad"].Style.ForeColor = Color.White;
                            }
                            else
                            {
                                dgvEditFileImport.Rows[i].Cells["StatusLoad"].Style.BackColor = Color.Red;
                                dgvEditFileImport.Rows[i].Cells["StatusLoad"].Style.ForeColor = Color.White;
                            }
                            if (dgvEditFileImport.Rows[i].Cells["strContractTime"].Value.ToString() != string.Empty)
                            {
                                dgvEditFileImport.Rows[i].Cells["strContractTime"].Style.BackColor = Color.Red;
                                dgvEditFileImport.Rows[i].Cells["strContractTime"].Style.ForeColor = Color.White;
                            }                           
                        }
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = distributionData.Note;
                    }));
                }
                else
                {
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = new List<DistributionDataItem>();
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Load data from Excel file be error!";
                    }));
                }
            }
            catch (Exception ex)
            {
                distributionData = new DistributionData();
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDatafromDB(string currentIdDsChoise)
        {
            try
            {
                distributionDataItems = new List<DistributionDataItem>();
                Guid guid = new Guid(currentIdDsChoise);                
                var data = (from s in ctx.DistributionDataItems
                            where s.DistributionDataId == guid
                            orderby s.WorkInNo ascending
                            select s).ToList();
                if (data != null)
                {
                    distributionDataItems = data;
                    dgvEditFileImport.Invoke(new MethodInvoker(delegate
                    {
                        dgvEditFileImport.DataSource = distributionDataItems;
                    }));
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        var item = ctx.importMapWorkMembers.FirstOrDefault(x => x.Id.ToString() == currentIdDsChoise);
                        if (item != null)
                        {
                            tssLblInfo.Text = item.Note;
                        }
                    }));
                }
                else
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("Load data from database be error!");
                }
                currentIdDsChoise = "";
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

        private void SaveDataToDatabase()
        {
            try
            {
                if (distributionData == null || distributionDataItems == null)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("importMapWorkMember is null or ImportMapWorkMemberDetails is null");
                }
                else
                {

                    //if (distributionData != null)
                    //{
                    //    distributionData.MemberBHId = guidBhMember;
                    //    distributionData.ImportMapWorkMemberId = guidWorkMember;
                    //    distributionData.ExceptionWorkId = guidExceptWorkMember;
                    //}
                    ctx.DistributionDatas.Add(distributionData);
                    foreach (var item in distributionDataItems)
                    {
                        item.Royalty = decimal.Round(item.Royalty, 6);
                        ctx.DistributionDataItems.Add(item);
                    }
                    ctx.SaveChanges();
                    statusTripMain.Invoke(new MethodInvoker(delegate
                    {
                        tssLblInfo.Text = "Save data to database be successfull";
                    }));
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

        private void LookupCA()
        {
            try
            {
                #region pre-data  
                VcpmcContext ctx = new VcpmcContext();
                //1.member detail
                var listMemberBhDetail = (from s in ctx.MemberBHDetails
                                      where s.MemberBHId == guidBhMember
                                      select s).ToList();
                if(listMemberBhDetail.Count == 0)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("Please import Bhmedia Member");
                    return;
                }
                string[] arrStrBhDetailTmp = null;
                foreach (var item in listMemberBhDetail)
                {                    
                    //1.1.but danh
                    if (item.Type == "SINGLE")
                    {
                        arrStrBhDetailTmp = item.StageName.Split(',');
                        foreach (var itemD in arrStrBhDetailTmp)
                        {
                            item.ListStageName.Add(itemD.Trim());
                        }
                    }                    
                    //1.2. Thanh vien nhom
                    else
                    {
                        arrStrBhDetailTmp = item.SubMember.Split(',');
                        foreach (var itemD in arrStrBhDetailTmp)
                        {
                            item.ListSubMember.Add(itemD.Trim());
                        }
                    }
                }
                //2.
                var listWorkMemberDetail = (from s in ctx.ImportMapWorkMemberDetails
                              where s.ImportMapWorkMemberId == guidWorkMember
                              select s).ToList();
                if (listWorkMemberDetail.Count == 0)
                {
                    pcloader.Invoke(new MethodInvoker(delegate
                    {
                        pcloader.Visible = false;
                    }));
                    MessageBox.Show("Please import work-member");
                    return;
                }
                //3. danh sach loai tru                
                var listExceptionWorkDetail = (from s in ctx.ExceptionWorkDetails
                              where s.ExceptionWorkId == guidExceptWorkMember && s.Type == "EXCEPT"
                              select s).ToList();
                string[] arrPoolName = null;
                foreach (var item in listExceptionWorkDetail)
                {
                    arrPoolName = item.PoolName.Split(';');
                    foreach (var itemD in arrPoolName)
                    {
                        if(itemD.Trim()!="")
                        {
                            item.ListPoolName.Add(itemD.Trim());
                        }                        
                    }
                }
                var listIncudeWorkDetail = (from s in ctx.ExceptionWorkDetails
                                               where s.ExceptionWorkId == guidExceptWorkMember && s.Type == "INCLUDE"
                                               select s).ToList();
                string[] arrPoolNameIncude = null;
                foreach (var item in listIncudeWorkDetail)
                {
                    arrPoolNameIncude = item.PoolName.Split(';');
                    foreach (var itemD in arrPoolNameIncude)
                    {                        
                        if (itemD.Trim() != "")
                        {
                            item.ListPoolName.Add(itemD.Trim());
                        }
                    }
                }
                #endregion

                #region map data
                //int pos = -1;
                foreach (var item in distributionDataItems)
                {                     

                    #region I.tim tac gia dua vao bai hat
                    item.TotalAuthor = "";
                    //ImportMapWorkMemberDetail detail = null;
                    foreach (var wm in listWorkMemberDetail)
                    {
                        //danh sach bai hat can tim = danh sach nap vao mau: dua vao ten=> lay duoc tac gia 
                        if (item.WorkInNo == wm.Internal)
                        {
                            //hien lay ca ma va ten bị sai
                            //detail = wm;
                            //if (item.Title2.Trim() == wm.Title2.Trim())
                            //{
                            //    if (item.Title2 == "SAU LE BONG 2")
                            //    {
                            //        int A = 1;
                            //    }
                            //    item.TotalAuthor = wm.TotalAuthor;
                            //    item.IsMapAuthor = true;
                            //    break;
                            //}
                            //else
                            //{
                            //    int a = 1;
                            //}
                            item.TotalAuthor = wm.TotalAuthor;
                            item.IsMapAuthor = true;
                            break;
                        }
                    }
                    if (item.TotalAuthor.Length == 0)
                    {
                        item.IsMapAuthor = false;
                        continue;
                    }
                    #endregion                 

                    #region II.map voi danh sach bh de lay thong tin
                    //1.lay duoc tac gia
                    //lay danh sach tac gia BH nam trong danh sach
                    //#region Lay tac gia BH
                    string[] arrAyyOuthor = item.TotalAuthor.Split(',');
                    for (int i = 0; i < arrAyyOuthor.Length; i++)
                    {
                        arrAyyOuthor[i] = arrAyyOuthor[i].Trim();
                    }                   
                   
                    //2.tim chi tiet tac gia trong danh sach BH, dua vao ten tac gia
                    //arrAyyOuthor: VINH SU, BHMEDIA
                    //arrAyyOuthor: CO PHUONG, TRINH CONG SON, BHMEDIA
                    //Member: VINH SU       
                    MemberBHDetail memberHbDetailTmp = null;                    
                    foreach (var temp in arrAyyOuthor)
                    {
                        if(memberHbDetailTmp == null)//danh dau de lay tac gia dau tien
                        {
                            foreach (var tempD in listMemberBhDetail)
                            {
                                if (tempD.Type == "SINGLE")
                                {
                                    if (tempD.ListStageName.Count > 0)
                                    {
                                        if (StringHelper.CheckStringInArrayString(temp, tempD.ListStageName))
                                        {
                                            if (item.Title2 == "THUONG TIEC MOT DOI HOA")
                                            {
                                                //int a = 1;
                                            }                                            
                                            memberHbDetailTmp = tempD;
                                            break;
                                        }
                                    }
                                }
                            }
                        }                        
                    }                    
                    if (memberHbDetailTmp == null)
                    {
                        continue;
                    }
                    else
                    {                       
                        item.SubMember = memberHbDetailTmp.SubMember;                      
                        item.Beneficiary = memberHbDetailTmp.Beneficiary;
                        item.GetPart = memberHbDetailTmp.GetPart;
                        item.IsAlwaysGet = memberHbDetailTmp.IsAlwaysGet;
                        item.returnDate = memberHbDetailTmp.returnDate;
                        item.Percent = memberHbDetailTmp.Percent;
                        item.IsGiveBeneficiary = memberHbDetailTmp.IsGiveBeneficiary;
                        item.IsCreateReport = memberHbDetailTmp.IsCreateReport;
                        item.Note = memberHbDetailTmp.Note;
                        //xac dinh map bang single
                        item.IsMapByGroup = false;
                    }                    
                    item.BhAuthor = memberHbDetailTmp.ListStageName[0];
                    #endregion

                    //test
                    //if (item.BhAuthor == "ANH BANG")
                    //{
                    //    int a = 1;
                    //    if(item.Title2 == "NGAP NGUNG")
                    //    {
                    //        int ii = 1;
                    //    }
                    //}

                    #region III.calc
                    if (item.IsAlwaysGet)
                    {
                        item.IsCondittionTime = true;                       
                        if (item.Note2.Length > 0)
                        {
                            item.Note2 += ",";
                        }
                        item.Note2 += "ALLWAYS";
                    }
                    else
                    {
                        if(item.returnDate == null)
                        {
                            item.IsCondittionTime = false;
                        } 
                        else
                        {
                            //if (item.returnDate >= item.ContractTime)
                            if (item.ContractTime >= item.returnDate)
                            {
                                item.IsCondittionTime = true;                               
                            }
                            else
                            {
                                item.IsCondittionTime = false;                               
                                if (item.Note2.Length > 0)
                                {
                                    item.Note2 += ",";
                                }
                                item.Note2 += "CONTRACT TIME LESS DISTRIBUTION TIME";
                            }
                        }                        
                    }
                    #endregion

                    #region Bao gom
                    if (listIncudeWorkDetail.Count > 0)
                    {                        
                        var filter = listIncudeWorkDetail.FindAll(x => x.Member2 == item.BhAuthor).ToList();
                        if (filter != null && filter.Count > 0)
                        {
                            //tac gia chi ban trong 10 bai nay thoi
                            bool isNameTrong = false;

                            #region Xac dinh co nam trong nhung tac pham ban hay khong
                            foreach (var f in filter)
                            {
                                //NEU BAI HAT DANG XET NAM TRONG DANH SACH
                                if (item.Title2 == f.Title2)
                                {
                                    if (f.ListPoolName.Count > 0)
                                    {
                                        #region Chi bao gom linh vuc
                                        //THI XET TOI THUOC NHOM NHAC CHUONG, NHACC= CHO, MIDE FILE
                                        if (StringHelper.CheckStringContainsArrayString(item.PoolName.Trim(), f.ListPoolName))
                                        {
                                            //thanh son chi ban 10 bai  
                                            isNameTrong = true;
                                            //item.IsCondittionTime = true;                                        
                                            //if (item.Note2.Length > 0)
                                            //{
                                            //    item.Note2 += ",";
                                            //}
                                            //item.Note2 += "INCLUDE";
                                            break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        //item.IsCondittionTime = true;
                                        isNameTrong = true;
                                        //if (item.Note2.Length > 0)
                                        //{
                                        //    item.Note2 += ",";
                                        //}
                                        //item.Note2 += "INCLUDE";
                                        break;

                                    }
                                }

                            }
                            #endregion

                            #region Neu khong nam trong thi luon la tac gia
                            if (!isNameTrong)
                            {
                                //khong nam trong danh sach ban, no thuoc ve tac gia
                                item.IsCondittionTime = false;
                            }
                            else
                            {
                                //int a = 1;
                            }
                            #endregion
                        }
                        
                    }
                    #endregion

                    #region IV.ngoai tru
                    //if(item.Title2== "CAU TRE KY NIEM" && item.PoolName2== "NHAC CHUONG, NHAC CHO" 
                    //    && item.IsCondittionTime ==false)
                    //{
                    //    int a = 1;
                    //}
                    if (listExceptionWorkDetail.Count > 0)
                    {
                        //LAY DANH SACH NGOAI TRU THEO TAC GIA
                        var filter = listExceptionWorkDetail.FindAll(x => x.Member2 == item.BhAuthor).ToList();
                        if (filter != null && filter.Count > 0)
                        {
                            foreach (var f in filter)
                            {
                                //NEU BAI HAT DANG XET NAM TRONG DANH SACH
                                if (item.Title2 == f.Title2)
                                {
                                    if(f.ListPoolName.Count > 0)
                                    {
                                        #region Loai tri theo linh vuc
                                        //THI XET TOI THUOC NHOM NHAC CHUONG, NHACC= CHO, MIDE FILE
                                        if (StringHelper.CheckStringContainsArrayString(item.PoolName.Trim(), f.ListPoolName))
                                        {
                                            //vinh su
                                            //ban cho maseco: 22/01/2014
                                            //ban cho hb: 01/02/2018: nen luon cua maseco
                                            //TODO: can truyen time vao de so sanh
                                            item.IsCondittionTime = true;//hien ong vinh su 
                                            item.IsExcept = true;
                                            if (item.Note2.Length > 0)
                                            {
                                                item.Note2 += ",";
                                            }
                                            item.Note2 += "EXCEPTION";
                                            break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Loai tru theo toan bo
                                        item.IsExcept = true;
                                        #endregion
                                    }

                                }
                            }                            
                        }
                    }
                    #endregion

                    #region Nếu là nhóm: lấy ngày theo nhóm
                    foreach (var tempD in listMemberBhDetail)
                    {
                        if (tempD.Type == "GROUP")
                        {
                            if (tempD.ListSubMember.Count > 0)
                            {
                                //neu la nhom thi pool source name phai bang group
                                //kiem tra tac gia co nam trong nhom thanh vien khong
                                string[] strSoureName = null;
                                if (item.PoolName2 == "GOOGLE")
                                {
                                    #region Nếu là google thì lấy theo quý
                                    DateTime? time = null;
                                    if (item.SourceName.Substring(0, 3) == "HCM")
                                    {
                                        //hcm: HCM20142831_GOOGLE_YOUTUBE_Q4_2019_MR
                                        //item.SourceName = "HN20142831_GOOGLE_YOUTUBE_Q324_2019_MR";
                                        //item.SourceName = "HCM20142831_GOOGLE_2019_MR";
                                        strSoureName = item.SourceName.Split('_');
                                        if (strSoureName.Length > 2)
                                        {
                                            string strYear = strSoureName[strSoureName.Length - 2];
                                            string quy = strSoureName[strSoureName.Length - 3];
                                            if (quy.Length > 1 && quy.Length < 6 & quy.Substring(0, 1).ToUpper() == "Q")
                                            {
                                                quy = quy.Substring(1, quy.Length - 1);
                                                try
                                                {
                                                    int conver = int.Parse(quy);
                                                    time = CreateTime(strYear, quy);
                                                }
                                                catch (Exception)
                                                {
                                                    time = null;
                                                }
                                            }
                                            else
                                            {
                                                time = null;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        //TODO: cho ha noi format
                                    }
                                    if (time != null)
                                    {
                                        item.ContractTime = (DateTime)time;
                                    }
                                    else
                                    {
                                        //int a = 1;
                                    }
                                    #endregion
                                }
                                if (item.PoolName2 == tempD.Member && StringHelper.CheckStringInArrayString(item.BhAuthor, tempD.ListSubMember))
                                {
                                    //nếu cần map ma ngay la không xác định thì coi như không cần map
                                    if (tempD.IsAlwaysGet == false && tempD.returnDate == null)
                                    {
                                        break;
                                    }
                                    item.SubMember = tempD.SubMember;
                                    item.Beneficiary = tempD.Beneficiary;
                                    item.GetPart = tempD.GetPart;
                                    item.IsAlwaysGet = tempD.IsAlwaysGet;
                                    if(item.PoolName2 != "GOOGLE")
                                    {
                                        item.returnDate = tempD.returnDate;
                                    }                                    
                                    item.Percent = tempD.Percent;
                                    item.IsGiveBeneficiary = tempD.IsGiveBeneficiary;
                                    item.IsCreateReport = tempD.IsCreateReport;
                                    item.Note = tempD.Note;
                                    //chuyen sang map nhom
                                    item.IsMapByGroup = true;

                                    if (item.IsAlwaysGet)
                                    {
                                        item.IsCondittionTime = true;
                                        if (item.Note2.Length > 0)
                                        {
                                            item.Note2 += ",";
                                        }
                                        item.Note2 += "ALLWAYS";                                        
                                    }
                                    else
                                    {
                                        if(tempD.returnDate == null)
                                        {
                                            item.IsCondittionTime = false;
                                        }  
                                        else
                                        {
                                            //if (memberHbDetailTmp.returnDate >= item.ContractTime)
                                            if (item.ContractTime >= memberHbDetailTmp.returnDate)
                                            {
                                                item.IsCondittionTime = true;                                               
                                            }
                                            else
                                            {
                                                item.IsCondittionTime = false;                                               
                                                if (item.Note2.Length > 0)
                                                {
                                                    item.Note2 += ",";
                                                }
                                                item.Note2 += "CONTRACT TIME LESS DISTRIBUTION TIME";
                                            }
                                        }                                        
                                    }
                                    break;
                                }
                            }
                        }
                    }                    
                    #endregion
                }
                #endregion

                #region test danh sach khong co tac gia
                string listEmpty = "";
                foreach (var item in distributionDataItems)
                {
                    if(item.BhAuthor=="")
                    {
                        listEmpty += item.WorkInNo + ",";
                        //t a = 1;
                    }
                }
                //string sfsd = "";
                #endregion

                #region Show
                //dgvEditFileImport.Invoke(new MethodInvoker(delegate
                //{
                //    dgvEditFileImport.DataSource = distributionDataItems;
                //    for (int i = 0; i < dgvEditFileImport.Rows.Count; i++)
                //    {
                //        if ((bool)dgvEditFileImport.Rows[i].Cells["IsCondittionTime"].Value == false)
                //        {
                //            dgvEditFileImport.Rows[i].Cells["IsCondittionTime"].Style.BackColor = Color.Red;
                //            dgvEditFileImport.Rows[i].Cells["IsCondittionTime"].Style.ForeColor = Color.White;
                //        }

                //        //if ((bool)dgvEditFileImport.Rows[i].Cells["IsExeptWork"].Value == true)
                //        //{
                //        //    dgvEditFileImport.Rows[i].Cells["IsExeptWork"].Style.BackColor = Color.Red;
                //        //    dgvEditFileImport.Rows[i].Cells["IsExeptWork"].Style.ForeColor = Color.White;
                //        //}
                //        if (dgvEditFileImport.Rows[i].Cells["Beneficiary"].Value.ToString() == string.Empty)
                //        {
                //            dgvEditFileImport.Rows[i].Cells["Beneficiary"].Style.BackColor = Color.Red;
                //            dgvEditFileImport.Rows[i].Cells["Beneficiary"].Style.ForeColor = Color.White;
                //        }
                //        //if ((bool)dgvEditFileImport.Rows[i].Cells["IsGoogle"].Value == true)
                //        //{
                //        //    dgvEditFileImport.Rows[i].Cells["IsGoogle"].Style.BackColor = Color.Red;
                //        //    dgvEditFileImport.Rows[i].Cells["IsGoogle"].Style.ForeColor = Color.White;
                //        //}
                //    }
                //}));
                statusTripMain.Invoke(new MethodInvoker(delegate
                {
                    tssLblInfo.Text = "Lookup CA is successfull";
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
        private  DateTime? CreateTime(string strYear, string quy)
        {
            DateTime? time =null;
            try
            {               
                int month = 1;
                int temp = 10;
                string MinQuy = "1";
                foreach (var item in quy)
                {                   
                    if(int.Parse(item.ToString())<=temp)
                    {
                        temp = int.Parse(item.ToString());
                    }
                }
                if(temp!=10)
                {
                    MinQuy = temp.ToString();
                }
                switch (MinQuy)
                {
                    case "1":
                        month = 1;//1,2,3
                        break;
                    case "2":
                        month = 4;//4,5,6
                        break;
                    case "3":
                        month = 7;//7,8,9
                        break;
                    case "4":
                        month = 10;//10,11,12
                        break;
                    default:
                        month = 1;
                        break;
                }
                time = new DateTime(int.Parse(strYear), month, 1);
            }
            catch (Exception)
            {
                return null;
            }
            return time;
        }
        #endregion       
    }
}
