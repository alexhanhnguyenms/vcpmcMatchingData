using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis
{
    public class WorkController
    {
        private readonly WorkApiClient _apiClient;
        public WorkController(WorkApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        #region get data       
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetAllPaging(GetWorkPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }
        public async Task<ApiResult<PagedResult<WorkViewModel>>> Details(string id)
        {
            var result = await _apiClient.GetById(id);
            return result;
        }
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetByWorkCodes(WorkByStringListRequest workCodeListRequest)
        {
            var result = await _apiClient.GetByWorkCodes(workCodeListRequest);
            return result;
        }
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetByTitles(WorkByStringListRequest titleListRequest)
        {
            var result = await _apiClient.GetByTitles(titleListRequest);
            return result;
        }
        #endregion 

        #region Update
        public async Task<UpdateStatusViewModel> Update(WorkUpdateRequest request)
        {
            var result = await _apiClient.Update(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Create(WorkCreateRequest request)
        {
            var result = await _apiClient.Create(request);
            return result;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var result = await _apiClient.Delete(id);
            return result;
        }
        public async Task<UpdateStatusViewModelList> ChangeList(WorkChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion

        #region sync
        public async Task<UpdateStatusViewModelList> SyncFromTrackingWorkToWork(GetWorkTrackingPagingRequest request)
        {
            var result = await _apiClient.SyncFromTrackingWorkToWork(request);
            return result;
        }        
        #endregion

        #region Matching
        public async Task<ApiResult<PagedResult<WorkViewModel>>> MatchingWork(WorkMatchingListRequest request)
        {
            var result = await _apiClient.MatchingWork(request);
            return result;
        }
        #endregion
    }
}
