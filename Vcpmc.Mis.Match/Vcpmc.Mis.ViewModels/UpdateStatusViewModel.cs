using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Utilities.Common;

namespace Vcpmc.Mis.ViewModels
{
    public class UpdateStatusViewModel
    {
        public CommandType Command { get; set; } = CommandType.Update;
        public UpdateStatus Status { get; set; } = UpdateStatus.Successfull;
        /// <summary>
        /// ghi chú
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Tổng số cập nhật
        /// </summary>
        public long TotalEffect { get; set; } = 1;
        /// <summary>
        /// Mã hệ thống youtube
        /// </summary>
        public string Asset_id { get; set; } = string.Empty;
        /// <summary>
        /// Mã bài hat
        /// </summary>
        public string WorkCode { get; set; } = string.Empty;
        /// <summary>
        /// kỳ phân phối của youtube
        /// </summary>
        public long Month { get; set; } = 1;
        /// <summary>
        /// Năm phân phối của youtube
        /// </summary>
        public long Year { get; set; } = 2020;
    }
}
