using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.ViewModels.Common;

namespace Vcpmc.Mis.ViewModels.Mis.History
{
    public class GetWorkHistoryPagingRequest : PagingRequestBase
    {
        /// <summary>
        /// 0: tim like, 1: tim chinh xac
        /// </summary>
        public int SearchType { get; set; } = 0;
        /// <summary>
        /// Tiêu đề tác phẩm cung cấp
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// tiêu đề tác phẩm cung cấp
        /// </summary>
        public string Title2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn cung cấp
        /// </summary>
        public string Artist { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn cũng cấp
        /// </summary>
        public string Artist2 { get; set; } = string.Empty;
        /// <summary>
        /// Tác giả cung cấp
        /// </summary>
        public string Writer { get; set; } = string.Empty;
        /// <summary>
        /// tác giả cung cấp
        /// </summary>
        public string Writer2 { get; set; } = string.Empty;
        /// <summary>
        /// mã tác phẩm
        /// </summary>
        public string WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề tác phẩm
        /// </summary>
        public string WK_Title { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề tác phẩm
        /// </summary>
        public string WK_Title2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn: THUY TRANG  ,KIM TU LONG  KIM TỬ LONG,VU LINH  VŨ LINH
        /// </summary>
        public string WK_Artist { get; set; } = string.Empty;
        public string WK_Artist2 { get; set; } = string.Empty;
        public string WK_Writer { get; set; } = string.Empty;
        public string WK_Writer2 { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái: COMPLETE, INCOMPLETE, UNIDENTIFIED
        /// </summary>
        public string WK_STATUS { get; set; } = string.Empty;

    }
}
