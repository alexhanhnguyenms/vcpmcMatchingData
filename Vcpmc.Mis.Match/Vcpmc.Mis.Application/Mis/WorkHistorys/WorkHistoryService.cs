using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using MongoDB.Driver.Linq;
using System.Linq;
using Vcpmc.Mis.Utilities.Common;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using Vcpmc.Mis.Application.Mis.Monopolys;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.Utilities;
using Vcpmc.Mis.Data.Entities.Mis.Historys;
using Vcpmc.Mis.ViewModels.Mis.History;
using Vcpmc.Mis.Shared.Mis.Works;

namespace Vcpmc.Mis.Application.Mis.WorkHistorys
{
    public class WorkHistoryService:IWorkHistoryService
    {
        private readonly IMongoCollection<WorkHistory> _collection;       
        public WorkHistoryService(IDatabaseSettings settings, MonopolyService monoService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<WorkHistory>(settings.WorkHistorysCollectionName);
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkHistoryPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<WorkHistory> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<WorkHistoryViewModel>> GetAllPaging(GetWorkHistoryPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestWorkHistory)
            {
                request.PageSize = LimitRequestBackend.LimitRequestWorkHistory;
            }
            IMongoQueryable<WorkHistory> query = CreateQueryGetAllPaging(request);
            //TODO: them 1 qery thay so luong dau tien????
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<WorkHistoryViewModel> models = new List<WorkHistoryViewModel>();
            List<MonopolyViewModel> monoWorkHistorys = new List<MonopolyViewModel>();
            List<MonopolyViewModel> monoMembers = new List<MonopolyViewModel>();
            
            //4. Select and projection               
            var pagedResult = new PagedResult<WorkHistoryViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<WorkHistory> CreateQueryGetAllPaging(GetWorkHistoryPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            //2. filter  
            //if (request.SearchType == 0)
            //{
                if (!string.IsNullOrEmpty(request.Title2))
                {
                    query = query.Where(p => p.Title2 == request.Title2);
                }
                if (!string.IsNullOrEmpty(request.Artist2))
                {
                    query = query.Where(p => p.Artist2.Contains(request.Artist2));
                }
                if (!string.IsNullOrEmpty(request.WK_Artist2))
                {
                    query = query.Where(p => p.WK_Artist2.Contains(request.WK_Artist2));
                }
                if (!string.IsNullOrEmpty(request.WK_INT_NO))
                {
                    query = query.Where(p => p.WK_INT_NO == (request.WK_INT_NO));
                }
                if (!string.IsNullOrEmpty(request.WK_Title2))
                {
                    query = query.Where(p => p.WK_Title2 == (request.WK_Title2));
                }
                if (!string.IsNullOrEmpty(request.Artist2))
                {
                    query = query.Where(p => p.Artist2.Contains(request.Artist2));
                }
                if (!string.IsNullOrEmpty(request.WK_Writer2))
                {
                    query = query.Where(p => p.InterestedParties.Where(x => x.IP_NAME.Contains(request.WK_Writer2)).Any());
                }                
            //} 
            return query;
        }

        public async Task<PagedResult<WorkHistoryViewModel>> GetById(string Id, IMapper _mapper)
        {
            // 1.Select
            var preclaim = await GetById(Id);
            //4.map
            List<WorkHistoryViewModel> models = new List<WorkHistoryViewModel>();
            int totalRow = 0;
            if (preclaim != null)
            {
                WorkHistoryViewModel preVM = _mapper.Map<WorkHistoryViewModel>(preclaim);
                preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<WorkHistoryViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<WorkHistory> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<WorkHistory>> GetByWorkHistoryCode(string WorkHistoryCode)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            //2. filter     
            query = query.Where(p => WorkHistoryCode == p.WK_INT_NO);
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkHistoryViewModel>> GetByWorkHistoryCodes(List<string> WorkHistoryCodes, IMapper _mapper)
        {
            //1.get
            var list = await GetByWorkHistoryCodes(WorkHistoryCodes);
            //2.map
            int totalRow = list.Count;
            List<WorkHistoryViewModel> models = new List<WorkHistoryViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkHistoryViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkHistoryViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<WorkHistory>> GetByWorkHistoryCodes(List<string> WorkHistoryCodes)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            //2. filter     
            query = query.Where(p => WorkHistoryCodes.Contains(p.WK_INT_NO));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkHistoryViewModel>> GetByTitles(List<string> titles, IMapper _mapper)
        {
            //1.get
            var list = await GetByTitles(titles);
            //2.map
            int totalRow = list.Count;
            List<WorkHistoryViewModel> models = new List<WorkHistoryViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<WorkHistoryViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<WorkHistoryViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<WorkHistory>> GetByTitles(List<string> titles)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            //2. filter     
            query = query.Where(p => titles.Contains(p.Title2));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        #endregion

        #region Update  
        public async Task<List<UpdateStatusViewModel>> ChangeList(List<WorkHistoryCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                WorkHistory In;
                List<string> WorkHistoryCodes = new List<string>();
                foreach (var item in request)
                {
                    if (!WorkHistoryCodes.Contains(item.WK_INT_NO))
                    {
                        WorkHistoryCodes.Add(item.WK_INT_NO);
                    }
                }
                var dataCheck = await GetByWorkHistoryCodes(WorkHistoryCodes);
                List<WorkHistory> insertList = new List<WorkHistory>();
                foreach (var item in request)
                {
                    #region change                    
                    In = _mapper.Map<WorkHistory>(item);

                    var data = dataCheck
                        .Where(
                                p => p.WK_INT_NO == In.WK_INT_NO
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
                        if (string.IsNullOrEmpty(In.Artist))
                        {
                            In.Artist = data[0].Artist;
                        }
                        else
                        {
                            int a = 1;
                        }
                        In.OtherTitles = data[0].OtherTitles;
                        In.InterestedParties = data[0].InterestedParties;
                        In.WK_STATUS = data[0].WK_STATUS;
                        //bool isComplete = true;

                        #region Cap nhat tac pham
                        foreach (var par in otherTitleNews)
                        {
                            #region Add danh sach tac gia
                            if (In.OtherTitles.Where(p => p.Title == par.Title.Trim()).ToList().Count == 0)
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
                                #region nêu đã có tác gia có mã, thì cập nhật thêm thông tin                               
                                x2 = dataPar.IP_NAME;
                                x22 = dataPar.IP_INT_NO;
                                //dataPar.No = dataPar.No;//giu nguyen stt
                                //dataPar.IP_INT_NO = par.IP_INT_NO;   
                                //thong tin moi co dulieu moi cap nhat, khong thi van de cai cu
                                if (!string.IsNullOrEmpty(par.IP_NAME))
                                {
                                    dataPar.IP_NAME = par.IP_NAME;
                                }
                                if (!string.IsNullOrEmpty(par.IP_NAME_LOCAL))
                                {
                                    dataPar.IP_NAME_LOCAL = par.IP_NAME_LOCAL;
                                }
                                if (!string.IsNullOrEmpty(par.IP_WK_ROLE))
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
                                if (!string.IsNullOrEmpty(par.IP_NAMETYPE))
                                {
                                    dataPar.IP_NAMETYPE = par.IP_NAMETYPE;
                                }
                                dataPar.CountUpdate++;
                                dataPar.LastUpdateAt = DateTime.Now;
                                dataPar.LastChoiseAt = DateTime.Now;
                                //int maxNo = In.InterestedParties.Count;
                                //dataPar.No = maxNo;
                                //In.InterestedParties.Add(par);
                                #endregion
                            }
                            else
                            {
                                #region Nếu tác giả chỉ chưa có mã thì cập nhật cả mã. Ngược lại thêm mới                                
                                var dataParNames = In.InterestedParties.Where(p => p.IP_NAME == par.IP_NAME).ToList();
                                if (dataParNames.Count > 0)
                                {
                                    //dataParNames[0].IP_INT_NO = par.IP_INT_NO;/giu nguyen stt
                                    dataParNames[0].IP_INT_NO = par.IP_INT_NO;
                                    dataParNames[0].IP_NAME = par.IP_NAME;
                                    dataParNames[0].IP_NAME_LOCAL = par.IP_NAME_LOCAL;
                                    if (!string.IsNullOrEmpty(par.IP_WK_ROLE))
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
                                    if (!string.IsNullOrEmpty(par.Society))
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
                                #endregion
                            }
                            #endregion
                        }
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
                
                return listReturn;
            }
            catch (Exception ex)
            {
                return listReturn;
            }
        }
        #endregion

        #region Sync
        
        #endregion

        #region Matching
        //public async Task<List<ObjectId>> GetWorkHistoryByLikeListTitles(IList<string> Titles)
        public async Task<List<WorkHistory>> GetWorkHistoryByLikeListTitles(IList<string> Titles)
        {
            /*
             var filter = Builders<Book>.Filter.Or(
                Builders<Book>.Filter.Where(p=>p.Title.ToLower().Contains(queryText.ToLower())),
                Builders<Book>.Filter.Where(p => p.Publisher.ToLower().Contains(queryText.ToLower())),
                Builders<Book>.Filter.Where(p => p.Description.ToLower().Contains(queryText.ToLower()))
            );
             */
            var regexFilter = "(" + string.Join("|", Titles) + ")";
            var projection = Builders<WorkHistory>.Projection.Include(x => x.Id);
            //var filter = Builders<WorkHistory>.Filter.Regex("TTL_ENG,OtherTitles.Title",//TTL_ENG
            //      new BsonRegularExpression(new Regex(regexFilter)));
            var filter = Builders<WorkHistory>.Filter.Or(
                    Builders<WorkHistory>.Filter.Regex("TTL_ENG",
                      new BsonRegularExpression(new Regex(regexFilter))),
                    Builders<WorkHistory>.Filter.Regex("OtherTitles.Title",
                      new BsonRegularExpression(new Regex(regexFilter)))
                );
            //var entities = await _collection.Find(filter).Project(projection).ToListAsync();
            var entities = await _collection.Find(filter).ToListAsync();
            //return entities.Select(x => x["_id"].AsObjectId).ToList();
            return entities;
        }
        public async Task<List<WorkHistory>> GetWorkHistoryByLikeListTitlesCorrect(IList<string> Titles)
        {
            //1. Select join
            //TODO
            var query = (from e in _collection.AsQueryable<WorkHistory>()
                         select e);
            query = query.Where(p => Titles.Contains(p.Title2) || p.OtherTitles.Where(x => Titles.Contains(x.Title)).Any());
            //2. filter     
            //query = query.Where(p => Titles.Contains(p.TTL_ENG));
            //3. Paging
            var list = await query
                .ToListAsync();
            return list;
        }
        public async Task<PagedResult<WorkHistoryViewModel>> MatchingWorkHistory(WorkHistoryMatchingListRequest request, IMapper _mapper)
        {
            List<string> titleList = new List<string>();
            if (request != null && request.Items != null)
            {               
                foreach (var item in request.Items)
                {                   
                    if (item.Title2 != string.Empty)
                    {
                        if (!titleList.Contains(item.Title2))
                        {
                            titleList.Add(item.Title2);
                        }
                    }
                }
            }
            List<WorkHistory> list;
            if (titleList.Count > 0)
            {                
                list = await GetWorkHistoryByLikeListTitlesCorrect(titleList);
            }
            else
            {
                list = new List<WorkHistory>();
            }
            List<WorkHistoryViewModel> models = new List<WorkHistoryViewModel>();           
            if (list != null && list.Count > 0)
            {                

                for (int i = 0; i < list.Count; i++)
                {
                    WorkHistoryViewModel preVM = _mapper.Map<WorkHistoryViewModel>(list[i]);
                    preVM.SerialNo = (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<WorkHistoryViewModel>()
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
