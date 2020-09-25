using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mis.Works;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;
using MongoDB.Driver.Linq;
using System.Linq;
using Vcpmc.Mis.Utilities.Common;
using MongoDB.Bson;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.Mis.Works.Tracking
{
    public class WorkTrackingService:IWorkTrackingService
    {
        private readonly IMongoCollection<WorkTracking> _collection;
        //private readonly IMongoCollection _collection2;
        public WorkTrackingService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<WorkTracking>(settings.WorkTrackingsCollectionName);
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetWorkTrackingPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<WorkTracking> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<WorkTrackingViewModel>> GetAllPaging(GetWorkTrackingPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestTrackingWork)
            {
                request.PageSize = LimitRequestBackend.LimitRequestTrackingWork;
            }
            IMongoQueryable<WorkTracking> query = CreateQueryGetAllPaging(request);

            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1)* request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<WorkTrackingViewModel> models = new List<WorkTrackingViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    WorkTrackingViewModel preVM = _mapper.Map<WorkTrackingViewModel>(list[i]);
                    preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<WorkTrackingViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<WorkTracking> CreateQueryGetAllPaging(GetWorkTrackingPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkTracking>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Year == request.Year);
            query = query.Where(p => p.MONTH == request.MONTH);
            query = query.Where(p => p.Type == request.Type);
            return query;
        }
        public async Task<List<WorkTracking>> GetWorkTracking(GetWorkTrackingPagingRequest request)
        {
            IMongoQueryable<WorkTracking> query = CreateQueryGetAllPaging(request);

            //3. Paging           
            var list = await query
                .Skip((request.PageIndex - 1)*request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();            
            return list;
        }
        public async Task<WorkTracking> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkTracking>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<WorkTracking>> GetByWorkCodes(int year, int month,int type, List<string> workCodes)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<WorkTracking>()
                         select e);
            //2. filter     
            query = query.Where(
                p => workCodes.Contains(p.WK_INT_NO) 
                && p.Year == year
                && p.MONTH == month
                &&p.Type ==type
                );
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<WorkTrackingAggregateViewModel>> GetArreggateMasterList(GetWorkTrackingPagingRequest request)
        {
            BsonDocument match = null;
            if(request.MONTH == -1)
            {
                match  = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"Year", request.Year}                               
                            }
                    }
                };
            }
            else
            {
                match  = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"Year", request.Year},
                                {"MONTH", request.MONTH},
                            }
                    }
                };
            }
            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", new BsonDocument
                                             {
                                                 { "Year","$Year" },
                                                 { "MONTH","$MONTH" },
                                                 { "Type","$Type" },
                                             }
                                },
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     { "$sum", 1 }//{ "$sum", "$Count" }
                                                 }
                                }
                            }
                  }
                };

            var project = new BsonDocument
                {
                    {
                        "$project",
                        new BsonDocument
                            {
                                {"_id", 0},
                                {"Year","$_id.Year"},
                                {"MONTH", "$_id.MONTH"},
                                {"Type", "$_id.Type"},
                                {"Count", 1},
                            }
                    }
                };

            var pipeline = new[] { match, group, project };
            var result = await _collection.AggregateAsync<WorkTrackingAggregateViewModel>(pipeline);

            var matchingExamples = result//.ResultDocuments
                //.Select(x => x.ToDynamic())
                .ToList();

            //foreach (var example in matchingExamples)
            //{
            //    var message = string.Format("{0} - {1} - {2}", example.Year, example.MONTH, example.Count);
            //    Console.WriteLine(message);
            //}                   
            var pagedResult = new PagedResult<WorkTrackingAggregateViewModel>()
            {
                TotalRecords = matchingExamples.Count,
                PageSize = matchingExamples.Count,
                PageIndex = matchingExamples.Count,
                Items = matchingExamples
            };
            return pagedResult;
        }
        #endregion

        #region Update          
        public async Task<List<UpdateStatusViewModel>> ChangeList(int year, int month, int type,List<WorkTrackingCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                WorkTracking In;

                List<string> workCodes = new List<string>();

                foreach (var item in request)
                {
                    workCodes.Add(item.WK_INT_NO);
                }
                var dataCheck = await GetByWorkCodes(year, month, type, workCodes);
                List<WorkTracking> insertList = new List<WorkTracking>();
                foreach (var item in request)
                {
                    #region change
                    In = _mapper.Map<WorkTracking>(item);

                    var data = dataCheck
                        .Where(
                                p => p.WK_INT_NO == In.WK_INT_NO
                            )
                        .ToList();
                    if (data != null && data.Count == 0)
                    {
                        #region note Add
                        In.TimeCreate = DateTime.Now;
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
                        In.TimeCreate = data[0].TimeCreate;
                        In.TimeUpdate = DateTime.Now;
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
    }
}
