using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    public class MemberBHDetail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [ForeignKey(nameof(MemberBH))]
        public Guid MemberBHId { get; set; }
        public MemberBH MemberBH { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Loai: S: single = 1, G: group = 2
        /// SINGLE, GROUP
        /// </summary>
        public string Type { get; set; } = "SINGLE";
        /// <summary>
        /// Tác giả
        /// </summary>
        public string Member { get; set; } = string.Empty;
        /// <summary>
        /// tên có dau
        /// </summary>
        public string MemberVN { get; set; }
        /// <summary>
        /// Nghệ danh
        /// </summary>
        public string StageName { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach ten stageName
        /// </summary>
        public List<string> ListStageName { get; set; } = new List<string>();
        /// <summary>
        /// thành viên nhóm, nếu là nhóm
        /// </summary>
        public string SubMember { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach thanh vien nhom
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
        /// Luon lay
        /// </summary>
        public bool IsAlwaysGet { get; set; } = false;
        /// <summary>
        /// thời điểm vcpmc phân phối cho bh
        /// </summary>
        public DateTime? returnDate { get; set; }
        /// <summary>
        /// ty le thu huong
        /// </summary>
        public decimal Percent { get; set; } = 100;
        /// <summary>
        /// Co dua vao ch nguoi thu huong hay khong
        /// </summary>
        public bool IsGiveBeneficiary { get; set; } = true;
        /// <summary>
        /// Tạo báo cáo
        /// </summary>
        public bool IsCreateReport { get; set; } = true;
        /// <summary>
        /// ghi chu
        /// </summary>
        public string Note { get; set; } = string.Empty;
        
    }
}
