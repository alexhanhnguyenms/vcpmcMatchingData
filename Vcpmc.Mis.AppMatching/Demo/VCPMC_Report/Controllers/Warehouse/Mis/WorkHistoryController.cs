using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.History;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis
{
    public class WorkHistoryHistoryController
    {
        private readonly WorkHistoryApiClient _apiClient;
        public WorkHistoryHistoryController(WorkHistoryApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        #region get data       
        public async Task<ApiResult<PagedResult<WorkHistoryViewModel>>> GetAllPaging(GetWorkHistoryPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkHistoryPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }       
        #endregion 

        #region Update
       
        public async Task<UpdateStatusViewModelList> ChangeList(WorkHistoryChangeListRequest request)
        {
            var result = await _apiClient.ChangeList(request);
            return result;
        }
        #endregion

        

        #region Matching
        public async Task<ApiResult<PagedResult<WorkHistoryViewModel>>> MatchingWorkHistory(WorkHistoryMatchingListRequest request)
        {
            var result = await _apiClient.MatchingWorkHistory(request);
            return result;
        }
        #endregion
    }
}
