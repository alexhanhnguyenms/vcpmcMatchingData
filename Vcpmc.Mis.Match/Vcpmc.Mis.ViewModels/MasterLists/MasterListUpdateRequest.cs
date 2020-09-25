using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.MasterLists
{
    public class MasterListUpdateRequest
    {
        public string Id { get; set; } = string.Empty;
        public int Year { get; set; } = 2020;
        public int Month { get; set; } = 08;
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// mã
        /// </summary>
        public string ID_youtube { get; set; } = string.Empty;
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
        public string COMP_WRITERS { get; set; } = string.Empty;
        /// <summary>
        /// Mã khách hàng sử dụng
        /// </summary>
        public string COMP_CUSTOM_ID { get; set; } = string.Empty;
        /// <summary>
        /// Số lượng
        /// </summary>
        public int QUANTILE { get; set; } = 0;
        /// <summary>
        /// Mã tác phẩm lookup trong preclaim
        /// </summary>
        public string C_Workcode { get; set; } = string.Empty;
        /// <summary>
        /// Loại report 1
        /// </summary>
        public bool IsReport1 { get; set; } = false;
        /// <summary>
        /// Loại report 2
        /// </summary>
        public bool IsReport2 { get; set; } = false;
        /// <summary>
        /// Loại report 3
        /// </summary>
        public bool IsReport3 { get; set; } = false;
        /// <summary>
        /// Loại report 4
        /// </summary>
        public bool IsReport4 { get; set; } = false;
        /// <summary>
        /// Loại report 5
        /// </summary>
        public bool IsReport5 { get; set; } = false;
        /// <summary>
        /// Loại report 6
        /// </summary>
        public bool IsReport6 { get; set; } = false;
        /// <summary>
        /// Loại report 7
        /// </summary>
        public bool IsReport7 { get; set; } = false;
        /// <summary>
        /// Loại report 8
        /// </summary>
        public bool IsReport8 { get; set; } = false;
        /// <summary>
        /// Loại report 9
        /// </summary>
        public bool IsReport9 { get; set; } = false;
        /// <summary>
        /// Loại report 10
        /// </summary>
        public bool IsReport10 { get; set; } = false;
        /// <summary>
        /// Số điểm bằng dấu tiếng việt, nếu phát hiện dấu là tiếng việt cho 2 điểm
        /// </summary>
        public decimal ScoreDetect1Vn { get; set; } = 0;
        /// <summary>
        /// số điểm bằng thuật toán t, phát hiện cho 0-1 điểm
        /// </summary>
        public decimal ScoreDetect2Algorithm { get; set; } = 0;
        /// <summary>
        /// số điểm bằng detect API, nếu phát hiện cho 2 điểm
        /// </summary>
        public decimal ScoreDetect3API { get; set; } = 0;
        /// <summary>
        /// Detect language
        /// </summary>
        public string DetectLanguage { get; set; } = string.Empty;
        /// <summary>
        /// note when detect vietnamese
        /// </summary>
        public string Note { get; set; } = string.Empty;
        //đoạn này tam bỏ
        //[NotMapped]
        //public ReportType Type { get; set; } = new ReportType();
        /// <summary>
        /// Tổng số điểm
        /// </summary>
        public decimal TotalScore { get; set; } = 0;
        /// <summary>
        /// Đã phát hiện tiếng việt bằng sign
        /// </summary>
        public bool IsDetect1Vn { get; set; } = false;
        /// <summary>
        /// Đã phát hiện tiếng việt bằng thuật toán
        /// </summary>
        public bool IsDetect2Algorithm { get; set; } = false;
        /// <summary>
        /// Đã phát hiện tiếng việt bằng thuật toán
        /// </summary>
        public bool IsDetectAPI { get; set; } = false;
        /// <summary>
        /// Xác định thuộc nhóm việt nam
        /// </summary>
        public bool IsISRC { get; set; }
        /// <summary>
        ///// Thuộc nhóm việt nam
        /// </summary>
        public bool IsLABEL { get; set; }
        /// <summary>
        /// Là tiếng việt
        /// </summary>
        public bool IsVn { get; set; } = false;
        /// <summary>
        /// 1: TITLE2 LIKE '%TAP[ ][0-9]%'
        /// </summary>        
        public int Condition { get; set; } = 0;
        public int Condition2 { get; set; } = 0;
        /// <summary>
        /// Tổng số từ đã phân tích
        /// </summary>
        public int TotalWord { get; set; } = 0;
        /// <summary>
        /// số từ chính xác
        /// </summary>
        public int CorrectWord { get; set; } = 0;
        /// <summary>
        /// Xác định đã convert
        /// </summary>
        public int IsConvertNotSignVN { get; set; } = 0;
        /// <summary>
        /// xác định đã phân tích
        /// </summary>
        public int IsAnalysic { get; set; } = 0;
        public decimal Percents { get; set; } = 0;
    }
}
