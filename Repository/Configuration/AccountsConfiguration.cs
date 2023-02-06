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
                Id = 1,
                Name = "Account 1",
                Type = 0,
                Token = "Some token 1"
            },
            new Account
            {
                Id = 2,
                Name = "Account 2",
                Type = 0,
                Token = "Some token 2"
            }
        );
    }
}