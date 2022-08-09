using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class YazarCFG : IEntityTypeConfiguration<Yazar>
    {
        public void Configure(EntityTypeBuilder<Yazar> builder)
        {
            builder.HasData(new Yazar { YazarID=1, YazarAdi="Ahmet Ümit", Biyografi="..." });
            builder.HasData(new Yazar { YazarID=2, YazarAdi="Tolstoy", Biyografi="..." });
            builder.HasData(new Yazar { YazarID=3, YazarAdi="Jack London", Biyografi="..." });
            builder.HasData(new Yazar {YazarID=4, YazarAdi = "İskender Pala" });
        }
    }
}
