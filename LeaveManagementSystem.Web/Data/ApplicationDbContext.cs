using LeaveManagementSystem.Web.Data.Configurations;
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

            builder.ApplyConfiguration(new IdentityRoleConfiguration());

            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());

            builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<Period> Periods { get; set; }

        public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

    }
}
