using DriveSalez.Application.Abstractions.User.Factory;
using DriveSalez.Application.Abstractions.User.Strategy;
using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class UserService(
    UserFactorySelector userFactorySelector,
    UserStrategySelector userStrategySelector,
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    IRoleService roleService) : IUserService
{
    public async Task<Result<CustomUser>> AddCustomUserAsync(CustomUser customUser)
    {
        var result = await unitOfWork.UserRepository.AddAsync(customUser);
        await unitOfWork.SaveChangesAsync();
        return Result<CustomUser>.Success(result);
    }
    
    public async Task<Result<CustomUser>> FindCustomUserByIdAsync(Guid customUserId)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(customUserId);
        if (user is null) return Result<CustomUser>.Failure(UserErrors.NotFound);
        return Result<CustomUser>.Success(user);
    }

    public async Task<Result<CustomUser>> UpdateCustomUserAsync(CustomUser customCustomUser)
    {
        unitOfWork.UserRepository.Update(customCustomUser);
        await unitOfWork.SaveChangesAsync();
        return Result<CustomUser>.Success(customCustomUser);
    }
    
    public async Task<Result<Guid>> CreateUserAsync<TSignUpRequest>(TSignUpRequest request, UserType userType) where TSignUpRequest : ISignUpRequest
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var identityUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await identityService.CreateUserAsync(identityUser, request.Password);
            if (!result.IsSuccess)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Result<Guid>.Failure(UserErrors.UserCreationFailed(result.Error!.Message));
            }
            
            await roleService.AddUserToRole(identityUser, userType.ToString());
            await identityService.UpdateUserAsync(identityUser);
            var userFactory = userFactorySelector.GetFactory<TSignUpRequest>();
            var user = userFactory.CreateUserObject(request, identityUser.Id);
            var userStrategy = userStrategySelector.GetStrategy<TSignUpRequest>();
            var createdUser = await userStrategy.CreateUser(user);
            await unitOfWork.CommitTransactionAsync();
            return Result<Guid>.Success(createdUser.Id);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    
    public async Task<Result<Guid>> CompleteBusinessSignUpAsync(Guid pendingUserId, string orderId)
    {
        var user = await FindCustomUserByIdAsync(pendingUserId);
        if (!user.IsSuccess) return Result<Guid>.Failure(UserErrors.NotFound);
        
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<Guid>.Failure(PaymentErrors.NotFound);
        
        user.Value!.ActivateUserAfterPayment(payment);
        await UpdateCustomUserAsync(user.Value!);
        
        return Result<Guid>.Success(user.Value!.Id);
    }
}