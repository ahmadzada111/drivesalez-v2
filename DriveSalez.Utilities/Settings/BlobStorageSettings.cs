namespace DriveSalez.Utilities.Settings;

public class BlobStorageSettings
{
    public required string ConnectionString { get; set; } 
    
    public required string ContainerName { get; set; }
}