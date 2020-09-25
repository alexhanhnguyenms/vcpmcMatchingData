using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Common
{
    /// <summary>
    /// Phân trang
    /// </summary>
    public class PagingRequestBase
    {
        /// <summary>
        /// Chỉ số trang
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// Kích cỡ trang
        /// </summary>
        public int PageSize { get; set; } = 5000;
    }
}