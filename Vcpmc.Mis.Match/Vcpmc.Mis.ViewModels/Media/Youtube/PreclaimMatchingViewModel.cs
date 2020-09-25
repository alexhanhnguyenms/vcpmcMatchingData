using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Media.Youtube
{
    public class PreclaimMatchingViewModel
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// ID, trung voi ma asset id
        /// </summary>
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát 
        /// </summary>
        public string TITLE { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát 2
        /// </summary>
        //public string TITLE2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sĩ biểu diễn
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sĩ 2
        /// </summary>
        //public string ARTIST2 { get; set; } = string.Empty;
        /// <summary>
        /// Album 
        /// </summary>
        public string ALBUM { get; set; } = string.Empty;
        /// <summary>
        /// Album 2
        /// </summary>
        //public string ALBUM2 { get; set; } = string.Empty;
        /// <summary>
        /// Nhãn, công ty sản xuất
        /// </summary>
        public string LABEL { get; set; } = string.Empty;
        /// <summary>
        /// Nhãn, công ty sản xuất 2
        /// </summary>
        //public string LABEL2 { get; set; } = string.Empty;
        /// <summary>
        /// Tổ tức quốc gia VNRC11100141
        /// </summary>
        public string ISRC { get; set; } = string.Empty;
        /// <summary>
        /// mã soạn nhạc A162754603922949
        /// </summary>
        public string COMP_ID { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề soàn nhạc
        /// </summary>
        public string COMP_TITLE { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string COMP_ISWC { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string COMP_WRITERS { get; set; } = string.Empty;
        /// <summary>
        /// Mã tác giả
        /// </summary>
        public string COMP_CUSTOM_ID { get; set; } = string.Empty;
        /// <summary>
        /// Ty le danh gia
        /// </summary>
        public string QUANTILE { get; set; } = string.Empty;   
        /// <summary>
        /// Mã tác phẩm
        /// </summary>
        public string WorkCode { get; set; } = string.Empty;      
        /// <summary>
        /// Đã matching
        /// </summary>
        public bool IsMatching { get; set; } = false;          
        /// <summary>
        /// Thành công
        /// </summary>
        public bool IsSuccess { get; set; } = false;          
    }
}
