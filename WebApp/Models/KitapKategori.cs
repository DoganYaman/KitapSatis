using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class KitapKategori
    {
        public int KitapKategoriID { get; set; }
        public int KitapID { get; set; }
        public int KategoriID { get; set; }

        public Kitap Kitap  { get; set; }
        public Kategori Kategori { get; set; }
    }
}
