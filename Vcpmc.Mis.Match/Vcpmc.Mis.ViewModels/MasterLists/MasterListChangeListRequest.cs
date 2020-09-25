using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.MasterLists
{
    public class MasterListChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<MasterListCreateRequest> Items { get; set; } = new List<MasterListCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
        public int Year { get; set; } = 2020;
        public int Month { get; set; } = 8;
    }
}
