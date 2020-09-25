using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.makeData
{
    public class Distibution
    {
        [Key]
        public Guid Id { get; set; }// = new Guid();
        public string Name { get; set; } = string.Empty;
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public string User { get; set; } = string.Empty;
        //public virtual ICollection<DistributionData> DistributionDatas { get; set; }// = new ICollection<EdiFilesItem>();
        public long TotalRecord { get; set; } = 0;
    }
}
