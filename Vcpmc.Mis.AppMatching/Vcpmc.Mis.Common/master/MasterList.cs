using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vcpmc.Mis.ApplicationCore.Entities;
using Vcpmc.Mis.ApplicationCore.Entities.youtube;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.xml;

namespace Vcpmc.Mis.Common.master
{
    public static class MasterList
    {

        #region system  
        public static string Path = "";
        /// <summary>
        /// name of master list file
        /// </summary>
        public static string FileName = "";
        #endregion

        #region Report config
        /// <summary>
        /// file config 1
        /// </summary>
        public static ConfigFile ConfigFile1;
        /// <summary>
        /// file config 2
        /// </summary>
        public static ConfigFile ConfigFile2;
        /// <summary>
        /// file config 3
        /// </summary>
        public static ConfigFile ConfigFile3;
        /// <summary>
        /// 
        /// </summary>
        public static ConfigFile ConfigFile3_vi;
        /// <summary>
        /// 
        /// </summary>
        public static ConfigFile ConfigFile3_non_vi;
        /// <summary>
        /// file config 4 vi
        /// </summary>
        public static ConfigFile ConfigFile4_vi;
        /// <summary>
        /// file config 4 - none vi
        /// </summary>
        public static ConfigFile ConfigFile4_non_vi;
        /// <summary>
        /// file config 5 vi
        /// </summary>
        public static ConfigFile ConfigFile5_vi;       
        /// <summary>
        /// file config 5 none vi
        /// </summary>
        public static ConfigFile ConfigFile5_non_vi;
        /// <summary>
        /// Replace Charactor
        /// </summary>
        public static ConfigFile ConfigFileReplace;
        #endregion

        #region Data source       
        /// <summary>
        /// master list data
        /// </summary>
        public static List<YoutubeFileItems> YoutubeFileItems { get; set; } = new List<YoutubeFileItems>();       
        #endregion

        #region test
        //static string test = "SÁNG MẮT CHƯA? (MV) | TRÚC NHÂN (#SMC?)";
        public static readonly long TotalReportInfile = 200000;
        #endregion

        #region innit
        /// <summary>
        /// Khởi tạo thông số
        /// </summary>
        public static void Innit(string User,string path)
        {
           Path = path;
            LoadConfig(User);
        }
        /// <summary>
        /// Load file config of report
        /// </summary>
        public static void LoadConfig(string User)
        {
            ConfigFile1 = XmlHelper.ReadXML(NameReport.File1, User);
            ConfigFile2 = XmlHelper.ReadXML(NameReport.File2, User);
            ConfigFile3 = XmlHelper.ReadXML(NameReport.File3, User);
            ConfigFile3_vi = XmlHelper.ReadXML(NameReport.File3_vi, User);
            ConfigFile3_non_vi = XmlHelper.ReadXML(NameReport.File3_non_vi, User);
            ConfigFile4_vi = XmlHelper.ReadXML(NameReport.File4_vi, User);
            ConfigFile4_non_vi = XmlHelper.ReadXML(NameReport.File4_non_vi, User);
            ConfigFile5_vi = XmlHelper.ReadXML(NameReport.File5_vi, User);
            ConfigFile5_non_vi = XmlHelper.ReadXML(NameReport.File5_non_vi, User);
            ConfigFileReplace = XmlHelper.ReadXML(NameReport.File_repalce, User);
        }
        #endregion       

        #region replace
        public static string ReplaceSpecialCharactor(string str)
        {
            if(str.Trim() == string.Empty)
            {
                return str;
            }
            if (str.Contains("~"))
            {
                //int a = 1;
            }
            if (str.Contains("?"))
            {
                //int a = 1;
            }
            if (str.Contains("^"))
            {
                //int a = 1;
            }
            if (str.Contains("?"))
            {
                //int a = 1;
            }
            if (str.Contains("´"))
            {
                //int a = 1;
            }
            if (str.Contains("`"))
            {
                //int a = 1;
            }
            StringBuilder builder = new StringBuilder(str);
            
            for (int i = 0; i < master.MasterList.ConfigFileReplace.configItems.Count; i++)
            {
                if(str.Length == 0)
                {
                    break;
                }  
                str = str.Replace(master.MasterList.ConfigFileReplace.configItems[i].OldS, master.MasterList.ConfigFileReplace.configItems[i].NewS);
            }            
            return str;
        }
        #endregion
    }
}
