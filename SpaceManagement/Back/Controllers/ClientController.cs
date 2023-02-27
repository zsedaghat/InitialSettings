using Microsoft.AspNetCore.Mvc;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Service;

namespace SpaceManagment.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpPost("Client")]
        public async Task<ApiResult> AddClient(ClientDto clientDto)
        {
            await _clientService.Add(clientDto);
            return Ok();
        }

        [HttpGet("Client")]
        public async Task<ApiResult<List<ClientDto>>> GetClients()
        {
            var list = await _clientService.GetList();
            return Ok(list);
        }

        [HttpGet("Client/{id}")]
        public async Task<ApiResult<ClientDto>> GetClient(long id)
        {
            var client = await _clientService.Get(id);
            if (client == null)
                return BadRequest();
            return Ok(client);
        }

        [HttpPut("Client")]
        public async Task<ApiResult> UpdateClient(ClientDto clientDto)
        {
            await _clientService.Update(clientDto);
            return Ok();
        }

        [HttpDelete("Client")]
        public async Task<ApiResult> DeleteClient(long clientId)
        {
            await _clientService.Delete(clientId);
            return Ok();
        }
    }
}
