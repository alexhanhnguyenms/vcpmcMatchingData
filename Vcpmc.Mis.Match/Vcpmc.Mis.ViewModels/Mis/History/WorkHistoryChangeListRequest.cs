using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.History
{
    public class WorkHistoryChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<WorkHistoryCreateRequest> Items { get; set; } = new List<WorkHistoryCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
