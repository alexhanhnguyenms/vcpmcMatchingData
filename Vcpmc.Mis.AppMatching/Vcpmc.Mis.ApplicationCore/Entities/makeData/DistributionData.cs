//using FileHelpers.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.makeData
{
    public class DistributionData
    {
        public bool check { get; set; } = false;
        public int no { get; set; } = 0;
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public int TotalRecord { get; set; } = 0;
        public string Note { get; set; } = string.Empty;
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public string User { get; set; } = string.Empty;
        public bool StatusLoadData { get; set; } = false;
        public bool StatusPrinter { get; set; } = false;
        public bool StatusSaveDataToDatabase { get; set; } = false;
        //[ForeignKey(nameof(Distibution))]
        //public Guid DistibutionId { get; set; } = new Guid();
        //public Distibution Distibution { get; set; }
        /// <summary>
        /// Map bang thanh vien BH
        /// </summary>       
        public Guid MemberBHId { get; set; }        
        ///// <summary>
        ///// map bang danh sach bai hat import cung voi tac gia
        ///// </summary>
        //[ForeignKey(nameof(ImportMapWorkMember))]
        public Guid ImportMapWorkMemberId { get; set; }
        //public ImportMapWorkMember ImportMapWorkMember { get; set; }
        ///// <summary>
        ///// Map cac bai hat loai tru
        ///// </summary>
        //[ForeignKey(nameof(ExceptionWork))]
        public Guid ExceptionWorkId { get; set; }
        //public ExceptionWork ExceptionWork { get; set; }
        //TODO 2020-09-25
        public virtual ICollection<DistributionDataItem> DistributionDataItems { get; set; } = new List<DistributionDataItem>();
        
    }
}
