namespace JobFinderAlbania.Data;

public class Buyer : User
{
    public string? CompanyName { get; set; }
    public string PaymentMethod { get; set; }
    public double? Rating { get; set; }
}   