using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.Application.System.Roles
{
    public interface IRoleService2
    {
        Task<PagedResult<RoleViewModel>> GetAllPaging(IMapper _mapper);
    }
}
