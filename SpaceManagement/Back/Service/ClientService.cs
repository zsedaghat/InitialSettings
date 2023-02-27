using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public class ClientService : IClientService
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Client> _clientRepo;
        private readonly IMapper _mapper;

        public ClientService(IUserService userService, UserManager<User> userManager, IRepository<Client> clientRepo, IMapper mapper
         )
        {
            _userService = userService;
            _userManager = userManager;
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task Add(ClientDto clientDto)
        {
            var user = await _userService.GetByName(clientDto.UserName);
            if (user != null)
            {
                throw new BadRequestException("The user exists");
            }
            var PasswordHash = _userService.PasswordHash(clientDto.Password, clientDto.UserName);
            var client = _mapper.Map<SpaceManagment.Model.Client>(clientDto);
            client.PasswordHash = PasswordHash;
            client.IsActive = true;
            var result = await _userManager.CreateAsync(client);
            if (result.Succeeded)
            {
                var res = await _userService.GetByUserName(clientDto.UserName);
                await _userService.AddClaim(res);
            }
        }

        public async Task<List<ClientDto>> GetList()
        {
            
                var clients = await _clientRepo.TableNoTracking.ToListAsync();
                var result = _mapper.Map<List<ClientDto>>(clients);
                return result;
        }

        public async Task<ClientDto> Get(long id)
        {
            var client = await _clientRepo.GetByIdAsync(CancellationToken.None, id);
            if (client==null)
            {
                throw new NotFoundException("The client does not exist");
            }
            return _mapper.Map<ClientDto>(client);
        }

        public async Task Update(ClientDto clientDto)
        {
            var client = await _clientRepo.GetByIdAsync(CancellationToken.None, clientDto.Id);
            if (client == null)
            {
                throw new NotFoundException("The user is not exist");
            }
            client = _mapper.Map(clientDto, client);
            if (!string.IsNullOrEmpty(clientDto.Password))
            {
                var PasswordHash = _userService.PasswordHash(clientDto.Password, clientDto.UserName);
                client.PasswordHash = PasswordHash;
            }
            var result = await _userManager.UpdateAsync(client);
        }

        public async Task Delete(long clientId)
        {
            var user = await _userManager.FindByIdAsync(clientId.ToString());
            if (user == null)
            {
                throw new NotFoundException("client is not exists");
            }
            else
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
