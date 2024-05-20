using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            return View(await _context.LichTrinhs.ToListAsync());
        }

        // GET: LichTrinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LichTrinhs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDung,NgayBatDau,NgayKetThuc")] LichTrinh lichTrinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichTrinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lichTrinh);
        }

        // GET: LichTrinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichTrinh = await _context.LichTrinhs.FindAsync(id);
            if (lichTrinh == null)
            {
                return NotFound();
            }
            return View(lichTrinh);
        }

        // POST: LichTrinhs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDung,NgayBatDau,NgayKetThuc")] LichTrinh lichTrinh)
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
            return View(lichTrinh);
        }

        // GET: LichTrinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichTrinh = await _context.LichTrinhs
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
            var lichTrinh = await _context.LichTrinhs.FindAsync(id);
            _context.LichTrinhs.Remove(lichTrinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichTrinhExists(int id)
        {
            return _context.LichTrinhs.Any(e => e.Id == id);
        }
    }
}
