using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class YayinEvi
    {
        public int YayinEviID { get; set; }
        public string YayinEviAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }

        public ICollection<Kitap> Kitaplar { get; set; }
    }
}
