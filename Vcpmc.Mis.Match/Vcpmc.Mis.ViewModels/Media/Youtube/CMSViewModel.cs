using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Media.Youtube
{
    public class CMSViewModel
    {
        public string IssueID { get; set; } = string.Empty;//NdYfcyJNyuc
        public string IssueType  { get; set; } = string.Empty;//Potential claim

        public string OtherParty { get; set; } = string.Empty;//claim
        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public string ExpiryDate { get; set; } = string.Empty;//8/3/2020
        public string AssetName { get; set; } = string.Empty;//DON T YOU
        public string AssetType { get; set; } = string.Empty;//Composition
        public string AssetCreatedOn { get; set; } = string.Empty;//42366
        public string AssetID { get; set; } = string.Empty;//A251071082713927
        public string ReferenceID { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        public string UPC { get; set; } = string.Empty;
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomID { get; set; } = string.Empty;//9191442
        public string Labels { get; set; } = string.Empty;
        public string ISWC { get; set; } = string.Empty;
        public string OverlappingAssetID { get; set; } = string.Empty;
        public string OverlappingAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Mã video 
        /// </summary>
        public string VideoID { get; set; } = string.Empty;//Bbd9Qu3yAVU
        /// <summary>
        /// Tiêu đề video
        /// </summary>
        public string VideoTitle { get; set; } = string.Empty;//Live Piano Bar
        /// <summary>
        /// Tên kênh
        /// </summary>
        public string ChannelName { get; set; } = string.Empty;//Reksio Gaming
        /// <summary>
        /// Mã kênh
        /// </summary>
        public string ChannelID { get; set; } = string.Empty;//UCy5PO5eyFPTcm9OIjsdYJVw
        public string ClaimID { get; set; } = string.Empty;//q4MemR2iFcU
        public string MatchType { get; set; } = string.Empty;//Audio
        public int ViewsAffectedDaily { get; set; } = 0;
        public int ViewsLifetime { get; set; } = 0;
        public int ClaimedVideosAffected { get; set; } = 0;
        public int DurationTimeSeconds { get; set; } = 0;
        public int DurationPercentageReference { get; set; } = 0;
        public int DurationPercentageVideo { get; set; } = 0;
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Status { get; set; } = string.Empty;//Action required
        public string StatusDetail { get; set; } = string.Empty;
        /// <summary>
        /// Link: https://studio.youtube.com/owner/nKykFXU-Fc-
        /// </summary>
        public string LinkToIssue { get; set; } = string.Empty;
        /// <summary>
        /// Người dò
        /// </summary>
        public string PersonMatching { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái tự động
        /// </summary>
        public bool AutoStatus { get; set; } = false;                                  
        /// <summary>
        /// Note trạng thái tự động
        /// </summary>
        public string AutoNote { get; set; } = string.Empty;
    }
}
