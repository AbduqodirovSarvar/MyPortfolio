using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;

namespace MyPortfolio.Entity.Configuration
{
    public sealed class ProjectTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.PhotoUrl).IsUnique();
            builder.HasIndex(x => x.UrlToCode).IsUnique();
        }
    }
}
