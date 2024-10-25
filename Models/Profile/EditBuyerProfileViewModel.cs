using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models;

public class EditBuyerProfileViewModel : ProfileViewModel
{
    [StringLength(100)]
    public string? CompanyName { get; set; }
    
    [StringLength(100)]
    public string? PaymentMethod { get; set; }
    
}