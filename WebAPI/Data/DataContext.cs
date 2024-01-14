using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("tb_Users");
            builder.Entity<IdentityRole>().ToTable("tb_Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("tb_UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("tb_UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("tb_UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("tb_RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("tb_UserTokens");

        }
    }
}
