using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Configuration
{
    public sealed class ExperienceTypeConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
