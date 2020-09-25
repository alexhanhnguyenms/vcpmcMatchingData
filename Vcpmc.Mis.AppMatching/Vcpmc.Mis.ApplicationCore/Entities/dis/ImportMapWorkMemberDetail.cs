using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities.dis
{
    public class ImportMapWorkMemberDetail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        //public string User { get; set; } = string.Empty;
        [ForeignKey(nameof(ImportMapWorkMember))]
        public Guid ImportMapWorkMemberId { get; set; }
        public ImportMapWorkMember ImportMapWorkMember { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Ma quoc te
        /// </summary>
        public string Internal { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đều tác phẩm
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề tác phẩm
        /// </summary>
        public string Title2 { get; set; } = string.Empty;
        /// <summary>
        /// Danh sách tác giả
        /// </summary>
        public string Author { get; set; } = string.Empty;
        /// <summary>
        /// Danh sách tác giả 2
        /// </summary>
        public string Author2 { get; set; } = string.Empty;
        /// <summary>
        /// Viet nhac
        /// </summary>
        public string Composer { get; set; } = string.Empty;
        /// <summary>
        /// viet nhac 2
        /// </summary>
        public string Composer2 { get; set; } = string.Empty;
        /// <summary>
        /// Viet loi 
        /// </summary>
        public string Lyrics { get; set; } = string.Empty;
        /// <summary>
        /// Viet loi 2
        /// </summary>
        public string Lyrics2 { get; set; } = string.Empty;
        /// <summary>
        /// Phat hanh
        /// </summary>
        public string Publisher { get; set; } = string.Empty;
        /// <summary>
        /// phat hanh 2
        /// </summary>
        public string Publisher2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghe si
        /// </summary>
        public string Artistes { get; set; } = string.Empty;
        /// <summary>
        /// nghe sy 2
        /// </summary>
        public string Artistes2 { get; set; } = string.Empty;

        /// <summary>
        /// Chuoi trang thai
        /// </summary>
        public string strStatus { get; set; } = string.Empty;
        /// <summary>
        /// Toan bo danh sach tac gia
        /// </summary>
        public string TotalAuthor { get; set; } = string.Empty;
        /// <summary>
        /// Tac pham
        /// </summary>
        //public Work Work { get; set; }
        /// <summary>
        /// Tac gia
        /// </summary>
        //public virtual ICollection<Member> Members { get; set; }
        //public WorkStatus WorkStatus { get; set; }
    }
}
