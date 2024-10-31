namespace JobFinderAlbania.Models.Service;

public class ServiceForEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    public int DeliveryTime { get; set; }
    public int Revisions { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public int CategoryId { get; set; }
    public bool IsActive { get; set; }
}
