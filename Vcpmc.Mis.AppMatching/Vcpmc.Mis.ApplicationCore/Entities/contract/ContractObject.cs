using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vcpmc.Mis.ApplicationCore.Entities.contract
{
    public class ContractObject
    {
        /// <summary>
        /// Key tu tang
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// Người tạo
        /// </summary>
        public string User { get; set; } = string.Empty;

        #region Common       
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Khách hàng
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Quận
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Liên hệ, đại diện
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// Mã số thuê
        /// </summary>
        public string TaxCode { get; set; }
        #endregion

        #region Contract
        /// <summary>
        /// Giấy phép
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// Số hợp đồng
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// Lĩnh vực
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Bản hiệu
        /// </summary>
        public string NameSign { get; set; }
        /// <summary>
        /// Ngày hợp đồng
        /// </summary>
        public DateTime ContractTime { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// VAT
        /// </summary>
        public decimal Vat { get; set; } = 0.1M;
        /// <summary>
        /// Giá trị hợp đồng
        /// </summary>
        public decimal Value { get; set; } = 0;
        /// <summary>
        /// Giá trị có VAT
        /// </summary>
        public decimal ValueVAT { get; set; } = 0;
        /// <summary>
        /// Địa chỉ kinh doanh
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Ghi chú tái ký
        /// </summary>
        public string NoteReSigned { get; set; }
        /// <summary>
        /// tái ký
        /// </summary>
        public bool IsReSigned { get; set; }
        #endregion

        #region Postion
        /// <summary>
        /// tần trệt
        /// </summary>
        public string Ground { get; set; }
        /// <summary>
        /// Tầng lửng
        /// </summary>
        public string Badger { get; set; }
        /// <summary>
        /// tầng 1
        /// </summary>
        public string Floor1 { get; set; }
        /// <summary>
        /// Tầng 2
        /// </summary>
        public string Floor2 { get; set; }
        /// <summary>
        /// Tầng 3
        /// </summary>
        public string Floor3 { get; set; }
        /// <summary>
        /// Tầng 4
        /// </summary>
        public string Floor4 { get; set; }
        /// <summary>
        /// Tầng 5
        /// </summary>
        public string Floor5 { get; set; }
        /// <summary>
        /// Tầng 6
        /// </summary>
        public string Floor6 { get; set; }
        /// <summary>
        /// Tầng 7
        /// </summary>
        public string Floor7 { get; set; }
        /// <summary>
        /// Tầng 8
        /// </summary>
        public string Floor8 { get; set; }
        /// <summary>
        /// Tầng 9
        /// </summary>
        public string Floor9 { get; set; }
        /// <summary>
        /// Tầng 10
        /// </summary>
        public string Floor10 { get; set; }
        /// <summary>
        /// Sân thượng
        /// </summary>
        public string Terrace { get; set; }
        //public int Year { get; set; }
        //public int YearQuater { get; set; }
        //public int MonthExpired { get; set; }
        /// <summary>
        /// SL sân thượng
        /// </summary>
        public int CountGround { get; set; }
        /// <summary>
        /// SL tầng lửng
        /// </summary>
        public int CountBadger { get; set; }
        /// <summary>
        /// SL tâng 1
        /// </summary>
        public int CountFloor1 { get; set; }
        /// <summary>
        /// SL tâng 2
        /// </summary>
        public int CountFloor2 { get; set; }
        /// <summary>
        /// SL tâng 3
        /// </summary>
        public int CountFloor3 { get; set; }
        /// <summary>
        /// SL tâng 4
        /// </summary>
        public int CountFloor4 { get; set; }
        /// <summary>
        /// SL tâng 5
        /// </summary>
        public int CountFloor5 { get; set; }
        /// <summary>
        /// SL tâng 6
        /// </summary>
        public int CountFloor6 { get; set; }
        /// <summary>
        /// SL tâng 7
        /// </summary>
        public int CountFloor7 { get; set; }
        /// <summary>
        /// SL tâng 8
        /// </summary>
        public int CountFloor8 { get; set; }
        /// <summary>
        /// SL tâng 9
        /// </summary>
        public int CountFloor9 { get; set; }
        /// <summary>
        /// SL tâng 10
        /// </summary>
        public int CountFloor10 { get; set; }
        /// <summary>
        /// SL tâng sân thượng
        /// </summary>
        public int CountTerrace { get; set; }
        #endregion
    }
}
