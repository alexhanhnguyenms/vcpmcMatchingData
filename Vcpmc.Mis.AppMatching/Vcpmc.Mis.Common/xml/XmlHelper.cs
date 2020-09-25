using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ApplicationCore.Entities;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.master;

namespace Vcpmc.Mis.Common.xml
{
    public static class XmlHelper
    {
        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="FileName"></param>
        public static void DeleteFile(string FileName, bool isAddExtend)
        {
            try
            {
                var path = $"{MasterList.Path}\\{FileName}";
                if (isAddExtend)
                {
                    path = $"{path}.xml";
                }
                // Check if file exists with its full path    
                if (File.Exists(path))
                {
                    // If file found, delete it    
                    File.Delete(path);                   
                }                 
            }
            catch (IOException )
            {
                //TODO
                //MessageBox.Show($"Delete file  config is error, {ex.ToString()}");
            }
        }
        /// <summary>
        /// Ghi File Xml
        /// </summary>
        /// <param name="configFile"></param>
        public static void WriteXML(ConfigFile configFile, bool isAddExtend)
        {            
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(ConfigFile));

            var path = $"{MasterList.Path}\\{configFile.FileName}";
            if(isAddExtend)
            {
                path = $"{path}.xml";
            }
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, configFile);
            file.Close();
        }
        /// <summary>
        /// Read file XML
        /// </summary>
        /// <param name="nameReport"></param>
        /// <returns></returns>
        public static ConfigFile ReadXML(NameReport nameReport, string User)
        {
            try
            {
                var path = $"{MasterList.Path}\\{nameReport.ToString()}.xml";
                if (!File.Exists(path))
                {
                    ConfigFile configFile = new ConfigFile();
                    configFile.FileName = $"{nameReport.ToString()}.xml";
                    configFile.LastTimeEdit = DateTime.Now;
                    configFile.User = User;
                    switch (nameReport)
                    {
                        case NameReport.File1:
                            configFile.configItems = GetDefaultFile1();
                            break;
                        case NameReport.File2:
                            configFile.configItems = GetDefaultFile2();
                            break;
                        case NameReport.File3:
                            configFile.configItems = GetDefaultFile3();
                            break;
                        case NameReport.File3_vi:
                            configFile.configItems = GetDefaultFile3();
                            configFile.viDetectItems = GetDefaultFileVi();
                            break;
                        case NameReport.File3_non_vi:
                            configFile.configItems = GetDefaultFile3();
                            break;
                        case NameReport.File4_vi:
                            configFile.configItems = GetDefaultFile4();
                            configFile.viDetectItems = GetDefaultFileVi();
                            break;
                        case NameReport.File4_non_vi:
                            configFile.configItems = GetDefaultFile4();
                            break;
                        case NameReport.File5_vi:
                            configFile.configItems = GetDefaultFile5();
                            configFile.viDetectItems = GetDefaultFileVi();
                            break;
                        case NameReport.File5_non_vi:
                            configFile.configItems = GetDefaultFile5();                            
                            break;
                        case NameReport.File_repalce:
                            configFile.configItems = GetDefaultFileRepace();
                            break;
                        default:
                            break;
                    }                    
                    WriteXML(configFile,false);
                }
                // Now we can read the serialized book ...  
                System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(ConfigFile));
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                ConfigFile configFileRead = (ConfigFile)reader.Deserialize(file);
                file.Close();
                return configFileRead;      
            }
            catch (Exception )
            {
                return null;
            }
        }


        private static List<ConfigItem> GetDefaultFile1()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
                //1.COMP_CUSTOM_ID
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
                //2.COMP_ISWC
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
            }
            catch (Exception )
            {
                              
            }
            return configItem;
        }
        private static List<ConfigItem> GetDefaultFile2()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
                //1.COMP_CUSTOM_ID
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
                //2.COMP_ISWC
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
            }
            catch (Exception )
            {

            }
            return configItem;
        }
        private static List<ConfigItem> GetDefaultFile3()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
                //1.COMP_CUSTOM_ID
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_CUSTOM_ID.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
                //1.COMP_ISWC
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);               
            }
            catch (Exception )
            {

            }
            return configItem;
        }
        private static List<ConfigItem> GetDefaultFile4()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
                //1.COMP_WRITER
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_WRITERS.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_WRITERS.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
                string filter = "UNKNOWN WRITER (999990), PUBLISHER UNKNOWN/WRITER UNKNOWN, UNKNOWN, na -, N/A, #unknown#, PUBLISHER UNKNOWN WRITER UNKNOWN, null, /, (unknown composer), -, −, .., ?/ ??????; ????????, ?S, _____ ________, _____ _________, UNKNOWN WRITER, UNKNOWN COMPOSER AUTHOR, Unknown Writer, Unknow Composer";
                string[] strFilters = filter.Split(',');
                for (int i = 0; i < strFilters.Length; i++)
                {
                    item = new ConfigItem();
                    item.ColumnName = Column.COMP_WRITERS.ToString();
                    item.Value = strFilters[i].Trim();
                    item.IsCheck = true;
                    configItem.Add(item);
                }
                //2.COMP_ISWC
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "0";
                item.IsCheck = false;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "";
                item.IsCheck = false;
                configItem.Add(item);
                //#NAME
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "#NAME";
                item.IsCheck = false;
                configItem.Add(item);
            }
            catch (Exception )
            {

            }
            return configItem;
        }
        private static List<ConfigItem> GetDefaultFile5()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
                //1.COMP_WRITER
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_WRITERS.ToString();
                item.Value = "0";
                item.IsCheck = false;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_WRITERS.ToString();
                item.Value = "";
                item.IsCheck = false;
                configItem.Add(item);
                //2.COMP_ISWC
                //0
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "0";
                item.IsCheck = false;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "";
                item.IsCheck = false;
                configItem.Add(item);
                //#NAME
                item = new ConfigItem();
                item.ColumnName = Column.COMP_ISWC.ToString();
                item.Value = "#NAME";
                item.IsCheck = false;
                configItem.Add(item);
                //3.ARTIST 
                //0
                item = new ConfigItem();
                item.ColumnName = Column.ARTIST.ToString();
                item.Value = "0";
                item.IsCheck = true;
                configItem.Add(item);
                //blank
                item = new ConfigItem();
                item.ColumnName = Column.ARTIST.ToString();
                item.Value = "";
                item.IsCheck = true;
                configItem.Add(item);
                string filter = "-, 5-, 5#, [:SITD:], $tosba, $NOT, @fr4jola, $lim$, =E:E, #####, ---, ______, 2, unknow, Unknow, Unknown, unknown, Unknown performer";
                string[] strFilters = filter.Split(',');
                for (int i = 0; i < strFilters.Length; i++)
                {
                    item = new ConfigItem();
                    item.ColumnName = Column.ARTIST.ToString();
                    item.Value = strFilters[i].Trim();
                    item.IsCheck = true;
                    configItem.Add(item);
                }                
            }
            catch (Exception )
            {

            }
            return configItem;
        }
        private static List<ViDetectItem> GetDefaultFileVi()
        {
            List<ViDetectItem> configItem = new List<ViDetectItem>();
            ViDetectItem item;
            try
            {
                //1.ISRC               
                string filterISRC = "VN,  US, VG, QM, CI, FR, GB, ES, BG, CA, DE, IT, HK, QZ, NO, RU, SE, TC, TW, CH";
                string[] strFilterISRCs = filterISRC.Split(',');
                for (int i = 0; i < strFilterISRCs.Length; i++)
                {
                    item = new ViDetectItem();
                    item.ColumnName = Column.ISRC.ToString();
                    item.Value = strFilterISRCs[i].Trim();
                    item.IsCheck = true;
                    item.Order = 1;
                    configItem.Add(item);
                }
                // 1.LABEL              
                string filterLABEL = "Lang Van, Làng Văn,Thuy Nga, Thúy Nga, Thuy Nga Production, Thúy Anh, Rang Dong, RANGDONG, RangDong, Rang Dong INC, Mimosa, Kim Ngân, AudioSparx (some), Bai Hat Ru Cho Anh, VNG, Young Hit Young Beat, Elvis Phương, Walt Disney Records (some), LIDIO – SafeMUSE Sounds, Amy Music, SAIGON VAFACO, Saigon Broadcasting Television Network, Kawaiibi, Inédit / Maison des cultures du monde, Người Đẹp Bình Dương, Kiều Thơ Mellow,DONG GIAO PRO, Buda musique (some), TÂN HIỆP PHÁT, Caprice (some), Y Phuong, Horus Music Distribution (some), 1789537 Records DK, Thang Long AV, Nho oi, Lệ Hằng, TN Entertainment, JLP, Ho Entertainment & Events JSC, A Fang Entertainment, iMusician Digital, DONG GIAO, Dang Khoi, Dihavina, Doremi, Future Arts Production, Great Entertainment, Hãng Đĩa Thời Đại (Times Records), Ho Entertainment & Events, Kawaiibi, Kim Ngân, MT Entertainment, Người Đẹp Bình Dương Gold, SÀI GÒN VAFACO, SAI GON VAFACO, SÀI GÒN – VAFACO, SAIGON VAFACO., Thăng Long AV, Vega Media, VNG, TN Entertainment, Dong Dao, Dong Dao 2007, Best Of HKT, Do Bao, Wepro Entertainment, Tuan Trinh Production";
                string[] strFilterLABELs = filterLABEL.Split(',');
                for (int i = 0; i < strFilterLABELs.Length; i++)
                {
                    item = new ViDetectItem();
                    item.ColumnName = Column.LABEL.ToString();
                    item.Value = strFilterLABELs[i].Trim();
                    item.IsCheck = true;
                    item.Order = 2;
                    configItem.Add(item);
                }
                //1.Title                
                item = new ViDetectItem();
                item.ColumnName = Column.TITLE.ToString();
                item.Value = "(vietnamese)";
                item.IsCheck = true;
                item.Order = 1;
                configItem.Add(item);
            }
            catch (Exception )
            {

            }
            return configItem;
        }
        /// <summary>
        /// Replace charactor
        /// </summary>
        /// <returns></returns>
        private static List<ConfigItem> GetDefaultFileRepace()
        {
            List<ConfigItem> configItem = new List<ConfigItem>();
            ConfigItem item;
            try
            {
              
                string remove = "GUITAR SOLO,SOLO,LIVESHOW,LIVESTREAM,LIVESTREAMS,LIVE,EXTENDED,VERSION,AUDIO,DEMO,FIX HD,SINGLE,EDIT,OFFICIAL MV,OFFICIAL,MV,M/V,MUSIC,VIDEO,ORIGINAL MIX,ORIGINAL REMIX,EDM REMIX,VCLIP,CLIP,HIT,REMIX,DANCE,ALBUM,TV VERSION,LYRICS,LYRIC,MIX,ACOUSTIC,COVER,VIDEO,MP3,KARAOKE,BELERO,TV,(,),\\,/,[,],.,-,!,?,#,||,|,?,~,^,´,`";
                string[] removeArr = remove.Split(',');
                for (int i = 0; i < removeArr.Length; i++)
                {
                    item = new ConfigItem();
                    item.OldS = removeArr[i];
                    item.NewS = "";
                    item.IsCheck = true;
                    configItem.Add(item);
                }
                string olds = "VN,'";
                string news = "VIET NAM,";                   
                string[] oldsArr = olds.Split(',');
                string[] newsArr = news.Split(',');
                for (int i = 0; i < oldsArr.Length; i++)
                {
                    item = new ConfigItem();
                    item.OldS = oldsArr[i];
                    item.NewS = newsArr[i];
                    item.IsCheck = true;
                    configItem.Add(item);
                }
                
            }
            catch (Exception )
            {

            }
            return configItem;
        }
    }
}
