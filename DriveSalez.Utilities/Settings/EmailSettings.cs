namespace DriveSalez.Utilities.Settings;

public class EmailSettings
{
    public required string CompanyEmail { get; set; }
    
    public required string SenderName { get; set; }
    
    public required string EmailKey { get; set; }
    
    public required string SmtpServer { get; set; } 
    
    public required int Port { get; set; } 
}