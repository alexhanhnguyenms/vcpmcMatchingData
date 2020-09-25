
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
    public class WorkRetrievalStatusViewModel
    {
        public string status { get; set; } = string.Empty;//SUCCESS
        public string message { get; set; } = string.Empty;//0dc4f2bd-fccb-4145-86dd-9ba296fefd1e
        public WorkRetrievalStatusDetailViewModel _object { get; set; } = new WorkRetrievalStatusDetailViewModel();        
    }
}
