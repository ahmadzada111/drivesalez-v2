namespace DriveSalez.Utilities.Utilities;

public static class PaymentErrors
{
    public static Error NotFound => new Error("Payment Not Found", "The requested payment does not exist.");
    public static Error InvalidPurchaseType => new Error("Invalid Purchase Type", "Payment does not match the expected purchase type.");
}