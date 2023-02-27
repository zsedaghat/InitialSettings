using Microsoft.AspNetCore.Mvc;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Service;

namespace SpaceManagment.Controllers
{
    public class HostController : ControllerBase
    {
        private readonly IHostService _hostService;
        public HostController(IHostService hostService)
        {
            _hostService = hostService;
        }

        [HttpPost("Host")]
        public async Task<ApiResult> Add(HostDto hostDto)
        {
            await _hostService.Add(hostDto);
            return Ok();
        }

        [HttpGet("Host")]
        public async Task<ApiResult<List<HostDto>>> GetList()
        {
            var list = await _hostService.GetList();
            return Ok(list);
        }

        [HttpGet("Host/{id}")]
        public async Task<ApiResult<HostDto>> Get(long id)
        {
            var host = await _hostService.Get(id);
            return Ok(host);
        }

        [HttpPut("Host")]
        public async Task<ApiResult> Update(HostDto hostDto)
        {
            await _hostService.Update(hostDto);
            return Ok();
        }

        [HttpDelete("Host")]
        public async Task<ApiResult> Delete(long hostId)
        {
            await _hostService.Delete(hostId);
            return Ok();
        }
    }
}
