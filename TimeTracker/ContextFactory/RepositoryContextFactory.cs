using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace TimeTracker.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext> {
    public RepositoryContext CreateDbContext(string[] args) {
        var connection = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();


        var sSqlConnection = connection.GetConnectionString("sqlConnection");
        var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        if (isOsx) {
            sSqlConnection = connection.GetConnectionString("sqlConnectionMac");
        }

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(connection.GetConnectionString(sSqlConnection!),
                b => b.MigrationsAssembly("TimeTracker"));

        return new RepositoryContext(builder.Options);
    }
}