using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using MongoDB.Driver.Linq;
using System.Linq;
using Vcpmc.Mis.Utilities.Common;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.Data.Entities.Mis.Members;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.Mis.Members
{
    public class MemberService: IMemberService
    {
        private readonly IMongoCollection<Member> _collection;
        public MemberService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Member>(settings.MembersCollectionName);
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMemberPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<Member> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<MemberViewModel>> GetAllPaging(GetMemberPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestMemberList)
            {
                request.PageSize = LimitRequestBackend.LimitRequestMemberList;
            }
            IMongoQueryable<Member> query = CreateQueryGetAllPaging(request);
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<MemberViewModel> models = new List<MemberViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    MemberViewModel preVM = _mapper.Map<MemberViewModel>(list[i]);
                    //preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<MemberViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<Member> CreateQueryGetAllPaging(GetMemberPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Member>()
                         select e);
            //2. filter              
            if (!string.IsNullOrEmpty(request.IpiNumber))
            {
                query = query.Where(p => p.IpiNumber == request.IpiNumber);
            }
            if (!string.IsNullOrEmpty(request.InternalNo))
            {
                query = query.Where(p => p.InternalNo == request.InternalNo);
            }
            if (!string.IsNullOrEmpty(request.NameType))
            {
                query = query.Where(p => p.NameType == request.NameType);
            }
            if (!string.IsNullOrEmpty(request.IpEnglishName))
            {
                query = query.Where(p => p.IpEnglishName.Contains(request.IpEnglishName));
            }
            if (!string.IsNullOrEmpty(request.Society))
            {
                query = query.Where(p => p.Society.Contains(request.Society));
            }
            return query;
        }        
        public async Task<PagedResult<MemberViewModel>> GetById(string Id, IMapper _mapper)
        {
            // 1.Select
            var preclaim = await GetById(Id);
            //4.map
            List<MemberViewModel> models = new List<MemberViewModel>();
            int totalRow = 0;
            if (preclaim != null)
            {
                MemberViewModel preVM = _mapper.Map<MemberViewModel>(preclaim);
                //preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<MemberViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<Member> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Member>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<Member>> GetByListInternalNo(List<string> listInternalNo)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Member>()
                         select e);
            //2. filter     
            query = query.Where(p => listInternalNo.Contains(p.InternalNo));
            //3. Paging           
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;           
        }
        #endregion

        #region Update  
        public async Task<List<UpdateStatusViewModel>> ChangeList(List<MemberCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                Member In;

                List<string> listInterNo = new List<string>();

                foreach (var item in request)
                {
                    listInterNo.Add(item.InternalNo);
                }
                var dataCheck = await GetByListInternalNo(listInterNo);
                List<Member> insertList = new List<Member>();
                foreach (var item in request)
                {
                    #region change
                    In = _mapper.Map<Member>(item);

                    var data = dataCheck
                        .Where(
                                p => p.InternalNo == In.InternalNo
                                //&& p.IpiNumber == In.IpiNumber
                                //&& p.NameType == In.NameType
                            )
                        .ToList();
                    if (data != null && data.Count == 0)
                    {
                        #region note Add                        
                        insertList.Add(In);
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.WorkCode = In.InternalNo;
                        objectReturn.Command = CommandType.Add;
                        listReturn.Add(objectReturn);
                        #endregion
                    }
                    //update
                    else
                    {
                        #region Update   
                        In.Id = data[0].Id;
                        //nhung thong tin nay luc co luc khong, nen co thi moi cap nhat
                        //khong co thi lay cai cu
                        if(string.IsNullOrEmpty(In.Society))
                        {
                            In.Society = data[0].Society;
                        }
                        else
                        {
                            int a = 1;
                        }
                        if (string.IsNullOrEmpty(In.IpLocalName))
                        {
                            In.IpLocalName = data[0].IpLocalName;
                        }
                        if (string.IsNullOrEmpty(In.NameType))
                        {
                            In.NameType = data[0].NameType;
                        }
                        if (string.IsNullOrEmpty(In.IpiNumber))
                        {
                            In.IpiNumber = data[0].IpiNumber;
                        }
                        else
                        {
                            int a = 1;
                        }
                        var result = await _collection.ReplaceOneAsync
                                (p =>
                                    p.Id == In.Id,
                                    In
                                );
                        //Note update
                        if (result.MatchedCount > 0)
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Successfull;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.InternalNo;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.InternalNo;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        #endregion
                    }

                    #endregion
                }
                //excute insertlist
                if (insertList.Count > 0)
                {
                    await _collection.InsertManyAsync(insertList);
                }
                return listReturn;
            }
            catch (Exception ex)
            {
                return listReturn;
            }
        }
        #endregion
    }
}
