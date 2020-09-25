using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.MasterLists
{
    public class MasterSourceViewModel
    {
        public int SerialNo { get; set; } = 0;       
        public string Source { get; set; } = string.Empty;       
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát 
        /// </summary>
        public string TITLE { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề bài hát 2
        /// </summary>
        public string TITLE2 { get; set; } = string.Empty;
        /// <summary>
        /// Nghệ sĩ 
        /// </summary>
        public string ARTIST { get; set; } = string.Empty;		
        public string ALBUM { get; set; } = string.Empty;
        public string LABEL { get; set; } = string.Empty;
        public string ISRC { get; set; } = string.Empty;
        public string COMP_ID { get; set; } = string.Empty;
        public string COMP_TITLE { get; set; } = string.Empty;
        public string COMP_TITLE2 { get; set; } = string.Empty;
        public string COMP_ISWC { get; set; } = string.Empty;
        public string AT { get; set; } = string.Empty;
        public string COMP_WRITERS { get; set; } = string.Empty;
        public string COMP_CUSTOM_ID { get; set; } = string.Empty;
        public string QUANTILE { get; set; } = string.Empty;
        public string C_Workcode { get; set; } = string.Empty;
        public string CODE { get; set; } = string.Empty;
        public string Percent { get; set; } = string.Empty;
        public string CODE_RIGHT { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal ScoreCompare { get; set; } = 0;

    }
}
