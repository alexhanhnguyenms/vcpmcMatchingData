using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.SS.UserModel;
using Vcpmc.Mis.ApplicationCore.Entities.dis;
using Vcpmc.Mis.ApplicationCore.Entities.makeData;
using Vcpmc.Mis.ApplicationCore.Entities.control;
using Vcpmc.Mis.Common.master;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.Shared.viewModel.report;
using Vcpmc.Mis.ApplicationCore.Entities.youtube;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter;
using Z.Expressions;
using Vcpmc.Mis.ViewModels.Mis.Works;
using NPOI.HSSF.UserModel;
using Vcpmc.Mis.ViewModels.MasterLists;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.Shared.Tool;
using System.Globalization;
using Vcpmc.Mis.Shared.Mis.Members;

namespace Vcpmc.Mis.Common.common.excel
{
    public class ExcelHelper
    {
        #region 1.1.makeData.Distribution
        public List<DistributionDataItem> ReadExcelDistribution(Guid id,string fileName)
        {
            List<DistributionDataItem> distributionDataItems = new List<DistributionDataItem>();
            try
            {
                NPOI.SS.UserModel.IWorkbook workbook = null;               
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs); 
                }
                //First sheet
                NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.
                                                     // If first row is table head, i starts from 1
                    int cellIndex = 0;
                    for (int i = 4; i <= rowCount; i++)
                    {
                        cellIndex = 0;
                        NPOI.SS.UserModel.IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = i - 4;
                            break;
                        }
                        DistributionDataItem item = new DistributionDataItem();
                        item.DistributionDataId = id;
                        item.Id = Guid.NewGuid();
                        item.StatusLoad = true;
                        item.TimeCreate = DateTime.Now;
                        if (curRow.GetCell(cellIndex) != null)
                        {
                            string no = curRow.GetCell(cellIndex).ToString().Trim();
                            if (no == string.Empty || no == "0")
                            {
                                continue;
                            }
                            item.No = int.Parse(no);
                        }
                        else
                        {
                            continue;
                        }
                        
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            string WorkInNo = curRow.GetCell(cellIndex).ToString().Trim();                            
                            item.WorkInNo = curRow.GetCell(cellIndex).ToString().Trim();
                        }
                        else
                        {
                            continue;
                        }                        
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.Title = curRow.GetCell(cellIndex).ToString().Trim();
                            item.Title2 = VnHelper.ConvertToUnSign(item.Title);
                            item.Title2 = MasterList.ReplaceSpecialCharactor(item.Title2);
                        }
                        else
                        {
                            item.StatusLoad = false;
                        }
                        cellIndex++;
                        if (curRow.GetCell(0) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.PoolName = curRow.GetCell(cellIndex).ToString().Trim();
                            item.PoolName2 = VnHelper.ConvertToUnSign(item.PoolName);
                            item.PoolName2 = MasterList.ReplaceSpecialCharactor(item.PoolName2);
                        }
                        //else
                        //{
                        //    item.StatusLoad = false;
                        //}
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.SourceName = curRow.GetCell(cellIndex).ToString().Trim();
                            item.SourceName2 = VnHelper.ConvertToUnSign(item.SourceName);
                            item.SourceName2 = MasterList.ReplaceSpecialCharactor(item.SourceName2);
                        }
                        //else
                        //{
                        //    item.StatusLoad = false;
                        //}
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.Role = curRow.GetCell(cellIndex).ToString().Trim();
                        }
                        //else
                        //{
                        //    item.StatusLoad = false;
                        //}
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.Share = decimal.Parse(curRow.GetCell(cellIndex).ToString().Trim());
                        }
                        else
                        {
                            item.Share = 100;
                        }    
                        //else
                        //{
                        //    item.StatusLoad = false;
                        //}
                        cellIndex++;
                        object sd2 = curRow.GetCell(cellIndex);
                        object sd = curRow.GetCell(cellIndex).ToString();
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.Royalty = decimal.Parse(curRow.GetCell(cellIndex).ToString().Trim());
                        }
                        else
                        {
                            item.StatusLoad = false;
                        }
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {
                            item.Location =curRow.GetCell(cellIndex).ToString().Trim();
                        }
                        cellIndex++;
                        if (curRow.GetCell(cellIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cellIndex).ToString()))
                        {                            
                            string da = curRow.GetCell(cellIndex).ToString().Trim().Replace("..",".");
                            try
                            {
                                string[] date = da.Split('.');
                                item.ContractTime = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                            }
                            catch (Exception )
                            {
                                try
                                {
                                    item.strContractTime = curRow.GetCell(cellIndex).ToString().Trim();
                                    string[] date = da.Split(' ');
                                    item.ContractTime = new DateTime(int.Parse(date[1]), 1, 1);
                                }
                                catch (Exception )
                                {

                                    item.StatusLoad = false;
                                }
                            }                            
                        }
                        else
                        {
                            item.ContractTime = DateTime.Now;
                            item.StatusLoad = false;                          
                        }
                        cellIndex++;
                        if(item.No == 3865)
                        {
                            //int test = 0;
                        }
                        distributionDataItems.Add(item);
                    }                
                    
                }

                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception )
            {
                distributionDataItems = null;
            }
            return distributionDataItems;
        }

        #endregion

        #region 2.1.Control.EdiFiles
        /// <summary>
        /// Read file Export from mis
        /// </summary>
        /// <param name="id"></param>
        /// <param name="generateType">0: generrateReport, generate local report, new match report</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<EdiFilesItem> ReadExcelEditFile(string fileName, out int countread, out int countgood, out int countgood2,int generateType)
        {
            countread = 0;
            countgood = 0;
            countgood2 = 0;
            List<EdiFilesItem> distributionDataItems = new List<EdiFilesItem>();
            try
            {
                NPOI.SS.UserModel.IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    int countRow = 1;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        countread++;
                        countgood++;
                        countgood2++;
                        cIndex = 1;
                        if(generateType == 0)
                        {
                            cIndex = 0;
                        }
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                             rowCount = i - 1;
                            continue;
                        }
                        EdiFilesItem item = new EdiFilesItem();
                        //item.EdiFilesId = id;
                        //item.Id = Guid.NewGuid();
                        //item.TimeCreate = DateTime.Now;
                        item.index = countRow;
                        countRow++;
                        //1-5
                        /*
                         SEQ. NO.	
                         TITLE	
                        NO OF PERF.	
                        COMPOSER	
                        ARTIST	
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string a = curRow.GetCell(cIndex).ToString().Trim();
                            item.seqNo = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else
                        //{
                        //    countgood--;
                        //    continue;
                        //    string dsfs = curRow.GetCell(cIndex).ToString();
                        //    int a = 1;
                        //}
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Title = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                            item.Title3 = VnHelper.RemoveSpecialCharactor(item.Title);                           
                        }
                        //else item.Title = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.NoOfPerf = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else
                        //{
                        //    countgood2--;
                        //    continue;
                        //}
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Composer = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.Composer = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Artist = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.Artist = "";
                        cIndex++;
                        //6-10
                        /*
                         PUBLISHER	
                        Work Int. No	
                        REGIONAL NO	
                        WK TITLE	
                        WK ARTIST	
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Publisher = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.Publisher = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.WorkInternalNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else
                        //{
                        //    item.WorkInternalNo = "";
                        //}
                        cIndex++;
                        if(generateType == 2 || generateType == 1)
                        {
                            if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                            {
                                item.LocalWorkIntNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                            }
                            //else item.RegionalNo = "";
                            cIndex++;
                        } 
                        else
                        {//1
                            //khong dung
                            //if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                            //{
                            //    item.RegionalNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                            //}  
                            cIndex++;
                        }
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.WorkTitle = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.WorkTitle = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.WorkArtist = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.WorkArtist = "";
                        cIndex++;
                        //11-15
                        /*
                        WK COMPOSER	
                        WK STATUS	
                        IP SET No.	
                        IP INT NO	
                        NAME NO	
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.WorkComposer = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.WorkComposer = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.WorkStatus = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.WorkStatus = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpSetNo = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        //else item.IpSetNo = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpInNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.IpInNo = "";
                        cIndex++;//LOCAL IP INT NO
                        if(generateType==1 || generateType ==2)
                        {
                            if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                            {
                                item.LocalIpIntNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                            }
                            cIndex++;//00065015010
                        }
                        else
                        {
                            //mau generate report khong co LocalIpIntNo
                        }
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        { 
                            item.NameNo = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim()); 
                        }
                        //else item.NameNo = "";
                        cIndex++;
                        //16-20
                        /*
                        IP NAMETYPE	
                        IP WK ROLE	
                        IP NAME	
                        IP NAME LOCAL	
                        SOCIETY	
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpNameType = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.IpNameType = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpWorkRole = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        //else item.IpWorkRole = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpName = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.IpName = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.IpNameLocal = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.IpNameLocal = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Society = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.Society = "";
                        cIndex++;
                        //21-25
                        /*
                         SP NAME	
                        SOCIETY	
                        PER OWN SHR	
                        PER COL SHR	
                        MEC OWN SHR	
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SpName = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        else item.SpName = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Society2 = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        else item.Society2 = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string a = curRow.GetCell(cIndex).ToString().Trim();
                            item.PerOwnShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.PerOwnShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.PerColShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.PerColShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.MecOwnShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.MecOwnShr = "";
                        cIndex++;
                        //26-30
                        /*
                        MEC COL SHR	
                        SP SHR	
                        TOTAL MEC SHR	
                        SYN OWN SHR	
                        SYN COL SHR
                         */
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.MecColShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.MecColShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SpShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.SpShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.TotalMecShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.TotalMecShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SynOwnShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        //else item.SynOwnShr = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SynColShr = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim()); 
                        }
                        //else item.SynColShr = "";
                        cIndex++;
                        //final
                        distributionDataItems.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                distributionDataItems = null;
            }
            return distributionDataItems;
        }
        /// <summary>
        ///  Xuất file Excel khi chuyển đổi báo cáo từ MIS
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool WriteToEditFiles(List<EdiFilesItem> dataSource, string fullPath, int type, bool isVcpmcRegion)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = CreatenewFileEditFiles(fullPath, out workbookpart, out sheetData,type, isVcpmcRegion);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    if (type == 2)
                    {
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.seqNo.ToString()) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Title) });
                    }
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NoOfPerf.ToString()) });                    
                    if (type==2)
                    {
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Composer) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Artist) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Publisher) });
                    }
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkInternalNo) });
                    if(type ==2)
                    {
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LocalWorkIntNo) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkTitle) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkArtist) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkComposer) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkStatus) });

                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpSetNo) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpInNo) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LocalIpIntNo) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NameNo)});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpNameType) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpWorkRole) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpName) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IpNameLocal) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Society) });
                        //SP NAME	SOCIETY
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("")});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("")});
                       
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.PerOwnShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.PerColShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MecOwnShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MecColShr.ToString())});

                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SpShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TotalMecShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SynOwnShr.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SynColShr.ToString())});

                    }
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkTitle2.ToString()) }); 
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.StrOtherTitleOut.ToString()) }); 
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.StrOtherTitleOutUnSign.ToString()) }); 
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.GroupWriter.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.GroupComposer) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.GroupLyrics) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.GroupPublisher) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkArtist) }); 

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkStatus) }); 
                    //doc quyen                                   
                    if(type==0 || type == 2)
                    {
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkMonopolyNote) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MemberMonopolyNote) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NonMember) });
                        if (isVcpmcRegion)
                        {
                            newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.VcpmcRegion) });
                        }
                        if(item.MemberMonopolyNote !=string.Empty)
                        {
                            //int a = 1;
                        }
                    }
                    else
                    {
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IsWorkMonopoly.ToString()) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkFields) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkMonopolyNote) });

                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IsMemberMonopoly.ToString()) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MemberFields)});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MemberMonopolyNote)});

                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MesssageCompareTitleAndWriter)});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TotalWriter.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.CountMatchWriter.ToString())});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NonMember) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.VcpmcRegion.ToString()) });
                        if (item.MemberMonopolyNote != string.Empty)
                        {
                            //int a = 1;
                        }
                    }
                    sheetData.AppendChild(newRow);
                    count++;
                }    

                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument CreatenewFileEditFiles(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData, int type, bool isVcpmcRegion)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header   
            string strHeader = string.Empty;
            if(type==0)
            {
                strHeader = 
                "NO OF PERF.," +                           
                "WORK INTER NO,WORK TITLE,OTHER TITLE,UNSIGN OTHER TITLE," +               
                "WRITER,COMPOSER,LYRICS,PUBLISHER," +
                "ARTIST," +
                "WORK STATUS," +
                "WORK MONO NOTE, MEMBER MONO NOTE," +
                "NON MEMBER";
                if(isVcpmcRegion)
                {
                    strHeader += ",VcpmcRegion";
                }
            }
            else if(type == 1)
            {
                strHeader =
                 "NO OF PERF.," +
                 "WORK INTER NO,WORK TITLE,OTHER TITLE,UNSIGN OTHER TITLE," +
                 "WRITER,COMPOSER,LYRICS,PUBLISHER," +
                 "ARTIST," +
                 "WORK STATUS," + 
                 "IS WORK MONO,FIELD WORK MONO,WORK MONO NOTE," +
                 "IS MEMBER MONO,FIELD MEMBER MONO,MEMBER MONO NOT," +
                 "Mesg Compare Title And Writer, Total writer, count writer matched," +
                 "NON MEMBER,VcpmcRegion";                
            } 
            else
            {
                strHeader =
                //them
                "SEQ. NO.,TITLE," +
                "NO OF PERF.," +
                //them
                "COMPOSER,ARTIST,PUBLISHER,"+
                "Work Int. No,Local Work Int. No," +
                "WK TITLE,WK ARTIST,WK COMPOSER,WK STATUS," +
                "IP SET No.,IP INT NO,LOCAL IP INT NO," +
                "NAME NO,IP NAMETYPE,IP WK ROLE," +
                "IP NAME,IP NAME LOCAL,SOCIETY,SP NAME,SOCIETY," +
                "PER OWN SHR,PER COL SHR,MEC OWN SHR,MEC COL SHR," +
                "SP SHR,TOTAL MEC SHR,SYN OWN SHR,SYN COL SHR," +
                "WORK TITLE,WORK TITLE2," +
                "WRITER,COMPOSER,LYRICS,PUBLISHER," +
                "ARTIST," +
                "WORK STATUS," +
                //them
                "PER OWN SHR,PER COL SHR,MEC OWN SHR,MEC COL SHR," +
                "SP SHR,TOTAL MEC SHR,SYN OWN SHR,SYN COL SHR,"+
                //
                "WORK TITLE2,OTHER TITLE,UNSIGN OTHER TITLE," +
                "WRITER,COMPOSER,LYRICS,PUBLISHER," +
                "WORK MONO NOTE, MEMBER MONO NOTE," +
                "NON MEMBER,VcpmcRegion";
            }
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion
        #region 2.1.Control.MemberWorkList
        /// <summary>
        /// Read file Export from mis
        /// </summary>
        /// <param name="id"></param>
        /// <param name="generateType">0: generrateReport, generate local report, new match report</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<MemberWorkList> ReadExcelMemberEorkList(string fileName)
        {           
            List<MemberWorkList> distributionDataItems = new List<MemberWorkList>();
            try
            {
                NPOI.SS.UserModel.IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    int countRow = 1;
                    for (int i = 15; i <= rowCount; i++)
                    {                        
                        cIndex = 0;                        
                        IRow curRow = sheet.GetRow(i);                       
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = i - 1;
                            continue;
                        }
                        MemberWorkList item = new MemberWorkList();                       
                        item.SerialNo = countRow;
                        countRow++;
                       
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string a = curRow.GetCell(cIndex).ToString().Trim();
                            item.INTERNAL_NO = curRow.GetCell(cIndex).ToString().Trim();
                        }                        
                        cIndex++;
                        cIndex++;//WID NO.
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string a = curRow.GetCell(cIndex).ToString().Trim();
                            item.ISWC_NO = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.TITLE = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());                           
                        }                       
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.DURATION = curRow.GetCell(cIndex).ToString().Trim();
                        }                        
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.LANGUAGE = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }                       
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.CATEGORY = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }                       
                        cIndex++;                        
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.STATUS = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }                       
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.ARTISTE = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }                        
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SET_NO = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.NAME_TYPE = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.ROLE = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.NAME = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.SOCIETY = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        //final
                        distributionDataItems.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                distributionDataItems = null;
            }
            return distributionDataItems;
        }
        /// <summary>
        ///  Xuất file Excel khi chuyển đổi báo cáo từ MIS
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool WriteToExcelMemberEorkList(List<MemberWorkList> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = CreateToExcelMemberEorkList(fullPath, out workbookpart, out sheetData);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();                    
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.INTERNAL_NO) });
                    
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WID_NO.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ISWC_NO.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE3) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DURATION) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LANGUAGE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.CATEGORY) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.STATUS) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ARTISTE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SET_NO) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NAME_TYPE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ROLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NAME2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.NAME3) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SOCIETY) });                   
                    sheetData.AppendChild(newRow);
                    count++;
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument CreateToExcelMemberEorkList(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header   
            string strHeader = string.Empty;
            strHeader = " NTERNAL NO,WID NO.,ISWC NO,TITLE,TITLE2,TITLE3,DURATION,LANGUAGE,CATEGORY,STATUS,ARTISTE,SET NO.,NAME TYPE,ROLE,NAME2,NAME3,SOCIETY";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion
        #region convert to unsign
        public List<ConvertyToUnsign> ReadExcelConvertyToUnsign(string fileName)
        {           
            List<ConvertyToUnsign> distributionDataItems = new List<ConvertyToUnsign>();
            try
            {
                NPOI.SS.UserModel.IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    int countRow = 1;
                    for (int i = 1; i <= rowCount; i++)
                    {                       
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = i - 1;
                            continue;
                        }
                        ConvertyToUnsign item = new ConvertyToUnsign();  
                        countRow++;
                        
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string a = curRow.GetCell(cIndex).ToString().Trim();
                            item.SerialNo = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }                        
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c1 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c2 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c3 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c4 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c5 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c6 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c7 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c8 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c9 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.c10 = curRow.GetCell(cIndex).ToString().Trim();
                        }
                        cIndex++;
                        //final
                        distributionDataItems.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                distributionDataItems = null;
            }
            return distributionDataItems;
        }
        /// <summary>
        ///  Xuất file Excel khi chuyển đổi báo cáo từ MIS
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool WriteExcelConvertToUnsign(List<ConvertyToUnsign> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = WriteExcelConvertToUnsign(fullPath, out workbookpart, out sheetData);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();                   
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c1)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c2)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c3)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c4)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c5)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c6)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c7)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c8)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c9)});
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.c10)});
                   
                    sheetData.AppendChild(newRow);
                    count++;
                }

                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument WriteExcelConvertToUnsign(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header   
            string strHeader = string.Empty;
            strHeader = "SerialNo,c1,c2,c3,c4,c5,c6,c7,c8,c9,c10";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region 3.1.Map
        public List<ImportMapWorkMemberDetail> ReadExcelImportWorkMenber(Guid id, string fileName)
        {           
            List<ImportMapWorkMemberDetail> distributionDataItems = new List<ImportMapWorkMemberDetail>();
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    for (int i = 4; i <= rowCount; i++)
                    {                      
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = 4 - 1;
                            break;
                        }
                        ImportMapWorkMemberDetail item = new ImportMapWorkMemberDetail();
                        item.ImportMapWorkMemberId = id;
                        item.Id = Guid.NewGuid();
                        item.TimeCreate = DateTime.Now;
                        //
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.No = int.Parse(curRow.GetCell(cIndex).ToString().Trim());                        
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Internal = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Internal = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Title = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Title = "";
                        item.Title2 = VnHelper.ConvertToUnSign(item.Title);
                        item.Title2 = MasterList.ReplaceSpecialCharactor(item.Title2);
                        cIndex++;
                        
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Author = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Author = "";
                        item.Author = VnHelper.ConvertToUnSign(item.Author);
                        item.Author = MasterList.ReplaceSpecialCharactor(item.Author);
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Composer = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Composer = "";
                        item.Composer2 = VnHelper.ConvertToUnSign(item.Composer);
                        item.Composer2 = MasterList.ReplaceSpecialCharactor(item.Composer2);
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Lyrics = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Lyrics = "";
                        item.Lyrics2 = VnHelper.ConvertToUnSign(item.Lyrics);
                        item.Lyrics2 = MasterList.ReplaceSpecialCharactor(item.Lyrics2);
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Publisher = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Publisher = "";
                        item.Publisher2 = VnHelper.ConvertToUnSign(item.Publisher);
                        item.Publisher2 = MasterList.ReplaceSpecialCharactor(item.Publisher2);
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Artistes = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Artistes = "";
                        item.Artistes2 = VnHelper.ConvertToUnSign(item.Artistes);
                        item.Artistes2 = MasterList.ReplaceSpecialCharactor(item.Artistes2);
                        cIndex++;


                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.strStatus = curRow.GetCell(cIndex).ToString().Trim();
                        else item.strStatus = "";
                        //final
                        List<string> list = StringHelper.GetUniqueStringArray(new string[] { item.Author, item.Composer2, item.Lyrics2 });
                        item.TotalAuthor = StringHelper.ConvertListToString(list);

                        //
                        distributionDataItems.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception )
            {
                distributionDataItems = null;
            }
            return distributionDataItems;
        }
        #endregion

        #region 3.2.Exception work
        public List<ExceptionWorkDetail> ReadExcelImportExceptionWork(Guid id, string fileName)
        {
            List<ExceptionWorkDetail> list = new List<ExceptionWorkDetail>();
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = 1 - 1;
                            break;
                        }
                        ExceptionWorkDetail item = new ExceptionWorkDetail();
                        item.ExceptionWorkId = id;
                        item.Id = Guid.NewGuid();
                        item.TimeCreate = DateTime.Now;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Member = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Member = "";
                        item.Member2 = VnHelper.ConvertToUnSign(item.Member);
                        item.Member2 = MasterList.ReplaceSpecialCharactor(item.Member2);
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.No = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Title = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Title = "";
                        item.Title2 = VnHelper.ConvertToUnSign(item.Title);
                        item.Title2 = MasterList.ReplaceSpecialCharactor(item.Title2);
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.PoolName = curRow.GetCell(cIndex).ToString().Trim();
                        else item.PoolName = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Type = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Type = "";
                        list.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception )
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region 3.2.Danh sach thanh vien cua BH
        public List<MemberBHDetail> ReadExcelImportMemberBh(Guid id, string fileName)
        {
            List<MemberBHDetail> list = new List<MemberBHDetail>();
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    for (int i = 2; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = 2 - 1;
                            break;
                        }
                        MemberBHDetail item = new MemberBHDetail();
                        item.MemberBHId = id;
                        item.Id = Guid.NewGuid();
                        item.TimeCreate = DateTime.Now;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.No = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Type = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Type = "";                        
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Member = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Member = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.MemberVN = curRow.GetCell(cIndex).ToString().Trim();
                        else item.MemberVN = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.StageName = curRow.GetCell(cIndex).ToString().Trim();
                        else item.StageName = "";
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.SubMember = curRow.GetCell(cIndex).ToString().Trim();
                        else item.SubMember = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Beneficiary = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Beneficiary = "";
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.GetPart = curRow.GetCell(cIndex).ToString().Trim();
                        else item.GetPart = "";
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            if(curRow.GetCell(cIndex).ToString().Trim().ToUpper()=="TRUE")
                            {
                                item.IsAlwaysGet = true;
                            }    
                            else
                            {
                                item.IsAlwaysGet = false;
                            }                               
                        }
                        else item.IsAlwaysGet = false;
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string da = curRow.GetCell(cIndex).ToString().Trim().Replace("..", ".");
                            string[] date = da.Split('.');
                            item.returnDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                        }
                        //else item.returnDate = "";
                        cIndex++;
                        //if (i == 14)
                        //{
                        //    string dsad = "";
                        //}
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            //string s = curRow.GetCell(cIndex).ToString().Trim();
                            item.Percent = decimal.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        else item.Percent = 100;
                        cIndex++;                        
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            if (curRow.GetCell(cIndex).ToString().Trim().ToUpper() == "TRUE")
                            {
                                item.IsGiveBeneficiary = true;
                            }
                            else
                            {
                                item.IsGiveBeneficiary = false;
                            }
                        }
                        else item.IsGiveBeneficiary = true;
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            if (curRow.GetCell(cIndex).ToString().Trim().ToUpper() == "TRUE")
                            {
                                item.IsCreateReport = true;
                            }
                            else
                            {
                                item.IsCreateReport = false;
                            }
                        }
                        else item.IsCreateReport = true;
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Note = curRow.GetCell(cIndex).ToString().Trim();
                        else item.Note = "";
                        cIndex++;

                        list.Add(item);
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception )
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region 3.3. Distribution Quater
        /// <summary>
        /// Load master
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quater"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DistributionViewModel ReadExcelImportDistributionQuaterMaster(int year, int quater, string fileName)
        {
            int indexest =0;
            DistributionViewModel dis = new DistributionViewModel();
            dis.Year = year;
            dis.Quarter = quater;           
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {                  
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    for (int i = 4; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        if (curRow.GetCell(0) != null && string.IsNullOrEmpty(curRow.GetCell(0).ToString()))
                        {
                            string a = curRow.GetCell(0).ToString();
                            break;
                        }
                        Distribution item = new Distribution();                                 
                        //0
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string strxx = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.SerialNo = int.Parse(strxx);
                                indexest = item.SerialNo;
                            }
                            catch (Exception)
                            {
                                break;
                            }                           
                        }
                        cIndex++;
                        #region Distribution
                        //1
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.IntNo = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        //2.name
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Member = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        //3.
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();                                                     
                            try
                            {
                                item.WriterShare = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {                               
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                            
                        }
                        cIndex++;
                        //4.note
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.PublisherShare = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                        }
                        cIndex++;
                        //5
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.Adjs = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                        }
                        cIndex++;
                        //6
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.Youtube = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                        }
                        cIndex++;
                        //7
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.DisExcel = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                        }
                        cIndex++;
                        //8
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            string str = curRow.GetCell(cIndex).ToString().Trim();
                            try
                            {
                                item.NetRoyalties = Eval.Execute<decimal>(str);
                            }
                            catch (Exception)
                            {
                                throw new Exception($"convert to decimal is error, index:{indexest}");
                            }
                        }
                        cIndex++;   
                        #endregion

                        dis.Items.Add(item);
                    }

                }
                #region Sum
                foreach (var item in dis.Items)
                {
                    dis.TotalWriterShare += item.WriterShare;
                    dis.TotalPublisherShare += item.PublisherShare;
                    dis.TotalAdjs += item.Adjs;
                    dis.TotalYoutube += item.Youtube;
                    dis.TotalDisExcel += item.DisExcel;
                    dis.TotalNetRoyalties += item.NetRoyalties;                    
                }
                #endregion
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                int a = indexest;
                throw ex;
            }
            return dis;
        }
        /// <summary>
        /// Load chi tiết đetail
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        public DistributionViewModel ReadExcelImportDistributionQuaterDetail(DistributionViewModel master)
        {
            //DistributionViewModel list = new DistributionViewModel();
            string intNo = "";
            string ipiNameNo = "";
            string Name = "";
            string ipiBaseNo = "";
            try
            {
                foreach (var itemMaster in master.Items)
                {
                    //Distribution clone = (Distribution)itemMaster.Clone();
                    if(itemMaster.Path!=string.Empty)
                    {
                        string[] arrayPath = itemMaster.Path.Split(';');
                        foreach (string subPath in arrayPath)
                        {
                            #region Detail
                            IWorkbook workbook = null;
                            FileStream fs = new FileStream(subPath, FileMode.Open, FileAccess.Read);
                            if (itemMaster.Path.IndexOf(".xlsx") > 0)
                            {
                                workbook = new XSSFWorkbook(fs);
                            }
                            else if (itemMaster.Path.IndexOf(".xls") > 0)
                            {
                                workbook = new HSSFWorkbook(fs);
                            }
                            //First sheet
                            ISheet sheet = workbook.GetSheetAt(0);
                            if (sheet != null)
                            {
                                int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                                                                 // If first row is table head, i starts from 1                  
                                int cIndex = 0;
                                int SerialNo = 0;
                                int indexest = 0;
                                for (int i = 0; i <= rowCount; i++)
                                {
                                    indexest++;
                                    cIndex = 0;
                                    IRow curRow = sheet.GetRow(i);
                                    if(i==0)
                                    {
                                        if (curRow.GetCell(1) != null && !string.IsNullOrEmpty(curRow.GetCell(1).ToString())) intNo = curRow.GetCell(1).ToString();
                                        if (curRow.GetCell(3) != null && !string.IsNullOrEmpty(curRow.GetCell(3).ToString())) ipiNameNo = curRow.GetCell(3).ToString();
                                    }
                                    else if (i == 1)
                                    {
                                        if (curRow.GetCell(1) != null && !string.IsNullOrEmpty(curRow.GetCell(1).ToString())) Name = curRow.GetCell(1).ToString();
                                        if (curRow.GetCell(3) != null && !string.IsNullOrEmpty(curRow.GetCell(3).ToString())) ipiBaseNo = curRow.GetCell(3).ToString();
                                    }

                                    if(i<4)
                                    {
                                        continue;
                                    }                                   
                                    
                                    DistributionDetails item = new DistributionDetails();
                                    SerialNo++;
                                    item.SerialNo = SerialNo;
                                    //0
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                                    {
                                        item.WorkIntNo = curRow.GetCell(cIndex).ToString().Trim();
                                    }
                                    else
                                    {
                                        if(curRow.GetCell(6) != null && !string.IsNullOrEmpty(curRow.GetCell(6).ToString()))
                                        {
                                            string str = curRow.GetCell(6).ToString().Trim();
                                            try
                                            {
                                                itemMaster.TotalRoyalty = Eval.Execute<decimal>(str);
                                            }
                                            catch (Exception)
                                            {
                                                throw new Exception($"convert to decimal is error, index:{indexest}");
                                            }                                           
                                        }
                                        break;
                                    }
                                    cIndex++;
                                    //1
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Title = curRow.GetCell(cIndex).ToString().Trim();
                                    cIndex++;
                                    //2.name
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.PoolName = curRow.GetCell(cIndex).ToString().Trim();
                                    cIndex++;
                                    //3.
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.SourceName = curRow.GetCell(cIndex).ToString().Trim();
                                    cIndex++;
                                    //4.
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Role = curRow.GetCell(cIndex).ToString().Trim();
                                    cIndex++;
                                    //5
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                                    {
                                        string str = curRow.GetCell(cIndex).ToString().Trim();
                                        try
                                        {
                                            item.Share = Eval.Execute<decimal>(str);
                                        }
                                        catch (Exception)
                                        {
                                            //throw new Exception($"convert to decimal is error, index:{indexest}");
                                        }                                       
                                    }
                                    cIndex++;
                                    //6
                                    if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                                    {
                                        string str = curRow.GetCell(cIndex).ToString().Trim();
                                        try
                                        {
                                            item.Royalty = Eval.Execute<decimal>(str);
                                        }
                                        catch (Exception)
                                        {
                                            throw new Exception($"convert to decimal is error, index:{indexest}");
                                        }                                       
                                    }
                                    cIndex++;
                                    itemMaster.Items.Add(item);
                                }
                            }
                            #endregion
                            //list.Items.Add(clone);
                            //TODO
                            sheet = null;
                            workbook = null;
                            fs.Close();
                            fs = null;
                            GC.Collect();
                        }                       
                    }
                    if(itemMaster.IntNo != intNo)
                    {
                        itemMaster.IsLoadDetail = false;
                    }
                    else
                    {
                        itemMaster.IPINameNo = ipiNameNo;
                        //clone.Member = Name;
                        itemMaster.IPIBaseNo = ipiBaseNo;
                        itemMaster.IsLoadDetail = true;

                    }                    

                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return master;
        }
        public bool MakLinkDistributionQuarter(DistributionViewModel dataSource, string fullPath, string name,int year, int quarter,string regions)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = MakLinkDistributionQuarter(fullPath, name, out workbookpart, out sheetData);
                //detail part
                foreach (var item in dataSource.Items)
                {
                    if(item.Items.Count == 0)
                    {
                        continue;
                    }
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //1.stt
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    //2.link
                    string data = item.NameFile;
                    //string link = $"{fullPath}\\{data}.xls";
                    string link = $"{data}.xls";
                    
                    newRow.AppendChild(BuildHyperlinkCell(link,data));     
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IntNo) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Member) });
                    //4.Mien
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(regions) });
                    //5.In
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(regions) });
                    //6.số tiền
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TotalRoyalty.ToString("#.###")) });
                    //7.kỳ phân phối
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.Number,
                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(
                        $"{year.ToString().Substring(0, 2)}{quarter.ToString().PadLeft(2, '0')}")
                    });
                    //8.stt
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                    //9.Merge
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                    //10.error
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });

                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion
                    
                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument MakLinkDistributionQuarter(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "STT,FILE,CODE, Tác giả,Miền,In,Số Tiền,Kỳ PP,STT,Merge,Error";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);   
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
       
        #endregion

        #region 4.1.Warehouse
        public List<MonopolyViewModel> ReadExcelImportMonopoly(int group, string fileName)
        {
            List<MonopolyViewModel> list = new List<MonopolyViewModel>();
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int serial = 0;
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;                   
                    for (int i = 3; i <= rowCount; i++)
                    {
                        cIndex = 1;
                        IRow curRow = sheet.GetRow(i);
                        if (curRow.GetCell(0) != null && string.IsNullOrEmpty(curRow.GetCell(0).ToString()))                        
                        {
                            string a = curRow.GetCell(0).ToString();
                            break;
                        }
                        serial++;
                        MonopolyViewModel item = new MonopolyViewModel();
                        item.Group = group;
                        item.SerialNo = serial;
                        //0
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.CodeOld = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim().ToUpper());
                            item.CodeOld = VnHelper.ConvertToUnSign(item.CodeOld);
                        }
                        cIndex++;
                        //1
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.CodeNew = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim().ToUpper());
                            item.CodeNew = VnHelper.ConvertToUnSign(item.CodeNew);
                        }
                        cIndex++;
                        //2.name
                        if(group == 0)
                        {
                            if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                            {
                                item.Name = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim().ToUpper());
                                item.Name2 = VnHelper.ConvertToUnSign(item.Name);
                            }
                            cIndex++;
                        }
                        //2.name type
                        else
                        {
                            if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                            {
                                item.NameType = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim().ToUpper());
                                item.NameType = VnHelper.ConvertToUnSign(item.NameType);
                            }
                            cIndex++;
                            //bo dong ket,IP NAME
                            cIndex++;
                            cIndex++;
                        }
                        //3.
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Own = curRow.GetCell(cIndex).ToString().Trim().ToUpper();
                            item.Own2 = VnHelper.ConvertToUnSign(item.Own);
                        }
                        cIndex++;
                        //4.note
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.NoteMono = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        //5
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();   
                            item.Tone = check == "1" ? true : false;
                        }
                        cIndex++;
                        //6
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Web = check == "1" ? true : false;                           
                        }
                        cIndex++;
                        //7
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Performances = check == "1" ? true : false;                            
                        }
                        cIndex++;
                        //8
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.PerformancesHCM = check == "1" ? true : false;                           
                        } 
                        cIndex++;
                        //9
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Cddvd = check == "1" ? true : false;                           
                        }
                        cIndex++;
                        //10
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Kok = check == "1" ? true : false;                           
                        }
                        cIndex++;
                        //11
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Broadcasting = check == "1" ? true : false;                            
                        }
                        cIndex++;
                        //12
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Entertaiment = check == "1" ? true : false;                          
                        }
                        cIndex++;
                        //13
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Film = check == "1" ? true : false;                            
                        }
                        cIndex++;
                        //14
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Advertisement = check == "1" ? true : false;                           
                        }
                        cIndex++;
                        //15
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.PubMusicBook = check == "1" ? true : false;                            
                        }
                        cIndex++;
                        //16
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Youtube = check == "1" ? true : false;                            
                        }
                        cIndex++;
                        //17
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString().Trim()))
                        {
                            string check = curRow.GetCell(cIndex).ToString().Trim();
                            item.Other = check == "1" ? true : false;                           
                        }
                        cIndex++;
                        //18
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {                           
                            try
                            {
                                item.StartTime = DateTime.Parse(curRow.GetCell(cIndex).ToString());
                            }
                            catch (Exception)
                            {
                                item.StartTime = new DateTime(9999, 1, 1, 0, 0, 0);
                            }                            
                        }
                        cIndex++;
                        //19
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {                            
                            try
                            {                                
                                item.EndTime = DateTime.Parse(curRow.GetCell(cIndex).ToString());
                            }
                            catch (Exception)
                            {
                                item.EndTime = new DateTime(9999, 1, 1, 0, 0, 0);
                            }                           
                        }
                        else
                        {
                            //int a = 1;
                        }    
                        cIndex++;
                        //20
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {                           
                            try
                            {
                                item.UpdateTime = DateTime.Parse(curRow.GetCell(cIndex).ToString());
                            }
                            catch (Exception)
                            {
                                item.UpdateTime = new DateTime(9999, 1, 1, 0, 0, 0);
                            }                          
                        }
                        cIndex++;
                        //21
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {                          
                            try
                            {
                                item.ReceiveTime = DateTime.Parse(curRow.GetCell(cIndex).ToString());
                            }
                            catch (Exception)
                            {
                                item.ReceiveTime = new DateTime(9999, 1, 1, 0, 0, 0);
                            }                            
                        }
                        cIndex++;
                        //22
                        //Ghi chú nhận tác phẩm
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Note2 = ConvertAllToUnicode.ConvertFromComposite(curRow.GetCell(cIndex).ToString().Trim());
                        }
                        cIndex++;
                        //23
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.IsExpired = false;
                        cIndex++;
                        //24
                        //Ghi chu hết hạn độc quyền
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString()))
                        {
                            item.Note3 = "";
                        }
                        cIndex++;                       
                        list.Add(item);
                    }
                }

                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region preclaim Matching        
        public List<PreclaimMatchingViewModel> ReadExcelImportPreClaimMatching(string fileName)
        {
            //using (var excel = SpreadsheetDocument.Open(fileName, false))
            //{

            //    var sheets = excel.WorkbookPart.WorksheetParts;

            //    // Loop through each of the sheets in the spreadsheet
            //    foreach (var wp in sheets)
            //    {
            //        Worksheet worksheet = wp.Worksheet;

            //        // Loop through each of the rows in the current sheet
            //        var rows = worksheet.GetFirstChild<SheetData>().Elements<DocumentFormat.OpenXml.Spreadsheet.Row>();
            //        foreach (var row in rows)
            //        {
            //            // Loop through each of the cells in the current row.
            //            var cells = row.Elements<Cell>();
            //            foreach (var cell in cells)
            //            {
            //                // Here is where you would do something with the values of the spreadsheet.
            //                string str = cell.CellValue.Text;
            //            }
            //        }
            //    }

            //    excel.Close();
            //}
           
            List<PreclaimMatchingViewModel> list = new List<PreclaimMatchingViewModel>();
            try
            {    
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }               
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1
                    PreclaimMatchingViewModel item;
                    IRow curRow;
                    int cIndex = 0;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = 1 - 1;
                            break;
                        }
                        item = new PreclaimMatchingViewModel();
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.SerialNo = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.TITLE = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ARTIST = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ALBUM = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.LABEL = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ISRC = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.COMP_ID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.COMP_TITLE = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.COMP_ISWC = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.COMP_WRITERS = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.COMP_CUSTOM_ID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.QUANTILE = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        list.Add(item);
                        item = null;
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        public bool ExportPreClaimMatching(List<PreclaimMatchingViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportPreClaimMatching(fullPath, out workbookpart, out sheetData);
                //detail part
                //
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    newRow.AppendChild(new Cell() 
                    { 
                        DataType = CellValues.InlineString, 
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ID)
                        } 
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.TITLE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ARTIST)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ALBUM)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.LABEL)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ISRC)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_ID)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_TITLE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_ISWC)
                        }
                    });

                    //

                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_WRITERS)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_CUSTOM_ID)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.QUANTILE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WorkCode)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsMatching.ToString())
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsSuccess.ToString())
                        }
                    });                  
                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
                sheetData = null;
                workbookpart = null;
                spreadsheetDocument = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportPreClaimMatching(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,ID,TITLE,ARTIST,ALBUM,LABEL,ISRC,COMP_ID,COMP_TITLE,COMP_ISWC,COMP_WRITERS,"+
                "COMP_CUSTOM_ID,QUANTILE,WORK CODE,IS MATCHING,IS SUCCESS";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }

        #endregion

        #region  Work Matching
        public List<WorkMatchingViewModel> ReadExcelImportWorkMatching(string fileName)
        {  
            List<WorkMatchingViewModel> list = new List<WorkMatchingViewModel>();
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1
                    WorkMatchingViewModel item;
                    IRow curRow;
                    int cIndex = 0;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = 1 - 1;
                            break;
                        }
                        item = new WorkMatchingViewModel();
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.SerialNo = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.WorkCode = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Title = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Writer = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;                        
                        list.Add(item);
                        item = null;
                    }
                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }
        public bool ExportWorkMatching(List<WorkMatchingViewModel> dataSource, string fullPath, int typeExport)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportWorkMatching(fullPath, typeExport, out workbookpart, out sheetData);
                //detail part
                int noRefer = 0;
                string writer = string.Empty;
                string[] writerList = null;
                foreach (var item in dataSource)
                {
                    noRefer++;
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    #region type1: upmis
                    if(typeExport ==0)
                    {                        
                        //Pool,Source,Date,
                        //1
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("PER") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("ABC") });
                        //TODO
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(DateTime.Now.ToString("d/M/yyyy"))
                        //    }
                        //});
                        //newRow.AppendChild(DateCell(DateTime.Now));
                        newRow.AppendChild(DateCell2(DateTime.Now));

                        ////TITLE (ENG),TITLE (LOCAL),
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Title2) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        //COMPOSER,PERFORMER (ENG),PERFORMER (LOCAL),PUBLISHER,                        
                        writerList = item.Writer2.Split(',');
                        writer = string.Empty;
                        for (int k = 0; k < writerList.Length; k++)
                        {
                            writer += $"{writerList[k]} ";
                        }
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(writer.Trim()) });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        //MIN,SEC,NO OF PERF,Amount,
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(noRefer.ToString(CultureInfo.InvariantCulture))});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });                       
                        //ISRC,ALBUM NAME,
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        //Work Int No,ISWC
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkCodeMatching.ToString(CultureInfo.InvariantCulture))});
                        newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                        ////2
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.String,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(DateTime.Now.ToString("d/M/yyyy"))
                        //    }
                        //});
                        ////3
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.Date,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(DateTime.Now.ToString("M/dd/yyyy"))
                        //    }
                        //});
                        ////TITLE (ENG),TITLE (LOCAL),
                        ////4
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Title2)
                        //    }
                        //});
                        //5
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        //COMPOSER,PERFORMER (ENG),PERFORMER (LOCAL),PUBLISHER,
                        //6
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Writer2)
                        //    }
                        //});
                        ////7
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Artist2)
                        //    }
                        //});
                        ////8
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        ////9
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        //MIN,SEC,NO OF PERF,Amount,
                        //10
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        ////11
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        ////12: 
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.Number,
                        //    noRefer

                        //    //InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    //{
                        //    //    Text = new DocumentFormat.OpenXml.Spreadsheet.Text(noRefer.ToString())
                        //    //}
                        //});
                        ////13
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        //ISRC,ALBUM NAME,
                        //14
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        ////15
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});
                        ////Work Int No,ISWC
                        ////16
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WorkCodeMatching)
                        //    }
                        //}); 
                        ////17
                        //newRow.AppendChild(new Cell()
                        //{
                        //    DataType = CellValues.InlineString,
                        //    InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        //    {
                        //        Text = new DocumentFormat.OpenXml.Spreadsheet.Text("")
                        //    }
                        //});  
                    }
                    #endregion

                    #region type 2
                    else 
                    {
                        //DAU VAO
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                        //TAC PHAM VAO
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.Number,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkCode.ToString())                      
                        
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.Number,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkcodeChangeToNew.ToString())

                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Title)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Title2)
                            }
                        });
                        //TAC GIA VAO
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Writer)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Writer2)
                            }
                        });
                        //NGHE SY VAO
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Artist)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Artist2)
                            }
                        });
                        //TAC PHAM MATCHING
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.Number,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkCodeMatching.ToString())
                            //InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            //{
                            //    Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WorkCodeMatching)
                            //}
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.TitleMatching)
                            }
                        });
                        //TAC GIA MATCHING
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.Number,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WriterCodeMatching.ToString())
                            //InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            //{
                            //    Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WriterCodeMatching)
                            //}
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.Number,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WriterIPNumberMatching.ToString())
                            //InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            //{
                            //    Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WriterIPNumberMatching)
                            //}
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WriterMatching)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.SocietyMatching)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WriterMatchingWithSoceity)
                            }
                        });
                        //NGHE SY MATCHING
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ArtistMatching)
                            }
                        });
                        //TANG THAI MATCHING
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsMatching.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsSuccess.ToString())
                            }
                        });
                        //GHI CHI MATCHING
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Messsage.ToString())
                            }
                        });
                        //DOC QUYEN TAC PHAM
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsWorkMonopoly.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WorkFields.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WorkMonopolyNote.ToString())
                            }
                        });
                        //DOC QUYEN tac gia
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IsMemberMonopoly.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.MemberFields.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.MemberMonopolyNote.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.NonMember.ToString())
                            }
                        });
                    }
                    #endregion
                    
                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportWorkMatching(string fullPath,int typeExport, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = string.Empty;
            //Upmis
            if(typeExport == 0)
            {
                //Pool												 			
                strHeader = "Pool,Source,Date,TITLE (ENG),TITLE (LOCAL)," +
                    "COMPOSER,PERFORMER (ENG)," +
                    "PERFORMER (LOCAL),PUBLISHER,MIN,SEC,NO OF PERF," +
                    "Amount,ISRC,ALBUM NAME,Work Int No,ISWC";               
            }
            else
            {                
                strHeader = "Serial No," +
                "WORKCODE,WORKCODE CHANGE NEW" +
                "TITLE,TITLE2," +
                "WRITER,WRITER2," +
                "ARTIST,ARTIST2," +
                "WORKCODE MATCHING,TITLE MATCHING," +
                "WRITECODE MATCHING,IP NUMBER MATCHING,WRITER MATCHING," +
                "SOCIETY MATCHING,WRITER_SOCIETY,ARTIST MATCHING," +
                "IS MATCHING,IS SUCCESS," +
                "MESSAGE," +
                "IS WORK MONO,WORK MONO FIELDS,WORK MONO NOTE," +
                "IS MEMBER MONO,MEMBER MONO FIELDS,MEMBER MONO NOTE," +
                "NonMember";                             
            }
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            //them
            // Style Part
            WorkbookStylesPart wbsp = workbookpart.AddNewPart<WorkbookStylesPart>();
            wbsp.Stylesheet = CreateStylesheet();
            wbsp.Stylesheet.Save();
            //end them

            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        private static Stylesheet CreateStylesheet()
        {
            Stylesheet ss = new Stylesheet();

            var nfs = new NumberingFormats();
            var nformatDateTime = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(1),
                FormatCode = StringValue.FromString("dd/mm/yyyy")
            };
            nfs.Append(nformatDateTime);
            ss.Append(nfs);

            return ss;
        }
        #endregion

        #region  Work Search       
        public bool ExportSearch(List<WorkViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportSearch(fullPath, out workbookpart, out sheetData);
                //detail part
                //
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WK_INT_NO)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.TTL_ENG)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ISWC_NO)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ISRC)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.WRITER)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ARTIST)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.SOC_NAME)
                        }
                    });
                    

                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportSearch(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,WK_INT_NO,TTL_ENG,ISWC_NO,ISRC,WRITER,ARTIST,SOC_NAME";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }

        #endregion

        #region CMS claim tool
        public List<CMSViewModel> ReadExcelImportCMClaim(string fileName)
        {
            int indexest = 0;
            List<CMSViewModel> dis = new List<CMSViewModel>();           
            try
            {
                IWorkbook workbook = null;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.                    
                    // If first row is table head, i starts from 1                  
                    int cIndex = 0;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        cIndex = 0;
                        IRow curRow = sheet.GetRow(i);
                        if (curRow.GetCell(0) != null && string.IsNullOrEmpty(curRow.GetCell(0).ToString()))
                        {                           
                            break;
                        }
                        CMSViewModel item = new CMSViewModel();

                        #region Distribution
                        //KY2fVDpCI5M
                        //KY2fVDpCI5M
                        //1-10
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.IssueID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;                       
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.IssueType = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.OtherParty = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ExpiryDate = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AssetName = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AssetType = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AssetCreatedOn = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AssetID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ReferenceID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ISRC = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        //11-20
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.UPC = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.CustomID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Labels = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ISWC = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.OverlappingAssetID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.OverlappingAssetName = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.VideoID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.VideoTitle = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ChannelName = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ChannelID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        //21-30
                        #endregion
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ClaimID = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.MatchType = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ViewsAffectedDaily = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ViewsLifetime = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.ClaimedVideosAffected = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;

                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.DurationTimeSeconds = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.DurationPercentageReference = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.DurationPercentageVideo = int.Parse(curRow.GetCell(cIndex).ToString().Trim());
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.Status = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.StatusDetail = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        //31-34
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.LinkToIssue = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.PersonMatching = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AutoStatus = false;
                        cIndex++;
                        if (curRow.GetCell(cIndex) != null && !string.IsNullOrEmpty(curRow.GetCell(cIndex).ToString())) item.AutoNote = curRow.GetCell(cIndex).ToString().Trim();
                        cIndex++;                        
                        dis.Add(item);
                    }

                }
                sheet = null;
                workbook = null;
                fs.Close();
                fs = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                int a = indexest;
                throw ex;
            }
            return dis;
        }

        public bool ExportCMS(List<CMSViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportCMS(fullPath, out workbookpart, out sheetData);
                //detail part
                foreach (var item in dataSource)
                {                    
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IssueID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.IssueType) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.OtherParty) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ExpiryDate) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AssetName) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AssetType) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AssetCreatedOn) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AssetID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ReferenceID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ISRC) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.UPC) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.CustomID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Labels) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ISWC) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.OverlappingAssetID) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.OverlappingAssetName) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.VideoID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.VideoTitle) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ChannelName) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ChannelID) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ClaimID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.MatchType) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ViewsAffectedDaily.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ViewsLifetime.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ClaimedVideosAffected.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DurationTimeSeconds.ToString()) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DurationPercentageReference.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DurationPercentageVideo.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Status) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.StatusDetail) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LinkToIssue) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.PersonMatching) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AutoStatus.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.AutoNote) });
                    
                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportCMS(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Issue ID,Issue Type,Other Party,Expiry Date,Asset Name,Asset Type,Asset Created On,Asset ID,Reference ID,"+	
                "ISRC,UPC,Custom ID,Labels,ISWC,Overlapping Asset ID,Overlapping Asset Name,"+
                "Video ID,Video Title,Channel Name,Channel ID,Claim ID,Match Type,"+
                "Views Affected Daily,Views Lifetime,Claimed Videos Affected,"+
                "Duration Time (Seconds),Duration Percentage Reference,Duration Percentage Video,Status,Status Detail," +
                "Link To Issue,Person matching, Auto status, auto note";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }

        #endregion
        /// <summary>
        ///  Attemps to read workbook as XLSX, then XLS, then fails.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public NPOI.SS.UserModel.IWorkbook ReadWorkbook(string path)
        {
            NPOI.SS.UserModel.IWorkbook book = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                // Try to read workbook as XLSX:
                try
                {
                    book = new XSSFWorkbook(fs);
                }
                catch (Exception)
                {
                    book = null;
                    //MessageBox.Show($"Read file is error, {ex1.ToString()}");
                }

                // If reading fails, try to read workbook as XLS:
                if (book == null)
                {
                    book = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                }
            }
            catch (Exception )
            {
                //MessageBox.Show($"Read file is error, {ex.ToString()}");
            }
            return book;
        }

        public bool Write(List<YoutubeFileItems> dataSource, string fullPath)
        {
            bool check = false;
            try
            {
                //1.header
                string strHeader = "ID,TITLE,ARTIST,ALBUM,LABEL,ISRC,COMP_ID,COMP_TITLE,COMP_ISWC,COMP_WRITERS,COMP_CUSTOM_ID,QUANTILE,NOTE";
                string[] strArray = strHeader.Split(',');                
                
                XSSFWorkbook wb = new XSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet = wb.CreateSheet("Sheet1");   
                var row1 = sheet.CreateRow(0);
                for (int i = 0; i < strArray.Length; i++)
                {
                    row1.CreateCell(i).SetCellValue(strArray[i]);
                }              
               
                int rowIndex = 1;
                foreach (var item in dataSource)
                {                    
                    var row = sheet.CreateRow(rowIndex);

                    row.CreateCell(0).SetCellValue(item.ID);
                    row.CreateCell(1).SetCellValue(item.TITLE);
                    row.CreateCell(2).SetCellValue(item.TITLE2);
                    row.CreateCell(3).SetCellValue(item.ARTIST);
                    row.CreateCell(4).SetCellValue(item.ARTIST2);
                    row.CreateCell(5).SetCellValue(item.ALBUM);
                    row.CreateCell(6).SetCellValue(item.ALBUM2);
                    row.CreateCell(7).SetCellValue(item.LABEL);
                    row.CreateCell(8).SetCellValue(item.LABEL2);
                    row.CreateCell(9).SetCellValue(item.ISRC);
                       
                    row.CreateCell(10).SetCellValue(item.COMP_ID);
                    row.CreateCell(11).SetCellValue(item.COMP_TITLE);
                    row.CreateCell(12).SetCellValue(item.COMP_ISWC);
                    row.CreateCell(13).SetCellValue(item.COMP_WRITERS);
                    row.CreateCell(14).SetCellValue(item.COMP_CUSTOM_ID);
                    row.CreateCell(15).SetCellValue(item.QUANTILE.ToString());
                    row.CreateCell(16).SetCellValue(item.DetectLanguage);
                    row.CreateCell(17).SetCellValue(item.Note);
                    rowIndex++;
                }
                FileStream fs = new FileStream(fullPath, FileMode.CreateNew);
                wb.Write(fs);
                check = true;
            }
            catch (Exception)
            {
              
            }
            return check;
        }

        #region MasterList
        /// <summary>
        /// Luu du lieu xuong file
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <param name="totaRecordInlFile"></param>
        /// <returns></returns>
        public bool WriteToOxml(List<YoutubeFileItems> dataSource, string fullPath, string name, long totaRecordInlFile)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;
                
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                spreadsheetDocument = CreatenewFile(fullPath, name, out workbookpart, out sheetData, preFile);

                foreach (var item in dataSource)
                {
                    total++;
                    #region create file
                    if (count > totaRecordInlFile)
                    {
                        count = 1;
                        //save
                        workbookpart.Workbook.Save();
                        spreadsheetDocument.Close();
                        //reate new file
                        preIndexFile++;
                        preFile = preIndexFile.ToString().PadLeft(4, '0');
                        spreadsheetDocument = CreatenewFile(fullPath, name, out workbookpart, out sheetData, preFile);
                    }
                    #endregion
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ARTIST) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ARTIST2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ALBUM) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ALBUM2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LABEL) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LABEL2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ISRC) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_TITLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_ISWC) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_WRITERS) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_CUSTOM_ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.QUANTILE.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DetectLanguage) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Note) });

                    sheetData.AppendChild(newRow);
                    count++;
                }

                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        /// <summary>
        /// tao file
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <param name="workbookpart"></param>
        /// <param name="sheetData"></param>
        /// <param name="preFile"></param>
        /// <returns></returns>
        private static SpreadsheetDocument CreatenewFile(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData, string preFile)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header
            string strHeader = "ID,TITLE,ARTIST,ALBUM,LABEL,ISRC,COMP_ID,COMP_TITLE,COMP_ISWC,COMP_WRITERS,COMP_CUSTOM_ID,QUANTILE,NOTE";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}{name}-{preFile}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);

            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }

        /// <summary>
        /// Luu du lieu xuong file
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <param name="totaRecordInlFile"></param>
        /// <returns></returns>
        public bool WriteToOxmlNew(List<YoutubeFileItems> dataSource, string fullPath, string name)
        {
            int count = 1;
            bool check = false;           
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                spreadsheetDocument = CreatenewFileNew(fullPath, name, out workbookpart, out sheetData);

                foreach (var item in dataSource)
                {
                    total++;                    
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.TITLE2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ARTIST) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ARTIST2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ALBUM) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ALBUM2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LABEL) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.LABEL2) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.ISRC) });

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_TITLE) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_ISWC) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_WRITERS) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.COMP_CUSTOM_ID) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.QUANTILE.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.DetectLanguage) });
                    //newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Note) });

                    sheetData.AppendChild(newRow);
                    count++;
                }

                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        /// <summary>
        /// tao file
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        /// <param name="workbookpart"></param>
        /// <param name="sheetData"></param>
        /// <param name="preFile"></param>
        /// <returns></returns>
        private static SpreadsheetDocument CreatenewFileNew(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header
            string strHeader = "ID,TITLE,TITLE2,ARTIST,ARTIST2,ALBUM,ALBUM2,LABEL,LABEL2,ISRC,COMP_ID,COMP_TITLE,COMP_ISWC,COMP_WRITERS,COMP_CUSTOM_ID,QUANTILE";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);

            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region Distribution 
        public bool WriteToDistribution(List<BhDistributionViewModel> dataSource, string fullPath, string name, string strBh, string strMmeberVn, DateTime date)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = CreatenewFileDistribution(fullPath, name, out workbookpart, out sheetData, strBh, strMmeberVn);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;                    
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.No.ToString()) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.WorkInNo) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Title) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.PoolName) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SourceName) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Role) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Share.ToString("###.00")) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Royalty2.ToString("###.000")) });

                    sheetData.AppendChild(newRow);
                    count++;
                }
                //total part
                decimal Royalty2Total = decimal.Parse("0");
                for (int i = 0; i < dataSource.Count; i++)
                {
                    Royalty2Total = Royalty2Total + dataSource[i].Royalty2;
                }
                var cellTotal = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Total") });
                cellTotal.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Royalty2Total.ToString("###.000")) });
                sheetData.AppendChild(cellTotal);
                //footer

                var cellFooter = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });           
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(
                        $"Ngày {date.Day.ToString().PadLeft(2,'0')} Tháng {date.Month.ToString().PadLeft(2, '0')} Năm {date.Year.ToString().PadLeft(4, '0')}") });
                sheetData.AppendChild(cellFooter);

                cellFooter = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("(Ký và ghi rõ họ tên)")});
                sheetData.AppendChild(cellFooter);

                cellFooter = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                sheetData.AppendChild(cellFooter);

                cellFooter = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                sheetData.AppendChild(cellFooter);

                cellFooter = new DocumentFormat.OpenXml.Spreadsheet.Row();
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("") });
                cellFooter.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strMmeberVn) });
                sheetData.AppendChild(cellFooter);

                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }

        private static SpreadsheetDocument CreatenewFileDistribution(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData, string strBh, string member)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header
            //STT,	WORK INT NO	,TITLE	,POOL NAME	,SOURCE NAME	,ROLE	 ,SHARE 	 ,ROYALTY 

            string strHeader = "NO.,WORK INT NO,TITLE,POOL NAME,SOURCE NAME,ROLE,SHARE,ROYALTY";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //add title
            //1.member
            if(member.Length>0)
            {
                var rowMmeber = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var celMember = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(member) };
                rowMmeber.AppendChild(celMember);
                sheetData.AppendChild(rowMmeber);
            }
            //2.Bh
            if (strBh.Length > 0)
            {
                var rowBhmedia = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var cellBh = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strBh) };
                rowBhmedia.AppendChild(cellBh);
                sheetData.AppendChild(rowBhmedia);
            }
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region BhAggregates
        public bool WriteToBhAggregates(List<BhAggregationViewModel> dataSource, string fullPath, string name, string strBh, string strMmeberVn, DateTime date)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = CreatenewBhAggregates(fullPath, name, out workbookpart, out sheetData, strBh, strMmeberVn);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Type) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.BhAuthor) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Count_total.ToString())});
                    decimal Royalty = item.Royalty == null ? 0 : (decimal)item.Royalty;
                    decimal Royalty2 = item.Royalty2 == null ? 0 : (decimal)item.Royalty2;
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Royalty.ToString("#.###")) });
                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Royalty2.ToString("#.###")) });

                    sheetData.AppendChild(newRow);
                    count++;
                }   
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }

        private static SpreadsheetDocument CreatenewBhAggregates(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData, string strBh, string member)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header
            //STT,	WORK INT NO	,TITLE	,POOL NAME	,SOURCE NAME	,ROLE	 ,SHARE 	 ,ROYALTY 

            string strHeader = "TYPE,OBJECT,TOTAL COUNT,ROYALTY,ROYALTY";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //add title
            //1.member
            if (member.Length > 0)
            {
                var rowMmeber = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var celMember = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(member) };
                rowMmeber.AppendChild(celMember);
                sheetData.AppendChild(rowMmeber);
            }
            //2.Bh
            if (strBh.Length > 0)
            {
                var rowBhmedia = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var cellBh = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strBh) };
                rowBhmedia.AppendChild(cellBh);
                sheetData.AppendChild(rowBhmedia);
            }
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        public bool WriteToBhAggregates2(List<BhAggregation2ViewModel> dataSource, string fullPath, string name, string strBh, string strMmeberVn, DateTime date)
        {
            int count = 1;
            bool check = false;
            int preIndexFile = 1;
            string preFile = preIndexFile.ToString().PadLeft(4, '0');
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";

                long total = 0;

                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = CreatenewBhAggregates2(fullPath, name, out workbookpart, out sheetData, strBh, strMmeberVn);
                //detail part
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    newRow.AppendChild(new Cell() { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.BhAuthor) });

                    int sl = item.sltotalOriginal == null ? 0 : (int)item.sltotalOriginal;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(sl.ToString()) });
                    sl = item.sltotalAuthor == null ? 0 : (int)item.sltotalAuthor;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(sl.ToString()) });
                    sl = item.sltotalBhmedia == null ? 0 : (int)item.sltotalBhmedia;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(sl.ToString()) });
                    sl = item.sltotalExcept == null ? 0 : (int)item.sltotalExcept;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(sl.ToString()) });
                    sl = item.sltotalhold == null ? 0 : (int)item.sltotalhold;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(sl.ToString()) });

                    decimal value = item.totalOriginal == null ? 0 : (decimal)item.totalOriginal;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value.ToString("#.###")) });
                    value = item.totalAuthor == null ? 0 : (decimal)item.totalAuthor;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value.ToString("#.###")) });
                    value = item.totalBhmedia == null ? 0 : (decimal)item.totalBhmedia;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value.ToString("#.###")) });
                    value = item.totalExcept == null ? 0 : (decimal)item.totalExcept;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value.ToString("#.###")) });
                    value = item.totalhold == null ? 0 : (decimal)item.totalhold;
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value.ToString("#.###")) });


                    sheetData.AppendChild(newRow);
                    count++;
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument CreatenewBhAggregates2(string fullPath, string name, out WorkbookPart workbookpart, out SheetData sheetData, string strBh, string member)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header
            //STT,	WORK INT NO	,TITLE	,POOL NAME	,SOURCE NAME	,ROLE	 ,SHARE 	 ,ROYALTY 

            string strHeader = "MEMBER,ORIGINAL COUNT,AUTHOR COUNT,BHMEDIA COUNT,EXCEPT COUNT,HOLD COUNT,ORIGINAL VALUE,AUTHOR VALUE,BHMEDIA VALUE,EXCEPT VALUE,HOLD VALUE";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);


            var bold1 = new Bold();
            CellFormat cf = new CellFormat();


            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            sheets.Append(sheet);
            //add title
            //1.member
            if (member.Length > 0)
            {
                var rowMmeber = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var celMember = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(member) };
                rowMmeber.AppendChild(celMember);
                sheetData.AppendChild(rowMmeber);
            }
            //2.Bh
            if (strBh.Length > 0)
            {
                var rowBhmedia = new DocumentFormat.OpenXml.Spreadsheet.Row();
                var cellBh = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strBh) };
                rowBhmedia.AppendChild(cellBh);
                sheetData.AppendChild(rowBhmedia);
            }
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region masterlist source
        public bool ExportMasterListSource(List<MasterSourceViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportMasterListSource(fullPath, out workbookpart, out sheetData);
                //detail part
               
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    //int a = int.Parse("d");
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Source)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ID)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.TITLE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.TITLE2)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ARTIST)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ALBUM)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.LABEL)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ISRC)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_ID)
                        }
                    });
                   
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_TITLE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_TITLE2)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_ISWC)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.AT)
                        }
                    });

                    //

                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_WRITERS)
                        }
                    });                   
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.COMP_CUSTOM_ID)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.QUANTILE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.C_Workcode)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.CODE)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Percent)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.CODE_RIGHT)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Note)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ScoreCompare.ToString())
                        }
                    });
                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
                sheetData = null;
                workbookpart = null;
                spreadsheetDocument = null;
                GC.Collect();
            }
            catch (Exception)
            {
                throw;
                //string mes = ex.ToString();
            }
            return check;
        }
       
        private static SpreadsheetDocument ExportMasterListSource(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,Source,ID,TITLE,TITLE2,ARTIST,ALBUM,LABEL,ISRC,COMP_ID,COMP_TITLE,COMP_TITLE2,COMP_ISWC,AT,COMP_WRITERS,COMP_CUSTOM_ID," +
                "QUANTILE,C_Workcode,CODE,Percent,CODE_RIGHT,Note,ScoreCompare";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region monopoly
        public bool ExportMonopoly(List<MonopolyViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportMonopoly(fullPath, out workbookpart, out sheetData);
                //detail part
                //
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.SerialNo.ToString()) });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Group.ToString())
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.CodeOld)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.CodeNew)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Name)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.NameType)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Own)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.NoteMono)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Tone == false? 
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0"):
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Web == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Performances == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.PerformancesHCM == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Cddvd == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Kok == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Broadcasting == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Entertaiment == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Film == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Advertisement == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.PubMusicBook == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Youtube == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.Other == false ?
                                new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });                    
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.StartTime.ToString("dd/MM/yyyy"))
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.EndTime.ToString("dd/MM/yyyy"))
                        }
                    });                    
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.UpdateTime.ToString("dd/MM/yyyy"))
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.ReceiveTime.ToString("dd/MM/yyyy"))
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Note2)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = item.IsExpired == false ?
                                 new DocumentFormat.OpenXml.Spreadsheet.Text("0") :
                                 new DocumentFormat.OpenXml.Spreadsheet.Text("1")
                        }
                    });                    
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Note3)
                        }
                    });
                   
                    sheetData.AppendChild(newRow);
                    count++;
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
                sheetData = null;
                workbookpart = null;
                spreadsheetDocument = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportMonopoly(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,Group,CodeOld,CodeNew,Name,NameType,Own,NoteMono," +
                "Tone,Web,Performances,PerformancesHCM,Cddvd," +
                "Kok,Broadcasting,Entertaiment,Film,Advertisement," +
                "PubMusicBook,Youtube,Other," +
                "StartTime,EndTime,UpdateTime,ReceiveTime,Note receive,Expired,Note Expired";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region work
        public bool ExportWork(List<WorkViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportWork(fullPath, out workbookpart, out sheetData);
                //detail part
                string WK_INT_NO = string.Empty;
                string TTL_ENG = string.Empty;
                string ISWC_NO = string.Empty;
                string ISRC = string.Empty;
                string WRITER = string.Empty;
                string ARTIST = string.Empty;
                string SOC_NAME = string.Empty;
                foreach (var item in dataSource)
                {
                    WK_INT_NO = item.WK_INT_NO;
                    TTL_ENG = item.TTL_ENG;
                    ISWC_NO = item.ISWC_NO;
                    ISRC = item.ISRC;
                    WRITER = item.WRITER;
                    ARTIST = item.ARTIST;
                    SOC_NAME = item.SOC_NAME;
                    foreach (var subI in item.InterestedParties)
                    {
                        total++;
                        var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        //3.code
                        newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(total.ToString()) });
                        //tac pham
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(WK_INT_NO)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(TTL_ENG)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(ISWC_NO)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(ISRC)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(WRITER)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(ARTIST)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.SOC_NAME)
                            }
                        });
                        //tac gia
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_INT_NO)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_NUMBER)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_NAME)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_NAME_LOCAL)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_NAMETYPE)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.IP_WK_ROLE)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.Society)
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.PER_COL_SHR.ToString())
                            }
                        });
                        newRow.AppendChild(new Cell()
                        {
                            DataType = CellValues.InlineString,
                            InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                            {
                                Text = new DocumentFormat.OpenXml.Spreadsheet.Text(subI.MEC_COL_SHR.ToString())
                            }
                        });
                        sheetData.AppendChild(newRow);
                        count++;
                    }
                    #region MyRegion

                    #endregion
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportWork(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,WK_INT_NO,TTL_ENG,ISWC_NO,ISRC,WRITER,ARTIST,SOC_NAME,IP_INT_NO,IP_NUMBER,IP_NAME,IP_NAME_LOCAL,IP_NAMETYPE,IP_WK_ROLE,SOCIETY,PER_COL_SHR,MEC_COL_SHR";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion

        #region member
        public bool ExportMember(List<MemberViewModel> dataSource, string fullPath)
        {
            int count = 1;
            bool check = false;
            try
            {
                //const string fileName = @"C:\MyExcel.xlsx";
                long total = 0;
                SpreadsheetDocument spreadsheetDocument;
                WorkbookPart workbookpart;
                SheetData sheetData;
                //header part
                spreadsheetDocument = ExportMember(fullPath, out workbookpart, out sheetData);
               
                foreach (var item in dataSource)
                {
                    total++;
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    //3.code
                    newRow.AppendChild(new Cell() { DataType = CellValues.Number, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(total.ToString()) });
                    //tac pham
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IpiNumber)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.InternalNo)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IpEnglishName)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.IpLocalName)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.NameType)
                        }
                    });
                    newRow.AppendChild(new Cell()
                    {
                        DataType = CellValues.InlineString,
                        InlineString = new DocumentFormat.OpenXml.Spreadsheet.InlineString
                        {
                            Text = new DocumentFormat.OpenXml.Spreadsheet.Text(item.Society)
                        }
                    });
                    sheetData.AppendChild(newRow);
                    count++;
                }
                //save
                if (sheetData != null && sheetData.Count() > 0)
                {
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
                check = true;
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
            return check;
        }
        private static SpreadsheetDocument ExportMember(string fullPath, out WorkbookPart workbookpart, out SheetData sheetData)
        {          
            SpreadsheetDocument spreadsheetDocument;
            //1.header  
            string strHeader = "Serial No,IpiNumber,InternalNo,IpEnglishName,IpLocalName,NameType,Society";
            string[] strArray = strHeader.Split(',');
            //Delete the file if it exists. 
            //fullPath = $"{fullPath}\\{name}.xlsx";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            //This is the first time of creating the excel file and the first sheet.
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            spreadsheetDocument = SpreadsheetDocument.
                Create(fullPath, SpreadsheetDocumentType.Workbook);
            // Add a WorkbookPart to the document.
            workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            // Add a WorksheetPart to the WorkbookPart.
            /// Add a WorkbookPart to the document.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            // Add a WorksheetPart to the WorkbookPart.
            sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);
            var bold1 = new Bold();
            CellFormat cf = new CellFormat();
            // Add Sheets to the Workbook.
            Sheets sheets;
            sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());
            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = sheetId,
                Name = "Sheet" + sheetId
            };
            //gan sheet
            sheets.Append(sheet);
            //Add Header Row.
            var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            for (int i = 0; i < strArray.Length; i++)
            {
                var cell = new Cell { DataType = CellValues.String, CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strArray[i]) };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            return spreadsheetDocument;
        }
        #endregion


        #region Common
        private Cell BuildHyperlinkCell(string url, object data) =>
            new Cell
            {
                DataType = new EnumValue<CellValues>(CellValues.String),
                CellFormula = new CellFormula($"HyperLink(\"{url}\")"),
                StyleIndex = 4u,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(data.ToString())
            };
        private Cell DateCell(DateTime date) =>
            new Cell
            {
                DataType = new EnumValue<CellValues>(CellValues.Date),
                CellFormula = new CellFormula($"DATE({date.Year}, {date.Month}, {date.Day})"),               
                StyleIndex = 14,
                //CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(data.ToString())
            };
        private Cell DateCell2(DateTime date)
        {
            ////string strValue = date.ToOADate().ToString(CultureInfo.InvariantCulture);           
            //string strValue = date.ToString("d/M/yyyy");
            //Cell cell1 = new Cell {
            //    DataType = new EnumValue<CellValues>(CellValues.Date),
            //    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(date.ToString("d/M/yyyy")),
            //    StyleIndex = 14
            //}; 
            //Cell cell2 = new Cell
            //{
            //    DataType = new EnumValue<CellValues>(CellValues.Date),
            //    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(date.ToShortDateString()),
            //    StyleIndex = 14
            //};
            //Cell cell3 = new Cell();
            //string dataMemberS = date.ToOADate().ToString();  //OA Date needed to export number as Date  
            //cell3.DataType = CellValues.Number;
            //cell3.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dataMemberS);
            //cell3.StyleIndex = 4;

            Cell cell4 = new Cell();
            cell4.DataType = CellValues.Number;
            cell4.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(date.ToOADate().ToString());
            cell4.StyleIndex = 1; // <= here I try to apply the style...
            return cell4;
//            return new Cell
//            {
//                /*ID  FORMAT CODE
//0   General,1   0,2   0.00,3   #,##0,4   #,##0.00,9   0%,10  0.00%,11  0.00E+00,12  # ?/?,13  # ??/??
//14  d/m/yyyy,15  d-mmm-yy,16  d-mmm,17  mmm-yy,18  h:mm tt,19  h:mm:ss tt,20  H:mm,21  H:mm:ss,22  m/d/yyyy H:mm,37  #,##0 ;(#,##0)
//38  #,##0 ;[Red](#,##0),39  #,##0.00;(#,##0.00),40  #,##0.00;[Red](#,##0.00),45  mm:ss
//46  [h]:mm:ss,47  mmss.0,48  ##0.0E+0,49  @*/
//                DataType = new EnumValue<CellValues>(CellValues.Date),
//                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(strValue),
//                StyleIndex = 14
//            };
        }
        
        #endregion

    }
}
