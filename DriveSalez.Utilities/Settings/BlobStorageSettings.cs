namespace DriveSalez.Utilities.Settings;

public class BlobStorageSettings(string connectionString, string containerName)
{
    public string ConnectionString { get; } = connectionString;
    
    public string ContainerName { get; } = containerName;
}