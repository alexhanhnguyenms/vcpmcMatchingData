using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter
{
    [Serializable]
    public class DistributionDetails
    {
        /// <summary>
        /// Mã tác phẩm
        /// </summary>
        public string WorkIntNo { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác phẩm
        /// </summary>
        public string Title { get; set; } = string.Empty;
        public string PoolName { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        /// <summary>
        /// Quyền
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// Tỷ lệ share
        /// </summary>
        public decimal Share { get; set; } = 0;
        /// <summary>
        /// Thụ hưởng
        /// </summary>
        public decimal Royalty { get; set; } = 0;
        public int SerialNo { get; set; } = 0;        
    }
}
