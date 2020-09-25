using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Collections;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.common.excel;
using Clicker;
using Vcpmc.Mis.Common.export;

namespace Vcpmc.Mis.Clicker
{    
    public partial class MainForm : Form
    {
        #region win32
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);
        #endregion

        #region Variable
        List<CMSViewModel> data = new List<CMSViewModel>();
        OperationType Operation = OperationType.LoadExcel;
        string path = "";
        string currentDirectory = "";
        string pathConfig = Path.GetDirectoryName(Application.ExecutablePath) + @"\config.xml";
        int delay = 2000;
        #endregion

        #region Fields
        /// <summary>
        /// Chuot di chuyen
        /// </summary>
        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        /// <summary>
        /// Chuot trai nhan
        /// </summary>
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        /// <summary>
        /// Chuot trai nha
        /// </summary>
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        /// <summary>
        /// Chuot phai nhan
        /// </summary>
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        /// <summary>
        /// Chuot phai nha
        /// </summary>
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        /// <summary>
        /// con lan nhan
        /// </summary>
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        /// <summary>
        /// con lan nha
        /// </summary>
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        private const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
        private const int MOUSEEVENTF_XUP = 0x0100; /* x button down */
        private const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
        private const int MOUSEEVENTF_VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */

        private SynchronizationContext context = null;
        private DateTime start, end;
        private bool first = true;
        /// <summary>
        /// Danh sach thao tac
        /// </summary>
        private List<ActionEntry> actions;
        private Thread runActionThread;
        //dangd nhap text, bo qua
        private bool byTextEntry = false;
        /// <summary>
        /// Dang chay bo qua
        /// </summary>
        private bool byStart = false;
        private Hashtable schedualeList;
        #endregion

        #region Construction
        public MainForm()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
            actions = new List<ActionEntry>();
            schedualeList = new Hashtable();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            //int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Height = screenHeight;
            this.Location = new Point(0, 0);
            bool runIt = false;
            OpenFileXml(runIt, pathConfig);
        }
        /// <summary>
        /// Huy luong click tu dong khi dong form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (runActionThread != null && runActionThread.IsAlive)
            {
                runActionThread.Abort();
            }
        }
        /// <summary>
        /// Lay toa do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (byStart) return;
            if (byTextEntry) return;

            if (e.KeyChar.Equals('c') || e.KeyChar.Equals('d')
                || e.KeyChar.Equals('r') || e.KeyChar.Equals('t'))
            {
                end = DateTime.Now;
                if (first)
                {
                    start = end;
                    first = false;
                }

                ClickType ct = ClickType.click;
                //nhap chuot
                if (e.KeyChar.Equals('c'))
                {
                    //cl = ClickType.click;
                }
                //nhap dup chuot trai
                else if (e.KeyChar.Equals('d'))
                {
                    ct = ClickType.doubleClick;
                }
                //nhap chuot phai
                else if (e.KeyChar.Equals('r'))
                {
                    ct = ClickType.rightClick;
                }
                //nhap lieu
                else //if (e.KeyChar.Equals('t'))
                {
                    ct = ClickType.SendKeys;
                }
                //toa do
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;
                //thoi gain
                TimeSpan ts = end - start;
                double sec = 0;
                if (nWait.Value.Equals(0))
                {
                    sec = ts.TotalSeconds;
                    sec = Math.Round(sec, 1);
                }
                else
                {
                    sec = (double)nWait.Value;
                }
                start = end;
                //
                string point = x.ToString() + "," + y.ToString();
                string text = string.Empty;// ct.Equals(ClickType.SendKeys) ? txbEntry.Text : string.Empty;
                ListViewItem lvi = new ListViewItem(new string[] { point, ct.ToString(), "0", text });
                //
                ActionEntry acion = new ActionEntry(x, y, text, 0, ct);
                lvi.Tag = acion;
                lvActions.Items.Add(lvi);
                int index = lvActions.Items.Count;
                //if (index > 1)
                //{
                //    lvActions.Items[index - 2].SubItems[2].Text = sec.ToString();
                //    (lvActions.Items[index - 2].Tag as ActionEntry).Interval = (int)sec;
                //}
                lvActions.Items[index - 1].SubItems[2].Text = sec.ToString();
                (lvActions.Items[index - 1].Tag as ActionEntry).Interval = (int)sec;
            }
            //bat dau
            if (e.KeyChar.Equals('S'))
            {
                btnStart.PerformClick();
            }
            //Thoat
            else if (e.KeyChar.Equals((char)Keys.Escape))//Esc
            {
                btnCancel.PerformClick();
                this.Focus();
            }
        }
        #endregion

        #region Load data
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                path = "";
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    //openFileDialog.InitialDirectory = "D:\\";                   
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;
                        path = filePath;
                    }
                }
                if (path == string.Empty)
                {
                    MessageBox.Show("input file path!");
                    return;
                }               
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
        private void LoadData()
        {
            try
            {
                DateTime startTime = DateTime.Now;                
                data.Clear();
                ExcelHelper excelHelper = new ExcelHelper();
                data = excelHelper.ReadExcelImportCMClaim(path);
                DateTime endtime = DateTime.Now;
                if (data != null)
                {
                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = $"Load data is successfull, time {(endtime- startTime).TotalSeconds} (s), total: {data.Count}";
                    }));
                    dgData.Invoke(new MethodInvoker(delegate
                    {
                        dgData.DataSource = data;
                    }));
                    if (data == null)
                    {
                        data = new List<CMSViewModel>();
                    }
                }
                else
                {
                    mainStatus.Invoke(new MethodInvoker(delegate
                    {
                        lbInfo.Text = "Load data from excel file be error!";
                    }));
                    dgData.Invoke(new MethodInvoker(delegate
                    {
                        dgData.DataSource = new List<CMSViewModel>();
                    }));
                }
               
            }
            catch (Exception ex)
            {

               MessageBox.Show($"Load data is error! {Environment.NewLine}{ex.ToString()}");
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            currentDirectory = "";
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.InitialDirectory = @"C:\";
            saveDlg.Filter = "Excel files (*.xls)|*.xls";
            saveDlg.FilterIndex = 0;
            saveDlg.RestoreDirectory = true;
            saveDlg.Title = "Export Excel File To";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                currentDirectory = saveDlg.FileName;
            }
            if (currentDirectory == string.Empty)
            {
                MessageBox.Show("input file path!");
                return;
            }
            Operation = OperationType.SaveExcel;
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();
        }

        private void ExportData(List<CMSViewModel> data, string folderPath)
        {
            bool check = false;
            try
            {
                if (data == null || data.Count == 0 || currentDirectory == "")
                {
                    MessageBox.Show("data is empty!");
                }
                check = WriteReportHelper.ExportCMS(data, folderPath);

            }
            catch (Exception ex)
            {
                check = false;
                pcloader.Invoke(new MethodInvoker(delegate
                {
                    pcloader.Visible = false;
                }));
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnResetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                dgData.DataSource = new List<CMSViewModel>(); ;
                if (data == null)
                {
                    data = new List<CMSViewModel>();
                }
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.AutoNote = string.Empty;
                        item.AutoStatus = false;
                    }
                    dgData.DataSource = data;
                }                
            }
            catch (Exception)
            {

                throw;
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
                    LoadData();
                }
                else if (Operation == OperationType.SaveExcel)
                {
                    ExportData(data, currentDirectory);
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

        #region input data
        /// <summary>
        /// Nhap du lieu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((Char)Keys.Escape))//Esc
            {
                nWait.Focus();
            }
        }

        private void txbEntry_Enter(object sender, EventArgs e)
        {
            byTextEntry = true;
        }

        private void txbEntry_Leave(object sender, EventArgs e)
        {
            byTextEntry = false;
        }
        #endregion

        #region Config
        private void btnOpen_Click(object sender, EventArgs e)
        {
            bool runIt = false;
            ////if (MessageBox.Show("After openning configuration, are you want to run it?", "Clicker", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            ////     == DialogResult.Yes)
            ////{
            ////    runIt = true;
            ////}
            //OpenFileDialog file = new OpenFileDialog();
            //file.Filter = "XML File |*.xml";
            //file.Multiselect = false;
            //if (file.ShowDialog() == DialogResult.OK)
            //{
            //    OpenFileXml(runIt, file.FileName);
            //    string name = file.SafeFileName;
            //    this.Text = "Clicer - " + name.Substring(0, name.Length - 4);
            //}
            OpenFileXml(runIt, pathConfig);
        }
        /// <summary>
        /// Mở file config
        /// </summary>
        /// <param name="runIt">Mở và chạy luôn</param>
        /// <param name="file">file cấu hình</param>
        private void OpenFileXml(bool runIt, string file)
        {
            if(!File.Exists(file))
            {
                MessageBox.Show("Please create config file!");
                return;
            }
            //Get data from XML file
            XmlSerializer ser = new XmlSerializer(typeof(ActionsEntry));
            using (FileStream fs = System.IO.File.Open(file, FileMode.Open))
            {
                try
                {
                    ActionsEntry entry = (ActionsEntry)ser.Deserialize(fs);
                    if(entry!=null && entry.Action!=null)
                    {
                        lvActions.Items.Clear();
                        foreach (ActionsEntryAction ae in entry.Action)
                        {
                            string point = ae.X.ToString() + "," + ae.Y.ToString();
                            string interval = (ae.interval).ToString();
                            ListViewItem lvi = new ListViewItem(new string[] { point, ((ClickType)(ae.Type)).ToString(), interval, ae.Text });
                            ActionEntry acion = new ActionEntry(ae.X, ae.Y, ae.Text, ae.interval, (ClickType)(ae.Type));
                            lvi.Tag = acion;
                            lvActions.Items.Add(lvi);
                        }
                        if (runIt)
                        {
                            btnStart.PerformClick();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please create again config file");
                    }  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Clicer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig(pathConfig);
        }

        private void SaveConfig(string pathConfig)
        {
            //SaveFileDialog file = new SaveFileDialog();
            //file.Filter = "XML File |*.xml";
            //if (file.ShowDialog() == DialogResult.OK)
            //{
            //    XmlSerializer ser = new XmlSerializer(typeof(ActionsEntry));
            //    ActionsEntry tmpAction = new ActionsEntry();
            //    List<ActionsEntryAction> tmpActionsEntryActions = new List<ActionsEntryAction>();
            //    foreach (ListViewItem lvi in lvActions.Items)
            //    {
            //        ActionEntry tmpActionEntry = lvi.Tag as ActionEntry;
            //        ActionsEntryAction tmpActionsEntryAction = new ActionsEntryAction();
            //        tmpActionsEntryAction.X = tmpActionEntry.X;
            //        tmpActionsEntryAction.Y = tmpActionEntry.Y;
            //        tmpActionsEntryAction.Text = tmpActionEntry.Text;
            //        tmpActionsEntryAction.interval = tmpActionEntry.Interval;
            //        tmpActionsEntryAction.Type = (int)tmpActionEntry.Type;
            //        tmpActionsEntryActions.Add(tmpActionsEntryAction);
            //    }
            //    tmpAction.Action = tmpActionsEntryActions.ToArray();

            //    using (XmlWriter writer = XmlWriter.Create(file.FileName))
            //    {
            //        ser.Serialize(writer, tmpAction);
            //    }
            //}
            XmlSerializer ser = new XmlSerializer(typeof(ActionsEntry));
            ActionsEntry tmpAction = new ActionsEntry();
            List<ActionsEntryAction> tmpActionsEntryActions = new List<ActionsEntryAction>();
            foreach (ListViewItem lvi in lvActions.Items)
            {
                ActionEntry tmpActionEntry = lvi.Tag as ActionEntry;
                ActionsEntryAction tmpActionsEntryAction = new ActionsEntryAction();
                tmpActionsEntryAction.X = tmpActionEntry.X;
                tmpActionsEntryAction.Y = tmpActionEntry.Y;
                tmpActionsEntryAction.Text = tmpActionEntry.Text;
                tmpActionsEntryAction.interval = tmpActionEntry.Interval;
                tmpActionsEntryAction.Type = (int)tmpActionEntry.Type;
                tmpActionsEntryActions.Add(tmpActionsEntryAction);
            }
            tmpAction.Action = tmpActionsEntryActions.ToArray();

            using (XmlWriter writer = XmlWriter.Create(pathConfig))
            {
                ser.Serialize(writer, tmpAction);
            }
        }
        #endregion

        #region list view
        /// <summary>
        /// Xoa sanh sach Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lvActions.Items.Clear();
            first = true;
        }
        private void lvActions_MouseDown(object sender, MouseEventArgs e)
        {
            int coutselect = lvActions.SelectedItems.Count;
            deleteToolStripMenuItem.Available = coutselect > 0;
            editToolStripMenuItem.Available = coutselect == 1;
        }
        private void lvActions_DoubleClick(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Available)
            {
                editToolStripMenuItem.PerformClick();
            }
        }
        #endregion

        #region CMD
        /// <summary>
        /// Run
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            delay = (int)numDelay.Value * 1000;
            if (data == null)
            {
                data = new List<CMSViewModel>();
            }
            if (data.Count ==0)
            {
                MessageBox.Show("Please import data that need matching!");
                return;
            }
            if(lvActions.Items.Count==0)
            {
                MessageBox.Show("Please imput action!");
                return;
            }
            rbMessage.Clear();
            enableButtons(false);

            if (runActionThread == null || !runActionThread.IsAlive)
            {
                //Xoa danh sach lenh
                actions.Clear();
                //lay danh sach lenhj
                foreach (ListViewItem lvi in lvActions.Items)
                {
                    actions.Add(lvi.Tag as ActionEntry);
                }
                //kich hoat luong chay click
                runActionThread = new Thread(RunAction);
                runActionThread.Start();
            }

        }

        /// <summary>
        /// Huy auto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {           
            if (runActionThread != null && runActionThread.IsAlive)
            {
                runActionThread.Abort();
                enableButtons(true);
            }
        }
        #endregion

        #region Run
        /// <summary>
        /// Ham chay chinh
        /// </summary>
        private void RunAction()
        {
            if(data.Count==0)
            {
                return;
            }
            int count = 0;
            int total = data.Count;
            foreach (var item in data)
            {
                count++;
                //chay roi, khong chay nua
                if (item.AutoStatus)
                {
                    continue;
                }
                rbMessage.Invoke(new MethodInvoker(delegate
                {
                    if(rbMessage.Lines.Length > 200)
                    {
                        rbMessage.Clear();
                    }
                    rbMessage.Text += $"Start[{DateTime.Now.ToString("yyyy-mm-dd: HH:mm:ss")}] Issue ID: {item.IssueID} {Environment.NewLine}";
                    rbMessage.SelectionStart = rbMessage.TextLength;
                    rbMessage.Focus();
                }));    
                
                #region send lenh
                //duyet qua danh sach lenh
                foreach (ActionEntry action in actions)
                {
                    action.Text = item.LinkToIssue;
                    //nhap lieu
                    if (action.Type.Equals(ClickType.SendKeys))
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(WorkSendKeys), action);
                    }
                    //nhan chuot
                    else// if (entry is ClickEntry)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(WorkClick), action);
                    }

                    int tmpIntervl = action.Interval.Equals(0) ? 0 : action.Interval * 1000 - 100;
                    Thread.Sleep(tmpIntervl);
                }
                #endregion

                #region cap nhat
                item.AutoStatus = true;
                rbMessage.Invoke(new MethodInvoker(delegate
                {
                    rbMessage.Text += $"End[{DateTime.Now.ToString("yyyy-mm-dd: HH:mm:ss")}] Total: {count}/{total} link(s){Environment.NewLine}";
                    rbMessage.SelectionStart = rbMessage.TextLength;
                    rbMessage.Focus();
                }));
                dgData.Invoke(new MethodInvoker(delegate
                {                    
                    if (dgData.Rows[count - 1].Cells[0].Value.ToString() == item.IssueID)
                    {
                        dgData.Rows[count - 1].DefaultCellStyle.BackColor = Color.Green;
                    }
                }));
                #endregion
            }            
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorkEnableButtons), null);
            dgData.Invoke(new MethodInvoker(delegate
            {
                dgData.DataSource = data;
            }));
            rbMessage.Invoke(new MethodInvoker(delegate
            {                
                rbMessage.Text += $"Complete[{DateTime.Now.ToString("yyyy-mm-dd: HH:mm:ss")}] Successfull{Environment.NewLine}";
                rbMessage.SelectionStart = rbMessage.TextLength;
                rbMessage.Focus();
            }));
        }
        /// <summary>
        /// Send du lieu
        /// </summary>
        /// <param name="state"></param>
        private void WorkSendKeys(object state)
        {
            string link = "";
            this.context.Send(new SendOrPostCallback(delegate(object _state)
            {
                ActionEntry action = state as ActionEntry;
                //gui du lieu
                link = action.Text;
                SendKeys.Send(action.Text);
                Thread.Sleep(delay);
                SendKeys.Send("{ENTER}");
            }), state);
            try
            {
                rbMessage.Invoke(new MethodInvoker(delegate
                {
                    rbMessage.Text += $"processing-> Link: {link}{Environment.NewLine}";
                    rbMessage.SelectionStart = rbMessage.TextLength;
                    rbMessage.Focus();
                }));
            }
            catch (Exception)
            {              
            }            
        }
        /// <summary>
        /// Du lieu nhan chuot
        /// </summary>
        /// <param name="state"></param>
        private void WorkClick(object state)
        {
            this.context.Send(new SendOrPostCallback(delegate(object _state)
            {
                ActionEntry action = state as ActionEntry;
                //thiet lap toa do chuot
                SetCursorPos(action.X, action.Y);
                //dung 0.1s
                Thread.Sleep(100);
                if (action.Type.Equals(ClickType.click))
                {
                    ///nhan chuot trai
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    //nha chuot trai
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else if (action.Type.Equals(ClickType.doubleClick))
                {
                    //giong nhu nhan chuot lien tiep
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else //if (action.Type.Equals(ClickType.rightClick))
                {
                    //nguoc lai la nhan chuot trai
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }
            }), state);
        }        

        #endregion

        #region enable control
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void WorkEnableButtons(object state)
        {
            this.context.Send(new SendOrPostCallback(delegate (object _state)
            {
                enableButtons(true);
            }), state);
        }

        /// <summary>
        /// Kich hoa cac nut
        /// </summary>
        /// <param name="enabel"></param>
        private void enableButtons(bool enabel)
        {
            //nap data
            btnImport.Enabled = enabel;
            //xuat data
            btnExport.Enabled = enabel;
            //xoa du lieu
            btnClear.Enabled = enabel;
            //mo file
            btnOpen.Enabled = enabel;
            //luu file
            btnSave.Enabled = enabel;
            //danh sach lenh
            lvActions.Enabled = enabel;
            //
            btnStart.Enabled = enabel;
            btnCancel.Enabled = !enabel;
            byStart = !enabel;
        }
        #endregion

        #region temp
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionEntry action = lvActions.SelectedItems[0].Tag as ActionEntry;
            EditWin frm = new EditWin(action);
            frm.Actionentry = action;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                action = frm.Actionentry;
                lvActions.SelectedItems[0].Tag = action;
                lvActions.SelectedItems[0].Text = action.X + "," + action.Y;
                lvActions.SelectedItems[0].SubItems[1].Text = action.Type.ToString();
                lvActions.SelectedItems[0].SubItems[2].Text = action.Interval.ToString();
                lvActions.SelectedItems[0].SubItems[3].Text = action.Text;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx");

        }

        private void tgData_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cout = lvActions.Items.Count;
            int coutselect = lvActions.SelectedItems.Count;
            if (cout.Equals(coutselect))
            {
                btnClear.PerformClick();
            }
            else
            {
                for (int i = coutselect - 1; i >= 0; --i)
                {
                    int index = lvActions.SelectedItems[i].Index;
                    lvActions.Items[index].Remove();
                }
            }
        }
        #endregion
    }
}
