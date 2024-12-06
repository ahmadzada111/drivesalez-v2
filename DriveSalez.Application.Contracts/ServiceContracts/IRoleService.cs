using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IRoleService
{
    Task<Result<IdentityResult>> AddUserToRole(ApplicationUser user, string userRole);
}