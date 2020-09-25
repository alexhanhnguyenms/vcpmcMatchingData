using Vcpmc.Mis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; } = string.Empty;
    }
}