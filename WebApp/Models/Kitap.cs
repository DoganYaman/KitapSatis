using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Kitap
    {
        public int KitapID { get; set; }
        [Column(TypeName ="varchar")]
        [StringLength(100)]
        [Required(ErrorMessage ="Bos Gecemezsiniz")]
        [Display(Name ="Kitap Adı")]
        public string KitapAdi { get; set; }
        [Display(Name ="<Yazar Adı>")]
        public int YazarID { get; set; }
        public int YayinEviID { get; set; }
        public string KapakResmi { get; set; }
        public string Ozet { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime BasimTarihi { get; set; }
        public int StokAdedi { get; set; }
        public int TavsiyeSayisi { get; set; }


        public ICollection<KitapKategori> KitapKategoriler { get; set; }
        public Yazar Yazar { get; set; }
        public YayinEvi YayinEvi { get; set; }
        public ICollection<SatisDetay> SatisDetaylari { get; set; }
        public ICollection<Sepet> Sepetler { get; set; }

    }
}
