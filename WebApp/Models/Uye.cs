using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Uye:IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }

        public ICollection<Satis> Satislar { get; set; }
        public ICollection<Sepet> Sepetler { get; set; }
    }
}
