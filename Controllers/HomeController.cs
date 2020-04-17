using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaptopProject.Models;

namespace LaptopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Producer()
        {
            return RedirectToAction("Index", "Producers");
        }
        public IActionResult Feature()
        {
            return RedirectToAction("Index", "Features");
        }
        public IActionResult Country()
        {
            return RedirectToAction("Index", "Countries");
        }
        public IActionResult Laptops()
        {
            return RedirectToAction("Index", "Laptops");
        }
            public IActionResult Color()
        {
            return RedirectToAction("Index", "Colors");
        }
        public IActionResult Processor()
        {
            return RedirectToAction("Index", "Processors");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
