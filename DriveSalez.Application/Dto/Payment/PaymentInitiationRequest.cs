namespace DriveSalez.Application.Dto.Payment;

public record PaymentInitiationRequest(int ServiceId, string PurchaseType, Guid UserId);