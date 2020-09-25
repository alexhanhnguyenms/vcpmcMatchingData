using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Works
{
    public class WorkTXTRead
    {
        public WorkTXTRead(List<WorkTXT> SuccessList, List<string> FailList)
        {
            this.SuccessList = SuccessList;
            this.FailList = FailList;
        }
        public List<WorkTXT> SuccessList { get; set; } = new List<WorkTXT>();
        public List<string> FailList { get; set; } = new List<string>();

    }
}
