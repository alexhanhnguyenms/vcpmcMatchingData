using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vcpmc.Mis.ApplicationCore.Entities
{
    /// <summary>
    /// Cấu hình file báo cáo
    /// </summary>
    public class ConfigFile
    {
        public ConfigFile()
        {
        }
        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; } = "";
      
        /// <summary>
        /// Lần chỉnh sửa cuối cùng
        /// </summary>
        public DateTime LastTimeEdit { get; set; } = DateTime.Now; 
        /// <summary>
        /// Tài khoản cấu hình
        /// </summary>
        public string User { get; set; } = "";
        /// <summary>
        /// Danh sách cấu hình
        /// </summary>
        public List<ConfigItem> configItems { get; set; } = new List<ConfigItem>();
        /// <summary>
        /// Xác định tiếng việt
        /// </summary>
        public List<ViDetectItem> viDetectItems { get; set; } = new List<ViDetectItem>();
    }
}
