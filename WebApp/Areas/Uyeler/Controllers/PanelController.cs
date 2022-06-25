using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApp.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Uyeler.Controllers
{
    [Area("Uyeler")]
    public class PanelController : Controller
    {
        UserManager<Uye> _userManager;
        ApplicationDbContext _db;

        //  private int uyeID;
        public PanelController(ApplicationDbContext db, UserManager<Uye> userManager)
        {
            _db = db;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            int uyeID = int.Parse(_userManager.GetUserId(User));

            var result = _db.Sepetler.Include("Kitap").Where(s => s.UyeID == uyeID).ToList();

            return View(result);
        }

        public IActionResult SepeteEkle(int id)
        {
            //yoksa =>INSERT
            //varsa =>UPDATE

            int uyeID = int.Parse(_userManager.GetUserId(User));

            if (_db.Sepetler.Where(s => s.KitapID == id && s.UyeID == uyeID).Count() > 0)
            {
                //Update
                Sepet sepet = _db.Sepetler.Where(s => s.KitapID == id && s.UyeID == uyeID).Single();
                sepet.Adet += 1;
                _db.Entry<Sepet>(sepet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                //Insert
                Sepet sepet = new Sepet
                {
                    KitapID = id,
                    UyeID = uyeID,
                    Adet = 1
                };

                _db.Sepetler.Add(sepet);
            }
            _db.SaveChanges();

            return Redirect("~/Kitap/Index");
        }


        public IActionResult SepettenCikar(int id)
        {
            

            Sepet sepet = _db.Sepetler.Find(id);

            if (sepet.Adet > 1)
            {
                //Update
                sepet.Adet -= 1;
                _db.Entry(sepet).State = EntityState.Modified;
            }
            else if (sepet.Adet == 1)
            {
                //delete
                _db.Sepetler.Remove(sepet);
            }

            _db.SaveChanges();

            return Redirect("~/Uyeler/Panel/Index");
        }

        public IActionResult SepetiBosalt()
        {
            int uyeID = int.Parse(_userManager.GetUserId(User));
            SepetinTumunuTemizle(uyeID);
           
            return Redirect("~/Uyeler/Panel/Index");
        }

        private void SepetinTumunuTemizle(int uyeID)
        {
            var sonuc = _db.Sepetler.Where(s => s.UyeID == uyeID).ToList();
            _db.Sepetler.RemoveRange(sonuc);
            _db.SaveChanges();
        }
        private bool StokKontrolu(int uyeID,out string detay, out decimal toplamTutar) {
            //true =>var
            bool kontrol = true;
            detay = "";
            var sonuc = _db.Sepetler.Include("Kitap").Where(s => s.UyeID == uyeID).ToList();
            toplamTutar = 0;
            foreach (Sepet sepet in sonuc)
            {
                if (sepet.Adet > sepet.Kitap.StokAdedi)
                {
                    kontrol = false;
                    detay += sepet.Kitap.KitapAdi + "" + sepet.Kitap.StokAdedi +" ";
                   
                }
                toplamTutar += sepet.Adet * sepet.Kitap.Fiyat;
            }

            return kontrol;
        }
        public IActionResult SatinAl()
        {
            //1-Stok Kontorolu
            //2-Satısa Kaydet
            //3-Detaya Kaydet
            //4-Stoktan dus..
            //5-Sepete Bosalt

            int uyeID = int.Parse(_userManager.GetUserId(User));

            if (StokKontrolu(uyeID,out string Mesaj,out decimal toplamTutar))
            {
                //2.Asama
                Satis satis = new Satis() {
                 UyeID =uyeID, 
                  SatisTarihi=DateTime.Now,
                  ToplamTutar =toplamTutar 
                };

                _db.Satislar.Add(satis);
                _db.SaveChanges();

                int satisID = satis.SatisID;

                //3.Asama
                var sepet = _db.Sepetler.Include("Kitap").Where(s => s.UyeID == uyeID).ToList();

                foreach (Sepet s in sepet)
                {
                    //Satis detaya yaz
                    SatisDetay detay = new SatisDetay { 
                      KitapID=s.KitapID,
                      SatisID=satisID,
                       Adet =s.Adet,
                       Fiyat = s.Kitap.Fiyat
                    };

                    _db.SatisDetaylari.Add(detay);
                    //stoktan dus

                    s.Kitap.StokAdedi -= s.Adet;
                    _db.Entry(s.Kitap).State = EntityState.Modified;
                }

                _db.SaveChanges();
                SepetinTumunuTemizle(uyeID);
            }
            else 
            {
                TempData["Mesaj"] = Mesaj;
            }
           

            return Redirect("~/Uyeler/Panel/Index");
        }
    }
}
