using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; } = false;

        public string Message { get; set; } = string.Empty;

        public T ResultObj { get; set; }
    }
}