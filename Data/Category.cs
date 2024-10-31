namespace JobFinderAlbania.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual ICollection<Service> Services { get; set; } = new List<Service>(); 
    // made it virtual so that it will not be loaded unless requested. (lazy loading)
    
    
    
}