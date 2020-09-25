using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Users;

namespace Vcpmc.Mis.AppMatching.Services.Admin
{
    public class UserApiClient
    {
        HttpClient _client;
        public UserApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<UserViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<UserViewModel>>>(body);
        }
        public async Task<UserViewModel> GetUserByUsername(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/GetUserByUsername/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UserViewModel>(body);

            return JsonConvert.DeserializeObject<UserViewModel>(body);
        }
        public async Task<ApiResult<PagedResult<UserViewModel>>> GetAllPaging()
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/GetAllPaging");
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<UserViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<UserViewModel>> data = new ApiSuccessResult<PagedResult<UserViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search User is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<UserViewModel>> data = new ApiSuccessResult<PagedResult<UserViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search User is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        #endregion
        #region MyRegion
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _client.PostAsync("/api/users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        }
        #endregion

        #region Update        
        public async Task<UpdateStatusViewModel> Update(UserUpdateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/Users", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(UserCreateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/Users", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.DeleteAsync($"/api/Users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }
        #endregion

        #region MyRegion
        public async Task<UpdateStatusViewModel> ChangePassword(UserChangePasswordRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/Users/ChangePassword", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }       
        public async Task<UpdateStatusViewModel> ResetPassword(string id,string passwordDefault)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/ResetPassword/{id}/{passwordDefault}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }
        public async Task<UpdateStatusViewModel> UnLock(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/UnLock/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }
        public async Task<UpdateStatusViewModel> Lock(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/Users/Lock/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }
        #endregion
    }
}
