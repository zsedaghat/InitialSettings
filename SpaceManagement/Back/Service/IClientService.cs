using SpaceManagment.DTO;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public interface IClientService
    {
        Task Add(ClientDto clientDto);
        Task<List<ClientDto>> GetList();
        Task<ClientDto> Get(long id);
        Task Update(ClientDto client);
        Task Delete(long clientId);
    }
}