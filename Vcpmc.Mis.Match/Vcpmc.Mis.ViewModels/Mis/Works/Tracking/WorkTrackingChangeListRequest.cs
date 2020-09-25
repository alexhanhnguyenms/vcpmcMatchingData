using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking
{
    public class WorkTrackingChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<WorkTrackingCreateRequest> Items { get; set; } = new List<WorkTrackingCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 1;
        public int Year { get; set; } = 2020;
        public int Month { get; set; } = 8;
        /// <summary>
        /// 0: create, 1: update
        /// </summary>
        public int Type { get; set; } = 0;
    }
}
