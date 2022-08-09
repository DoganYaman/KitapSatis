using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SatisDetay
    {
        public int SatisDetayID { get; set; }
        public int SatisID { get; set; }
        public int KitapID { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }


        public Satis Satis { get; set; }
        public Kitap Kitap { get; set; }
    }
}
