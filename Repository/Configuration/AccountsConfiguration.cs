using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class AccountsConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasData(
            new Account
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                UserName = "admin",
                Type = 0,
                ClockworkAccountId = "unknown",
                Password = "adminadmin"
            }
        );
    }
}