using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Works
{
    /// <summary>
    /// Loại tiêu đề khác
    /// </summary>
    public class OtherTitle
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; } = 0;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Loại tiêu đề
        /// </summary>
        //public string TitleType { get; set; } = string.Empty;
        /// <summary>
        /// Ngôn ngữ
        /// </summary>
        //public string Language { get; set; } = string.Empty;
    }
}
