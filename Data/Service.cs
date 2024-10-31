namespace JobFinderAlbania.Data;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    
    public string UserId { get; set; }
    public virtual Seller User { get; set; }
    
    
    public int DeliveryTime { get; set; } 
    public int Revisions { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public double Rating { get; set; }
    
}    