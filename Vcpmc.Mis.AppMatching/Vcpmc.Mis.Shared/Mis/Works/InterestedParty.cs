using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Works
{
    public class InterestedParty
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; } = 0;
        /// <summary>
        /// Số thứ tự tác giả
        /// </summary>
        //public int IP_SET_No { get; set; } = 0;
        /// <summary>
        /// Mã tác giả: 2779664
        /// </summary>
        public string IP_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Mã local tác gia: 2761094
        /// </summary>
        //public string LOCAL_IP_INT_NO { get; set; } = string.Empty;        
        /// <summary>
        /// 00560848040
        /// </summary>
        //public string NAME_NO { get; set; } = string.Empty;
        /// <summary>
        /// PP, PA
        /// </summary>
        public string IP_NAMETYPE { get; set; } = "PP";// string.Empty;
        /// <summary>
        /// Quyền đối với tác phẩm: C-nhac, A-loi, CA-nhac va loi
        /// </summary>
        public string IP_WK_ROLE { get; set; } = "CA";// string.Empty;
        /// <summary>
        /// tác giả (không dấu): THANH, SON
        /// </summary>
        public string IP_NAME { get; set; } = string.Empty;
        /// <summary>
        /// Ten local
        /// </summary>
        public string IP_NAME_LOCAL { get; set; } = string.Empty;
        /// <summary>
        /// ten dinh danh quoc te
        /// </summary>
        public string IP_NUMBER { get; set; } = string.Empty;
        /// <summary>
        /// Tác giả(tiếng việt): THANHSƠN
        /// </summary>
        //public string IP_NAME_LOCAL { get; set; } = string.Empty;
        /// <summary>
        /// Quyền PR,Thành viên: NS-khong phải, VCPMC
        /// </summary>
        //public string SOCIETY_PR { get; set; } = string.Empty;
        /// <summary>
        /// Quyền MR,Thành viên: NS-khong phải, VCPMC
        /// </summary>
        //public string SOCIETY_MR { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái: COMPLETE
        /// </summary>
        public string WK_STATUS { get; set; } = "INCOMPLETE";// string.Empty;
        /// <summary>
        /// Quyền biểu diễn
        /// </summary>
        public decimal PER_OWN_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Quyền biểu diễn
        /// </summary>
        public decimal PER_COL_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Quyền phát hành
        /// </summary>
        public decimal MEC_OWN_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Quyền phát hành
        /// </summary>
        public decimal MEC_COL_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Tổng quyền biểu diễn
        /// </summary>
        public decimal SP_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Tổng quyền phát hành
        /// </summary>
        public decimal TOTAL_MEC_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Quyền đồng bộ
        /// </summary>
        public decimal SYN_OWN_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Quyền đồng bộ
        /// </summary>
        public decimal SYN_COL_SHR { get; set; } = 0.0M;
        /// <summary>
        /// Tổ chức uy quyen
        /// </summary>
        public string Society { get; set; } = string.Empty;
        /// <summary>
        /// Mới khởi  tạo: 0, cập nhật tăng lên
        /// </summary>
        public int CountUpdate { get; set; } = 0;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
        //public int StarRating { get; set; } = 0;
        public DateTime LastChoiseAt { get; set; } = DateTime.Now;

    }
}
