using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Configuration
{
    public class UyeCFG : IEntityTypeConfiguration<Uye>
    {
        public void Configure(EntityTypeBuilder<Uye> builder)
        {
            Uye uye = new Uye { Id = 1,Ad="Cevdet", Soyad="Korkmaz",  UserName = "Cevdo", NormalizedUserName = "CEVDO", Email = "cevdo@deneme.com", NormalizedEmail = "CEVDO@DENEME.COM", EmailConfirmed = false, Adres = "Kadıkoy" };

            PasswordHasher<Uye> hash = new PasswordHasher<Uye>();
            uye.PasswordHash= hash.HashPassword(uye, "123");
            
            //UserManager<Uye> uyeMan = new UserManager<Uye>()();
           

            //uyeMan.AddToRoleAsync(uye, "Admin");
            builder.HasData(uye);

        }
    }
}
