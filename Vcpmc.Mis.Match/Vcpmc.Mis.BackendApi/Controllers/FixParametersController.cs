using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vcpmc.Mis.Application.System.Para;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.System.Para;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FixParametersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly FixParameterService _Service;
        public FixParametersController(FixParameterService Service, IMapper mapper)
        {
            _Service = Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetFixParameterPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetFixParameterPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.TotalGetAllPaging(request, _mapper);
            return Ok(list);
        }
        #endregion

        #region find     
        
        
        #endregion

        #region Thêm sửa xóa
        [HttpPost]
        public async Task<IActionResult> Create(FixParameterCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Create(request, _mapper);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(FixParameterUpdateRequest request)
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
        [HttpPost("ChangeList")]
        public async Task<IActionResult> ChangeList(FixParameterChangeListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objects = await _Service.ChangeList(request.Items, _mapper);
            UpdateStatusViewModelList returnVm = new UpdateStatusViewModelList { Items = objects };
            return Ok(returnVm);
        }
        #endregion        
    }
}
