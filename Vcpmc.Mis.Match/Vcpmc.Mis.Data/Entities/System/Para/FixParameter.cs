using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vcpmc.Mis.Data.Entities.System.Para
{
    public class FixParameter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Loai
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 1
        /// </summary>
        public string Value1 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 2
        /// </summary>
        public string Value2 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 3
        /// </summary>
        public string Value3 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 4
        /// </summary>
        public string Value4 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 5
        /// </summary>
        public string Value5 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 6
        /// </summary>
        public string Value6 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 7
        /// </summary>
        public string Value7 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 8
        /// </summary>
        public string Value8 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 9
        /// </summary>
        public string Value9 { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị 10
        /// </summary>
        public string Value10 { get; set; } = string.Empty;
    }
}
