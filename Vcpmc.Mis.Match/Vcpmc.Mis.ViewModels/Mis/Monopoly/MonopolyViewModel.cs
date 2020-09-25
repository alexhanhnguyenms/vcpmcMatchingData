using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Monopoly
{
    public class MonopolyViewModel
    {
        /// <summary>
        /// Key tu tang
        /// </summary>        
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Nhóm độc quyền: 0: tác phẩm, 1 tác giả
        /// </summary>
        public int Group { get; set; } = 0;        
        /// <summary>
        /// Mã cũ
        /// </summary>
        public string CodeOld { get; set; } = string.Empty;
        /// <summary>
        /// Mã mới
        /// </summary>
        public string CodeNew { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác phẩm, Ip name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string Name2 { get; set; } = string.Empty;
        /// <summary>
        /// Loại: PA,PP
        /// </summary>
        public string NameType { get; set; } = string.Empty;
        /// <summary>
        /// Chủ sở hữu
        /// </summary>
        public string Own { get; set; } = string.Empty;
        public string Own2 { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chú độc quyền
        /// </summary>
        public string NoteMono { get; set; } = string.Empty;
        /// <summary>
        /// Thời điểm bắt dầu
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;
        /// <summary>
        /// thời điểm kết thúc
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;
        /// <summary>
        /// thời điểm cập nhật
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// thời điểm nhận thông tin
        /// </summary>
        public DateTime ReceiveTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Ghi chú nhận tác phẩm
        /// </summary>
        public string Note2 { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chu hết hạn độc quyền
        /// </summary>
        public string Note3 { get; set; } = string.Empty;
        /// <summary>
        /// 1
        /// </summary>
        public bool Tone { get; set; } = false;
        /// <summary>
        /// 2
        /// </summary>
        public bool Web { get; set; } = false;
        /// <summary>
        /// 3
        /// </summary>
        public bool Performances { get; set; } = false;
        /// <summary>
        /// 4
        /// </summary>
        public bool PerformancesHCM { get; set; } = false;
        /// <summary>
        /// 5
        /// </summary>
        public bool Cddvd { get; set; } = false;
        /// <summary>
        /// 6
        /// </summary>
        public bool Kok { get; set; } = false;
        /// <summary>
        /// 7
        /// </summary>
        public bool Broadcasting { get; set; } = false;
        /// <summary>
        /// 8
        /// </summary>
        public bool Entertaiment { get; set; } = false;
        /// <summary>
        /// 9
        /// </summary>
        public bool Film { get; set; } = false;
        /// <summary>
        /// 10
        /// </summary>
        public bool Advertisement { get; set; } = false;
        /// <summary>
        /// 11
        /// </summary>
        public bool PubMusicBook { get; set; } = false;
        /// <summary>
        /// 12
        /// </summary>
        public bool Youtube { get; set; } = false;
        /// <summary>
        /// 13
        /// </summary>
        public bool Other { get; set; } = false;
        /// <summary>
        /// Trạng thái hết hạn độc quyền
        /// </summary>
        public bool IsExpired { get; set; } = false;
        public int SerialNo { get; set; } = 1;
    }
}
