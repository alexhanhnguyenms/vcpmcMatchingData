using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Monopoly
{
    public class MonopolyChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<MonopolyCreateRequest> Items { get; set; } = new List<MonopolyCreateRequest>();
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
