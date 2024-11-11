namespace DriveSalez.Utilities.Settings;

public class PayPalSettings(
    string secret, 
    string clientId, 
    string mode)
{
    public string Secret { get; } = secret;

    public string ClientId { get; } = clientId;

    public string Mode { get; } = mode;
}