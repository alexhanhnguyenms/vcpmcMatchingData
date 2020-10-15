using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Shared.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    /// <summary>
    /// Thông tin bài hát
    /// </summary>
    public class WorkViewModel
    {
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// mã tác phẩm
        /// </summary>
        public string WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Mã tác phẩm
        /// </summary>
        //public string LOCAL_WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác phẩm: YEU CO GAI BAC LIEU; YEU CO GAI BAC LIEU YÊU CÔ GÁI BẠC LIÊU
        /// </summary>
        public string TTL_ENG { get; set; } = string.Empty;        
        public string TTL_LOCAL { get; set; } = string.Empty;        
        public string ISWC_NO { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        /// <summary>
        /// Tác giả
        /// </summary>
        public string WRITER { get; set; } = string.Empty;
        public string WRITER_LOCAL { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn: THUY TRANG  ,KIM TU LONG  KIM TỬ LONG,VU LINH  VŨ LINH
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái: COMPLETE, INCOMPLETE
        /// </summary>
        public string WK_STATUS { get; set; } = string.Empty;
        public string SOC_NAME { get; set; } = string.Empty;
        public int StarRating { get; set; } = 0;
        /// <summary>
        /// số thứ tự
        /// </summary>
        public int SerialNo { get; set; } = 1;
        /// <summary>
        /// Danh sách độc quyen tác phẩm
        /// </summary>
        public List<MonopolyViewModel> MonopolyWorks { get; set; } = new List<MonopolyViewModel>();
        /// <summary>
        /// Danh sách độc quyền tác giả
        /// </summary>
        public List<MonopolyViewModel> MonopolyMembers { get; set; } = new List<MonopolyViewModel>();
        /// <summary>
        /// Danh sách tên thường gọi khác
        /// </summary>
        public List<OtherTitle> OtherTitles { get; set; } = new List<OtherTitle>(); 
        /// <summary>
        /// Danh sách tác giả
        /// </summary>
        public List<InterestedParty> InterestedParties { get; set; } = new List<InterestedParty>();
        /// <summary>
        /// Tổng số tác giả yêu cầu phải matching
        /// </summary>
        public int TotalWriterRequest { get; set; } = 0;
        /// <summary>
        /// Tổng số tác giả matching chính xác
        /// </summary>
        public int TotalWriterMatching { get; set; } = 0;
        public decimal RateWriterMatch { get; set; } = 0;
        /// <summary>
        /// danh sach nghe sy bieu dien tach ra
        /// </summary>
        public List<string> ListArtist { get; set; } = new List<string>();
        /// <summary>
        /// neu true thi đúng điều kiện matching nghệ sy biểu diễn
        /// </summary>
        public bool IsCheckMatchingArtist { get; set; } = false;
        /// <summary>
        /// Tổng số tác giả thành viên
        /// </summary>
        public int TotalMember { get; set; } = 0;
        /// <summary>
        /// Tổng số tác giả không thành viên
        /// </summary>
        public int TotalNonMember { get; set; } = 0;
        /// <summary>
        /// Tổng số thành viên VCPMC
        /// </summary>
        public int TotalMemberVcpmc { get; set; } = 0;
    }
}
