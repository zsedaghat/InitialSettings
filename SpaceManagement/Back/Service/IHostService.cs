using SpaceManagment.DTO;

namespace SpaceManagment.Service
{
    public interface IHostService
    {
        Task Add(HostDto hostDto);
        Task<List<HostDto>> GetList();
        Task<HostDto> Get(long id);
        Task Update(HostDto hostDto);
        Task Delete(long hostId);
    }
}