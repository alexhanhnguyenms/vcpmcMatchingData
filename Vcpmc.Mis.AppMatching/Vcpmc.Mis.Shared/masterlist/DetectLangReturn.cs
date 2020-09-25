using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared.masterlist
{
    public class DetectLangReturn
    {
        public int TotalWord { get; set; } = 0;
        public int CorrectWord { get; set; } = 0;
        public decimal Score { get; set; } = 0;
    }
}
