namespace DriveSalez.Utilities.Settings;

public class EmailSettings(
    string companyEmail, 
    string senderName,
    string emailKey,
    string smtpServer,
    int port)
{
    public string CompanyEmail { get; } = companyEmail;
    
    public string SenderName { get; } = senderName;
    
    public string EmailKey { get; } = emailKey;
    
    public string SmtpServer { get; } = smtpServer;
    
    public int Port { get; } = port;
}