namespace JobFinderAlbania.Filters;

public class ServiceObjectQuery
{
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    
    public int? MinDeliveryTime { get; set; }
    public int? MaxDeliveryTime { get; set; }
    
    public int? MinRevisions { get; set; }
    public int? MaxRevisions { get; set; }
    
    public List<string>? Tags { get; set; }
    public double? MinRating { get; set; }
    
    public List<int> CategoryIds { get; set; } = new List<int>();
    
    public string? SellerId { get; set; }
    
    
}