using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vcpmc.Mis.ApplicationCore.Entities.makeData
{
    public class DistributionDataItem: ICloneable
    {
        [Key]
        public Guid Id { get; set; }// = new Guid();
        /// <summary>
        /// Thời giant tao
        /// </summary>
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Trạng thái nạp
        /// </summary>
        public bool StatusLoad { get; set; } = true;
        /// <summary>
        /// Mã master Po
        /// </summary>
        [ForeignKey(nameof(DistributionData))]
        public Guid DistributionDataId { get; set; }// = new Guid();
        public DistributionData DistributionData { get; set; }
        /// <summary>
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
        /// Bài hát 2
        /// </summary>
        public string Title2 { get; set; } = string.Empty;
        /// <summary>
        /// Nhóm nguồn
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// nhóm nguồn 2
        /// </summary>
        public string PoolName2 { get; set; } = string.Empty;
        /// <summary>
        /// Nguồn
        /// </summary>
        public string SourceName { get; set; } = string.Empty;
        /// <summary>
        /// nguồn 2
        /// </summary>
        public string SourceName2 { get; set; } = string.Empty;
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
        /// Nhuận bút(dùng để in báo cáo)
        /// </summary>
        public decimal Royalty2 { get; set; } = 0;
        /// <summary>
        /// Nơi thông kê
        /// </summary>
        public string Location { get; set; } = string.Empty;
        /// <summary>
        /// Ngày ký hợp đồng(chuỗi)
        /// </summary>
        public string strContractTime { get; set; } = string.Empty;
        /// <summary>
        /// Ngày ký hợp đồng
        /// </summary>
        public DateTime ContractTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Danh sach tac gia
        /// </summary>
        public string TotalAuthor { get; set; } = string.Empty;
        /// <summary>
        /// Map duoc tac gia
        /// </summary>
        public bool IsMapAuthor { get; set; } = false;
        /// <summary>
        /// Danh sach ta gia thuoc BH
        /// </summary>
        public string BhAuthor { get; set; } = string.Empty;                
        /// <summary>
        /// Điều kiện thời gian thoả mãn: true tra cho Bhmedia, false tra cho tac gia
        /// </summary>
        public bool IsCondittionTime { get; set; } = false;   
        /// <summary>
        /// thành viên nhóm, nếu là nhóm
        /// </summary>
        public string SubMember { get; set; } = string.Empty;
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
        /// Map theo group
        /// </summary>
        public bool IsMapByGroup { get; set; } = false;
        /// <summary>
        /// thời điểm vcpmc phân phối cho bh
        /// </summary>
        public DateTime? returnDate { get; set; }
        /// <summary>
        /// ty le thu huong
        /// </summary>
        public decimal Percent { get; set; } = 100;
        /// <summary>
        /// Co dua vao cho nguoi thu huong hay khong
        /// </summary>
        public bool IsGiveBeneficiary { get; set; } = true;
        /// <summary>
        /// là loại trừ hay không(neu la true la khong xet cua tac gia hoac cua bhmedia)
        /// </summary>
        public bool IsExcept { get; set; } = false;
        /// <summary>
        /// Tạo báo cáo
        /// </summary>
        public bool IsCreateReport { get; set; } = true;
        /// <summary>
        /// ghi chu(lay tu memberBH)
        /// </summary>
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chu tinh toan
        /// </summary>
        public string Note2 { get; set; } = string.Empty;

        // method for cloning 
        public object Clone()
        {
            // return cloned value using 
            // MemberwiseClone() method 
            return MemberwiseClone();
        }

    }
}