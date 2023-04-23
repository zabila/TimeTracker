﻿using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User> {
    public RepositoryContext(DbContextOptions options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountsConfiguration());
        modelBuilder.ApplyConfiguration(new ClockworkTaskConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Account>? Accounts { get; set; }
}