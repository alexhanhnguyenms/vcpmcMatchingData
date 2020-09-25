using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
    public class WorkRetrievalDetailViewModel
    {
        public string prominentQuery { get; set; } = string.Empty;
        public List<WorkRetrieval> retrevalList { get; set; } = new List<WorkRetrieval>();
    }
}
