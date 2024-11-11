namespace DriveSalez.Domain.Entities;

public class Image
{
    public int Id { get; set; }

    public required Uri Url { get; set; }
}