namespace JobFinderAlbania.Data;

public class Seller : User
{
    // public string PaymentMethod { get; set; }
    public double Rating { get; set; } = 0;
    public string? Description { get; set; }
    public string? Education { get; set; }
    
}