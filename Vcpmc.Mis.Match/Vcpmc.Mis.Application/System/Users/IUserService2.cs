using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.Application.System.Users
{
    public interface IUserService2
    {
        #region Get
        Task<PagedResult<UserViewModel>> GetAllPaging(GetUserPagingRequest request, IMapper _mapper);
        Task<UserViewModel> GetUserByUsername(string username, IMapper _mapper);
        //Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        #endregion

        #region Đang nhập
        Task<ApiResult<string>> Authencate(LoginRequest request);       
        #endregion

        #region update
        Task<UpdateStatusViewModel> Register(UserCreateRequest request, IMapper _mapper);
        Task<UpdateStatusViewModel> Update(UserUpdateRequest request, IMapper _mapper);
        Task<UpdateStatusViewModel> Delete(string id, IMapper _mapper);
        #endregion
    }
}
