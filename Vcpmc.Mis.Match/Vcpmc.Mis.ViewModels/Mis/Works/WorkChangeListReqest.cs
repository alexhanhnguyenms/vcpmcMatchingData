using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<WorkCreateRequest> Items { get; set; } = new List<WorkCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
