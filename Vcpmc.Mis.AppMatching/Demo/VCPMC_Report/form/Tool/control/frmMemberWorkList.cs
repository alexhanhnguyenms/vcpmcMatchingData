using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class frmMemberWorkList : Form
    {
        List<MemberWorkList> ediFilesItems = new List<MemberWorkList>();
        List<MemberWorkList> ediFilesItemsClone = new List<MemberWorkList>();
        int totalLoad = 0;
        int totalRest = 0;
        int totalRestAffCalc = 0;
        int totalDuplicate = 0;
        OperationType Operation = OperationType.LoadExcel;
        
        bool isGroupByMember = false;       
        bool isLoad = false;       
        bool isFilter = false;
        bool _comareTitleAndWriter = false;
        /// <summary>
        /// loai bao cao nhan tu mis
        /// </summary>
        int GenerateType = 0;
        int CompareTW = 0;
        float rateWriterMatched = 0;       
        #region init
        public frmMemberWorkList()
        {
            InitializeComponent();
        }
        private void frmMemberWorkList_Load(object sender, EventArgs e)
        {          
           
            
            cboType.SelectedIndex = 1;
            
        }
        
        
        #endregion

        #region btn
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
        private void btnExcel_import_Click(object sender, EventArgs e)
        {
            try
            {
                cboTypeChoise = cboType.SelectedIndex;
                Operation = OperationType.LoadExcel;
                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception)
            {

                //throw;
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
                else if (Operation == OperationType.ExportToExcel)
                {
                    ExportToExcel(currentDirectory);

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
                isLoad = true;                
                ExcelHelper excelHelper = new ExcelHelper();               
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
                ediFilesItems = new List<MemberWorkList>();
                ediFilesItemsClone = new List<MemberWorkList>();
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
                ediFilesItems = excelHelper.ReadExcelMemberEorkList(tstxtPath.Text);

                #region 1.Loai bo dong trong   
                List<MemberWorkList> editFiles = new List<MemberWorkList>();
                if (ediFilesItems != null)
                {
                    ediFilesItemsClone.Clear();
                    string WorkInternalNo = string.Empty;
                    string INTERNAL_NO = string.Empty;
                    int pos = 0;
                    string TITLE2 = string.Empty;
                    string TITLE3 = string.Empty;

                    string name = string.Empty;
                    string name3 = string.Empty;
                    string[] arrayname = null;
                    string text, text2;
                    string[] arrWriterReal;
                    for (int i = 0; i < ediFilesItems.Count; i++)
                    {
                        name = ediFilesItems[i].NAME;
                        arrayname = name.Split(',');
                        if (arrayname.Length != 2)
                        {
                            continue;
                        }
                        if (arrayname[1].Trim() == string.Empty)
                        {
                            name = arrayname[0].Trim();
                            name3 = name;
                        }
                        else
                        {
                            if(name == "TRAN THIEN, THANH TRẦN THIỆNTHANH")
                            {
                                int a = 1;
                            }
                            //TRAN THIEN, THANH TRẦN THIỆNTHANH
                            //TRAN THIEN
                            arrayname[0] = arrayname[0].Trim();
                            name = $"{arrayname[0]} ";
                            //THANH TRẦN THIỆNTHANH
                            arrayname[1] = arrayname[1].Trim();
                            name3 = arrayname[1];
                            //THANH:TRẦN:THIỆNTHANH
                            arrayname = arrayname[1].Split(' ');
                            arrayname[0] = arrayname[0].Trim();
                            //TRAN THIEN THANH
                            name += arrayname[0];
                            //TRẦN THIỆNTHANH
                            pos = name3.IndexOf(arrayname[0]);
                            if(pos >= 0)
                            {
                                name3 = name3.Substring(pos + arrayname[0].Length, name3.Length - arrayname[0].Length - pos).Trim();
                            }                            
                            #region nếu là tác giả việt
                            if (name3.Length > 0)
                            {
                                text = name3.Replace(" ", "");
                                text2 = VnHelper.ConvertToUnSign(text);
                                arrWriterReal = name.Split(' ');
                                name3 = string.Empty;
                                for (int k = 0; k < arrWriterReal.Length; k++)
                                {
                                    pos = text2.IndexOf(arrWriterReal[k]);
                                    if (pos >= 0)
                                    {
                                        int x = text.Length;
                                        int x1 = text2.Length;
                                        name3 += text.Substring(pos, arrWriterReal[k].Length) + " ";
                                        text = text.Substring(arrWriterReal[k].Length, text.Length - arrWriterReal[k].Length);
                                        text2 = text2.Substring(arrWriterReal[k].Length, text2.Length - arrWriterReal[k].Length);
                                    }                                    
                                }                               
                            }
                            #endregion
                        }
                        #region general report: thuong dung
                        if (ediFilesItems[i].INTERNAL_NO != string.Empty)
                        {
                            INTERNAL_NO = ediFilesItems[i].INTERNAL_NO;
                            var x = (MemberWorkList)ediFilesItems[i].Clone();
                            pos = x.TITLE.Length / 2;
                            TITLE2 = x.TITLE.Substring(0,pos).Trim();
                            TITLE3 = x.TITLE.Substring(pos, x.TITLE.Length - pos).Trim();
                            if(VnHelper.Detect(TITLE3) && TITLE2 == VnHelper.ConvertToUnSign(TITLE3))
                            {
                                x.TITLE2 = TITLE2;
                                x.TITLE3 = TITLE3;
                            } 
                            else
                            {
                                x.TITLE2 = x.TITLE;
                                x.TITLE3 = x.TITLE;
                            }
                            x.NAME2 = name;
                            x.NAME3 = name3.Trim();
                            ediFilesItemsClone.Add(x);
                        }
                        else
                        {
                            var x = ediFilesItemsClone.Where(p => p.INTERNAL_NO == INTERNAL_NO).FirstOrDefault();
                            if(x!=null)
                            {
                                if (x.NAME_TYPE != string.Empty) x.NAME_TYPE += ", ";
                                x.NAME_TYPE += ediFilesItems[i].NAME_TYPE;

                                if (x.ROLE != string.Empty) x.ROLE += ", ";
                                x.ROLE += ediFilesItems[i].ROLE;

                                if (x.SOCIETY != string.Empty) x.SOCIETY += ", ";
                                x.SOCIETY += ediFilesItems[i].SOCIETY;

                                if (x.NAME2 != string.Empty) x.NAME2 += ", ";
                                x.NAME2 += name;

                                if (x.NAME3 != string.Empty) x.NAME3 += ", ";
                                x.NAME3 += name3.Trim();
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

                    }));
                    FilterData();
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
        
        #endregion

        #region Export
        
        string currentDirectory = "";       
        private void btnExportDefault_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {                    
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

        
        private void ExportToExcel(string folderPath)
        {
            try
            {
                //progressBarImport
                //lbPercent
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbPercent.Text = "0%";
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 0;
                }));
                ExcelHelper excelHelper = new ExcelHelper();
                bool check = excelHelper.WriteToExcelMemberEorkList(ediFilesItemsClone, folderPath);
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    progressBarImport.Value = 100;
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbPercent.Text = "100%";
                }));
                statusMain.Invoke(new MethodInvoker(delegate
                {
                    lbOperation.Text = "Export to Excel is successfull!";
                }));
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

        #region Filter       
        
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
                isFilter = true;
                List<MemberWorkList> fill = new List<MemberWorkList>();
                if (cboTypeChoise == 0)
                {
                    //var query = ediFilesItemsClone.Where(delegate (WorkViewModel c)
                    //{
                    //    if (VnHelper.ConvertToUnSign(c.WK_INT_NO).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    //        return true;
                    //    else
                    //        return false;
                    //}).AsQueryable();
                    var query = ediFilesItemsClone.Where(c => c.INTERNAL_NO.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 1)
                {
                    var query = ediFilesItemsClone.Where(delegate (MemberWorkList c)
                    {
                        if (VnHelper.ConvertToUnSign(c.TITLE).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.GroupWriter.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                else if (cboTypeChoise == 2)
                {
                    var query = ediFilesItemsClone.Where(delegate (MemberWorkList c)
                    {
                        if (VnHelper.ConvertToUnSign(c.NAME).IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                    //var query = ediFilesItemsClone.Where(c => c.GroupWriter.IndexOf(txtFind.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                    fill = query.ToList();
                }
                dgvEditCalcXXX.Invoke(new MethodInvoker(delegate
                {
                    dgvEditCalcXXX.DataSource = fill;                    
                   
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
        #endregion
    }
}
