using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ClockworkTaskConfiguration : IEntityTypeConfiguration<ClockworkTask> {
    public void Configure(EntityTypeBuilder<ClockworkTask> builder) {
        builder.HasData(
            new ClockworkTask {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                ClockworkTaskId = 0,
                ClockworkTaskKey = "unknown",
                AccountId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            });
    }
}