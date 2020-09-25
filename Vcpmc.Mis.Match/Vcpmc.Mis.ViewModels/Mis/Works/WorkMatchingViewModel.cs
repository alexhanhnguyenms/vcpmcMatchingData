using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkMatchingViewModel
    {
        /// <summary>
        /// Xac dinh khong phai thanh vien 
        /// </summary>
        //public bool IsNonMember { get; set; } = false;

        /// <summary>
        /// Số thứ tự(đầu vào)
        /// </summary>
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// Mã tác phẩm(đầu vào)
        /// </summary>
        public string WorkCode { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát (đầu vào)
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát (đầu vào)
        /// </summary>
        public string Title2 { get; set; } = string.Empty;
        /// <summary>
        /// tác giả(đầu vào)
        /// </summary>
        public string Writer { get; set; } = string.Empty;
        /// <summary>
        /// tác giả 2(đầu vào)
        /// </summary>
        public string Writer2 { get; set; } = string.Empty;
        /// <summary>
        /// Danh sách tác giả không dấu (đầu vào)
        /// </summary>
        public List<string> ListWriter2 { get; set; } = new  List<string>();
        /// <summary>
        /// Nghệ sỹ (đầu vào)
        /// </summary>
        public string Artist { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ không dấu (đầu vào)
        /// </summary>
        public string Artist2 { get; set; } = string.Empty;
        /// <summary>
        /// Danh sách nghệ sỹ biểu diễn không dấu (đầu vào)
        /// </summary>
        public List<string> ListArtist2 { get; set; } = new List<string>();
        /// <summary>
        /// Mã bài hát matching
        /// </summary>
        public string WorkCodeMatching { get; set; } = string.Empty;
        /// <summary>
        /// Tên bài hát matching
        /// </summary>
        public string TitleMatching { get; set; } = string.Empty;
        /// <summary>
        /// Ma tac gia matching
        /// </summary>
        public string WriterCodeMatching { get; set; } = string.Empty;
        /// <summary>
        /// Mã local mathcing
        /// </summary>
        public string WriterIPNumberMatching { get; set; } = string.Empty;
        /// <summary>
        /// Tác gia matching
        /// </summary>
        public string WriterMatching { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ matching
        /// </summary>
        public string ArtistMatching { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach tac gia matching
        /// </summary>
        public List<InterestedParty> InterestedPartiesMatching { get; set; } = new List<InterestedParty>();
        /// <summary>
        /// to chuc matching
        /// </summary>
        public string SocietyMatching { get; set; } = string.Empty;
        /// <summary>
        /// tac gia voi to chuc
        /// </summary>
        public string WriterMatchingWithSoceity { get; set; } = string.Empty;
        /// <summary>
        /// Doc quyen tác phẩm
        /// </summary>
        public bool IsWorkMonopoly { get; set; } = false;
        /// <summary>
        /// Lĩnh vực độc quyền tác phẩm
        /// </summary>
        public string WorkFields { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chu độc quyền tác phẩm
        /// </summary>
        public string WorkMonopolyNote { get; set; } = string.Empty;
        /// <summary>
        ///  Doc quyen tác giả
        /// </summary>
        public bool IsMemberMonopoly { get; set; } = false;
        /// <summary>
        /// Lĩnh vực độc quyền tác giả
        /// </summary>
        public string MemberFields { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chú độc quyền tác phẩm
        /// </summary>
        public string MemberMonopolyNote { get; set; } = string.Empty;
        /// <summary>
        /// Đã matching
        /// </summary>
        public bool IsMatching { get; set; } = false;
        /// <summary>
        /// Thành công
        /// </summary>
        public bool IsSuccess { get; set; } = false;
        /// <summary>
        /// Ghi chú matching
        /// </summary>
        public string Messsage { get; set; } = string.Empty;
        /// <summary>
        /// Khong phai thanh vien
        /// </summary>
        public string NonMember { get; set; } = string.Empty;
        /// <summary>
        /// Code sau ki doi
        /// </summary>
        public string WorkcodeChangeToNew { get; set; } = string.Empty;
    }
}
