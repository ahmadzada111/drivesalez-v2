namespace DriveSalez.Application.Dto.Payment;

public record GetPaymentResponse(
    Guid Id,
    Guid UserId,
    string OrderId,
    decimal Amount,
    string Name,
    string PurchaseType,
    string PaymentStatus,
    int ServiceId,
    DateTimeOffset CreationDate)
{
    public static explicit operator GetPaymentResponse(Domain.Entities.Payment payment)
    {
        return new GetPaymentResponse(
            payment.Id,
            payment.UserId,
            payment.OrderId,
            payment.Amount,
            payment.Name,
            payment.PurchaseType.ToString(),
            payment.PaymentStatus.ToString(),
            payment.PaidServiceId,
            payment.CreationDate);
    }
}