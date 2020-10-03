using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;
using LumenWorks.Framework.IO.Csv;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vcpmc.Mis.ApplicationCore.Entities.youtube;
using Vcpmc.Mis.Common.master;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.Mis.Members;
using Vcpmc.Mis.Shared.Mis.Works;
using Vcpmc.Mis.Shared.work;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.MasterLists;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.Common.csv
{
    /// <summary>
    /// Read CSV file
    /// </summary>
    public class CsvReadHelper
    {
        #region MyRegion
        /// <summary>
        /// Read master list
        /// </summary>
        /// <param name="fullPath"></param>
        public static void ReadCsvMasterList2(string fullPath)
        {
            try
            {
                MasterList.YoutubeFileItems.Clear();
                int fieldCount = 0;
                //int rowCountBefore = 0;
                // open the file "data.csv" which is a CSV file with headers               
                List<YoutubeFileItems> YoutubeFiles = new List<YoutubeFileItems>();
                using (CsvReader csv = new CsvReader(new StreamReader(fullPath), true))
                {
                    fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    //rowCountBefore = csv.Count();
                    YoutubeFileItems item = null;
                    long no = 1;
                    while (csv.ReadNextRecord())
                    {

                        item = new YoutubeFileItems();
                        item.NO = no;
                        item.ID = csv[0].Trim();
                        item.TITLE = csv[1] == null ? string.Empty : csv[1].Trim();
                        item.TITLE2 = VnHelper.ConvertToUnSign(item.TITLE);
                        item.TITLE2 = MasterList.ReplaceSpecialCharactor(item.TITLE2);
                        item.ARTIST = csv[2] == null ? string.Empty : csv[2].Trim();
                        item.ARTIST2 = VnHelper.ConvertToUnSign(item.ARTIST);
                        item.ARTIST2 = MasterList.ReplaceSpecialCharactor(item.ARTIST2);
                        item.ALBUM = csv[3] == null ? string.Empty : csv[3].Trim();
                        item.ALBUM2 = VnHelper.ConvertToUnSign(item.ALBUM);
                        item.ALBUM2 = MasterList.ReplaceSpecialCharactor(item.ALBUM2);
                        item.LABEL = csv[4] == null ? string.Empty : csv[4].Trim();
                        item.LABEL2 = VnHelper.ConvertToUnSign(item.LABEL);
                        item.LABEL2 = MasterList.ReplaceSpecialCharactor(item.LABEL2);
                        item.ISRC = csv[5].Trim();

                        item.COMP_ID = csv[6] == null ? string.Empty : csv[6].Trim();
                        item.COMP_TITLE = csv[7] == null ? string.Empty : csv[7].Trim();
                        item.COMP_ISWC = csv[8] == null ? string.Empty : csv[8].Trim();
                        item.COMP_WRITERS = csv[9] == null ? string.Empty : csv[9].Trim();
                        item.COMP_CUSTOM_ID = csv[10] == null ? string.Empty : csv[10].Trim();
                        item.QUANTILE = csv[11] == null ? 0 : int.Parse(csv[11].Trim());
                        YoutubeFiles.Add(item);
                        no++;
                    }
                }
                var orderByDescendingResult = from s in YoutubeFiles
                                              orderby s.QUANTILE descending, s.COMP_WRITERS descending
                                              select s;
                MasterList.YoutubeFileItems = orderByDescendingResult.ToList();//YoutubeFiles.OrderBy(x=>x.QUANTILE).ToList();               
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        public static List<MasterListViewModel> ReadCsvMasterList(string fullPath, int year, int month)
        {
            List<MasterListViewModel> list = new List<MasterListViewModel>();
            try
            {
               
                int fieldCount = 0;
                //int rowCountBefore = 0;
                // open the file "data.csv" which is a CSV file with headers               
              
                using (CsvReader csv = new CsvReader(new StreamReader(fullPath), true))
                {
                    fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    //rowCountBefore = csv.Count();
                    MasterListViewModel item = null;
                    int no = 1;
                    while (csv.ReadNextRecord())
                    {

                        item = new MasterListViewModel();
                        item.SerialNo = no;
                        item.Year = year;
                        item.Month = month;
                        item.ID_youtube = csv[0].Trim();
                        item.TITLE = csv[1] == null ? string.Empty : csv[1].Trim();                       
                        item.ARTIST = csv[2] == null ? string.Empty : csv[2].Trim();                       
                        item.ALBUM = csv[3] == null ? string.Empty : csv[3].Trim();                        
                        item.LABEL = csv[4] == null ? string.Empty : csv[4].Trim();                       
                        item.ISRC = csv[5].Trim();
                        item.COMP_ID = csv[6] == null ? string.Empty : csv[6].Trim();
                        item.COMP_TITLE = csv[7] == null ? string.Empty : csv[7].Trim();
                        item.COMP_ISWC = csv[8] == null ? string.Empty : csv[8].Trim();
                        item.COMP_WRITERS = csv[9] == null ? string.Empty : csv[9].Trim();
                        item.COMP_CUSTOM_ID = csv[10] == null ? string.Empty : csv[10].Trim();
                        item.QUANTILE = csv[11] == null ? 0 : int.Parse(csv[11].Trim());
                        list.Add(item);
                        no++;
                    }
                }                      
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region Read preaaclaim matching
        public static List<PreclaimMatchingViewModel> ReadCSVPreClaimMatching(string fullPath)
        {
            List<PreclaimMatchingViewModel> list = new List<PreclaimMatchingViewModel>();
            try
            {
               
                PreclaimMatchingViewModel item = null;
                using (CsvReader csv = new CsvReader(new StreamReader(fullPath), true))
                {                    
                    string[] headers = csv.GetFieldHeaders();
                    string[] arrayHeader;
                    if(headers.Length==1)
                    {
                        arrayHeader = headers[0].Split('\t');
                    }
                    string[] row;
                    while (csv.ReadNextRecord())
                    {
                        //25	A357862163768962	Papa	Elvis Phương	Đêm Say	Người Đẹp Bình Dương        Gold	    QM9SX1502168	A825616047368398	PAPA		"Guillaume Soulan
                        //1	    A945915801200003	Circle  In The Sand	    Lynda Trang Đài	Doctor New Wave 9	Làng Văn	QM9SX1504403	A729356834721747	    Circle In The Sand			31439	10
                        string d = csv[0];
                        row = csv[0].Split('\t');
                        if(row!=null&& row.Length>12)
                        {
                            item = new PreclaimMatchingViewModel();
                            item.SerialNo = int.Parse(row[0].Trim());
                            item.ID =  row[1].Trim();
                            item.TITLE = row[2].Trim();
                            item.ARTIST =  row[3].Trim();
                            item.ALBUM = row[4].Trim();

                            item.LABEL = row[5].Trim();
                            item.ISRC = row[6].Trim();
                            item.COMP_ID = row[7].Trim();
                            item.COMP_TITLE = row[8].Trim();
                            item.COMP_ISWC = row[9].Trim();

                            item.COMP_WRITERS = row[10].Trim();
                            item.COMP_CUSTOM_ID = row[11].Trim();
                            item.QUANTILE = row[12].Trim();
                            list.Add(item);
                        }
                        else
                        {
                            //int a = 1;
                        }
                    }
                }                          
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        public static List<PreclaimMatchingViewModel> ReadUnicodePreClaimMatching(string fullPath)
        {           
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }           
            fileStream.Close();
            List<PreclaimMatchingViewModel> list = new List<PreclaimMatchingViewModel>();
            try
            {
                PreclaimMatchingViewModel item = null;
                string[] row;
                for (int i = 1; i < data.Count; i++)
                {                   
                    row = data[i].Replace("\"","").Split('\t');
                    if (row.Length > 12)
                    {
                        item = new PreclaimMatchingViewModel();
                        item.SerialNo = int.Parse(row[0].Trim());
                        item.ID = row[1].Trim();
                        item.TITLE = row[2].Trim();
                        item.ARTIST = row[3].Trim();
                        item.ALBUM = row[4].Trim();

                        item.LABEL = row[5].Trim();
                        item.ISRC = row[6].Trim();
                        item.COMP_ID = row[7].Trim();
                        item.COMP_TITLE = row[8].Trim();
                        item.COMP_ISWC = row[9].Trim();

                        item.COMP_WRITERS = row[10].Trim();
                        item.COMP_CUSTOM_ID = row[11].Trim();
                        item.QUANTILE = row[12].Trim();
                        list.Add(item);
                    }
                    else
                    {
                        //int a = 1;
                    }

                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region ReadUnicodeWorkMatching
        public static List<WorkMatchingViewModel> ReadUnicodeWorkMatching(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<WorkMatchingViewModel> list = new List<WorkMatchingViewModel>();
            string aa = "";
            try
            {
                WorkMatchingViewModel item = null;
                string[] row;
                string[] writer =null;
                string[] artist =null;
                for (int i = 1; i < data.Count; i++)
                {
                    aa = data[i];
                    //STT	MA	TAC PHAM	NHAC si	NGUOI BIEU DIEN
                    row = data[i].Replace("\"", "").Split('\t');
                    item = new WorkMatchingViewModel();
                    if (row.Length > 0)
                    {
                        item.SerialNo = int.Parse(row[0].Trim());
                    }
                    if(row.Length > 1)
                    {
                        item.WorkCode = ConvertAllToUnicode.ConvertFromComposite(row[1].Trim());
                    }
                    if (row.Length > 2)
                    {
                        item.Title = ConvertAllToUnicode.ConvertFromComposite(row[2].Trim());
                        item.Title2 = VnHelper.ConvertToUnSign(item.Title).Replace("(", "").Replace(")", "").ToUpper();
                    }
                    if (row.Length > 3)
                    {
                        item.Writer = ConvertAllToUnicode.ConvertFromComposite(row[3].Trim());
                        item.Writer2 = VnHelper.ConvertToUnSign(item.Writer).Replace("(", "").Replace(")", "").ToUpper();
                        writer = item.Writer2.Split(',');
                        for (int ik = 0; ik < writer.Length; ik++)
                        {
                            if (writer[ik].Trim() != string.Empty)
                            {
                                item.ListWriter2.Add(writer[ik].Trim());
                            }
                        }
                    }
                    if (row.Length > 4)
                    {
                        item.Artist = ConvertAllToUnicode.ConvertFromComposite(row[4].Trim());
                        item.Artist2 = VnHelper.ConvertToUnSign(item.Artist).ToUpper();

                        artist = item.Artist2.Split(',');
                        for (int ik = 0; ik < artist.Length; ik++)
                        {
                            if (artist[ik].Trim() != string.Empty)
                            {
                                item.ListArtist2.Add(artist[ik].Trim());
                            }
                        }
                    }
                    list.Add(item);


                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region VCPMCworkcodeAll
        public static List<VcpmcAllWork> ReadVCPMCAllWorkcode(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<VcpmcAllWork> list = new List<VcpmcAllWork>();
            string aa = "";
            try
            {
                VcpmcAllWork item = null;
                string[] row;
                //string[] writer = null;
                //string[] artist = null;
                for (int i = 1; i < data.Count; i++)
                {
                    aa = data[i];
                    //STT	MA	TAC PHAM	NHAC si	NGUOI BIEU DIEN
                    row = data[i].Replace("\"", "").Split('\t');
                    item = new VcpmcAllWork();
                    if (row.Length == 2)
                    {
                        item.OldCode = row[0];
                        item.NewCode = row[1];                       
                    }
                    //else
                    //{
                    //    int a = 1;
                    //}   
                    list.Add(item);

                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region vcpmc region
        public static List<VcpmcInfo> ReadVCPMCInfo(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<VcpmcInfo> list = new List<VcpmcInfo>();
            string aa = "";
            try
            {
                VcpmcInfo item = null;
                string[] row;                
                for (int i = 1; i < data.Count; i++)
                {
                    aa = data[i];                  
                    row = data[i].Replace("\"", "").Split('\t');
                    item = new VcpmcInfo();
                    if (row.Length == 2)
                    {
                        item.IpNumWithNameType = row[0];
                        item.Region = row[1];
                    }                   
                    list.Add(item);

                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region ReadUnicodeWorkTXT
        /// <summary>
        /// Mau an do, tac gia nuoc ngoai(mau cu)
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static WorkTXTRead ReadUnicodeWorkTXT(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<WorkTXT> list = new List<WorkTXT>();
            List<string> listErr = new List<string>();
            try
            {
                WorkTXT item = null;
                string[] row;
                int ser = 0;
                string strline;
                string[] arrArtist = null;
                string[] arrWriter = null;
                for (int i = 1; i < data.Count; i++)
                {
                    List<string> listItem = new List<string>();
                    //3,"ARABESQUE DEUXIEME NO 2",T0700000027,,"LOPEZ VINCENT",,INTER
                    //3,"ARABESQUE DEUXIEME NO 2",T0700000027,"","LOPEZ VINCENT","",INTER
                    //",=>",";,"=>","
                    strline = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(data[i].Trim().ToUpper()));
                    //strline = $"\"{ strline}";
                    //strline = strline.Replace(",,", ",\"\",");
                    //strline = strline.Replace("\",", "\",\"");
                    //strline = strline.Replace(",\"", "\",\"");
                    //row = data[i].Replace("\"", "").Split(',');
                    string xagain = strline;
                    int first, second;
                    string x, y;
                    string[] arrF;
                    string[] arrAgain;
                    bool isError = false;
                    while (xagain.Length>0)
                    {
                        isError = true;
                        first = xagain.IndexOf("\"");
                        if(first>=0)
                        {
                            if(first==0)
                            {
                                xagain = xagain.Substring(first + 1, xagain.Length - first - 1);//bo dau phay dau tien
                            }
                            else
                            {
                                x = xagain.Substring(0, first - 1);
                                arrF = x.Split(',');
                                for (int posF = 0; posF < arrF.Length; posF++)
                                {
                                    listItem.Add(arrF[posF]);
                                }
                                xagain = xagain.Substring(first + 1, xagain.Length - first - 1);//bo dau phay dau tien
                            }                            
                            second = xagain.IndexOf("\"");
                            if(second >= 0)
                            {
                                y = xagain.Substring(0, second);
                                listItem.Add(y);
                                if(xagain.Length < second + 2)
                                {
                                    isError = false;
                                    break;
                                }
                                xagain = xagain.Substring(second+2, xagain.Length - second-2);
                            }
                            else
                            {
                                isError = false;
                                //loi: khong dung format
                                break;
                            }
                        }
                        else
                        {
                            arrAgain = xagain.Split(',');
                            for (int posAg = 0; posAg < arrAgain.Length; posAg++)
                            {
                                listItem.Add(arrAgain[posAg]);
                            }
                            break;
                        }
                        xagain = xagain.Trim();
                    }
                    row = listItem.ToArray();
                    if (!isError)
                    {
                        listErr.Add(strline);
                    }
                    else if (row.Length!=7)
                    {
                        listErr.Add(strline);
                    }
                    else
                    {
                        ser++;
                        //WK_INT_NO,TTL_ENG,ISWC_NO,ISRC,WRITER,ARTIST,SOC_NAME
                        item = new WorkTXT();
                        item.SerialNo = ser;
                        item.WK_INT_NO = row[0].Trim();
                        item.TTL_ENG = row[1].Trim();
                        item.ISWC_NO = row[2].Trim();
                        item.ISRC = row[3].Trim();
                        item.WRITER = row[4].Trim();
                        arrWriter = item.WRITER.Split(',');
                        item.WRITER = string.Empty;                       
                        for (int mk = 0; mk < arrWriter.Length; mk++)
                        {
                            if (arrWriter[mk].Trim() != string.Empty)
                            {
                                item.WRITER += arrWriter[mk].Trim() + ",";
                            }
                        }
                        if (item.WRITER.Length > 0)
                        {
                            item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);
                        }
                        item.WRITER_LOCAL = item.WRITER;
                        item.ARTIST = row[5].Trim();
                        arrArtist = item.ARTIST.Split(',');
                        item.ARTIST = string.Empty;
                        for (int mk = 0; mk < arrArtist.Length; mk++)
                        {
                            if (arrArtist[mk].Trim() != string.Empty)
                            {
                                item.ARTIST += arrArtist[mk].Trim() + ",";
                            }
                        }
                        if (item.ARTIST.Length > 0)
                        {
                            item.ARTIST = item.ARTIST.Substring(0, item.ARTIST.Length - 1);
                        }
                        item.STATUS = "UNIDENTIFIED";
                        item.SOC_NAME = row[6].Trim();
                        list.Add(item);
                    } 
                }                
            }
            catch (Exception)
            {
                list = null;
            }
            return new WorkTXTRead(list,listErr);
        }
        /// <summary>
        /// Mau an do, tac gia nuoc ngoai 3(17 trieu)
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static WorkTXTRead ReadUnicodeWorkTXT3(string fullPath)
        {           
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<WorkTXT> list = new List<WorkTXT>();
            List<string> listErr = new List<string>();
            WorkTXT item = null;
            string[] row;
            string strline;
            string[] arrArtist = null;
            string[] arrWriter = null;
            int ser = 0;
            try
            {
                
                for (int i = 1; i < data.Count; i++)
                {
                    ser++;
                    strline = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(data[i].Trim().ToUpper()));
                    row = strline.Replace("\"", "").Split('\t');
                    //if(row[4].Trim()=="")
                    //{
                    //    int a = 1;
                    //}    
                    if (row.Length != 7)
                    {
                        if(row.Length > 8)
                        {
                            //int a = 1;
                        }
                        //if(row.Length ==8 )
                        //{

                        //}
                        //else
                        //{
                        //    listErr.Add(strline);
                        //}
                        item = new WorkTXT();
                        item.SerialNo = ser;
                        item.WK_INT_NO = row[0].Trim();
                        item.TTL_ENG = row[1].Trim();
                        item.TTL_LOCAL = row[1].Trim();
                        item.ISWC_NO = row[2].Trim();
                        item.ISRC = row[3].Trim();
                        item.WRITER = row[4].Trim();
                        arrWriter = item.WRITER.Split(',');
                        item.WRITER = string.Empty;
                        for (int mk = 0; mk < arrWriter.Length; mk++)
                        {
                            if (arrWriter[mk].Trim() != string.Empty)
                            {
                                item.WRITER += arrWriter[mk].Trim() + ",";
                            }
                        }
                        if (item.WRITER.Length > 0)
                        {
                            item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);
                        }
                        item.WRITER_LOCAL = item.WRITER;
                        item.WRITER2 = string.Empty;
                        item.WRITER3 = string.Empty;
                        for (int ik = 5; ik < row.Length - 1; ik++)
                        {
                            item.ARTIST += row[ik].Trim();
                        }
                        arrArtist = item.ARTIST.Split(',');
                        item.ARTIST = string.Empty;
                        for (int mk = 0; mk < arrArtist.Length; mk++)
                        {
                            if(arrArtist[mk].Trim()!=string.Empty)
                            {
                                item.ARTIST += arrArtist[mk].Trim() + ",";
                            }                            
                        }
                        if (item.ARTIST.Length > 0)
                        {
                            item.ARTIST = item.ARTIST.Substring(0, item.ARTIST.Length - 1);
                        }
                        item.STATUS = "UNIDENTIFIED";
                        item.SOC_NAME = row[row.Length - 1].Trim();
                        if(item.WRITER==string.Empty)
                        {
                            listErr.Add(strline);
                        }   
                        else
                        {
                            list.Add(item);
                        }                        
                    }
                    else
                    {
                        //WK_INT_NO	TTL_ENGLISH	ISWC_NO	ISRC	WRITTER	ARTIST	SOC_NAME
                        item = new WorkTXT();
                        item.SerialNo = ser;
                        item.WK_INT_NO = row[0].Trim();
                        item.TTL_ENG = row[1].Trim();
                        item.ISWC_NO = row[2].Trim();
                        item.ISRC = row[3].Trim();
                        item.WRITER = row[4].Trim();
                        arrWriter = item.WRITER.Split(',');
                        item.WRITER = string.Empty;
                        for (int mk = 0; mk < arrWriter.Length; mk++)
                        {
                            if (arrWriter[mk].Trim() != string.Empty)
                            {
                                item.WRITER += arrWriter[mk].Trim() + ",";
                            }
                        }
                        if (item.WRITER.Length > 0)
                        {
                            item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);
                        }
                        item.WRITER_LOCAL = item.WRITER;
                        item.WRITER2 = string.Empty;
                        item.WRITER3 = string.Empty;
                        item.ARTIST = row[5].Trim();
                        arrArtist = item.ARTIST.Split(',');
                        item.ARTIST = string.Empty;
                        for (int mk = 0; mk < arrArtist.Length; mk++)
                        {
                            if (arrArtist[mk].Trim() != string.Empty)
                            {
                                item.ARTIST += arrArtist[mk].Trim() + ",";
                            }
                        }
                        if (item.ARTIST.Length > 0)
                        {
                            item.ARTIST = item.ARTIST.Substring(0, item.ARTIST.Length - 1);
                        }
                        item.STATUS = "UNIDENTIFIED";
                        item.SOC_NAME = row[6].Trim();
                        if (item.WRITER == string.Empty)
                        {
                            listErr.Add(strline);
                        }
                        else
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                list = null;
            }
            return new WorkTXTRead(list, listErr);
        }
        /// <summary>
        /// Mau chung
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static WorkTXTRead ReadUnicodeWorkTXTComon(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<WorkTXT> list = new List<WorkTXT>();
            List<string> listErr = new List<string>();
            try
            {
                WorkTXT item = null;
                string[] row;               
                string strline;
                string[] arrArtist = null;
                string[] arrWriter = null;
                for (int i = 1; i < data.Count; i++)
                {                    
                    strline = VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(data[i].Trim().ToUpper()));                    
                    row = strline.Replace("\"", "").Split('\t');        
                    if(row.Length!=11)
                    {
                        if(row.Length>11)
                        {
                            //STT	Code	Tác Phẩm	Tác Giả	Nhạc	Lời	Ca Sỹ	Status
                            item = new WorkTXT();
                            item.SerialNo = int.Parse(row[0].Trim());
                            item.WK_INT_NO = row[1].Trim();
                            item.TTL_ENG = row[2].Trim();
                            item.ISWC_NO = row[3].Trim();
                            item.ISRC = row[4].Trim();
                            item.WRITER = row[5].Trim();
                            arrWriter = item.WRITER.Split(',');
                            item.WRITER = string.Empty;
                            for (int mk = 0; mk < arrWriter.Length; mk++)
                            {
                                if (arrWriter[mk].Trim() != string.Empty)
                                {
                                    item.WRITER += arrWriter[mk].Trim() + ",";
                                }
                            }
                            if (item.WRITER.Length > 0)
                            {
                                item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);
                            }
                            item.WRITER2 = row[6].Trim();
                            item.WRITER3 = row[7].Trim();
                            for (int ik = 8; ik < row.Length-2; ik++)
                            {
                                item.ARTIST += row[ik].Trim();
                            }
                            arrArtist = item.ARTIST.Split(',');
                            item.ARTIST = string.Empty;
                            for (int mk = 0; mk < arrArtist.Length; mk++)
                            {
                                if (arrArtist[mk].Trim() != string.Empty)
                                {
                                    item.ARTIST += arrArtist[mk].Trim() + ",";
                                }
                            }
                            item.STATUS = row[row.Length-2].Trim();
                            item.SOC_NAME = row[row.Length-1].Trim();
                            list.Add(item);
                        }
                        else
                        {
                            listErr.Add(strline);
                        }                        
                    }
                    else
                    {
                        //STT	Code	Tác Phẩm	Tác Giả	Nhạc	Lời	Ca Sỹ	Status
                        item = new WorkTXT();
                        item.SerialNo = int.Parse(row[0].Trim());
                        item.WK_INT_NO = row[1].Trim();
                        item.TTL_ENG = row[2].Trim();
                        item.ISWC_NO = row[3].Trim();
                        item.ISRC = row[4].Trim();
                        item.WRITER = row[5].Trim();
                        arrWriter = item.WRITER.Split(',');
                        item.WRITER = string.Empty;
                        for (int mk = 0; mk < arrWriter.Length; mk++)
                        {
                            if (arrWriter[mk].Trim() != string.Empty)
                            {
                                item.WRITER += arrWriter[mk].Trim() + ",";
                            }
                        }
                        if (item.WRITER.Length > 0)
                        {
                            item.WRITER = item.WRITER.Substring(0, item.WRITER.Length - 1);
                        }
                        item.WRITER2 = row[6].Trim();
                        item.WRITER3 = row[7].Trim();
                        item.ARTIST = row[8].Trim();
                        arrArtist = item.ARTIST.Split(',');
                        item.ARTIST = string.Empty;
                        for (int mk = 0; mk < arrArtist.Length; mk++)
                        {
                            if (arrArtist[mk].Trim() != string.Empty)
                            {
                                item.ARTIST += arrArtist[mk].Trim() + ",";
                            }
                        }
                        if (item.ARTIST.Length > 0)
                        {
                            item.ARTIST = item.ARTIST.Substring(0, item.ARTIST.Length - 1);
                        }
                        item.STATUS = row[9].Trim();
                        item.SOC_NAME = row[10].Trim();
                        list.Add(item);
                    }                    
                }               
            }
            catch (Exception)
            {
                list = null;
            }
            return new WorkTXTRead(list, listErr);
        }
        /// <summary>
        /// Tac gia viet
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static WorkTXTRead ReadUnicodeWorkTXT2(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<WorkTXT> list = new List<WorkTXT>();
            List<string> listErr = new List<string>();
            try
            {
                WorkTXT item = null;
                string[] row;
                string strline;                
                string writer = "";
                int ser = 0;
                for (int i = 1; i < data.Count; i++)
                {
                    //WK_INT_NO	WK_ENGLISH_TITLE	WK LOCAL TITLE	WK_STATUS	IPI NUMBER	IP_WK_Role	Internal No	IP NAME Type	IP ENGLISH NAME	IP LOCAL NAME	Society	PER OWN SHR	MEC COL SHR	ISWC
                    strline = ConvertAllToUnicode.ConvertFromComposite(data[i].Trim().ToUpper());
                    row = strline.Replace("\"", "").Split('\t');
                    if (row.Length != 14 && row.Length != 13)
                    {
                        listErr.Add(strline);
                    }
                    else
                    {
                        //if(row[0].Trim()== "00835414639")
                        //{
                        //    int a = 1;
                        //}
                        //WK_INT_NO	WK_ENGLISH_TITLE	WK LOCAL TITLE	WK_STATUS	IPI NUMBER	IP_WK_Role	Internal No	IP NAME Type	IP ENGLISH NAME	IP LOCAL NAME	Society	PER OWN SHR	MEC COL SHR	ISWC
                        item = new WorkTXT();
                        item.SerialNo = ser;
                        item.WK_INT_NO = row[0].Trim();
                        item.TTL_ENG = VnHelper.ConvertToUnSign(row[1].Trim().ToUpper());                        
                        item.TTL_LOCAL = row[2].Trim().ToUpper();   
                        if(item.TTL_LOCAL == string.Empty)
                        {
                            item.TTL_LOCAL = item.TTL_ENG;
                        }
                        if (row[3].Trim() == "COM")
                        {
                            item.STATUS = "COMPLETE";
                        }
                        else if (row[3].Trim() == "INC")
                        {
                            item.STATUS = "INCOMPLETE";
                        }
                        else
                        {
                            item.STATUS = "UNIDENTIFIED";
                        }    
                        item.IpNumber = VnHelper.ConvertToUnSign(row[4].Trim());
                        item.WK_ROLE = VnHelper.ConvertToUnSign(row[5].Trim().ToUpper());
                        item.InternalNo = VnHelper.ConvertToUnSign(row[6].Trim());
                        item.IP_NAME_TYPE = VnHelper.ConvertToUnSign(row[7].Trim().ToUpper());
                        writer = VnHelper.ConvertToUnSign(row[8].Trim().ToUpper());
                        item.WRITER = SwappName(writer);
                        //item.WRITER2 = row[6].Trim();
                        //item.WRITER3 = row[7].Trim();
                        item.WRITER_LOCAL = SwappName(row[9].Trim().ToUpper());
                        if(item.WRITER_LOCAL==string.Empty)
                        {
                            item.WRITER_LOCAL = item.WRITER;
                        }    
                        item.ARTIST = string.Empty;                          
                        item.Society = VnHelper.ConvertToUnSign(row[10].Trim().ToUpper());                        
                        item.PER_OWN_SHR = decimal.Parse(row[11].Trim());
                        item.MEC_COL_SHR = decimal.Parse(row[12].Trim());
                        if(row.Length == 14)
                        {
                            item.ISWC_NO = row[13].Trim();
                        }                        
                        item.ISRC = "";
                        item.SOC_NAME = "";                        
                        list.Add(item);
                        ser++;
                    }
                }
            }
            catch (Exception)
            {
                list = null;
            }
            return new WorkTXTRead(list, listErr);
        }

        private static string SwappName(string WRITER)
        {
            try
            {
                if (WRITER.Trim().Length == 0)
                {
                    return string.Empty;
                }
                string[] arrSubWriter = null;
                string[] arrWriter = WRITER.Split(',');
                string totalWRITER = string.Empty;
                string subwriter = string.Empty;
                for (int sub = 0; sub < arrWriter.Length; sub++)
                {
                    if (arrWriter[sub].Trim().Length > 0)
                    {
                        arrSubWriter = arrWriter[sub].Trim().Split(' ');
                        subwriter = string.Empty;
                        if (arrSubWriter.Length > 1)
                        {                           
                            for (int ik = 1; ik < arrSubWriter.Length; ik++)
                            {
                                subwriter += arrSubWriter[ik].Trim() + " ";
                            }                            
                        }
                        subwriter += arrSubWriter[0];
                        totalWRITER += subwriter.Trim() + ",";

                    }
                }
                if (totalWRITER.Length > 0)
                {
                    totalWRITER = totalWRITER.Substring(0, totalWRITER.Length - 1);
                }
                if(totalWRITER == null)
                {
                    totalWRITER = string.Empty;
                }
                return totalWRITER;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }
        #endregion

        #region masterlist source
        public static List<MasterSourceViewModel> ReadUnicodeMasterListSource(string fullPath)
        {
            var data = new List<string>();
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            fileStream.Close();
            List<MasterSourceViewModel> list = new List<MasterSourceViewModel>();
            try
            {
                MasterSourceViewModel item = null;
                string[] row;
                for (int i = 1; i < data.Count; i++)
                {
                    row = data[i].Replace("\"", "").Split('\t');
                    if (row.Length > 12)
                    {
                        item = new MasterSourceViewModel();
                        item.SerialNo = int.Parse(row[0].Trim());
                        item.Source = row[1].Trim();
                        item.ID = row[2].Trim();
                        item.TITLE = row[3].Trim();
                        item.TITLE2 = row[4].Trim();

                        item.ARTIST = row[5].Trim();
                        item.ALBUM = row[6].Trim();
                        item.LABEL = row[7].Trim();
                        item.ISRC = row[8].Trim();
                        item.COMP_ID = row[9].Trim();

                        item.COMP_TITLE = row[10].Trim();
                        item.COMP_TITLE2 = row[11].Trim();
                        item.COMP_ISWC = row[12].Trim();
                        item.AT = row[13].Trim();
                        item.COMP_WRITERS = row[14].Trim();
                        item.COMP_CUSTOM_ID = row[15].Trim();
                        

                        item.QUANTILE = row[16].Trim();
                        item.C_Workcode = row[17].Trim();
                        item.CODE = row[18].Trim();
                        item.Percent = row[19].Trim();
                        item.CODE_RIGHT = row[20].Trim();

                        item.Note = row[21].Trim();
                        item.ScoreCompare = 0;
                        list.Add(item);
                    }
                    else
                    {
                        //int a = 1;
                    }

                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion
    }
}
