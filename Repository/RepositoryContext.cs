using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : DbContext {
    public RepositoryContext(DbContextOptions options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new AccountsConfiguration());
        modelBuilder.ApplyConfiguration(new ClockworkTaskConfiguration());
    }

    public DbSet<Account>? Accounts { get; set; }
}