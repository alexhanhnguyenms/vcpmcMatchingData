using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.AppMatching.Services.Admin
{
    public class RoleApiClient
    {
        HttpClient _client;
        public RoleApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<RoleViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Roles/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<RoleViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<RoleViewModel>>>(body);
        }

        public async Task<ApiResult<PagedResult<RoleViewModel>>> GetAllPaging()
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Roles/GetAllPaging");
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<RoleViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<RoleViewModel>> data = new ApiSuccessResult<PagedResult<RoleViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search Role is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<RoleViewModel>> data = new ApiSuccessResult<PagedResult<RoleViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search Role is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<ApiResult<PagedResult<AppClaimViewModel>>> GetAllPagingAppClaim()
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Roles/GetAllPagingAppClaim");
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<AppClaimViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<AppClaimViewModel>> data = new ApiSuccessResult<PagedResult<AppClaimViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search Role is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<AppClaimViewModel>> data = new ApiSuccessResult<PagedResult<AppClaimViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search Role is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        #endregion

        #region Update        
        public async Task<UpdateStatusViewModel> Update(RoleUpdateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/Roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(RoleCreateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/Roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.DeleteAsync($"/api/Roles/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }        
        #endregion
    }
}
