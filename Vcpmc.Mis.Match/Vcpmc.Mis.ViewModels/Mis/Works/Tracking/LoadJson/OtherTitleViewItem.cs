using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson
{
	public class OtherTitleViewItem
	{
		/// <summary>
		/// Số thứ tự tieu đề
		/// </summary>
		public string titleNo { get; set; } = string.Empty;//1,
		public string titleType { get; set; } = string.Empty;//"OT",
		public string engTitle { get; set; } = string.Empty;//"DROP DAT WHAT THEME OPENING",
		public string localTitle { get; set; } = string.Empty;//
		public string title { get; set; } = string.Empty;//"DROP DAT WHAT THEME OPENING",
		/// <summary>
		/// Mã ngôn ngữ
		/// </summary>
		public string languageCode { get; set; } = string.Empty;//"EN",
		public string languageName { get; set; } = string.Empty;//
		/// <summary>
		/// Mã ngôn ngữ thứ 2
		/// </summary>
		public string subLanguageCode { get; set; } = string.Empty;//"NIL",
		public string subLanguageName { get; set; } = string.Empty;//null,
		public string language { get; set; } = string.Empty;//null,
	}
}
