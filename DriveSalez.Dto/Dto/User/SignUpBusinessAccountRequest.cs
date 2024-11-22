namespace DriveSalez.Shared.Dto.Dto.User;

public record SignUpBusinessAccountRequest( 
    string PhoneNumber,
    string BusinessName,
    string Address,
    string Description,
    string Email, 
    string Password,
    string ConfirmPassword,
    List<string> ContactNumbers,
    List<WorkHourRequest> WorkHours) : ISignUpRequest;