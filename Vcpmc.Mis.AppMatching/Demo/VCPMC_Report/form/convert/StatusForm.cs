using System;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.convert
{
    public partial class StatusForm : Form
    {
        public System.Windows.Forms.TextBox TextBox
        {
            get { return textBox; }
            set { textBox = value; }
        }
        public StatusForm(string title)
        {
            base.Text = title;
            InitializeComponent();
        }

        private void StatusForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            this.textBox.Clear();
            e.Cancel = true;
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {

        }
    }
}
