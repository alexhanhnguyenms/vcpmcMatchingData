using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking
{
    public class WorkTrackingCreateRequest
    {
        public string Id { get; set; } = string.Empty;
        public string WK_INT_NO { get; set; } = string.Empty;
        public string TTL_ENG { get; set; } = string.Empty;
        public string ISWC_NO { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        public string WRITER { get; set; } = string.Empty;
        public string ARTIST { get; set; } = string.Empty;
        public string SOC_NAME { get; set; } = string.Empty;
        /// <summary>
        /// 0: create, 1: update
        /// </summary>
        public int Type { get; set; } = 0;
        public int Year { get; set; } = 2020;
        public int MONTH { get; set; } = 8;
        public DateTime? TimeUpdate { get; set; }
        public DateTime? TimeCreate { get; set; }
    }
}
