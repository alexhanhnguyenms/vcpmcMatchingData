using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
    public class WorkRetrievalStatusDetailViewModel
    {
        /// <summary>
        /// Số thứ tự trang
        /// </summary>
        public string pageNumber { get; set; } = string.Empty;//1,
        /// <summary>
        /// Tổng số dòng trên trang
        /// </summary>
        public string rowsPerPage { get; set; } = string.Empty;//300
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public string totalNoOfRecords { get; set; } = string.Empty;//36011,
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public string totalPages { get; set; } = string.Empty;//0,
        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public ExtraData extraData { get; set; } = new ExtraData();//"TOT_COMPOSITE_WKS":15,"TOT_NORMAL_WKS":35996
    }
}
