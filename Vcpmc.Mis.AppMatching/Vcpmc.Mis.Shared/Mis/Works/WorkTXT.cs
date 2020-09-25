using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.Mis.Works
{
    public class WorkTXT
    {
        //WK_INT_NO,TTL_ENG,ISWC_NO,ISRC,WRITER,ARTIST,SOC_NAME
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// Ma tac pham
        /// </summary>
        public string WK_INT_NO { get; set; } = string.Empty;
        /// <summary>
        /// Tieu de bai hat
        /// </summary>
        public string TTL_ENG { get; set; } = string.Empty;
        public string ISWC_NO { get; set; } = string.Empty;
        /// <summary>
        /// Ma tac gia neu co
        /// </summary>
        public string InternalNo { get; set; } = string.Empty;
        /// <summary>
        /// But danh: PA, ten that PA
        /// </summary>
        public string IP_NAME_TYPE { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        /// <summary>
        /// Tac gia
        /// </summary>
        public string WRITER { get; set; } = string.Empty;
        /// <summary>
        /// Nhac
        /// </summary>
        public string WRITER2 { get; set; } = string.Empty;
        /// <summary>
        /// Lời
        /// </summary>
        public string WRITER3 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sỹ
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;
        public string SOC_NAME { get; set; } = string.Empty;
        /// <summary>
        /// Trang thai: COMPLETE, INCOMPLETE
        /// </summary>
        public string STATUS { get; set; } = string.Empty;
        /// <summary>
        /// ROLE: C, A, CA
        /// </summary>
        public string WK_ROLE { get; set; } = string.Empty;
        /// <summary>
        ///  VCPMC, GEMA, GEMA,VCPMC
        /// </summary>
        public string Society { get; set; }
        public string IpNumber { get; set; }
        public string WRITER_LOCAL { get; set; }
        /// <summary>
        /// quyen bieu dien
        /// </summary>
        public decimal PER_OWN_SHR { get; set; }
        /// <summary>
        /// quyen so huu
        /// </summary>
        public decimal MEC_COL_SHR { get; set; }
    }
}
