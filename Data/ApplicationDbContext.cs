using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nikola_WebSite.Models;

namespace Nikola_WebSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Nikola_WebSite.Models.Price> Price { get; set; } = default!;
        public DbSet<Nikola_WebSite.Models.Broker> Broker { get; set; } = default!;
        public DbSet<Nikola_WebSite.Models.text_class> text_class { get; set; } = default!;
    }
}