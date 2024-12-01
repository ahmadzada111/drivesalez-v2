namespace DriveSalez.Shared.Dto.Dto.Payment;

public record PaymentInitiationRequest(int ServiceId, string PurchaseType, Guid UserId);