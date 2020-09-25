using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.Application.Mis.Works.Tracking
{
    public interface IWorkTrackingService
    {
        Task<PagedResult<WorkTrackingViewModel>> GetAllPaging(GetWorkTrackingPagingRequest request, IMapper _mapper);
        Task<PagedResult<WorkTrackingAggregateViewModel>> GetArreggateMasterList(GetWorkTrackingPagingRequest request);
        //Task<PagedResult<WorkTrackingViewModel>> GetById(string Id, IMapper _mapper);
        //Task<PagedResult<WorkTrackingViewModel>> GetByWorkCodes(List<string> workCodes, IMapper _mapper);
        //Task<PagedResult<WorkTrackingViewModel>> GetByTitles(List<string> titles, IMapper _mapper);       
        #region Update
        //Task<UpdateStatusViewModel> Create(WorkTrackingCreateRequest request, IMapper _mapper);
        //Task<UpdateStatusViewModel> Update(WorkTrackingUpdateRequest request, IMapper _mapper);
        Task<List<UpdateStatusViewModel>> ChangeList(int year, int month, int type, List<WorkTrackingCreateRequest> request, IMapper _mapper);
        //Task<UpdateStatusViewModel> Remove(string id, IMapper _mapper);
        #endregion
    }
}
