using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter
{
    public class DistributionViewModel
    {

        public string Id { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; } = DateTime.Now;
        /// <summary>
        /// Nam
        /// </summary>
        public int Year { get; set; } = 2020;
        /// <summary>
        /// Quy
        /// </summary>
        public int Quarter { get; set; } = 1;
        /// <summary>
        /// Tiền tác giả
        /// </summary>
        public decimal TotalWriterShare { get; set; } = 0;
        /// <summary>
        /// Tiền phát hành
        /// </summary>
        public decimal TotalPublisherShare { get; set; } = 0;
        /// <summary>
        /// Điều chỉnh
        /// </summary>
        public decimal TotalAdjs { get; set; } = 0;
        /// <summary>
        /// Phân phối youtube
        /// </summary>
        public decimal TotalYoutube { get; set; } = 0;
        /// <summary>
        /// Phân phối ex
        /// </summary>
        public decimal TotalDisExcel { get; set; } = 0;
        /// <summary>
        /// Tiền bản Quyền
        /// </summary>
        public decimal TotalNetRoyalties { get; set; } = 0;
        public List<Distribution> Items { get; set; } = new List<Distribution>();
    }
}
