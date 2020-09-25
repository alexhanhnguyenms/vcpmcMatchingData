using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Admin;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.Controllers.Admin
{
    public class UserController
    {
        private readonly UserApiClient _apiClient;
        private readonly RoleApiClient _apiClientRole;
        public UserController(UserApiClient apiClient, RoleApiClient apiClientRole)
        {
            _apiClient = apiClient;
            _apiClientRole = apiClientRole;
        }
        #region Login
        public bool IsLogin()
        {
            if (Core.User != string.Empty)
            {
                if ((DateTime.Now - Core.TimeLogin).TotalMinutes <= Core.TimeSession)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<string> SignInAsync(LoginRequest request)
        {
            Core.User = string.Empty;
            var result = await _apiClient.Authenticate(request);
            if (result.ResultObj == null)
            {
                return result.Message;
            }

            Core.Token = result.ResultObj;
            Core.User = request.UserName;
            Core.TimeLogin = DateTime.Now;
            return "";
        }
        #endregion

        #region get data       
        public async Task<ApiResult<PagedResult<UserViewModel>>> GetAllPaging()
        {
            var data = await _apiClient.GetAllPaging();
            return data;
        }
        public async Task<UserViewModel> GetUserByUsername(string username)
        {
            var data = await _apiClient.GetUserByUsername(username);
            return data;
        }
        public async Task<ApiResult<PagedResult<RoleViewModel>>> GetAllPagingRole()
        {
            var data = await _apiClientRole.GetAllPaging();
            return data;
        }
        public async Task<ApiResult<PagedResult<UserViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }
        #endregion

        #region Update
        public async Task<UpdateStatusViewModel> Update(UserUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(UserCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }

        #endregion

        #region MyRegion
        public async Task<UpdateStatusViewModel> ChangePassword(UserChangePasswordRequest request)
        {
            var result = await _apiClient.ChangePassword(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> ResetPassword(string id, string passwordDefault)
        {
            var result = await _apiClient.ResetPassword(id, passwordDefault);
            return result;
        }
        public async Task<UpdateStatusViewModel> UnLock(string id)
        {
            var result = await _apiClient.UnLock(id);
            return result;
        }
        public async Task<UpdateStatusViewModel> Lock(string id)
        {
            var result = await _apiClient.Lock(id);
            return result;
        }
        #endregion

    }
}
