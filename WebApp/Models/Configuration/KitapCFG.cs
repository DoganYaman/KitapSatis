using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class KitapCFG : IEntityTypeConfiguration<Kitap>
    {
        public void Configure(EntityTypeBuilder<Kitap> builder)
        {
            builder.HasData(new Kitap { 
             KitapID=1,
             KitapAdi="Savaş ve Barış",
             YazarID=2,
             YayinEviID=1,
             TavsiyeSayisi=0,
             KapakResmi="savasvebaris.jpg",
             Fiyat=45,
             Ozet=" .... bla bla ...",
             StokAdedi=10,
             BasimTarihi=DateTime.Parse("12/12/2010")
            });
        }
    }
}
