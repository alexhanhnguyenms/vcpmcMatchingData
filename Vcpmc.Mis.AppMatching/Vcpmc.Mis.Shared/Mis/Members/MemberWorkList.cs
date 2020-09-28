using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Members
{
    public class MemberWorkList: ICloneable
    {
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// Mã bài hát
        /// </summary>
        public string INTERNAL_NO { get; set; } = string.Empty;
        public string WID_NO { get; set; } = string.Empty;
        /// <summary>
        /// Mã local
        /// </summary>
        public string ISWC_NO { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string TITLE { get; set; } = string.Empty;
        public string TITLE2 { get; set; } = string.Empty;
        public string TITLE3 { get; set; } = string.Empty;
        /// <summary>
        /// Thời lượng
        /// </summary>
        public string DURATION { get; set; } = string.Empty;
        /// <summary>
        /// Ngôn ngữ
        /// </summary>
        public string LANGUAGE { get; set; } = string.Empty;
        /// <summary>
        /// Phân loại
        /// </summary>
        public string CATEGORY { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string STATUS { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ biểu diễn
        /// </summary>
        public string ARTISTE { get; set; } = string.Empty;
        public string SET_NO { get; set; } = string.Empty;
        /// <summary>
        /// Loại bút danh
        /// </summary>
        public string NAME_TYPE { get; set; } = string.Empty;
        /// <summary>
        /// Quyền
        /// </summary>
        public string ROLE { get; set; } = string.Empty;
        /// <summary>
        /// Tac gia
        /// </summary>
        public string NAME { get; set; } = string.Empty;
        public string NAME2 { get; set; } = string.Empty;
        public string NAME3 { get; set; } = string.Empty;
        /// <summary>
        /// Bỏ BHMEDIA
        /// </summary>
        //public string NAME2 { get; set; } = string.Empty;
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string SOCIETY { get; set; } = string.Empty;
        // method for cloning 
        public object Clone()
        {
            // return cloned value using 
            // MemberwiseClone() method 
            return MemberwiseClone();
        }
    }
}
