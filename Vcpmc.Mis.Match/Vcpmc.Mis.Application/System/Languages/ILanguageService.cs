using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Languages;
using Vcpmc.Mis.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Application.System.Languages
{
    public interface ILanguageService
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}