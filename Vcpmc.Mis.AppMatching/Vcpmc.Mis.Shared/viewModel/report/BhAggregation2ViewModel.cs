using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Shared.viewModel.report
{
    public class BhAggregation2ViewModel
    {
        public string BhAuthor { get; set; }
        public int? sltotalOriginal { get; set; }
        public int? sltotalAuthor { get; set; }
        /// <summary>
        /// SO LUONG LOAI TRU
        /// </summary>
        public int? sltotalExcept { get; set; }
        public int? sltotalBhmedia { get; set; }
        public int? sltotalhold { get; set; }
        public decimal? totalOriginal { get; set; }
        public decimal? totalAuthor { get; set; }
        public decimal? totalBhmedia { get; set; }
        public decimal? totalExcept { get; set; }     
        public decimal? totalhold { get; set; }     
    }
}
