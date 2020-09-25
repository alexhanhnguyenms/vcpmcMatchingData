using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.System;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.AppMatching.Controllers.System
{
    public class FixParameterController
    {
        private readonly FixParameterApiClient _apiClient;
        public FixParameterController(FixParameterApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        #region get data       
        public async Task<ApiResult<PagedResult<FixParameterViewModel>>> GetAllPaging(GetFixParameterPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetFixParameterPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<FixParameterViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }
        #endregion

        #region Update
        public async Task<UpdateStatusViewModel> Update(FixParameterUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(FixParameterCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }
        public async Task<UpdateStatusViewModelList> ChangeList(FixParameterChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion
    }
}
