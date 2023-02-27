using SpaceManagment.DTO;

namespace SpaceManagment.Service
{
    public interface ISpaceService
    {
        Task Add(SpaceDto spaceDto);
        Task Delete(int spaceId);
        Task<SpaceDto> Get(int id);
        Task<List<SpaceDto>> GetList();
        Task Update(SpaceDto spaceDto);
    }
}