using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    internal class RolesConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RolesConstants.Admin,
                    NormalizedName = RolesConstants.Admin.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = RolesConstants.User,
                        NormalizedName = RolesConstants.User.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
        }
    }
}
