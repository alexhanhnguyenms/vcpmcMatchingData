using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.ViewModels.Common;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking
{
    public class GetWorkTrackingPagingRequest: PagingRequestBase
    {
        //public string Id { get; set; }
        //public string WK_INT_NO { get; set; }
        //public string TTL_ENG { get; set; }
        //public string ISWC_NO { get; set; }
        //public string ISRC { get; set; }
        //public string WRITER { get; set; }
        //public string ARTIST { get; set; }
        //public string SOC_NAME { get; set; }
        /// <summary>
        /// 0: create, 1: update
        /// </summary>
        public int Type { get; set; } = 0;
        public int Year { get; set; } = 2020;
        public int MONTH { get; set; } = 8;
        //public DateTime? TimeUpdate { get; set; }
        //public DateTime? TimCreate { get; set; }
    }
}
