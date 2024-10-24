using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<string>, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    
}