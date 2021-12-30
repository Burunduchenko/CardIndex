using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration
{
    public class AdministrationDbContext : IdentityDbContext<UserApp>
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
                new IdentityRole("user"),
                new IdentityRole("admin")
            }) ;
        }
    }
}
