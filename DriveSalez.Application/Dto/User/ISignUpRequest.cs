namespace DriveSalez.Application.Dto.User;

public interface ISignUpRequest
{
    string Email { get; }
    
    string Password { get; }
    
    string PhoneNumber { get; }
}