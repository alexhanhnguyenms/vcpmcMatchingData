using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    public class ExceptionWorkDetail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.Now;      
        [ForeignKey(nameof(ExceptionWork))]
        public Guid ExceptionWorkId { get; set; }
        public ExceptionWork ExceptionWork { get; set; }       
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Tác giả
        /// </summary>
        public string Member { get; set; } = string.Empty;
        /// <summary>
        /// tac giả 2
        /// </summary>
        public string Member2 { get; set; } = string.Empty;
        /// <summary>
        /// tác phẩm 
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Tac phẩm
        /// </summary>
        public string Title2 { get; set; } = string.Empty;
        /// <summary>
        /// Tên nguồn loại trừ, cách nhau dấu ;
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Loại: EXCEPT : loai tru, INCLUDE: bao gom
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach pool name
        /// </summary>
        public List<string> ListPoolName { get; set; } = new List<string>(); 
    }
}
