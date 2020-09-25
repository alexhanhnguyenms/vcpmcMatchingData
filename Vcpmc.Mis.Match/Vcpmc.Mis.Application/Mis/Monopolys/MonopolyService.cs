using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;
using Vcpmc.Mis.Data.Entities.Mis.Monopolys;
using MongoDB.Driver.Linq;
using Vcpmc.Mis.Utilities.Common;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.Mis.Monopolys
{
    public class MonopolyService: IMonopolyService
    {
        private readonly IMongoCollection<Monopoly> _collection;
        public MonopolyService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Monopoly>(settings.MonopolysCollectionName);
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetMonopolyPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<Monopoly> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<MonopolyViewModel>> GetAllPaging(GetMonopolyPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestMonopoly)
            {
                request.PageSize = LimitRequestBackend.LimitRequestMonopoly;
            }
            IMongoQueryable<Monopoly> query = CreateQueryGetAllPaging(request);
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<MonopolyViewModel> models = new List<MonopolyViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(list[i]);
                    preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<MonopolyViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<Monopoly> CreateQueryGetAllPaging(GetMonopolyPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Monopoly>()
                         select e);
            //2. filter  
            //if (request.WK_INT_NO != null && request.WK_INT_NO != string.Empty)
            //{
            //    query = query.Where(p => p.WK_INT_NO.Contains(request.WK_INT_NO));
            //}  
            if(request.SearchType == 0)
            {
                //tim like
                if (!string.IsNullOrEmpty(request.CodeNew))
                {
                    query = query.Where(p => p.CodeNew == request.CodeNew);
                }
                if (request.Group == 0)
                {
                    query = query.Where(p => p.Group == request.Group);
                    if (!string.IsNullOrEmpty(request.Name2))
                    {
                        query = query.Where(p => p.Name2.Contains(request.Name2));
                    }
                }
                else if (request.Group == 1)
                {
                    query = query.Where(p => p.Group == request.Group);                  
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Own2.Contains(request.Own2));
                    }
                }
                else if (request.Group == 2)
                {
                    //form search, thi searchung
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Name2.Contains(request.Own2) || p.Own2.Contains(request.Own2));//vi chi truyen para qua Own2
                    }
                }
                else if (request.Group == 3)
                {
                    query = query.Where(p => p.Group == 0);
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Name2.Contains(request.Own2));//vi chi truyen para qua Own2
                    }
                }
                else if (request.Group == 4)
                {
                    query = query.Where(p => p.Group == 1);                  
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Own2.Contains(request.Own2));
                    }
                }
            }
            else
            {
                //tim chinh xac
                if (!string.IsNullOrEmpty(request.CodeNew))
                {
                    query = query.Where(p => p.CodeNew == request.CodeNew);
                }
                if (request.Group == 0)
                {
                    query = query.Where(p => p.Group == request.Group);
                    if (!string.IsNullOrEmpty(request.Name2))
                    {
                        query = query.Where(p => p.Name2 == (request.Name2));
                    }
                }
                else if (request.Group == 1)
                {
                    query = query.Where(p => p.Group == request.Group);
                    //var aa = GetWorkByLikeListTitles()
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Own2 == (request.Own2));
                    }
                }
                else if (request.Group == 2)
                {
                    //form search, thi searchung
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Name2 == request.Own2 || p.Own2 == (request.Own2));//vi chi truyen para qua Own2
                    }
                }
                else if (request.Group == 3)
                {
                    query = query.Where(p => p.Group == 0);
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Name2 == (request.Own2));//vi chi truyen para qua Own2
                    }
                }
                else if (request.Group == 4)
                {
                    query = query.Where(p => p.Group == 1);
                    //var aa = GetWorkByLikeListTitles()
                    if (!string.IsNullOrEmpty(request.Own2))
                    {
                        query = query.Where(p => p.Own2 == (request.Own2));
                    }
                }
            }
           
            return query;
        }
        public async Task<List<Monopoly>> GetWorkByLikeListTitles(IList<string> writers)
        {
            var regexFilter = "(" + string.Join("|", writers) + ")";
            var projection = Builders<Monopoly>.Projection.Include(x => x.Id);
            var filter = Builders<Monopoly>.Filter.Regex("Own2",
                  new BsonRegularExpression(new Regex(regexFilter, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace)));
            //var entities = await _collection.Find(filter).Project(projection).ToListAsync();
            var entities = await _collection.Find(filter).ToListAsync();
            //return entities.Select(x => x["_id"].AsObjectId).ToList();
            return entities;
        }
        public async Task<PagedResult<MonopolyViewModel>> GetById(string Id, IMapper _mapper)
        {
            // 1.Select
            var preclaim = await GetById(Id);
            //4.map
            List<MonopolyViewModel> models = new List<MonopolyViewModel>();
            int totalRow = 0;
            if (preclaim != null)
            {
                MonopolyViewModel preVM = _mapper.Map<MonopolyViewModel>(preclaim);
                preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<MonopolyViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<Monopoly> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Monopoly>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<List<Monopoly>> GetBygroupAndWorkCode(int group,string MonopolyCode)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Monopoly>()
                         select e);
            //2. filter     
            query = query.Where(p => group == p.Group && MonopolyCode == p.CodeNew);
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<MonopolyViewModel>> GetByGroupAndWorkCodes(int group,List<string> MonopolyCodes, IMapper _mapper)
        {
            //1.get
            var list = await GetByGroupAndWorkCodes(group,MonopolyCodes);
            //2.map
            int totalRow = list.Count;
            List<MonopolyViewModel> models = new List<MonopolyViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<MonopolyViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<MonopolyViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Monopoly>> GetByGroupAndWorkCodes(int group, List<string> MonopolyCodes)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Monopoly>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Group ==group && MonopolyCodes.Contains(p.CodeNew));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<MonopolyViewModel>> GetByTitles(int group,List<string> titles, IMapper _mapper)
        {
            //1.get
            var list = await GetByGroupAndTitles(group,titles);
            //2.map
            int totalRow = list.Count;
            List<MonopolyViewModel> models = new List<MonopolyViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<MonopolyViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<MonopolyViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Monopoly>> GetByGroupAndTitles(int group, List<string> titles)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<Monopoly>()
                         select e);
            //2. filter   
            if(group==0)
            {
                query = query.Where(p => p.Group == group && titles.Contains(p.Name2));
            }   
            else
            {
                //var aa = GetWorkByLikeListTitles(titles);
                query = query.Where(p => p.Group == group && titles.Contains(p.Own2));
            }            
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }    
        #endregion

        #region Update  
        public async Task<UpdateStatusViewModel> Create(MonopolyCreateRequest request, IMapper _mapper)
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
                Monopoly In = _mapper.Map<Monopoly>(request);
                var data = await GetBygroupAndWorkCode(request.Group,In.CodeNew);
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
        public async Task<UpdateStatusViewModel> Update(MonopolyUpdateRequest request, IMapper _mapper)
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
                Monopoly In = _mapper.Map<Monopoly>(request);
                var data = await GetBygroupAndWorkCode(request.Group, request.CodeNew);
                if(data.Count>0)
                {
                    var dataCheck = data
                        .Where(
                                p => p.Id != In.Id
                            )
                        .ToList();
                    if (dataCheck.Count > 0)
                    {
                        #region note Add
                        objectReturn.Message = "Update records failure because Existence code new";
                        return objectReturn;
                        #endregion
                    }
                }
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
        public async Task<List<UpdateStatusViewModel>> ChangeList(int group, List<MonopolyCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                Monopoly In;

                List<string> MonopolyCodes = new List<string>();

                foreach (var item in request)
                {
                    MonopolyCodes.Add(item.CodeNew);
                }
                var dataCheck = await GetByGroupAndWorkCodes(group, MonopolyCodes);
                List<Monopoly> insertList = new List<Monopoly>();
                foreach (var item in request)
                {
                    #region change
                    In = _mapper.Map<Monopoly>(item);

                    var data = dataCheck
                        .Where(
                                p => p.CodeNew == In.CodeNew
                            )
                        .ToList();
                    if (data.Count == 0)
                    {
                        #region note Add
                        In.Group = group;
                        insertList.Add(In);
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.WorkCode = In.CodeNew;
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
                            objectReturn.WorkCode = In.CodeNew;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.CodeNew;
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
