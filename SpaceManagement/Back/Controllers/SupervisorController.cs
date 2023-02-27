using Microsoft.AspNetCore.Mvc;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Service;

namespace SpaceManagment.Controllers
{
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        public SupervisorController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }

        [HttpPost("Supervisor")]
        public async Task<ApiResult> Add(SupervisorDto supervisorDto)
        {
            await _supervisorService.Add(supervisorDto);
            return Ok();
        }

        [HttpGet("Supervisor")]
        public async Task<ApiResult<List<SupervisorDto>>> GetList()
        {
            var list = await _supervisorService.GetList();
            return Ok(list);
        }

        [HttpGet("Supervisor/{id}")]
        public async Task<ApiResult<SupervisorDto>> Get(long id)
        {
            var supervisor = await _supervisorService.Get(id);
            return Ok(supervisor);
        }

        [HttpPut("Supervisor")]
        public async Task<ApiResult> Update(SupervisorDto supervisorDto)
        {
            await _supervisorService.Update(supervisorDto);
            return Ok();
        }

        [HttpDelete("Supervisor")]
        public async Task<ApiResult> Delete(long supervisorId)
        {
            await _supervisorService.Delete(supervisorId);
            return Ok();
        }
    }
}
