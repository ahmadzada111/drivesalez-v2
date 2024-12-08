namespace DriveSalez.Domain.Aggregates.UserAggregate;

public record ContactPhoneNumber(string Number)
{
    public string Number { get; private set; } = Number;
}