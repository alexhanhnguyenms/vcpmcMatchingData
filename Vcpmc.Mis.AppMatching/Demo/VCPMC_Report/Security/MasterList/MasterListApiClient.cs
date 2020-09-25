using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.MasterLists;

namespace Vcpmc.Mis.AppMatching.Security.MasterList
{
    public class MasterListApiClient
    {
        HttpClient _client;
        public MasterListApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<MasterListViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/MasterLists/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<MasterListViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<MasterListViewModel>>>(body);
        }

        public async Task<ApiResult<PagedResult<MasterListViewModel>>> GetAllPaging(GetMasterListPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/MasterLists/GetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&Year={request.Year}" +
                $"&Month={request.Month}&IsNeedDetectAPI={request.IsNeedDetectAPI}"
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<MasterListViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<MasterListViewModel>> data = new ApiSuccessResult<PagedResult<MasterListViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search MasterList is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<MasterListViewModel>> data = new ApiSuccessResult<PagedResult<MasterListViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search MasterList is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMasterListPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/MasterLists/TotalGetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&Year={request.Year}" +
                $"&Month={request.Month}&IsNeedDetectAPI={request.IsNeedDetectAPI}"
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
        public async Task<UpdateStatusViewModel> Update(MasterListUpdateRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/MasterLists", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(MasterListCreateRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/MasterLists", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await _client.DeleteAsync($"/api/MasterLists/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }

        public async Task<UpdateStatusViewModelList> ChangeList(MasterListChangeListRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/MasterLists/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        #endregion

        
    }
}
