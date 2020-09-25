using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Media.Youtube;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.Application.Media.Youtube
{
    public interface IPreclaimService
    {
        Task<PagedResult<PreclaimViewModel>> GetAllPaging(GetPreclaimPagingRequest request, IMapper _mapper);
        Task<PagedResult<PreclaimViewModel>> GetByAssetId(string assetId, IMapper _mapper);
        //Task<PagedResult<PreclaimViewModel>> GetByAssetIdAndMothAndYear(string assetId, int month, int year, IMapper _mapper);
        Task<PagedResult<PreclaimViewModel>> GetById(string Id, IMapper _mapper);
        Task<PagedResult<PreclaimViewModel>> GetByAsset_ids(List<string> asset_ids, IMapper _mapper);
        Task<PagedResult<PreclaimViewModel>> GetByWorkCodes(List<string> workCodes, IMapper _mapper);
        #region Update
        Task<UpdateStatusViewModel> Create(PreclaimCreateRequest request,IMapper _mapper);

        Task<UpdateStatusViewModel> Update(PreclaimUpdateRequest request, IMapper _mapper);
        Task<List<UpdateStatusViewModel>> ChangeList(List<PreclaimCreateRequest> request, IMapper _mapper);
        Task<UpdateStatusViewModel> Remove(string id, IMapper _mapper);
        #endregion
    }
}
