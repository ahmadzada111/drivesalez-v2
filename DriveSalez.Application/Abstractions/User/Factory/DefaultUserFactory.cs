using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions.User;

public class DefaultUserFactory(IUnitOfWork unitOfWork) : IUserFactory<SignUpDefaultAccountRequest>
{
    public async Task<Domain.IdentityEntities.User> CreateUser(SignUpDefaultAccountRequest signUpRequest)
    {
        var subscription = await unitOfWork.SubscriptionRepository.GetByUserTypeAsync(UserType.Business);
        if (subscription is null) throw new KeyNotFoundException("Subscription not found");
        
        var user = new Domain.IdentityEntities.User
        {
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            CreationDate = DateTimeOffset.UtcNow,
            UserStatus = UserStatus.Active,
            Subscription = subscription
        };

        // var premiumLimit = new UserLimit()
        // {
        //     LimitType = LimitType.Regular,
        //     LimitValue = subscription.RegularUploadLimit,
        //     UsedValue = 0,
        //     User = user
        // };
        //
        // var regularLimit = new UserLimit()
        // {
        //     LimitType = LimitType.Premium,
        //     LimitValue = subscription.PremiumUploadLimit,
        //     UsedValue = 0,
        //     User = user
        // };
        //
        // await unitOfWork.BeginTransactionAsync();
        // try
        // {
        //     await unitOfWork.UserRepository.AddAsync(user);
        //     await unitOfWork.UserLimitRepository.AddAsync(premiumLimit);
        //     await unitOfWork.UserLimitRepository.AddAsync(regularLimit);
        //     await unitOfWork.CommitTransactionAsync();
        // }
        // catch (Exception)
        // {
        //     await unitOfWork.RollbackTransactionAsync();
        //     throw;
        // }
        
        return user;
    }
}