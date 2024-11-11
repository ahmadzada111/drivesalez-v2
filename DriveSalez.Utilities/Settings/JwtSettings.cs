namespace DriveSalez.Utilities.Settings;

public class JwtSettings(
    string secret, 
    string issuer, 
    string audience, 
    int expiration)
{
    public string Secret { get; } = secret;
    
    public string Issuer { get; } = issuer;
    
    public string Audience { get; } = audience;
    
    public int Expiration { get; } = expiration;
}