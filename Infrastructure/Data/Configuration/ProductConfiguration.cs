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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.MinQuantity).IsRequired();
            builder.Property(e => e.ImagePath).IsRequired();
            builder.Property(e => e.CategoryId).IsRequired(false);
            builder.HasOne(e => e.Category).WithMany(e=>e.products).HasForeignKey(e=>e.CategoryId);

          
        }
    }
}
