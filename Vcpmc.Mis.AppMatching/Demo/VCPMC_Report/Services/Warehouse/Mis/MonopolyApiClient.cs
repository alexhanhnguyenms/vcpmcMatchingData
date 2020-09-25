using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.AppMatching.Services.Warehouse.Mis
{
    public class MonopolyApiClient
    {
        HttpClient _client;
        public MonopolyApiClient(HttpClient client)
        {
            _client = client;
        }

        #region get
        public async Task<ApiResult<PagedResult<MonopolyViewModel>>> GetById(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/monopolys/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<MonopolyViewModel>>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<MonopolyViewModel>>>(body);
        }

        public async Task<ApiResult<PagedResult<MonopolyViewModel>>> GetAllPaging(GetMonopolyPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/monopolys/GetAllPaging?pageIndex="+
                $"{request.PageIndex}&pageSize={request.PageSize}"+
                $"&group={request.Group}&Name2={request.Name}&Own2={request.Own2}&SearchType={request.SearchType}&CodeNew={request.CodeNew}"
                //$"&ISRC={request.ISRC}&WRITER={request.WRITER}" +
                //$"&ARTIST={request.ARTIST}&SOC_NAME={request.SOC_NAME}"
                ) ;
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<MonopolyViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<MonopolyViewModel>> data = new ApiSuccessResult<PagedResult<MonopolyViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<MonopolyViewModel>> data = new ApiSuccessResult<PagedResult<MonopolyViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMonopolyPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/monopolys/TotalGetAllPaging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}" +
                 $"&group={request.Group}&Name2={request.Name}&Own2={request.Own2}&SearchType={request.SearchType}&CodeNew={request.CodeNew}"
                //$"&ISRC={request.ISRC}&WRITER={request.WRITER}" +
                //$"&ARTIST={request.ARTIST}&SOC_NAME={request.SOC_NAME}"
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
        public async Task<UpdateStatusViewModel> Update(MonopolyUpdateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/monopolys", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Create(MonopolyCreateRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/monopolys", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(result);
            return null;
        }
        public async Task<UpdateStatusViewModel> Delete(string id)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.DeleteAsync($"/api/monopolys/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModel>(body);
            return null;
        }

        public async Task<UpdateStatusViewModelList> ChangeList(MonopolyChangeListRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/monopolys/ChangeList", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UpdateStatusViewModelList>(result);
            return null;
        }
        #endregion
    }
}
