using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuoiKiLTC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.BaiViets.Include(b => b.TheLoai).ToListAsync());
        }

        // GET: BaiViets/Create
        public IActionResult Create()
        {
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "TenTheLoai");
            return View();
        }

        // POST: BaiViets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiViet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "TenTheLoai", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // GET: BaiViets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "TenTheLoai", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // POST: BaiViets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId")] BaiViet baiViet)
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
            ViewData["TheLoaiId"] = new SelectList(_context.TheLoais, "Id", "TenTheLoai", baiViet.TheLoaiId);
            return View(baiViet);
        }

        // GET: BaiViets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViets
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
            var baiViet = await _context.BaiViets.FindAsync(id);
            _context.BaiViets.Remove(baiViet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiVietExists(int id)
        {
            return _context.BaiViets.Any(e => e.Id == id);
        }
    }
}
