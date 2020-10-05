using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure.data;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.Infrastructure
{
    public static class Core
    {      
       /// <summary>
       /// Xác nhận đã đăng nhập hay chưa
       /// </summary>
        public static bool IsLogin { get; set; } = false;
        public static string Password { get; set; } = string.Empty;
        /// <summary>
        /// Tài khoản đăng nhập
        /// </summary>
        public static string User = "";
        /// <summary>
        /// Dia chi API
        /// </summary>
        public static string BaseAddress = "https://localhost:5001";
        /// <summary>
        /// Token hệ thống
        /// </summary>

        public static string Token = "";
        /// <summary>
        /// 
        /// </summary>
        public static string Issuer = "/.,mnbvcxzasdfgh";
        /// <summary>
        /// Key
        /// </summary>
        public static string Key = "http://trangwebkhongtontai.com.vn";
        /// <summary>
        /// Số record một lần gửi lên server
        /// </summary>
        public static int countPerSave = 5000;

        /// <summary>
        /// Thoi gian dang nhap
        /// </summary>
        public static DateTime TimeLogin { get; set; } = DateTime.Now;
        /// <summary>
        /// Thoi gian het han, tinh bang phut cho moi phien dang nhap
        /// KHi hoạt động thì cap nhật
        /// </summary>
        public static double TimeSession { get; internal set; } = 60;
        /// <summary>
        /// Client
        /// </summary>
        public static HttpClient Client { get; set; }
        /// <summary>
        /// Thời gian time out của client
        /// </summary>
        public static TimeSpan TimeoutHttpClient { get; set; } = new TimeSpan(0, 0, 0, 120, 0);
        /// <summary>
        /// Giới hạn một trang hiển thị
        /// </summary>
        public static int LimitDisplayDGV { get; set; } = 10000;
        /// <summary>
        /// Gioi han xuat excel
        /// </summary>
        public static int LimitDisplayExportExcel { get; set; } = 250000;
        /// <summary>
        /// Giới hạn một lần request
        /// </summary>
        //public static int LimitRequest { get; set; } = 10000;
        public static int LimitRequestMonopoly { get; set; } = 10000;
        public static int LimitRequestMemberList { get; set; } = 10000;
        public static int LimitRequestWork { get; set; } = 300;
        public static int LimitRequestPreclaim { get; set; } = 300;
        public static int LimitRequestMasterlist { get; set; } = 300;
        public static int LimitRequestFixParameter { get; set; } = 5000;
        /// <summary>
        /// Goi han mot lan matching
        /// </summary>
        public static int LimitMatchingWorkRequest { get; set; } = 100;
        public static int LimitMatchingPreclaimRequest { get; set; } = 100;
        /// <summary>
        /// Request detect ngon ngu
        /// </summary>
        public static int LimitRequestDetect { get; set; } = 1000;
        public static int LimitRequestUpdate { get; set; } = 5000;
        public static int LimitRequestWorkHistory { get; set; } = 5000;
        /// <summary>
        /// Quyen
        /// </summary>
        public static string Role { get; set; } = string.Empty;
        /// <summary>
        /// Xac dinh la admin
        /// </summary>
        public static bool IsAdmin { get; set; } = false;
        /// <summary>
        /// Chi tiet Quyen
        /// </summary>
        public static RoleViewModel RoleViewModel { get; set; }
        /// <summary>
        /// Gio he thong
        /// </summary>
        public static DateTime Time { get; set; }
        public static int LimitRequestTrackingWork { get; set; } = 300;

        /// <summary>
        /// Duong dan he thong
        /// </summary>
        public static string Path = Directory.GetCurrentDirectory();       

        public static void Innit()
        {
            BaseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();
            LimitDisplayDGV = Int32.Parse(ConfigurationManager.AppSettings["LimitDisplayDGV"]);
            LimitDisplayExportExcel = Int32.Parse(ConfigurationManager.AppSettings["LimitDisplayExportExcel"]);
            //LimitRequest = Int32.Parse(ConfigurationManager.AppSettings["LimitRequest"]);
            LimitRequestMonopoly = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestMonopoly"]);
            LimitRequestMemberList = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestMemberList"]);
            LimitRequestMasterlist = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestMasterlist"]);
            LimitRequestTrackingWork = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestTrackingWork"]);
            LimitRequestFixParameter = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestFixParameter"]);
            LimitRequestWork = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestWork"]);
            LimitRequestPreclaim = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestPreclaim"]);
            LimitRequestUpdate = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestUpdate"]);
            TimeSession = Int32.Parse(ConfigurationManager.AppSettings["TimeSession"]);
            LimitMatchingWorkRequest = Int32.Parse(ConfigurationManager.AppSettings["LimitMatchingWorkRequest"]);
            LimitMatchingPreclaimRequest = Int32.Parse(ConfigurationManager.AppSettings["LimitMatchingPreclaimRequest"]);
            LimitRequestWorkHistory = Int32.Parse(ConfigurationManager.AppSettings["LimitRequestWorkHistory"]);
            int timmeOut = Int32.Parse(ConfigurationManager.AppSettings["TimeoutHttpClient"]);
            TimeoutHttpClient = new TimeSpan(0, 0, 0, timmeOut, 0);
        }
    }
}
