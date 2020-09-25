using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Data.Entities.System
{
    public class AppUser2
    {
        /*
        "_id" : ObjectId("5f30a2dba77f53558ca8e6e4"), 
    "UserName" : "admin", 
    "Email" : "alexnguyenhanhms@gmail.com", 
    "PasswordHash" : "AQAAAAEAACcQAAAAEJaHVNYr4XMtGgZtM0gP0Ukv+TYZYB+aJ60E0nUuzH/l9zEOyHWfX1JI1fc8Du6W+w==", 
    "PhoneNumber" : "string",    
    "AccessFailedCount" : "0", 
    "FirstName" : "trung", 
    "LastName" : "nguyen"
         */
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;        
        public int AccessFailedCount { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        /// <summary>
        /// Tài khoản bị khóa
        /// </summary>
        public bool IsLock { get; set; } = false;
    }
}
