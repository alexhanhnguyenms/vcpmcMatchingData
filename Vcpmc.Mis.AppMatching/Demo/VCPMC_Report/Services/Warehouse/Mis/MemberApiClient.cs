
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vcpmc.Mis.Infrastructure;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Members;

namespace Vcpmc.Mis.AppMatching.Services.Warehouse.Mis
{
    public class MemberApiClient
    {
        HttpClient _client;
        public MemberApiClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApiResult<PagedResult<MemberViewModel>>> GetAllPaging(GetMemberPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/members/GetAllPaging?pageIndex=" +
                    $"{request.PageIndex}&pageSize={request.PageSize}&IpiNumber={request.IpiNumber}" +
                    $"&InternalNo={request.IpEnglishName}&ISWC_NO={request.IpEnglishName}" +
                    $"&Society={request.Society}&NameType={request.NameType}"                 
                );
            var body = await response.Content.ReadAsStringAsync();
            var dataReturn = JsonConvert.DeserializeObject<PagedResult<MemberViewModel>>(body);
            if (dataReturn != null)
            {
                ApiSuccessResult<PagedResult<MemberViewModel>> data = new ApiSuccessResult<PagedResult<MemberViewModel>>
                {
                    IsSuccessed = true,
                    Message = "search work is successful",
                    ResultObj = dataReturn
                };
                return data;
            }
            else
            {
                ApiSuccessResult<PagedResult<MemberViewModel>> data = new ApiSuccessResult<PagedResult<MemberViewModel>>
                {
                    IsSuccessed = false,
                    Message = "search work is failure",
                    ResultObj = null
                };
                return data;
            }
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMemberPagingRequest request)
        {
            var sessions = Core.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await _client.GetAsync($"/api/members/TotalGetAllPaging?pageIndex=" +
                    $"{request.PageIndex}&pageSize={request.PageSize}&IpiNumber={request.IpiNumber}" +
                    $"&InternalNo={request.IpEnglishName}&ISWC_NO={request.IpEnglishName}" +
                    $"&Society={request.Society}&NameType={request.NameType}"
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
    }
}
