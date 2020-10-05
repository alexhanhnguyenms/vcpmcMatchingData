using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.History
{
    public class WorkHistoryMatchingListRequest
    {
        public List<WorkHistoryMatchingRequest> Items { get; set; } = new List<WorkHistoryMatchingRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
