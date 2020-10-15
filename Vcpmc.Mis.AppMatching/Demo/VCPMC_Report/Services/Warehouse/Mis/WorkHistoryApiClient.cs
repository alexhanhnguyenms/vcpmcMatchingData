using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.History;

namespace Vcpmc.Mis.AppMatching.Services.Warehouse.Mis
{

    public class WorkHistoryApiClient
    {
        HttpClient _client;
        public WorkHistoryApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        

        public async Task<ApiResult<PagedResult<WorkHistoryViewModel>>> GetAllPaging(GetWorkHistoryPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/WorkHistorys/GetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&Title2={request.Title2}" +
                $"&Writer2={request.Writer2}&Artist2={request.Artist2}" +
                $"&WK_INT_NO={request.WK_INT_NO}&WK_Title2={request.WK_Title2}" +
                $"&WK_Artist2={request.WK_Artist2}"
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkHistoryViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkHistoryViewModel>> data = new ApiSuccessResult<PagedResult<WorkHistoryViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search WorkHistory is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkHistoryViewModel>> data = new ApiSuccessResult<PagedResult<WorkHistoryViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search WorkHistory is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkHistoryPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/WorkHistorys/TotalGetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&Title2={request.Title2}" +
                $"&Writer2={request.Writer2}&Artist2={request.Artist2}" +
                $"&WK_INT_NO={request.WK_INT_NO}&WK_Title2={request.WK_Title2}" +
                $"&WK_Artist2={request.WK_Artist2}"
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<MasterPageViewModel>(body);
            if (dataReturn != null)
            {
                return dataReturn;
            }
            else
            {
                MasterPageViewModel data = new MasterPageViewModel
                {
                    TotalRecordes = 0
                };
                return data;
            }
        }
      
        #endregion

        #region Update        
       
        public async Task<UpdateStatusViewModelList> ChangeList(WorkHistoryChangeListRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/WorkHistorys/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
       
        #endregion

        #region Matching
        public async Task<ApiResult<PagedResult<WorkHistoryViewModel>>> MatchingWorkHistory(WorkHistoryMatchingListRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/WorkHistorys/MatchingWorkHistory", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkHistoryViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkHistoryViewModel>> data = new ApiSuccessResult<PagedResult<WorkHistoryViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search preclaim is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkHistoryViewModel>> data = new ApiSuccessResult<PagedResult<WorkHistoryViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search preclaim is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        #endregion
    }
}
