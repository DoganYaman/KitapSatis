using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class YayinEviCFG : IEntityTypeConfiguration<YayinEvi>
    {
        public void Configure(EntityTypeBuilder<YayinEvi> builder)
        {
            builder.HasData(new YayinEvi { YayinEviID=1,  YayinEviAdi="Abc Yayincilik", Adres="Beyazıt"  });
            builder.HasData(new YayinEvi { YayinEviID=2, YayinEviAdi="Dbc Yayincilik", Adres="Kadıköy"  });
        }
    }
}
