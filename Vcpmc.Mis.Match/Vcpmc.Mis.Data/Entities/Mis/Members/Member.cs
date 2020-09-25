using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Vcpmc.Mis.Data.Entities.Mis.Members
{
    public class Member
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// mã tác phẩm
        /// </summary>
        public string IpiNumber { get; set; } = string.Empty;
        /// <summary>
        /// Mã quôc tế
        /// </summary>
        public string InternalNo { get; set; } = string.Empty;
        /// <summary>
        /// Bút danh hay tên thật
        /// </summary>
        public string NameType { get; set; } = string.Empty;
        /// <summary>
        /// Tên tiếng anh
        /// </summary>
        public string IpEnglishName { get; set; } = string.Empty;
        /// <summary>
        /// Tên local
        /// </summary>
        public string IpLocalName { get; set; } = string.Empty;
        /// <summary>
        /// Thàn viên, NS không phải
        /// </summary>
        public string Society { get; set; } = string.Empty;
    }
}
