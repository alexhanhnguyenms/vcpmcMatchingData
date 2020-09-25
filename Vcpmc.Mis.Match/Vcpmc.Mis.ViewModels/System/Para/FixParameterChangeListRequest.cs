using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.System.Para
{
    public class FixParameterChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<FixParameterCreateRequest> Items { get; set; } = new List<FixParameterCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// Nhom doc quyen tacpham hay tac gia: 0: tac pham, 1 tac gia
        /// </summary>
        public int Group { get; set; } = 0;
    }
}
