using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis
{
    public class MonopolyController
    {
        private readonly MonopolyApiClient _apiClient;
        public MonopolyController(MonopolyApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        #region get data       
        public async Task<ApiResult<PagedResult<MonopolyViewModel>>> GetAllPaging(GetMonopolyPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMonopolyPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<MonopolyViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }        
        #endregion 

        #region Update
        public async Task<UpdateStatusViewModel> Update(MonopolyUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(MonopolyCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }
        public async Task<UpdateStatusViewModelList> ChangeList(MonopolyChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion
    }
}
