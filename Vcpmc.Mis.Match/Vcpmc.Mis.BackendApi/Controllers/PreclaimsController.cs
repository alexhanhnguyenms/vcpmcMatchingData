using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Vcpmc.Mis.Application.Media.Youtube;
using Vcpmc.Mis.Data.Entities.Media.Youtube;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;

namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PreclaimsController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly PreclaimService _Service;
        public PreclaimsController(PreclaimService preclaimService, IMapper mapper)       
        {           
            _Service = preclaimService;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPreclaimPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);           
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetPreclaimPagingRequest request)
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
        [HttpPost("GetByAsset_ids")]
        public async Task<IActionResult> GetByAsset_ids(PeclaimByStringListRequest assetIdListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByAsset_ids(assetIdListRequest.Items, _mapper);           
            return Ok(list);
        }
        [HttpPost("GetByWorkCodes")]
        public async Task<IActionResult> GetByWorkCodes(PeclaimByStringListRequest workCodeListRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetByWorkCodes(workCodeListRequest.Items, _mapper);
            return Ok(list);
        }
        #endregion

        #region Thêm sửa xóa
        [HttpPost]
        public async Task<IActionResult> Create(PreclaimCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            var result = await _Service.Create(request,_mapper);                
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(PreclaimUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            var result = await _Service.Update(request,_mapper);            
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
        public async Task<IActionResult> ChangeList(PeclaimChangeListRequest request)
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

        #region Matching
        [HttpPost("MatchingPreclaim")]
        public async Task<IActionResult> MatchingPreclaim(PreclaimMatchingListRequest request)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.MatchingPreclaim(request, _mapper);
            return Ok(list);
        }
        #endregion
    }
}
