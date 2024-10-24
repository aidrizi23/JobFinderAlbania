using Microsoft.AspNetCore.Identity;

namespace JobFinderAlbania.Data;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.Now;
}