using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.Data.Entities.Mongo;
using Vcpmc.Mis.Data.Entities.System;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.System.Roles;
using System.Linq;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.Utilities.Common;
using MongoDB.Driver.Linq;

namespace Vcpmc.Mis.Application.System.Roles
{
    public class RoleService2:IRoleService2
    {
        private readonly IMongoCollection<AppRole2> _collection;

        private readonly IConfiguration _config;
        public RoleService2(IDatabaseSettings settings, IConfiguration config)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<AppRole2>(settings.AppRolesCollectionName);
            _config = config;
        }

        #region GetData
        public async Task<PagedResult<RoleViewModel>> GetAllPaging(IMapper _mapper)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppRole2>()
                         select e);
            //3. Paging
            int totalRow = 0;
            var list = query.ToList();
            //4.map
            List<RoleViewModel> models = new List<RoleViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    RoleViewModel preVM = _mapper.Map<RoleViewModel>(list[i]);
                    //preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<RoleViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = list.Count==0?5000:list.Count,
                PageIndex = 1,
                Items = models,               
            };
            return pagedResult;
        }
        public async Task<AppRole2> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppRole2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<AppRole2> FindByName(string username)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppRole2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Name == username);
            //3. Paging           
            var item = await query
                .FirstOrDefaultAsync();
            return item;
        }
        #endregion

        #region Update       
        public async Task<UpdateStatusViewModel> Create(RoleCreateRequest request, IMapper _mapper)
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
                AppRole2 In = _mapper.Map<AppRole2>(request);
                var user = await FindByName(In.Name);
                if (user != null)
                {
                    objectReturn.Status = UpdateStatus.Failure;
                    objectReturn.TotalEffect = 1;
                    objectReturn.Message = "Role is exist";
                    return objectReturn;
                } 

                await _collection.InsertOneAsync(In);
                objectReturn.Status = UpdateStatus.Successfull;
                objectReturn.TotalEffect = 1;
                objectReturn.Message = "Create role is successfull";
                return objectReturn;
            }
            catch (Exception ex)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }
        public async Task<UpdateStatusViewModel> Update(RoleUpdateRequest request, IMapper _mapper)
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
                AppRole2 In = _mapper.Map<AppRole2>(request);
                var data = await GetById(In.Id);
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
                        objectReturn.Message = "Delele role is failure";
                        return objectReturn;
                    }
                    else
                    {
                        objectReturn.Message = "Delete role is successfull";
                        return objectReturn;
                    }
                }
                else
                {
                    objectReturn.Message = "Role is not exist";
                    return objectReturn;
                }

            }
            catch (Exception)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }

        #endregion
    }
}
