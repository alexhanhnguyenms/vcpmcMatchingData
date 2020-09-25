using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.ApplicationCore.Entities
{
    /// <summary>
    /// Report type
    /// </summary>
    public class ReportType
    {        
        public bool File1 { get; set; } = false;
        public bool File2 { get; set; } = false;
        public bool File3 { get; set; } = false;
        public bool File3_vi { get; set; } = false;
        public bool File3_non_vi { get; set; } = false;
        public bool File3_Affter_non_vi { get; set; } = false;
        public bool File4_vi { get; set; } = false;
        public bool File4_non_vi { get; set; } = false;
        public bool File4_Affter_non_vi { get; set; } = false;
        public bool File5_vi { get; set; } = false;
        public bool File5_non_vi { get; set; } = false;
        public bool File5_Affter_non_vi { get; set; } = false;
    }
}
