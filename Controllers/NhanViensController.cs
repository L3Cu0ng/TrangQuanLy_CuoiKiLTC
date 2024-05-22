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
    public class NhanViensController : Controller
    {
        private readonly QuanLyCongTyContext _context;

        public NhanViensController(QuanLyCongTyContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        public async Task<IActionResult> Index(string searchString)
        {
            var nhanViens = from nv in _context.NhanViens.Include(n => n.PhongBan)
                            select nv;

            if (!String.IsNullOrEmpty(searchString))
            {
                nhanViens = nhanViens.Where(nv => nv.HoTen.Contains(searchString) ||
                                                   nv.SoDienThoai.Contains(searchString) ||
                                                   nv.Email.Contains(searchString) ||
                                                   nv.DiaChi.Contains(searchString) ||
                                                   nv.ChucVu.Contains(searchString) ||
                                                   nv.PhongBan.TenPhongBan.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await nhanViens.ToListAsync());
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.PhongBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "Id", "TenPhongBan");
            return View();
        }

        // POST: NhanViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,SoDienThoai,Email,DiaChi,NgaySinh,ChucVu,PhongBanId")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "Id", "TenPhongBan", nhanVien.PhongBanId);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "Id", "TenPhongBan", nhanVien.PhongBanId);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,SoDienThoai,Email,DiaChi,NgaySinh,ChucVu,PhongBanId")] NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Id))
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
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "Id", "TenPhongBan", nhanVien.PhongBanId);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.PhongBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.Id == id);
        }
    }
}
