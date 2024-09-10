using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>//dodela role adminu. ApplicationUserRole je tabela vise na vise koja povezuje usera i role
            {
                RoleId = "938bfcf3-9072-4fbb-9c0c-87a69d935a2e",
                UserId = "7a709332-5d98-43e2-8dfc-414711b163a8"

            });
        }
    }
}
