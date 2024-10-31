namespace JobFinderAlbania.Models.Service;

public record ServiceForCreationDto
{
    
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; init; }
    public double Price { get; init; } 
    public int DeliveryTime { get; init; }  
    public int Revisions { get; init; }  
    public List<string> Tags { get; init; } = new();  
    public int CategoryId { get; init; }  
    public bool IsActive { get; init; }  
}