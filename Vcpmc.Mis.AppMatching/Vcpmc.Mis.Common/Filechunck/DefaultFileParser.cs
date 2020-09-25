using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Vcpmc.Mis.Common.Filechunck
{
    public class DefaultFileParser : IFileParser
    {       
        /// <summary>
        /// Giới hạn số dàng
        /// </summary>
        int _chunkRowLimit = 3;
        string _connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_chunkRowLimit">Giới hạn số dòng</param>
        /// <param name="strConnect">Chuỗi kết nối database</param>
        public DefaultFileParser(int _chunkRowLimit,string strConnect)
        {
            this._chunkRowLimit = _chunkRowLimit;           
            this._connectionString = strConnect;// ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        }
        /// <summary>
        /// Cắt dữ liệu ra nhiều file
        /// </summary>
        /// <param name="sourceFileFullName"></param>
        /// <returns></returns>
        IEnumerable<DataTable> IFileParser.GetFileData(string sourceFileFullName)
        {
            //tao cot của bảng
            bool firstLineOfChunk = true;
            int chunkRowCount = 0;
            DataTable chunkDataTable = null;
            string columnData = null;
            //neu dong dau tien khong doc
            bool firstLineOfFile = true;
            using (var sr = new StreamReader(sourceFileFullName))
            {
                string line = null;
                //Read and display lines from the file until the end of the file is reached.                
                while ((line = sr.ReadLine()) != null)
                {
                    //when reach first line it is column list need to create datatable based on that.
                    if (firstLineOfFile)
                    {
                        columnData = line;
                        firstLineOfFile = false;
                        continue;
                    }
                    if (firstLineOfChunk)
                    {
                        firstLineOfChunk = false;
                        chunkDataTable = CreateEmptyDataTable(columnData);
                    }
                    AddRow(chunkDataTable, line);
                    chunkRowCount++;

                    if (chunkRowCount == _chunkRowLimit)
                    {
                        firstLineOfChunk = true;
                        chunkRowCount = 0;
                        yield return chunkDataTable;
                        chunkDataTable = null;
                    }
                }
            }
            //return last set of data which less then chunk size
            if (null != chunkDataTable)
                yield return chunkDataTable;
        }

        /// <summary>
        /// tạo table
        /// </summary>
        /// <param name="firstLine"></param>
        /// <returns></returns>
        private DataTable CreateEmptyDataTable(string firstLine)
        {
            IList<string> columnList = Split(firstLine);
            var dataTable = new DataTable("Data");
            dataTable.Columns.AddRange(columnList.Select(v => new DataColumn(v)).ToArray());
            return dataTable;
        }
        /// <summary>
        /// Thêm dòng
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="line"></param>
        private void AddRow(DataTable dataTable, string line)
        {
            DataRow newRow = dataTable.NewRow();
            IList<string> fieldData = Split(line);
            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                newRow[columnIndex] = fieldData[columnIndex];
            }
            dataTable.Rows.Add(newRow);
        }

        private IList<string> Split(string input)
        {
            //our csv file will be tab delimited
            var dataList = new List<string>();
            foreach (string column in input.Split('\t'))
            {
                dataList.Add(column);
            }
            return dataList;
        }
        /// <summary>
        /// Ghi dữ liệu ra nhiều file
        /// </summary>
        /// <param name="table"></param>
        /// <param name="distinationTable"></param>
        /// <param name="mapList"></param>
        void IFileParser.WriteChunkData(DataTable table, string distinationTable, IList<KeyValuePair<string, string>> mapList)
        {
            using (var bulkCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default))
            {
                bulkCopy.BulkCopyTimeout = 0;//unlimited
                bulkCopy.DestinationTableName = distinationTable;
                foreach (KeyValuePair<string, string> map in mapList)
                {
                    bulkCopy.ColumnMappings.Add(map.Key, map.Value);
                }
                bulkCopy.WriteToServer(table, DataRowState.Added);
            }
        }
    }
}
