using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking
{
    public class WorkTrackingAggregateViewModel
    {
        public int Type { get; set; } = 0;
        public int Year { get; set; } = 2020;
        public int MONTH { get; set; } = 8;
        public long Count { get; set; } = 0;
        public int Serial { get; set; } = 0;
        public string NameType { get; set; } = "Create";
    }
}
