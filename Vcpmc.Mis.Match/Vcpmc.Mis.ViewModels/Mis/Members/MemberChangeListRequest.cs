using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Members
{
    public class MemberChangeListRequest
    {
        /// <summary>
        /// Danh sach can update
        /// </summary>
        public List<MemberCreateRequest> Items { get; set; } = new List<MemberCreateRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
