using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models.AuthModels;

public class SellerRegistrationViewModel
{
    // Common fields from Step 1
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    // Seller-specific fields
    [Display(Name = "Description")]
    [StringLength(500)]
    public string? Bio { get; set; }

    [Display(Name = "Education")]
    public string? Education { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}