using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KitapKategoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitapKategoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/KitapKategori
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.KitapKategorileri.Include(k => k.Kategori).Include(k => k.Kitap);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/KitapKategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitapKategori = await _context.KitapKategorileri
                .Include(k => k.Kategori)
                .Include(k => k.Kitap)
                .FirstOrDefaultAsync(m => m.KitapKategoriID == id);
            if (kitapKategori == null)
            {
                return NotFound();
            }

            return View(kitapKategori);
        }

        // GET: Admin/KitapKategori/Create
        public IActionResult Create()
        {
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi");
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "KitapAdi");
            return View();
        }

        // POST: Admin/KitapKategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapKategoriID,KitapID,KategoriID")] KitapKategori kitapKategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitapKategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", kitapKategori.KategoriID);
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "KitapAdi", kitapKategori.KitapID);
            return View(kitapKategori);
        }

        // GET: Admin/KitapKategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitapKategori = await _context.KitapKategorileri.FindAsync(id);
            if (kitapKategori == null)
            {
                return NotFound();
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", kitapKategori.KategoriID);
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "KitapAdi", kitapKategori.KitapID);
            return View(kitapKategori);
        }

        // POST: Admin/KitapKategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapKategoriID,KitapID,KategoriID")] KitapKategori kitapKategori)
        {
            if (id != kitapKategori.KitapKategoriID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitapKategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapKategoriExists(kitapKategori.KitapKategoriID))
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
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", kitapKategori.KategoriID);
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "KitapAdi", kitapKategori.KitapID);
            return View(kitapKategori);
        }

        // GET: Admin/KitapKategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitapKategori = await _context.KitapKategorileri
                .Include(k => k.Kategori)
                .Include(k => k.Kitap)
                .FirstOrDefaultAsync(m => m.KitapKategoriID == id);
            if (kitapKategori == null)
            {
                return NotFound();
            }

            return View(kitapKategori);
        }

        // POST: Admin/KitapKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitapKategori = await _context.KitapKategorileri.FindAsync(id);
            _context.KitapKategorileri.Remove(kitapKategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapKategoriExists(int id)
        {
            return _context.KitapKategorileri.Any(e => e.KitapKategoriID == id);
        }
    }
}
