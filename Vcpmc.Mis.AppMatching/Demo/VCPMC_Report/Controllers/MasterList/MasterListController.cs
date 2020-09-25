using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Security.MasterList;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.MasterLists;

namespace Vcpmc.Mis.AppMatching.Controllers.MasterList
{
    public class MasterListController
    {
        private readonly MasterListApiClient _apiClient;
        public MasterListController(MasterListApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        #region get data       
        public async Task<ApiResult<PagedResult<MasterListViewModel>>> GetAllPaging(GetMasterListPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMasterListPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<MasterListViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }        
        #endregion 

        #region Update
        public async Task<UpdateStatusViewModel> Update(MasterListUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(MasterListCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }
        public async Task<UpdateStatusViewModelList> ChangeList(MasterListChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion       
    }
}
