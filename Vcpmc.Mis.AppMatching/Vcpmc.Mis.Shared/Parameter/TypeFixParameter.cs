using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Parameter
{
    public enum TypeFixParameter
    {
        /// <summary>
        /// Chuyển từ không thành viên sang thành viên
        /// </summary>
        NonMemberToMember,
        /// <summary>
        /// Chuyển sang publisher
        /// </summary>
        IpWorkRoleToE,
        /// <summary>
        /// Thay thế title trong matching
        /// </summary>
        MatchingReplateTitle,
        /// <summary>
        /// Thay thế tác giả trong matching
        /// </summary>
        MatchingReplateWriter,
        
    }
}
