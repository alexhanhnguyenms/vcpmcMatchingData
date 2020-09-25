using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vcpmc.Mis.Application.Mis.Works.Tracking;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkTrackingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WorkTrackingService _Service;
        public WorkTrackingsController(WorkTrackingService Service, IMapper mapper)
        {
            _Service = Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWorkTrackingPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetWorkTrackingPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.TotalGetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("GetArreggateMasterList")]
        public async Task<IActionResult> GetArreggateMasterList([FromQuery] GetWorkTrackingPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetArreggateMasterList(request);
            return Ok(list);
        }
        #endregion

        #region Thêm sửa xóa       
        [HttpPost("ChangeList")]
        public async Task<IActionResult> ChangeList(WorkTrackingChangeListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objects = await _Service.ChangeList(request.Year, request.Month, request.Type, request.Items, _mapper);
            UpdateStatusViewModelList returnVm = new UpdateStatusViewModelList { Items = objects };
            return Ok(returnVm);
        }
        #endregion        
    }
}
