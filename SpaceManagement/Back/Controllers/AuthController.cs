using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Model;
using SpaceManagment.Service;
using System.Net.Http.Headers;

namespace SpaceManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthController(IJwtService jwtService, IUserService userService, IAccountService accountService, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _userService = userService;
            _accountService = accountService;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<ApiResult<TokenResponsDto>> Login(string username, string password)
        {
            var user = await _userService.GetByUserName(username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await _userService.CheckPasswordValid(user, password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var signInresult = await _accountService.SignIn(user, password);
            if (signInresult.Succeeded)
            {
                var refreshToken = await _jwtService.GenerateRefreshToken(user);
                var resultResult = await _jwtService.CreateToken(user);
                var respons = new TokenResponsDto
                {
                    AccessToken = resultResult.AccessToken,
                    TokenExpire = resultResult.TokenExpire
                };
                Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true });
                Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true });
                return Ok(respons);
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("LogOut")]
        public async Task<ApiResult> LogOut(string userName)
        {
            await _accountService.Logout(userName);
            return Ok();
        }
     
        [HttpGet("RefreshToken")]
        public async Task<ApiResult<TokenResponsDto>> RefreshToken(/*[FromHeader] string authorization*/)
        {
            if (!(Request.Cookies.TryGetValue("X-Username", out var userName) && Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                return BadRequest();
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var scheme = headerValue.Scheme;
                var accessToken = headerValue.Parameter;
                if (accessToken is null)
                    return BadRequest("Invalid client request");

                var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Claims.Where(e => e.Type == "UserName").FirstOrDefault()?.Value;
                //var username = principal.Identity.Name; //this is mapped to the Name claim by default
                if (username == null)
                {
                    return BadRequest("Invalid client request");
                }
                var user = await _userService.GetByUserName(username);

                var existingRefreshToken = _jwtService.GetRefreshToken(refreshToken, user.Id);
                if (existingRefreshToken != null)
                {
                    if (user is null || existingRefreshToken.RefreshTokenExpires <= DateTime.Now)
                        return BadRequest("Invalid client request");

                    if (existingRefreshToken.IsUsed)
                    {
                        var lockUserTask = _userManager.SetLockoutEnabledAsync(user, true);
                        lockUserTask.Wait();
                        return BadRequest("Invalid client request");
                    }
                    else
                    {
                        var newRefreshToken = await _jwtService.GenerateRefreshToken(user);
                        await _jwtService.ChangeIsUsedRefreshToken(refreshToken);

                        Response.Cookies.Append("X-Refresh-Token", newRefreshToken, new CookieOptions() { HttpOnly = true });
                        var newAccessToken = await _jwtService.CreateToken(user);
                        return Ok(new TokenResponsDto
                        {
                            AccessToken = newAccessToken.AccessToken,
                            TokenExpire = newAccessToken.TokenExpire
                        });
                    }
                }
                else
                {
                    return BadRequest("Invalid client request");
                }
            }

            return Ok();  
        }

        [Authorize]
        [HttpGet("AddRole")]
        public async Task<ApiResult<IdentityResult>> AddRole(string name)
        {
            var result = await _roleManager.CreateAsync(new Role
            {
                Name = name,
            });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetRoles")]
        public async Task<ApiResult<List<Role>>> GetRoles()
        {
            var roles =await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("ClaimList")]
        public async Task<ApiResult<List<CreateCliam>>> ClaimList()
        {
            var list = await _userService.GetClaimList();
            return Ok(list);
        }

        [AllowAnonymous]
        [HttpGet("AddAdminClaims")]
        public async Task<ApiResult> AddAdminClaims()
        {
            await _userService.AddAdminClaims();
            return Ok();
        }
    }
}
