using AutoMapper;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Model;
using System.Reflection;
using System.Security.Claims;


namespace SpaceManagment.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.IsActive = true;
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"{result.Errors}");
            }
            await AddClaim(user);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(User user, UserDto userDto)
        {           
            user = _mapper.Map(userDto, user);
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                var PassHash = PasswordHash(userDto.Password, userDto.UserName);
                user.PasswordHash = PassHash;
            }
            var result = await _userManager.UpdateAsync(user);

            var oldClaims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, oldClaims);
            var newClaims = await AddClaim(user);
            return result;
        }

        public async Task<IdentityResult> DeleteUser(long id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if (user == null)
             throw  new NotFoundException("کاربری با این مشخصات موجود نمیباشد");
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<List<UserDto>> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            var results = _mapper.Map<List<UserDto>>(users);
            return results;
        }

        public async Task<UserDto> GetById(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<UserDto> GetByName(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<User> GetByUserName(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);
            return user;
        }

        public async Task<User> Get(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user==null)
            {
                throw new NotFoundException("user is not exist");
            }
            return user;
        }

        public async Task<bool> CheckPasswordValid(User user, string password)
        {
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            return isPasswordValid;
        }

        public async Task<List<Claim>> AddClaim(User user)
        {
            if (user == null)
            {
                throw new NotFoundException("The user is not exist");
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(type: "UserName", user.UserName));
            claims.Add(new Claim(type: "UserId", user.Id.ToString()));
            await _userManager.AddClaimsAsync(user, claims);
            return claims;
        }

        public async Task AddUserToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.RoleExistsAsync(roleName);
            if (!role)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task AddUserToRoles(UserRoles userRoles)
        {
            var user = await _userManager.FindByNameAsync(userRoles.user.UserName);
            if (user?.UserName != null)
            {
                foreach (var role in userRoles.RoleNames)
                {
                    var existrole = await _roleManager.RoleExistsAsync(role);
                    if (existrole)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }
            throw new NotFoundException("The user is not exist");
        }

        public async Task<List<CreateCliam>> GetClaimList()
        {
            var asm = Assembly.GetAssembly(typeof(SpaceManagment.Controllers.AuthController));
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                    })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var list = new List<CreateCliam>();
            foreach (var action in controlleractionlist)
            {
                list.Add(new CreateCliam
                {
                    ClaimType = "Permission",
                    ClaimValue = $"{action.Controller}.{action.Action}"
                }
                    );
            }
            return list;
        }

        public async Task AddAdminClaims()
        {
            var claimList = await GetClaimList();
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            var oldClaims = await _roleManager.GetClaimsAsync(adminRole);
            foreach (var item in oldClaims)
            {
                await _roleManager.RemoveClaimAsync(adminRole, item);

            }

            foreach (var item in claimList)
            {
                var claim = new Claim(item.ClaimType, item.ClaimValue);
                await _roleManager.AddClaimAsync(adminRole, claim);
            }
        }

        public string PasswordHash(string password, string userName)
        {
            if (password == null)
            {
                throw new BadRequestException(nameof(password));
            }
            var user = new User
            {
                UserName = userName,
            };

            var PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            return PasswordHash;
        }
    }
}
