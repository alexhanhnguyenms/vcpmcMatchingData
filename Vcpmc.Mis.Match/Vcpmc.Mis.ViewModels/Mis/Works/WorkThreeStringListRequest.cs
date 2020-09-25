using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkThreeStringListRequest
    {
        public WorkByStringListRequest Item1 { get; set; } = new WorkByStringListRequest();
        public WorkByStringListRequest Item2 { get; set; } = new WorkByStringListRequest();
        public WorkByStringListRequest Item3 { get; set; } = new WorkByStringListRequest();
    }
}
