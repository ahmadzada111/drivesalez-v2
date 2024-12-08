namespace DriveSalez.Application.Dto.Payment;

public record PaymentDetails(
    string ReturnUrl,
    string CancelUrl
);