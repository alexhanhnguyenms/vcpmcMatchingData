using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    public class WorkStatus
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
