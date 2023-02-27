using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public class HostService : IHostService
    {
        private readonly IRepository<SpaceManagment.Model.Host> _hostRepo;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public HostService(IRepository<SpaceManagment.Model.Host> hostRepo, IUserService userService, UserManager<User> userManager, IMapper mapper)
        {
            _hostRepo = hostRepo;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Add(HostDto hostDto)
        {
            var user = await _userService.GetByName(hostDto.UserName);
            if (user != null)
            {
                throw new BadRequestException("The user exists");
            }
            var PasswordHash = _userService.PasswordHash(hostDto.Password, hostDto.UserName);
            var host = _mapper.Map<SpaceManagment.Model.Host>(hostDto);
            host.PasswordHash= PasswordHash;
            host.IsActive=true;
            var result = await _userManager.CreateAsync(host);
            if (result.Succeeded)
            {
                var res = await _userManager.FindByNameAsync(hostDto.UserName);
                await _userService.AddClaim(res);
            }
        }

        public async Task<List<HostDto>> GetList()
        {
            var hosts = await _hostRepo.TableNoTracking.ToListAsync();
            return _mapper.Map<List<HostDto>>(hosts);
        }

        public async Task<HostDto> Get(long id)
        {
            var host = await _hostRepo.GetByIdAsync(CancellationToken.None, id);
            return _mapper.Map<HostDto>(host);
        }

        public async Task Update(HostDto hostDto)
        {
            var host = await _hostRepo.TableNoTracking.Where(w => w.Id == hostDto.Id).FirstOrDefaultAsync();
            if (host == null)
            {
                throw new NotFoundException("The user is not exist");
            }
            host = _mapper.Map(hostDto, host);
            if (!string.IsNullOrEmpty(hostDto.Password))
            {
                var PasswordHash = _userService.PasswordHash(hostDto.Password, hostDto.UserName);
                host.PasswordHash = PasswordHash;
            }
            var result = await _userManager.UpdateAsync(host);
        }

        public async Task Delete(long hostId)
        {
            var user = await _userManager.FindByIdAsync(hostId.ToString());
            if (user == null)
            {
                throw new NotFoundException("host is not exists");
            }
            else
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
