using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.main
{
    public class MenuList
    {
        public ToolStripMenuItem Item { get; set; } = new ToolStripMenuItem();
        //public ToolStripMenuItem Parent { get; set; } = null;
        public int Level { get; set; } = 0;
        public bool IsShow { get; set; } = false;
        /// <summary>
        /// So phan tu con
        /// </summary>
        //public int CountSub { get; set; }

    }
}
