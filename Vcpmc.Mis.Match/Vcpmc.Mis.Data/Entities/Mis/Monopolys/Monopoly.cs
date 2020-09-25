using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Vcpmc.Mis.Data.Entities.Mis.Monopolys
{
    public class Monopoly
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Nhóm độc quyền: 0: tác phẩm, 1 tác giả
        /// </summary>
        public int Group { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Người tạo
        /// </summary>
        public string User { get; set; } = string.Empty;
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Mã cũ
        /// </summary>
        public string CodeOld { get; set; }
        /// <summary>
        /// Mã mới
        /// </summary>
        public string CodeNew { get; set; }
        /// <summary>
        /// Tên tác phẩm, Ip name
        /// </summary>
        public string Name { get; set; }
        public string Name2 { get; set; }
        /// <summary>
        /// Loại
        /// </summary>
        public string NameType { get; set; }
        /// <summary>
        /// Chủ sở hữu
        /// </summary>
        public string Own { get; set; }
        public string Own2 { get; set; }
        /// <summary>
        /// Ghi chú độc quyền
        /// </summary>
        public string NoteMono { get; set; }
        /// <summary>
        /// Thời điểm bắt dầu
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// thời điểm kết thúc
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// thời điểm cập nhật
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// thời điểm nhận thông tin
        /// </summary>
        public DateTime ReceiveTime { get; set; }
        /// <summary>
        /// Ghi chú nhận tác phẩm
        /// </summary>
        public string Note2 { get; set; }
        /// <summary>
        /// Ghi chu hết hạn độc quyền
        /// </summary>
        public string Note3 { get; set; }
        public bool Tone { get; set; }
        public bool Web { get; set; }
        public bool Performances { get; set; }
        public bool PerformancesHCM { get; set; }
        public bool Cddvd { get; set; }
        public bool Kok { get; set; }
        public bool Broadcasting { get; set; }
        public bool Entertaiment { get; set; }
        public bool Film { get; set; }
        public bool Advertisement { get; set; }
        public bool PubMusicBook { get; set; }
        public bool Youtube { get; set; }
        public bool Other { get; set; }
        /// <summary>
        /// Trạng thái hết hạn độc quyền
        /// </summary>
        public bool IsExpired { get; set; }
    }
}
