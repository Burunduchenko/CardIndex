using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Administration
{
    public class AdministrationDbContext : IdentityDbContext<User>
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new[]
            {
                new IdentityRole() {Name = "user", NormalizedName = "USER"},
                new IdentityRole() {Name = "admin", NormalizedName = "ADMIN"}
            });
        }
    }
}
