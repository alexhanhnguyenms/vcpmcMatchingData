using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vcpmc.Mis.Application.Mis.Monopolys;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Monopoly;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonopolysController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MonopolyService _Service;
        public MonopolysController(MonopolyService Service, IMapper mapper)
        {
            _Service = Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetMonopolyPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetMonopolyPagingRequest request)
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
        [HttpPost("GetByTitles")]
        public async Task<IActionResult> GetByTitles(MonopolyByStringListRequest titleListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByTitles(titleListRequest.Group,titleListRequest.Items, _mapper);
            return Ok(list);
        }
        [HttpPost("GetByWorkCodes")]
        public async Task<IActionResult> GetByWorkCodes(MonopolyByStringListRequest workCodeListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByGroupAndWorkCodes(workCodeListRequest.Group,workCodeListRequest.Items, _mapper);
            return Ok(list);
        }        
        #endregion

        #region Thêm sửa xóa
        [HttpPost]
        public async Task<IActionResult> Create(MonopolyCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Create(request, _mapper);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(MonopolyUpdateRequest request)
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
        public async Task<IActionResult> ChangeList(MonopolyChangeListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objects = await _Service.ChangeList(request.Group,request.Items, _mapper);
            UpdateStatusViewModelList returnVm = new UpdateStatusViewModelList { Items = objects };
            return Ok(returnVm);
        }
        #endregion        
    }
}
