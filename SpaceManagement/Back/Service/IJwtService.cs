using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Model;
using System.Security.Claims;

namespace SpaceManagment.Service
{
    public interface IJwtService
    {
        Task<TokenResponsDto> CreateToken(User user);
        Task<IEnumerable<Claim>> GetClaimsAsync(User user);
        Task<string> GenerateRefreshToken(User user/*, bool isLogin*/);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        RefreshToken GetRefreshToken(string refreshToken, long userId);
        Task DeleteRefteshTokens(long userId);
        Task ChangeIsUsedRefreshToken(string token);
    }
}
