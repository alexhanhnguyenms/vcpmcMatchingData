using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Members
{
    public class CheckMember
    {
        /// <summary>
        /// Gia tri: VCPMC,E
        /// </summary>
        public string CheckValue;
        /// <summary>
        /// sang
        /// </summary>

        public string ToValue;

        /// <summary>
        /// ma tac gia
        /// </summary>
        public string IpCode { get; set; }
        /// <summary>
        /// Mã tac pham
        /// </summary>
        public string IpNameType { get; set; }
    }
    public class ReplateText
    {
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }
}
