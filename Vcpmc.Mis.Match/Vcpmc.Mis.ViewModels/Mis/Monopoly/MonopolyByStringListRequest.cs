using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Monopoly
{
    public class MonopolyByStringListRequest
    {
        /// <summary>
        /// List string need compare 
        /// </summary>
        public List<string> Items { get; set; } = new List<string>();
        /// <summary>
        /// Tong so
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// 0: độc quyền tác phẩm, 1: độc quyền tác giả
        /// </summary>
        public int Group { get; set; } = 0;
    }
}
