using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.ViewModels.Mis.Works
{
    public class WorkUpdateRequest
    {
        /// <summary>
        /// Mã hệ thống
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// mã tác phẩm
        /// </summary>
        public string WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Mã tác phẩm
        /// </summary>
        //public string LOCAL_WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác phẩm: YEU CO GAI BAC LIEU; YEU CO GAI BAC LIEU YÊU CÔ GÁI BẠC LIÊU
        /// </summary>
        public string TTL_ENG { get; set; } = string.Empty;
        public string TTL_LOCAL { get; set; } = string.Empty;
        public string ISWC_NO { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        /// <summary>
        /// Tác giả
        /// </summary>
        public string WRITER { get; set; } = string.Empty;
        public string WRITER_LOCAL { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn: THUY TRANG  ,KIM TU LONG  KIM TỬ LONG,VU LINH  VŨ LINH
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái: COMPLETE, INCOMPLETE
        /// </summary>
        public string WK_STATUS { get; set; } = string.Empty;
        public string SOC_NAME { get; set; } = string.Empty;
        public int StarRating { get; set; } = 0;
        /// <summary>
        /// Danh sách tên thường gọi khác
        /// </summary>
        public List<OtherTitle> OtherTitles { get; set; } = new List<OtherTitle>();
        /// <summary>
        /// Danh sách tác giả
        /// </summary>
        public List<InterestedParty> InterestedParties { get; set; } = new List<InterestedParty>();
    }
}
