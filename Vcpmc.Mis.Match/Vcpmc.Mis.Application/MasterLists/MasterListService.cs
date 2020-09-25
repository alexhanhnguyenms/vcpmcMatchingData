using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vcpmc.Mis.Application.Media.Youtube;
using Vcpmc.Mis.Data.Entities.MasterLists;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.Utilities;
using Vcpmc.Mis.Utilities.Common;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.MasterLists;

namespace Vcpmc.Mis.Application.MasterLists
{
    public class MasterListService: IMasterListService
    {
        private readonly IMongoCollection<MasterList> _collection;
        private readonly PreclaimService _preclaimService;
        public MasterListService(IDatabaseSettings settings, PreclaimService preclaimService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<MasterList>(settings.MasterListsCollectionName);
            _preclaimService = preclaimService;
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMasterListPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<MasterList> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<MasterListViewModel>> GetAllPaging(GetMasterListPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestMasterlist)
            {
                request.PageSize = LimitRequestBackend.LimitRequestMasterlist;
            }
            IMongoQueryable<MasterList> query = CreateQueryGetAllPaging(request);
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<MasterListViewModel> models = new List<MasterListViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    MasterListViewModel preVM = _mapper.Map<MasterListViewModel>(list[i]);
                    preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<MasterListViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<MasterList> CreateQueryGetAllPaging(GetMasterListPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<MasterList>()
                         select e);
            //2. filter  
            //if (request.WK_INT_NO != null && request.WK_INT_NO != string.Empty)
            //{
            //    query = query.Where(p => p.WK_INT_NO.Contains(request.WK_INT_NO));
            //}      
            query = query.Where(p => p.Year == request.Year && p.Month == request.Month);

            if (request.ReportType != -1)
            {
               if(request.ReportType  == 1)
               {
                    query = query.Where(p => p.IsReport1 == true);
               }
               else if (request.ReportType == 2)
                {
                    query = query.Where(p => p.IsReport2 == true);
                }
               else if (request.ReportType == 3)
                {
                    query = query.Where(p => p.IsReport3 == true);
                }
            }

            if(request.IsNeedDetectAPI)
            {
                query = query.Where(p => p.IsNeedDetectAPI == true && p.IsDetectAPI == false && p.ScoreDetect2Algorithm > 0);
            }

            return query;
        }
        public async Task<List<MasterList>> GetWorkByLikeListTitles(IList<string> writers)
        {
            var regexFilter = "(" + string.Join("|", writers) + ")";
            var projection = Builders<MasterList>.Projection.Include(x => x.Id);
            var filter = Builders<MasterList>.Filter.Regex("Own2",
                  new BsonRegularExpression(new Regex(regexFilter, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace)));
            //var entities = await _collection.Find(filter).Project(projection).ToListAsync();
            var entities = await _collection.Find(filter).ToListAsync();
            //return entities.Select(x => x["_id"].AsObjectId).ToList();
            return entities;
        }
        public async Task<PagedResult<MasterListViewModel>> GetById(string Id, IMapper _mapper)
        {
            // 1.Select
            var preclaim = await GetById(Id);
            //4.map
            List<MasterListViewModel> models = new List<MasterListViewModel>();
            int totalRow = 0;
            if (preclaim != null)
            {
                MasterListViewModel preVM = _mapper.Map<MasterListViewModel>(preclaim);
                preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<MasterListViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<MasterList> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<MasterList>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<MasterList>> GetByMasterListCodes(int year, int month, List<string> masterListCodes)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<MasterList>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Year == year && p.Month == month && masterListCodes.Contains(p.ID_youtube));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        #endregion

        #region Update  
        public async Task<List<UpdateStatusViewModel>> ChangeList(int year, int month, List<MasterListCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                MasterList In;

                List<string> MasterListCodes = new List<string>();

                foreach (var item in request)
                {
                    MasterListCodes.Add(item.ID_youtube);
                }

                #region C_workcode trong preclaim
                var dataPreclaims = await _preclaimService.GetByAsset_ids(MasterListCodes);               
                #endregion
                var dataCheck = await GetByMasterListCodes(year, month, MasterListCodes);
                List<MasterList> insertList = new List<MasterList>();
                foreach (var item in request)
                {
                    #region change
                    In = _mapper.Map<MasterList>(item);
                    //preclaim
                    var datapreclaim = dataPreclaims
                        .Where(
                                p => p.Asset_ID == In.ID_youtube
                            )
                        .FirstOrDefault();
                    if(datapreclaim!=null)
                    {
                        In.C_Workcode = datapreclaim.C_Workcode;
                    }
                    //máterlist
                    var data = dataCheck
                        .Where(
                                p => p.Year == year && p.Month == month && p.ID_youtube == In.ID_youtube
                            )
                        .ToList();
                    if (data != null && data.Count == 0)
                    {
                        #region note Add
                        //In.Group = group;
                        insertList.Add(In);
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.WorkCode = In.ID_youtube;
                        objectReturn.Command = CommandType.Add;
                        listReturn.Add(objectReturn);
                        #endregion
                    }
                    //update
                    else
                    {
                        #region Update   
                        In.Id = data[0].Id;
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
                            objectReturn.WorkCode = In.ID_youtube;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.ID_youtube;
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
