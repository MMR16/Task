using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Name).IsRequired();
            //seed data
            builder.HasData(
                new Category { Id = 1, Name = "TV" },
                new Category { Id = 2, Name = "Screen" },
                new Category { Id = 3, Name = "Laptop" }
                );
            
        }
    }
}
