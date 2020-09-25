using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Members.Tracking
{
    public class MembershipViewModel
    {
        public string status { get; set; } = string.Empty;// ":"SUCCESS",
        public string message { get; set; } = string.Empty;//":"26=7787",
        public List<MembershipDetailViewModel> _object { get; set; } = new List<MembershipDetailViewModel>();//":
}
}
