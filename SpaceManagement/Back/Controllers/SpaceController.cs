using Microsoft.AspNetCore.Mvc;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Service;

namespace SpaceManagment.Controllers
{
    public class SpaceController :  ControllerBase
    {
        private readonly ISpaceService _spaceService;
        public SpaceController(ISpaceService spaceService)
        {
            _spaceService= spaceService;
        }

        [HttpPost("Space")]
        public async Task<ApiResult> Add(SpaceDto spaceDto)
        {
            await _spaceService.Add(spaceDto);
            return Ok();
        }

        [HttpGet("Space")]
        public async Task<ApiResult<List<SpaceDto>>> GetList()
        {
            var list = await _spaceService.GetList();
            return Ok(list);
        }

        [HttpGet("Space/{id}")]
        public async Task<ApiResult<SpaceDto>> Get(int id)
        {
            var space = await _spaceService.Get(id);
            return Ok(space);
        }

        [HttpPut("Space")]
        public async Task<ApiResult> Update(SpaceDto spaceDto)
        {
            await _spaceService.Update(spaceDto);
            return Ok();
        }

        [HttpDelete("Space")]
        public async Task<ApiResult> Delete(int spaceId)
        {
            await _spaceService.Delete(spaceId);
            return Ok();
        }
    }
}
