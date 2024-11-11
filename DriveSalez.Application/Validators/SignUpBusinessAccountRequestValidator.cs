using DriveSalez.Shared.Dto.Dto;
using DriveSalez.Shared.Dto.Dto.User;
using FluentValidation;

namespace DriveSalez.Application.Validators;

public class SignUpBusinessAccountRequestValidator : AbstractValidator<SignUpBusinessAccountRequest>
{
    public SignUpBusinessAccountRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.ContactNumbers)
            .Must(phoneNumbers => phoneNumbers == null || phoneNumbers.Count > 0)
            .WithMessage("At least one phone number is required if phone numbers are provided.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("At least one phone number is required if phone numbers are provided.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid international format.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");
    }
}