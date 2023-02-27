using Microsoft.AspNetCore.Identity;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Model;
using System.Security.Claims;

namespace SpaceManagment.Service
{
    public interface IUserService
    {
        Task<IdentityResult> AddUser(UserDto userDto);
        Task<IdentityResult> UpdateUser(User user, UserDto userDto);
        Task<IdentityResult> DeleteUser(long id);
        Task<List<UserDto>> Users();
        Task<UserDto> GetById(long id);
        Task<UserDto> GetByName(string Name);
        Task<bool> CheckPasswordValid(User user, string password);
        Task AddUserToRole(string userName, string roleName);
        Task AddUserToRoles(UserRoles userRoles);
        Task<List<CreateCliam>> GetClaimList();
        Task AddAdminClaims();
        string PasswordHash(string password, string userName);
        Task<List<Claim>> AddClaim(User user);
        Task<User> Get(long id);
        Task<User> GetByUserName(string Name);
    }
}
