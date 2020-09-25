using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works;

namespace Vcpmc.Mis.Application.Mis.Works
{
    public interface IWorkService
    {

        Task<PagedResult<WorkViewModel>> GetAllPaging(GetWorkPagingRequest request, IMapper _mapper);
        Task<PagedResult<WorkViewModel>> GetById(string Id, IMapper _mapper);       
        Task<PagedResult<WorkViewModel>> GetByWorkCodes(List<string> workCodes, IMapper _mapper);
        Task<PagedResult<WorkViewModel>> GetByTitles(List<string> titles, IMapper _mapper);
        Task<PagedResult<WorkViewModel>> GetByWorkCodeAndTitle(WorkThreeStringListRequest workCodeAndTitles, IMapper _mapper);                
        Task<PagedResult<WorkViewModel>> GetByWorkTitleAndWriter(WorkThreeStringListRequest TitleAndWriters, IMapper _mapper);                
        #region Update
        Task<UpdateStatusViewModel> Create(WorkCreateRequest request, IMapper _mapper);
        Task<UpdateStatusViewModel> Update(WorkUpdateRequest request, IMapper _mapper);
        Task<List<UpdateStatusViewModel>> ChangeList(List<WorkCreateRequest> request, IMapper _mapper);
        Task<UpdateStatusViewModel> Remove(string id, IMapper _mapper);
        #endregion
    }
}
