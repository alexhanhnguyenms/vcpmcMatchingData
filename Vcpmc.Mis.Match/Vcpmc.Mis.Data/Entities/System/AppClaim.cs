using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Data.Entities.System
{
    public class AppClaim
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Group { get; set; } = string.Empty;
        public string Claim { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
