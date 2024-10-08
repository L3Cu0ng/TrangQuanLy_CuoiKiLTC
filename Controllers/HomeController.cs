﻿using CuoiKiLTC.Models;
using CuoiKiLTC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CuoiKiLTC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyCongTyContext _context;

        public HomeController(ILogger<HomeController> logger, QuanLyCongTyContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/TrangChu")]
        public async Task<IActionResult> TrangChu()
        {
            var viewModel = new HomeViewModel
            {
                BaiViets = await _context.BaiViets.Include(b => b.TheLoai).Include(b => b.Admin).ToListAsync(),
                LichTrinhs = await _context.LichTrinhs.Include(l => l.Admin).ToListAsync()
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Action method to render a view without using the shared layout
        public IActionResult ViewWithoutLayout()
        {
            return View("Index", model: null); // Replace "ViewName" with the name of your view
        }
    }
}
