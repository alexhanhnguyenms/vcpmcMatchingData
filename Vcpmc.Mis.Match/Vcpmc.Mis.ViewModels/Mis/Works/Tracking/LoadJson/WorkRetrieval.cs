﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
    public class WorkRetrieval
    {
        public string wkIntNo { get; set; } = string.Empty;//":18914524,
        public string localwkIntNo { get; set; } = string.Empty;//":"",
        public string id { get; set; } = string.Empty;//":"0_18914524_LOCAL",
        public string iswcNo { get; set; } = string.Empty;//":"T9202732136",
        public string isrcNo { get; set; } = string.Empty;//":"","pubNo":null,
        public string title { get; set; } = string.Empty;//":"DROP DAT WHAT THEME OPENING",
        public string engTitle { get; set; } = string.Empty;//":"DROP DAT WHAT THEME OPENING",
        public string localTitle { get; set; } = string.Empty;//":"",
        public string composer { get; set; } = string.Empty;//":"DANIEL J SCHNEIDER, ERIC PETER GOLDMAN, JAMES ANDREW FARROW, MICHAEL THOMAS CORCORAN",
        public string[] artiste { get; set; } = new string[]{};//":[],
        public string wkStatus { get; set; } = string.Empty;//":
        public string UNI { get; set; } = string.Empty;//:MR,COM:PR",
        public string prWkStatus { get; set; } = string.Empty;//":"COM",
        public string mrWkStatus { get; set; } = string.Empty;//":"UNI",
        public string avInd { get; set; } = string.Empty;//":"Y","albInd":"N",
        public string compositeInd { get; set; }//":"N",
        public string remarks { get; set; } = string.Empty;//":"",
        public string language { get; set; } = string.Empty;//":"ENGLISH",
        public string languageCode { get; set; } = string.Empty;//":"EN",
        public string subLanguageCode { get; set; } = string.Empty;//":"NIL",
        public string subLanguage { get; set; } = string.Empty;//":"NIL",
        public string workCategoryCode { get; set; } = string.Empty;//":"POP",
        public string workCategoryName { get; set; } = string.Empty;//":"POPULAR",
        public string workSubCategoryCode { get; set; } = string.Empty;//":"NIL",
        public string workSubCategoryName { get; set; } = string.Empty;//":"NIL",
        public string createdBy { get; set; } = string.Empty;//":"CISNET",
        public string createdDate { get; set; } = string.Empty;//":null,
        public string amendmentBy { get; set; } = string.Empty;//":null,
        public string amendmentDate { get; set; } = string.Empty;//":"",
        public string matchingScore { get; set; } = string.Empty;//":0,
        public string wkPopularityScore { get; set; }//":0,
        public string logiDeleted { get; set; } = string.Empty;//":"N",
        public string sourceDB { get; set; } = string.Empty;//":"LOCAL",
        public string cisnetWorkSocietyCode { get; set; } = string.Empty;//":null,
        public string cisnetWorkSourceDB { get; set; } = string.Empty;//":null,
        public string cisnetWorkSourceCode { get; set; } = string.Empty;//":null,
        public string is_Publisher_Present { get; set; } = string.Empty;//":"",
        public string starRating { get; set; } = string.Empty;//":23,
        public string titleNo { get; set; } = string.Empty;//":1,
        public string titleType { get; set; } = string.Empty;//":"OT",
        public List<OtherTitleViewItem> otherTitleViews { get; set; } = new List<OtherTitleViewItem>();//"
        public string duration { get; set; } = string.Empty;//":"00:00:33",
        public string usageType { get; set; } = string.Empty;//":null,
        public string cueType { get; set; } = string.Empty;//":null,
        public string origin { get; set; } = string.Empty;//":null,
        public string noOfUsage { get; set; } = string.Empty;//":null,
        public string avrWksStatus { get; set; } = string.Empty;//":null,
        public string avrFileName { get; set; } = string.Empty;//":null,
        public string linkSeqNo { get; set; } = string.Empty;//":null,
        public string wkCWRStatus { get; set; } = string.Empty;//":null,
        public string workType { get; set; } = string.Empty;//":"VOC",
        public string crossRefNo { get; set; } = string.Empty;//":"",
        public string txnStatus { get; set; } = string.Empty;//":"FA:MR,FA:PR",
        public string conflict { get; set; } = string.Empty;//":null,
        public string songName { get; set; } = string.Empty;//":null,
        public string songFileUrl { get; set; } = string.Empty;//":null,
        public string songFileName { get; set; } = string.Empty;//":null,
        public string songLive { get; set; } = string.Empty;//":false,
        public string titlelanguageCode { get; set; } = string.Empty;//":null,
        public string titlelanguageName { get; set; } = string.Empty;//":null,
        public string titlesubLanguageCode { get; set; } = string.Empty;//":null,
        public string titlesubLanguageName { get; set; } = string.Empty;//":null,
        public string pubWksNo { get; set; } = string.Empty;//":null
    }
}