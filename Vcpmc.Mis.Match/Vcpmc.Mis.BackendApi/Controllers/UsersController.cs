using System;
using System.Threading.Tasks;
using Vcpmc.Mis.Application.System.Users;
using Vcpmc.Mis.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {        
        private readonly IMapper _mapper;
        private readonly UserService2 _userService;
        public UsersController(UserService2 Service, IMapper mapper)
        {
            _userService = Service;
            _mapper = mapper;
        }

        #region get        
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var products = await _userService.GetAllPaging(request, _mapper);
            return Ok(products);
        }
        [HttpGet("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var products = await _userService.GetUserByUsername(username, _mapper);
            return Ok(products);
        }
        [HttpPost("GetById/{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
        #endregion

        #region Chung thuc
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPost]
        [AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody] UserCreateRequest request)
        public async Task<IActionResult> Register(UserCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request, _mapper);
            return Ok(result);
        }

        //PUT: http://localhost/api/users/id
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update([FromBody] UserUpdateRequest request
        [HttpPut()]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(request, _mapper);
            return Ok(result);
        }
        [HttpDelete("{id:length(24)}")]
        //[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.Delete(id, _mapper);
            return Ok(result);
        }
        #endregion

        #region Reset and change password
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _userService.ChangePassword(request);
            return Ok(list);
        }
        [HttpGet("ResetPassword/{id}/{passwordDefault}")]
        public async Task<IActionResult> ResetPassword(string id,string passwordDefault)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _userService.ResetPassword(id,passwordDefault);
            return Ok(list);
        }
        [HttpGet("Unlock/{id}")]
        public async Task<IActionResult> Unlock(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _userService.Unlock(id);
            return Ok(list);
        }
        [HttpGet("Lock/{id}")]
        public async Task<IActionResult> Lock(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _userService.Lock(id);
            return Ok(list);
        }
        #endregion
    }
}