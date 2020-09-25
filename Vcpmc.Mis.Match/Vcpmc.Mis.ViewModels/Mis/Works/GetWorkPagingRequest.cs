using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.ViewModels.Common;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class GetWorkPagingRequest: PagingRequestBase
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
        /// to chuc thanh vien
        /// </summary>
        public string SOCIETY { get; set; } = string.Empty;
        /// <summary>
        /// 0: tim like, 1: tim chinh xac
        /// </summary>
        public int SearchType { get; set; } = 0;
        /// <summary>
        /// Lay thong tin doc quyen
        /// </summary>
        public bool IsGetMonopolyInfo { get; set; } = false;
    }
}
