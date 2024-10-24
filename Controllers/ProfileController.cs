using JobFinderAlbania.Data;
using JobFinderAlbania.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class ProfileController : Controller
{
    // GET
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // now i will create the index method that will return us the data that we decide to be returned in the profile page.
    // to make this easier to use i have decided to create a view model that will hold the data that we want to return.
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        
        // now i will check if the user is a buyer or a seller
        if (user is Buyer buyer)
        {
            var model = new BuyerProfileViewModel()
            {
                FirstName = buyer.FirstName,
                LastName = buyer.LastName,
                Email = buyer.Email!, // the ! means that there is no way that the email will be null
                Bio = buyer.Bio,
                JoinDate = buyer.JoinDate,
                CompanyName = buyer.CompanyName?? "Company Name not set",
                PaymentMethod = buyer.PaymentMethod
            };
            
            return View("BuyerProfile", model); 
        }
        else if (user is Seller seller)
        {
            var model = new SellerProfileViewModel()
            {
                FirstName = seller.FirstName,
                LastName = seller.LastName,
                Email = seller.Email!, // the ! means that there is no way that the email will be null
                Bio = seller.Bio,
                JoinDate = seller.JoinDate,
                Education = seller.Education ?? "Education not set",
            };
            
            return View("SellerProfile", model);
        }
        
        // if the user is not a buyer or a seller (this can not happen) than we will return a 404 page
        return NotFound();
    }
    
    
    
}