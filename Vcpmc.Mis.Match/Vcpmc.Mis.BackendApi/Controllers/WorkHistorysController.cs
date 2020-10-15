using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vcpmc.Mis.Application.Mis.WorkHistorys;
using Vcpmc.Mis.ViewModels;
using Vcpmc.Mis.ViewModels.Mis.History;


namespace Vcpmc.Mis.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkHistoryHistorysController : Controller
    {
        private readonly IMapper _mapper;
        private readonly WorkHistoryService _Service;
        public WorkHistoryHistorysController(WorkHistoryService Service, IMapper mapper)
        {
            _Service = Service;
            _mapper = mapper;
        }
        #region GetAll
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetWorkHistoryPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.GetAllPaging(request, _mapper);
            return Ok(list);
        }
        [HttpGet("TotalGetAllPaging")]
        public async Task<IActionResult> TotalGetAllPaging([FromQuery] GetWorkHistoryPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.TotalGetAllPaging(request, _mapper);
            return Ok(list);
        }
        #endregion


        #region Thêm sửa xóa
        [HttpPost("ChangeList")]
        public async Task<IActionResult> ChangeList(WorkHistoryChangeListRequest request)
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
        [HttpPost("MatchingWorkHistory")]
        public async Task<IActionResult> MatchingWorkHistory(WorkHistoryMatchingListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _Service.MatchingWorkHistory(request, _mapper);
            return Ok(list);
        }
        #endregion
    }
}
