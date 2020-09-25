using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
namespace Vcpmc.Mis.Data.Entities.Mis.Works
{
    public class WorkTracking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string WK_INT_NO { get; set; }
        public string TTL_ENG { get; set; }
        public string ISWC_NO { get; set; }
        public string ISRC { get; set; }
        public string WRITER { get; set; }
        public string ARTIST { get; set; }
        public string SOC_NAME { get; set; }
        /// <summary>
        /// 0: create, 1: update
        /// </summary>
        public int Type { get; set; }
        public int Year { get; set; }
        public int MONTH { get; set; }
        public DateTime? TimeUpdate { get; set; }
        public DateTime? TimeCreate { get; set; }
    }
}
