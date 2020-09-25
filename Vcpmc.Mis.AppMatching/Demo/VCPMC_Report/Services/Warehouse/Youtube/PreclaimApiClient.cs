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
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.Services.Warehouse.Youtube
{    
    public class PreclaimApiClient
    {
        HttpClient _client;
        public PreclaimApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/preclaims/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<PreclaimViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<PreclaimViewModel>>>(body);
        }

        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> GetAllPaging(GetPreclaimPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/preclaims/GetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&asset_id={request.Asset_ID}"+
                $"&c_title={request.C_Title}&c_iswc={request.C_ISWC}" +
                $"&c_workcode={request.C_Workcode}&c_writers={request.C_Writers}" 
                );
            var body = await response.Content.ReadAsStringAsync();  
            var  dataReturn = JsonConvert.DeserializeObject<PagedResult<PreclaimViewModel>>(body);
            if(dataReturn != null)
            {
                ApiSuccessResult<PagedResult<PreclaimViewModel>> data = new ApiSuccessResult<PagedResult<PreclaimViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search preclaim is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<PreclaimViewModel>> data = new ApiSuccessResult<PagedResult<PreclaimViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search preclaim is failure",
                    ResultObj = null
                };
                return data;
            }  
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetPreclaimPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/preclaims/TotalGetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&asset_id={request.Asset_ID}" +
                $"&c_title={request.C_Title}&c_iswc={request.C_ISWC}" +
                $"&c_workcode={request.C_Workcode}&c_writers={request.C_Writers}"
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
        public async Task<UpdateStatusViewModel> Update(PreclaimUpdateRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/preclaims", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(PreclaimCreateRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/preclaims", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await _client.DeleteAsync($"/api/preclaims/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }

        public async Task<UpdateStatusViewModelList> ChangeList(PeclaimChangeListRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/preclaims/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        #endregion

        #region Matching
        public async Task<ApiResult<PagedResult<PreclaimViewModel>>> MatchingPreclaim(PreclaimMatchingListRequest request)
        {
            var sessions = Core.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/preclaims/MatchingPreclaim", httpContent);            
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<PreclaimViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<PreclaimViewModel>> data = new ApiSuccessResult<PagedResult<PreclaimViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search preclaim is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<PreclaimViewModel>> data = new ApiSuccessResult<PagedResult<PreclaimViewModel>>
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
