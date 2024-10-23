using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models.AuthModels;

public class BuyerRegistrationViewModel
{
    // Common fields from Step 1
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    // Buyer-specific fields
    [Display(Name = "Company Name")]
    public string? CompanyName { get; set; }

    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}