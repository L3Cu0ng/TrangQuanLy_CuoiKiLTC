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
    public class BaiVietsController : Controller
    {
        private readonly QuanLyCongTyContext _context;

        public BaiVietsController(QuanLyCongTyContext context)
        {
            _context = context;
        }

        // GET: BaiViets
        public async Task<IActionResult> Index(string searchString)
        {
            var baiViets = from bv in _context.BaiViets.Include(b => b.Admin).Include(b => b.TheLoai)
                           select bv;

            if (!String.IsNullOrEmpty(searchString))
            {
                baiViets = baiViets.Where(bv => bv.TieuDe.Contains(searchString) ||
                                                bv.NoiDung.Contains(searchString) ||
                                                bv.TacGia.Contains(searchString) ||
                                                bv.Admin.UserName.Contains(searchString) ||
                                                bv.TheLoai.TenTheLoai.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await baiViets.ToListAsync());
        }

        // GET: BaiViets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaiViets == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
                .Include(b => b.Admin)
                .Include(b => b.TheLoai)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // GET: BaiViets/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id");
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "Id");
            return View();
        }

        // POST: BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId,AdminId")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiViet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", baiViet.AdminId);
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "Id", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // GET: BaiViets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaiViets == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", baiViet.AdminId);
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "Id", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // POST: BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId,AdminId")] BaiViet baiViet)
        {
            if (id != baiViet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiViet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiVietExists(baiViet.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Id", baiViet.AdminId);
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "Id", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // GET: BaiViets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaiViets == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
                .Include(b => b.Admin)
                .Include(b => b.TheLoai)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // POST: BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaiViets == null)
            {
                return Problem("Entity set 'QuanLyCongTyContext.BaiViets'  is null.");
            }
            var baiViet = await _context.BaiViets.FindAsync(id);
            if (baiViet != null)
            {
                _context.BaiViets.Remove(baiViet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiVietExists(int id)
        {
          return (_context.BaiViets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
