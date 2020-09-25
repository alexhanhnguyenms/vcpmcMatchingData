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
    public class WorkApiClient
    {
        HttpClient _client;
        public WorkApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/works/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<WorkViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<WorkViewModel>>>(body);
        }

        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetAllPaging(GetWorkPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/works/GetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&WK_INT_NO={request.WK_INT_NO}" +
                $"&TTL_ENG={request.TTL_ENG}&ISWC_NO={request.ISWC_NO}" +
                $"&ISRC={request.ISRC}&WRITER={request.WRITER}" +
                $"&ARTIST={request.ARTIST}&SOC_NAME={request.SOC_NAME}&SearchType={request.SearchType}&IsGetMonopolyInfo={request.IsGetMonopolyInfo}&SOCIETY={request.SOCIETY}"
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/works/TotalGetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&WK_INT_NO={request.WK_INT_NO}" +
                $"&TTL_ENG={request.TTL_ENG}&ISWC_NO={request.ISWC_NO}" +
                $"&ISRC={request.ISRC}&WRITER={request.WRITER}" +
                $"&ARTIST={request.ARTIST}&SOC_NAME={request.SOC_NAME}&SearchType={request.SearchType}&IsGetMonopolyInfo={request.IsGetMonopolyInfo}&SOCIETY={request.SOCIETY}"
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
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetByWorkCodes(WorkByStringListRequest workCodeListRequest)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(workCodeListRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/works/GetByWorkCodes", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkViewModel>>(result);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<ApiResult<PagedResult<WorkViewModel>>> GetByTitles(WorkByStringListRequest titleListRequest)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(titleListRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/works/GetByTitles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkViewModel>>(result);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
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
        public async Task<UpdateStatusViewModel> Update(WorkUpdateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/works", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(WorkCreateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/works", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.DeleteAsync($"/api/works/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }

        public async Task<UpdateStatusViewModelList> ChangeList(WorkChangeListRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/works/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        public async Task<UpdateStatusViewModelList> SyncFromTrackingWorkToWork(GetWorkTrackingPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/works/SyncFromTrackingWorkToWork", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        #endregion

        #region Matching
        public async Task<ApiResult<PagedResult<WorkViewModel>>> MatchingWork(WorkMatchingListRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/works/MatchingWork", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<WorkViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search preclaim is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<WorkViewModel>> data = new ApiSuccessResult<PagedResult<WorkViewModel>>
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
