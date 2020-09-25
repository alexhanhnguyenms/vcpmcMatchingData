using System;
using System.Collections.Generic;
using Vcpmc.Mis.ApplicationCore.Entities.control;
using Vcpmc.Mis.ApplicationCore.Entities.youtube;
using Vcpmc.Mis.Common.common.excel;
using Vcpmc.Mis.Common.csv;
using Vcpmc.Mis.Common.enums;
using Vcpmc.Mis.Common.master;
using Vcpmc.Mis.Shared.Tool;
using Vcpmc.Mis.Shared.viewModel.report;
using Vcpmc.Mis.ViewModels.MasterLists;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.Common.export
{
    /// <summary>
    /// Ghi bao Cao
    /// </summary>
    public class WriteReportHelper
    {
        #region Masterlist
        public static bool WriteCsvOld(string folderPath)
        {
            try
            {
                bool check = false;
                int count = 0;
                //string message = "";
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File1 == true), $"{folderPath}\\{NameReport.File1}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File2 == true), $"{folderPath}\\{NameReport.File2}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3 == true), $"{folderPath}\\{NameReport.File3}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File4_non_vi == true), $"{folderPath}\\{NameReport.File4_non_vi}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File5_non_vi == true), $"{folderPath}\\{NameReport.File5_non_vi}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File4_vi == true), $"{folderPath}\\{NameReport.File4_vi}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File5_vi == true), $"{folderPath}\\{NameReport.File5_vi}.csv");
                if (check) count++;
                //test
                //check = WriteCsv(Core.YoutubeFiles, $"{folderPath}\\{NameReport.File1}1.csv");

                return true;
            }
            catch (Exception )
            {
                return false;
                //MessageBox.Show(ex.ToString());
            }
        }
        public static bool WriteCsvNew(string folderPath)
        {
            try
            {
                bool check = false;
                int count = 0;
                //string message = "";
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File1 == true), $"{folderPath}\\{NameReport.File1}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File2 == true), $"{folderPath}\\{NameReport.File2}.csv");                
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_non_vi == true), $"{folderPath}\\{NameReport.File3_non_vi}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_vi == true), $"{folderPath}\\{NameReport.File3_vi}.csv");
                if (check) count++;
                check = WriteCsv(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_Affter_non_vi == true), $"{folderPath}\\{NameReport.File3_Affter_non_vi}.csv");
                if (check) count++;   
                return true;
            }
            catch (Exception )
            {
                return false;
                //MessageBox.Show(ex.ToString());
            }
        }
        private static bool WriteCsv(List<YoutubeFileItems> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter(fullPath))
                {
                    //1.header
                    string strHeader = "ID,TITLE,TITLE2,ARTIST,ARTIST2,ALBUM,ALBUM2,LABEL,LABEL2,ISRC,COMP_ID,COMP_TITLE,COMP_ISWC,COMP_WRITERS,COMP_CUSTOM_ID,QUANTILE,NOTE";
                    string[] strArray = strHeader.Split(',');
                    CsvRow row = new CsvRow();
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        row.Add(strArray[i]);
                    }
                    writer.WriteRow(row);
                    //2.data
                    foreach (var item in dataSource)
                    {
                        row = new CsvRow();
                        row.Add(item.ID);
                        row.Add(item.TITLE);
                        row.Add(item.TITLE2);
                        row.Add(item.ARTIST);
                        row.Add(item.ARTIST2);
                        row.Add(item.ALBUM);
                        row.Add(item.ALBUM2);
                        row.Add(item.LABEL);
                        row.Add(item.LABEL2);
                        row.Add(item.ISRC);

                        row.Add(item.COMP_ID);
                        row.Add(item.COMP_TITLE);
                        row.Add(item.COMP_ISWC);
                        row.Add(item.COMP_WRITERS);
                        row.Add(item.COMP_CUSTOM_ID);
                        row.Add(item.QUANTILE.ToString());
                        row.Add(item.DetectLanguage);
                        row.Add(item.Note);
                        writer.WriteRow(row);
                    }
                }
                result = true;
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }

        public static bool WriteExcelOld(string folderPath, long totalInfile)
        {
            try
            {
                bool check = false;
                int count = 0;
                //string message = "";               
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File1 == true), $"{folderPath}\\",$"{NameReport.File1}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File2 == true), $"{folderPath}\\",$"{NameReport.File2}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3 == true), $"{folderPath}\\",$"{ NameReport.File3}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File4_non_vi == true), $"{folderPath}\\",$"{ NameReport.File4_non_vi}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File5_non_vi == true), $"{folderPath}\\",$"{ NameReport.File5_non_vi}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File4_vi == true), $"{folderPath}\\",$"{ NameReport.File4_vi}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File5_vi == true), $"{folderPath}\\",$"{ NameReport.File5_vi}", totalInfile);
                if (check) count++;
                
                return true;
            }
            catch (Exception )
            {
                return false;
                //MessageBox.Show(ex.ToString());
            }
        }
        public static bool WriteExcelNew(string folderPath, long totalInfile)
        {
            try
            {
                bool check = false;
                int count = 0;
                //string message = "";
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File1 == true), $"{folderPath}\\", $"{NameReport.File1}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File2 == true), $"{folderPath}\\", $"{NameReport.File2}", totalInfile);
                if (check) count++;               
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_non_vi == true), $"{folderPath}\\", $"{ NameReport.File3_non_vi}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_vi == true), $"{folderPath}\\", $"{ NameReport.File3_vi}", totalInfile);
                if (check) count++;
                check = WriteExcel(MasterList.YoutubeFileItems.FindAll(x => x.Type.File3_Affter_non_vi == true), $"{folderPath}\\", $"{ NameReport.File3_Affter_non_vi}", totalInfile);
                if (check) count++;
                return true;
            }
            catch (Exception )
            {
                return false;
                //MessageBox.Show(ex.ToString());
            }
        }
        public static bool WriteExcel(List<YoutubeFileItems> dataSource, string fullPath,string name, long totalInfile)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteToOxmlNew(dataSource, fullPath, name);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool ExportMasterListSource(List<MasterSourceViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportMasterListSource(dataSource, fullPath);
            }
            catch (Exception)
            {
                throw;
                //result = false;
            }
            return result;
        }
        #endregion

        #region Distribition
        public static bool WriteExcelDistribution(List<BhDistributionViewModel> dataSource,string fullPath, string name, string strBh, string strMemberVn, DateTime date)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteToDistribution(dataSource, fullPath, name, strBh, strMemberVn, date);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool WriteExcelBhAggregates(List<BhAggregationViewModel> dataSource, string fullPath, string name, string strBh, string strMemberVn, DateTime date)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteToBhAggregates(dataSource, fullPath, name, strBh, strMemberVn, date);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool WriteExcelBhAggregates2(List<BhAggregation2ViewModel> dataSource, string fullPath, string name, string strBh, string strMemberVn, DateTime date)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteToBhAggregates2(dataSource, fullPath, name, strBh, strMemberVn, date);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Make link Distribution Quarter
        public static bool MakLinkDistributionQuarter(DistributionViewModel dataSource, string fullPath, string name,int year, int quarter, string regions)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();                             
                result = writer.MakLinkDistributionQuarter(dataSource, fullPath, name,year,quarter, regions);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region ExportCMS
        public static bool ExportCMS(List<CMSViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportCMS(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Export  matching
        public static bool ExportPreClaimMatching(List<PreclaimMatchingViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportPreClaimMatching(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool ExportWorkMatching(List<WorkMatchingViewModel> dataSource, string fullPath, int typeExport)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportWorkMatching(dataSource, fullPath, typeExport);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Export Work
        public static bool ExportWork(List<WorkViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportWork(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion
        #region Export Work
        public static bool ExportMember(List<MemberViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportMember(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Export search
        public static bool ExportSearch(List<WorkViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportSearch(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region EditFile
        /// <summary>
        /// Xuất file Excel khi chuyển đổi báo cáo từ MIS
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool WriteExcelEditFiles(List<EdiFilesItem> dataSource, string fullPath,int type)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteToEditFiles(dataSource, fullPath,type);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region MyRegion
        public static bool WriteExcelConvertToUnsign(List<ConvertyToUnsign> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                //result = writer.Write(dataSource, fullPath);                
                result = writer.WriteExcelConvertToUnsign(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Export monopoly
        public static bool ExportMonopoly(List<MonopolyViewModel> dataSource, string fullPath)
        {
            bool result = false;
            try
            {
                ExcelHelper writer = new ExcelHelper();
                result = writer.ExportMonopoly(dataSource, fullPath);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
