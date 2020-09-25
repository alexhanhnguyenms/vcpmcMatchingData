using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vcpmc.Mis.Application.Mis.Works;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.Works;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WorkService _Service;
        public WorksController(WorkService Service, IMapper mapper)
        {
            _Service = Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWorkPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetWorkPagingRequest request)
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
        public async Task<IActionResult> GetByTitles(WorkByStringListRequest titleListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByTitles(titleListRequest.Items, _mapper);
            return Ok(list);
        }
        [HttpPost("GetByWorkCodes")]
        public async Task<IActionResult> GetByWorkCodes(WorkByStringListRequest workCodeListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByWorkCodes(workCodeListRequest.Items, _mapper);
            return Ok(list);
        }
        //[HttpPost("GetByWorkCodeAndTitle")]
        //public async Task<IActionResult> GetByWorkCodeAndTitle(WorkThreeStringListRequest workCodeAndTitles)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var list = await _Service.GetByWorkCodeAndTitle(workCodeAndTitles, _mapper);
        //    return Ok(list);
        //}
        //[HttpPost("GetByWorkTitleAndWriter")]
        //public async Task<IActionResult> GetByWorkTitleAndWriter(WorkThreeStringListRequest TitleAndWriters)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var list = await _Service.GetByWorkTitleAndWriter(TitleAndWriters, _mapper);
        //    return Ok(list);
        //}
        #endregion

        #region Thêm sửa xóa
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Service.Create(request, _mapper);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(WorkUpdateRequest request)
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
        public async Task<IActionResult> ChangeList(WorkChangeListRequest request)
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

        #region Sync
        [HttpPost("SyncFromTrackingWorkToWork")]
        public async Task<IActionResult> SyncFromTrackingWorkToWork(GetWorkTrackingPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objects = await _Service.SyncFromTrackingWorkToWork(request, _mapper);
            UpdateStatusViewModelList returnVm = new UpdateStatusViewModelList { Items = objects };
            return Ok(returnVm);
        }
        #endregion

        #region Matching
        [HttpPost("MatchingWork")]
        public async Task<IActionResult> MatchingWork(WorkMatchingListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.MatchingWork(request, _mapper);
            return Ok(list);
        }
        #endregion
    }

}
