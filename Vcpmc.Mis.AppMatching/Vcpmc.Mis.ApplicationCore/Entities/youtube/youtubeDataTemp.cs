using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vcpmc.Mis.ApplicationCore.Entities.youtube
{
    public class YoutubeDataTemp
    {
        /// <summary>
        /// Mã tăng tự động
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        [ForeignKey(nameof(YoutubeTemp))]
        public int YoutubeTempId { get; set; } = 0;
        public YoutubeTemp YoutubeTemp { get; set; }
        /// <summary>
        /// mã
        /// </summary>
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát
        /// </summary>
        public string TITLE { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát 2
        /// </summary>
        public string TITLE2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sĩ
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sĩ 2
        /// </summary>
        public string ARTIST2 { get; set; } = string.Empty;
        /// <summary>
        /// Album
        /// </summary>
        public string ALBUM { get; set; } = string.Empty;
        /// <summary>
        /// Album 2
        /// </summary>
        public string ALBUM2 { get; set; } = string.Empty;
        /// <summary>
        /// Nhãn, công ty sản xuất
        /// </summary>
        public string LABEL { get; set; } = string.Empty;
        /// <summary>
        /// Nhãn, công ty sản xuất 2
        /// </summary>
        public string LABEL2 { get; set; } = string.Empty;
        /// <summary>
        /// Quốc gia đăng ký và bảo hộ
        /// </summary>
        public string ISRC { get; set; } = string.Empty;
        /// <summary>
        /// Mã bài hát
        /// </summary>
        public string COMP_ID { get; set; } = string.Empty;
        /// <summary>
        /// Tên bài hát
        /// </summary>
        public string COMP_TITLE { get; set; } = string.Empty;
        /// <summary>
        /// Mã bảo hộ
        /// </summary>
        public string COMP_ISWC { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string COMP_WRITERS { get; set; } = string.Empty;
        /// <summary>
        /// Mã khách hàng sử dụng
        /// </summary>
        public string COMP_CUSTOM_ID { get; set; } = string.Empty;
        /// <summary>
        /// Số lượng
        /// </summary>
        public int QUANTILE { get; set; } = 0;
    }
}
