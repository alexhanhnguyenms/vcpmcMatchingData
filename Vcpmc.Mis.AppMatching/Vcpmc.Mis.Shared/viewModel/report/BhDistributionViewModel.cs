using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Shared.viewModel.report
{
    public class BhDistributionViewModel: ICloneable
    {
        // <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; } = 0;
        /// <summary>
        /// Mã bài hát
        /// </summary>
        public string WorkInNo { get; set; } = string.Empty;
        /// <summary>
        /// Bài hát
        /// </summary>
        public string Title { get; set; } = string.Empty;        
        /// <summary>
        /// Nhóm nguồn
        /// </summary>
        public string PoolName { get; set; } = string.Empty;        
        /// <summary>
        /// Nguồn
        /// </summary>
        public string SourceName { get; set; } = string.Empty;        
        /// <summary>
        /// Quyền
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// Tỷ lệ chia
        /// </summary>
        public decimal Share { get; set; } = 100;
        /// <summary>
        /// Nhuận bút
        /// </summary>
        public decimal Royalty { get; set; } = 0;
        /// <summary>
        /// Nhuận bút: chia theo ty le
        /// </summary>
        public decimal Royalty2 { get; set; } = 0;
        /// <summary>
        /// Danh sach ta gia thuoc BH
        /// </summary>
        public string BhAuthor { get; set; } = string.Empty;
        /// <summary>
        /// Điều kiện thời gian thoả mãn
        /// </summary>
        public bool IsCondittionTime { get; set; } = false;
        /// <summary>
        /// Thanh vien nhom
        /// </summary>
        public string SubMember { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach member
        /// </summary>
        public List<string> ListSubMember { get; set; } = new List<string>();
        /// <summary>
        /// người thụ hưởng
        /// </summary>
        public string Beneficiary { get; set; } = string.Empty;
        /// <summary>
        /// main là phần chính, excpt là lấy phần ngoại lệ, ví dụ cho maseco
        /// 1: MAIN
        /// 2: EXCEPT
        /// </summary>
        public string GetPart { get; set; } = "Main";
        /// <summary>
        /// Map theo group
        /// </summary>
        public bool IsMapByGroup { get; set; } = false;
        /// <summary>
        /// ty le thu huong
        /// </summary>
        public decimal Percent { get; set; } = 100;
        /// <summary>
        /// Co dua vao cho nguoi thu huong hay khong
        /// </summary>
        public bool IsGiveBeneficiary { get; set; } = true;
        /// <summary>
        /// là loại trừ hay không
        /// </summary>
        public bool IsExcept { get; set; } = false;
        // method for cloning 
        public object Clone()
        {
            // return cloned value using 
            // MemberwiseClone() method 
            return MemberwiseClone();
        }

    }
}
