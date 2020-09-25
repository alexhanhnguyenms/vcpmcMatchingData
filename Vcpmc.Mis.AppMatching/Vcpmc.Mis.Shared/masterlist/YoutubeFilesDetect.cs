using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.masterlist
{
    public class YoutubeFilesDetect
    {
        public int Code { get; set; }
        public int YoutubeFileId { get; set; }   
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
        /// số điểm bằng thuật toán t, phát hiện cho 0-1 điểm
        /// </summary>
        public decimal ScoreDetect2Algorithm { get; set; } = 0;
        /// <summary>
        /// Đã phát hiện tiếng việt bằng thuật toán
        /// </summary>
        public bool IsDetect2Algorithm { get; set; } = false;
        /// <summary>
        /// Tổng số từ
        /// </summary>
        public int TotalWord { get; set; } = 0;
        /// <summary>
        /// Tổng số từ chính xác
        /// </summary>
        public int CorrectWord { get; set; } = 0;
        public decimal Percents { get; set; } = 0;
        /// <summary>
        /// Check bang API, 100 diem la check thanh cong
        /// </summary>
        public int ScoreDetect3API { get; set; } = 0;
    }
}
