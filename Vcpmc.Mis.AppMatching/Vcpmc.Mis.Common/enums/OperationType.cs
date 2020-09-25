using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.enums
{
    public enum OperationType
    {
        //load tu excel
        LoadExcel,
        //load db
        LoadDB,
        /// <summary>
        /// luu db
        /// </summary>
        SaveDatabase,
        /// <summary>
        /// Chỉ update
        /// </summary>
        UpdateDatabase,
        /// <summary>
        /// loc
        /// </summary>
        Filter,
        /// <summary>
        /// Tìm kiếm thông tin
        /// </summary>
        Lookup,
        /// <summary>
        /// Load du lieu
        /// </summary>
        LoadReport,
        /// <summary>
        /// In mo bill
        /// </summary>
        PrinterBill,
        /// <summary>
        /// In tat ca bill
        /// </summary>
        PrinterListBill,
        /// <summary>
        /// Xuat Excel
        /// </summary>
        ExportToExcel,
        /// <summary>
        /// Xuat du lieu
        /// </summary>
        ExportData,
        /// <summary>
        /// save All Pdf
        /// </summary>
        SaveAllPdf,
        /// <summary>
        /// Lưu Excel
        /// </summary>
        SaveExcel,
        /// <summary>
        /// Lưu CSV
        /// </summary>
        SaveCsv,
        /// <summary>
        /// Tạo report
        /// </summary>
        CreateMasterListReporting,
        /// <summary>
        /// Test ngôn ngữ
        /// </summary>
        TestDetectLanguage,
        /// <summary>
        /// Phát hiện tiếng việt bằng thuật toán
        /// </summary>
        DetectLanguageByAlgorithm,
        /// <summary>
        /// Phat hien ngon tieng viet bang API
        /// </summary>
        DetectLanguageByAPI,
        /// <summary>
        /// Phân tích dữ liệu masterlist
        /// </summary>
        AnalysicMaterlistData,
        /// <summary>
        /// chuyen sang tieng viet khong dau
        /// </summary>
        ConvertNotSignVienamese,
        /// <summary>
        /// Cập nhật group master list
        /// </summary>
        UpdateGroupMasterList,
        /// <summary>
        /// Cập nhật các title master list
        /// </summary>
        UpdateExtendMasterlist,
        /// <summary>
        /// CopyDataTemp masterlist
        /// </summary>
        CopyDatatempMasterList,
        /// <summary>
        /// Tạo bảng lưu trữ dữ liệu tạm
        /// </summary>
        CreateYoutubeDatatempFileYYYYMM,
        /// <summary>
        /// Loc file bao cao Edit file tuw mis
        /// </summary>
        FilterDistinct,
        /// <summary>
        /// Lấy dữ liệu từ server
        /// </summary>
        GetDataFromServer,
        /// <summary>
        /// Nạp Json
        /// </summary>
        LoadJson,
        /// <summary>
        /// Nhay den trang
        /// </summary>
        GoPage,
        /// <summary>
        /// Nhay den trang loi
        /// </summary>
        GoPageFailure,
        /// <summary>
        /// Mở tài khoản
        /// </summary>
        UnlockAccount,
        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        LockAccount,
        /// <summary>
        /// reset trangj thai
        /// </summary>
        ResetStatus,
        /// <summary>
        /// Làm mới giao diện
        /// </summary>
        RefreshUI,
        /// <summary>
        /// Dong bo du lieu
        /// </summary>
        SysnData,
        /// <summary>
        /// Thuat toan tim kiem
        /// </summary>
        LevenshteinDistance,
        /// <summary>
        /// Tìm độc quyền
        /// </summary>
        FindMonopoly
    }
}
