using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Youtube;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Youtube
{   
    public class PreclaimController
    {
        private readonly PreclaimApiClient _apiClient;
        public PreclaimController(PreclaimApiClient preclaimApiClient)
        {
            _apiClient = preclaimApiClient;
        }
        #region get data       
        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> GetAllPaging(GetPreclaimPagingRequest request)
        {            
            var data = await _apiClient.GetAllPaging(request);
            return data;           
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetPreclaimPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }
        #endregion 

        #region Update
        public async Task<UpdateStatusViewModel> Update(PreclaimUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(PreclaimCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }
        public async Task<UpdateStatusViewModelList> ChangeList(PeclaimChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion

        #region Matching
        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> MatchingPreclaim(PreclaimMatchingListRequest request)
        {
            var result = await _apiClient.MatchingPreclaim(request);
            return result;
        }
        #endregion
    }
}
