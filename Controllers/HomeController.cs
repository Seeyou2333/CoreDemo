using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreDemo.Models;
using Microsoft.AspNetCore.Authorization;
namespace CoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqlServerDbcontext _db;
        public HomeController(ILogger<HomeController> logger, SqlServerDbcontext db)
        {
            _logger = logger;
            _db = db;
        }

      
        public IActionResult Index(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewData["ReturnUrl"] = returnUrl;
            }
            return View();
        }
        [Authorize]
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
