using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class KitapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Kitap
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kitaplar.Include(k => k.YayinEvi).Include(k => k.Yazar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Kitap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .Include(k => k.YayinEvi)
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // GET: Admin/Kitap/Create
        public IActionResult Create()
        {
            ViewData["YayinEviID"] = new SelectList(_context.YayinEvleri, "YayinEviID", "YayinEviAdi");
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi");
            return View();
        }

        // POST: Admin/Kitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapID,KitapAdi,YazarID,YayinEviID,KapakResmi,Ozet,Fiyat,BasimTarihi,StokAdedi,TavsiyeSayisi")] Kitap kitap,IFormFile dosya)
        {
            if (ModelState.IsValid)
            {

                Guid guid = Guid.NewGuid();
                string dosyaAdi = guid + dosya.FileName;

                FileStream fs = new FileStream("wwwroot/KapakResimleri/" + dosyaAdi, FileMode.Create);
                await dosya.CopyToAsync(fs);
                fs.Close();
                kitap.KapakResmi = dosyaAdi;

               
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YayinEviID"] = new SelectList(_context.YayinEvleri, "YayinEviID", "YayinEviAdi", kitap.YayinEviID);
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi", kitap.YazarID);
            return View(kitap);
        }

        // GET: Admin/Kitap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["YayinEviID"] = new SelectList(_context.YayinEvleri, "YayinEviID", "YayinEviAdi", kitap.YayinEviID);
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi", kitap.YazarID);
            return View(kitap);
        }

        // POST: Admin/Kitap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapID,KitapAdi,YazarID,YayinEviID,KapakResmi,Ozet,Fiyat,BasimTarihi,StokAdedi,TavsiyeSayisi")] Kitap kitap, IFormFile dosya)
        {
            if (id != kitap.KitapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (dosya != null)
                    {
                        if (!kitap.KapakResmi.Contains(dosya.FileName))
                        {
                            Guid guid = Guid.NewGuid();
                            string dosyaAdi = guid + dosya.FileName;

                            FileStream fs = new FileStream("wwwroot/KapakResimleri/" + dosyaAdi, FileMode.Create);
                            await dosya.CopyToAsync(fs);
                            fs.Close();
                            kitap.KapakResmi = dosyaAdi;
                        }
                    }


                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.KitapID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["YayinEviID"] = new SelectList(_context.YayinEvleri, "YayinEviID", "YayinEviAdi", kitap.YayinEviID);
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi", kitap.YazarID);
            return View(kitap);
        }

        // GET: Admin/Kitap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .Include(k => k.YayinEvi)
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Admin/Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            _context.Kitaplar.Remove(kitap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.Kitaplar.Any(e => e.KitapID == id);
        }
    }
}
