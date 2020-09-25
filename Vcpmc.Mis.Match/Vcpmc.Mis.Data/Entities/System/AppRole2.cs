using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.Data.Entities.System
{
    public class AppRole2
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<AppClaimViewModel> Rights { get; set; } = new List<AppClaimViewModel>();
    }
}
