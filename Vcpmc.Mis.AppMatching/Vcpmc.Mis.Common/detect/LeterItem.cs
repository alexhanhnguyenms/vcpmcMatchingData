using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.detect
{
    public class LeterItem
    {
        public LeterErrorType ErrorType { get; set; } = LeterErrorType.Normal;
        /// <summary>
        /// Điểm nhóm 1: hệ số chiều dài
        /// </summary>
        public int Score1 { get; set; } = 1;
        /// <summary>
        /// Điểm nhóm 2: hệ số chiều dài
        /// </summary>
        public int Score2 { get; set; } = 1;
        /// <summary>
        /// Điểm nhóm 3: hệ số chiều dài
        /// </summary>
        public int Score3 { get; set; } = 1;
        /// <summary>
        /// So nhom
        /// </summary>
        public int CoutGroup { get; set; } = 0;
        //public List<GroupInLeterItem> Items { get; set; } = new List<GroupInLeterItem>();
    }
}
