using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.Application.Mis.Monopolys
{
    public interface IMonopolyService
    {
        Task<PagedResult<MonopolyViewModel>> GetAllPaging(GetMonopolyPagingRequest request, IMapper _mapper);
        Task<PagedResult<MonopolyViewModel>> GetById(string Id, IMapper _mapper);        
        
        #region Update
        Task<UpdateStatusViewModel> Create(MonopolyCreateRequest request, IMapper _mapper);
        Task<UpdateStatusViewModel> Update(MonopolyUpdateRequest request, IMapper _mapper);
        Task<List<UpdateStatusViewModel>> ChangeList(int group, List<MonopolyCreateRequest> request, IMapper _mapper);
        Task<UpdateStatusViewModel> Remove(string id, IMapper _mapper);
        #endregion
    }
}
