using System.ComponentModel.DataAnnotations;

namespace JobFinderAlbania.Models;

public class DeleteAccountViewModel
{
    [ Microsoft.Build.Framework.Required]
    [ DataType(DataType.Password)]
    [ Display(Name = "Password")]
    public string Password { get; set; }
}