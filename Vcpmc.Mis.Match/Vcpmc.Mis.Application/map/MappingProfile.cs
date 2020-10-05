using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vcpmc.Mis.Data.Entities.Media.Youtube;
using Vcpmc.Mis.Data.Entities.Mis.Monopolys;
using Vcpmc.Mis.Data.Entities.Mis.Works;
using Vcpmc.Mis.Data.Entities.System;
using Vcpmc.Mis.ViewModels.MasterLists;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;
using Vcpmc.Mis.ViewModels.System.Roles;
using Vcpmc.Mis.ViewModels.System.Users;
using Vcpmc.Mis.Data.Entities.MasterLists;
using Vcpmc.Mis.Data.Entities.Mis.Members;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.Data.Entities.System.Para;
using Vcpmc.Mis.ViewModels.System.Para;
using Vcpmc.Mis.Data.Entities.Mis.Historys;
using Vcpmc.Mis.ViewModels.Mis.History;
using Vcpmc.Mis.Data.Entities.Mis.Historys;

namespace Vcpmc.Mis.Application.map
{
    public class MappingProfile : Profile
    {       
        public MappingProfile()
        {
            #region user
            //1.view
            CreateMap<AppUser2, UserViewModel>();
            CreateMap<UserViewModel, AppUser2>();
            //2.paging
            //CreateMap<AppUser2, GetUserPagingRequest>();
            //CreateMap<GetUserPagingRequest, AppUser2>();
            //3.update
            CreateMap<AppUser2, UserUpdateRequest>();
            CreateMap<UserUpdateRequest, AppUser2>();
            //4.create
            CreateMap<AppUser2, UserCreateRequest>();
            CreateMap<UserCreateRequest, AppUser2>();
            #endregion

            #region role
            //1.view
            CreateMap<AppRole2, RoleViewModel>();
            CreateMap<RoleViewModel, AppRole2>();
            //2.paging
            //CreateMap<AppRole2, GetWorkPagingRequest>();
            //CreateMap<GetWorkPagingRequest, AppRole2>();
            //3.update
            CreateMap<AppRole2, RoleUpdateRequest>();
            CreateMap<RoleUpdateRequest, AppRole2>();
            //4.create
            CreateMap<AppRole2, RoleCreateRequest>();
            CreateMap<RoleCreateRequest, AppRole2>();
            #endregion

            #region Claim
            //1.view
            CreateMap<AppClaim, AppClaimViewModel>();
            CreateMap<AppClaimViewModel, AppClaim>();
            #endregion
            // Add as many of these lines as you need to map your objects
            #region preclaim
            //1.view
            CreateMap<Preclaim, PreclaimViewModel>();
            CreateMap<PreclaimViewModel, Preclaim>();
            //2.paging
            CreateMap<Preclaim, GetPreclaimPagingRequest>();
            CreateMap<GetPreclaimPagingRequest, Preclaim>();
            //3.update
            CreateMap<Preclaim, PreclaimUpdateRequest>();
            CreateMap<PreclaimUpdateRequest, Preclaim>();
            //4.create
            CreateMap<Preclaim, PreclaimCreateRequest>();
            CreateMap<PreclaimCreateRequest, Preclaim>();
            #endregion

            #region Work
            //1.view
            CreateMap<Work, WorkViewModel>();
            CreateMap<WorkViewModel, Work>();
            //2.paging
            CreateMap<Work, GetWorkPagingRequest>();
            CreateMap<GetWorkPagingRequest, Work>();
            //3.update
            CreateMap<Work, WorkUpdateRequest>();
            CreateMap<WorkUpdateRequest, Work>();
            //4.create
            CreateMap<Work, WorkCreateRequest>();
            CreateMap<WorkCreateRequest, Work>();
            #endregion

            #region WorkHistory
            //1.view
            CreateMap<WorkHistory, WorkHistoryViewModel>();
            CreateMap<WorkHistoryViewModel, WorkHistory>();
            //2.paging
            CreateMap<WorkHistory, GetWorkHistoryPagingRequest>();
            CreateMap<GetWorkHistoryPagingRequest, WorkHistory>();
            //3.update
            CreateMap<WorkHistory, WorkHistoryUpdateRequest>();
            CreateMap<WorkHistoryUpdateRequest, WorkHistory>();
            //4.create
            CreateMap<WorkHistory, WorkHistoryCreateRequest>();
            CreateMap<WorkHistoryCreateRequest, WorkHistory>();
            #endregion

            #region WorkTracking
            //1.view
            CreateMap<WorkTracking, WorkTrackingViewModel>();
            CreateMap<WorkTrackingViewModel, WorkTracking>();
            //2.paging
            CreateMap<WorkTracking, GetWorkTrackingPagingRequest>();
            CreateMap<GetWorkTrackingPagingRequest, WorkTracking>();
            //3.update
            CreateMap<WorkTracking, WorkTrackingUpdateRequest>();
            CreateMap<WorkTrackingUpdateRequest, WorkTracking>();
            //4.create
            CreateMap<WorkTracking, WorkTrackingCreateRequest>();
            CreateMap<WorkTrackingCreateRequest, WorkTracking>();
            #endregion

            #region Monopoly
            //1.view
            CreateMap<Monopoly, MonopolyViewModel>();
            CreateMap<MonopolyViewModel, Monopoly>();
            //2.paging
            CreateMap<Monopoly, GetMonopolyPagingRequest>();
            CreateMap<GetMonopolyPagingRequest, Monopoly>();
            //3.update
            CreateMap<Monopoly, MonopolyUpdateRequest>();
            CreateMap<MonopolyUpdateRequest, Monopoly>();
            //4.create
            CreateMap<Monopoly, MonopolyCreateRequest>();
            CreateMap<MonopolyCreateRequest, Monopoly>();
            #endregion

            #region Materlist
            //1.view
            CreateMap<MasterList, MasterListViewModel>();
            CreateMap<MasterListViewModel, MasterList>();
            //2.paging
            CreateMap<MasterList, GetMasterListPagingRequest>();
            CreateMap<GetMasterListPagingRequest, MasterList>();
            //3.update
            CreateMap<MasterList, MasterListUpdateRequest>();
            CreateMap<MasterListUpdateRequest, MasterList>();
            //4.create
            CreateMap<MasterList, MasterListCreateRequest>();
            CreateMap<MasterListCreateRequest, MasterList>();
            #endregion

            #region member
            //1.view
            CreateMap<Member, MemberViewModel>();
            CreateMap<MemberViewModel, Member>();
            //2.paging
            CreateMap<Member, GetMemberPagingRequest>();
            CreateMap<GetMemberPagingRequest, Member>();
            //3.update
            //CreateMap<Member, MemberUpdateRequest>();
            //CreateMap<MemberUpdateRequest, Member>();
            //4.create
            CreateMap<Member, MemberCreateRequest>();
            CreateMap<MemberCreateRequest, Member>();
            #endregion

            #region FixParameter
            //1.view
            CreateMap<FixParameter, FixParameterViewModel>();
            CreateMap<FixParameterViewModel, FixParameter>();
            //2.paging
            CreateMap<FixParameter, GetFixParameterPagingRequest>();
            CreateMap<GetFixParameterPagingRequest, FixParameter>();
            //3.update
            CreateMap<FixParameter, FixParameterUpdateRequest>();
            CreateMap<FixParameterUpdateRequest, FixParameter>();
            //4.create
            CreateMap<FixParameter, FixParameterCreateRequest>();
            CreateMap<FixParameterCreateRequest, FixParameter>();
            #endregion
        }
    }
}
