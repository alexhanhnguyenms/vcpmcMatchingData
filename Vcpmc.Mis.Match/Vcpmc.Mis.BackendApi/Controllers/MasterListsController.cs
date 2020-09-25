using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vcpmc.Mis.Application.MasterLists;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.MasterLists;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MasterListService _Service;
        public MasterListsController(MasterListService Service, IMapper mapper)
        {
            _Service =Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetMasterListPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetMasterListPagingRequest request)
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
        //[HttpPost("GetByAsset_ids")]
        //public async Task<IActionResult> GetByAsset_ids(PeclaimByStringListRequest assetIdListRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var list = await _Service.GetByAsset_ids(assetIdListRequest.Items, _mapper);
        //    return Ok(list);
        //}
        //[HttpPost("GetByWorkCodes")]
        //public async Task<IActionResult> GetByWorkCodes(PeclaimByStringListRequest workCodeListRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var list = await _Service.GetByWorkCodes(workCodeListRequest.Items, _mapper);
        //    return Ok(list);
        //}
        #endregion

        #region Thêm sửa xóa
        //[HttpPost]
        //public async Task<IActionResult> Create(MasterListCreateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _Service.Create(request, _mapper);
        //    return Ok(result);
        //}

        //[HttpPut()]
        //public async Task<IActionResult> Update(MasterListUpdateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _Service.Update(request, _mapper);
        //    return Ok(result);
        //}

        ////[HttpDelete("{preclaimId}")]
        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _Service.Remove(id, _mapper);
        //    return Ok(result);
        //}
        [HttpPost("ChangeList")]
        public async Task<IActionResult> ChangeList(MasterListChangeListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objects = await _Service.ChangeList(request.Year,request.Month,request.Items, _mapper);
            UpdateStatusViewModelList returnVm = new UpdateStatusViewModelList { Items = objects };
            return Ok(returnVm);
        }
        #endregion
        
    }
}
