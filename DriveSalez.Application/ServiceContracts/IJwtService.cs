using System.Security.Claims;
using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.ServiceContracts;

public interface IJwtService
{
    Task<AuthResponse> GenerateSecurityTokenAsync(ApplicationUser identityUser, Guid customUserId);
    ClaimsPrincipal GetPrincipalFromJwtToken(string token);
}