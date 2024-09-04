using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)//data seeding. punimo IdentityRole tabelu sa nekim default vrednostima
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                {
                    Id= "eba21fe0-287b-41f3-90f6-994dfb8bedd3",//guid.Dobijeno pomocu guid generatora 
                    Name="Employee",
                    NormalizedName="EMPLOYEE"
                },
                new IdentityRole 
                {
                    Id = "e9536cf8-0cb2-4fa0-9037-eb3797924347",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole {

                    Id = "938bfcf3-9072-4fbb-9c0c-87a69d935a2e",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

            var hasher=new PasswordHasher<ApplicationUser>();//da nam kreira hash na osnovu sifre koju zadamo

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser//kreirali smo admin usera
            {
                Id = "7a709332-5d98-43e2-8dfc-414711b163a8",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1950, 12, 01)
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>//dodela role adminu. ApplicationUserRole je tabela vise na vise koja povezuje usera i role
            {
                RoleId = "938bfcf3-9072-4fbb-9c0c-87a69d935a2e",
                UserId = "7a709332-5d98-43e2-8dfc-414711b163a8"

            });
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<Period> Periods { get; set; }


    }
}
