using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Service;
using static SpaceManagment.Common.CustomAuthorize;

namespace SpaceManagment.Controllers
{
    [MyAuthorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<ApiResult<List<UserDto>>> GetUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.Users();
            return Ok(users);
        }

        [HttpGet("Get")]
        public async Task<ApiResult<UserDto>> Get(long id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetById(id);
            return user;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ApiResult<IdentityResult>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var existUser = await _userService.GetByName(userDto.UserName);
            if (existUser != null)
                throw new BadRequestException("نام کاربری تکراری است");
            var result = await _userService.AddUser(userDto);
            _logger.LogInformation($"کاربر جدید  با نام کاربری {userDto.UserName} ایجاد شد");
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<IdentityResult>> Update(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userService.Get(userDto.Id);
            if (user == null)
                throw new BadRequestException("کاربری با این مشخصات موجود نمیباشد");

            var result = await _userService.UpdateUser(user,userDto);
            _logger.LogInformation($"کاربر  با نام کاربری {userDto.UserName} ویرایش شد");
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult<IdentityResult>> Delete(long id, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteUser(id);
            if (result.Succeeded)
                _logger.LogInformation($"کاربر  با  آیدی {id} حذف شد");
            return Ok(result);
        }

        [HttpPost("AddUserToRole")]
        public async Task<ApiResult> AddUserToRole(string userName, string roleName)
        {
            await _userService.AddUserToRole(userName, roleName);
            return Ok();
        }

        [HttpPost("AddUserToRoles")]
        public async Task<ApiResult> AddUserToRoles(UserRoles userRoles)
        {
            await _userService.AddUserToRoles(userRoles);
            return Ok();
        }
    }
}
