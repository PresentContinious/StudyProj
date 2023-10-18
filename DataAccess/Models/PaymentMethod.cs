namespace DataAccess.Models;

public class PaymentMethod
{
    public int Id { get; set; }

    public string CardNumber { get; set; } = null!;
    public string ExpiryDate { get; set; } = null!;
    public string Cvv { get; set; } = null!;
    public string? Email { get; set; }
}
