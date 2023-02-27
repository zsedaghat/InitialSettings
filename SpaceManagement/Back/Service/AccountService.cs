using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using SpaceManagment.Common;
using SpaceManagment.Controllers;
using SpaceManagment.Model;

namespace SpaceManagment.Service
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        public AccountService(SignInManager<User> signInManager, ILogger<AccountService> logger, UserManager<User> userManager, IJwtService jwtService)           
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<SignInResult> SignIn(User user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);
            return result;
        }

        public async Task Logout(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new BadRequestException("Invalid user name", nameof(userName));
            await _jwtService.DeleteRefteshTokens(user.Id);
            _logger.LogInformation("User logged out.");
        }
    }
}
