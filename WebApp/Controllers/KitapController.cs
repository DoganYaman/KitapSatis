using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KitapController : Controller
    {
        ApplicationDbContext _db;
       
        public KitapController(ApplicationDbContext db)
        {
            _db = db;
           // db.Database.EnsureDeleted();
           // db.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
           return View(_db.Kitaplar.Include("Yazar").ToList());
        }

        public IActionResult Detay(int id)
        {
            return View(_db.Kitaplar.Include("Yazar").Include("YayinEvi").Where(k=>k.KitapID==id).Single());
        }
    }
}
