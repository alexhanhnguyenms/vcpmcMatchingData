using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.AppMatching.Services.Warehouse.Mis
{
    public class WorkTrackingApiClient
    {
        HttpClient _client;
        public WorkTrackingApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get   
        public async Task<ApiResult<PagedResult<WorkTrackingViewModel>>> GetAllPaging(GetWorkTrackingPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/worktrackings/GetAllPaging?pageIndex=" +
                    $"{request.PageIndex}&pageSize={request.PageSize}&Year={request.Year}" +
                    $"&MONTH={request.MONTH}&Type={request.Type}"               
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkTrackingViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkTrackingViewModel>> data = new ApiSuccessResult<PagedResult<WorkTrackingViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkTrackingViewModel>> data = new ApiSuccessResult<PagedResult<WorkTrackingViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkTrackingPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/worktrackings/TotalGetAllPaging?pageIndex=" +
                    $"{request.PageIndex}&pageSize={request.PageSize}&Year={request.Year}" +
                    $"&MONTH={request.MONTH}&Type={request.Type}"
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
        public async Task<ApiResult<PagedResult<WorkTrackingAggregateViewModel>>> GetArreggateMasterList(GetWorkTrackingPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/worktrackings/GetArreggateMasterList?pageIndex=" +
                    $"{request.PageIndex}&pageSize={request.PageSize}&Year={request.Year}" +
                    $"&MONTH={request.MONTH}&Type={request.Type}"
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkTrackingAggregateViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkTrackingAggregateViewModel>> data = new ApiSuccessResult<PagedResult<WorkTrackingAggregateViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkTrackingAggregateViewModel>> data = new ApiSuccessResult<PagedResult<WorkTrackingAggregateViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        #endregion

        #region Update    
        public async Task<UpdateStatusViewModelList> ChangeList(WorkTrackingChangeListRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/worktrackings/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        #endregion
    }
}
