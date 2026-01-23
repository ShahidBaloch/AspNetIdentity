using AspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename Tables
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.Parse("c8d89a25-4b96-4f20-9d79-7f8a54c5213d"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Administrator role with full permissions.",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("b92f0a3e-573b-4b12-8db1-2ccf6d58a34a"),
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Standard user role.",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationRole
                {
                    // Fixed the length of the Manager GUID string here
                    Id = Guid.Parse("d7f44a42-1c1b-4c9f-8a50-55f6b234e8e2"),
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    Description = "Manager role with moderate permissions.",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("f2e6b8a1-9d43-4a7c-9f32-71d7c5dbe9f0"),
                    Name = "Guest",
                    NormalizedName = "GUEST",
                    Description = "Guest role with limited access.",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedAt = new DateTime(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                   warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public DbSet<Address> Addresses { get; set; }
    }
}