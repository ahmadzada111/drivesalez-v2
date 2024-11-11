using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Application.Services;

internal class JwtService(
    IOptions<JwtSettings> jwtSettings,
    IOptions<RefreshTokenSettings> refreshTokenSettings,
    UserManager<ApplicationUser> userManager)
    : IJwtService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly RefreshTokenSettings _refreshTokenSettings = refreshTokenSettings.Value;

    private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser identityUser)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        var role = await userManager.GetRolesAsync(identityUser);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
            new Claim(ClaimTypes.Email, identityUser.Email ?? throw new NullReferenceException("Email is null.")), 
        };
        claims.AddRange(role.Select(roleClaim => new Claim(ClaimTypes.Role, roleClaim)));
        
        var possibleClaims = await userManager.GetClaimsAsync(identityUser);
        await userManager.RemoveClaimsAsync(identityUser, possibleClaims);
        await userManager.AddClaimsAsync(identityUser, claims);
        
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        return token;
    }
    
    public async Task<AuthResponse> GenerateSecurityTokenAsync(ApplicationUser identityUser)
    {
        DateTimeOffset expiration = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.Expiration);
        JwtSecurityToken token = await CreateJwtTokenAsync(identityUser);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string response = tokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTimeOffset.UtcNow.AddMinutes(_refreshTokenSettings.Expiration);

        return new AuthResponse(
            identityUser.BaseUser.Id, 
            response, 
            expiration,
            refreshToken,
            refreshTokenExpiration);
    }

    public ClaimsPrincipal GetPrincipalFromJwtToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is JwtSecurityToken jwtSecurityToken && 
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                StringComparison.InvariantCultureIgnoreCase))
        {
            return principal;
        }

        throw new SecurityTokenException("Invalid JWT token");
    }

    private static string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        
        return Convert.ToBase64String(bytes);
    }
}