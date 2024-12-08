namespace DriveSalez.Domain.Aggregates.PaymentAggregate;

public class PaidService
{
    public int Id { get; set; }
    
    public string Name { get; set; } 
    
    public decimal Price { get; set; }
    
    public PaidService(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}