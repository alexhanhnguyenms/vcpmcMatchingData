using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.DEmo.LanguageDetection;
using Vcpmc.Mis.Common.detect;
using Vcpmc.Mis.Common.search;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Infrastructure.data;
using Vcpmc.Mis.Shared.masterlist;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace TH.Test.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string axx = "LK SONG QUE";
            DetectLangReturn ir;
            ir = new DetectLangReturn(); 
            ir = FormatVietnamese.DetectVietnamese(axx);
            string afasd = "CHÂU ĐĂNG KHOA (KHOA CHÂU)";
            afasd = ConvertAllToUnicode.ConvertFromComposite(afasd);
            afasd = VnHelper.ConvertToUnSign(afasd);
            //Console.WriteLine(LevenshteinDistance.Compute("tình khúc vàng anh yêu", "tình khúc vàng anh yêu em"));
            Console.WriteLine(LevenshteinDistance.Compute("tình khúc vàng anh yêu", "tình khúc VÀNG anh yêu em"));
            Console.WriteLine(LevenshteinDistance.Compute("tình khúc vàng anh yêu", "tình khúc anh yêu em"));
            Console.WriteLine(LevenshteinDistance.Compute("tình khúc vàng anh yêu", "tình khúc"));
            //Console.WriteLine(LevenshteinDistance.Compute("aunt", "ant"));
            //Console.WriteLine(LevenshteinDistance.Compute("Sam", "Samantha"));
            //Console.WriteLine(LevenshteinDistance.Compute("flomax", "volmax"));
            //return;
            string body = @"{'Items':
                                [{
                                'Id':'5efda2e7e3c0e66276aef5d6',
                                'Asset_ID':'__3TAkpnnhE',
                                'ISRC':'USC4R0303586',
                                'Comp_Asset_ID':'',
                                'C_Title':'ORANGE COLORED SKY',
                                'C_ISWC':'T0701595949',
                                'C_Workcode':'159595',
                                'C_Writers':'WILLIAM STEIN|MILTON DELUGG',
                                'Combined_Claim':'100', 
                                'Mechanical':'100',
                                'Performance':'100',
                                'MONTH':2,
                                'CREATED_AT':1593705798000,
                                'UPDATED_AT':null}],
                            'PageIndex':1,
                            'PageSize':5000,
                            'TotalRecords':1,
                            'PageCount:1
                           }";
            var users = JsonConvert.DeserializeObject<PagedResult<PreclaimViewModel>>(body);
            string test = "ladyTV | Tân Dòng Sông Ly Biệt - Tập 40 | Phim Đài Loan Bất Hủ";
            //ladyTV | Tân Dòng Sông Ly Biệt - Tập 40 | Phim Đài Loan Bất Hu
            //VnHelper.Converter();
            // ladyTV | Tõn Dũng Sụng Ly Biệt - Tập 40 | Phim Đài Loan Bṍt Hủ
            //LADYTV | TÂN DÒNG SÔNG LY BIỆT - TẬP 40 | PHIM ĐÀI LOAN B
            test = VnHelper.ConvertVNIToUnicode(test);
            string adsf = "Lang Van, Làng Văn,Thuy Nga, Thúy Nga, Thuy Nga Production, Thúy Anh, Rang Dong, RANGDONG, RangDong, Rang Dong INC, Mimosa, Kim Ngân, AudioSparx (some), Bai Hat Ru Cho Anh, VNG, Young Hit Young Beat, Elvis Phương, Walt Disney Records (some), LIDIO – SafeMUSE Sounds, Amy Music, SAIGON VAFACO, Saigon Broadcasting Television Network, Kawaiibi, Inédit / Maison des cultures du monde, Người Đẹp Bình Dương, Kiều Thơ Mellow,DONG GIAO PRO, Buda musique (some), TÂN HIỆP PHÁT, Caprice (some), Y Phuong, Horus Music Distribution (some), 1789537 Records DK, Thang Long AV, Nho oi, Lệ Hằng, TN Entertainment, JLP, Ho Entertainment & Events JSC, A Fang Entertainment, iMusician Digital, DONG GIAO, Dang Khoi, Dihavina, Doremi, Future Arts Production, Great Entertainment, Hãng Đĩa Thời Đại (Times Records), Ho Entertainment & Events, Kawaiibi, Kim Ngân, MT Entertainment, Người Đẹp Bình Dương Gold, SÀI GÒN VAFACO, SAI GON VAFACO, SÀI GÒN – VAFACO, SAIGON VAFACO., Thăng Long AV, Vega Media, VNG, TN Entertainment, Dong Dao, Dong Dao 2007, Best Of HKT, Do Bao, Wepro Entertainment, Tuan Trinh Production";
            string lk = "";
            string[] mang = adsf.Split(',');
            foreach (var item in mang)
            {
                lk = lk + ",'" + VnHelper.ConvertToUnSign(item.Trim().ToUpper())+"'";
            }
            string str = "ù";
            bool check = VnHelper.Detect(str);
            //int a = 1;
            
        }

        private static void DetectLanguage(string lang, string[] texts, string[][] pairs = null) {
            try
            {
                LanguageDetector detector;
                detector = new LanguageDetector();
                detector.RandomSeed = 1;
                detector.AddAllLanguages();
                foreach (string text in texts)
                {
                    string langxx = detector.Detect(text);
                    //var x = detector.DetectAll(text);
                }
                //if (pairs != null)
                //{
                //    foreach (string[] pair in pairs)
                //    {
                //        detector = new LanguageDetector();
                //        detector.RandomSeed = 1;
                //        detector.AddLanguages(pair);
                //        detector.AddLanguages(lang);

                //        foreach (string text in texts)
                //        {
                //            string langxx = detector.Detect(text);
                //            var x = detector.DetectAll(text);
                //        }                            
                //    }
                //}
            }
            catch (Exception ex)
            {
                string a = ex.ToString();

                //throw;
            }
        
        }
    }
}
