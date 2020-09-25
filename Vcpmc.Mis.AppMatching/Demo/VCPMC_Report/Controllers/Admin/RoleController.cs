using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Admin;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.AppMatching.Controllers.Admin
{
    public class RoleController
    {
        private readonly RoleApiClient _apiClient;
        public RoleController(RoleApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        #region get data       
        public async Task<ApiResult<PagedResult<RoleViewModel>>> GetAllPaging()
        {
            var data = await _apiClient.GetAllPaging();
            return data;
        }
        public async Task<ApiResult<PagedResult<AppClaimViewModel>>> GetAllPagingAppClaim()
        {
            var data = await _apiClient.GetAllPagingAppClaim();
            return data;
        }
        public async Task<ApiResult<PagedResult<RoleViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }        
        #endregion 

        #region Update
        public async Task<UpdateStatusViewModel> Update(RoleUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(RoleCreateRequest request)
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
        
    }
}
