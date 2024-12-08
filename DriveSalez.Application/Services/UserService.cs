using DriveSalez.Application.Abstractions.User.Factory;
using DriveSalez.Application.Abstractions.User.Strategy;
using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Enums;
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
    public async Task<Result<TUser>> AddBaseUserAsync<TUser>(TUser user) where TUser : BaseUser
    {
        var result = await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync();
        return Result<TUser>.Success(result);
    }
    
    public async Task<Result<TUser>> FindBaseUserByIdAsync<TUser>(Guid baseUserId) where TUser : BaseUser
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync<TUser>(baseUserId);
        if (user is null) return Result<TUser>.Failure(UserErrors.NotFound);
        return Result<TUser>.Success(user);
    }

    public async Task<Result<TUser>> UpdateBaseUserAsync<TUser>(TUser baseUser) where TUser : BaseUser
    {
        unitOfWork.UserRepository.Update(baseUser);
        await unitOfWork.SaveChangesAsync();
        return Result<TUser>.Success(baseUser);
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
        var user = await FindBaseUserByIdAsync<User>(pendingUserId);
        if (!user.IsSuccess) return Result<Guid>.Failure(UserErrors.NotFound);
        
        var payment = await unitOfWork.PaymentRepository.GetPaymentByOrderIdAsync(orderId);
        if (payment is null) return Result<Guid>.Failure(PaymentErrors.NotFound);
        
        user.Value!.ActivateUserAfterPayment(payment);
        await UpdateBaseUserAsync(user.Value!);
        
        return Result<Guid>.Success(user.Value!.Id);
    }
}