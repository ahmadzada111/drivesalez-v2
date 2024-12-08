namespace DriveSalez.Application.Dto.User;

public record SignUpDefaultAccountRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber) : ISignUpRequest;