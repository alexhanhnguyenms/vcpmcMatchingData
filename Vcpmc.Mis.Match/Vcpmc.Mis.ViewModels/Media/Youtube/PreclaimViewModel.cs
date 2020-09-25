using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Media.Youtube
{
    public class PreclaimViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Asset_ID { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        public string Comp_Asset_ID { get; set; } = string.Empty;
        public string C_Title { get; set; } = string.Empty;
        public string C_ISWC { get; set; } = string.Empty;
        public string C_Workcode { get; set; } = string.Empty;
        public string C_Writers { get; set; } = string.Empty;
        public string Combined_Claim { get; set; } = string.Empty;
        public string Mechanical { get; set; } = string.Empty;
        public string Performance { get; set; } = string.Empty;
        //public int MONTH { get; set; } = 1;
        //public int Year { get; set; } = 2020;
        public DateTime DtCREATED_AT { get; set; } = DateTime.Now;     
        public DateTime? DtUPDATED_AT { get; set; }       
        public int SerialNo { get; set; } = 1;
        /*
         "Asset_ID" : "__3TAkpnnhE", 
        "ISRC" : "USC4R0303586", 
        "Comp_Asset_ID" : "", 
        "C_Title" : "ORANGE COLORED SKY", 
        "C_ISWC" : "T0701595949", 
        "C_Workcode" : "159595", 
        "C_Writers" : "WILLIAM STEIN|MILTON DELUGG", 
        "Combined_Claim" : "100", 
        "Mechanical" : "100", 
        "Performance" : "100", 
        "MONTH" : NumberInt(2), 
        "CREATED_AT" : NumberLong(1593705798000), 
        "UPDATED_AT" : null
         */
    }
}
