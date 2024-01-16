using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Models;

namespace Beat2Book.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        
        }
        public DbSet<Event> Races { get; set; } 
        public DbSet<Band> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; } 

    }

    
}
