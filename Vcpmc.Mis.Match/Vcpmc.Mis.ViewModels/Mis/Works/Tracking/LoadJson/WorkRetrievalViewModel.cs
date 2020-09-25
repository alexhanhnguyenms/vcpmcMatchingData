using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
    public class WorkRetrievalViewModel
    {
        public string status { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public WorkRetrievalDetailViewModel _object { get; set; } = new WorkRetrievalDetailViewModel();
    }
}
