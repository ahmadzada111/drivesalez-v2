namespace DriveSalez.Application.Abstractions.User.Strategy;

public class UserStrategySelector(IServiceProvider serviceProvider)
{
    public IUserStrategy<TSignUpRequest> GetStrategy<TSignUpRequest>()
    {
        var strategy = serviceProvider.GetService(typeof(IUserStrategy<TSignUpRequest>)) as IUserStrategy<TSignUpRequest>;
        if (strategy == null)
        {
            throw new NotImplementedException($"No factory implemented for sign-up request type {nameof(TSignUpRequest)}");
        }
        return strategy;
    }
}