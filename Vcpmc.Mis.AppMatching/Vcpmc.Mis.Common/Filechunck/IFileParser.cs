using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Vcpmc.Mis.Common.Filechunck
{
    public interface IFileParser
    {
        IEnumerable<DataTable> GetFileData(string sourceDirectory);
        void WriteChunkData(DataTable table, string distinationTable, IList<KeyValuePair<string, string>> mapList);
    }
}
