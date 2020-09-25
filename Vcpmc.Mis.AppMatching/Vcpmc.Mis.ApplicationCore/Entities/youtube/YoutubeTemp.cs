using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vcpmc.Mis.ApplicationCore.Entities.youtube
{
    /// <summary>
    /// Phiếu import Datatemp
    /// </summary>
    public class YoutubeTemp
    {
        /// <summary>
        /// Key tu tang
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public string User { get; set; } = string.Empty;
        /// <summary>
        /// Chi tiết dữ liệu nhập từ SQL server
        /// </summary>
        public virtual ICollection<YoutubeDataTemp> YoutubeDataTemps { get; set; }
    }
}
