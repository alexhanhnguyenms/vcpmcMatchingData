using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    public class ExceptionWork
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public string User { get; set; } = string.Empty;
        public virtual ICollection<ExceptionWorkDetail> ExceptionWorkDetails { get; set; }
        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public long TotalRecord { get; set; } = 0;
    }
}
