using DriveSalez.Application.Dto.Payment;
using DriveSalez.Domain.Enums;
using FluentValidation;

namespace DriveSalez.Application.Validators;

public class PaymentInitiationRequestValidator : AbstractValidator<PaymentInitiationRequest>
{
    public PaymentInitiationRequestValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0)
            .WithMessage("ServiceId should be greater than 0.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");

        RuleFor(x => x.PurchaseType)
            .NotEmpty()
            .WithMessage("PurchaseType cannot be empty.")
            .Must(BeAValidPaymentType)
            .WithMessage("Invalid payment type.");
    }

    private bool BeAValidPaymentType(string purchaseType)
    {
        return Enum.TryParse(typeof(PurchaseType), purchaseType, true, out _);
    }
}