using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class KitapKategoriCFG : IEntityTypeConfiguration<KitapKategori>
    {
        public void Configure(EntityTypeBuilder<KitapKategori> builder)
        {
            builder.HasData(new KitapKategori
            {
                KitapKategoriID=1,
                KitapID = 1
             ,
                KategoriID = 2
            });
            builder.HasData(new KitapKategori
            {
                KitapKategoriID=2,
                KitapID = 1
            ,
                KategoriID = 4
            });
        }
    }
}
