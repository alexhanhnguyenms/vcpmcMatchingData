using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Members.Tracking
{
	public class MembershipDetailViewModel
	{
		public string id { get; set; } = string.Empty;//":"2815934_MO",
		public string localFirstName { get; set; } = string.Empty;//":"",
		public string localLastName { get; set; } = string.Empty;//":"",
		public string engFirstName { get; set; } = string.Empty;//":"HOA",
		public string engLastName { get; set; } = string.Empty;//":"AN DO",
		public string ipiNameNo { get; set; } = string.Empty;//":"00559360818",
		public string ipiBaseNo { get; set; } = string.Empty;//":"I-002795920-0",
		public string ipCreatedSocietyCode { get; set; } = string.Empty;//":null,
		public string ipCreatedSocietyName { get; set; } = string.Empty;//":null,
		public string societyMemberUser { get; set; } = string.Empty;//":false,
		public string name { get; set; } = string.Empty;//":"HOA AN DO",
		public string typeIpCode { get; set; } = string.Empty;//":"",
		public string internalNo { get; set; } = string.Empty;//":2815934,
		public string localinternalNo { get; set; } = string.Empty;//":0,
		public string paIntNo { get; set; } = string.Empty;//":2815934,
		public string nameType { get; set; } = string.Empty;//":"MO",
		public string crossRefNo { get; set; } = string.Empty;//":0,
		public string typeIp { get; set; } = string.Empty;//":"",
		public string admissionDate { get; set; } = string.Empty;//":null,
		public string foundationDate { get; set; } = string.Empty;//":null,
		public string createdBy { get; set; } = string.Empty;//":"",
		public string creationDate { get; set; } = string.Empty;//":null,
		public string amendedBy { get; set; } = string.Empty;//":null,
		public string amendmentDate { get; set; } = string.Empty;//":null,
		public string statusIn { get; set; } = string.Empty;//":0,
		public string memberCname { get; set; } = string.Empty;//":null,
		public string memberCvalidity { get; set; } = string.Empty;//":null,
		public string roleComposer { get; set; } = string.Empty;//":"",
		public string roleLyrist { get; set; } = string.Empty;//":"",
		public string roleArranger { get; set; } = string.Empty;//":"",
		public string deedsOfAssingments { get; set; } = string.Empty;//":null,
		public string ownIpNameType { get; set; } = string.Empty;//":null,
		public string[] activeMemberRoles { get; set; } = new string[] { };//":[],
		public string statusInd { get; set; } = string.Empty;//":null,
		public string memberStatus { get; set; } = string.Empty;//":null,
		public string conflictStatus { get; set; } = string.Empty;//":null,
		public IpAdminSocietyViewModel ipAdminSocietyView { get; set; } = new IpAdminSocietyViewModel();//":
		public string[] otherNameTypeBasicDetails { get; set; } = new string[] { };//":[],
		public string pagination { get; set; } = string.Empty;//":null,
		public string ppInd { get; set; } = string.Empty;//":""
	}
}
