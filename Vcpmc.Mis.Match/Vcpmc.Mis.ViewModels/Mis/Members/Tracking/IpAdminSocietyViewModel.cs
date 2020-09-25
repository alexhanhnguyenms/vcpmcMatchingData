using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Members.Tracking
{
    public class IpAdminSocietyViewModel
	{
        public string paIntNo { get; set; } = string.Empty;//":0,
        public string adminSocCode { get; set; } = string.Empty;//":"246",
        public string adminSocName { get; set; } = string.Empty;//":"VCPMC",
        public string prSocCode { get; set; } = string.Empty;//":"246",
        public string prSocName { get; set; } = string.Empty;//":"VCPMC",
        public string prevPrSocCode1 { get; set; } = string.Empty;//":null,
        public string prevPrSocCode2 { get; set; } = string.Empty;//":null,
        public string prActiveStatus { get; set; } = string.Empty;//":null,
        public string mrSocCode { get; set; } = string.Empty;//":"246",
        public string mrSocName { get; set; } = string.Empty;//":"VCPMC",
        public string prevMrSocCode1 { get; set; } = string.Empty;//":null,
        public string prevMrSocCode2 { get; set; } = string.Empty;//":null,
        public string mrActiveStatus { get; set; } = string.Empty;//":null,
        public string drSocCode { get; set; } = string.Empty;//":"246",
        public string drSocName { get; set; } = string.Empty;//":"",
        public string prevDrSocCode1 { get; set; } = string.Empty;//":null,
        public string prevDrSocCode2 { get; set; } = string.Empty;//":null,
        public string drActiveStatus { get; set; } = string.Empty;//":null,
        public string remarks { get; set; } = string.Empty;//":null,
        public string createdBy { get; set; } = string.Empty;//":null,
        public string creationDate { get; set; } = string.Empty;//":null
    }
}
