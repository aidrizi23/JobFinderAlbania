using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models;

public class ProfileViewModel
{
    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [StringLength(500)]
    public string? Bio { get; set; }
    
    public DateTime JoinDate { get; set; }
}