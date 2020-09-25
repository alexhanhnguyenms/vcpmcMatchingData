using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    //[Flags]
    /// <summary>
    /// Loai nhan
    /// </summary>
    public enum ClickType
    {
        /// <summary>
        /// nhan trai
        /// </summary>
        click = 0,
        /// <summary>
        /// Nhan phai
        /// </summary>
        rightClick = 1,
        //.Nhan trai 2 nhan
        doubleClick = 2,
        /// <summary>
        /// Gui key
        /// </summary>
        SendKeys = 3
    }
}
