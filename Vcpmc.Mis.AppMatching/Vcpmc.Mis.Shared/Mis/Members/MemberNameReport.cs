using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Members
{
    public class MemberNameReport
    {
        /// <summary>
        /// Mã tác giả: ++PP+PA
        /// </summary>
        public string MemberCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác giả
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Tên local
        /// </summary>
        public string NameLocal { get; set; } = string.Empty;
    }
}
