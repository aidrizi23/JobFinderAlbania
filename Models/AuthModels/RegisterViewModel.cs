using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models.AuthModels;

public class RegisterStep1ViewModel
{
    
    [Required]
    [Display(Name = "First Name")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
    
  
    
    [DataType(DataType.Upload)]
    [Display(Name = "Profile Picture")]
    public IFormFile? ProfilePicture { get; set; } // Optional for all users

    [Required]
    [Display(Name = "User Type")]
    public string UserType { get; set; } // Admin, Buyer, Seller
}