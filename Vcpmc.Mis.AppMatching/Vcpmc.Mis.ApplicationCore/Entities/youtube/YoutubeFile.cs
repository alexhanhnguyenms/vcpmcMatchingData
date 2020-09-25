using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vcpmc.Mis.ApplicationCore.Entities.youtube
{
    public class YoutubeFile
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
        public long TotalRecord { get; set; } = 0;
        public virtual ICollection<YoutubeFile> YoutubeFiles { get; set; }
    }
}
