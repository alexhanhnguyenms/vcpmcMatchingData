using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.ViewModels.Common;

namespace Vcpmc.Mis.ViewModels.System.Para
{
    public class GetFixParameterPagingRequest: PagingRequestBase
    {
        /// <summary>
        /// Loai
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; } = string.Empty;
    }
}
