using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class YayinEviController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YayinEviController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/YayinEvi
        public async Task<IActionResult> Index()
        {
            return View(await _context.YayinEvleri.ToListAsync());
        }

        // GET: Admin/YayinEvi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yayinEvi = await _context.YayinEvleri
                .FirstOrDefaultAsync(m => m.YayinEviID == id);
            if (yayinEvi == null)
            {
                return NotFound();
            }

            return View(yayinEvi);
        }

        // GET: Admin/YayinEvi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/YayinEvi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YayinEviID,YayinEviAdi,Adres,Telefon")] YayinEvi yayinEvi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yayinEvi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yayinEvi);
        }

        // GET: Admin/YayinEvi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yayinEvi = await _context.YayinEvleri.FindAsync(id);
            if (yayinEvi == null)
            {
                return NotFound();
            }
            return View(yayinEvi);
        }

        // POST: Admin/YayinEvi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YayinEviID,YayinEviAdi,Adres,Telefon")] YayinEvi yayinEvi)
        {
            if (id != yayinEvi.YayinEviID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yayinEvi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YayinEviExists(yayinEvi.YayinEviID))
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
            return View(yayinEvi);
        }

        // GET: Admin/YayinEvi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yayinEvi = await _context.YayinEvleri
                .FirstOrDefaultAsync(m => m.YayinEviID == id);
            if (yayinEvi == null)
            {
                return NotFound();
            }

            return View(yayinEvi);
        }

        // POST: Admin/YayinEvi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yayinEvi = await _context.YayinEvleri.FindAsync(id);
            _context.YayinEvleri.Remove(yayinEvi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YayinEviExists(int id)
        {
            return _context.YayinEvleri.Any(e => e.YayinEviID == id);
        }
    }
}
