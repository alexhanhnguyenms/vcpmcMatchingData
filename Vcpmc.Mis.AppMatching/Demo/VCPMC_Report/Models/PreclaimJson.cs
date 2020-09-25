using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.AppMatching.Models
{
    public class PreclaimJson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id               { get; set;}
        public string Asset_ID          { get; set;}
        public string ISRC              { get; set;}
        public string Comp_Asset_ID     { get; set;}
        public string C_Title           { get; set;}
        public string C_ISWC            { get; set;}
        public string C_Workcode        { get; set;}
        public string C_Writers         { get; set;}
        public string Combined_Claim    { get; set;}
        public string Mechanical        { get; set;}
        public string Performance       { get; set;}
        public int MONTH             { get; set;}       
        public long CREATED_AT        { get; set;}
        public long? UPDATED_AT        { get; set; }

        #region str
        /*
         "_id" : ObjectId("5efda2e7e3c0e66276aef5d6"),
        "Asset_ID" : "__3TAkpnnhE",
        "ISRC" : "USC4R0303586",
        "Comp_Asset_ID" : "",
        "C_Title" : "ORANGE COLORED SKY1",
        "C_ISWC" : "T0701595949",
        "C_Workcode" : "159595",
        "C_Writers" : "WILLIAM STEIN|MILTON DELUGG",
        "Combined_Claim" : "100",
        "Mechanical" : "100",
        "Performance" : "99",
        "MONTH" : 2,
        "Year" : 2020,
        "CREATED_AT" : NumberLong(1593705798000),
        "UPDATED_AT" : NumberLong(1595927035259)
         */
        #endregion
    }
}
