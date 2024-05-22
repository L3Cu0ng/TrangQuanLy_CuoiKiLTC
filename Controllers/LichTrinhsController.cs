using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CuoiKiLTC.Models;

namespace CuoiKiLTC.Controllers
{
    public class LichTrinhsController : Controller
    {
        private readonly QuanLyCongTyContext _context;

        public LichTrinhsController(QuanLyCongTyContext context)
        {
            _context = context;
        }

        // GET: LichTrinhs
        public async Task<IActionResult> Index(string searchString)
        {
            var lichTrinhs = from lt in _context.LichTrinhs
                             select lt;

            if (!String.IsNullOrEmpty(searchString))
            {
                lichTrinhs = lichTrinhs.Where(lt => lt.TieuDe.Contains(searchString) ||
                                                     lt.NoiDung.Contains(searchString) ||
                                                     lt.NgayBatDau.ToString().Contains(searchString) ||
                                                     lt.NgayKetThuc.ToString().Contains(searchString) ||
                                                     lt.Admin.UserName.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await lichTrinhs.ToListAsync());
        }

        // GET: LichTrinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LichTrinhs == null)
            {
                return NotFound();
            }

            var lichTrinh = await _context.LichTrinhs
                .Include(l => l.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichTrinh == null)
            {
                return NotFound();
            }

            return View(lichTrinh);
        }

        // GET: LichTrinhs/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id");
            return View();
        }

        // POST: LichTrinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDung,NgayBatDau,NgayKetThuc,AdminId")] LichTrinh lichTrinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichTrinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", lichTrinh.AdminId);
            return View(lichTrinh);
        }

        // GET: LichTrinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LichTrinhs == null)
            {
                return NotFound();
            }

            var lichTrinh = await _context.LichTrinhs.FindAsync(id);
            if (lichTrinh == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", lichTrinh.AdminId);
            return View(lichTrinh);
        }

        // POST: LichTrinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDung,NgayBatDau,NgayKetThuc,AdminId")] LichTrinh lichTrinh)
        {
            if (id != lichTrinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichTrinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichTrinhExists(lichTrinh.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", lichTrinh.AdminId);
            return View(lichTrinh);
        }

        // GET: LichTrinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LichTrinhs == null)
            {
                return NotFound();
            }

            var lichTrinh = await _context.LichTrinhs
                .Include(l => l.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichTrinh == null)
            {
                return NotFound();
            }

            return View(lichTrinh);
        }

        // POST: LichTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LichTrinhs == null)
            {
                return Problem("Entity set 'QuanLyCongTyContext.LichTrinhs'  is null.");
            }
            var lichTrinh = await _context.LichTrinhs.FindAsync(id);
            if (lichTrinh != null)
            {
                _context.LichTrinhs.Remove(lichTrinh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichTrinhExists(int id)
        {
          return (_context.LichTrinhs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
