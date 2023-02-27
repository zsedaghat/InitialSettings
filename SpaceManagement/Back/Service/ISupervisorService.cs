using SpaceManagment.DTO;

namespace SpaceManagment.Service
{
    public interface ISupervisorService
    {
        Task Add(SupervisorDto supervisorDto);
        Task Delete(long supervisorId);
        Task<SupervisorDto> Get(long id);
        Task<List<SupervisorDto>> GetList();
        Task Update(SupervisorDto supervisorDto);
    }
}