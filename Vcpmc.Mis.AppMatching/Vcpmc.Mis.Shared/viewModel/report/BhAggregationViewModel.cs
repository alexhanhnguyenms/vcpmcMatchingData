using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Shared.viewModel.report
{
    public class BhAggregationViewModel
    {
        public string Type { get; set; }
        public string BhAuthor { get; set; }
        public int? Count_total { get; set; }
        public decimal? Royalty { get; set; }
        public decimal? Royalty2 { get; set; }       
    }
}
