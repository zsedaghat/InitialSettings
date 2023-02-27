using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpaceManagment.Common;
using SpaceManagment.DTO;
using SpaceManagment.Infrastructure;
using SpaceManagment.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SpaceManagment.Service
{
    public class JwtService : IJwtService
    {
        private readonly Settings _Setting;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<RefreshToken> _refreshTokenRepo;

        public JwtService(IOptionsSnapshot<Settings> settings, SignInManager<User> signInManager, UserManager<User> userManager, IRepository<RefreshToken> refreshTokenRepo)
        {
            _Setting = settings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<TokenResponsDto> CreateToken(User user)
        {
            var claims = await GetClaimsAsync(user);
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Setting.JwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_Setting.JwtSettings.ExpirationMinutes),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponsDto
            {
                AccessToken = jwt,
                TokenExpire = _Setting.JwtSettings.ExpirationMinutes
            };
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            //var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            var result = await _userManager.GetClaimsAsync(user);
            var list = new List<Claim>(result);
            return list;
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshTokenResponse
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            await AddRefreshToken(refreshToken, user);
            return refreshToken.Token;
        }

        public async Task AddRefreshToken(RefreshTokenResponse refreshTokenResponse, User user)
        {
            var refreshToken = new RefreshToken
            {
                RefreshTokenCreated = DateTime.Now,
                RefreshTokenExpires = refreshTokenResponse.Expires,
                Token = refreshTokenResponse.Token,
                UserId = user.Id,
                IsUsed = false
            };
            await _refreshTokenRepo.AddAsync(refreshToken);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Setting.JwtSettings.Key)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512"/*SecurityAlgorithms.Sha512, StringComparison.InvariantCultureIgnoreCase*/))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public RefreshToken GetRefreshToken(string refreshToken, long userId)
        {
            var result = _refreshTokenRepo.TableNoTracking.Where(w => w.Token == refreshToken && w.UserId == userId).FirstOrDefault();
            return result;
        }

        public async Task DeleteRefteshTokens(long userId)
        {
            var refreshTokens = _refreshTokenRepo.TableNoTracking.Where(e => e.UserId == userId);
            if (refreshTokens != null)
            {
                await _refreshTokenRepo.DeleteRangeAsync(refreshTokens);
            }
        }

        public async Task ChangeIsUsedRefreshToken(string token)
        {
            var Reftoken = _refreshTokenRepo.TableNoTracking.Where(s => s.Token == token).FirstOrDefault();
            if (Reftoken != null)
            {
                Reftoken.IsUsed = true;
                await _refreshTokenRepo.UpdateAsync(Reftoken);
            }
        }
    }
}
