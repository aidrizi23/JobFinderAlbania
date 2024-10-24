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
    
    // now i will make a method that will allow the user to delete his account after he has confirmed his password
    // the account deletion will be done in 30 days
    // let's first make the get method
    [Authorize]
    [HttpGet]
    public IActionResult DeleteAccount()
    {
        var model = new DeleteAccountViewModel();
        return View(model);
    }
    
    // now let's make the post method
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteAccount(DeleteAccountViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        var password = model.Password;
        var result = await _userManager.CheckPasswordAsync(user, password); // checks if the password is correct
        
        if (result)
        {
            user.LockoutEnd = DateTimeOffset.Now.AddDays(30);
            user.AccountDeletionRequested = true;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            
            TempData["Message"] = "Your account has been marked for deletion. You can recover it by logging in within 30 days.";
            
            return RedirectToAction("Login", "Account");
            // this will redirect the user to the login page after he has requested the deletion of his account
        }
        
        ModelState.AddModelError("Password", "Password is incorrect");
        
        return RedirectToAction( "DeleteAccount");
    }
    
    
}