using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkByStringListRequest
    {
        /// <summary>
        /// List string need compare 
        /// </summary>
        public List<string> Items { get; set; } = new List<string>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
