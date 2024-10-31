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
    
    public DbSet<Category?> Categories { get; set; }
    
    public DbSet<Service> Services { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Service>()
            .HasOne(s => s.Category)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.CategoryId);
        
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Graphic Design" },
            new Category { Id = 2, Name = "Web Development" },
            new Category { Id = 3, Name = "Content Writing" },
            new Category { Id = 4, Name = "Digital Marketing" },
            new Category { Id = 5, Name = "Video Editing" }
        );
    }
    
}