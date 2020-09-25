using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Services.Warehouse.Mis;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Members;

namespace Vcpmc.Mis.AppMatching.Controllers.Warehouse.Mis
{
    public class MemberController
    {
        private readonly MemberApiClient _apiClient;
        public MemberController(MemberApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        #region get data       
        public async Task<ApiResult<PagedResult<MemberViewModel>>> GetAllPaging(GetMemberPagingRequest request)
        {
            var data = await _apiClient.GetAllPaging(request);
            return data;
        }
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMemberPagingRequest request)
        {
            var data = await _apiClient.TotalGetAllPaging(request);
            return data;
        }        
        #endregion
    }
}
