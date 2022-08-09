using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class RolCFG : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(new Rol {Id=1, Name="Admin" , NormalizedName="ADMIN" });
            builder.HasData(new Rol { Id=2,Name="Uye" , NormalizedName="UYE" });
        }
    }
}
