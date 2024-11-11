namespace DriveSalez.Utilities.Settings;

public class RefreshTokenSettings(int expiration)
{
    public int Expiration { get; } = expiration;
}