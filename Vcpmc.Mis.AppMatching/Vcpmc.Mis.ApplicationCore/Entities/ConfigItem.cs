using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities
{
    /// <summary>
    /// Phần tử caais hình
    /// </summary>
    public class ConfigItem
    {
        /// <summary>
        /// Tên Cột
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;
        /// <summary>
        /// Giá trị
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int Order { get; set; } = 0;
        /// <summary>
        /// Có lọc không
        /// </summary>
        public bool IsCheck { get; set; } = false;

        public string OldS { get; set; } = string.Empty;
        public string NewS { get; set; } = string.Empty;
    }
}
