using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public class SpaceService : ISpaceService
    {
        private readonly IRepository<Space> _spaceRepo;
        private readonly IMapper _mapper;

        public SpaceService(IRepository<Space> spaceRepo, IMapper mapper)
        {
            _spaceRepo = spaceRepo;
            _mapper = mapper;
        }

        public async Task Add(SpaceDto spaceDto)
        {
            var space = await _spaceRepo.TableNoTracking.Where(s => s.Name == spaceDto.Name).FirstOrDefaultAsync();
            if (space == null)
            {
                var model = _mapper.Map<Space>(spaceDto);
                await _spaceRepo.AddAsync(model);
            }
            else
            {
                throw new BadRequestException("This name is available at this host .");
            }
        }

        public async Task<List<SpaceDto>> GetList()
        {
            var spaces = await _spaceRepo.TableNoTracking.ToListAsync();
            return _mapper.Map<List<SpaceDto>>(spaces);
        }

        public async Task<SpaceDto> Get(int id)
        {
            var space = await _spaceRepo.GetByIdAsync(CancellationToken.None, id);
            return _mapper.Map<SpaceDto>(space);
        }

        public async Task Update(SpaceDto spaceDto)
        {
            var space = await _spaceRepo.GetByIdAsync(CancellationToken.None, spaceDto.Id);
            if (space == null)
            {
                throw new NotFoundException("space is not exist");
            }
            var model = _mapper.Map<Space>(spaceDto);
            await _spaceRepo.UpdateAsync(model);
        }

        public async Task Delete(int spaceId)
        {
            var space = _spaceRepo.Table.Where(d => d.Id == spaceId).FirstOrDefault();
            if (space != null)
                await _spaceRepo.DeleteAsync(space);
            else
                throw new NotFoundException("space is not exists");
        }
    }
}
