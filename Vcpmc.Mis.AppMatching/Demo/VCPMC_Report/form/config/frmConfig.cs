using System;
using System.Windows.Forms;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.master;
using Vcpmc.Mis.Common.xml;
using Vcpmc.Mis.Infrastructure;

namespace Vcpmc.Mis.AppMatching.form.config
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
            LoadData();
        }

        private void tsLoadConfig_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                MasterList.LoadConfig(Core.User);
                AddRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load data is error, {ex.ToString()}");
            }
        }

        private void tsSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                tbMain.Focus();
                var path1 = NameReport.File1.ToString();
                var path2 = NameReport.File2.ToString();
                var path3 = NameReport.File3.ToString();
                var path3_non_vi = NameReport.File3_non_vi.ToString();
                var path3_vi = NameReport.File3_vi.ToString();
                var path4_non_vi = NameReport.File4_non_vi.ToString();
                var path4_vi = NameReport.File4_vi.ToString();
                var path5_vi = NameReport.File5_vi.ToString();
                var path5_non_vi = NameReport.File5_non_vi.ToString();
                var pathRepalce = NameReport.File_repalce.ToString();

                #region Delete File
                XmlHelper.DeleteFile(path1, true);
                XmlHelper.DeleteFile(path2, true);
                XmlHelper.DeleteFile(path3, true);
                XmlHelper.DeleteFile(path3_non_vi, true);
                XmlHelper.DeleteFile(path3_vi, true);
                XmlHelper.DeleteFile(path4_non_vi, true);
                XmlHelper.DeleteFile(path5_non_vi, true);
                XmlHelper.DeleteFile(path4_vi, true);
                XmlHelper.DeleteFile(path5_vi, true);
                XmlHelper.DeleteFile(pathRepalce, true);
                #endregion
                //file1               
                MasterList.ConfigFile1.FileName = path1;
                MasterList.ConfigFile1.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile1.User = Core.User;
                for (int i = 0; i < dgvFile1.Rows.Count; i++)
                {
                    if (dgvFile1.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile1.configItems[i].ColumnName = dgvFile1.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile1.configItems[i].Value = dgvFile1.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile1.configItems[i].Order = int.Parse(dgvFile1.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile1.configItems[i].IsCheck = (bool)dgvFile1.Rows[i].Cells[6].Value;
                    }
                    
                }
                XmlHelper.WriteXML(MasterList.ConfigFile1,true);
                //file2
                MasterList.ConfigFile2.FileName = path2;
                MasterList.ConfigFile2.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile2.User = Core.User;
                for (int i = 0; i < dgvFile2.Rows.Count; i++)
                {
                    if(dgvFile2.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile2.configItems[i].ColumnName = dgvFile2.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile2.configItems[i].Value = dgvFile2.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile2.configItems[i].Order = int.Parse(dgvFile2.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile2.configItems[i].IsCheck = (bool)dgvFile2.Rows[i].Cells[6].Value;
                    }  
                }
                XmlHelper.WriteXML(MasterList.ConfigFile2, true);
                //file3
                MasterList.ConfigFile3.FileName = path3;
                MasterList.ConfigFile3.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile3.User = Core.User;
                for (int i = 0; i < dgvFile3.Rows.Count; i++)
                {
                    if (dgvFile3.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile3.configItems[i].ColumnName = dgvFile3.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile3.configItems[i].Value = dgvFile3.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile3.configItems[i].Order = int.Parse(dgvFile3.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile3.configItems[i].IsCheck = (bool)dgvFile3.Rows[i].Cells[6].Value;
                    }                    
                }
                XmlHelper.WriteXML(MasterList.ConfigFile3, true);
                //file 3-non-vi
                MasterList.ConfigFile3_non_vi.FileName = path3_non_vi;
                MasterList.ConfigFile3_non_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile3_non_vi.User = Core.User;
                for (int i = 0; i < dgvFile3_non_vi.Rows.Count; i++)
                {
                    if (dgvFile3_non_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile3_non_vi.configItems[i].ColumnName = dgvFile3_non_vi.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile3_non_vi.configItems[i].Value = dgvFile3_non_vi.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile3_non_vi.configItems[i].Order = int.Parse(dgvFile3_non_vi.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile3_non_vi.configItems[i].IsCheck = (bool)dgvFile3_non_vi.Rows[i].Cells[6].Value;
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile3_non_vi, true);
                //file3-vi
                MasterList.ConfigFile3_vi.FileName = path3_vi;
                MasterList.ConfigFile3_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile3_vi.User = Core.User;
                int indexconfigItems = 0;
                int indexviDetectItems = 0;
                for (int i = 0; i < dgvFile3_vi.Rows.Count; i++)
                {
                    if (dgvFile3_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        if (dgvFile3_vi.Rows[i].Cells[4].Value == null || dgvFile3_vi.Rows[i].Cells[4].Value.ToString().Trim() == "")
                        {
                            MasterList.ConfigFile3_vi.configItems[indexconfigItems].ColumnName = dgvFile3_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile3_vi.configItems[indexconfigItems].Value = dgvFile3_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile3_vi.configItems[indexconfigItems].Order = int.Parse(dgvFile3_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile3_vi.configItems[indexconfigItems].IsCheck = (bool)dgvFile3_vi.Rows[i].Cells[7].Value;
                            indexconfigItems++;
                        }
                        else
                        {
                            MasterList.ConfigFile3_vi.viDetectItems[indexviDetectItems].ColumnName = dgvFile3_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile3_vi.viDetectItems[indexviDetectItems].Value = dgvFile3_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile3_vi.viDetectItems[indexviDetectItems].Order = int.Parse(dgvFile3_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile3_vi.viDetectItems[indexviDetectItems].IsCheck = (bool)dgvFile3_vi.Rows[i].Cells[7].Value;
                            indexviDetectItems++;
                        }
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile3_vi, true);
                //file4-non-vi
                MasterList.ConfigFile4_non_vi.FileName = path4_non_vi;
                MasterList.ConfigFile4_non_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile4_non_vi.User = Core.User;
                for (int i = 0; i < dgvFile4_non_vi.Rows.Count; i++)
                {
                    if (dgvFile4_non_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile4_non_vi.configItems[i].ColumnName = dgvFile4_non_vi.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile4_non_vi.configItems[i].Value = dgvFile4_non_vi.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile4_non_vi.configItems[i].Order = int.Parse(dgvFile4_non_vi.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile4_non_vi.configItems[i].IsCheck = (bool)dgvFile4_non_vi.Rows[i].Cells[6].Value;
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile4_non_vi, true);
                //file5-non-vi
                MasterList.ConfigFile5_non_vi.FileName = path5_non_vi;
                MasterList.ConfigFile5_non_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile5_non_vi.User = Core.User;
                for (int i = 0; i < dgvFile5_non_vi.Rows.Count; i++)
                {
                    if (dgvFile5_non_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        MasterList.ConfigFile5_non_vi.configItems[i].ColumnName = dgvFile5_non_vi.Rows[i].Cells[3].Value.ToString();
                        MasterList.ConfigFile5_non_vi.configItems[i].Value = dgvFile5_non_vi.Rows[i].Cells[4].Value.ToString();
                        MasterList.ConfigFile5_non_vi.configItems[i].Order = int.Parse(dgvFile5_non_vi.Rows[i].Cells[5].Value.ToString());
                        MasterList.ConfigFile5_non_vi.configItems[i].IsCheck = (bool)dgvFile5_non_vi.Rows[i].Cells[6].Value;
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile5_non_vi, true);
                //file4-vi
                MasterList.ConfigFile4_vi.FileName = path4_vi;
                MasterList.ConfigFile4_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile4_vi.User = Core.User;
                indexconfigItems = 0;
                indexviDetectItems = 0;
                for (int i = 0; i < dgvFile4_vi.Rows.Count; i++)
                {
                    if (dgvFile4_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {                        
                        if (dgvFile4_vi.Rows[i].Cells[4].Value == null || dgvFile4_vi.Rows[i].Cells[4].Value.ToString().Trim() == "")
                        {
                            MasterList.ConfigFile4_vi.configItems[indexconfigItems].ColumnName = dgvFile4_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile4_vi.configItems[indexconfigItems].Value = dgvFile4_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile4_vi.configItems[indexconfigItems].Order = int.Parse(dgvFile4_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile4_vi.configItems[indexconfigItems].IsCheck = (bool)dgvFile4_vi.Rows[i].Cells[7].Value;
                            indexconfigItems++;
                        }   
                        else
                        {
                            MasterList.ConfigFile4_vi.viDetectItems[indexviDetectItems].ColumnName = dgvFile4_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile4_vi.viDetectItems[indexviDetectItems].Value = dgvFile4_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile4_vi.viDetectItems[indexviDetectItems].Order = int.Parse(dgvFile4_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile4_vi.viDetectItems[indexviDetectItems].IsCheck = (bool)dgvFile4_vi.Rows[i].Cells[7].Value;
                            indexviDetectItems++;
                        } 
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile4_vi, true);
                //file5-vi
                MasterList.ConfigFile5_vi.FileName = path4_vi;
                MasterList.ConfigFile5_vi.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFile5_vi.User = Core.User;
                indexconfigItems = 0;
                indexviDetectItems = 0;
                for (int i = 0; i < dgvFile5_vi.Rows.Count; i++)
                {
                    if (dgvFile5_vi.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        if (dgvFile5_vi.Rows[i].Cells[4].Value == null || dgvFile5_vi.Rows[i].Cells[4].Value.ToString().Trim() == "")
                        {
                            MasterList.ConfigFile5_vi.configItems[indexconfigItems].ColumnName = dgvFile5_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile5_vi.configItems[indexconfigItems].Value = dgvFile5_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile5_vi.configItems[indexconfigItems].Order = int.Parse(dgvFile5_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile5_vi.configItems[indexconfigItems].IsCheck = (bool)dgvFile5_vi.Rows[i].Cells[7].Value;
                            indexconfigItems++;
                        }
                        else
                        {
                            MasterList.ConfigFile5_vi.viDetectItems[indexviDetectItems].ColumnName = dgvFile5_vi.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFile5_vi.viDetectItems[indexviDetectItems].Value = dgvFile5_vi.Rows[i].Cells[5].Value.ToString();
                            MasterList.ConfigFile5_vi.viDetectItems[indexviDetectItems].Order = int.Parse(dgvFile5_vi.Rows[i].Cells[6].Value.ToString());
                            MasterList.ConfigFile5_vi.viDetectItems[indexviDetectItems].IsCheck = (bool)dgvFile5_vi.Rows[i].Cells[7].Value;
                            indexviDetectItems++;
                        }
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFile5_vi, true);
                //replace
                MasterList.ConfigFileReplace.FileName = pathRepalce;
                MasterList.ConfigFileReplace.LastTimeEdit = DateTime.Now;
                MasterList.ConfigFileReplace.User = Core.User;
                indexconfigItems = 0;
                indexviDetectItems = 0;
                for (int i = 0; i < dgvFileReplace.Rows.Count; i++)
                {
                    if (dgvFileReplace.Rows[i].Cells[0].ToString().Trim() != string.Empty)
                    {
                        if (dgvFileReplace.Rows[i].Cells[4].Value == null || dgvFileReplace.Rows[i].Cells[4].Value.ToString().Trim() == "")
                        {
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].OldS = dgvFileReplace.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].NewS = dgvFileReplace.Rows[i].Cells[4].Value.ToString();
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].IsCheck = (bool)dgvFileReplace.Rows[i].Cells[5].Value;
                            indexconfigItems++;
                        }
                        else
                        {
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].OldS = dgvFileReplace.Rows[i].Cells[3].Value.ToString();
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].NewS = dgvFileReplace.Rows[i].Cells[4].Value.ToString();
                            MasterList.ConfigFileReplace.configItems[indexconfigItems].IsCheck = (bool)dgvFileReplace.Rows[i].Cells[5].Value;
                            indexviDetectItems++;
                        }
                    }
                }
                XmlHelper.WriteXML(MasterList.ConfigFileReplace, true);

                MessageBox.Show($"The config file saved Successfull!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save config file is error, {ex.ToString()}");
            }
        }

        private void AddRow()
        {
            dgvFile1.Rows.Clear();                                                                  
            dgvFile2.Rows.Clear();           
            dgvFile3.Rows.Clear();           
            dgvFile3_non_vi.Rows.Clear();           
            dgvFile3_vi.Rows.Clear();           
            dgvFile4_non_vi.Rows.Clear();           
            dgvFile5_non_vi.Rows.Clear();           
            dgvFile4_vi.Rows.Clear();           
            dgvFile5_vi.Rows.Clear();           
            dgvFileReplace.Rows.Clear();           
            //file1
            for (int i = 0; i < MasterList.ConfigFile1.configItems.Count; i++)
            {
                int indexRow = dgvFile1.Rows.Add();

                dgvFile1.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile1.User;
                dgvFile1.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile1.LastTimeEdit;
                dgvFile1.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile1.FileName;
                
                dgvFile1.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile1.configItems[i].ColumnName;
                dgvFile1.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile1.configItems[i].Value;
                dgvFile1.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile1.configItems[i].Order;
                dgvFile1.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile1.configItems[i].IsCheck;                
            }
            //file2
            for (int i = 0; i < MasterList.ConfigFile2.configItems.Count; i++)
            {
                int indexRow = dgvFile2.Rows.Add();

                dgvFile2.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile2.User;
                dgvFile2.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile2.LastTimeEdit;
                dgvFile2.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile2.FileName;

                dgvFile2.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile2.configItems[i].ColumnName;
                dgvFile2.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile2.configItems[i].Value;
                dgvFile2.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile2.configItems[i].Order;
                dgvFile2.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile2.configItems[i].IsCheck;
            }
            //file3
            for (int i = 0; i < MasterList.ConfigFile3.configItems.Count; i++)
            {
                int indexRow = dgvFile3.Rows.Add();

                dgvFile3.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile3.User;
                dgvFile3.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile3.LastTimeEdit;
                dgvFile3.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile3.FileName;

                dgvFile3.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile3.configItems[i].ColumnName;
                dgvFile3.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile3.configItems[i].Value;
                dgvFile3.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile3.configItems[i].Order;
                dgvFile3.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile3.configItems[i].IsCheck;
            }
            //file3-non-vi
            for (int i = 0; i < MasterList.ConfigFile3_non_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile3_non_vi.Rows.Add();

                dgvFile3_non_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile3_non_vi.User;
                dgvFile3_non_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile3_non_vi.LastTimeEdit;
                dgvFile3_non_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile3_non_vi.FileName;

                dgvFile3_non_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile3_non_vi.configItems[i].ColumnName;
                dgvFile3_non_vi.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile3_non_vi.configItems[i].Value;
                dgvFile3_non_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile3_non_vi.configItems[i].Order;
                dgvFile3_non_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile3_non_vi.configItems[i].IsCheck;
            }
            //file3-vi
            for (int i = 0; i < MasterList.ConfigFile3_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile3_vi.Rows.Add();

                dgvFile3_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile3_vi.User;
                dgvFile3_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile3_vi.LastTimeEdit;
                dgvFile3_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile3_vi.FileName;

                dgvFile3_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile3_vi.configItems[i].ColumnName;
                dgvFile3_vi.Rows[indexRow].Cells[4].Value = "";
                dgvFile3_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile3_vi.configItems[i].Value;
                dgvFile3_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile3_vi.configItems[i].Order;
                dgvFile3_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile3_vi.configItems[i].IsCheck;
            }
            for (int i = 0; i < MasterList.ConfigFile3_vi.viDetectItems.Count; i++)
            {
                int indexRow = dgvFile3_vi.Rows.Add();

                dgvFile3_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile3_vi.User;
                dgvFile3_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile3_vi.LastTimeEdit;
                dgvFile3_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile3_vi.FileName;

                dgvFile3_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile3_vi.viDetectItems[i].ColumnName;
                dgvFile3_vi.Rows[indexRow].Cells[4].Value = "Filter vietnamese";
                dgvFile3_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile3_vi.viDetectItems[i].Value;
                dgvFile3_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile3_vi.viDetectItems[i].Order;
                dgvFile3_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile3_vi.viDetectItems[i].IsCheck;
            }
            //file4-non-vi
            for (int i = 0; i < MasterList.ConfigFile4_non_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile4_non_vi.Rows.Add();

                dgvFile4_non_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile4_non_vi.User;
                dgvFile4_non_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile4_non_vi.LastTimeEdit;
                dgvFile4_non_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile4_non_vi.FileName;

                dgvFile4_non_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile4_non_vi.configItems[i].ColumnName;
                dgvFile4_non_vi.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile4_non_vi.configItems[i].Value;
                dgvFile4_non_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile4_non_vi.configItems[i].Order;
                dgvFile4_non_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile4_non_vi.configItems[i].IsCheck;
            }
            //file5-non-vi
            for (int i = 0; i < MasterList.ConfigFile5_non_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile5_non_vi.Rows.Add();

                dgvFile5_non_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile5_non_vi.User;
                dgvFile5_non_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile5_non_vi.LastTimeEdit;
                dgvFile5_non_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile5_non_vi.FileName;

                dgvFile5_non_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile5_non_vi.configItems[i].ColumnName;
                dgvFile5_non_vi.Rows[indexRow].Cells[4].Value = MasterList.ConfigFile5_non_vi.configItems[i].Value;
                dgvFile5_non_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile5_non_vi.configItems[i].Order;
                dgvFile5_non_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile5_non_vi.configItems[i].IsCheck;
            }

            //file4-vi
            for (int i = 0; i < MasterList.ConfigFile4_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile4_vi.Rows.Add();

                dgvFile4_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile4_vi.User;
                dgvFile4_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile4_vi.LastTimeEdit;
                dgvFile4_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile4_vi.FileName;
                                                                                  
                dgvFile4_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile4_vi.configItems[i].ColumnName;
                dgvFile4_vi.Rows[indexRow].Cells[4].Value = "";
                dgvFile4_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile4_vi.configItems[i].Value;
                dgvFile4_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile4_vi.configItems[i].Order;
                dgvFile4_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile4_vi.configItems[i].IsCheck;
            }

            for (int i = 0; i < MasterList.ConfigFile4_vi.viDetectItems.Count; i++)
            {
                int indexRow = dgvFile4_vi.Rows.Add();

                dgvFile4_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile4_vi.User;
                dgvFile4_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile4_vi.LastTimeEdit;
                dgvFile4_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile4_vi.FileName;

                dgvFile4_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile4_vi.viDetectItems[i].ColumnName;
                dgvFile4_vi.Rows[indexRow].Cells[4].Value = "Filter vietnamese";
                dgvFile4_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile4_vi.viDetectItems[i].Value;
                dgvFile4_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile4_vi.viDetectItems[i].Order;
                dgvFile4_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile4_vi.viDetectItems[i].IsCheck;
            }
            //file5-vi
            for (int i = 0; i < MasterList.ConfigFile5_vi.configItems.Count; i++)
            {
                int indexRow = dgvFile5_vi.Rows.Add();

                dgvFile5_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile5_vi.User;
                dgvFile5_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile5_vi.LastTimeEdit;
                dgvFile5_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile5_vi.FileName;

                dgvFile5_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile5_vi.configItems[i].ColumnName;
                dgvFile5_vi.Rows[indexRow].Cells[4].Value = "";
                dgvFile5_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile5_vi.configItems[i].Value;
                dgvFile5_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile5_vi.configItems[i].Order;
                dgvFile5_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile5_vi.configItems[i].IsCheck;
            }

            for (int i = 0; i < MasterList.ConfigFile5_vi.viDetectItems.Count; i++)
            {
                int indexRow = dgvFile5_vi.Rows.Add();

                dgvFile5_vi.Rows[indexRow].Cells[0].Value = MasterList.ConfigFile5_vi.User;
                dgvFile5_vi.Rows[indexRow].Cells[1].Value = MasterList.ConfigFile5_vi.LastTimeEdit;
                dgvFile5_vi.Rows[indexRow].Cells[2].Value = MasterList.ConfigFile5_vi.FileName;

                dgvFile5_vi.Rows[indexRow].Cells[3].Value = MasterList.ConfigFile5_vi.viDetectItems[i].ColumnName;
                dgvFile5_vi.Rows[indexRow].Cells[4].Value = "Filter vietnamese";
                dgvFile5_vi.Rows[indexRow].Cells[5].Value = MasterList.ConfigFile5_vi.viDetectItems[i].Value;
                dgvFile5_vi.Rows[indexRow].Cells[6].Value = MasterList.ConfigFile5_vi.viDetectItems[i].Order;
                dgvFile5_vi.Rows[indexRow].Cells[7].Value = MasterList.ConfigFile5_vi.viDetectItems[i].IsCheck;
            }
            //repalce
            for (int i = 0; i < MasterList.ConfigFileReplace.configItems.Count; i++)
            {
                int indexRow = dgvFileReplace.Rows.Add();

                dgvFileReplace.Rows[indexRow].Cells[0].Value = MasterList.ConfigFileReplace.User;
                dgvFileReplace.Rows[indexRow].Cells[1].Value = MasterList.ConfigFileReplace.LastTimeEdit;
                dgvFileReplace.Rows[indexRow].Cells[2].Value = MasterList.ConfigFileReplace.FileName;

                dgvFileReplace.Rows[indexRow].Cells[3].Value = MasterList.ConfigFileReplace.configItems[i].OldS;                
                dgvFileReplace.Rows[indexRow].Cells[4].Value = MasterList.ConfigFileReplace.configItems[i].NewS;               
                dgvFileReplace.Rows[indexRow].Cells[5].Value = MasterList.ConfigFileReplace.configItems[i].IsCheck;
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {

        }

        private void tsBtnRenew_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to rdelete and create new config files?", "Confirm renew config files",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    var path1 = NameReport.File1.ToString();
                    var path2 = NameReport.File2.ToString();
                    var path3 = NameReport.File3.ToString();
                    var path3_non_vi = NameReport.File3_non_vi.ToString();
                    var path3_vi = NameReport.File3_vi.ToString();
                    var path4_non_vi = NameReport.File4_non_vi.ToString();
                    var path4_vi = NameReport.File4_vi.ToString();
                    var path5_vi = NameReport.File5_vi.ToString();
                    var path5_non_vi = NameReport.File5_non_vi.ToString();
                    var pathRepalce = NameReport.File_repalce.ToString();
                    #region Delete File
                    XmlHelper.DeleteFile(path1, true);
                    XmlHelper.DeleteFile(path2, true);
                    XmlHelper.DeleteFile(path3, true);
                    XmlHelper.DeleteFile(path3_non_vi, true);
                    XmlHelper.DeleteFile(path3_vi, true);
                    XmlHelper.DeleteFile(path4_non_vi, true);
                    XmlHelper.DeleteFile(path5_non_vi, true);
                    XmlHelper.DeleteFile(path4_vi, true);
                    XmlHelper.DeleteFile(path5_vi, true);
                    XmlHelper.DeleteFile(pathRepalce, true);
                    #endregion

                    //MasterList.Innit();
                    LoadData();
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Save data to database be error!");
            }
        }
    }
}
