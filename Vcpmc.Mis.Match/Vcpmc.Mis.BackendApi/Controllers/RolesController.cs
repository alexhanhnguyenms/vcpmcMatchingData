using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vcpmc.Mis.Application.System.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Vcpmc.Mis.ViewModels.System.Roles;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        //private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly RoleService2 _Service;
        private readonly AppClaimService _appClaimService;
        public RolesController(RoleService2 Service, AppClaimService appClaimService, IMapper mapper)
        {
            //_roleService = roleService;
            _Service = Service;
            _appClaimService = appClaimService;
            _mapper = mapper;
        }
        #region GetData
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging()
        {
            var roles = await _Service.GetAllPaging(_mapper);
            return Ok(roles);
        }
        [HttpGet("GetAllPagingAppClaim")]
        public async Task<IActionResult> GetAllPagingAppClaim()
        {
            var roles = await _appClaimService.GetAllPaging(_mapper);
            return Ok(roles);
        }
        #endregion

        #region Thêm sửa xóa
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Create(request, _mapper);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(RoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Update(request, _mapper);
            return Ok(result);
        }

        //[HttpDelete("{preclaimId}")]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Remove(id, _mapper);
            return Ok(result);
        }

        #endregion
    }
}