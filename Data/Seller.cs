namespace JobFinderAlbania.Data;

public class Seller : User
{
    public string? Education { get; set; }
    
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    
}