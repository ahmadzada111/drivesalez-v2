namespace DriveSalez.Domain.Aggregates.UserAggregate;

public record Image(Uri Url)
{
    public Uri Url { get; private set; } = Url;
}