using System.Security.Claims;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IJwtService
{
    Task<AuthResponse> GenerateSecurityTokenAsync(ApplicationUser identityUser, Guid baseUserId);
    ClaimsPrincipal GetPrincipalFromJwtToken(string token);
}