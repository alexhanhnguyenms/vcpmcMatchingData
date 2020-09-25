using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Media.Youtube
{
    /// <summary>
    /// Danh sách matching prelaim của youtube
    /// </summary>
    public class PreclaimMatchingListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<PreclaimMatchingRequest> Items { get; set; } = new List<PreclaimMatchingRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// Matching theo nam
        /// </summary>
        //public int Year { get; set; } = -1;
        /// <summary>
        /// Mathcing theo thang
        /// </summary>
        //public int MONTH { get; set; } = -1;
    }
}
