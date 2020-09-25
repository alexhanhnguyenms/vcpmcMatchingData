using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Media.Youtube;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using MongoDB.Driver.Linq;
using AutoMapper;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.Utilities.Common;
using System.Linq;
using Vcpmc.Mis.Utilities;

namespace Vcpmc.Mis.Application.Media.Youtube
{
    public class PreclaimService : IPreclaimService
    {        
        private readonly IMongoCollection<Preclaim> _preClaim;
        public PreclaimService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _preClaim = database.GetCollection<Preclaim>(settings.PreclaimsCollectionName);
        }

        #region Get data
        public async Task<MasterPageViewModel> TotalGetAllPaging(GetPreclaimPagingRequest request, IMapper _mapper)
        {
            IMongoQueryable<Preclaim> query = CreateQueryGetAllPaging(request);
            int totalRow = await query.CountAsync();
            MasterPageViewModel model = new MasterPageViewModel();
            model.TotalRecordes = totalRow;
            return model;
        }
        /// <summary>
        /// Lấy theo điều kiện lọc
        /// </summary>
        /// <param name="request"></param>
        /// <param name="_mapper"></param>
        /// <returns></returns>
        public async Task<PagedResult<PreclaimViewModel>> GetAllPaging(GetPreclaimPagingRequest request, IMapper _mapper)
        {
            if (request.PageSize > LimitRequestBackend.LimitRequestPreclaim)
            {
                request.PageSize = LimitRequestBackend.LimitRequestPreclaim;
            }
            IMongoQueryable<Preclaim> query = CreateQueryGetAllPaging(request);
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(list[i]);
                    //if(list[i].CREATED_AT != null)
                    //{
                    preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(list[i].CREATED_AT);
                    //}
                    if (list[i].UPDATED_AT != null)
                    {
                        preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)list[i].UPDATED_AT);
                    }
                    preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }

        private IMongoQueryable<Preclaim> CreateQueryGetAllPaging(GetPreclaimPagingRequest request)
        {
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            //2. filter     
            //query = query.Where(p => );
            if (request.Asset_ID != null && request.Asset_ID != string.Empty)
            {
                query = query.Where(p => p.Asset_ID ==(request.Asset_ID));
            }
            if (request.C_Title != null && request.C_Title != string.Empty)
            {
                query = query.Where(p => p.C_Title.Contains(request.C_Title));
            }
            if (request.C_ISWC != null && request.C_ISWC != string.Empty)
            {
                query = query.Where(p => p.C_ISWC.Contains(request.C_ISWC));
            }
            if (request.C_Workcode != null && request.C_Workcode != string.Empty)
            {
                query = query.Where(p => p.C_Workcode.Contains(request.C_Workcode));
            }
            if (request.C_Writers != null && request.C_Writers != string.Empty)
            {
                query = query.Where(p => p.C_Writers.Contains(request.C_Writers));
            }
            //if (request.MONTH != -1)
            //{
            //    query = query.Where(p => p.MONTH == request.MONTH);
            //}
            //query = query.Where(p => p.Year == request.Year);
            return query;
        }

        //public async Task<PagedResult<PreclaimViewModel>> GetByAssetId(string assetId, IMapper _mapper)
        //{
        //    //1. Select join
        //    var query = (from e in _preClaim.AsQueryable<Preclaim>()
        //                 select e);
        //    //2. filter     
        //    query = query.Where(p =>p.Asset_ID == assetId);
        //    //3. Paging
        //    int totalRow = await query.CountAsync();
        //    var list = await query               
        //        .ToListAsync();
        //    //4.map
        //    List<PreclaimViewModel> models = new List<PreclaimViewModel>();
        //    if (list != null && list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(list[i]);
        //            //if(list[i].CREATED_AT != null)
        //            //{
        //            preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(list[i].CREATED_AT);
        //            //}
        //            if (list[i].UPDATED_AT != null)
        //            {
        //                preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)list[i].UPDATED_AT);
        //            }
        //            preVM.SerialNo = (i + 1);
        //            models.Add(preVM);
        //        }
        //    }
        //    //4. Select and projection               
        //    var pagedResult = new PagedResult<PreclaimViewModel>()
        //    {
        //        TotalRecords = totalRow,
        //        PageSize = totalRow,
        //        PageIndex = 1,
        //        Items = models
        //    };
        //    return pagedResult;   
        //}
        /// <summary>
        /// Lấy theo asset_id, month and year
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="_mapper"></param>
        /// <returns></returns>
        //public async Task<PagedResult<PreclaimViewModel>> GetByAssetIdAndMothAndYear(string assetId,int month,int year, IMapper _mapper)
        //{
        //    //1. Select join   
        //    var list = await GetByAssetIdAndMothAndYear(assetId, month, year);
        //    int totalRow = list.Count;
        //    //4.map
        //    List<PreclaimViewModel> models = new List<PreclaimViewModel>();
        //    if (list != null && list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(list[i]);
        //            //if(list[i].CREATED_AT != null)
        //            //{
        //            preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(list[i].CREATED_AT);
        //            //}
        //            if (list[i].UPDATED_AT != null)
        //            {
        //                preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)list[i].UPDATED_AT);
        //            }
        //            preVM.SerialNo = (i + 1);
        //            models.Add(preVM);
        //        }
        //    }
        //    //4. Select and projection               
        //    var pagedResult = new PagedResult<PreclaimViewModel>()
        //    {
        //        TotalRecords = totalRow,
        //        PageSize = totalRow,
        //        PageIndex = 1,
        //        Items = models
        //    };
        //    return pagedResult;
        //}
        public async Task<PagedResult<PreclaimViewModel>> GetByAssetId(string assetId, IMapper _mapper)
        {
            //1. Select join   
            var list = await GetByAssetId(assetId);
            int totalRow = list.Count;
            //4.map
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(list[i]);
                    //if(list[i].CREATED_AT != null)
                    //{
                    preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(list[i].CREATED_AT);
                    //}
                    if (list[i].UPDATED_AT != null)
                    {
                        preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)list[i].UPDATED_AT);
                    }
                    preVM.SerialNo = (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        //public async Task<List<Preclaim>> GetByAssetIdAndMothAndYear(string assetId, int month, int year)
        //{
        //    //1. Select join
        //    var query = (from e in _preClaim.AsQueryable<Preclaim>()
        //                 select e);
        //    //2. filter     
        //    query = query.Where(p => p.Asset_ID == assetId);
        //    //query = query.Where(p => p.MONTH == month);
        //    //query = query.Where(p => p.Year == year);
        //    //3. Paging
        //    int totalRow = await query.CountAsync();
        //    var list = await query
        //        .ToListAsync();
        //    //4.map            
        //    return list;
        //}
        public async Task<List<Preclaim>> GetByAssetId(string assetId)
        {
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Asset_ID == assetId);            
            //3. Paging
            int totalRow = await query.CountAsync();
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<PreclaimViewModel>> GetById(string Id, IMapper _mapper)
        {
            //1. Select            
            var preclaim = await GetById(Id);
            //4.map
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            int totalRow = 0;
            if (preclaim!=null)
            {
                PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(preclaim);                
                preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(preclaim.CREATED_AT);                
                if (preclaim.UPDATED_AT != null)
                {
                    preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)preclaim.UPDATED_AT);
                }
                preVM.SerialNo = 1;
                models.Add(preVM);
            }
            //5. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;          
        }
        public async Task<Preclaim> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();            
            return preclaim;
        }
        public async Task<PagedResult<PreclaimViewModel>> GetByAsset_ids(List<string> asset_ids, IMapper _mapper)
        {
            //1.get
            var list = await GetByAsset_ids(asset_ids);
            //2.map
            int totalRow = list.Count;
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<PreclaimViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Preclaim>> GetByAsset_ids(List<string> asset_ids)
        {            
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            //2. filter     
            query = query.Where(p => asset_ids.Contains(p.Asset_ID));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        public async Task<PagedResult<PreclaimViewModel>> GetByWorkCodes(List<string> workCodes, IMapper _mapper)
        {
            //1.get
            var list = await GetByWorkCodes(workCodes);
            //2.map
            int totalRow = list.Count;
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    models.Add(_mapper.Map<PreclaimViewModel>(list[i]));
                }
            }
            //3. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        public async Task<List<Preclaim>> GetByWorkCodes(List<string> workCodes)
        {
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            //2. filter     
            query = query.Where(p => workCodes.Contains(p.C_Workcode));
            //3. Paging
            var list = await query
                .ToListAsync();
            //4.map            
            return list;
        }
        #endregion

        #region Update        
        public async Task<UpdateStatusViewModel> Create(PreclaimCreateRequest request, IMapper _mapper)
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
                Preclaim preclaimIn = _mapper.Map<Preclaim>(request);
                if (request.DtCREATED_AT != null)
                {
                    preclaimIn.CREATED_AT = TimeTicks.ConvertCToTicksMongo(request.DtCREATED_AT);
                }
                if (request.DtUPDATED_AT != null)
                {
                    preclaimIn.UPDATED_AT = TimeTicks.ConvertCToTicksMongo((DateTime)request.DtUPDATED_AT);
                }
                //var data = await GetByAssetIdAndMothAndYear(preclaimIn.Asset_ID, preclaimIn.MONTH, preclaimIn.Year);
                var data = await GetByAssetId(preclaimIn.Asset_ID);
                if (data != null && data.Count == 0)
                {
                    await _preClaim.InsertOneAsync(preclaimIn);
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
        public async Task<UpdateStatusViewModel> Update(PreclaimUpdateRequest request, IMapper _mapper)
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
                Preclaim preclaimIn = _mapper.Map<Preclaim>(request);
                if (request.DtCREATED_AT != null)
                {
                    preclaimIn.CREATED_AT = TimeTicks.ConvertCToTicksMongo(request.DtCREATED_AT);
                }
                if (request.DtUPDATED_AT != null)
                {
                    preclaimIn.UPDATED_AT = TimeTicks.ConvertCToTicksMongo((DateTime)request.DtUPDATED_AT);
                }
                var data = await GetById(preclaimIn.Id);
                if(data!=null)
                {
                    var result = await _preClaim.ReplaceOneAsync(p => p.Id == preclaimIn.Id, preclaimIn);
                    if(result.MatchedCount > 0)
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
        public async Task<UpdateStatusViewModel> Remove(string id,IMapper _mapper)
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
                    var result = await _preClaim.DeleteOneAsync(p => p.Id == id);
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
        public async Task<List<UpdateStatusViewModel>> ChangeList(List<PreclaimCreateRequest> request, IMapper _mapper)
        {
            List<UpdateStatusViewModel> listReturn = new List<UpdateStatusViewModel>();            
            try
            {
                Preclaim preclaimIn;

                List<string> asset_ids = new List<string>();

                foreach (var item in request)
                {
                    if (!asset_ids.Contains(item.Asset_ID))
                    {
                        asset_ids.Add(item.Asset_ID);
                    }
                }
                var dataCheck = await GetByAsset_ids(asset_ids);
                List<Preclaim> insertList = new List<Preclaim>();
                foreach (var item in request)
                {
                    #region change
                    preclaimIn = _mapper.Map<Preclaim>(item);
                    if (item.DtCREATED_AT != null)
                    {
                        preclaimIn.CREATED_AT = TimeTicks.ConvertCToTicksMongo(item.DtCREATED_AT);
                    }
                    if (item.DtUPDATED_AT != null)
                    {
                        preclaimIn.UPDATED_AT = TimeTicks.ConvertCToTicksMongo((DateTime)item.DtUPDATED_AT);
                    }
                  
                    var data = dataCheck
                        .Where(
                                p => p.Asset_ID == preclaimIn.Asset_ID
                                //&& p.MONTH == preclaimIn.MONTH
                                //&& p.Year == preclaimIn.Year
                            )
                        .ToList();
                    if (data != null && data.Count == 0)
                    {
                        #region note Add
                        insertList.Add(preclaimIn);
                        //await _preClaim.InsertOneAsync(preclaimIn);
                        UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Adding a record is successfull";
                        objectReturn.Asset_id = preclaimIn.Asset_ID;
                        //objectReturn.Month = preclaimIn.MONTH;
                        //objectReturn.Year = preclaimIn.Year;
                        objectReturn.Command = CommandType.Add;
                        listReturn.Add(objectReturn);
                        #endregion
                    }
                    //update
                    else
                    {
                        #region Update                       
                        preclaimIn.CREATED_AT = data[0].CREATED_AT;
                        preclaimIn.UPDATED_AT = TimeTicks.ConvertCToTicksMongo(DateTime.Now);
                        preclaimIn.Id = data[0].Id;
                        var result = await _preClaim.ReplaceOneAsync
                                (p =>
                                    p.Id == preclaimIn.Id,
                                    //p.Asset_ID == preclaimIn.Asset_ID
                                    //&& p.MONTH == preclaimIn.MONTH
                                    //&& p.Year == preclaimIn.Year,
                                    preclaimIn
                                );
                        //Note update
                        if (result.MatchedCount > 0)
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Successfull;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.Asset_id = preclaimIn.Asset_ID;
                            //objectReturn.Month = preclaimIn.MONTH;
                            //objectReturn.Year = preclaimIn.Year;
                            objectReturn.Command = CommandType.Update;
                            listReturn.Add(objectReturn);
                        }
                        else
                        {
                            UpdateStatusViewModel objectReturn = new UpdateStatusViewModel();
                            objectReturn.Status = UpdateStatus.Failure;
                            objectReturn.TotalEffect = result.ModifiedCount;
                            objectReturn.Message = "Update records successfully";
                            objectReturn.Asset_id = preclaimIn.Asset_ID;
                            //objectReturn.Month = preclaimIn.MONTH;
                            //objectReturn.Year = preclaimIn.Year;
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
                    await _preClaim.InsertManyAsync(insertList);
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
        private IMongoQueryable<Preclaim> GetByAssetIdS(List<string> asset_idList)
        {
            //1. Select join
            var query = (from e in _preClaim.AsQueryable<Preclaim>()
                         select e);
            query = query.Where(p => asset_idList.Contains(p.Asset_ID));
            return query;
        }

        public async Task<PagedResult<PreclaimViewModel>> MatchingPreclaim(PreclaimMatchingListRequest request, IMapper _mapper)
        {
            List<string> asset_idList = new List<string>();
            if (request != null && request.Items != null)
            {
                foreach (var item in request.Items)
                {
                    if(!asset_idList.Contains(item.AssetId))
                    {
                        asset_idList.Add(item.AssetId);
                    }                    
                }
            }
            ////1. Select join
            //var query = (from e in _preClaim.AsQueryable<Preclaim>()
            //             select e);
            ////2. filter     
            ////query = query.Where(p => );
            //if (request != null && request.Items != null)
            //{
            //    query = query.Where(p => asset_idList.Contains(p.Asset_ID));
            //}    
            //if(request.Year !=-1)
            //{
            //    query = query.Where(p => p.Year == request.Year);
            //}
            //if (request.MONTH != -1)
            //{
            //    query = query.Where(p => p.MONTH == request.MONTH);
            //}
            //3. Paging
            List<Preclaim> list;
            int totalRow = 0;
            if (asset_idList.Count > 0)
            {
                IMongoQueryable<Preclaim> query = GetByAssetIdS(asset_idList);
               
                list = await query
                    .ToListAsync();
            }
            else
            {
                list = new List<Preclaim>();
            }
            //4.map
            List<PreclaimViewModel> models = new List<PreclaimViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    PreclaimViewModel preVM = _mapper.Map<PreclaimViewModel>(list[i]);                    
                    preVM.SerialNo = (i+1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<PreclaimViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = models
            };
            return pagedResult;
        }
        #endregion
    }
}
