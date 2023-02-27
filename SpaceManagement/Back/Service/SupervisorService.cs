using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public class SupervisorService : ISupervisorService
    {
        private readonly IRepository<SpaceManagment.Model.Supervisor> _supervisorRepo;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public SupervisorService(IRepository<SpaceManagment.Model.Supervisor> supervisorRepo, IUserService userService, UserManager<User> userManager, IMapper mapper)
        {
            _supervisorRepo = supervisorRepo;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task Add(SupervisorDto supervisorDto)
        {
            var user = await _userService.GetByName(supervisorDto.UserName);
            if (user != null)
            {
                throw new BadRequestException("The user exists");
            }
            var PasswordHash = _userService.PasswordHash(supervisorDto.Password, supervisorDto.UserName);
            var supervisor = _mapper.Map<Supervisor>(supervisorDto);
            supervisor.PasswordHash = PasswordHash;
            supervisor.IsActive = true;
            var result = await _userManager.CreateAsync(supervisor);
            if (result.Succeeded)
            {
                var res = await _userManager.FindByNameAsync(supervisorDto.UserName);
                await _userService.AddClaim(res);
            }
        }

        public async Task<List<SupervisorDto>> GetList()
        {
            var supervisors = await _supervisorRepo.TableNoTracking.ToListAsync();
            return _mapper.Map<List<SupervisorDto>>(supervisors);
        }

        public async Task<SupervisorDto> Get(long id)
        {
            var supervisor = await _supervisorRepo.GetByIdAsync(CancellationToken.None, id);
            return _mapper.Map<SupervisorDto>(supervisor);
        }

        public async Task Update(SupervisorDto supervisorDto)
        {
            var supervisor = await _supervisorRepo.GetByIdAsync(CancellationToken.None, supervisorDto.Id);
            if (supervisor == null)
            {
                throw new NotFoundException("The user is not exist");
            }
            _mapper.Map(supervisorDto, supervisor);
            if (!string.IsNullOrEmpty(supervisorDto.Password))
            {
                var PasswordHash = _userService.PasswordHash(supervisorDto.Password, supervisorDto.UserName);
                supervisor.PasswordHash = PasswordHash;
            }
            var result = await _userManager.UpdateAsync(supervisor);
        }

        public async Task Delete(long supervisorId)
        {
            var user = await _userManager.FindByIdAsync(supervisorId.ToString());
            if (user == null)
            {
                throw new NotFoundException("supervisor is not exists");
            }
            else
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
