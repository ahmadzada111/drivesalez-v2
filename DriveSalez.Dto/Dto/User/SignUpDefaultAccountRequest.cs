namespace DriveSalez.Shared.Dto.Dto.User;

public record SignUpDefaultAccountRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber) : ISignUpRequest;