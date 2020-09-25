using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Utilities.Common;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkMatchingListRequest
    {
        /// <summary>
        /// Danh sach can matching
        /// </summary>
        public List<WorkMatchingRequest> Items { get; set; } = new List<WorkMatchingRequest>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;        
        /// <summary>
        /// Linh vuc doc quyen
        /// </summary>
        //public MonopolyType Monopoly { get; set; } = 0;    
        /// <summary>
        /// Loai doc quyen
        /// </summary>
        //public MonopolyGroupType MonopolyGroup { get; set; } = 0;        
    }
}
