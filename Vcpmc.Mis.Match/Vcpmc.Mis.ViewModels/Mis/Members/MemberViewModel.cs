using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Members
{
    public class MemberViewModel
    {
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// mã tác phẩm
        /// </summary>
        public string IpiNumber { get; set; } = string.Empty;
        /// <summary>
        /// Mã quôc tế
        /// </summary>
        public string InternalNo { get; set; } = string.Empty;
        /// <summary>
        /// Bút danh hay tên thật
        /// </summary>
        public string NameType { get; set; } = string.Empty;
        /// <summary>
        /// Tên tiếng anh
        /// </summary>
        public string IpEnglishName { get; set; } = string.Empty;
        /// <summary>
        /// Tên local
        /// </summary>
        public string IpLocalName { get; set; } = string.Empty;
        /// <summary>
        /// Thàn viên, NS không phải
        /// </summary>
        public string Society { get; set; } = string.Empty;        
    }
}
