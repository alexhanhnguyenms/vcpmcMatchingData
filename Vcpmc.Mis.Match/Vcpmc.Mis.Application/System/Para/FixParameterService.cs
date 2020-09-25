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
using Vcpmc.Mis.Data.Entities.System.Para;
using Vcpmc.Mis.ViewModels.System.Para;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.System.Para
{
    public class FixParameterService: IFixParameterService
    {
        private readonly IMongoCollection<FixParameter> _collection;
        public FixParameterService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<FixParameter>(settings.FixParametersCollectionName);
        }
        #region get
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetFixParameterPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<FixParameter> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        public async Task<PagedResult<FixParameterViewModel>> GetAllPaging(GetFixParameterPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestFixParameter)
            {
                request.PageSize = LimitRequestBackend.LimitRequestFixParameter;
            }
            IMongoQueryable<FixParameter> query = CreateQueryGetAllPaging(request);
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<FixParameterViewModel> models = new List<FixParameterViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    FixParameterViewModel preVM = _mapper.Map<FixParameterViewModel>(list[i]);
                    //preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<FixParameterViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<FixParameter> CreateQueryGetAllPaging(GetFixParameterPagingRequest request)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter  
            if (!string.IsNullOrEmpty(request.Type))
            {
                query = query.Where(p => p.Type == request.Type);
            }
            if (!string.IsNullOrEmpty(request.Key))
            {
                query = query.Where(p => p.Key == request.Key);
            }
            return query;
        }
        
        public async Task<PagedResult<FixParameterViewModel>> GetByType(string type, IMapper _mapper)
        {
            // 1.Select
            var fix = await GetByType(type);
            //4.map
            List<FixParameterViewModel> models = new List<FixParameterViewModel>();
            int totalRow = 0;
            if (fix != null && fix.Count > 0)
            {
                for (int i = 0; i < fix.Count; i++)
                {
                    FixParameterViewModel preVM = _mapper.Map<FixParameterViewModel>(fix[i]);
                    models.Add(preVM);
                }
                
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<FixParameterViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<FixParameter>> GetByType(string type)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Type == type);
            //3. Paging           
            var preclaim = await query
                .ToListAsync();
            return preclaim;
        }
        public async Task<List<FixParameter>> GetByTypes(List<string> types)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter     
            query = query.Where(p => types.Contains(p.Type));
            //3. Paging           
            var preclaim = await query
                .ToListAsync();
            return preclaim;
        }
        public async Task<List<FixParameter>> GetByKey(string key)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Key == key);
            //3. Paging           
            var preclaim = await query
                .ToListAsync();
            return preclaim;
        }
        public async Task<List<FixParameter>> GetByTypeAndKey(string type,string key)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Type == type && p.Key == key);
            //3. Paging           
            var preclaim = await query
                .ToListAsync();
            return preclaim;
        }
        public async Task<List<FixParameter>> GetById(string id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<FixParameter>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == id);
            //3. Paging           
            var preclaim = await query
                .ToListAsync();
            return preclaim;
        }
        #endregion

        #region Update  
        public async Task<UpdateStatusViewModel> Create(FixParameterCreateRequest request, IMapper _mapper)
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
                FixParameter In = _mapper.Map<FixParameter>(request);
                var data = await GetByTypeAndKey(request.Type, request.Key);
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
        public async Task<UpdateStatusViewModel> Update(FixParameterUpdateRequest request, IMapper _mapper)
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
                FixParameter In = _mapper.Map<FixParameter>(request);
                var data = await GetByTypeAndKey(request.Type, request.Key);
                if (data.Count > 0)
                {
                    var dataCheck = data
                        .Where(
                                p => p.Id != In.Id
                            )
                        .ToList();
                    if (dataCheck.Count > 0)
                    {
                        #region note Add
                        objectReturn.Message = "Update records failure because Existence";
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
        public async Task<List<UpdateStatusViewModel>> ChangeList(List<FixParameterCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();
            try
            {
                FixParameter In;

                List<string> fixListtyppe = new List<string>();

                foreach (var item in request)
                {
                    fixListtyppe.Add(item.Type);
                }
                var dataCheck = await GetByTypes(fixListtyppe);
                List<FixParameter> insertList = new List<FixParameter>();
                foreach (var item in request)
                {
                    #region change
                    In = _mapper.Map<FixParameter>(item);

                    var data = dataCheck
                        .Where(
                                p => p.Type == In.Type
                                &&
                                 p.Key == In.Key
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
                        objectReturn.WorkCode = In.Key;
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
                            objectReturn.WorkCode = In.Key;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.WorkCode = In.Key;
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
