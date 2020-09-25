using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Shared.viewModel.report
{
    /// <summary>
    /// Danh sach thanh vien chon
    /// </summary>
    public class ItemMember
    {
        /// <summary>
        /// danh so thu tu
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// thong tin
        /// </summary>
        public string Name { get; set; } = string.Empty;
        //public string NameOfContent { get; set; } = string.Empty;
        /// <summary>
        /// Loai: SINGLE, GROUP, EXTEND
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Thanh vien
        /// </summary>
        public string Member { get; set; } = string.Empty;
        /// <summary>
        /// Thanh vien tieng viet
        /// </summary>
        public string MemberVN { get; set; } = string.Empty;
        /// <summary>
        /// Thanh vien tinh theo phan chinh
        /// </summary>
        public List<string> MAIN_subMember { get; set; } = new List<string>();
        /// <summary>
        /// Thanh vien tinh theo phan ngoai le
        /// </summary>
        public List<string> EXCEPT_subMember { get; set; } = new List<string>();
    }
}
