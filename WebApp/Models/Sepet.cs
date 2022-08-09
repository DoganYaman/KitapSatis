using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Sepet
    {
        public int SepetID { get; set; }
        public int UyeID { get; set; }
        public int KitapID { get; set; }
        public int Adet { get; set; }

        public Uye Uye { get; set; }
        public Kitap Kitap { get; set; }

    }
}
