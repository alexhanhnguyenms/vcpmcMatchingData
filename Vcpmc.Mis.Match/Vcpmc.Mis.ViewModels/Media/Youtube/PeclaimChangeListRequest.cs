using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Media.Youtube
{
    /// <summary>
    /// Danh sach update preclaim
    /// </summary>
    public class PeclaimChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<PreclaimCreateRequest> Items { get; set; } = new List<PreclaimCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
