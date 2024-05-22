using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using CuoiKiLTC.Models;

namespace CuoiKiLTC.Controllers
{
    public class BaiVietsController : Controller
    {
        private readonly QuanLyCongTyContext _context;
        private readonly string _imageFolderPath = "wwwroot/hinhanh";

        public BaiVietsController(QuanLyCongTyContext context)
        {
            _context = context;
        }

        // GET: BaiViets
        public async Task<IActionResult> Index(string searchString, string searchCategory)
        {
            // IQueryable to hold the filtered and included data
            IQueryable<BaiViet> baiViets;

            // Eagerly load related entities if needed
            baiViets = _context.BaiViets.Include(bv => bv.Admin)
                                         .Include(bv => bv.TheLoai);

            // Filter by search string and search category (if applicable)
            if (!string.IsNullOrEmpty(searchString))
            {
                switch (searchCategory)
                {
                    case "TieuDe":
                        baiViets = baiViets.Where(bv => bv.TieuDe.Contains(searchString));
                        break;
                    case "NoiDung":
                        baiViets = baiViets.Where(bv => bv.NoiDung.Contains(searchString));
                        break;
                    case "NgayDang":
                        // Assuming NgayDang is a DateTime property, you might need to handle it differently
                        break;
                    case "TacGia":
                        baiViets = baiViets.Where(bv => bv.TacGia.Contains(searchString));
                        break;
                    case "TenTheLoai":
                        baiViets = baiViets.Where(bv => bv.TheLoai.TenTheLoai.Contains(searchString));
                        break;
                    default:
                        // Default search by TieuDe
                        baiViets = baiViets.Where(bv => bv.TieuDe.Contains(searchString));
                        break;
                }
            }

            // Pass the current filter values to the view
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCategory"] = searchCategory;

            // Execute the query asynchronously and return the view
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId,AdminId")] BaiViet baiViet, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null && HinhAnh.Length > 0)
                    {
                        var fileName = Path.GetFileName(HinhAnh.FileName);
                        var filePath = Path.Combine(_imageFolderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnh.CopyToAsync(stream);
                        }

                        baiViet.HinhAnhUrl = "/hinhanh/" + fileName;
                    }

                    _context.Add(baiViet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi tại đây
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi tải lên ảnh: " + ex.Message);
                }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDung,NgayDang,TacGia,TheLoaiId,AdminId,HinhAnhUrl")] BaiViet baiViet, IFormFile HinhAnh)
        {
            if (id != baiViet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null && HinhAnh.Length > 0)
                    {
                        var fileName = Path.GetFileName(HinhAnh.FileName);
                        var filePath = Path.Combine(_imageFolderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnh.CopyToAsync(stream);
                        }

                        baiViet.HinhAnhUrl = "/hinhanh/" + fileName;
                    }

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
            return _context.BaiViets.Any(e => e.Id == id);
        }
    }
}
