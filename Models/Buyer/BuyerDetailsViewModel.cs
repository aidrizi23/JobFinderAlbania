namespace JobFinderAlbania.Models.Buyer;

public class BuyerDetailsViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Bio { get; set; }
    public required string ProfilePicture { get; set; }
    public required  DateTime JoinDate { get; set; }

}