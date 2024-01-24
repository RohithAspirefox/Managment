using Management.Common.Models;
using Management.Common.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Management.Data.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TechStack> TechStack { get; set; }
        public DbSet<ProjectTechStack> ProjectTechStack { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            User user = new User()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                FirstName = "Admin",
                NormalizedUserName = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin@123")
            };
            builder.Entity<User>().HasData(user);

            User hrUser = new User()
            {
                Id = "554a8f54-c054-4de6-9654-654321098765",
                FirstName = "HR",
                NormalizedUserName = "HR",
                UserName = "HR",
                Email = "hr@gmail.com",
                NormalizedEmail = "hr@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "9876543210",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "HR@123")
            };
            builder.Entity<User>().HasData(hrUser);

            User publicUser = new User()
            {
                Id = "774a8f54-c054-4de6-9654-654321098755",
                FirstName = "User",
                NormalizedUserName = "User",
                UserName = "User",
                Email = "user@gmail.com",
                NormalizedEmail = "user@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "987452361",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "user@123")
            };
            builder.Entity<User>().HasData(publicUser);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "HR", ConcurrencyStamp = "2", NormalizedName = "Human Resource" },
                new IdentityRole() { Id = "123013f0-5201-4317-abd8-c211f91b7123", Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                ,
                new IdentityUserRole<string>() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "554a8f54-c054-4de6-9654-654321098765" },
                new IdentityUserRole<string>() { RoleId = "123013f0-5201-4317-abd8-c211f91b7123", UserId = "774a8f54-c054-4de6-9654-654321098755" }

                );
        }
    }
}