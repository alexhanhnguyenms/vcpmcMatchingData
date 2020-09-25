using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis
{
    public class WorkTrackingController
    {
        private readonly WorkTrackingApiClient _apiClient;
        public WorkTrackingController(WorkTrackingApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        #region get data       
        public async Task<ApiResult<PagedResult<WorkTrackingViewModel>>> GetAllPaging(GetWorkTrackingPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkTrackingPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<WorkTrackingAggregateViewModel>>> GetArreggateMasterList(GetWorkTrackingPagingRequest request)
        {
            var data = await _apiClient.GetArreggateMasterList(request);
            return data;
        }
        #endregion

        #region Update       
        public async Task<UpdateStatusViewModelList> ChangeList(WorkTrackingChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion
    }
}
