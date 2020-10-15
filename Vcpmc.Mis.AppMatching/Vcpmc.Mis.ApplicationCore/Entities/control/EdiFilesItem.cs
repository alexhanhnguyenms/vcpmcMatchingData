using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.ApplicationCore.Entities.control
{
    public class EdiFilesItem: ICloneable
    {
        #region DAU VAO
        /// <summary>
        /// đánh dấu
        /// </summary>
        public int index { get; set; } = 0;
        /// <summary>
        /// So thu thu khi tim duoc
        /// </summary>
        public int seqNo { get; set; } = 0;
        /// <summary>
        /// số thứ tự khi nạp vào
        /// </summary>
        public int NoOfPerf { get; set; } = 0;
        /// <summary>
        /// tiêu đề bài hát (tiếng việt không dấu)
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// tieu de bai hat khong dau, bo nhung ky tu dac biet
        /// </summary>
        public string Title3 { get; set; } = string.Empty;
        /// <summary>
        /// soạn nhạc(la writer vào)
        /// </summary>
        public string Composer { get; set; } = string.Empty;
        /// <summary>
        /// danh sach soạn nhạc(la writer vào)
        /// </summary>
        public List<string> ListComposer { get; set; } = new List<string>();
        /// <summary>
        /// nghệ sỹ
        /// </summary>
        public string Artist { get; set; } = string.Empty;
        /// <summary>
        /// nhà xuất bản
        /// </summary>
        public string Publisher { get; set; } = string.Empty;
        #endregion

        #region THONG TIN TAC PHAM RA
        /// <summary>
        /// Ma quoc te
        /// </summary>
        public string WorkInternalNo { get; set; } = string.Empty;
        public string LocalWorkIntNo { get; set; } = string.Empty;
        /// <summary>
        /// mã vùng
        /// </summary>
        //public string RegionalNo { get; set; } = string.Empty;
        /// <summary>
        /// tiêu đề bài hát (tiếng vietj không dấu, tiếng việt có dấu)
        /// </summary>
        public string WorkTitle { get; set; } = string.Empty;
        /// <summary>
        /// tieu de chinh sua có dấu(ten chính xác)
        /// </summary>
        public string WorkTitle2 { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề chỉnh sửa không dấu(ten chính xác)
        /// </summary>
        public string WorkTitle2Unsign { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach ca si bieu dien
        /// </summary>
        public string WorkArtist { get; set; } = string.Empty;
        /// <summary>
        /// Danh sách soạn nhạc tham gia
        /// </summary>
        public string WorkComposer { get; set; } = string.Empty;        
        /// <summary>
        /// Trang thai xac thuc
        /// </summary>
        public string WorkStatus { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach tieu de xuat ra khac
        /// </summary>
        public List<string> ListOtherTitleOutUnSign { get; set; } = new List<string>();
        /// <summary>
        /// Danh sach tieu de xuat ra khac
        /// </summary>
        public string StrOtherTitleOutUnSign { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach tieu de xuat ra khac, co dau
        /// </summary>
        public List<string> ListOtherTitleOut { get; set; } = new List<string>();
        /// <summary>
        /// Danh sach tieu de xuat ra khac, co dau
        /// </summary>
        public string StrOtherTitleOut { get; set; } = string.Empty;
        #endregion

        #region TAC GIA
        public string IpSetNo { get; set; } = string.Empty;
        /// <summary>
        /// Mã nghệ sĩ, quốc tế
        /// </summary>
        public string IpInNo { get; set; } = string.Empty;
        //LOCAL IP INT NO
        public string LocalIpIntNo { get; set; } = string.Empty;
        /// <summary>
        /// Mã tên nghệ sỹ(number)
        /// </summary>
        public string NameNo { get; set; } = string.Empty;
        /// <summary>
        /// Loại định danh nghệ sĩ: PA: tên thật, PP: bút danh
        /// </summary>
        public string IpNameType { get; set; } = string.Empty;
        /// <summary>
        /// Quyền sở hữu bài hát: CA: nhạc và lời, E: publisher, A: viết ời, C: viết nhạc
        /// </summary>
        public string IpWorkRole { get; set; } = string.Empty;
        /// <summary>
        /// Tên quốc tế load lên
        /// </summary>
        public string IpName { get; set; } = string.Empty;
        /// <summary>
        /// ten tac gia nếu là vn sẽ chuyển lai đúng tên họ
        /// </summary>
        public string IpName2 { get; set; } = string.Empty;
        /// <summary>
        /// Tên tác giả load lên
        /// </summary>
        public string IpNameLocal { get; set; } = string.Empty;
        /// <summary>
        /// ten tac gia local sau khi tính toán
        /// </summary>
        public string IpNameLocal2 { get; set; } = string.Empty;
        /// <summary>
        /// Tổ chức uỷ quyền(load)
        /// </summary>
        public string Society { get; set; } = string.Empty;
        /// <summary>
        /// danh sách tổ chức uỷ quyền, cộng gộp theo danh sách tác giả nếu có
        /// </summary>
        public string Society2 { get; set; } = string.Empty;
        public string SpName { get; set; } = string.Empty;
        #endregion
        
        /// <summary>
        /// Danh sach tac gia nhom
        /// </summary>
        public string GroupWriter { get; set; } = string.Empty;
        /// <summary>
        /// Danh sach tac gia nhac nhom
        /// </summary>
        public string GroupComposer { get; set; } = string.Empty;

        /// <summary>
        /// Danh sachs tacs giar loi nhom
        /// </summary>
        public string GroupLyrics { get; set; } = string.Empty;

        /// <summary>
        /// Nhom nha xuat ban
        /// </summary>
        public string GroupPublisher { get; set; } = string.Empty;

        public List<string> ListGroupWriterCode { get; set; } = new List<string>();
        public List<string> ListGroupWriterName { get; set; } = new List<string>();
        public List<string> ListGroupComposerCode { get; set; } = new List<string>();
        public List<string> ListGroupLyricsCode { get; set; } = new List<string>();
        public List<string> ListGroupPublisherCode { get; set; } = new List<string>();
        /// <summary>
        /// Danh sach tac gia(ten co dau) vo ma la key
        /// </summary>
        public Dictionary<string, string> DicMember { get; set; } = new Dictionary<string, string>();
        public List<InterestedParty> ListInterestedParty { get; set; } = new List<InterestedParty>();
        /// <summary>
        /// Khong phai thanh vien
        /// </summary>
        public string NonMember { get; set; } = string.Empty;


        #region TY LE
        /// <summary>
        /// Quyền sở hữu cá nhân
        /// </summary>
        public decimal PerOwnShr { get; set; } = 0;
        /// <summary>
        /// Quyền dong sở hữu
        /// </summary>
        public decimal PerColShr { get; set; } = 0;
        /// <summary>
        /// Quyền biểu biểu diễn sở hữu
        /// </summary>
        public decimal MecOwnShr { get; set; } = 0;
        /// <summary>
        /// Quyền đồng biểu diễn
        /// </summary>
        public decimal MecColShr { get; set; } = 0;
        public decimal SpShr { get; set; } = 0;
        /// <summary>
        /// Tổng biểu diễn
        /// </summary>
        public decimal TotalMecShr { get; set; } = 0;
        public decimal SynOwnShr { get; set; } = 0;
        public decimal SynColShr { get; set; } = 0;
        //public bool IsDuplicate { get; set; } = false;
        //public int CountDuplicate { get; set; } = 0;
        //public string Note { get; set; } = string.Empty;      
        #endregion

        #region doc quyen
        /// <summary>
        /// Doc quyen tác phẩm
        /// </summary>
        public bool IsWorkMonopoly { get; set; } = false;
        /// <summary>
        /// Lĩnh vực độc quyền tác phẩm
        /// </summary>
        public string WorkFields { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chu độc quyền tác phẩm
        /// </summary>
        public string WorkMonopolyNote { get; set; } = string.Empty;
        /// <summary>
        ///  Doc quyen tác giả
        /// </summary>
        public bool IsMemberMonopoly { get; set; } = false;
        /// <summary>
        /// Lĩnh vực độc quyền tác giả
        /// </summary>
        public string MemberFields { get; set; } = string.Empty;
        /// <summary>
        /// Ghi chú độc quyền tác phẩm
        /// </summary>
        public string MemberMonopolyNote { get; set; } = string.Empty;
        #endregion

        #region so sanh
        /// <summary>
        /// so sanh writer và tile
        /// </summary>
        public bool IscheckCompareTitleAndWriter { get; set; } = true;
        /// <summary>
        /// Só sanh title
        /// </summary>
        public bool IscheckCompareTitle { get; set; } = true;
        /// <summary>
        /// so sanh writer
        /// </summary>
        public bool IscheckCompareWriter { get; set; } = true;
        /// <summary>
        /// So sanh thông tin đâu vào và ra
        /// </summary>
        public string MesssageCompareTitleAndWriter { get; set; } = string.Empty;
        /// <summary>
        /// Số tác giả matching
        /// </summary>
        public int CountMatchWriter { get; set; } = 0;
        /// <summary>
        /// Tổng số tác giả
        /// </summary>
        public int TotalWriter { get; set; } = 0;
        /// <summary>
        /// Vùng miền của thành viên VCPMC
        /// </summary>
        public string VcpmcRegion { get; set; } = string.Empty;
        #endregion



        //public string RegionalNo { get; set; } = string.Empty;



        // method for cloning 
        public object Clone()
        {
            // return cloned value using 
            // MemberwiseClone() method 
            return MemberwiseClone();
        }
    }
}
