using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.detect
{
    public class GroupInLeterItem
    {
        /// <summary>
        /// len of group
        /// </summary>
        public int Len { get; set; }
        /// <summary>
        /// index Fisrt
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// value of string
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// check format
        /// </summary>
        public bool IsCorrect { get; set; }
        /// <summary>
        /// position
        /// </summary>
        public int Pos { get; set; }
    }
}
