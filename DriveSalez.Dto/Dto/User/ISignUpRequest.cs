namespace DriveSalez.Shared.Dto.Dto.User;

public interface ISignUpRequest
{
    string Email { get; }
    
    string Password { get; }
    
    string PhoneNumber { get; }
}