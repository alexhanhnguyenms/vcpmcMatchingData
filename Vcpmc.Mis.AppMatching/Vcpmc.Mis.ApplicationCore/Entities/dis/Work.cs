using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    /// <summary>
    /// Bai hat, tac pham
    /// </summary>
    public class Work
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Member> Members { get; set; }
    }
}
