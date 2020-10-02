using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mis.Works;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works;
using MongoDB.Driver.Linq;
using System.Linq;
using Vcpmc.Mis.Utilities.Common;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using Vcpmc.Mis.Application.Mis.Monopolys;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.Application.Mis.Works.Tracking;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;
using Vcpmc.Mis.Shared.Mis.Works;
using Vcpmc.Mis.Application.Mis.Members;
using Vcpmc.Mis.ViewModels.Mis.Members;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.Mis.Works
{
    public class WorkService : IWorkService
    {
        private readonly IMongoCollection<Work> _collection;
        MonopolyService _monoService;
        WorkTrackingService _workTrackingService;
        MemberService _memberService;
        public WorkService(IDatabaseSettings settings, MonopolyService monoService, WorkTrackingService workTrackingService, MemberService memberService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Work>(settings.WorksCollectionName);
            _monoService = monoService;
            _workTrackingService = workTrackingService;
            _memberService = memberService;

        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<Work> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;           
        }
        public async Task<PagedResult<WorkViewModel>> GetAllPaging(GetWorkPagingRequest request, IMapper _mapper)
        {
            if(request.PageSize > LimitRequestBackend.LimitRequestWork)
            {
                request.PageSize = LimitRequestBackend.LimitRequestWork;
            }
            IMongoQueryable<Work> query = CreateQueryGetAllPaging(request);
            //TODO: them 1 qery thay so luong dau tien????
            //3. Paging
            int totalRow = 0;
            var list = await query                
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<WorkViewModel> models = new List<WorkViewModel>();
            List<MonopolyViewModel> monoWorks = new List<MonopolyViewModel>();
            List<MonopolyViewModel> monoMembers = new List<MonopolyViewModel>();
            if (list != null && list.Count > 0)
            {
                #region Lay thong tin doc quyen
                if(request.IsGetMonopolyInfo)
                {
                    #region Doc quyen tac pham
                    List<string> MonopolyCodes = new List<string>();
                    foreach (var item in list)
                    {
                        MonopolyCodes.Add(item.WK_INT_NO);                   
                    }
                    if(MonopolyCodes.Count>0)
                    {
                        var dataCheckMonoWork = await _monoService.GetByGroupAndWorkCodes(0, MonopolyCodes);


                        if (list != null && dataCheckMonoWork.Count > 0)
                        {
                            for (int i = 0; i < dataCheckMonoWork.Count; i++)
                            {
                                MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(dataCheckMonoWork[i]);
                                monoWorks.Add(preVM);
                            }

                        }
                    }                    
                    #endregion

                    #region Doc quyen tac gia
                    List<string> CodoMember = new List<string>();
                    foreach (var item in list)
                    {
                        foreach (var sub in item.InterestedParties)
                        {
                            if (sub.IP_INT_NO != string.Empty)
                            {
                                CodoMember.Add(sub.IP_INT_NO);
                            }
                        }
                       
                    }
                    if(CodoMember.Count>0)
                    {
                        var dataCheckMonomemer = await _monoService.GetByGroupAndWorkCodes(1, CodoMember);

                        if (list != null && dataCheckMonomemer.Count > 0)
                        {

                            for (int i = 0; i < dataCheckMonomemer.Count; i++)
                            {
                                MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(dataCheckMonomemer[i]);
                                monoMembers.Add(preVM);
                            }

                        }
                    }
                   
                    #endregion
                }
                #endregion

                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {                   
                    WorkViewModel preVM = _mapper.Map<WorkViewModel>(list[i]);
                    preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    if(monoWorks.Count>0)
                    {
                        var itemMonoWork = monoWorks.Where(p => p.CodeNew == preVM.WK_INT_NO).ToList();
                        if(itemMonoWork!=null)
                        {
                            preVM.MonopolyWorks = itemMonoWork;
                        }
                    }
                    if (monoMembers.Count > 0)
                    {
                        var itemMonoMember = monoMembers
                            .Where(p => 
                                    preVM.InterestedParties
                                    .Where
                                    (x=> x.IP_INT_NO == p.CodeNew && x.IP_NAMETYPE == p.NameType).Any()
                                   )
                            .ToList();
                        if (itemMonoMember != null)
                        {
                            preVM.MonopolyMembers = itemMonoMember;
                        }
                    }
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<Work> CreateQueryGetAllPaging(GetWorkPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter  
            if (request.SearchType == 0)
            {
                if (!string.IsNullOrEmpty(request.WK_INT_NO))
                {
                    query = query.Where(p => p.WK_INT_NO == request.WK_INT_NO);
                }
                if (!string.IsNullOrEmpty(request.TTL_ENG))
                {
                    query = query.Where(p => p.TTL_ENG.Contains(request.TTL_ENG));
                }
                if (!string.IsNullOrEmpty(request.ISWC_NO))
                {
                    query = query.Where(p => p.ISWC_NO == (request.ISWC_NO));
                }
                if (!string.IsNullOrEmpty(request.ISRC))
                {
                    query = query.Where(p => p.ISRC == (request.ISRC));
                }
                if (!string.IsNullOrEmpty(request.WRITER))
                {
                    query = query.Where(p => p.WRITER.Contains(request.WRITER) || p.InterestedParties.Where(x=>x.IP_NAME.Contains(request.WRITER)).Any());
                }
                if (!string.IsNullOrEmpty(request.ARTIST))
                {
                    query = query.Where(p => p.ARTIST.Contains(request.ARTIST));
                }
                if (!string.IsNullOrEmpty(request.SOC_NAME))
                {
                    query = query.Where(p => p.SOC_NAME.Contains(request.SOC_NAME));
                }
                if (!string.IsNullOrEmpty(request.SOCIETY))
                {
                    query = query.Where(p => p.InterestedParties.Where(x=>x.Society.Contains(request.SOCIETY)).Any());
                }
            }
            else//1
            {
                if (!string.IsNullOrEmpty(request.WK_INT_NO))
                {
                    query = query.Where(p => p.WK_INT_NO == request.WK_INT_NO);
                }
                if (!string.IsNullOrEmpty(request.TTL_ENG))
                {
                    query = query.Where(p => p.TTL_ENG == request.TTL_ENG);
                }
                if (!string.IsNullOrEmpty(request.ISWC_NO))
                {
                    query = query.Where(p => p.ISWC_NO == request.ISWC_NO);
                }
                if (!string.IsNullOrEmpty(request.ISRC))
                {
                    query = query.Where(p => p.ISRC == request.ISRC);
                }
                if (!string.IsNullOrEmpty(request.WRITER))
                {                   
                    query = query.Where(p => p.WRITER == request.WRITER || p.InterestedParties.Where(x => x.IP_NAME == request.WRITER).Any());
                }
                if (!string.IsNullOrEmpty(request.ARTIST))
                {
                    query = query.Where(p => p.ARTIST == request.ARTIST);
                }
                if (!string.IsNullOrEmpty(request.SOC_NAME))
                {
                    query = query.Where(p => p.SOC_NAME == request.SOC_NAME);
                }
                if (!string.IsNullOrEmpty(request.SOCIETY))
                {
                    query = query.Where(p => p.InterestedParties.Where(x => x.Society == (request.SOCIETY)).Any());
                }
            }

            return query;
        }

        public async Task<PagedResult<WorkViewModel>> GetById(string Id, IMapper _mapper)
        {
            // 1.Select
            var preclaim = await GetById(Id);
            //4.map
            List<WorkViewModel> models = new List<WorkViewModel>();
            int totalRow = 0;
            if (preclaim != null)
            {
                WorkViewModel preVM = _mapper.Map<WorkViewModel>(preclaim);               
                preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<Work> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<Work>> GetByWorkCode(string workCode)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => workCode == p.WK_INT_NO);
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkViewModel>> GetByWorkCodes(List<string> workCodes, IMapper _mapper)
        {
            //1.get
            var list = await GetByWorkCodes(workCodes);
            //2.map
            int totalRow = list.Count;
            List<WorkViewModel> models = new List<WorkViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Work>> GetByWorkCodes(List<string> workCodes)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => workCodes.Contains(p.WK_INT_NO));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkViewModel>> GetByTitles(List<string> titles, IMapper _mapper)
        {
            //1.get
            var list = await GetByTitles(titles);
            //2.map
            int totalRow = list.Count;
            List<WorkViewModel> models = new List<WorkViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Work>> GetByTitles(List<string> titles)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => titles.Contains(p.TTL_ENG));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkViewModel>> GetByWorkCodeAndTitle(WorkThreeStringListRequest workCodeAndTitles, IMapper _mapper)
        {
            //1.get
            var list = await GetByWorkCodeAndTitle(workCodeAndTitles);
            //2.map
            int totalRow = list.Count;
            List<WorkViewModel> models = new List<WorkViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Work>> GetByWorkCodeAndTitle(WorkThreeStringListRequest workCodeAndTitles)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => workCodeAndTitles.Item2.Items.Contains(p.TTL_ENG));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkViewModel>> GetByWorkTitleAndWriter(WorkThreeStringListRequest TitleAndWriters, IMapper _mapper)
        {
            //1.get
            //TODO
            var list = await GetByWorkCodeAndTitle(TitleAndWriters);
            //2.map
            int totalRow = list.Count;
            List<WorkViewModel> models = new List<WorkViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Work>> GetByWorkTitleAndWriter(WorkThreeStringListRequest TitleAndWriters)
        {
            //1. Select join
            //TODO
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            //2. filter     
            query = query.Where(p => TitleAndWriters.Item2.Items.Contains(p.TTL_ENG));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }        
        #endregion

        #region Update  
        public async Task<UpdateStatusViewModel> Create(WorkCreateRequest request, IMapper _mapper)
        {
            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel
            {
                Command = CommandType.Add,
                Status = UpdateStatus.Failure,
                Message = "",
                TotalEffect = 0
            };
            try
            {
                Work In = _mapper.Map<Work>(request);               
                var data = await GetByWorkCode(In.WK_INT_NO);
                if (data != null && data.Count == 0)
                {
                    await _collection.InsertOneAsync(In);
                    objectReturn.Status = UpdateStatus.Successfull;
                    objectReturn.TotalEffect = 1;
                    objectReturn.Message = "Adding a record is successfull";
                    return objectReturn;
                }
                else
                {
                    objectReturn.Message = "Adding a record is a failure because Existence record";
                    return objectReturn;
                }                
            }
            catch (Exception)
            {
                objectReturn.Message = "Addind record is failure";
                return objectReturn;
            }         
        }
        public async Task<UpdateStatusViewModel> Update(WorkUpdateRequest request, IMapper _mapper)
        {
            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel
            {
                Command = CommandType.Update,
                Status = UpdateStatus.Failure,
                Message = "",
                TotalEffect = 0
            };
            try
            {
                Work In = _mapper.Map<Work>(request);                
                var data = await GetByWorkCode(In.WK_INT_NO);
                if (data != null)
                {
                    var result = await _collection.ReplaceOneAsync(p => p.Id == In.Id, In);
                    if (result.MatchedCount > 0)
                    {
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = result.ModifiedCount;
                        objectReturn.Message = "Update records successfully";
                        return objectReturn;
                    }
                    else
                    {
                        objectReturn.Message = "Update records failure";
                        return objectReturn;
                    }

                }
                else
                {
                    objectReturn.Message = "not find record for update";
                    return objectReturn;
                }
            }
            catch (Exception ex)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }        
        public async Task<UpdateStatusViewModel> Remove(string id, IMapper _mapper)
        {
            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel
            {
                Command = CommandType.Delete,
                Status = UpdateStatus.Failure,
                Message = "",
                TotalEffect = 0
            };
            try
            {
                var data = await GetById(id);
                if (data != null)
                {
                    var result = await _collection.DeleteOneAsync(p => p.Id == id);
                    if (result.DeletedCount > 0)
                    {
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = result.DeletedCount;
                        objectReturn.Message = "Delete a record successfully";
                        return objectReturn;
                    }
                    else
                    {
                        objectReturn.Message = "Delete a recored failure";
                        return objectReturn;
                    }
                }
                else
                {
                    objectReturn.Message = "not find record for delete";
                    return objectReturn;
                }

            }
            catch (Exception)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }
        public async Task<List<UpdateStatusViewModel>> ChangeList(List<WorkCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                Work In;
                List<string> workCodes = new List<string>();                
                foreach (var item in request)
                {
                    if (!workCodes.Contains(item.WK_INT_NO))
                    {
                        workCodes.Add(item.WK_INT_NO);
                    }
                }
                var dataCheck = await GetByWorkCodes(workCodes);
                List<Work> insertList = new List<Work>();
                foreach (var item in request)
                {
                    #region change                    
                    In = _mapper.Map<Work>(item);                   

                    var data = dataCheck
                        .Where(
                                p => p.WK_INT_NO == In.WK_INT_NO
                            )
                        .ToList();

                    //if (In.WK_INT_NO == "17615461")
                    //{
                    //    int a = 1;
                    //}
                    if (data != null && data.Count == 0)
                    {
                        #region note Add
                        insertList.Add(In);                      
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.WorkCode = In.WK_INT_NO;                        
                        objectReturn.Command = CommandType.Add;
                        listReturn.Add(objectReturn);
                        #endregion
                    }
                    //update
                    else
                    {
                        #region Update   
                        In.Id = data[0].Id;
                        List<InterestedParty> parNews = In.InterestedParties;
                        List<OtherTitle> otherTitleNews = In.OtherTitles;
                        //Chinh sua mot so thong tin
                        //neu khong co thong tin moi, tam lay thong tin cu
                        //vi mot so khong co thong tin nay
                        //khi co thi cap nhat moi nhat
                        if (string.IsNullOrEmpty(In.ARTIST))
                        {
                            In.ARTIST = data[0].ARTIST;
                        }
                        else
                        {
                            int a = 1;
                        }
                        if (string.IsNullOrEmpty(In.ISWC_NO))
                        {
                            In.ISWC_NO = data[0].ISWC_NO;
                        }
                        if (string.IsNullOrEmpty(In.ISRC))
                        {
                            In.ISRC = data[0].ISRC;
                        }
                        if (string.IsNullOrEmpty(In.SOC_NAME))
                        {
                            In.SOC_NAME = data[0].SOC_NAME;
                        }
                        
                        if (In.StarRating == 0)
                        {
                            int a = 1;
                        }
                        In.StarRating += data[0].StarRating;
                        In.OtherTitles = data[0].OtherTitles;
                        In.InterestedParties = data[0].InterestedParties;                                         
                        //bool isComplete = true;

                        #region Cap nhat tac pham
                        foreach (var par in otherTitleNews)
                        {
                            #region Add danh sach tac gia
                            if(In.OtherTitles.Where(p => p.Title == par.Title.Trim()).ToList().Count == 0)
                            {
                                var itemOrder = In.OtherTitles.OrderByDescending(p => p.No).ToList();
                                int maxNo = 1;
                                if (itemOrder.Count > 0)
                                {                                    
                                    maxNo = itemOrder[0].No + 1;
                                }
                                par.No = maxNo;
                                In.OtherTitles.Add(par);
                            } 
                            #endregion
                        }
                        #endregion

                        #region Cap nhat tac gia  
                        foreach (var par in parNews)
                        {
                            string x11 = par.IP_INT_NO;
                            string x22 = par.IP_INT_NO;
                            string x1 = par.IP_NAME;
                            string x2 = par.IP_NAME;
                            #region Add danh sach tac gia
                            var dataPar = In.InterestedParties.Where(p => p.IP_INT_NO == par.IP_INT_NO && p.IP_INT_NO != string.Empty).FirstOrDefault();
                            if (dataPar != null)
                            {
                                x2 = dataPar.IP_NAME;
                                x22 = dataPar.IP_INT_NO;
                                //dataPar.No = dataPar.No;//giu nguyen stt
                                //dataPar.IP_INT_NO = par.IP_INT_NO;   
                                //thong tin moi co dulieu moi cap nhat, khong thi van de cai cu
                                if(!string.IsNullOrEmpty(par.IP_NAME))
                                {
                                    dataPar.IP_NAME = par.IP_NAME;
                                }                                
                                if(!string.IsNullOrEmpty(par.IP_WK_ROLE))
                                {
                                    dataPar.IP_WK_ROLE = par.IP_WK_ROLE;
                                }                               
                                //dataPar.WK_STATUS = "COMPLETE";

                                dataPar.PER_OWN_SHR = par.PER_OWN_SHR;
                                dataPar.PER_COL_SHR = par.PER_COL_SHR;

                                dataPar.MEC_OWN_SHR = par.MEC_OWN_SHR;
                                dataPar.MEC_COL_SHR = par.MEC_COL_SHR;

                                dataPar.SP_SHR = par.SP_SHR;
                                dataPar.TOTAL_MEC_SHR = par.TOTAL_MEC_SHR;

                                dataPar.SYN_OWN_SHR = par.SYN_OWN_SHR;
                                dataPar.SYN_COL_SHR = par.SYN_COL_SHR;
                                if (!string.IsNullOrEmpty(par.Society))//to chuc thanh vien
                                {
                                    dataPar.Society = par.Society;
                                }                              
                                if(!string.IsNullOrEmpty(par.IP_NAMETYPE))
                                {
                                    dataPar.IP_NAMETYPE = par.IP_NAMETYPE;
                                }
                                dataPar.CountUpdate++;
                                dataPar.LastUpdateAt = DateTime.Now;
                                dataPar.LastChoiseAt = DateTime.Now;
                                //int maxNo = In.InterestedParties.Count;
                                //dataPar.No = maxNo;
                                //In.InterestedParties.Add(par);
                            }
                            else
                            {
                                var dataParNames = In.InterestedParties.Where(p => p.IP_NAME == par.IP_NAME).ToList();
                                if (dataParNames.Count > 0)
                                {
                                    //dataParNames[0].IP_INT_NO = par.IP_INT_NO;/giu nguyen stt
                                    dataParNames[0].IP_INT_NO = par.IP_INT_NO;
                                    dataParNames[0].IP_NAME = par.IP_NAME;
                                    if(!string.IsNullOrEmpty(par.IP_WK_ROLE))
                                    {
                                        dataParNames[0].IP_WK_ROLE = par.IP_WK_ROLE;
                                    }
                                    
                                    //dataParNames[0].WK_STATUS = "COMPLETE";

                                    dataParNames[0].PER_OWN_SHR = par.PER_OWN_SHR;
                                    dataParNames[0].PER_COL_SHR = par.PER_COL_SHR;

                                    dataParNames[0].MEC_OWN_SHR = par.MEC_OWN_SHR;
                                    dataParNames[0].MEC_COL_SHR = par.MEC_COL_SHR;

                                    dataParNames[0].SP_SHR = par.SP_SHR;
                                    dataParNames[0].TOTAL_MEC_SHR = par.TOTAL_MEC_SHR;

                                    dataParNames[0].SYN_OWN_SHR = par.SYN_OWN_SHR;
                                    dataParNames[0].SYN_COL_SHR = par.SYN_COL_SHR;
                                    if(!string.IsNullOrEmpty(par.Society))
                                    {
                                        dataParNames[0].Society = par.Society;
                                    }
                                    if (!string.IsNullOrEmpty(par.IP_NAMETYPE))
                                    {
                                        dataParNames[0].IP_NAMETYPE = par.IP_NAMETYPE;
                                    }
                                    dataParNames[0].CountUpdate++;
                                    dataParNames[0].LastUpdateAt = DateTime.Now;
                                    dataParNames[0].LastChoiseAt = DateTime.Now;
                                }
                                else
                                {
                                    int maxNo = In.InterestedParties.Count + 1;                                    
                                    par.No = maxNo;
                                    In.InterestedParties.Add(par);
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region trang thai
                        //2020-10-02 TODO
                        //foreach (var par in In.InterestedParties)
                        //{
                        //    if (par.WK_STATUS != "COMPLETE")
                        //    {
                        //        isComplete = false;
                        //        break;
                        //    }
                        //}
                        //In.WK_STATUS = isComplete == true ? "COMPLETE" : "INCOMPLETE";
                        In.WK_STATUS = In.WK_STATUS;
                        #endregion


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
                            objectReturn.WorkCode = In.WK_INT_NO;                            
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.WK_INT_NO;
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

                #region Cap nhat danh sach tac gia co ma
                List<MemberCreateRequest> memberList = new List<MemberCreateRequest>();
                foreach (var item in request)
                {
                    foreach (var itemX in item.InterestedParties)
                    {
                        if(itemX.IP_INT_NO!=string.Empty && !memberList.Where(P=>P.InternalNo == itemX.IP_INT_NO).Any())
                        {
                            MemberCreateRequest mb = new MemberCreateRequest();
                            mb.IpiNumber = itemX.IP_NUMBER;
                            mb.InternalNo = itemX.IP_INT_NO;
                            mb.IpEnglishName = itemX.IP_NAME;
                            mb.IpLocalName = itemX.IP_NAME_LOCAL;
                            mb.NameType = itemX.IP_NAMETYPE;
                            mb.Society = itemX.Society;
                            memberList.Add(mb);
                        }   
                    }
                }
                if(memberList.Count>0)
                {
                   var dataUpdatMb =  _memberService.ChangeList(memberList, _mapper);
                }
                #endregion
                return listReturn;
            }
            catch (Exception ex)
            {
                return listReturn;
            }
        }
        #endregion

        #region Sync
        public async Task<List<UpdateStatusViewModel>> SyncFromTrackingWorkToWork(GetWorkTrackingPagingRequest request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                var workTrackings = await _workTrackingService.GetWorkTracking(request);

                Work In;

                List<string> workCodes = new List<string>();

                foreach (var item in workTrackings)
                {
                    if (!workCodes.Contains(item.WK_INT_NO))
                    {
                        workCodes.Add(item.WK_INT_NO);
                    }
                }
                var dataCheck = await GetByWorkCodes(workCodes);
                List<Work> insertList = new List<Work>();
                foreach (var item in workTrackings)
                {
                    #region change
                    In = new Work();
                    In.Id = item.Id;
                    In.WK_INT_NO = item.WK_INT_NO;
                    In.TTL_ENG = item.TTL_ENG == null?string.Empty: item.TTL_ENG;
                    In.ISWC_NO = item.ISWC_NO == null ? string.Empty : item.ISWC_NO; 
                    In.ISRC = item.ISRC == null ? string.Empty : item.ISRC;
                    In.WRITER = item.WRITER == null ? string.Empty : item.WRITER;
                    In.ARTIST = item.ARTIST == null ? string.Empty : item.ARTIST;
                    In.SOC_NAME = item.SOC_NAME == null ? string.Empty : item.SOC_NAME;
                    In.WK_STATUS = "COMPLETE";
                    In.StarRating = 0;

                    var data = dataCheck
                        .Where(
                                p => p.WK_INT_NO == In.WK_INT_NO
                            )
                        .ToList();
                    if (data != null && data.Count == 0)
                    {
                        #region note Add
                        In.Id = string.Empty;
                        insertList.Add(In);
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.WorkCode = In.WK_INT_NO;
                        objectReturn.Command = CommandType.Add;
                        listReturn.Add(objectReturn);
                        #endregion
                    }
                    //update
                    else
                    {
                        #region Update   
                        In.Id = data[0].Id;
                        In.WK_STATUS = data[0].WK_STATUS;
                        In.StarRating = data[0].StarRating;
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
                            objectReturn.WorkCode = In.WK_INT_NO;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.WK_INT_NO;
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

        #region Matching
        //public async Task<List<ObjectId>> GetWorkByLikeListTitles(IList<string> Titles)
        public async Task<List<Work>> GetWorkByLikeListTitles(IList<string> Titles)
        {
            /*
             var filter = Builders<Book>.Filter.Or(
                Builders<Book>.Filter.Where(p=>p.Title.ToLower().Contains(queryText.ToLower())),
                Builders<Book>.Filter.Where(p => p.Publisher.ToLower().Contains(queryText.ToLower())),
                Builders<Book>.Filter.Where(p => p.Description.ToLower().Contains(queryText.ToLower()))
            );
             */
            var regexFilter = "(" + string.Join("|", Titles) + ")";
            var projection = Builders<Work>.Projection.Include(x => x.Id);
            //var filter = Builders<Work>.Filter.Regex("TTL_ENG,OtherTitles.Title",//TTL_ENG
            //      new BsonRegularExpression(new Regex(regexFilter)));
            var filter = Builders<Work>.Filter.Or(
                    Builders<Work>.Filter.Regex("TTL_ENG",
                      new BsonRegularExpression(new Regex(regexFilter))),
                    Builders<Work>.Filter.Regex("OtherTitles.Title",
                      new BsonRegularExpression(new Regex(regexFilter)))
                );
            //var entities = await _collection.Find(filter).Project(projection).ToListAsync();
            var entities = await _collection.Find(filter).ToListAsync();
            //return entities.Select(x => x["_id"].AsObjectId).ToList();
            return entities;
        }
        public async Task<List<Work>> GetWorkByLikeListTitlesCorrect(IList<string> Titles)
        {
            //1. Select join
            //TODO
            var query = (from e in _collection.AsQueryable<Work>()
                         select e);
            query = query.Where(p => Titles.Contains(p.TTL_ENG) || p.OtherTitles.Where(x => Titles.Contains(x.Title)).Any());
            //2. filter     
            //query = query.Where(p => Titles.Contains(p.TTL_ENG));
            //3. Paging
            var list = await query
                .ToListAsync();            
            return list;
        }
        public async Task<PagedResult<WorkViewModel>> MatchingWork(WorkMatchingListRequest request, IMapper _mapper)
        {
            List<string> titleList = new List<string>();
            if (request != null && request.Items != null)
            {
                //int i = 0;
                foreach (var item in request.Items)
                {
                    //i++;
                    //if(i > 50)
                    //{
                    //    break;
                    //}  
                    if(item.Title2!=string.Empty)
                    {
                        if (!titleList.Contains(item.Title2))
                        {
                            titleList.Add(item.Title2);
                        }
                    }                                       
                }
            }
            List<Work> list;
            if(titleList.Count>0)
            {
                //TODO
                //list = await GetWorkByLikeListTitles(titleList);
                list = await GetWorkByLikeListTitlesCorrect(titleList);
            }
            else
            {
                list = new List<Work>();
            }
            List<WorkViewModel> models = new List<WorkViewModel>();
            //List<MonopolyViewModel> monoWorks = new List<MonopolyViewModel>();
            //List<MonopolyViewModel> monoMembers = new List<MonopolyViewModel>();
            if (list != null && list.Count > 0)
            {
                #region Doc quyen tac pham(remove)
                //List<string> MonopolyCodes = new List<string>();
                //foreach (var item in list)
                //{
                //    if(!MonopolyCodes.Contains(item.WK_INT_NO))
                //    {
                //        MonopolyCodes.Add(item.WK_INT_NO);
                //    }                    
                //}
                //var dataCheckMonoWork = await _monoService.GetByGroupAndWorkCodes(0, MonopolyCodes);
                //if (list != null && dataCheckMonoWork.Count > 0)
                //{
                //    for (int i = 0; i < dataCheckMonoWork.Count; i++)
                //    {
                //        MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(dataCheckMonoWork[i]);
                //        monoWorks.Add(preVM);
                //    }
                //}
                #endregion

                #region Doc quyen tac gia(remove)
                ////TODO
                ////khong doc quyen tac gia
                //List<string> writerCodes = new List<string>();
                //foreach (var item in list)
                //{
                //    //if(item.TTL_ENG == "DUNG HOI EM")
                //    //{
                //    //    int a = 1;
                //    //}
                //    foreach (var x in item.InterestedParties)
                //    {
                //        if(x.IP_INT_NO!=string.Empty)
                //        {
                //            if (!writerCodes.Contains(item.WK_INT_NO))
                //            {
                //                writerCodes.Add(x.IP_INT_NO);
                //            }                            
                //        }    
                //    }                    
                //}
                //var dataCheckMonomemer = await _monoService.GetByGroupAndWorkCodes(1, writerCodes);
                //if (list != null && dataCheckMonomemer.Count > 0)
                //{

                //    for (int i = 0; i < dataCheckMonomemer.Count; i++)
                //    {
                //        MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(dataCheckMonomemer[i]);
                //        monoMembers.Add(preVM);
                //    }
                //}
                #endregion

                for (int i = 0; i < list.Count; i++)
                {
                    WorkViewModel preVM = _mapper.Map<WorkViewModel>(list[i]);
                    #region gan doc quyen tac pham(remove)
                    //if (monoWorks.Count > 0)
                    //{
                    //    var itemMonoWork = monoWorks.Where(p => p.CodeNew == preVM.WK_INT_NO).ToList();
                    //    if (itemMonoWork != null)
                    //    {
                    //        preVM.MonopolyWorks = itemMonoWork;
                    //    }
                    //}
                    #endregion

                    #region gan doc quyen tac gia(remove)
                    //if (monoMembers.Count > 0)
                    //{
                    //    //TODO
                    //    var itemMonoMember = monoMembers.Where(p => preVM.InterestedParties.Where(x => x.IP_INT_NO.Contains(p.CodeNew) && x.IP_NAMETYPE == p.NameType).Any()).ToList();
                    //    if (itemMonoMember != null)
                    //    {
                    //        preVM.MonopolyMembers = itemMonoMember;
                    //    }
                    //}
                    #endregion

                    preVM.SerialNo = (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<WorkViewModel>()
            {
                TotalRecords = list.Count,
                PageSize = list.Count,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        #endregion
    }
}
